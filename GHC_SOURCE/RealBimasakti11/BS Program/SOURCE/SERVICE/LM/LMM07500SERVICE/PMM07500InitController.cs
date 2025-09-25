using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PMM07500BACK;
using PMM07500COMMON;
using PMM07500COMMON.DTO_s;
using PMM07500COMMON.Interfaces;
using R_BackEnd;
using R_Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PMM07500SERVICE
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PMM07500InitController : ControllerBase, IPMM07500General
    {
        private LoggerPMM07500 _logger;

        private readonly ActivitySource _activitySource;
        public PMM07500InitController(ILogger<LoggerPMM07500> logger)
        {
            //initiate
            LoggerPMM07500.R_InitializeLogger(logger);
            _logger = LoggerPMM07500.R_GetInstanceLogger();
            _activitySource = PMM07500Activity.R_InitializeAndGetActivitySource(GetType().Name);
        }

        [HttpPost]
        public IAsyncEnumerable<PropertyDTO> GetPropertyList()
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            R_Exception loException = new R_Exception();
            List<PropertyDTO> loRtnTemp = null;
            PMM007500InitCls loCls;
            try
            {
                loCls = new PMM007500InitCls();
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
        public IAsyncEnumerable<CurrencyDTO> GetCurrencyList()
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            R_Exception loException = new R_Exception();
            List<CurrencyDTO> loRtnTemp = null;
            PMM007500InitCls loCls;
            try
            {
                loCls = new PMM007500InitCls();
                ShowLogExecute();
                loRtnTemp = loCls.GetCurrencyList(new CurrencyDTO()
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
        public PMM07500ResultBaseDTO<TodayDTO> GetTodayDate()
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            var loEx = new R_Exception();
            PMM07500ResultBaseDTO<TodayDTO> loRtn = new PMM07500ResultBaseDTO<TodayDTO>();

            try
            {
                var loCls = new PMM007500InitCls();

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

        #region helper
        private async IAsyncEnumerable<T> StreamListHelper<T>(List<T> poList)
        {
            foreach (T loEntity in poList)
            {
                yield return loEntity;
            }
        }

        private void ShowLogStart([CallerMemberName] string pcMethodCallerName = "") => _logger.LogInfo($"Starting {pcMethodCallerName} in {GetType().Name}");

        private void ShowLogExecute([CallerMemberName] string pcMethodCallerName = "") => _logger.LogInfo($"Executing cls method in {GetType().Name}.{pcMethodCallerName}");

        private void ShowLogEnd([CallerMemberName] string pcMethodCallerName = "") => _logger.LogInfo($"End {pcMethodCallerName} in {GetType().Name}");

        private void ShowLogError(Exception exception, [CallerMemberName] string pcMethodCallerName = "") => _logger.LogError(exception);

        #endregion
    }
}
