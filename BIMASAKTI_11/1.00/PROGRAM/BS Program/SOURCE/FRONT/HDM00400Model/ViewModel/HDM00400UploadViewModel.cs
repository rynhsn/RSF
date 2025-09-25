using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using HDM00400Common;
using HDM00400Common.DTOs.Upload;
using R_APICommonDTO;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using R_ProcessAndUploadFront;
using R_Error = R_BlazorFrontEnd.Exceptions.R_Error;

namespace HDM00400Model.ViewModel
{
    public class HDM00400UploadViewModel : R_IProcessProgressStatus
    {
        public ObservableCollection<HDM00400UploadForSystemDTO> GridListUpload =
            new ObservableCollection<HDM00400UploadForSystemDTO>();

        public HDM00400UploadParam UploadParam { get; set; } = new HDM00400UploadParam();

        public string CompanyId { get; set; }
        public string UserId { get; set; }

        public int TotalRows { get; set; }
        public bool IsError { get; set; } = false;
        public int ValidRows { get; set; }
        public int InvalidRows { get; set; }

        public bool FileHasData { get; set; } = false;


        public string Message = "";
        public int Percentage = 0;

        public Action ShowSuccessAction { get; set; }
        public Action StateChangeAction { get; set; }
        public Action<R_Exception> DisplayErrorAction { get; set; }
        public List<R_Error> ErrorList { get; set; } = new List<R_Error>();

        public DataSet ExcelDataSet { get; set; }
        public Func<Task> ActionDataSetExcel { get; set; }

        public void Init(object poParam)
        {
            UploadParam = (HDM00400UploadParam)poParam;
        }

        public async Task ConvertGrid(List<HDM00400UploadFromFileDTO> poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                //Onchanged Visible Error
                IsError = false;
                ValidRows = 0;
                InvalidRows = 0;

                //Convert Collection excel to Collection DTO to display in Grid
                var loData = R_FrontUtility.ConvertCollectionToCollection<HDM00400UploadForSystemDTO>(poEntity);

                loData = poEntity.Select((item, i) => new HDM00400UploadForSystemDTO
                {
                    NO = i + 1,
                    PublicLocId = item.PublicLocationId,
                    PublicLocName = item.PublicLocationName,
                    BuildingId = item.BuildingId,
                    FloorId = item.FloorId,
                    Description = item.Description,
                    Active = item.Active,
                    NonActiveDate = item.NonActiveDate
                }).ToList();

                // isi NO setiap data di loData
                for (var i = 0; i < loData.Count; i++)
                {
                    loData[i].NO = i + 1;

                    
                    // cek apakah NonActionDate bukan 8 angka maka set empty
                    if (loData[i].NonActiveDate.Length != 8)
                    {
                        loData[i].NonActiveDate = string.Empty;
                    }
                    
                    //cek jika ada Active yang isinya 1 maka NonActiveDate harus kosongkan
                    if (loData[i].Active)
                    {
                        loData[i].NonActiveDate = string.Empty;
                    }
                    else
                    {
                        loData[i].DNonActiveDate = DateTime.TryParseExact(loData[i].NonActiveDate, "yyyyMMdd",
                            System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None,
                            out var loDate)
                            ? loDate
                            : (DateTime?)null;
                    }
                }

                TotalRows = loData.Count;
                GridListUpload = new ObservableCollection<HDM00400UploadForSystemDTO>(loData);
                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task SaveBulkFile(HDM00400UploadParam poUploadParam, List<HDM00400UploadForSystemDTO> poDataList)
        {
            var loEx = new R_Exception();
            R_BatchParameter loBatchPar;
            R_ProcessAndUploadClient loCls;
            List<HDM00400UploadForSystemDTO> ListFromExcel;
            List<R_KeyValue> loBatchParUserParameters;

            try
            {
                // set Param
                loBatchParUserParameters = new List<R_KeyValue>();
                loBatchParUserParameters.Add(new R_KeyValue
                    { Key = HDM00400ContextConstant.CPROPERTY_ID, Value = poUploadParam.CPROPERTY_ID });

                //Instantiate ProcessClient
                loCls = new R_ProcessAndUploadClient(
                    pcModuleName: "HD",
                    plSendWithContext: true,
                    plSendWithToken: true,
                    pcHttpClientName: "R_DefaultServiceUrlHD",
                    poProcessProgressStatus: this);

                //Set Data
                if (poDataList.Count == 0)
                    return;

                ListFromExcel = poDataList.ToList();

                //preapare Batch Parameter
                loBatchPar = new R_BatchParameter();
                loBatchPar.COMPANY_ID = CompanyId;
                loBatchPar.USER_ID = UserId;
                loBatchPar.UserParameters = loBatchParUserParameters;
                loBatchPar.ClassName = "HDM00400Back.HDM00400UploadCls";
                loBatchPar.BigObject = ListFromExcel;

                await loCls.R_BatchProcess<List<HDM00400UploadForSystemDTO>>(loBatchPar, 10);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }


        #region ProgressStatus

        public async Task ProcessComplete(string pcKeyGuid, eProcessResultMode poProcessResultMode)
        {
            var loEx = new R_Exception();
            try
            {
                if (poProcessResultMode == eProcessResultMode.Success)
                {
                    Message = $"Process Complete and success with GUID {pcKeyGuid}";
                    IsError = false;
                    ShowSuccessAction();
                }

                if (poProcessResultMode == eProcessResultMode.Fail)
                {
                    Message = $"Process Complete but fail with GUID {pcKeyGuid}";
                    IsError = true;
                    await ServiceGetError(pcKeyGuid);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            StateChangeAction();

            loEx.ThrowExceptionIfErrors();
        }

        public async Task ProcessError(string pcKeyGuid, R_APIException ex)
        {
            // IF ERROR CONNECTION, PROGRAM WILL RUN THIS METHOD
            var loException = new R_Exception();

            Message = $"Process Error with GUID {pcKeyGuid}";
            // ex.ErrorList.ForEach(x => loException.Add(x.ErrNo, x.ErrDescp));
            ErrorList.ForEach(x=>loException.Add(x.ErrNo, x.ErrDescp));
            
            DisplayErrorAction.Invoke(loException);
            // DisplayErrorAction(loException);
            StateChangeAction();
            await Task.CompletedTask;
        }

        public async Task ReportProgress(int pnProgress, string pcStatus)
        {
            Percentage = pnProgress;
            Message = $"Process Progress {pnProgress} with status {pcStatus}";

            StateChangeAction();
            await Task.CompletedTask;
        }

        #endregion


        private async Task ServiceGetError(string pcKeyGuid)
        {
            var loException = new R_Exception();

            List<R_ErrorStatusReturn> loResultData;
            R_GetErrorWithMultiLanguageParameter loParameterData;
            R_ProcessAndUploadClient loCls;
            try
            {
                // Add Parameter
                loParameterData = new R_GetErrorWithMultiLanguageParameter();
                loParameterData.COMPANY_ID = CompanyId;
                loParameterData.USER_ID = UserId;
                loParameterData.KEY_GUID = pcKeyGuid;
                loParameterData.RESOURCE_NAME = "RSP_HD_UPLOAD_PUBLIC_LOCResources";

                loCls = new R_ProcessAndUploadClient(
                    pcModuleName: "HD",
                    plSendWithContext: true,
                    plSendWithToken: true,
                    pcHttpClientName: "R_DefaultServiceUrlHD");

                // Get error result
                loResultData = await loCls.R_GetStreamErrorProcess(loParameterData);
                loResultData.ForEach(x => loException.Add(x.SeqNo.ToString(), x.ErrorMessage));

                // check error if unhandle
                if (loResultData.Any(y => y.SeqNo <= 0))
                {
                    var loUnhandledEx = loResultData.Where(y => y.SeqNo <= 0).Select(x =>
                        new R_Error(x.SeqNo.ToString(), x.ErrorMessage)).ToList();
                    ErrorList = new List<R_Error>(loUnhandledEx);
                    loUnhandledEx.ForEach(x => loException.Add(x));
                }

                if (loResultData.Any(y => y.SeqNo > 0))
                {
                    // Display Error Handle if get seq
                    GridListUpload.ToList().ForEach(x =>
                    {
                        //Assign ErrorMessage, ErrorFlag and Set Valid And Invalid Data
                        if (loResultData.Any(y => y.SeqNo == x.NO))
                        {
                            x.ErrorMessage = loResultData.Where(y => y.SeqNo == x.NO).FirstOrDefault().ErrorMessage;
                            x.ValidFlag = "N";
                            InvalidRows++;
                        }
                        else
                        {
                            x.ValidFlag = "Y";
                            ValidRows++;
                        }
                    });

                    var loConvertData = GridListUpload.Select(item => new HDM00400UploadExcelDTO()
                    {
                        No = item.NO.ToString(),
                        PublicLocationId = item.PublicLocId,
                        PublicLocationName = item.PublicLocName,
                        BuildingId = item.BuildingId,
                        FloorId = item.FloorId,
                        Description = item.Description,
                        Active = item.Active,
                        NonActiveDate = item.NonActiveDate,
                        Valid = item.ValidFlag,
                        Notes = item.ErrorMessage
                    }).ToList();

                    ////Set DataSetTable and get error
                    var loDataTable = R_FrontUtility.R_ConvertTo(loConvertData);
                    loDataTable.TableName = "PublicLocation";

                    var loDataSet = new DataSet();
                    loDataSet.Tables.Add(loDataTable);

                    // Asign Dataset
                    ExcelDataSet = loDataSet;

                    //// Dowload if get Error
                    //await ActionDataSetExcel.Invoke();
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();
        }
    }
}