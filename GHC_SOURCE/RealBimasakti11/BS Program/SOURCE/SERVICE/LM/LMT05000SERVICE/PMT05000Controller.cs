using System.Runtime.InteropServices;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System.Runtime.CompilerServices;
using R_OpenTelemetry;
using System.Diagnostics;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using PMT05000COMMON;
using PMT05000COMMON.DTO_s;
using PMT05000BACK;
namespace PMT05000SERVICE
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PMT05000Controller : ControllerBase, IPMM05000Init
    {
        private LogerPMT05000 _logger;

        private readonly ActivitySource _activitySource;

        public PMT05000Controller(ILogger<PMT05000Controller> logger)
        {
            //initiate
            LogerPMT05000.R_InitializeLogger(logger);
            _logger = LogerPMT05000.R_GetInstanceLogger();
            _activitySource = PMT05000Activity.R_InitializeAndGetActivitySource(GetType().Name);
        }

        private async IAsyncEnumerable<T> StreamListHelper<T>(List<T> poList)
        {
            foreach (T loEntity in poList)
            {
                yield return loEntity;
            }
        }

        [HttpPost]
        public IAsyncEnumerable<GSB_CodeInfoDTO> GetGSBCodeInfoList()
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            var loEx = new R_Exception();
            IAsyncEnumerable<GSB_CodeInfoDTO> loRtn = null;

            try
            {
                var loCls = new PMM05000InitCls();

                ShowLogExecute();
                var loParam = new GSB_CodeInfoParam()
                {
                    CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID,
                    CLASS_APPLICATION = ContextConstantPMT05000.CLASS_APPLICATION,
                    CLASS_ID = ContextConstantPMT05000.CLASS_ID,
                    REC_ID_LIST = ContextConstantPMT05000.REC_ID_LIST,
                    LANG_ID = R_BackGlobalVar.CULTURE
                };
                var loTempRtn = loCls.GetGSBCodeInfoList(loParam);

                loRtn = StreamListHelper<GSB_CodeInfoDTO>(loTempRtn);
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
        public IAsyncEnumerable<GSPeriodDT_DTO> GetGSPeriodDTList()
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            var loEx = new R_Exception();
            IAsyncEnumerable<GSPeriodDT_DTO> loRtn = null;

            try
            {
                var loCls = new PMM05000InitCls();

                ShowLogExecute();
                var loParam = new GSTPeriodDT_Param()
                {
                    CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID,
                    CYEAR = R_Utility.R_GetStreamingContext<string>(ContextConstantPMT05000.CYEAR),
                };
                var loTempRtn = loCls.GetGSPeriodDT(loParam);

                loRtn = StreamListHelper<GSPeriodDT_DTO>(loTempRtn);
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
            PMM05000InitCls loCls;
            try
            {
                loCls = new PMM05000InitCls();
                ShowLogExecute();
                loRtnTemp = loCls.GetPropertyList(new PropertyParamDTO()
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
        EndBlock:
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
