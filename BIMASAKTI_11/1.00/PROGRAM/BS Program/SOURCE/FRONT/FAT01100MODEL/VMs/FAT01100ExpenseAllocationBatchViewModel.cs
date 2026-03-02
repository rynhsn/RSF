using FAT01100Common.DTOs;
using FAT01100FrontResources;
using R_APICommonDTO;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using R_ProcessAndUploadFront;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAT01100Model.VMs
{
    public class FAT01100ExpenseAllocationBatchViewModel : R_IProcessProgressStatus
    {
        public Action<R_APIException>? ShowErrorAction { get; set; }
        public Action? StateChangeAction { get; set; }
        public Action? ShowSuccessAction { get; set; }
        public string Message { get; set; } = string.Empty;
        public int Percentage { get; set; }
        public bool VisibleError { get; set; }
        private string _companyId = string.Empty;
        private string _userId = string.Empty;

        public async Task R_SaveBatchAsync(FAT01100ExpenseAllocationR_SaveBatchParameterDTO poParameter, string pcLangId)
        {
            R_Exception loEx = new R_Exception();
            R_BatchParameter loBatchPar;
            R_ProcessAndUploadClient loCls;
            string lcGuid = "";
            List<FAT01100ExpenseAllocationBatchListDisplayDTO> loBigObject;
            var loUserParam = new List<R_KeyValue>();
            List<R_KeyValue> loBatchParUserParameters;

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

                loBatchParUserParameters = new List<R_KeyValue>();
                loBatchParUserParameters.Add(new R_KeyValue
                { Key = FAT01100BatchContextConstant.CPROPERTY_ID, Value = poParameter.UserParameters.CPROPERTY_ID });
                loBatchParUserParameters.Add(new R_KeyValue
                { Key = FAT01100BatchContextConstant.CPARENT_ID, Value = poParameter.UserParameters.CPARENT_ID });
                loBatchParUserParameters.Add(new R_KeyValue
                { Key = FAT01100BatchContextConstant.CDEPT_CODE, Value = poParameter.UserParameters.CDEPT_CODE });
                loBatchParUserParameters.Add(new R_KeyValue
                { Key = FAT01100BatchContextConstant.CTRANSACTION_CODE, Value = poParameter.UserParameters.CTRANSACTION_CODE });
                loBatchParUserParameters.Add(new R_KeyValue
                { Key = FAT01100BatchContextConstant.CREF_NO, Value = poParameter.UserParameters.CREF_NO });
                loBatchParUserParameters.Add(new R_KeyValue
                { Key = FAT01100BatchContextConstant.CASSET_CODE, Value = poParameter.UserParameters.CASSET_CODE });
                loBatchParUserParameters.Add(new R_KeyValue
                { Key = FAT01100BatchContextConstant.CTRANS_SEQ_NO, Value = poParameter.UserParameters.CTRANS_SEQ_NO });



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
                loBatchPar.ClassName = "FAT01100Back.FAT01100ExpenseAllocationBatchCls";
                loBatchPar.UserParameters = loBatchParUserParameters;
                loBatchPar.BigObject = loBigObject;

                lcGuid = await loCls.R_BatchProcess<List<FAT01100ExpenseAllocationBatchListDisplayDTO>>(loBatchPar, loBigObject.Count > 0 ? loBigObject.Count : 1);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }


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
                    RESOURCE_NAME = "FAT01100BackResources"
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
