using PMT00500BACK;
using PMT00500COMMON;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System.Diagnostics;

namespace PMT00500SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class PMT00550Controller : ControllerBase, IPMT00550
    {
        private LoggerPMT00550 _Logger;
        private readonly ActivitySource _activitySource;
        public PMT00550Controller(ILogger<LoggerPMT00550> logger)
        {
            //Initial and Get Logger
            LoggerPMT00550.R_InitializeLogger(logger);
            _Logger = LoggerPMT00550.R_GetInstanceLogger();
            _activitySource = PMT00550ActivitySourceBase.R_InitializeAndGetActivitySource(nameof(PMT00550Controller));
        }

        #region Stream Data
        private async IAsyncEnumerable<T> GetStreamData<T>(List<T> poParameter)
        {
            foreach (var item in poParameter)
            {
                yield return item;
            }
        }
        #endregion


        [HttpPost]
        public IAsyncEnumerable<PMT00550DTO> GetAgreementInvPlanListStream()
        {
            using Activity activity = _activitySource.StartActivity("GetAgreementInvPlanListStream");
            var loEx = new R_Exception();
            IAsyncEnumerable<PMT00550DTO> loRtn = null;
            _Logger.LogInfo("Start GetAgreementInvPlanListStream");

            try
            {
                _Logger.LogInfo("Set Param GetAgreementInvPlanListStream");
                PMT00550DTO loEntity = new PMT00550DTO();
                loEntity.CREC_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CREC_ID);
                loEntity.CCHARGES_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CCHARGES_ID);
                loEntity.CCHARGE_MODE = R_Utility.R_GetStreamingContext<string>(ContextConstant.CCHARGE_MODE);
                loEntity.CBUILDING_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CBUILDING_ID);
                loEntity.CFLOOR_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CFLOOR_ID);
                loEntity.CUNIT_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CUNIT_ID);

                _Logger.LogInfo("Call Back Method GetAllAgreemenetInvPlan");
                var loCls = new PMT00550Cls();
                var loTempRtn = loCls.GetAllAgreemenetInvPlan(loEntity);

                _Logger.LogInfo("Call Stream Method Data GetAgreementInvPlanListStream");
                loRtn = GetStreamData<PMT00550DTO>(loTempRtn);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetAgreementInvPlanListStream");

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<PMT00551DTO> GetLOIInvPlanChargeStream()
        {
            using Activity activity = _activitySource.StartActivity("GetLOIInvPlanChargeStream");
            var loEx = new R_Exception();
            IAsyncEnumerable<PMT00551DTO> loRtn = null;
            _Logger.LogInfo("Start GetLOIInvPlanChargeStream");

            try
            {
                _Logger.LogInfo("Set Param GetLOIInvPlanChargeStream");
                PMT00551DTO loEntity = new PMT00551DTO();
                loEntity.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CPROPERTY_ID);
                loEntity.CDEPT_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstant.CDEPT_CODE);
                loEntity.CREF_NO = R_Utility.R_GetStreamingContext<string>(ContextConstant.CREF_NO);
                loEntity.CUNIT_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CUNIT_ID);
                loEntity.CFLOOR_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CFLOOR_ID);
                loEntity.CBUILDING_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CBUILDING_ID);
                loEntity.CCHARGE_MODE = R_Utility.R_GetStreamingContext<string>(ContextConstant.CCHARGE_MODE);

                _Logger.LogInfo("Call Back Method GetAllLOICharges");
                var loCls = new PMT00550Cls();
                var loTempRtn = loCls.GetAllLOIInvPlanCharges(loEntity);

                _Logger.LogInfo("Call Stream Method Data GetLOIInvPlanChargeStream");
                loRtn = GetStreamData<PMT00551DTO>(loTempRtn);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetLOIInvPlanChargeStream");

            return loRtn;
        }
    }
}