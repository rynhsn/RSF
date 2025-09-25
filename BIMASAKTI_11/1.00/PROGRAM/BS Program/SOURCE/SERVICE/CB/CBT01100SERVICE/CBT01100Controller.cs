using CBT01100BACK;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_Common;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using R_BackEnd;
using R_CommonFrontBackAPI;
using CBT01100COMMON;
using CBT01100COMMON.Loggers;
using CBT01100COMMON.DTO_s.CBT01100;

namespace CBT01100SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class CBT01100Controller : ControllerBase, ICBT01100
    {
        private LoggerCBT01100 _logger;

        private readonly ActivitySource _activitySource;

        public CBT01100Controller(ILogger<LoggerCBT01100> logger)
        {
            //Initial and Get Logger
            LoggerCBT01100.R_InitializeLogger(logger);
            _logger = LoggerCBT01100.R_GetInstanceLogger();
            _activitySource = CBT01100Activity.R_InitializeAndGetActivitySource(GetType().Name);

        }


        [HttpPost]
        public IAsyncEnumerable<CBT01100DTO> GetJournalList()
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            var loEx = new R_Exception();
            IAsyncEnumerable<CBT01100DTO> loRtn = null;

            try
            {
                var loParam = new CBT01100JournalHDParam();
                loParam.CDEPT_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstantCBT01100.CDEPT_CODE);
                loParam.CPERIOD = R_Utility.R_GetStreamingContext<string>(ContextConstantCBT01100.CPERIOD);
                loParam.CSTATUS = R_Utility.R_GetStreamingContext<string>(ContextConstantCBT01100.CSTATUS);
                loParam.CSEARCH_TEXT = R_Utility.R_GetStreamingContext<string>(ContextConstantCBT01100.CSEARCH_TEXT);

                var loCls = new CBT01100Cls();

                ShowLogExecute();
                var loTempRtn = loCls.GetJournalList(loParam);

                loRtn = StreamListData<CBT01100DTO>(loTempRtn);
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
        public CBT01100RecordResult<CBT01100UpdateStatusDTO> UpdateJournalStatus(CBT01100UpdateStatusDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            var loEx = new R_Exception();
            CBT01100RecordResult<CBT01100UpdateStatusDTO> loRtn = new CBT01100RecordResult<CBT01100UpdateStatusDTO>();
            try
            {
                var loCls = new CBT01100Cls();

                ShowLogExecute();
                loCls.UpdateJournalStatus(poEntity);
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
        public CBT01100RecordResult<CBT01110LastCurrencyRateDTO> GetLastCurrency(CBT01110LastCurrencyRateDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            var loEx = new R_Exception();
            CBT01100RecordResult<CBT01110LastCurrencyRateDTO> loRtn = new CBT01100RecordResult<CBT01110LastCurrencyRateDTO>();

            try
            {
                var loCls = new CBT01100Cls();
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
        public R_ServiceGetRecordResultDTO<CBT01100JournalHDParam> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<CBT01100JournalHDParam> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceGetRecord");

            _logger.LogInfo("Start - GetRecord");

            R_Exception loEx = new R_Exception();
            R_ServiceGetRecordResultDTO<CBT01100JournalHDParam> loRtn = null;
            CBT01100Cls loCls;

            try
            {
                loCls = new CBT01100Cls();
                loRtn = new R_ServiceGetRecordResultDTO<CBT01100JournalHDParam>();

                _logger.LogInfo("Set Parameter");
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
                poParameter.Entity.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;

                _logger.LogInfo("Get Account Record");
                loRtn.data = loCls.R_GetRecord(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

        EndBlock:
            loEx.ThrowExceptionIfErrors();
            _logger.LogInfo("End - Get Account Record");
            return loRtn;
        }

        [HttpPost]
        public R_ServiceSaveResultDTO<CBT01100JournalHDParam> R_ServiceSave(R_ServiceSaveParameterDTO<CBT01100JournalHDParam> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceSave");
            _logger.LogInfo("Start - Save Transaction Record");
            R_Exception loEx = new R_Exception();
            R_ServiceSaveResultDTO<CBT01100JournalHDParam> loRtn = null;
            CBT01100Cls loCls;

            try
            {
                loCls = new CBT01100Cls();
                loRtn = new R_ServiceSaveResultDTO<CBT01100JournalHDParam>();
                _logger.LogInfo("Set Parameter");
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
                poParameter.Entity.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;

                _logger.LogInfo("Save Transaction Entity");
                loRtn.data = loCls.R_Save(poParameter.Entity, poParameter.CRUDMode);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

        EndBlock:
            loEx.ThrowExceptionIfErrors();
            _logger.LogInfo("End - Save Account Entity");
            return loRtn;
        }

        [HttpPost]
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<CBT01100JournalHDParam> poParameter)
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            R_ServiceDeleteResultDTO loRtn = new R_ServiceDeleteResultDTO();
            var loEx = new R_Exception();
            try
            {
                _logger.LogInfo("Set Parameter");
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
                var loCls = new CBT01100Cls();
                _logger.LogInfo("Delete Transaction Entity");
                loCls.R_Delete(poParameter.Entity);

                ShowLogExecute();

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                ShowLogError(loEx);

            }

            loEx.ThrowExceptionIfErrors();
            _logger.LogInfo("End - Delete Account Entity");
            ShowLogEnd();
            return loRtn;
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

        #region logger

        private void ShowLogStart([CallerMemberName] string pcMethodCallerName = "") => _logger.LogInfo($"Starting {pcMethodCallerName} in {GetType().Name}");

        private void ShowLogExecute([CallerMemberName] string pcMethodCallerName = "") => _logger.LogInfo($"Executing cls method in {GetType().Name}.{pcMethodCallerName}");

        private void ShowLogEnd([CallerMemberName] string pcMethodCallerName = "") => _logger.LogInfo($"End {pcMethodCallerName} in {GetType().Name}");

        private void ShowLogError(Exception exception, [CallerMemberName] string pcMethodCallerName = "") => _logger.LogError(exception);

        #endregion
    }
}
