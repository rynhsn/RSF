using PMM01000COMMON;
using R_APICommonDTO;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using R_ProcessAndUploadFront;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace PMM01000MODEL
{
    public class PMM01050SaveBatchViewModel : R_IProcessProgressStatus
    {
        private string Message;
        private int Percentage;
        public List<R_BlazorFrontEnd.Exceptions.R_Error> ErrorList { get; set; } = new List<R_BlazorFrontEnd.Exceptions.R_Error>();
        public async Task SaveDeleteRateOT(PMM01050DTO poNewEntity, eCRUDMode peCRUDMode, string pcCompanyId, string pcUserId)
        {
            var loEx = new R_Exception();
            R_BatchParameter loUploadPar;
            R_ProcessAndUploadClient loCls;
            List<R_KeyValue> loUserParameneters;
            R_IProcessProgressStatus loProgressStatus;

            try
            {
                //Set Param
                CompanyID = pcCompanyId;
                UserId = pcUserId;
                if (peCRUDMode == eCRUDMode.AddMode)
                {
                    poNewEntity.CACTION = "ADD";
                }
                else if (peCRUDMode == eCRUDMode.EditMode)
                {
                    poNewEntity.CACTION = "EDIT";
                }
                else if (peCRUDMode == eCRUDMode.DeleteMode)
                {
                    poNewEntity.CACTION = "DELETE";
                }

                //preapare Batch Parameter
                loUploadPar = new R_BatchParameter();
                loUploadPar.COMPANY_ID = pcCompanyId;
                loUploadPar.USER_ID = pcUserId;
                loUploadPar.UserParameters = new List<R_KeyValue>();
                loUploadPar.ClassName = "PMM01000BACK.PMM01050Cls";
                loUploadPar.BigObject = poNewEntity;

                //Instantiate ProcessClient
                loCls = new R_ProcessAndUploadClient(
                    pcModuleName: "PM",
                    plSendWithContext: true,
                    plSendWithToken: true,
                    pcHttpClientName: "R_DefaultServiceUrlPM",
                    poProcessProgressStatus: this);

                var loKeyGuid = await loCls.R_BatchProcess<PMM01050DTO>(loUploadPar, 5);
            } catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        #region Status
        // Func Proses is Success
        public Func<Task> ActionIsCompleteSuccess { get; set; }

        // Action StateHasChanged
        public Action StateChangeAction { get; set; }

        // Action Get Error Unhandle
        public Action<R_APIException> ShowErrorAction { get; set; }
        private string CompanyID { get; set; }
        private string UserId { get; set; }

        public async Task ProcessComplete(string pcKeyGuid, eProcessResultMode poProcessResultMode)
        {
            var loEx = new R_Exception();

            try
            {
                if (poProcessResultMode == eProcessResultMode.Success)
                {
                    await ActionIsCompleteSuccess();
                }

                if (poProcessResultMode == eProcessResultMode.Fail)
                {
                    await ServiceGetError(pcKeyGuid);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            // Call Method Action StateHasChange
            StateChangeAction();

            loEx.ThrowExceptionIfErrors();
        }

        public async Task ProcessError(string pcKeyGuid, R_APIException ex)
        {
            // Call Method Action Error Unhandle
            ShowErrorAction(ex);

            // Call Method Action StateHasChange
            StateChangeAction();

            await Task.CompletedTask;
        }

        public async Task ReportProgress(int pnProgress, string pcStatus)
        {
            StateChangeAction();

            await Task.CompletedTask;
        }

        private async Task ServiceGetError(string pcKeyGuid)
        {
            R_Exception loException = new R_Exception();

            List<R_ErrorStatusReturn> loResultData;
            R_GetErrorWithMultiLanguageParameter loParameterData;
            R_ProcessAndUploadClient loCls;

            try
            {
                // Add Parameter
                loParameterData = new R_GetErrorWithMultiLanguageParameter()
                {
                    COMPANY_ID = CompanyID,
                    USER_ID = UserId,
                    KEY_GUID = pcKeyGuid,
                    RESOURCE_NAME = "RSP_PM_MAINTAIN_RATE_OTResources"
                };

                loCls = new R_ProcessAndUploadClient(pcModuleName: "PM",
                    plSendWithContext: true,
                    plSendWithToken: true,
                    pcHttpClientName: "R_DefaultServiceUrlPM");

                // Get error result
                loResultData = await loCls.R_GetStreamErrorProcess(loParameterData);

                // check error if unhandle
                if (loResultData.Count > 0)
                {
                    var loUnhandleEx = loResultData.Select(x => new R_BlazorFrontEnd.Exceptions.R_Error(x.SeqNo.ToString(), x.ErrorMessage)).ToList();
                    ErrorList = new List<R_BlazorFrontEnd.Exceptions.R_Error>(loUnhandleEx);
                    loUnhandleEx.ForEach(x => loException.Add(x));
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
