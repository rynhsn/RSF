using GLM00200BACK;
using GLM00200COMMON;
using GLM00200COMMON.DTO_s.Helper_DTO_s;
using GLM00200COMMON.Loggers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace GLM00200SERVICE
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GLM00200GeneralController : ControllerBase, IGLM00200Init
    {
        private LoggerGLM00200 _logger;

        private readonly ActivitySource _activitySource;

        public GLM00200GeneralController(ILogger<LoggerGLM00200> logger)
        {
            //Initial and Get Logger
            LoggerGLM00200.R_InitializeLogger(logger);
            _logger = LoggerGLM00200.R_GetInstanceLogger();
            _activitySource = GLM00200Activity.R_InitializeAndGetActivitySource(nameof(GLM00200GeneralController));
        }

        #region helper method

        private async IAsyncEnumerable<T> GetStreamData<T>(List<T> poParameter)
        {
            foreach (var loItem in poParameter)
            {
                yield return loItem;
            }
        }

        private void ShowLogStart([CallerMemberName] string pcMethodCallerName = "") => _logger.LogInfo($"Starting {pcMethodCallerName} in {GetType().Name}");

        private void ShowLogExecute([CallerMemberName] string pcMethodCallerName = "") => _logger.LogInfo($"Executing cls method in {GetType().Name}.{pcMethodCallerName}");

        private void ShowLogEnd([CallerMemberName] string pcMethodCallerName = "") => _logger.LogInfo($"End {pcMethodCallerName} in {GetType().Name}");

        private void ShowLogError(Exception exception, [CallerMemberName] string pcMethodCallerName = "") => _logger.LogError(exception);

        #endregion

        [HttpPost]
        public RecordResultDTO<AllInitRecordDTO> GetAllInitRecord()
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            var loEx = new R_Exception();
            RecordResultDTO<AllInitRecordDTO> loRtn = new();
            ShowLogStart();
            try
            {
                var loCls = new GLM00200GeneralCls();
                ShowLogExecute();
                loRtn.data = loCls.GetAllInitRecord(new GeneralParamDTO()
                {
                    CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID,
                    CUSER_ID = R_BackGlobalVar.USER_ID,
                    CLANG_ID = R_BackGlobalVar.CULTURE
                });
            }
            catch (Exception ex)
            {
                ShowLogError(loEx);
                loEx.Add(ex);
            }
            ShowLogEnd();
            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<CurrencyDTO> GetListCurrency()
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            var loEx = new R_Exception();
            IAsyncEnumerable<CurrencyDTO> loRtn = null;
            ShowLogStart();
            try
            {
                GLM00200GeneralCls loCls = new();
                ShowLogExecute();
                var loTempRtn = loCls.GetCurrencyList(new GeneralParamDTO()
                {
                    CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID,
                    CUSER_ID = R_BackGlobalVar.USER_ID,
                    CLANG_ID = R_BackGlobalVar.CULTURE
                });
                loRtn = GetStreamData<CurrencyDTO>(loTempRtn);
            }
            catch (Exception ex)
            {
                ShowLogError(loEx);
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            ShowLogEnd();
            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<StatusDTO> GetListStatus()
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            var loEx = new R_Exception();
            IAsyncEnumerable<StatusDTO> loRtn = null;
            ShowLogStart();
            try
            {
                GLM00200GeneralCls loCls = new();
                ShowLogExecute();
                var loTempRtn = loCls.GetStatusList(new GeneralParamDTO()
                {
                    CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID,
                    CUSER_ID = R_BackGlobalVar.USER_ID,
                    CLANG_ID = R_BackGlobalVar.CULTURE
                });
                loRtn = GetStreamData<StatusDTO>(loTempRtn);
            }
            catch (Exception ex)
            {
                ShowLogError(loEx);
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            ShowLogEnd();
            return loRtn;
        }

        [HttpPost]
        public RecordResultDTO<CurrencyRateResultDTO> GetCurrencyRateRecord(CurrencyRateParamDTO poParam)
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            var loEx = new R_Exception();
            RecordResultDTO<CurrencyRateResultDTO> loRtn = new();
            ShowLogStart();
            try
            {
                var loCls = new GLM00200GeneralCls();
                ShowLogExecute();
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CUSER_ID = R_BackGlobalVar.USER_ID;
                poParam.CLANG_ID = R_BackGlobalVar.CULTURE;
                loRtn.data = loCls.GetLastCurrencyRecord(poParam);
            }
            catch (Exception ex)
            {
                ShowLogError(loEx);
                loEx.Add(ex);
            }
            ShowLogEnd();
            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }

        [HttpPost]
        public AllInitRecordDTO GetAllInitRecord2()
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            var loEx = new R_Exception();
            AllInitRecordDTO loRtn = new();
            ShowLogStart();
            try
            {
                var loCls = new GLM00200GeneralCls();
                ShowLogExecute();
                loRtn = loCls.GetAllInitRecord(new GeneralParamDTO()
                {
                    CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID,
                    CUSER_ID = R_BackGlobalVar.USER_ID,
                    CLANG_ID = R_BackGlobalVar.CULTURE
                });
            }
            catch (Exception ex)
            {
                ShowLogError(loEx);
                loEx.Add(ex);
            }
            ShowLogEnd();
            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }
    }
}
