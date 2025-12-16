using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_ProcessAndUploadFront;
using R_CommonFrontBackAPI;
using R_APICommonDTO;
using FAT00100Common.DTOs;
using FAT00100FrontResources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FAT00100Model.VMs
{
    /// <summary>
    /// Batch ViewModel for FAT0010002 - Expense Allocation Batch processing
    /// Implements R_IProcessProgressStatus for batch operations
    /// </summary>
    public class FAT0010002ExpenseAllocationBatchViewModel : R_IProcessProgressStatus
    {
        public Action<R_APIException>? ShowErrorAction { get; set; }
        public Action? StateChangeAction { get; set; }
        public Action? ShowSuccessAction { get; set; }
        public string Message { get; set; } = string.Empty;
        public int Percentage { get; set; }
        public bool VisibleError { get; set; }

        // Private fields to store COMPANY_ID and USER_ID for multi-language error retrieval
        private string _companyId = string.Empty;
        private string _userId = string.Empty;

        /// <summary>
        /// Save batch data - main batch processing method
        /// NET4: btnSaveAllocExpense_Click (lines 2056-2140)
        /// </summary>
        /// <param name="poParameter">Batch parameter containing data and user parameters</param>
        /// <param name="pcLangId">Language ID (from IClientHelper context)</param>
        public async Task R_SaveBatchAsync(R_SaveBatchParameterDTO poParameter, string pcLangId)
        {
            R_Exception loEx = new R_Exception();
            R_BatchParameter loBatchPar;
            R_ProcessAndUploadClient loCls;
            string lcGuid = "";
            List<FAT0010002CommonDTO> loBigObject;
            var loUserParam = new List<R_KeyValue>();

            try
            {
                // Validate data
                if (poParameter.Data == null || poParameter.Data.Count == 0)
                {
                    loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "PS018"));
                    loEx.ThrowExceptionIfErrors();
                    return;
                }

                // Store company and user ID for error retrieval with multi-language support
                _companyId = poParameter.CCOMPANY_ID;
                _userId = poParameter.CUSER_ID;

                // Set custom UserParameters from poParameter.UserParameters
                // NET4: Sets CDEPT_CODE, CTRANSACTION_CODE, CREFERENCE_NO, CASSET_CODE, CASSET_TRANS_SEQNO
                loUserParam.Add(new R_KeyValue { Key = "CDEPT_CODE", Value = poParameter.UserParameters.CDEPT_CODE });
                loUserParam.Add(new R_KeyValue { Key = "CTRANSACTION_CODE", Value = poParameter.UserParameters.CTRANSACTION_CODE });
                loUserParam.Add(new R_KeyValue { Key = "CREFERENCE_NO", Value = poParameter.UserParameters.CREFERENCE_NO });
                loUserParam.Add(new R_KeyValue { Key = "CASSET_CODE", Value = poParameter.UserParameters.CASSET_CODE });
                loUserParam.Add(new R_KeyValue { Key = "CASSET_TRANS_SEQNO", Value = poParameter.UserParameters.CASSET_TRANS_SEQNO });

                loCls = new R_ProcessAndUploadClient(
                    pcModuleName: "FA",
                    plSendWithContext: true,
                    plSendWithToken: true,
                    pcHttpClientName: "R_DefaultServiceUrlFA",
                    poProcessProgressStatus: this);

                // Convert Data to BigObject (List<FAT0010002CommonDTO>)
                loBigObject = poParameter.Data.ToList();

                // Prepare Batch Parameter
                loBatchPar = new R_BatchParameter();

                loBatchPar.COMPANY_ID = poParameter.CCOMPANY_ID;
                loBatchPar.USER_ID = poParameter.CUSER_ID;
                loBatchPar.ClassName = "FAT00100Back.FAT0010002BatchCls";
                loBatchPar.UserParameters = loUserParam;
                loBatchPar.BigObject = loBigObject;

                lcGuid = await loCls.R_BatchProcess<List<FAT0010002CommonDTO>>(loBatchPar, loBigObject.Count > 0 ? loBigObject.Count : 1);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        /// <summary>
        /// Called when batch process completes
        /// NET4: ProcessComplete (lines 1989-2030)
        /// </summary>
        public async Task ProcessComplete(string pcKeyGuid, eProcessResultMode poProcessResultMode)
        {
            R_APIException loException = new R_APIException();

            try
            {
                if (poProcessResultMode == eProcessResultMode.Success)
                {
                    Message = R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "_ProcessComplete");
                    VisibleError = false;
                    ShowSuccessAction?.Invoke();
                }
                else if (poProcessResultMode == eProcessResultMode.Fail)
                {
                    Message = string.Format(R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "M007"), pcKeyGuid);
                    await ServiceGetError(pcKeyGuid);
                    VisibleError = true;
                }
            }
            catch (Exception ex)
            {
                loException.add(ex);
            }

            StateChangeAction?.Invoke();
            loException.ThrowExceptionIfErrors();
        }

        /// <summary>
        /// Called when batch process encounters an error
        /// NET4: ProcessError (lines 2032-2054)
        /// </summary>
        public Task ProcessError(string pcKeyGuid, R_APIException poException)
        {
            Message = string.Format(R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "M008"), pcKeyGuid);
            VisibleError = true;
            
            if (poException?.ErrorList != null)
            {
                ShowErrorAction?.Invoke(poException);
            }
            
            StateChangeAction?.Invoke();
            return Task.CompletedTask;
        }

        /// <summary>
        /// Called to report progress during batch processing
        /// </summary>
        public Task ReportProgress(int pnProgress, string pcStatus)
        {
            Percentage = pnProgress;
            Message = string.Format(R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "M009"), pnProgress, pcStatus);
            StateChangeAction?.Invoke();
            return Task.CompletedTask;
        }

        /// <summary>
        /// Get error details from batch process with multi-language support
        /// NET4: ProcessComplete - Fail case (lines 1996-2024)
        /// </summary>
        private async Task ServiceGetError(string pcKeyGuid)
        {
            R_APIException loException = new R_APIException();

            List<R_ErrorStatusReturn>? loResultData = null;
            R_GetErrorWithMultiLanguageParameter loParameterData;
            R_ProcessAndUploadClient loCls;

            try
            {
                // Add Parameter
                loParameterData = new R_GetErrorWithMultiLanguageParameter()
                {
                    COMPANY_ID = _companyId,
                    USER_ID = _userId,
                    KEY_GUID = pcKeyGuid,
                    RESOURCE_NAME = "FAT00100BackResources"
                };

                loCls = new R_ProcessAndUploadClient(pcModuleName: "FA",
                    plSendWithContext: true,
                    plSendWithToken: true,
                    pcHttpClientName: "R_DefaultServiceUrlFA");

                // Get error result
                loResultData = await loCls.R_GetStreamErrorProcess(loParameterData);

                // Handle unhandled errors (SeqNo < 0)
                if (loResultData != null && loResultData.Any(x => x.SeqNo < 0))
                {
                    var loUnhandleEx = loResultData.Where(x => x.SeqNo < 0).Select(x => new R_BlazorFrontEnd.Exceptions.R_Error(x.SeqNo.ToString(), x.ErrorMessage)).ToList();
                    var loEx = new R_Exception();
                    loUnhandleEx.ForEach(x => loEx.Add(x));

                    loException = R_FrontUtility.R_ConvertToAPIException(loEx);
                }
            }
            catch (Exception ex)
            {
                loException.add(ex);
            }

            loException.ThrowExceptionIfErrors();
        }
    }
}

