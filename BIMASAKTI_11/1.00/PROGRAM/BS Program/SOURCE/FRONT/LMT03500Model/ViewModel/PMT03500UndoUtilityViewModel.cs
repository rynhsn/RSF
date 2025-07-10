using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PMT03500Common;
using PMT03500Common.DTOs;
using R_APICommonDTO;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using R_ProcessAndUploadFront;

namespace PMT03500Model.ViewModel
{
    public class PMT03500UndoUtilityViewModel : R_IProcessProgressStatus
    {
        List<PMT03500UndoUtilityDTO> DataList = new List<PMT03500UndoUtilityDTO>();
        PMT03500UndoParam Parameter = new PMT03500UndoParam();
        public string CompanyId { get; set; }
        public string UserId { get; set; }

        public string Message = "";
        public int Percentage = 0;

        public Action ShowSuccessAction { get; set; }
        public Action StateChangeAction { get; set; }
        public Action<R_APIException> DisplayErrorAction { get; set; }

        public List<R_BlazorFrontEnd.Exceptions.R_Error> ErrorList { get; set; } =
            new List<R_BlazorFrontEnd.Exceptions.R_Error>();

        public void Init(object poParam)
        {
            Parameter = (PMT03500UndoParam)poParam;
        }

        public async Task SaveBulk(PMT03500UndoParam poParam, List<PMT03500UtilityUsageDTO> poDataList)
        {
            var loEx = new R_Exception();
            R_BatchParameter loBatchPar;
            R_ProcessAndUploadClient loCls;
            List<PMT03500UtilityUsageDTO> ListFromExcel;
            List<R_KeyValue> loBatchParUserParameters;

            try
            {
                // set Param
                loBatchParUserParameters = new List<R_KeyValue>();
                loBatchParUserParameters.Add(new R_KeyValue
                    { Key = PMT03500ContextConstant.CPROPERTY_ID, Value = poParam.CPROPERTY_ID });
                loBatchParUserParameters.Add(new R_KeyValue
                    { Key = PMT03500ContextConstant.CBUILDING_ID, Value = poParam.CBUILDING_ID });
                loBatchParUserParameters.Add(new R_KeyValue
                    { Key = PMT03500ContextConstant.CCHARGES_TYPE, Value = poParam.CCHARGES_TYPE });
                loBatchParUserParameters.Add(new R_KeyValue
                    { Key = PMT03500ContextConstant.CINVOICE_PRD, Value = poParam.CINV_PRD });

                //Instantiate ProcessClient
                loCls = new R_ProcessAndUploadClient(
                    pcModuleName: "PM",
                    plSendWithContext: true,
                    plSendWithToken: true,
                    pcHttpClientName: "R_DefaultServiceUrlPM",
                    poProcessProgressStatus: this);

                //Set Data
                if (poDataList.Count == 0)
                {
                    loEx.Add("","Please select at lease 1 data!");
                    goto EndBlock;
                }

                foreach (var item in poDataList)
                {
                    item.NO = poDataList.IndexOf(item) + 1;
                }
                // ListFromExcel = poDataList.ToList();

                //preapare Batch Parameter
                loBatchPar = new R_BatchParameter();
                loBatchPar.COMPANY_ID = poParam.CCOMPANY_ID;
                loBatchPar.USER_ID = poParam.CUSER_ID;
                loBatchPar.UserParameters = loBatchParUserParameters;
                loBatchPar.ClassName = "PMT03500Back.PMT03500UndoUtilityCls";
                loBatchPar.BigObject = poDataList;

                await loCls.R_BatchProcess<List<PMT03500UtilityUsageDTO>>(loBatchPar, 4);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            EndBlock:
            loEx.ThrowExceptionIfErrors();
        }

        public async Task ProcessComplete(string pcKeyGuid, eProcessResultMode poProcessResultMode)
        {
            var loEx = new R_Exception();
            try
            {
                //switch (poProcessResultMode)
                //{
                //    case eProcessResultMode.Success:
                //        Message = $"Process Complete and success with GUID {pcKeyGuid}";
                //        ShowSuccessAction();
                //        break;
                //    case eProcessResultMode.Fail:
                //        await ServiceGetError(pcKeyGuid);
                //        Message = $"Process Complete but fail with GUID {pcKeyGuid}";
                //        break;
                //}


                if (poProcessResultMode == eProcessResultMode.Success)
                {
                    Message = $"Process Complete and success with GUID {pcKeyGuid}";
                    //IsError = false;
                    ShowSuccessAction();
                }

                if (poProcessResultMode == eProcessResultMode.Fail)
                {
                    Message = $"Process Complete but fail with GUID {pcKeyGuid}";
                    await ServiceGetError(pcKeyGuid);
                    //IsError = true;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            StateChangeAction();

            loEx.ThrowExceptionIfErrors();
            //await Task.CompletedTask;
        }

        public async Task ProcessError(string pcKeyGuid, R_APIException ex)
        {
            // var loException = new R_Exception();

            // DisplayErrorAction.Invoke(loException);
            // ex.ErrorList.ForEach(l =>
            // {
            //     loException.Add(l.ErrNo, l.ErrDescp);
            // });

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
                loParameterData.COMPANY_ID = CompanyId;
                loParameterData.USER_ID = UserId;
                loParameterData.KEY_GUID = pcKeyGuid;
                loParameterData.RESOURCE_NAME = "RSP_PM_UNDO_UTILITY_USAGEResources";

                loCls = new R_ProcessAndUploadClient(
                    pcModuleName: "PM",
                    plSendWithContext: true,
                    plSendWithToken: true,
                    pcHttpClientName: "R_DefaultServiceUrlPM");

                // Get error result
                loResultData = await loCls.R_GetStreamErrorProcess(loParameterData);

                // check error if unhandle
                //if (loResultData.Any())
                //{
                    var loUnhandledEx = loResultData.Select(x => new R_BlazorFrontEnd.Exceptions.R_Error(x.SeqNo.ToString(), x.ErrorMessage)).ToList();
                    ErrorList = new List<R_BlazorFrontEnd.Exceptions.R_Error>(loUnhandledEx);

                    var loEx = new R_Exception();
                    loUnhandledEx.ForEach(x => loEx.Add(x));
                    
                    loException = R_FrontUtility.R_ConvertToAPIException(loEx);

                //}
            }
            catch (Exception ex)
            {
                loException.add(ex);
            }

            loException.ThrowExceptionIfErrors();
        }
    }
}