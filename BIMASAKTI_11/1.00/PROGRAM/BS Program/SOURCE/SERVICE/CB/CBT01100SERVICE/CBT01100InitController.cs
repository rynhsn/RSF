using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_Common;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using CBT01100BACK;
using CBT01100COMMON;
using CBT01100COMMON.Loggers;

namespace CBT01100SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class CBT01100InitController : ControllerBase, ICBT01100Init
    {
        private LoggerInitCBT01100 _logger;
        private readonly ActivitySource _activitySource;

        public CBT01100InitController(ILogger<LoggerInitCBT01100> logger)
        {
            //Initial and Get Logger
            LoggerInitCBT01100.R_InitializeLogger(logger);
            _logger = LoggerInitCBT01100.R_GetInstanceLogger();
            _activitySource = CBT01100Activity.R_InitializeAndGetActivitySource(GetType().Name);
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
        public IAsyncEnumerable<CBT01100GSCenterDTO> GetCenterList()
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            var loEx = new R_Exception();
            IAsyncEnumerable<CBT01100GSCenterDTO> loRtn = null;

            try
            {
                var loCls = new CBT01100InitCls();

                ShowLogExecute();
                var loTempRtn = loCls.GetCenterList();

                loRtn = StreamListData<CBT01100GSCenterDTO>(loTempRtn);
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
        public IAsyncEnumerable<CBT01100GSCurrencyDTO> GetCurrencyList()
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            var loEx = new R_Exception();
            IAsyncEnumerable<CBT01100GSCurrencyDTO> loRtn = null;

            try
            {
                var loCls = new CBT01100InitCls();

                ShowLogExecute();
                var loTempRtn = loCls.GetCurrencyList();

                loRtn = StreamListData<CBT01100GSCurrencyDTO>(loTempRtn);
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
        public CBT01100RecordResult<CBT01100GLSystemParamDTO> GetGLSystemParam()
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            var loEx = new R_Exception();
            CBT01100RecordResult<CBT01100GLSystemParamDTO> loRtn = new CBT01100RecordResult<CBT01100GLSystemParamDTO>();
            try
            {
                var loCls = new CBT01100InitCls();

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
        public CBT01100RecordResult<CBT01100CBSystemParamDTO> GetCBSystemParam()
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            var loEx = new R_Exception();
            CBT01100RecordResult<CBT01100CBSystemParamDTO> loRtn = new CBT01100RecordResult<CBT01100CBSystemParamDTO>();
            try
            {
                var loCls = new CBT01100InitCls();

                ShowLogExecute();
                loRtn.Data = loCls.GetCBSystemParamRecord();
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
        public IAsyncEnumerable<CBT01100GSGSBCodeDTO> GetGSBCodeList()
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            var loEx = new R_Exception();
            IAsyncEnumerable<CBT01100GSGSBCodeDTO> loRtn = null;

            try
            {
                var loCls = new CBT01100InitCls();

                ShowLogExecute();
                var loTempRtn = loCls.GetGSBCodeList();

                loRtn = StreamListData<CBT01100GSGSBCodeDTO>(loTempRtn);
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
        public CBT01100RecordResult<CBT01100GSCompanyInfoDTO> GetGSCompanyInfo()
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            var loEx = new R_Exception();
            CBT01100RecordResult<CBT01100GSCompanyInfoDTO> loRtn = new CBT01100RecordResult<CBT01100GSCompanyInfoDTO>();
            try
            {
                var loCls = new CBT01100InitCls();

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
        public CBT01100RecordResult<CBT01100GSPeriodDTInfoDTO> GetGSPeriodDTInfo(CBT01100ParamGSPeriodDTInfoDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            var loEx = new R_Exception();
            CBT01100RecordResult<CBT01100GSPeriodDTInfoDTO> loRtn = new CBT01100RecordResult<CBT01100GSPeriodDTInfoDTO>();
            try
            {
                var loCls = new CBT01100InitCls();

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
        public CBT01100RecordResult<CBT01100GSPeriodYearRangeDTO> GetGSPeriodYearRange()
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            var loEx = new R_Exception();
            CBT01100RecordResult<CBT01100GSPeriodYearRangeDTO> loRtn = new CBT01100RecordResult<CBT01100GSPeriodYearRangeDTO>();
            try
            {
                var loCls = new CBT01100InitCls();

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
        public CBT01100RecordResult<CBT01100GLSystemEnableOptionInfoDTO> GetGSSystemEnableOptionInfo()
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            var loEx = new R_Exception();
            CBT01100RecordResult<CBT01100GLSystemEnableOptionInfoDTO> loRtn = new CBT01100RecordResult<CBT01100GLSystemEnableOptionInfoDTO>();
            try
            {
                var loCls = new CBT01100InitCls();
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
        public CBT01100RecordResult<CBT01100GSTransInfoDTO> GetGSTransCodeInfo()
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            var loEx = new R_Exception();
            CBT01100RecordResult<CBT01100GSTransInfoDTO> loRtn = new CBT01100RecordResult<CBT01100GSTransInfoDTO>();
            try
            {
                var loCls = new CBT01100InitCls();

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
        public CBT01100RecordResult<CBT01100InitDTO> GetTabJournalListInitVar()
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            var loEx = new R_Exception();
            CBT01100RecordResult<CBT01100InitDTO> loRtn = new CBT01100RecordResult<CBT01100InitDTO>();

            try
            {
                var loCls = new CBT01100InitCls();
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
        public CBT01100RecordResult<CBT01100TodayDateDTO> GetTodayDate()
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            var loEx = new R_Exception();
            CBT01100RecordResult<CBT01100TodayDateDTO> loRtn = new CBT01100RecordResult<CBT01100TodayDateDTO>();
            try
            {
                var loCls = new CBT01100InitCls();

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
        public CBT01100RecordResult<CBT01110InitDTO> GetTabJournalEntryInitVar()
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            var loEx = new R_Exception();
            CBT01100RecordResult<CBT01110InitDTO> loRtn = new CBT01100RecordResult<CBT01110InitDTO>();

            try
            {
                var loCls = new CBT01100InitCls();

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

