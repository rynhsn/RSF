using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PMR00600BACK;
using PMR00600COMMON;
using PMR00600COMMON.DTO_s;
using R_BackEnd;
using R_Common;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace PMR00600SERVICE
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PMR00600Controller : ControllerBase,IPMR00600General
    {
        private PMR00600Logger _logger;

        private readonly ActivitySource _activitySource;

        //constructor
        public PMR00600Controller(ILogger<PMR00600Controller> logger)
        {
            //initiate
            PMR00600Logger.R_InitializeLogger(logger);
            _logger = PMR00600Logger.R_GetInstanceLogger();
            _activitySource = PMR00600Activity.R_InitializeAndGetActivitySource(GetType().Name);
        }

        [HttpPost]
        public IAsyncEnumerable<PropertyDTO> GetPropertyList()
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            R_Exception loException = new R_Exception();
            List<PropertyDTO> loRtnTemp = null;
            PMR00600GeneralCls loCls;
            try
            {
                loCls = new PMR00600GeneralCls();
                ShowLogExecute();
                loRtnTemp = loCls.GetPropertyList(new PropertyDTO()
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
        public PMR00600Result<TodayDTO> GetTodayDate()
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            var loEx = new R_Exception();
            PMR00600Result<TodayDTO> loRtn = new PMR00600Result<TodayDTO>();

            try
            {
                var loCls = new PMR00600GeneralCls();

                ShowLogExecute();
                loRtn.Data = loCls.GetTodayDateRecord(R_BackGlobalVar.COMPANY_ID);
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
        public PMR00600Result<PeriodYearDTO> GetPeriodYearRecord(PeriodYearDTO poParam) {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            var loEx = new R_Exception();
            PMR00600Result<PeriodYearDTO> loRtn = new PMR00600Result<PeriodYearDTO>();

            try
            {
                var loCls = new PMR00600GeneralCls();

                ShowLogExecute();
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loRtn.Data = loCls.GetPeriodYearRecord(poParam);
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
