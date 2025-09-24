using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PMR00220BACK;
using PMR00220COMMON;
using PMR00220COMMON.DTO_s;
using R_BackEnd;
using R_Common;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace PMR00220SERVICE
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PMR00220Controller : ControllerBase,IPMR00220General
    {
        private PMR00220Logger _logger;

        private readonly ActivitySource _activitySource;

        //constructor
        public PMR00220Controller(ILogger<PMR00220Controller> logger)
        {
            //initiate
            PMR00220Logger.R_InitializeLogger(logger);
            _logger = PMR00220Logger.R_GetInstanceLogger();
            _activitySource = PMR00220Activity.R_InitializeAndGetActivitySource(GetType().Name);
        }

        [HttpPost]
        public IAsyncEnumerable<PropertyDTO> GetPropertyList()
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            R_Exception loException = new R_Exception();
            List<PropertyDTO> loRtnTemp = null;
            PMR00220GeneralCls loCls;
            try
            {
                loCls = new PMR00220GeneralCls();
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
        public PMR00220ResultBaseDTO<TodayDTO> GetTodayDate()
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            var loEx = new R_Exception();
            PMR00220ResultBaseDTO<TodayDTO> loRtn = new PMR00220ResultBaseDTO<TodayDTO>();

            try
            {
                var loCls = new PMR00220GeneralCls();

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
        public IAsyncEnumerable<PeriodDtDTO> GetPeriodList()
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            R_Exception loException = new R_Exception();
            List<PeriodDtDTO> loRtnTemp = null;
            PMR00220GeneralCls loCls;
            try
            {
                loCls = new PMR00220GeneralCls();
                ShowLogExecute();
                loRtnTemp = loCls.GetPeriodDtList(R_BackGlobalVar.COMPANY_ID, R_Utility.R_GetStreamingContext<string>(PMR00220ContextConstant.CYEAR));
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
        public PMR00220ResultBaseDTO<PeriodYearDTO> GetPeriodYearRecord(PeriodYearDTO poParam) {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            var loEx = new R_Exception();
            PMR00220ResultBaseDTO<PeriodYearDTO> loRtn = new PMR00220ResultBaseDTO<PeriodYearDTO>();

            try
            {
                var loCls = new PMR00220GeneralCls();

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
