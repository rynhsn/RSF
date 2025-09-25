using System;
using System.Threading.Tasks;
using R_ProcessAndUploadFront;
using R_CommonFrontBackAPI;
using R_APICommonDTO;
using R_BlazorFrontEnd.Exceptions;
using System.Collections.Generic;
using System.ComponentModel.Design;
using PMB01600Common.Batch;
using PMB01600Common;
using System.Linq;
using System.Data;
using R_BlazorFrontEnd.Helpers;

namespace PMB01600Model.ViewModel
{
    public class PMB01600BatchViewModel : R_IProcessProgressStatus
    {

        public PMB01600BatchParameter BatchParam = new PMB01600BatchParameter();

        public string Message = "";
        public int Percentage = 0;
        public bool IsError = false;

        public Action ShowSuccessAction { get; set; }
        public Action StateChangeAction { get; set; }
        public Action<R_APIException> DisplayErrorAction { get; set; }

        public List<R_BlazorFrontEnd.Exceptions.R_Error> ErrorList { get; set; } =
            new List<R_BlazorFrontEnd.Exceptions.R_Error>();

        public async Task SaveBulkFile(PMB01600BatchParameter poBatchParam,
            List<PMB01600ForBatchDTO> poDataList)
        {
            var loEx = new R_Exception();
            R_BatchParameter loBatchPar;
            R_ProcessAndUploadClient loCls;
            List<PMB01600ForBatchDTO> loDataList;
            List<R_KeyValue> loBatchParUserParameters;

            try
            {
                // set Param
                loBatchParUserParameters = new List<R_KeyValue>();
                loBatchParUserParameters.Add(new R_KeyValue
                { Key = PMB01600ContextConstantHeader.CPROPERTY_ID, Value = poBatchParam.CPROPERTY_ID });
                loBatchParUserParameters.Add(new R_KeyValue
                { Key = PMB01600ContextConstantHeader.CREF_PRD, Value = poBatchParam.CREF_PRD });

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

                poDataList = poDataList
                    .Select((x, index) =>
                    {
                        x.INO = index + 1; // mulai dari 1
                        return x;
                    })
                    .ToList();


                //preapare Batch Parameter
                loBatchPar = new R_BatchParameter();
                loBatchPar.COMPANY_ID = poBatchParam.CCOMPANY_ID;
                loBatchPar.USER_ID = poBatchParam.CUSER_ID;
                loBatchPar.UserParameters = loBatchParUserParameters;
                loBatchPar.ClassName = "PMB01600Back.PMB01600BatchCls";
                loBatchPar.BigObject = poDataList;

                await loCls.R_BatchProcess<List<PMB01600ForBatchDTO>>(loBatchPar, 4);
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
            Message = $"Process Error with GUID {pcKeyGuid}";

            //DisplayErrorAction(ex);

            DisplayErrorAction.Invoke(ex);
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

        private async Task ServiceGetError(string pcKeyGuid)
        {
            var loException = new R_APIException();

            List<R_ErrorStatusReturn> loResultData;
            R_GetErrorWithMultiLanguageParameter loParameterData;
            R_ProcessAndUploadClient loCls;
            try
            {
                // Add Parameter
                loParameterData = new R_GetErrorWithMultiLanguageParameter();
                loParameterData.COMPANY_ID = BatchParam.CCOMPANY_ID;
                loParameterData.USER_ID = BatchParam.CUSER_ID;
                loParameterData.KEY_GUID = pcKeyGuid;
                loParameterData.RESOURCE_NAME = "RSP_PM_UNDO_BILL_STMT_PROCESSResources";

                loCls = new R_ProcessAndUploadClient(
                    pcModuleName: "PM",
                    plSendWithContext: true,
                    plSendWithToken: true,
                    pcHttpClientName: "R_DefaultServiceUrlPM");

                // Get error result
                loResultData = await loCls.R_GetStreamErrorProcess(loParameterData);

                // check error if unhandle
                if (loResultData.Any())
                {
                    //var loUnhandledEx = loResultData.Where(y => y.SeqNo <= 0).Select(x => new R_BlazorFrontEnd.Exceptions.R_Error(x.SeqNo.ToString(), x.ErrorMessage)).ToList();
                    //loUnhandledEx.ForEach(x => loException.Add(x));

                    var loUnhandledEx = loResultData.Select(x => new R_BlazorFrontEnd.Exceptions.R_Error(x.SeqNo.ToString(), x.ErrorMessage)).ToList();
                    ErrorList = new List<R_BlazorFrontEnd.Exceptions.R_Error>(loUnhandledEx);

                    var loEx = new R_Exception();
                    loUnhandledEx.ForEach(x => loEx.Add(x));

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
