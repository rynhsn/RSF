using PMM10000COMMON.Upload;
using PMM10000COMMON.UtilityDTO;
using R_APICommonDTO;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using R_ProcessAndUploadFront;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Design;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using R_Error = R_BlazorFrontEnd.Exceptions.R_Error;

namespace PMM10000MODEL.ViewModel
{
    public class PMM10000ViewModel_Upload : R_IProcessProgressStatus
    {

        public ObservableCollection<CallTypeErrorDTO> CallTypeGrid { get; set; } = new ObservableCollection<CallTypeErrorDTO>();
        public Action<R_Exception>? DisplayErrorAction { get; set; }
        public Action? ShowSuccessAction { get; set; }
        public Action? StateChangeAction { get; set; }
        public DataSet? ExcelDataSetError { get; set; }
        public Func<Task>? ActionDataSetExcel { get; set; }
        // Func Proses is Success
        public Func<Task>? ActionIsCompleteSuccess { get; set; }
        public List<R_Error> ErrorList { get; set; } = new List<R_Error>();
        public string Message = "";
        public int Percentage = 0;
        public string? PropertyId { get; set; } = "";
        public PMM10000DbParameterDTO? _parameterUpload = new PMM10000DbParameterDTO();

        #region Public Summary Upload
        public int SumListExcel { get; set; }
        public int SumValidDataExcel { get; set; }
        public int SumInvalidDataExcel { get; set; }
        public string lcFilterResult { get; set; } = "";
        #endregion
        public string? CompanyID { get; set; }
        public string? UserId { get; set; }
        public bool IsError { get; set; }

        //ASSIGN NO to ALL DATA
        public async Task ConvertGrid(List<CallTypeUploadExcelDTO> poEntity)
        {
            R_Exception loException = new R_Exception();
            try
            {
                IsError = false;
                SumValidDataExcel = 0;
                SumInvalidDataExcel = 0;

                // Convert Excel DTO and add SeqNo
                List<CallTypeErrorDTO> Data = poEntity.Select((item, i)
                    => new CallTypeErrorDTO
                    {
                        No = i + 1,
                        CallTypeId = item.CallTypeId,
                        CallTypeName = item.CallTypeName,
                        Category = item.Category,
                        Days = item.Days,
                        Hours = item.Hours,
                        Minutes = item.Minutes,
                        LinkToPriorityApps = item.LinkToPriorityApps,
                    }).ToList();

                SumListExcel = Data.Count;
                CallTypeGrid = new ObservableCollection<CallTypeErrorDTO>(Data);
                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }
        //PROCESS SEND EXCEL DATA
        public async Task SaveFileBulk()
        {
            var loEx = new R_Exception();
            R_BatchParameter loBatchPar;
            R_ProcessAndUploadClient loCls;
            List<CallTypeErrorDTO> ListFromExcel;
            List<R_KeyValue> loBatchParUserParameters;

            try
            {
                // set Param
                loBatchParUserParameters = new List<R_KeyValue>
                {
                    new R_KeyValue()
                    { Key = ContextConstant.CPROPERTY_ID, Value = _parameterUpload.CPROPERTY_ID }
                };

                //Instantiate ProcessClient
                loCls = new R_ProcessAndUploadClient(
                    pcModuleName: "PM",
                    plSendWithContext: true,
                    plSendWithToken: true,
                    pcHttpClientName: "R_DefaultServiceUrlPM",
                    poProcessProgressStatus: this);

                //Set Data
                if (CallTypeGrid.Count == 0)
                    return;

                ListFromExcel = CallTypeGrid.ToList();

                //preapare Batch Parameter
                loBatchPar = new R_BatchParameter
                {
                    COMPANY_ID = CompanyID!,
                    USER_ID = UserId!,
                    UserParameters = loBatchParUserParameters,
                    ClassName = "PMM10000BACK.PMM10000UploadCls",
                    BigObject = ListFromExcel
                };

                await loCls.R_BatchProcess<List<CallTypeErrorDTO>>(loBatchPar, 9);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task ProcessComplete(string pcKeyGuid, eProcessResultMode poProcessResultMode)
        {
            var loEx = new R_Exception();

            try
            {
                if (poProcessResultMode == eProcessResultMode.Success)
                {
                    Message = string.Format("Process Complete and success", pcKeyGuid);
                    IsError = false;
                    ShowSuccessAction();
                }

                if (poProcessResultMode == eProcessResultMode.Fail)
                {
                    Message = string.Format("Process Complete but fail", pcKeyGuid);
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
            R_Exception loException = new R_Exception();
            Message = string.Format("Process Error with GUID {0}", pcKeyGuid);
            ErrorList.ForEach(x => loException.Add(x.ErrNo, x.ErrDescp));

           // loException = R_FrontUtility.R_ConvertFromAPIException(ErrorListAPI);
            DisplayErrorAction.Invoke(loException);
            StateChangeAction();
            await Task.CompletedTask;
        }

        public async Task ReportProgress(int pnProgress, string pcStatus)
        {
            Message = string.Format("Process Progress {0} with status {1}", pnProgress, pcStatus);

            Percentage = pnProgress;
            Message = string.Format("Process Progress {0} with status {1}", pnProgress, pcStatus);

            // Call Method Action StateHasChange
            StateChangeAction();

            await Task.CompletedTask;
        }
        private async Task ServiceGetError(string pcKeyGuid)
        {
            R_Exception loException = new R_Exception();

            List<R_ErrorStatusReturn> loResultData;
            R_GetErrorWithMultiLanguageParameter loParameterData;
            R_ProcessAndUploadClient loCls;
            DataTable loDataTable;
            try
            {
                // Add Parameter
                loParameterData = new R_GetErrorWithMultiLanguageParameter()
                {
                    COMPANY_ID = CompanyID!,
                    USER_ID = UserId!,
                    KEY_GUID = pcKeyGuid,
                    RESOURCE_NAME = "RSP_PM_MAINTAIN_SLA_CALL_TYPEResources"
                };

                loCls = new R_ProcessAndUploadClient(pcModuleName: "PM",
                    plSendWithContext: true,
                    plSendWithToken: true,
                    pcHttpClientName: "R_DefaultServiceUrlPM");

                // Get error result
                loResultData = await loCls.R_GetStreamErrorProcess(loParameterData);
                loResultData.ForEach(x => loException.Add(x.SeqNo.ToString(), x.ErrorMessage));

                // check error if unhandle
                if (loResultData.Any(y => y.SeqNo <= 0))
                {
                    var loUnhandleEx = loResultData.Select(x => new R_Error(x.SeqNo.ToString(), x.ErrorMessage)).ToList();
                    ErrorList = new List<R_Error>(loUnhandleEx);
                    loUnhandleEx.ForEach(x => loException.Add(x));
                }

                if (loResultData.Any(y => y.SeqNo > 0))
                {
                    // Display Error Handle if get seq Agreement
                    CallTypeGrid.ToList().ForEach(x =>
                    {
                        //Assign ErrorMessage, Valid and Set Valid And Invalid Data
                        if (loResultData.Any(y => y.SeqNo == x.No))
                        {
                            x.ErrorMessage = loResultData.Where(y => y.SeqNo == x.No).FirstOrDefault().ErrorMessage;
                            x.ErrorFlag = "N";
                            SumInvalidDataExcel++;
                        }
                        else
                        {
                            x.ErrorFlag = "Y";
                            SumValidDataExcel++;
                        }
                    });

                    List<CallTypeDownloadWithErrorDTO> loData = CallTypeGrid.Select((item)
                    => new CallTypeDownloadWithErrorDTO
                    {
                        CallTypeId = item.CallTypeId,
                        CallTypeName = item.CallTypeName,
                        Category = item.Category,
                        Days = item.Days,
                        Hours = item.Hours,
                        Minutes = item.Minutes,
                        LinkToPriorityApps = item.LinkToPriorityApps,
                        Valid = item.ErrorFlag,
                        Notes = item.ErrorMessage
                    }).ToList();

                  //  ErrorListAPI = R_FrontUtility.R_ConvertToAPIException(loException);

                    loDataTable = R_FrontUtility.R_ConvertTo(loData);
                    loDataTable.TableName = "SLAMaster";

                    var loDataSet = new DataSet();
                    loDataSet.Tables.Add(loDataTable);

                    // Assign Dataset
                    ExcelDataSetError = loDataSet;

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
