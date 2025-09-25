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
    public class GLT00100UniversalController : ControllerBase, IGLT00100Universal
    {
        private LoggerGLT00100Universal _logger;
        private readonly ActivitySource _activitySource;

        public GLT00100UniversalController(ILogger<LoggerGLT00100Universal> logger)
        {
            //Initial and Get Logger
            LoggerGLT00100Universal.R_InitializeLogger(logger);
            _logger = LoggerGLT00100Universal.R_GetInstanceLogger();
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
        public IAsyncEnumerable<GLT00100GSCenterDTO> GetCenterList()
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            var loEx = new R_Exception();
            IAsyncEnumerable<GLT00100GSCenterDTO> loRtn = null;

            try
            {
                var loCls = new GLT00100UniversalCls();

                ShowLogExecute();
                var loTempRtn = loCls.GetCenterList();

                loRtn = StreamListData<GLT00100GSCenterDTO>(loTempRtn);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                ShowLogError(loEx);

            }

            loEx.ThrowExceptionIfErrors();
            ShowLogEnd();
            _logger.LogInfo("End GetCenterList");

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<GLT00100GSCurrencyDTO> GetCurrencyList()
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            var loEx = new R_Exception();
            IAsyncEnumerable<GLT00100GSCurrencyDTO> loRtn = null;

            try
            {
                var loCls = new GLT00100UniversalCls();

                ShowLogExecute();
                var loTempRtn = loCls.GetCurrencyList();

                loRtn = StreamListData<GLT00100GSCurrencyDTO>(loTempRtn);
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
        public GLT00100RecordResult<GLT00100GLSystemParamDTO> GetGLSystemParam()
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            var loEx = new R_Exception();
            GLT00100RecordResult<GLT00100GLSystemParamDTO> loRtn = new GLT00100RecordResult<GLT00100GLSystemParamDTO>();

            try
            {
                var loCls = new GLT00100UniversalCls();

                ShowLogExecute();
                loRtn.Data = loCls.GetGLSystemParamRecord();
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
        public IAsyncEnumerable<GLT00100GSGSBCodeDTO> GetGSBCodeList()
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            var loEx = new R_Exception();
            IAsyncEnumerable<GLT00100GSGSBCodeDTO> loRtn = null;

            try
            {
                var loCls = new GLT00100UniversalCls();

                ShowLogExecute();
                var loTempRtn = loCls.GetGSBCodeList();

                loRtn = StreamListData<GLT00100GSGSBCodeDTO>(loTempRtn);
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
        public GLT00100RecordResult<GLT00100GSCompanyInfoDTO> GetGSCompanyInfo()
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            var loEx = new R_Exception();
            GLT00100RecordResult<GLT00100GSCompanyInfoDTO> loRtn = new GLT00100RecordResult<GLT00100GSCompanyInfoDTO>();

            try
            {
                var loCls = new GLT00100UniversalCls();

                ShowLogExecute();
                loRtn.Data = loCls.GetCompanyInfoRecord();
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
        public GLT00100RecordResult<GLT00100GSPeriodDTInfoDTO> GetGSPeriodDTInfo(GLT00100ParamGSPeriodDTInfoDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            var loEx = new R_Exception();
            GLT00100RecordResult<GLT00100GSPeriodDTInfoDTO> loRtn = new GLT00100RecordResult<GLT00100GSPeriodDTInfoDTO>();

            try
            {
                var loCls = new GLT00100UniversalCls();

                ShowLogExecute();
                loRtn.Data = loCls.GetPeriodDTInfoRecord(poEntity);
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
        public GLT00100RecordResult<GLT00100GSPeriodYearRangeDTO> GetGSPeriodYearRange()
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            var loEx = new R_Exception();
            GLT00100RecordResult<GLT00100GSPeriodYearRangeDTO> loRtn = new GLT00100RecordResult<GLT00100GSPeriodYearRangeDTO>();

            try
            {
                var loCls = new GLT00100UniversalCls();

                ShowLogExecute();
                loRtn.Data = loCls.GetPeriodYearRangeRecord();
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
        public GLT00100RecordResult<GLT00100GLSystemEnableOptionInfoDTO> GetGSSystemEnableOptionInfo()
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            var loEx = new R_Exception();
            GLT00100RecordResult<GLT00100GLSystemEnableOptionInfoDTO> loRtn = new GLT00100RecordResult<GLT00100GLSystemEnableOptionInfoDTO>();

            try
            {
                var loCls = new GLT00100UniversalCls();

                ShowLogExecute();
                loRtn.Data = loCls.GetGLSystemEnableOptionRecord();
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
        public GLT00100RecordResult<GLT00100GSTransInfoDTO> GetGSTransCodeInfo()
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            var loEx = new R_Exception();
            GLT00100RecordResult<GLT00100GSTransInfoDTO> loRtn = new GLT00100RecordResult<GLT00100GSTransInfoDTO>();

            try
            {
                var loCls = new GLT00100UniversalCls();

                ShowLogExecute();
                loRtn.Data = loCls.GetTransCodeInfoRecord();
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
        public GLT00100RecordResult<GLT00100UniversalDTO> GetTabJournalListUniversalVar()
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            var loEx = new R_Exception();
            GLT00100RecordResult<GLT00100UniversalDTO> loRtn = new GLT00100RecordResult<GLT00100UniversalDTO>();

            try
            {
                var loCls = new GLT00100UniversalCls();
                ShowLogExecute();

                loRtn.Data = new()
                {
                    VAR_GL_SYSTEM_PARAM = loCls.GetGLSystemParamRecord(),
                    VAR_GSM_TRANSACTION_CODE = loCls.GetTransCodeInfoRecord(),
                    VAR_IUNDO_COMMIT_JRN = loCls.GetGLSystemEnableOptionRecord(),
                    VAR_GSB_CODE_LIST = loCls.GetGSBCodeList(),
                    VAR_GSM_PERIOD = loCls.GetPeriodYearRangeRecord(),
                };
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
        public GLT00100RecordResult<GLT00100TodayDateDTO> GetTodayDate()
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            var loEx = new R_Exception();
            GLT00100RecordResult<GLT00100TodayDateDTO> loRtn = new GLT00100RecordResult<GLT00100TodayDateDTO>();

            try
            {
                var loCls = new GLT00100UniversalCls();

                ShowLogExecute();
                loRtn.Data = loCls.GetTodayDateRecord();
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
        public GLT00100RecordResult<GLT00110UniversalDTO> GetTabJournalEntryUniversalVar()
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            var loEx = new R_Exception();
            GLT00100RecordResult<GLT00110UniversalDTO> loRtn = new GLT00100RecordResult<GLT00110UniversalDTO>();

            try
            {
                var loCls = new GLT00100UniversalCls();

                ShowLogExecute();
                loRtn.Data = new()
                {
                    VAR_GSM_TRANSACTION_CODE = loCls.GetTransCodeInfoRecord(),
                    VAR_GSM_COMPANY = loCls.GetCompanyInfoRecord(),
                    VAR_TODAY = loCls.GetTodayDateRecord(),
                    VAR_GL_SYSTEM_PARAM = loCls.GetGLSystemParamRecord(),
                    VAR_CENTER_LIST = loCls.GetCenterList(),
                    VAR_CURRENCY_LIST = loCls.GetCurrencyList(),
                    VAR_GSB_CODE_LIST = loCls.GetGSBCodeList(),
                    VAR_IUNDO_COMMIT_JRN = loCls.GetGLSystemEnableOptionRecord(),
                };
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