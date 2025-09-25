using PMT04100BACK;
using PMT04100COMMON;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_Common;
using System.Diagnostics;

namespace PMT04100SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class PMT04100InitialProcessController : ControllerBase, IPMT04100InitialProcess
    {
        private LoggerPMT04100InitialProcess _Logger;
        private readonly ActivitySource _activitySource;
        public PMT04100InitialProcessController(ILogger<LoggerPMT04100InitialProcess> logger)
        {
            //Initial and Get Logger
            LoggerPMT04100InitialProcess.R_InitializeLogger(logger);
            _Logger = LoggerPMT04100InitialProcess.R_GetInstanceLogger();
            _activitySource = PMT04100InitialProcessActivityInitSourceBase.R_InitializeAndGetActivitySource(nameof(PMT04100InitialProcessController));
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
        public PMT04100SingleResult<PMT04100GSCompanyInfoDTO> GetGSCompanyInfo()
        {
            using Activity activity = _activitySource.StartActivity("GetGSCompanyInfo");
            var loEx = new R_Exception();
            PMT04100SingleResult<PMT04100GSCompanyInfoDTO> loRtn = new PMT04100SingleResult<PMT04100GSCompanyInfoDTO>();
            _Logger.LogInfo("Start GetGSCompanyInfo");

            try
            {
                var loCls = new PMT04100InitialProcessCls();

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
        public PMT04100SingleResult<PMT04100GLSystemParamDTO> GetGLSystemParam()
        {
            using Activity activity = _activitySource.StartActivity("GetGLSystemParam");
            var loEx = new R_Exception();
            PMT04100SingleResult<PMT04100GLSystemParamDTO> loRtn = new PMT04100SingleResult<PMT04100GLSystemParamDTO>();
            _Logger.LogInfo("Start GetGLSystemParam");

            try
            {
                var loCls = new PMT04100InitialProcessCls();

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
        public PMT04100SingleResult<PMT04100CBSystemParamDTO> GetCBSystemParam()
        {
            using Activity activity = _activitySource.StartActivity("GetCBSystemParam");
            var loEx = new R_Exception();
            PMT04100SingleResult<PMT04100CBSystemParamDTO> loRtn = new PMT04100SingleResult<PMT04100CBSystemParamDTO>();
            _Logger.LogInfo("Start GetCBSystemParam");

            try
            {
                var loCls = new PMT04100InitialProcessCls();

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
        public PMT04100SingleResult<PMT04100GSTransInfoDTO> GetGSTransCodeInfo()
        {
            using Activity activity = _activitySource.StartActivity("GetGSTransCodeInfo");
            var loEx = new R_Exception();
            PMT04100SingleResult<PMT04100GSTransInfoDTO> loRtn = new PMT04100SingleResult<PMT04100GSTransInfoDTO>();
            _Logger.LogInfo("Start GetGSTransCodeInfo");

            try
            {
                var loCls = new PMT04100InitialProcessCls();

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
        public PMT04100SingleResult<PMT04100GSPeriodYearRangeDTO> GetGSPeriodYearRange()
        {
            using Activity activity = _activitySource.StartActivity("GetGSPeriodYearRange");
            var loEx = new R_Exception();
            PMT04100SingleResult<PMT04100GSPeriodYearRangeDTO> loRtn = new PMT04100SingleResult<PMT04100GSPeriodYearRangeDTO>();
            _Logger.LogInfo("Start GetGSPeriodYearRange");

            try
            {
                var loCls = new PMT04100InitialProcessCls();

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
        public PMT04100SingleResult<PMT04100TodayDateDTO> GetTodayDate()
        {
            using Activity activity = _activitySource.StartActivity("GetTodayDate");
            var loEx = new R_Exception();
            PMT04100SingleResult<PMT04100TodayDateDTO> loRtn = new PMT04100SingleResult<PMT04100TodayDateDTO>();
            _Logger.LogInfo("Start GetTodayDate");

            try
            {
                var loCls = new PMT04100InitialProcessCls();

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
        public PMT04100SingleResult<PMT04100GSPeriodDTInfoDTO> GetGSPeriodDTInfo(PMT04100ParamGSPeriodDTInfoDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("GetGSPeriodDTInfo");
            var loEx = new R_Exception();
            PMT04100SingleResult<PMT04100GSPeriodDTInfoDTO> loRtn = new PMT04100SingleResult<PMT04100GSPeriodDTInfoDTO>();
            _Logger.LogInfo("Start GetGSPeriodDTInfo");

            try
            {
                var loCls = new PMT04100InitialProcessCls();

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
        public PMT04100SingleResult<PMT04100LastCurrencyRateDTO> GetLastCurrencyRate(PMT04100LastCurrencyRateDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("GetLastCurrencyRate");
            var loEx = new R_Exception();
            PMT04100SingleResult<PMT04100LastCurrencyRateDTO> loRtn = new PMT04100SingleResult<PMT04100LastCurrencyRateDTO>();
            _Logger.LogInfo("Start GetLastCurrencyRate");

            try
            {
                var loCls = new PMT04100InitialProcessCls();

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
        public IAsyncEnumerable<PMT04100GSGSBCodeDTO> GetGSBCodeList()
        {
            using Activity activity = _activitySource.StartActivity("GetGSBCodeList");
            var loEx = new R_Exception();
            IAsyncEnumerable<PMT04100GSGSBCodeDTO> loRtn = null;
            _Logger.LogInfo("Start GetGSBCodeList");

            try
            {
                var loCls = new PMT04100InitialProcessCls();

                _Logger.LogInfo("Call Back Method GetGSBCodeList");
                var loTempRtn = loCls.GetGSBCodeList();

                _Logger.LogInfo("Call Stream Method Data GetGSBCodeList");
                loRtn = StreamListData<PMT04100GSGSBCodeDTO>(loTempRtn);
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
        public IAsyncEnumerable<PMT04100PropertyDTO> GetPropertyList()
        {
            using Activity activity = _activitySource.StartActivity("GetCenterList");
            var loEx = new R_Exception();
            IAsyncEnumerable<PMT04100PropertyDTO> loRtn = null;
            _Logger.LogInfo("Start GetCenterList");

            try
            {
                var loCls = new PMT04100InitialProcessCls();

                _Logger.LogInfo("Call Back Method GetGSBCodeList");
                var loTempRtn = loCls.GetAllProperty();

                _Logger.LogInfo("Call Stream Method Data GetCenterList");
                loRtn = StreamListData<PMT04100PropertyDTO>(loTempRtn);
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

        [HttpPost]
        public PMT04100SingleResult<PMT04100PMSystemParamDTO> GetPMSystemParam(PMTInitialParamDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("GetPMSystemParam");
            var loEx = new R_Exception();
            PMT04100SingleResult<PMT04100PMSystemParamDTO> loRtn = new PMT04100SingleResult<PMT04100PMSystemParamDTO>();
            _Logger.LogInfo("Start GetPMSystemParam");

            try
            {
                var loCls = new PMT04100InitialProcessCls();

                _Logger.LogInfo("Call Back Method GetPMSystemParamRecord");
                loRtn.Data = loCls.GetPMSystemParamRecord(poEntity.CPROPERTY_ID);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetPMSystemParam");

            return loRtn;
        }
        #endregion

        [HttpPost]
        public PMT04100SingleResult<PMT04100JournalListInitialProcessDTO> GetTabJournalListInitialProcess()
        {
            using Activity activity = _activitySource.StartActivity("GetTabJournalListInitialProcess");
            var loEx = new R_Exception();
            PMT04100SingleResult<PMT04100JournalListInitialProcessDTO> loRtn = new PMT04100SingleResult<PMT04100JournalListInitialProcessDTO>();
            _Logger.LogInfo("Start GetTabJournalListInitialProcess");

            try
            {
                var loCls = new PMT04100InitialProcessCls();
                _Logger.LogInfo("Call All Back Method GetTabJournalListInitialProcess");
                var loTempResult = new PMT04100JournalListInitialProcessDTO
                {
                    VAR_GL_SYSTEM_PARAM = loCls.GetGLSystemParamRecord(),
                    VAR_GSB_CODE_LIST = loCls.GetGSBCodeList(),
                    VAR_GS_PROPERTY_LIST = loCls.GetAllProperty()
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
        public PMT04100SingleResult<PMT04110JournalEntryInitialProcessDTO> GetTabJournalEntryInitialProcess(PMTInitialParamDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("GetTabJournalListInitialProcess");
            var loEx = new R_Exception();
            PMT04100SingleResult<PMT04110JournalEntryInitialProcessDTO> loRtn = new PMT04100SingleResult<PMT04110JournalEntryInitialProcessDTO>();
            _Logger.LogInfo("Start GetTabJournalListInitialProcess");

            try
            {
                var loCls = new PMT04100InitialProcessCls();
                _Logger.LogInfo("Call All Back Method GetTabJournalListInitialProcess");
                var loPMSystemParam = loCls.GetPMSystemParamRecord(poEntity.CPROPERTY_ID);
                var loTempResult = new PMT04110JournalEntryInitialProcessDTO
                {
                    VAR_CB_SYSTEM_PARAM = loCls.GetSystemParamCB(),
                    VAR_GSM_TRANSACTION_CODE = loCls.GetTransCodeInfoRecord(),
                    VAR_PM_SYSTEM_PARAM = loPMSystemParam,
                    VAR_PROPERTY_LIST = loCls.GetAllProperty(),
                    VAR_GSM_COMPANY = loCls.GetCompanyInfoRecord(),
                    VAR_TODAY = loCls.GetTodayDateRecord(),
                    VAR_SOFT_PERIOD_START_DATE = loCls.GetPeriodDTInfoRecord(new PMT04100ParamGSPeriodDTInfoDTO { CCYEAR = loPMSystemParam.CSOFT_PERIOD_YY, CPERIOD_NO = loPMSystemParam.CSOFT_PERIOD_MM }),
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