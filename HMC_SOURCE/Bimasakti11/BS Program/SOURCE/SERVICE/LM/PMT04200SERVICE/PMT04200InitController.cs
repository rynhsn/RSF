using PMT04200Back;
using PMT04200Common.Loggers;
using PMT04200Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_Common;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using PMT04200Common.DTOs;
using R_BackEnd;

namespace PMT04200SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class PMT04200InitController : ControllerBase, IPMT04200Init
    {
        private LoggerPMT04200InitialProcess _Logger;
        private readonly ActivitySource _activitySource;

        public PMT04200InitController(ILogger<LoggerPMT04200InitialProcess> logger)
        {
            //Initial and Get Logger
            LoggerPMT04200InitialProcess.R_InitializeLogger(logger);
            _Logger = LoggerPMT04200InitialProcess.R_GetInstanceLogger();
            _activitySource = PMT04200ActivityInitSourceBase.R_InitializeAndGetActivitySource(GetType().Name);
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
        public IAsyncEnumerable<PropertyListDTO> GetPropertyList()
        {
            R_Exception loException = new R_Exception();
            IAsyncEnumerable<PropertyListDTO> loRtn = null;
            PMT04200InitCls loCls;
            PMT04200ParamDTO loPar;
            List<PropertyListDTO> loRtnTmp;

            try
            {
                loPar = new PMT04200ParamDTO();
              
                loCls = new PMT04200InitCls();

                loRtnTmp = loCls.PropertyListDB();

                loRtn = StreamListData<PropertyListDTO>(loRtnTmp);

            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<PMT04200GSCurrencyDTO> GetCurrencyList()
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            var loEx = new R_Exception();
            IAsyncEnumerable<PMT04200GSCurrencyDTO> loRtn = null;

            try
            {
                var loCls = new PMT04200InitCls();

                ShowLogExecute();
                var loTempRtn = loCls.GetCurrencyList();

                loRtn = StreamListData<PMT04200GSCurrencyDTO>(loTempRtn);
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
        public PMT04200RecordResult<PMT04200GLSystemParamDTO> GetGLSystemParam()
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            var loEx = new R_Exception();
            PMT04200RecordResult<PMT04200GLSystemParamDTO> loRtn = new PMT04200RecordResult<PMT04200GLSystemParamDTO>();
            try
            {
                var loCls = new PMT04200InitCls();

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
        public PMT04200RecordResult<PMT04200CBSystemParamDTO> GetCBSystemParam()
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            var loEx = new R_Exception();
            PMT04200RecordResult<PMT04200CBSystemParamDTO> loRtn = new PMT04200RecordResult<PMT04200CBSystemParamDTO>();
            try
            {
                var loCls = new PMT04200InitCls();

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
        public PMT04200RecordResult<PMT04200PMSystemParamDTO> GetPMSystemParam()
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            var loEx = new R_Exception();
            PMT04200RecordResult<PMT04200PMSystemParamDTO> loRtn = new PMT04200RecordResult<PMT04200PMSystemParamDTO>();
            try
            {
                var loCls = new PMT04200InitCls();

                ShowLogExecute();
                loRtn.Data = loCls.GetPMSystemParamRecord();
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
        public IAsyncEnumerable<PMT04200GSGSBCodeDTO> GetGSBCodeList()
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            var loEx = new R_Exception();
            IAsyncEnumerable<PMT04200GSGSBCodeDTO> loRtn = null;

            try
            {
                var loCls = new PMT04200InitCls();

                ShowLogExecute();
                var loTempRtn = loCls.GetGSBCodeList();

                loRtn = StreamListData<PMT04200GSGSBCodeDTO>(loTempRtn);
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
        public PMT04200RecordResult<PMT04200LastCurrencyRateDTO> GetLastCurrencyRate(PMT04200LastCurrencyRateDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("GetLastCurrencyRate");
            var loEx = new R_Exception();
            PMT04200RecordResult<PMT04200LastCurrencyRateDTO> loRtn = new PMT04200RecordResult<PMT04200LastCurrencyRateDTO>();
            _Logger.LogInfo("Start GetLastCurrencyRate");

            try
            {
                var loCls = new PMT04200InitCls();

                _Logger.LogInfo("Call Back Method GetLastCurrency");
                loRtn.Data = loCls.GetLastCurrency(poEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetLastCurrencyRate");

            return loRtn;
        }
        [HttpPost]
        public PMT04200RecordResult<PMT04200InitDTO> GetTabTransactionListInitVar()
        {
            using Activity activity = _activitySource.StartActivity("GetTabJournalListInitialProcess");
            var loEx = new R_Exception();
            PMT04200RecordResult<PMT04200InitDTO> loRtn = new PMT04200RecordResult<PMT04200InitDTO>();
            _Logger.LogInfo("Start GetTabJournalListInitialProcess");

            try
            {
                var loCls = new PMT04200InitCls();
                _Logger.LogInfo("Call All Back Method GetTabJournalListInitialProcess");
                var loGLSystemParam = loCls.GetGLSystemParamRecord();
                var loTempResult = new PMT04200InitDTO
                {
                    VAR_CB_SYSTEM_PARAM = loCls.GetCBSystemParamRecord(),
                    VAR_GSM_TRANSACTION_CODE = loCls.GetTransCodeInfoRecord(),
                    VAR_GSM_COMPANY = loCls.GetCompanyInfoRecord(),
                    VAR_TODAY = loCls.GetTodayDateRecord(),
                    VAR_SOFT_PERIOD_START_DATE = loCls.GetPeriodDTInfoRecord(new PMT04200ParamGSPeriodDTInfoDTO { CCYEAR = loGLSystemParam.CSOFT_PERIOD_YY, CPERIOD_NO = loGLSystemParam.CSOFT_PERIOD_MM }),
                    VAR_PM_SYSTEM_PARAM = loCls.GetPMSystemParamRecord(),
                    VAR_GL_SYSTEM_PARAM = loCls.GetGLSystemParamRecord(),
                    VAR_GSB_CODE_LIST = loCls.GetGSBCodeList(),
                    VAR_GS_PROPERTY_LIST = loCls.PropertyListDB()
                };

                loRtn.Data = loTempResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetTabJournalListInitialProcess");

            return loRtn;
        }
        [HttpPost]
        public PMT04200RecordResult<PMT04200JournalListInitialProcessDTO> GetTabJournalListInitialProcess()
        {
            using Activity activity = _activitySource.StartActivity("GetTabJournalListInitialProcess");
            var loEx = new R_Exception();
            PMT04200RecordResult<PMT04200JournalListInitialProcessDTO> loRtn = new PMT04200RecordResult<PMT04200JournalListInitialProcessDTO>();
            _Logger.LogInfo("Start GetTabJournalListInitialProcess");

            try
            {
                var loCls = new PMT04200InitCls();
                _Logger.LogInfo("Call All Back Method GetTabJournalListInitialProcess");
                var loTempResult = new PMT04200JournalListInitialProcessDTO
                {
                    VAR_PM_SYSTEM_PARAM = loCls.GetPMSystemParamRecord(),
                    VAR_GL_SYSTEM_PARAM = loCls.GetGLSystemParamRecord(),
                    VAR_GSB_CODE_LIST = loCls.GetGSBCodeList(),
                };

                loRtn.Data = loTempResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetTabJournalListInitialProcess");

            return loRtn;
        }
        [HttpPost]
        public PMT04200RecordResult<PMT04210JournalEntryInitialProcessDTO> GetTabJournalEntryInitialProcess()
        {
            using Activity activity = _activitySource.StartActivity("GetTabJournalListInitialProcess");
            var loEx = new R_Exception();
            PMT04200RecordResult<PMT04210JournalEntryInitialProcessDTO> loRtn = new PMT04200RecordResult<PMT04210JournalEntryInitialProcessDTO>();
            _Logger.LogInfo("Start GetTabJournalListInitialProcess");

            try
            {
                var loCls = new PMT04200InitCls();
                _Logger.LogInfo("Call All Back Method GetTabJournalListInitialProcess");
                var loCbSystemParam = loCls.GetGLSystemParamRecord();
                var loTempResult = new PMT04210JournalEntryInitialProcessDTO
                {
                    VAR_CB_SYSTEM_PARAM = loCls.GetCBSystemParamRecord(),
                    VAR_GSM_TRANSACTION_CODE = loCls.GetTransCodeInfoRecord(),
                    VAR_PM_SYSTEM_PARAM = loCls.GetPMSystemParamRecord(),
                    VAR_PROPERTY_LIST = loCls.PropertyListDB(),
                    VAR_GSM_COMPANY = loCls.GetCompanyInfoRecord(),
                    VAR_TODAY = loCls.GetTodayDateRecord(),
                    VAR_SOFT_PERIOD_START_DATE = loCls.GetPeriodDTInfoRecord(new PMT04200ParamGSPeriodDTInfoDTO { CCYEAR = loCbSystemParam.CSOFT_PERIOD_YY, CPERIOD_NO = loCbSystemParam.CSOFT_PERIOD_MM }),
                };

                loRtn.Data = loTempResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetTabJournalListInitialProcess");

            return loRtn;
        }

        [HttpPost]
        public PMT04200RecordResult<PMT04200GSCompanyInfoDTO> GetGSCompanyInfo()
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            var loEx = new R_Exception();
            PMT04200RecordResult<PMT04200GSCompanyInfoDTO> loRtn = new PMT04200RecordResult<PMT04200GSCompanyInfoDTO>();
            try
            {
                var loCls = new PMT04200InitCls();

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
        public PMT04200RecordResult<PMT04200GSPeriodDTInfoDTO> GetGSPeriodDTInfo(PMT04200ParamGSPeriodDTInfoDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            var loEx = new R_Exception();
            PMT04200RecordResult<PMT04200GSPeriodDTInfoDTO> loRtn = new PMT04200RecordResult<PMT04200GSPeriodDTInfoDTO>();
            try
            {
                var loCls = new PMT04200InitCls();

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
        public PMT04200RecordResult<PMT04200GSPeriodYearRangeDTO> GetGSPeriodYearRange()
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            var loEx = new R_Exception();
            PMT04200RecordResult<PMT04200GSPeriodYearRangeDTO> loRtn = new PMT04200RecordResult<PMT04200GSPeriodYearRangeDTO>();
            try
            {
                var loCls = new PMT04200InitCls();

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
        public PMT04200RecordResult<PMT04200GSTransInfoDTO> GetGSTransCodeInfo()
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            var loEx = new R_Exception();
            PMT04200RecordResult<PMT04200GSTransInfoDTO> loRtn = new PMT04200RecordResult<PMT04200GSTransInfoDTO>();
            try
            {
                var loCls = new PMT04200InitCls();

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
        public PMT04200RecordResult<PMT04200InitDTO> GetTabJournalListInitVar()
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            var loEx = new R_Exception();
            PMT04200RecordResult<PMT04200InitDTO> loRtn = new PMT04200RecordResult<PMT04200InitDTO>();

            try
            {
                var loCls = new PMT04200InitCls();
                ShowLogExecute();

                loRtn.Data = new()
                {
                    VAR_GL_SYSTEM_PARAM = loCls.GetGLSystemParamRecord(),
                    VAR_GSM_TRANSACTION_CODE = loCls.GetTransCodeInfoRecord(),
                    VAR_TODAY = loCls.GetTodayDateRecord(),
                    VAR_GSM_COMPANY = loCls.GetCompanyInfoRecord(),
                    VAR_CB_SYSTEM_PARAM = loCls.GetCBSystemParamRecord(),
                    VAR_PM_SYSTEM_PARAM = loCls.GetPMSystemParamRecord(),
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
        public PMT04200RecordResult<PMT04200TodayDateDTO> GetTodayDate()
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            var loEx = new R_Exception();
            PMT04200RecordResult<PMT04200TodayDateDTO> loRtn = new PMT04200RecordResult<PMT04200TodayDateDTO>();
            try
            {
                var loCls = new PMT04200InitCls();

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

        #region logger

        private void ShowLogStart([CallerMemberName] string pcMethodCallerName = "") => _Logger.LogInfo($"Starting {pcMethodCallerName} in {GetType().Name}");

        private void ShowLogExecute([CallerMemberName] string pcMethodCallerName = "") => _Logger.LogInfo($"Executing cls method in {GetType().Name}.{pcMethodCallerName}");

        private void ShowLogEnd([CallerMemberName] string pcMethodCallerName = "") => _Logger.LogInfo($"End {pcMethodCallerName} in {GetType().Name}");

        private void ShowLogError(Exception exception, [CallerMemberName] string pcMethodCallerName = "") => _Logger.LogError(exception);
        #endregion
    }
}

