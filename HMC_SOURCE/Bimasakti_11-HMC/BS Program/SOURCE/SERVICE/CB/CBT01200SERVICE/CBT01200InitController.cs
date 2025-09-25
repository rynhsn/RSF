using CBT01200Back;
using CBT01200Common.Loggers;
using CBT01200Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_Common;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using CBT01200Common.DTOs;

namespace CBT01200SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class CBT01200InitController : ControllerBase, ICBT01200Init
    {
        private LoggerInitCBT01200 _logger;
        private readonly ActivitySource _activitySource;

        public CBT01200InitController(ILogger<LoggerInitCBT01200> logger)
        {
            //Initial and Get Logger
            LoggerInitCBT01200.R_InitializeLogger(logger);
            _logger = LoggerInitCBT01200.R_GetInstanceLogger();
            _activitySource = CBT01200Activity.R_InitializeAndGetActivitySource(GetType().Name);
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
        public IAsyncEnumerable<CBT01200GSCenterDTO> GetCenterList()
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            var loEx = new R_Exception();
            IAsyncEnumerable<CBT01200GSCenterDTO> loRtn = null;

            try
            {
                var loCls = new CBT01200InitCls();

                ShowLogExecute();
                var loTempRtn = loCls.GetCenterList();

                loRtn = StreamListData<CBT01200GSCenterDTO>(loTempRtn);
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
        public IAsyncEnumerable<CBT01200GSCurrencyDTO> GetCurrencyList()
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            var loEx = new R_Exception();
            IAsyncEnumerable<CBT01200GSCurrencyDTO> loRtn = null;

            try
            {
                var loCls = new CBT01200InitCls();

                ShowLogExecute();
                var loTempRtn = loCls.GetCurrencyList();

                loRtn = StreamListData<CBT01200GSCurrencyDTO>(loTempRtn);
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
        public CBT01200RecordResult<CBT01200GLSystemParamDTO> GetGLSystemParam()
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            var loEx = new R_Exception();
            CBT01200RecordResult<CBT01200GLSystemParamDTO> loRtn = new CBT01200RecordResult<CBT01200GLSystemParamDTO>();
            try
            {
                var loCls = new CBT01200InitCls();

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
        public CBT01200RecordResult<CBT01200CBSystemParamDTO> GetCBSystemParam()
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            var loEx = new R_Exception();
            CBT01200RecordResult<CBT01200CBSystemParamDTO> loRtn = new CBT01200RecordResult<CBT01200CBSystemParamDTO>();
            try
            {
                var loCls = new CBT01200InitCls();

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
        public IAsyncEnumerable<CBT01200GSGSBCodeDTO> GetGSBCodeList()
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            var loEx = new R_Exception();
            IAsyncEnumerable<CBT01200GSGSBCodeDTO> loRtn = null;

            try
            {
                var loCls = new CBT01200InitCls();

                ShowLogExecute();
                var loTempRtn = loCls.GetGSBCodeList();

                loRtn = StreamListData<CBT01200GSGSBCodeDTO>(loTempRtn);
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
        public CBT01200RecordResult<CBT01200GSCompanyInfoDTO> GetGSCompanyInfo()
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            var loEx = new R_Exception();
            CBT01200RecordResult<CBT01200GSCompanyInfoDTO> loRtn = new CBT01200RecordResult<CBT01200GSCompanyInfoDTO>();
            try
            {
                var loCls = new CBT01200InitCls();

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
        public CBT01200RecordResult<CBT01200GSPeriodDTInfoDTO> GetGSPeriodDTInfo(CBT01200ParamGSPeriodDTInfoDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            var loEx = new R_Exception();
            CBT01200RecordResult<CBT01200GSPeriodDTInfoDTO> loRtn = new CBT01200RecordResult<CBT01200GSPeriodDTInfoDTO>();
            try
            {
                var loCls = new CBT01200InitCls();

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
        public CBT01200RecordResult<CBT01200GSPeriodYearRangeDTO> GetGSPeriodYearRange()
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            var loEx = new R_Exception();
            CBT01200RecordResult<CBT01200GSPeriodYearRangeDTO> loRtn = new CBT01200RecordResult<CBT01200GSPeriodYearRangeDTO>();
            try
            {
                var loCls = new CBT01200InitCls();

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
        public CBT01200RecordResult<CBT01200GLSystemEnableOptionInfoDTO> GetGSSystemEnableOptionInfo()
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            var loEx = new R_Exception();
            CBT01200RecordResult<CBT01200GLSystemEnableOptionInfoDTO> loRtn = new CBT01200RecordResult<CBT01200GLSystemEnableOptionInfoDTO>();
            try
            {
                var loCls = new CBT01200InitCls();
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
        public CBT01200RecordResult<CBT01200GSTransInfoDTO> GetGSTransCodeInfo()
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            var loEx = new R_Exception();
            CBT01200RecordResult<CBT01200GSTransInfoDTO> loRtn = new CBT01200RecordResult<CBT01200GSTransInfoDTO>();
            try
            {
                var loCls = new CBT01200InitCls();

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
        public CBT01200RecordResult<CBT01200InitDTO> GetTabJournalListInitVar()
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            var loEx = new R_Exception();
            CBT01200RecordResult<CBT01200InitDTO> loRtn = new CBT01200RecordResult<CBT01200InitDTO>();

            try
            {
                var loCls = new CBT01200InitCls();
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
        public CBT01200RecordResult<CBT01200TodayDateDTO> GetTodayDate()
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            var loEx = new R_Exception();
            CBT01200RecordResult<CBT01200TodayDateDTO> loRtn = new CBT01200RecordResult<CBT01200TodayDateDTO>();
            try
            {
                var loCls = new CBT01200InitCls();

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
        public CBT01200RecordResult<CBT01210InitDTO> GetTabJournalEntryInitVar()
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            var loEx = new R_Exception();
            CBT01200RecordResult<CBT01210InitDTO> loRtn = new  CBT01200RecordResult<CBT01210InitDTO>();

            try
            {
                var loCls = new CBT01200InitCls();

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

