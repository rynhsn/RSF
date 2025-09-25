using PMT02100COMMON;
using PMT02100COMMON.DTOs.PMT02100;
using PMT02100MODEL.FrontDTOs.PMT02100;
using R_APICommonDTO;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using R_ProcessAndUploadFront;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMT02100MODEL.ViewModel
{
    public class PMT02101ViewModel : R_ViewModel<PMT02100HandoverDTO>, R_IProcessProgressStatus
    {
        public Action<R_APIException> ShowErrorAction { get; set; }
        public Action StateChangeAction { get; set; }
        public Action ShowSuccessAction { get; set; }
        public ObservableCollection<PMT02100HandoverDTO> loHandoverList = new ObservableCollection<PMT02100HandoverDTO>();
        public ScheduleProcessParameterDTO loScheduleParameter = null;
        public ScheduleProcessHeaderDTO loScheduleHeader = new ScheduleProcessHeaderDTO();

        public string SelectedCompanyId = "";
        public string SelectedUserId = "";

        public async Task ScheduleProcessAsync()
        {
            R_Exception loEx = new R_Exception();
            R_BatchParameter loBatchPar;
            R_ProcessAndUploadClient loCls;
            string lcGuid = "";
            List<PMT02100ScheduleDTO> Bigobject;
            List<R_KeyValue> loUserParam;

            try
            {
                loUserParam = new List<R_KeyValue>();

                loUserParam.Add(new R_KeyValue() { Key = ContextConstant.PMT02100_TAB_OPEN_SCHEDULE_PROCESS_PROPERTY_ID, Value = loScheduleParameter.CPROPERTY_ID });
                loUserParam.Add(new R_KeyValue() { Key = ContextConstant.PMT02100_TAB_OPEN_SCHEDULE_PROCESS_SCHEDULED_HO_DATE, Value = loScheduleHeader.DSCHEDULED_DATE.Value.ToString("yyyyMMdd") });
                loUserParam.Add(new R_KeyValue() { Key = ContextConstant.PMT02100_TAB_OPEN_SCHEDULE_PROCESS_SCHEDULED_HO_TIME, Value = loScheduleHeader.DSCHEDULED_DATE.Value.ToString("HH:mm") });

                //Instantiate ProcessClient
                loCls = new R_ProcessAndUploadClient(
                    pcModuleName: "PM",
                    plSendWithContext: true,
                    plSendWithToken: true,
                    pcHttpClientName: "R_DefaultServiceUrlPM",
                    poProcessProgressStatus: this);

                //Check Data
                if (loHandoverList.Count == 0)
                    return;

                Bigobject = loHandoverList.Select((x, i) => new PMT02100ScheduleDTO()
                {
                    NO = i + 1,
                    CCOMPANY_ID = SelectedCompanyId,
                    CDEPT_CODE = x.CDEPT_CODE,
                    CPROPERTY_ID = loScheduleParameter.CPROPERTY_ID,
                    CREF_NO = x.CREF_NO,
                    CTRANS_CODE = x.CTRANS_CODE
                }).ToList();

                //preapare Batch Parameter
                loBatchPar = new R_BatchParameter();

                loBatchPar.COMPANY_ID = SelectedCompanyId;
                loBatchPar.USER_ID = SelectedUserId;
                loBatchPar.ClassName = "PMT02100BACK.PMT02101Cls";
                loBatchPar.UserParameters = loUserParam;
                loBatchPar.BigObject = Bigobject;

                //PROCESS_TYPE = UPLOAD_PROCESS;
                lcGuid = await loCls.R_BatchProcess<List<PMT02100ScheduleDTO>>(loBatchPar, 4);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        #region Status
        public async Task ProcessComplete(string pcKeyGuid, eProcessResultMode poProcessResultMode)
        {
            R_APIException loException = new R_APIException();
            List<R_ErrorStatusReturn> loResult = null;

            try
            {
                if (poProcessResultMode == eProcessResultMode.Success)
                {
                    ShowSuccessAction();
                }

                if (poProcessResultMode == eProcessResultMode.Fail)
                {
                    loResult = await ServiceGetError(pcKeyGuid);
                }
            }
            catch (Exception ex)
            {
                loException.add(ex);
            }

            // Call Method Action StateHasChange
            StateChangeAction();

            loException.ThrowExceptionIfErrors();
        }

        public async Task ProcessError(string pcKeyGuid, R_APIException ex)
        {
            ShowErrorAction(ex);
            await Task.CompletedTask;
        }

        public async Task ReportProgress(int pnProgress, string pcStatus)
        {
            await Task.CompletedTask;
        }

        private async Task<List<R_ErrorStatusReturn>> ServiceGetError(string pcKeyGuid)
        {
            R_APIException loException = new R_APIException();

            List<R_ErrorStatusReturn> loResultData = null;
            R_GetErrorWithMultiLanguageParameter loParameterData;
            R_ProcessAndUploadClient loCls;

            try
            {
                // Add Parameter
                loParameterData = new R_GetErrorWithMultiLanguageParameter()
                {
                    COMPANY_ID = SelectedCompanyId,
                    USER_ID = SelectedUserId,
                    KEY_GUID = pcKeyGuid,
                    RESOURCE_NAME = "RSP_PM_HANDOVER_SCHEDULE_PROCESSResources"
                };

                loCls = new R_ProcessAndUploadClient(pcModuleName: "PM",
                    plSendWithContext: true,
                    plSendWithToken: true,
                    pcHttpClientName: "R_DefaultServiceUrlPM");

                // Get error result
                loResultData = await loCls.R_GetStreamErrorProcess(loParameterData);
                
                var loUnhandleEx = loResultData.Select(x => new R_BlazorFrontEnd.Exceptions.R_Error(x.SeqNo.ToString(), x.ErrorMessage)).ToList();
                var loEx = new R_Exception();
                loUnhandleEx.ForEach(x => loEx.Add(x));

                loException = R_FrontUtility.R_ConvertToAPIException(loEx);
            }
            catch (Exception ex)
            {
                loException.add(ex);
            }

            loException.ThrowExceptionIfErrors();
            return loResultData;
        }
        #endregion
    }
}
