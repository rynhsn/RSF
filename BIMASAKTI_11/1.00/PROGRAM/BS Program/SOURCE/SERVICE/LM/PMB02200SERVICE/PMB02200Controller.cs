using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PMB02200BACK;
using PMB02200COMMON;
using PMB02200COMMON.DTO_s;
using R_BackEnd;
using R_Common;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace PMB02200SERVICE
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PMB02200Controller : ControllerBase, IPMB02200
    {
        private LoggerPMB02200 _logger;

        private readonly ActivitySource _activitySource;

        public PMB02200Controller(ILogger<PMB02200Controller> logger)
        {
            LoggerPMB02200.R_InitializeLogger(logger);
            _logger = LoggerPMB02200.R_GetInstanceLogger();
            _activitySource = PMB02200Activity.R_InitializeAndGetActivitySource(GetType().Name);

        }

        [HttpPost]
        public IAsyncEnumerable<UtilityChargesDTO> GetUtilityCharges()
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            R_Exception loException = new R_Exception();
            List<UtilityChargesDTO> loRtnTemp = null;
            PMB02200Cls loCls;
            try
            {
                loCls = new PMB02200Cls();
                ShowLogExecute();
                loRtnTemp = loCls.GetUpdatetUtilityChargesList(new UtilityChargesParamDTO()
                {
                    CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID,
                    CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(PMB02200ContextConstant.CPROPERTY_ID),
                    CDEPT_CODE = R_Utility.R_GetStreamingContext<string>(PMB02200ContextConstant.CDEPT_CODE),
                    CBUILDING_ID = R_Utility.R_GetStreamingContext<string>(PMB02200ContextConstant.CBUILDING_ID),
                    LALL_BUILDING = R_Utility.R_GetStreamingContext<bool>(PMB02200ContextConstant.LALL_BUILDING),
                    CTENANT_ID = R_Utility.R_GetStreamingContext<string>(PMB02200ContextConstant.CTENANT_ID),
                    CUTILITY_TYPE = R_Utility.R_GetStreamingContext<string>(PMB02200ContextConstant.CUTILITY_TYPE),
                    CLANG_ID=R_BackGlobalVar.CULTURE,
                    CUSER_ID = R_BackGlobalVar.USER_ID,
                });
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                ShowLogError(loException);
            }
            loException.ThrowExceptionIfErrors();
            ShowLogEnd();
            return StreamListHelper(loRtnTemp);
        }


        //stream list helper
        private async IAsyncEnumerable<T> StreamListHelper<T>(List<T> poList)
        {
            foreach (T loEntity in poList)
            {
                yield return loEntity;
            }
        }
        #region logger

        private void ShowLogStart([CallerMemberName] string pcMethodCallerName = "") => _logger.LogInfo($"Starting {pcMethodCallerName} in {GetType().Name}");

        private void ShowLogExecute([CallerMemberName] string pcMethodCallerName = "") => _logger.LogInfo($"Executing cls method in {GetType().Name}.{pcMethodCallerName}");

        private void ShowLogEnd([CallerMemberName] string pcMethodCallerName = "") => _logger.LogInfo($"End {pcMethodCallerName} in {GetType().Name}");

        private void ShowLogError(Exception exception, [CallerMemberName] string pcMethodCallerName = "") => _logger.LogError(exception);

        #endregion
    }
}
