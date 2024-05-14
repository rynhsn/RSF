using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PMT03500Common;
using PMT03500Common.DTOs;
using R_APICommonDTO;
using R_BlazorFrontEnd.Exceptions;
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
        
        public Action ShowSuccessAction { get; set; }
        public Action StateChangeAction { get; set; }
        public Action<R_Exception> DisplayErrorAction { get; set; }

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
                loBatchParUserParameters.Add(new R_KeyValue{ Key = PMT03500ContextConstant.CBUILDING_ID, Value = poParam.CBUILDING_ID});
                loBatchParUserParameters.Add(new R_KeyValue{ Key = PMT03500ContextConstant.CCHARGES_TYPE, Value = poParam.CCHARGES_TYPE});
                loBatchParUserParameters.Add(new R_KeyValue{ Key = PMT03500ContextConstant.CINVOICE_PRD, Value = poParam.CINV_PRD});

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

            loEx.ThrowExceptionIfErrors();
        }
        
        public async Task ProcessComplete(string pcKeyGuid, eProcessResultMode poProcessResultMode)
        {
            var loEx = new R_Exception();
            try
            {
                switch (poProcessResultMode)
                {
                    case eProcessResultMode.Success:
                        ShowSuccessAction();
                        break;
                    case eProcessResultMode.Fail:
                        break;
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
            var loException = new R_Exception();

            DisplayErrorAction.Invoke(loException);
            StateChangeAction();
            await Task.CompletedTask;
        }

        public async Task ReportProgress(int pnProgress, string pcStatus)
        {
            StateChangeAction();
            await Task.CompletedTask;
        }
    }
}