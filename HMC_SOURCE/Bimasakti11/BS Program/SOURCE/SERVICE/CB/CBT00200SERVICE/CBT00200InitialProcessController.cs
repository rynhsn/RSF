using CBT00200BACK;
using CBT00200COMMON;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_Common;
using System.Diagnostics;

namespace CBT00200SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class CBT00200InitialProcessController : ControllerBase, ICBT00200InitialProcess
    {
        private LoggerCBT00200InitialProcess _Logger;
        private readonly ActivitySource _activitySource;
        public CBT00200InitialProcessController(ILogger<LoggerCBT00200InitialProcess> logger)
        {
            //Initial and Get Logger
            LoggerCBT00200InitialProcess.R_InitializeLogger(logger);
            _Logger = LoggerCBT00200InitialProcess.R_GetInstanceLogger();
            _activitySource = CBT00200InitialProcessActivityInitSourceBase.R_InitializeAndGetActivitySource(nameof(CBT00200InitialProcessController));
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

        #region Universal
        [HttpPost]
        public CBT00200SingleResult<CBT00200GSCompanyInfoDTO> GetGSCompanyInfo()
        {
            using Activity activity = _activitySource.StartActivity("GetGSCompanyInfo");
            var loEx = new R_Exception();
            CBT00200SingleResult<CBT00200GSCompanyInfoDTO> loRtn = new CBT00200SingleResult<CBT00200GSCompanyInfoDTO>();
            _Logger.LogInfo("Start GetGSCompanyInfo");

            try
            {
                var loCls = new CBT00200InitialProcessCls();

                _Logger.LogInfo("Call Back Method GetCompanyInfoRecord");
                loRtn.Data = loCls.GetCompanyInfoRecord();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetGSCompanyInfo");

            return loRtn;
        }

        [HttpPost]
        public CBT00200SingleResult<CBT00200GLSystemParamDTO> GetGLSystemParam()
        {
            using Activity activity = _activitySource.StartActivity("GetGLSystemParam");
            var loEx = new R_Exception();
            CBT00200SingleResult<CBT00200GLSystemParamDTO> loRtn = new CBT00200SingleResult<CBT00200GLSystemParamDTO>();
            _Logger.LogInfo("Start GetGLSystemParam");

            try
            {
                var loCls = new CBT00200InitialProcessCls();

                _Logger.LogInfo("Call Back Method GetGLSystemParamRecord");
                loRtn.Data = loCls.GetGLSystemParamRecord();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetGLSystemParam");

            return loRtn;
        }

        [HttpPost]
        public CBT00200SingleResult<CBT00200CBSystemParamDTO> GetCBSystemParam()
        {
            using Activity activity = _activitySource.StartActivity("GetCBSystemParam");
            var loEx = new R_Exception();
            CBT00200SingleResult<CBT00200CBSystemParamDTO> loRtn = new CBT00200SingleResult<CBT00200CBSystemParamDTO>();
            _Logger.LogInfo("Start GetCBSystemParam");

            try
            {
                var loCls = new CBT00200InitialProcessCls();

                _Logger.LogInfo("Call Back Method GetSystemParamCB");
                loRtn.Data = loCls.GetSystemParamCB();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetCBSystemParam");

            return loRtn;
        }

        [HttpPost]
        public CBT00200SingleResult<CBT00200GSTransInfoDTO> GetGSTransCodeInfo()
        {
            using Activity activity = _activitySource.StartActivity("GetGSTransCodeInfo");
            var loEx = new R_Exception();
            CBT00200SingleResult<CBT00200GSTransInfoDTO> loRtn = new CBT00200SingleResult<CBT00200GSTransInfoDTO>();
            _Logger.LogInfo("Start GetGSTransCodeInfo");

            try
            {
                var loCls = new CBT00200InitialProcessCls();

                _Logger.LogInfo("Call Back Method GetTransCodeInfoRecord");
                loRtn.Data = loCls.GetTransCodeInfoRecord();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetGSTransCodeInfo");

            return loRtn;
        }

        [HttpPost]
        public CBT00200SingleResult<CBT00200GSPeriodYearRangeDTO> GetGSPeriodYearRange()
        {
            using Activity activity = _activitySource.StartActivity("GetGSPeriodYearRange");
            var loEx = new R_Exception();
            CBT00200SingleResult<CBT00200GSPeriodYearRangeDTO> loRtn = new CBT00200SingleResult<CBT00200GSPeriodYearRangeDTO>();
            _Logger.LogInfo("Start GetGSPeriodYearRange");

            try
            {
                var loCls = new CBT00200InitialProcessCls();

                _Logger.LogInfo("Call Back Method GetPeriodYearRangeRecord");
                loRtn.Data = loCls.GetPeriodYearRangeRecord();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetGSPeriodYearRange");

            return loRtn;
        }

        [HttpPost]
        public CBT00200SingleResult<CBT00200TodayDateDTO> GetTodayDate()
        {
            using Activity activity = _activitySource.StartActivity("GetTodayDate");
            var loEx = new R_Exception();
            CBT00200SingleResult<CBT00200TodayDateDTO> loRtn = new CBT00200SingleResult<CBT00200TodayDateDTO>();
            _Logger.LogInfo("Start GetTodayDate");

            try
            {
                var loCls = new CBT00200InitialProcessCls();

                _Logger.LogInfo("Call Back Method GetTodayDateRecord");
                loRtn.Data = loCls.GetTodayDateRecord();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetTodayDate");

            return loRtn;
        }

        [HttpPost]
        public CBT00200SingleResult<CBT00200GSPeriodDTInfoDTO> GetGSPeriodDTInfo(CBT00200ParamGSPeriodDTInfoDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("GetGSPeriodDTInfo");
            var loEx = new R_Exception();
            CBT00200SingleResult<CBT00200GSPeriodDTInfoDTO> loRtn = new CBT00200SingleResult<CBT00200GSPeriodDTInfoDTO>();
            _Logger.LogInfo("Start GetGSPeriodDTInfo");

            try
            {
                var loCls = new CBT00200InitialProcessCls();

                _Logger.LogInfo("Call Back Method GetTodayDateRecord");
                loRtn.Data = loCls.GetPeriodDTInfoRecord(poEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetGSPeriodDTInfo");

            return loRtn;
        }

        [HttpPost]
        public CBT00200SingleResult<CBT00200LastCurrencyRateDTO> GetLastCurrencyRate(CBT00200LastCurrencyRateDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("GetLastCurrencyRate");
            var loEx = new R_Exception();
            CBT00200SingleResult<CBT00200LastCurrencyRateDTO> loRtn = new CBT00200SingleResult<CBT00200LastCurrencyRateDTO>();
            _Logger.LogInfo("Start GetLastCurrencyRate");

            try
            {
                var loCls = new CBT00200InitialProcessCls();

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
        public IAsyncEnumerable<CBT00200GSGSBCodeDTO> GetGSBCodeList()
        {
            using Activity activity = _activitySource.StartActivity("GetGSBCodeList");
            var loEx = new R_Exception();
            IAsyncEnumerable<CBT00200GSGSBCodeDTO> loRtn = null;
            _Logger.LogInfo("Start GetGSBCodeList");

            try
            {
                var loCls = new CBT00200InitialProcessCls();

                _Logger.LogInfo("Call Back Method GetGSBCodeList");
                var loTempRtn = loCls.GetGSBCodeList();

                _Logger.LogInfo("Call Stream Method Data GetGSBCodeList");
                loRtn = StreamListData<CBT00200GSGSBCodeDTO>(loTempRtn);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetGSBCodeList");

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<CBT00200GSCenterDTO> GetCenterList()
        {
            using Activity activity = _activitySource.StartActivity("GetCenterList");
            var loEx = new R_Exception();
            IAsyncEnumerable<CBT00200GSCenterDTO> loRtn = null;
            _Logger.LogInfo("Start GetCenterList");

            try
            {
                var loCls = new CBT00200InitialProcessCls();

                _Logger.LogInfo("Call Back Method GetGSBCodeList");
                var loTempRtn = loCls.GetCenterList();

                _Logger.LogInfo("Call Stream Method Data GetCenterList");
                loRtn = StreamListData<CBT00200GSCenterDTO>(loTempRtn);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetCenterList");

            return loRtn;
        }
        #endregion

        [HttpPost]
        public CBT00200SingleResult<CBT00200JournalListInitialProcessDTO> GetTabJournalListInitialProcess()
        {
            using Activity activity = _activitySource.StartActivity("GetTabJournalListInitialProcess");
            var loEx = new R_Exception();
            CBT00200SingleResult<CBT00200JournalListInitialProcessDTO> loRtn = new CBT00200SingleResult<CBT00200JournalListInitialProcessDTO>();
            _Logger.LogInfo("Start GetTabJournalListInitialProcess");

            try
            {
                var loCls = new CBT00200InitialProcessCls();
                _Logger.LogInfo("Call All Back Method GetTabJournalListInitialProcess");
                var loTempResult = new CBT00200JournalListInitialProcessDTO
                {
                    VAR_CB_SYSTEM_PARAM = loCls.GetSystemParamCB(),
                    VAR_GL_SYSTEM_PARAM = loCls.GetGLSystemParamRecord(),
                    VAR_GSM_PERIOD = loCls.GetPeriodYearRangeRecord(),
                    VAR_GSM_TRANSACTION_CODE = loCls.GetTransCodeInfoRecord()
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
        public CBT00200SingleResult<CBT00210JournalEntryInitialProcessDTO> GetTabJournalEntryInitialProcess()
        {
            using Activity activity = _activitySource.StartActivity("GetTabJournalListInitialProcess");
            var loEx = new R_Exception();
            CBT00200SingleResult<CBT00210JournalEntryInitialProcessDTO> loRtn = new CBT00200SingleResult<CBT00210JournalEntryInitialProcessDTO>();
            _Logger.LogInfo("Start GetTabJournalListInitialProcess");

            try
            {
                var loCls = new CBT00200InitialProcessCls();
                _Logger.LogInfo("Call All Back Method GetTabJournalListInitialProcess");
                var loCbSystemParam = loCls.GetGLSystemParamRecord();
                var loTempResult = new CBT00210JournalEntryInitialProcessDTO
                {
                    VAR_CB_SYSTEM_PARAM = loCls.GetSystemParamCB(),
                    VAR_GL_SYSTEM_PARAM = loCbSystemParam,
                    VAR_GSM_TRANSACTION_CODE = loCls.GetTransCodeInfoRecord(),
                    VAR_GSM_COMPANY = loCls.GetCompanyInfoRecord(),
                    VAR_TODAY = loCls.GetTodayDateRecord(),
                    VAR_SOFT_PERIOD_START_DATE = loCls.GetPeriodDTInfoRecord(new CBT00200ParamGSPeriodDTInfoDTO { CCYEAR = loCbSystemParam.CSOFT_PERIOD_YY, CPERIOD_NO = loCbSystemParam.CSOFT_PERIOD_MM }),
                    VAR_CENTER_LIST = loCls.GetCenterList()
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
    }
}