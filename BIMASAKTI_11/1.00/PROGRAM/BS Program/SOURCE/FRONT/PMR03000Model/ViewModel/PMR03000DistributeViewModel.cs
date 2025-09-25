using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using R_APICommonDTO;
using R_BlazorFrontEnd.Exceptions;
using R_CommonFrontBackAPI;
using R_ProcessAndUploadFront;
using R_Error = R_APICommonDTO.R_Error;

namespace PMR03000Model.ViewModel
{
    public class PMR03000DistributeViewModel : R_IProcessProgressStatus
    {
        public string Message = "";
        public int Percentage = 0;

        public Action ShowSuccessAction { get; set; }
        public Action StateChangeAction { get; set; }
        public Func<Task> ActionDataSetExcel { get; set; }
        public Action<R_Exception> DisplayErrorAction { get; set; }

        public List<R_Error> ErrorList { get; set; } = new List<R_Error>();

        public async Task DistributeReport(PMR03000ReportParamDTO poReportParam)
        {
            var loEx = new R_Exception();
            R_BatchParameter loBatchPar;
            R_ProcessAndUploadClient loCls;
            List<R_KeyValue> loBatchParUserParameters;

            try
            {
                // set Param
                loBatchParUserParameters = new List<R_KeyValue>();

                //Instantiate ProcessClient
                loCls = new R_ProcessAndUploadClient(
                    pcModuleName: "PM",
                    plSendWithContext: true,
                    plSendWithToken: true,
                    pcHttpClientName: "R_DefaultServiceUrlPM",
                    poProcessProgressStatus: this);

                //preapare Batch Parameter
                loBatchPar = new R_BatchParameter();
                loBatchPar.COMPANY_ID = poReportParam.CCOMPANY_ID;
                loBatchPar.USER_ID = poReportParam.CUSER_ID;
                loBatchPar.UserParameters = loBatchParUserParameters;
                loBatchPar.ClassName = "PMR03000Back.PMR03000DistributeReportCls";
                loBatchPar.BigObject = poReportParam;

                await loCls.R_BatchProcess<PMR03000ReportParamDTO>(loBatchPar, 10);
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
                    Message = $"Process Complete and success with GUID {pcKeyGuid}";
                    // IsError = false;
                    ShowSuccessAction();
                }

                if (poProcessResultMode == eProcessResultMode.Fail)
                {
                    Message = $"Process Complete but fail with GUID {pcKeyGuid}";
                    // await ServiceGetError(pcKeyGuid);
                    // IsError = true;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            StateChangeAction();

            loEx.ThrowExceptionIfErrors();
            await Task.CompletedTask;
        }

        public async Task ProcessError(string pcKeyGuid, R_APIException ex)
        {
            //IF ERROR CONNECTION, PROGRAM WILL RUN THIS METHOD
            var loException = new R_Exception();

            Message = $"Process Error with GUID {pcKeyGuid}";
            ex.ErrorList.ForEach(x => loException.Add(x.ErrNo, x.ErrDescp));

            // DisplayErrorAction.Invoke(loException);
            DisplayErrorAction(loException);
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
    }
}