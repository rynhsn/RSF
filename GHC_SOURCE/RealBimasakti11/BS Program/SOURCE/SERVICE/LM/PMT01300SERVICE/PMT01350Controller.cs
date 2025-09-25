using PMT01300BACK;
using PMT01300COMMON;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System.Diagnostics;

namespace PMT01300SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class PMT01350Controller : ControllerBase, IPMT01350
    {
        private LoggerPMT01350 _Logger;
        private readonly ActivitySource _activitySource;
        public PMT01350Controller(ILogger<LoggerPMT01350> logger)
        {
            //Initial and Get Logger
            LoggerPMT01350.R_InitializeLogger(logger);
            _Logger = LoggerPMT01350.R_GetInstanceLogger();
            _activitySource = PMT01350ActivitySourceBase.R_InitializeAndGetActivitySource(nameof(PMT01350Controller));
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
        public IAsyncEnumerable<PMT01350DTO> GetAgreementInvPlanListStream()
        {
            using Activity activity = _activitySource.StartActivity("GetAgreementInvPlanListStream");
            var loEx = new R_Exception();
            IAsyncEnumerable<PMT01350DTO> loRtn = null;
            _Logger.LogInfo("Start GetAgreementInvPlanListStream");

            try
            {
                _Logger.LogInfo("Set Param GetAgreementInvPlanListStream");
                PMT01350DTO loEntity = new PMT01350DTO();
                loEntity.CREC_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CREC_ID);
                loEntity.CCHARGES_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CCHARGES_ID);
                loEntity.CCHARGE_MODE = R_Utility.R_GetStreamingContext<string>(ContextConstant.CCHARGE_MODE);
                loEntity.CBUILDING_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CBUILDING_ID);
                loEntity.CFLOOR_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CFLOOR_ID);
                loEntity.CUNIT_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CUNIT_ID);

                _Logger.LogInfo("Call Back Method GetAllAgreemenetInvPlan");
                var loCls = new PMT01350Cls();
                var loTempRtn = loCls.GetAllAgreemenetInvPlan(loEntity);

                _Logger.LogInfo("Call Stream Method Data GetAgreementInvPlanListStream");
                loRtn = GetStreamData<PMT01350DTO>(loTempRtn);
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
        public IAsyncEnumerable<PMT01351DTO> GetLOIInvPlanChargeStream()
        {
            using Activity activity = _activitySource.StartActivity("GetLOIInvPlanChargeStream");
            var loEx = new R_Exception();
            IAsyncEnumerable<PMT01351DTO> loRtn = null;
            _Logger.LogInfo("Start GetLOIInvPlanChargeStream");

            try
            {
                _Logger.LogInfo("Set Param GetLOIInvPlanChargeStream");
                PMT01351DTO loEntity = new PMT01351DTO();
                loEntity.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CPROPERTY_ID);
                loEntity.CDEPT_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstant.CDEPT_CODE);
                loEntity.CREF_NO = R_Utility.R_GetStreamingContext<string>(ContextConstant.CREF_NO);
                loEntity.CUNIT_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CUNIT_ID);
                loEntity.CFLOOR_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CFLOOR_ID);
                loEntity.CBUILDING_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CBUILDING_ID);
                loEntity.CCHARGE_MODE = R_Utility.R_GetStreamingContext<string>(ContextConstant.CCHARGE_MODE);

                _Logger.LogInfo("Call Back Method GetAllLOICharges");
                var loCls = new PMT01350Cls();
                var loTempRtn = loCls.GetAllLOIInvPlanCharges(loEntity);

                _Logger.LogInfo("Call Stream Method Data GetLOIInvPlanChargeStream");
                loRtn = GetStreamData<PMT01351DTO>(loTempRtn);
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