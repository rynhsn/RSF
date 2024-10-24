using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using PMT03500Common;
using PMT03500Common.DTOs;
using PMT03500FrontResources;
using R_APICommonDTO;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using R_ProcessAndUploadFront;

namespace PMT03500Model.ViewModel
{
    public class PMT03500UploadUtilityViewModel : R_IProcessProgressStatus
    {
        public ObservableCollection<PMT03500UploadUtilityErrorValidateDTO> GridListUpload { get; set; } =
            new ObservableCollection<PMT03500UploadUtilityErrorValidateDTO>();

        public PMT03500UploadParam UploadParam { get; set; } = new PMT03500UploadParam();

        public string CompanyId { get; set; }
        public string UserId { get; set; }

        public int TotalRows { get; set; }
        public bool IsError { get; set; } = false;
        public int ValidRows { get; set; }
        public int InvalidRows { get; set; }

        public string Message = "";
        public int Percentage = 0;
        public Action ShowSuccessAction { get; set; }
        public Action StateChangeAction { get; set; }
        public DataSet ExcelDataSet { get; set; }
        public Func<Task> ActionDataSetExcel { get; set; }
        public Action<R_Exception> DisplayErrorAction { get; set; }

        public bool FileHasData = false;

        public void Init(object poParam)
        {
            UploadParam = (PMT03500UploadParam)poParam;
        }

        public async Task SaveBulkFile(PMT03500UploadParam poUploadParam, List<PMT03500UploadUtilityErrorValidateDTO> poDataList)
        {
            var loEx = new R_Exception();
            R_BatchParameter loBatchPar;
            R_ProcessAndUploadClient loCls;
            List<PMT03500UploadUtilityErrorValidateDTO> ListFromExcel;
            List<R_KeyValue> loBatchParUserParameters;

            try
            {
                // set Param
                loBatchParUserParameters = new List<R_KeyValue>();
                loBatchParUserParameters.Add(new R_KeyValue
                    { Key = PMT03500ContextConstant.CPROPERTY_ID, Value = poUploadParam.CPROPERTY_ID });
                loBatchParUserParameters.Add(new R_KeyValue
                    { Key = PMT03500ContextConstant.CUTILITY_TYPE, Value = poUploadParam.EUTILITY_TYPE == EPMT03500UtilityUsageType.EC ? "EC" : "WG" });

                //Instantiate ProcessClient
                loCls = new R_ProcessAndUploadClient(
                    pcModuleName: "PM",
                    plSendWithContext: true,
                    plSendWithToken: true,
                    pcHttpClientName: "R_DefaultServiceUrlPM",
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
                loBatchPar.ClassName = "PMT03500Back.PMT03500UploadUtilityCls";
                loBatchPar.BigObject = ListFromExcel;

                await loCls.R_BatchProcess<List<PMT03500UploadUtilityErrorValidateDTO>>(loBatchPar, 20);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task ConvertGrid(List<PMT03500UploadUtilityExcelDTO> poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                //Onchanged Visible Error
                IsError = false;
                IsError = false;
                ValidRows = 0;
                InvalidRows = 0;

                // Convert Excel DTO And Add SeqNo
                // var loData = poEntity.Select((item, i) => new PMT03500UploadUtilityErrorValidateDTO
                // {
                //     NO = i + 1,
                //     CBUILDING_ID = item.BuildingId,
                //     CDEPT_CODE = item.Department,
                //     CREF_NO = item.AgreementNo,
                //     CUTILITY_TYPE = item.UtilityType,
                //     CPROPERTY_ID = UploadParam.CPROPERTY_ID,
                //     CFLOOR_ID = item.FloorId,
                //     CUNIT_ID = item.UnitId,
                //     CCHARGES_TYPE = item.ChargesType,
                //     CCHARGES_ID = item.ChargesId,
                //     CMETER_NO = item.MeterNo,
                //     CSEQ_NO = item.SeqNo,
                //     CINV_PRD = item.InvoicePeriod,
                //     CUTILITY_PRD = item.UtilityPeriod,
                //     CSTART_DATE = item.StartDate,
                //     CEND_DATE = item.EndDate,
                //     IBLOCK1_START = item.BlockIStart,
                //     IBLOCK2_START = item.BlockIIStart,
                //     IBLOCK1_END = item.BlockIEnd,
                //     IBLOCK2_END = item.BlockIIEnd,
                //     IMETER_START = item.MeterStart,
                //     IMETER_END = item.MeterEnd,
                //
                //     CCOMPANY_ID = CompanyId
                // }).ToList();

                var loData = new List<PMT03500UploadUtilityErrorValidateDTO>();

                if(UploadParam.EUTILITY_TYPE == EPMT03500UtilityUsageType.EC)
                {
                    loData = poEntity.Select((item, i) => new PMT03500UploadUtilityErrorValidateDTO
                    {
                        NO = i + 1,
                        CBUILDING_ID = item.BuildingId,
                        CDEPT_CODE = item.Department,
                        CREF_NO = item.AgreementNo,
                        CUTILITY_TYPE = item.UtilityType,
                        CPROPERTY_ID = UploadParam.CPROPERTY_ID,
                        CFLOOR_ID = item.FloorId,
                        CUNIT_ID = item.UnitId,
                        CCHARGES_TYPE = item.ChargesType,
                        CCHARGES_ID = item.ChargesId,
                        CMETER_NO = item.MeterNo,
                        CSEQ_NO = item.SeqNo,
                        CINV_PRD = item.InvoicePeriod,
                        CUTILITY_PRD = item.UtilityPeriod,
                        CSTART_DATE = item.StartDate,
                        CEND_DATE = item.EndDate,
                        IBLOCK1_START = item.BlockIStart,
                        IBLOCK2_START = item.BlockIIStart,
                        IBLOCK1_END = item.BlockIEnd,
                        IBLOCK2_END = item.BlockIIEnd,
                        NBEBAN_BERSAMA = item.BebanBersama,

                        CCOMPANY_ID = CompanyId
                    }).ToList();
                    
                    //cek apakah di loData ada start date yang lebih besar dari end date dan jika ada maka akan di set error dan property notes akan diisi
                    foreach (var item in loData)
                    {
                        if (int.Parse(item.CSTART_DATE) > int.Parse(item.CEND_DATE))
                        {
                            IsError = true;
                            InvalidRows++;
                            ValidRows = loData.Count - InvalidRows;
                            item.ErrorFlag = "N";
                            item.ErrorMessage = R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "START_DATE_LESS_THAN_END_DATE");
                        }
                    }
                }
                else if(UploadParam.EUTILITY_TYPE == EPMT03500UtilityUsageType.WG)
                {
                    loData = poEntity.Select((item, i) => new PMT03500UploadUtilityErrorValidateDTO
                    {
                        NO = i + 1,
                        CBUILDING_ID = item.BuildingId,
                        CDEPT_CODE = item.Department,
                        CREF_NO = item.AgreementNo,
                        CUTILITY_TYPE = item.UtilityType,
                        CPROPERTY_ID = UploadParam.CPROPERTY_ID,
                        CFLOOR_ID = item.FloorId,
                        CUNIT_ID = item.UnitId,
                        CCHARGES_TYPE = item.ChargesType,
                        CCHARGES_ID = item.ChargesId,
                        CMETER_NO = item.MeterNo,
                        CSEQ_NO = item.SeqNo,
                        CINV_PRD = item.InvoicePeriod,
                        CUTILITY_PRD = item.UtilityPeriod,
                        CSTART_DATE = item.StartDate,
                        CEND_DATE = item.EndDate,
                        IMETER_START = item.MeterStart,
                        IMETER_END = item.MeterEnd,

                        CCOMPANY_ID = CompanyId
                    }).ToList();
                }
                
                
                TotalRows = loData.Count;
                GridListUpload = new ObservableCollection<PMT03500UploadUtilityErrorValidateDTO>(loData);
                await Task.CompletedTask;
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
                    await ServiceGetError(pcKeyGuid);
                    IsError = true;
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
            //IF ERROR CONNECTION, PROGRAM WILL RUN THIS METHOD
            var loException = new R_Exception();

            Message = $"Process Error with GUID {pcKeyGuid}";
            ex.ErrorList.ForEach(x => loException.Add(x.ErrNo, x.ErrDescp));

            DisplayErrorAction.Invoke(loException);
            StateChangeAction();
            await Task.CompletedTask;
        }

        public async Task ReportProgress(int pnProgress, string pcStatus)
        {
            Percentage = pnProgress;
            Message = $"Process Progress {pnProgress} with status {pcStatus}";
            Message = $"Process Progress {pnProgress} with status {pcStatus}";

            StateChangeAction();
            await Task.CompletedTask;
        }

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
                loParameterData.RESOURCE_NAME = UploadParam.EUTILITY_TYPE switch
                {
                    EPMT03500UtilityUsageType.EC => "RSP_PM_UPLOAD_UTILITY_USAGE_ECResources",
                    EPMT03500UtilityUsageType.WG => "RSP_PM_UPLOAD_UTILITY_USAGE_WGResources",
                    _ => loParameterData.RESOURCE_NAME
                };

                loCls = new R_ProcessAndUploadClient(
                    pcModuleName: "PM",
                    plSendWithContext: true,
                    plSendWithToken: true,
                    pcHttpClientName: "R_DefaultServiceUrlPM");

                // Get error result
                loResultData = await loCls.R_GetStreamErrorProcess(loParameterData);

                // check error if unhandle
                if (loResultData.Any(y => y.SeqNo <= 0))
                {
                    var loUnhandledEx = loResultData.Where(y => y.SeqNo <= 0).Select(x =>
                        new R_BlazorFrontEnd.Exceptions.R_Error(x.SeqNo.ToString(), x.ErrorMessage)).ToList();
                    loUnhandledEx.ForEach(x => loException.Add(x));
                }
                else
                {
                    // Display Error Handle if get seq
                    GridListUpload.ToList().ForEach(x =>
                    {
                        //Assign ErrorMessage, ErrorFlag and Set Valid And Invalid Data
                        if (loResultData.Any(y => y.SeqNo == x.NO))
                        {
                            x.ErrorMessage = loResultData.Where(y => y.SeqNo == x.NO).FirstOrDefault().ErrorMessage;
                            x.ErrorFlag = "N";
                            InvalidRows++;
                        }
                        else
                        {
                            x.ErrorFlag = "Y";
                            ValidRows++;
                        }
                    });

                    if (UploadParam.EUTILITY_TYPE == EPMT03500UtilityUsageType.EC)
                    {
                        var loConvertData = GridListUpload.Select(item => new PMT03500UploadUtilityExcelECDTO
                        {
                            DisplaySeq = item.NO.ToString(),
                            BuildingId = item.CBUILDING_ID,
                            Department = item.CDEPT_CODE,
                            AgreementNo = item.CREF_NO,
                            UtilityType = item.CUTILITY_TYPE,
                            FloorId = item.CFLOOR_ID,
                            UnitId = item.CUNIT_ID,
                            ChargesType = item.CCHARGES_TYPE,
                            ChargesId = item.CCHARGES_ID,
                            MeterNo = item.CMETER_NO,
                            SeqNo = item.CSEQ_NO,
                            InvoicePeriod = item.CINV_PRD,
                            UtilityPeriod = item.CUTILITY_PRD,
                            StartDate = item.CSTART_DATE,
                            EndDate = item.CEND_DATE,
                            BlockIStart = item.IBLOCK1_START,
                            BlockIIStart = item.IBLOCK2_START,
                            BlockIEnd = item.IBLOCK1_END,
                            BlockIIEnd = item.IBLOCK2_END,
                            BebanBersama = item.NBEBAN_BERSAMA,
                            Valid = item.ErrorFlag,
                            Notes = item.ErrorMessage
                        }).ToList();

                        ////Set DataSetTable and get error
                        var loDataTable = R_FrontUtility.R_ConvertTo(loConvertData);
                        loDataTable.TableName = "UtilityUsage";

                        var loDataSet = new DataSet();
                        loDataSet.Tables.Add(loDataTable);
                        
                        // Asign Dataset
                        ExcelDataSet = loDataSet;

                        //// Dowload if get Error
                        //await ActionDataSetExcel.Invoke();
                    }
                    else if (UploadParam.EUTILITY_TYPE == EPMT03500UtilityUsageType.WG)
                    {
                        var loConvertData = GridListUpload.Select(item => new PMT03500UploadUtilityExcelWGDTO
                        {
                            DisplaySeq = item.NO.ToString(),
                            BuildingId = item.CBUILDING_ID,
                            Department = item.CDEPT_CODE,
                            AgreementNo = item.CREF_NO,
                            UtilityType = item.CUTILITY_TYPE,
                            FloorId = item.CFLOOR_ID,
                            UnitId = item.CUNIT_ID,
                            ChargesType = item.CCHARGES_TYPE,
                            ChargesId = item.CCHARGES_ID,
                            MeterNo = item.CMETER_NO,                            
                            SeqNo = item.CSEQ_NO,
                            InvoicePeriod = item.CINV_PRD,
                            UtilityPeriod = item.CUTILITY_PRD,
                            StartDate = item.CSTART_DATE,
                            EndDate = item.CEND_DATE,
                            MeterStart = item.IMETER_START,
                            MeterEnd = item.IMETER_END,
                            Valid = item.ErrorFlag,
                            Notes = item.ErrorMessage
                        }).ToList();
                        
                        var loDataTable = R_FrontUtility.R_ConvertTo(loConvertData);
                        loDataTable.TableName = "UtilityUsage";

                        var loDataSet = new DataSet();
                        loDataSet.Tables.Add(loDataTable);

                        // Asign Dataset
                        ExcelDataSet = loDataSet;
                    }
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();
        }

        #endregion
    }
    
    public class PMT03500UndoParam
    {
        public string CCOMPANY_ID { get; set; }
        public string CUSER_ID { get; set; }
        public string CPROPERTY_ID { get; set; }
        public string CCHARGES_TYPE { get; set; }
        public string CBUILDING_ID { get; set; }
        public string CINV_PRD { get; set; }
    }


    public class PMT03500UploadParam
    {
        public EPMT03500UtilityUsageType EUTILITY_TYPE { get; set; }
        public string CPROPERTY_ID { get; set; }
        public string CPROPERTY_NAME { get; set; }
    }

    public class PMT03500DetailParam
    {
        public EPMT03500UtilityUsageType EUTILITY_TYPE { get; set; }
        public PMT03500UtilityUsageDTO OUTILITY_HEADER { get; set; }
    }
}