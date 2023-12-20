using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using GSM04500Common;
using GSM04500Common.DTOs;
using R_APICommonDTO;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using R_ProcessAndUploadFront;

namespace GSM04500Model.ViewModel
{
    public class GSM04500UploadViewModel : R_IProcessProgressStatus
    {
        public ObservableCollection<GSM04500UploadFromSystemDTO> UploadedList = new ObservableCollection<GSM04500UploadFromSystemDTO>();
        public GSM04500ParamDTO Param = new GSM04500ParamDTO();
        
        public int TotalRows { get; set; }
        public int ValidRows { get; set; }
        public int InvalidRows { get; set; }
        public bool HasData { get; set; }
        public bool IsError { get; set; }

        public string Message = "";
        public int Percentage = 0;
        public string keyGuid { get; set; }

        public Action StateChangeAction { get; set; }

        public Action<R_Exception> ShowErrorAction { get; set; }        
        public Action ShowSuccessAction { get; set; }

        public Func<Task> ActionDataSetExcel { get; set; }

        public DataSet ExcelDataSet { get; set; }

        public void Init(object poParam)
        {
            Param = (GSM04500ParamDTO) poParam;
        }
        public void AssignData(IEnumerable<GSM04500UploadFromFileDTO> poData)
        {
            var loReturn = poData.Select((x, i) => new GSM04500UploadFromSystemDTO
            {
                No = i + 1,
                JournalGroup = x.JournalGroup,
                JournalGroupName = x.JournalGroupName,
                EnableAccrual = x.EnableAccrual
            }).ToList();

            TotalRows = loReturn.Count;
            HasData = TotalRows > 0;
            ValidRows = 0;
            InvalidRows = 0;
            IsError = false;

            UploadedList = new ObservableCollection<GSM04500UploadFromSystemDTO>(loReturn);
        }
        
        public async Task UploadFile(List<GSM04500UploadFromSystemDTO> poBigObject)
        {
            var loEx = new R_Exception();
            R_BatchParameter loUploadPar;
            R_ProcessAndUploadClient loCls;
            // List<GSM04500UploadFromSystemDTO> Bigobject;
            List<R_KeyValue> loUserParameters;
            // R_IProcessProgressStatus loProgressStatus;

            try
            {
                if (this.UploadedList.Count == 0)
                    return;
                
                loUserParameters = new List<R_KeyValue>()
                {
                    new R_KeyValue(){ Key = GSM04500ContextConstant.CPROPERTY_ID, Value = Param.CPROPERTY_ID },
                    new R_KeyValue(){ Key = GSM04500ContextConstant.CJRNGRP_TYPE, Value = Param.CJRNGRP_TYPE }
                };
                
                //prepare Batch Parameter
                loUploadPar = new R_BatchParameter()
                {
                    COMPANY_ID = Param.CCOMPANY_ID,
                    USER_ID = Param.CUSER_ID,
                    ClassName = "GSM04500Back.GSM04500UploadCls",
                    BigObject = poBigObject,
                    UserParameters = loUserParameters
                };

                //Instantiate ProcessClient
                loCls = new R_ProcessAndUploadClient(
                    pcModuleName: "GS",
                    plSendWithContext: true,
                    plSendWithToken: true,
                    pcHttpClientName: "R_DefaultServiceUrl",
                    poProcessProgressStatus: this);

                keyGuid = await loCls.R_BatchProcess<List<GSM04500UploadFromSystemDTO>>(loUploadPar, 10);

                UploadedList = new ObservableCollection<GSM04500UploadFromSystemDTO>(poBigObject);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        
        #region ProgressBar
        public async Task ProcessComplete(string pcKeyGuid, eProcessResultMode poProcessResultMode)
        {
            var loEx = new R_Exception();
            try
            {
                if (poProcessResultMode == eProcessResultMode.Success)
                {
                    Message = "Process Complete and success";
                    ShowSuccessAction();
                    IsError = false;
                }

                if (poProcessResultMode == eProcessResultMode.Fail)
                {
                    IsError = true;
                    Message = "Process Complete but fail";
                    await ServiceGetError(pcKeyGuid);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            StateChangeAction();
            await Task.CompletedTask;
        }

        public async Task ProcessError(string pcKeyGuid, R_APIException ex)
        {
            var loException = new R_Exception();

            Message = $"Process Error with GUID {pcKeyGuid}";
            ex.ErrorList.ForEach(x => loException.Add(x.ErrNo, x.ErrDescp));

            ShowErrorAction.Invoke(loException);
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
        #endregion
        
        #region getError
        
            private async Task ServiceGetError(string pcKeyGuid)
            {
                var loException = new R_Exception();

                List<R_ErrorStatusReturn> loResultData;
                R_GetErrorWithMultiLanguageParameter loParameterData;
                R_ProcessAndUploadClient loCls;

                try
                {
                    // Add Parameter
                    loParameterData = new R_GetErrorWithMultiLanguageParameter()
                    {
                        COMPANY_ID = Param.CCOMPANY_ID,
                        USER_ID = Param.CUSER_ID,
                        KEY_GUID = pcKeyGuid,
                        RESOURCE_NAME = "RSP_GS_UPLOAD_JOURNAL_GROUPResources"
                    };

                    loCls = new R_ProcessAndUploadClient(
                        pcModuleName: "GS",
                        plSendWithContext: true,
                        plSendWithToken: true,
                        pcHttpClientName: "R_DefaultServiceUrl");

                    // Get error result
                    loResultData = await loCls.R_GetStreamErrorProcess(loParameterData);

                    // check error if unhandle, jika nilai dari seq negatif maka error unhandle maka dipisahkan disini
                    if (loResultData.Any(y => y.SeqNo <= 0))
                    {
                        var loUnhandleEx = loResultData.Where(y => y.SeqNo <= 0).Select(x =>
                            new R_BlazorFrontEnd.Exceptions.R_Error(x.SeqNo.ToString(), x.ErrorMessage)).ToList();
                        loUnhandleEx.ForEach(x => loException.Add(x));
                    }
                    // ERROR, jika nilai dari seq positif maka error handle dari data yang diinput
                    if (loResultData.Any(y => y.SeqNo > 0))
                    {
                        // Display Error Handle if get seq
                        UploadedList.ToList().ForEach(x =>
                        {
                            //Assign ErrorMessage, ErrorFlag and Set Valid And Invalid Data
                            if (loResultData.Any(y => y.SeqNo == x.No))
                            {
                                x.ErrorMessage = loResultData.Where(y => y.SeqNo == x.No).FirstOrDefault().ErrorMessage;
                                x.ErrorFlag = "N";
                                InvalidRows++;
                            }
                            else
                            {
                                ValidRows++;
                            }
                        });

                        // Convert DB DTO => excel, for user downlaod
                        List<GSM04500UploadFromFileDTO> loData = UploadedList.Select((item)
                            => new GSM04500UploadFromFileDTO()
                            {
                                JournalGroup = item.JournalGroup,
                                JournalGroupName = item.JournalGroupName,
                                EnableAccrual = item.EnableAccrual,
                                Notes = item.ErrorMessage
                            }).ToList();
                        //   Set DataSetTable and get error
                        //var loExcelData =
                        //    R_FrontUtility.ConvertCollectionToCollection<GSM04500UploadFromExcelDTO>(JournalGroupValidateUploadError);

                        var loDataTable = R_FrontUtility.R_ConvertTo(loData);
                        loDataTable.TableName = "Journal Group";

                        var loDataSet = new DataSet();
                        loDataSet.Tables.Add(loDataTable);

                        // Assign Dataset
                        ExcelDataSet = loDataSet;

                        //// Download if get Error
                        //await ActionDataSetExcel.Invoke();
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
}