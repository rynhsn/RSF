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

namespace PMB02200SERVICE
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PMB02200HelperController : ControllerBase, IPMB02200General
    {
        private LoggerPMB02200 _logger;

        private readonly ActivitySource _activitySource;

        public PMB02200HelperController(ILogger<PMB02200HelperController> logger)
        {
            LoggerPMB02200.R_InitializeLogger(logger);
            _logger = LoggerPMB02200.R_GetInstanceLogger();
            _activitySource = PMB02200Activity.R_InitializeAndGetActivitySource(GetType().Name);

        }

        //stream list helper
        private async IAsyncEnumerable<T> StreamListHelper<T>(List<T> poList)
        {
            foreach (T loEntity in poList)
            {
                yield return loEntity;
            }
        }

        [HttpPost]
        public GeneralAPIResultBaseDTO<PMSystemParamDTO> GetPMSysParam()
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            GeneralAPIResultBaseDTO<PMSystemParamDTO> loRtn = null;
            ShowLogStart();
            var loEx = new R_Exception();
            try
            {
                var loCls = new PMB02200HelperCls();

                ShowLogExecute();
                loRtn.Data = loCls.GetPMSysParam(new GeneralParamDTO()
                {
                    CCOMPANY_ID=R_BackGlobalVar.COMPANY_ID,
                    CLANGUAGE_ID=R_BackGlobalVar.CULTURE
                });
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                ShowLogError(loEx);

            }

            loEx.ThrowExceptionIfErrors();
            ShowLogEnd();

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<PropertyDTO> GetPropertyList()
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            R_Exception loException = new R_Exception();
            List<PropertyDTO> loRtnTemp = null;
            PMB02200HelperCls loCls;
            try
            {
                loCls = new PMB02200HelperCls();
                ShowLogExecute();
                loRtnTemp = loCls.GetPropertyList(new GeneralParamDTO()
                {
                    CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID,
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

        [HttpPost]
        public IAsyncEnumerable<GeneralTypeDTO> GetUtilityTypeList()
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            R_Exception loException = new R_Exception();
            List<GeneralTypeDTO> loRtnTemp = null;
            PMB02200HelperCls loCls;
            try
            {
                loCls = new PMB02200HelperCls();
                ShowLogExecute();
                loRtnTemp = loCls.GetUtilityTypeList(new GeneralParamDTO()
                {
                    CAPPLICATION_ID=PMB02200ContextConstant.CAPPLICATION_ID,
                    CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID,
                    CCLASS_ID=PMB02200ContextConstant.CCLASS_ID,
                    CLANGUAGE_ID = R_BackGlobalVar.CULTURE,
                    CREC_ID_LIST=PMB02200ContextConstant.CREC_ID_LIST
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

        #region logger

        private void ShowLogStart([CallerMemberName] string pcMethodCallerName = "") => _logger.LogInfo($"Starting {pcMethodCallerName} in {GetType().Name}");

        private void ShowLogExecute([CallerMemberName] string pcMethodCallerName = "") => _logger.LogInfo($"Executing cls method in {GetType().Name}.{pcMethodCallerName}");

        private void ShowLogEnd([CallerMemberName] string pcMethodCallerName = "") => _logger.LogInfo($"End {pcMethodCallerName} in {GetType().Name}");

        private void ShowLogError(Exception exception, [CallerMemberName] string pcMethodCallerName = "") => _logger.LogError(exception);

        #endregion
    }
}
