using APB00200BACK;
using APB00200COMMON;
using APB00200COMMON.DTO_s;
using APB00200COMMON.DTO_s.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace APB00200SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class APB00200Controller : ControllerBase, IAPB00200
    {
        //variable & constructor
        private LoggerAPB00200 _logger;
        private readonly ActivitySource _activitySource;
        public APB00200Controller(ILogger<APB00200Controller> logger)
        {
            LoggerAPB00200.R_InitializeLogger(logger);
            _logger = LoggerAPB00200.R_GetInstanceLogger();
            _activitySource = APB00200Activity.R_InitializeAndGetActivitySource(GetType().Name);
        }

        //helpers
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

        //methods
        [HttpPost]
        public RecordResultAPI<ClosePeriodDTO> GetRecord_ClosePeriod(ClosePeriodParam poParam)
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            var loEx = new R_Exception();
            RecordResultAPI<ClosePeriodDTO> loRtn = new();

            try
            {
                var loCls = new APB00200Cls();
                ShowLogExecute();
                poParam.CUSER_LOGIN_ID = R_BackGlobalVar.USER_ID;
                poParam.CCOMPANY_ID= R_BackGlobalVar.COMPANY_ID;
                loRtn.Data = loCls.GetRecord_ClosePeriod(poParam);
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
        public RecordResultAPI<CloseAPProcessResultDTO> ProcessCloseAPPeriod(CloseAPProcessParam poParam)
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            R_Exception loException = new R_Exception();
            RecordResultAPI<CloseAPProcessResultDTO> loRtn = new() ;
            APB00200Cls loCls;
            try
            {
                loCls = new();
                ShowLogExecute();
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CUSER_ID = R_BackGlobalVar.USER_ID;
                loRtn.Data = loCls.CloseAPProcess(poParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                ShowLogError(loException);
            }
            loException.ThrowExceptionIfErrors();
            ShowLogEnd();
            return loRtn;
        }
        
        [HttpPost]
        public IAsyncEnumerable<ErrorCloseAPProcessDTO> GetList_ErrorProcessCloseAPPeriod(CloseAPProcessParam poParam)
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            R_Exception loException = new R_Exception();
            List<ErrorCloseAPProcessDTO> loRtnTemp = new();
            try
            {
                var loCls = new APB00200Cls();
                ShowLogExecute();
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CUSER_ID = R_BackGlobalVar.USER_ID;
                loRtnTemp = loCls.GetList_ErrorCloseAPProcess(poParam);
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
    }
}
