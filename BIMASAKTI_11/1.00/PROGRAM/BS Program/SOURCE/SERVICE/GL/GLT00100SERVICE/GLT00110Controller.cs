using GLT00100BACK;
using GLT00100COMMON;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace GLT00100SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class GLT00110Controller : ControllerBase, IGLT00110
    {
        private LoggerGLT00100 _logger;

        private readonly ActivitySource _activitySource;

        public GLT00110Controller(ILogger<LoggerGLT00100Universal> logger)
        {
            LoggerGLT00100.R_InitializeLogger(logger);
            _logger = LoggerGLT00100.R_GetInstanceLogger();
            _activitySource = GLT00100Activity.R_InitializeAndGetActivitySource(GetType().Name);
        }

        #region Stream List Data
        private async IAsyncEnumerable<T> StreamListData<T>(List<T> poParameter)
        {
            foreach (var item in poParameter)
            {
                yield return item;
            }
        }
        #endregion


        [HttpPost]
        public GLT00100RecordResult<GLT00110LastCurrencyRateDTO> GetLastCurrency(GLT00110LastCurrencyRateDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            var loEx = new R_Exception();
            GLT00100RecordResult<GLT00110LastCurrencyRateDTO> loRtn = new GLT00100RecordResult<GLT00110LastCurrencyRateDTO>();

            try
            {
                var loCls = new GLT00110Cls();
                ShowLogExecute();
                loRtn.Data = loCls.GetLastCurrency(poEntity);
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
        public GLT00100RecordResult<GLT00110DTO> GetJournalRecord(GLT00110DTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            var loEx = new R_Exception();
            GLT00100RecordResult<GLT00110DTO> loRtn = new GLT00100RecordResult<GLT00110DTO>();

            try
            {
                var loCls = new GLT00110Cls();

                loRtn.Data = loCls.GetJournalDisplay(poEntity);
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
        public GLT00100RecordResult<GLT00110DTO> SaveJournal(GLT00110HeaderDetailDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            var loEx = new R_Exception();
            GLT00100RecordResult<GLT00110DTO> loRtn = new GLT00100RecordResult<GLT00110DTO>();

            try
            {
                var loCls = new GLT00110Cls();

                //Save
                var loResult = loCls.SaveJournal(poEntity);
                
                //Get
                loRtn.Data = loCls.GetJournalDisplay(loResult);
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

        #region logger

        private void ShowLogStart([CallerMemberName] string pcMethodCallerName = "") => _logger.LogInfo($"Starting {pcMethodCallerName} in {GetType().Name}");

        private void ShowLogExecute([CallerMemberName] string pcMethodCallerName = "") => _logger.LogInfo($"Executing cls method in {GetType().Name}.{pcMethodCallerName}");

        private void ShowLogEnd([CallerMemberName] string pcMethodCallerName = "") => _logger.LogInfo($"End {pcMethodCallerName} in {GetType().Name}");

        private void ShowLogError(Exception exception, [CallerMemberName] string pcMethodCallerName = "") => _logger.LogError(exception);

        #endregion
    }
}