using PMT02100COMMON.DTOs.PMT02100;
using PMT02100COMMON.DTOs.PMT02110;
using PMT02100MODEL.FrontDTOs.PMT02100;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using R_CommonFrontBackAPI;
using R_ProcessAndUploadFront;
using System.ComponentModel.Design;
using PMT02100REPORTCOMMON.DTOs.PMT02100PDF;
using R_APICommonDTO;

namespace PMT02100MODEL.ViewModel
{
    public class PMT02111ViewModel : R_ViewModel<PMT02100HandoverBuildingDTO>, R_IProcessProgressStatus
    {
        private PMT02110Model loModel = new PMT02110Model();

        public ScheduleProcessHeaderDTO loScheduleHeader = new ScheduleProcessHeaderDTO();

        public PMT02100HandoverDTO loSelectedHandover = new PMT02100HandoverDTO();

        public string lcCompanyId = "";
        public string lcUserId = "";
        public string Message = "";
        public bool _isSuccess;
        public Action? StateChangeAction { get; set; }
        public Action<R_Exception>? DisplayErrorAction { get; set; }
        public Action? ShowSuccessAction { get; set; }

        //public async Task ConfirmScheduleProcessAsync()
        //{
        //    R_Exception loEx = new R_Exception();
        //    try
        //    {
        //        //loScheduleHeader.CSCHEDULED_TIME_HOURS = loScheduleHeader.ISCHEDULED_TIME_HOURS.ToString("D2");
        //        //loScheduleHeader.CSCHEDULED_TIME_MINUTES = loScheduleHeader.ISCHEDULED_TIME_MINUTES.ToString("D2");
        //        await loModel.ConfirmScheduleProcessAsync(new PMT02110ConfirmParameterDTO()
        //        {
        //            CDEPT_CODE = loSelectedHandover.CDEPT_CODE,
        //            CPROPERTY_ID = loSelectedHandover.CPROPERTY_ID,
        //            CREF_NO = loSelectedHandover.CREF_NO,
        //            CTENANT_EMAIL = loSelectedHandover.CTENANT_EMAIL,
        //            CTRANS_CODE = loSelectedHandover.CTRANS_CODE,
        //            CSCHEDULED_HO_DATE = loScheduleHeader.DSCHEDULED_DATE.Value.ToString("yyyyMMdd"),
        //            CSCHEDULED_HO_TIME = loScheduleHeader.DSCHEDULED_DATE.Value.ToString("HH:mm") //loScheduleHeader.CSCHEDULED_TIME_HOURS + ":" + loScheduleHeader.CSCHEDULED_TIME_MINUTES
        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        loEx.Add(ex);
        //    }

        //    loEx.ThrowExceptionIfErrors();
        //}

        public async Task ConfirmScheduleProcessAsync()
        {
            var loEx = new R_Exception();
            try
            {
                var loUserParameters = new List<R_KeyValue>();

                // set Param
                loUserParameters = new List<R_KeyValue>();
                /*
                {
                    new R_KeyValue()
                    {
                        Key = PMR01700ContextDistributeReportDTO.CPROPERTY_ID,
                        Value = poParam.ParameterSP.CPROPERTY_ID!
                    },
                    new R_KeyValue()
                    {
                        Key = PMR01700ContextDistributeReportDTO.CDEPT_CODE,
                        Value = poParam.ParameterSP.CDEPT_CODE!
                    }
                };
                */
                //Instantiate ProcessClient
                R_ProcessAndUploadClient loCls = new R_ProcessAndUploadClient(
                    pcModuleName: "PM",
                    plSendWithContext: true,
                    plSendWithToken: true,
                    pcHttpClientName: "R_DefaultServiceUrlPM",
                    poProcessProgressStatus: this);

                //prepare Batch Parameter
                R_BatchParameter loUploadPar = new R_BatchParameter();
                loUploadPar.COMPANY_ID = lcCompanyId;
                loUploadPar.USER_ID = lcUserId;
                loUploadPar.UserParameters = loUserParameters;
                loUploadPar.ClassName = "PMT02100BACK.PMT02110InvitationEmailCls";
                loUploadPar.BigObject = new PMT02100InvitationParameterDTO()
                {
                    CCOMPANY_ID = lcCompanyId,
                    CUSER_ID = lcUserId,
                    CDEPT_CODE = loSelectedHandover.CDEPT_CODE,
                    CPROPERTY_ID = loSelectedHandover.CPROPERTY_ID,
                    CREF_NO = loSelectedHandover.CREF_NO,
                    CTENANT_EMAIL = loSelectedHandover.CTENANT_EMAIL,
                    CTRANS_CODE = loSelectedHandover.CTRANS_CODE,
                    LCONFIRM_SCHEDULE = true,
                    CSCHEDULED_HO_DATE = loScheduleHeader.DSCHEDULED_DATE.Value.ToString("yyyyMMdd"),
                    CSCHEDULED_HO_TIME = loScheduleHeader.DSCHEDULED_DATE.Value.ToString("HH:mm") //loScheduleHeader.CSCHEDULED_TIME_HOURS + ":" + loScheduleHeader.CSCHEDULED_TIME_MINUTES
                };
                await loCls.R_BatchProcess<PMT02100InvitationParameterDTO>(loUploadPar, 10);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task<PMT02110TenantDTO> GetTenantDetailAsync()
        {
            R_Exception loEx = new R_Exception();
            PMT02110TenantResultDTO loResult = null;
            try
            {
                loResult = await loModel.GetTenantDetailAsync(new PMT02110TenantParameterDTO()
                {
                    CPROPERTY_ID = loSelectedHandover.CPROPERTY_ID,
                    CTENANT_ID = loSelectedHandover.CTENANT_ID
                });
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loResult.Data;
        }


        #region ProgressBar
        public async Task ProcessComplete(string pcKeyGuid, eProcessResultMode poProcessResultMode)
        {
            if (poProcessResultMode == eProcessResultMode.Success)
            {
                Message = string.Format("Finish Process Cancel Receipt!");
                ShowSuccessAction!();
                _isSuccess = true;
            }
            if (poProcessResultMode == eProcessResultMode.Fail)
            {
                _isSuccess = false;

                Message = "Process Completed With Fail";
                try
                {
                    await ServiceGetError(pcKeyGuid);
                }
                catch (R_Exception ex)
                {
                    DisplayErrorAction?.Invoke(ex);
                }
            }

            StateChangeAction!();
            await Task.CompletedTask;
        }

        public async Task ProcessError(string pcKeyGuid, R_APIException ex)
        {
            R_Exception loException = new R_Exception();
            var lcMessage = string.Format("Process Error with GUID {0}", pcKeyGuid);
            Message = string.Format("Process Error with ");

            ex.ErrorList.ForEach(x => loException.Add(x.ErrNo, x.ErrDescp));

            DisplayErrorAction!.Invoke(loException);
            StateChangeAction!();
            await Task.CompletedTask;
        }

        public async Task ReportProgress(int pnProgress, string pcStatus)
        {
            //if (TypeProcessReport == "DISTRIBUTE")
            //{
            //    ProgressBarPercentage = pnProgress;
            //    Message = string.Format("Process Progress {0} with status {1}", pnProgress, pcStatus);
            //}
            StateChangeAction!();
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
                loParameterData = new R_GetErrorWithMultiLanguageParameter()
                {
                    COMPANY_ID = lcCompanyId,
                    USER_ID = lcUserId,
                    KEY_GUID = pcKeyGuid,
                    RESOURCE_NAME = "RSP_PM_HANDOVER_CONFIRM_SCHEDULE"//INI NANTI DIGANTI
                };
                loCls = new R_ProcessAndUploadClient(
                   pcModuleName: "PM",
                   plSendWithContext: true,
                   plSendWithToken: true,
                   pcHttpClientName: "R_DefaultServiceUrlPM",
                   poProcessProgressStatus: this);
                loResultData = await loCls.R_GetStreamErrorProcess(loParameterData);
                loResultData.ForEach(x => loException.Add(x.SeqNo.ToString(), x.ErrorMessage));
                goto EndBlock;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
        EndBlock:
            loException.ThrowExceptionIfErrors();

        }
        #endregion
    }
}
