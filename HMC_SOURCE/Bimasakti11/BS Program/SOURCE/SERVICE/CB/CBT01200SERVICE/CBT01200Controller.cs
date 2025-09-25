using CBT01200BACK;
using CBT01200Common;
using CBT01200Common.Loggers;
using CBT01200Back;
using CBT01200Common.Loggers;
using CBT01200Common.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_Common;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using CBT01200Common;
using CBT01200Back;
using R_BackEnd;
using R_CommonFrontBackAPI;

namespace CBT01200SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class CBT01200Controller : ControllerBase, ICBT01200
    {
        private LoggerCBT01200 _logger;

        private readonly ActivitySource _activitySource;

        public CBT01200Controller(ILogger<LoggerCBT01200> logger)
        {
            //Initial and Get Logger
            LoggerCBT01200.R_InitializeLogger(logger);
            _logger = LoggerCBT01200.R_GetInstanceLogger();
            _activitySource = CBT01200Activity.R_InitializeAndGetActivitySource(GetType().Name);

        }
        

        [HttpPost]
        public IAsyncEnumerable<CBT01200DTO> GetJournalList()
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            var loEx = new R_Exception();
            IAsyncEnumerable<CBT01200DTO> loRtn = null;

            try
            {
                var loParam = new CBT01200JournalHDParam();
                loParam.CDEPT_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstant.CDEPT_CODE);
                loParam.CPERIOD = R_Utility.R_GetStreamingContext<string>(ContextConstant.CPERIOD);
                loParam.CSTATUS = R_Utility.R_GetStreamingContext<string>(ContextConstant.CSTATUS);
                loParam.CSEARCH_TEXT = R_Utility.R_GetStreamingContext<string>(ContextConstant.CSEARCH_TEXT);

                var loCls = new CBT01200Cls();

                ShowLogExecute();
                var loTempRtn = loCls.GetJournalList(loParam);

                loRtn = StreamListData<CBT01200DTO>(loTempRtn);
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
        public CBT01200RecordResult<CBT01200UpdateStatusDTO> UpdateJournalStatus(CBT01200UpdateStatusDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            var loEx = new R_Exception();
            CBT01200RecordResult<CBT01200UpdateStatusDTO> loRtn = new CBT01200RecordResult<CBT01200UpdateStatusDTO>();
            try
            {
                var loCls = new CBT01200Cls();

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
        public CBT01200RecordResult<CBT01210LastCurrencyRateDTO> GetLastCurrency(CBT01210LastCurrencyRateDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            var loEx = new R_Exception();
            CBT01200RecordResult<CBT01210LastCurrencyRateDTO> loRtn = new CBT01200RecordResult<CBT01210LastCurrencyRateDTO>();

            try
            {
                var loCls = new CBT01200Cls();
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
        public R_ServiceGetRecordResultDTO<CBT01200JournalHDParam> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<CBT01200JournalHDParam> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceGetRecord");

            _logger.LogInfo("Start - GetRecord");

            R_Exception loEx = new R_Exception();
            R_ServiceGetRecordResultDTO<CBT01200JournalHDParam> loRtn = null;
            CBT01200Cls loCls;

            try
            {
                loCls = new CBT01200Cls();
                loRtn = new R_ServiceGetRecordResultDTO<CBT01200JournalHDParam>();

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
        public R_ServiceSaveResultDTO<CBT01200JournalHDParam> R_ServiceSave(R_ServiceSaveParameterDTO<CBT01200JournalHDParam> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceSave");
            _logger.LogInfo("Start - Save Transaction Record");
            R_Exception loEx = new R_Exception();
            R_ServiceSaveResultDTO<CBT01200JournalHDParam> loRtn = null;
            CBT01200Cls loCls;

            try
            {
                loCls = new CBT01200Cls();
                loRtn = new R_ServiceSaveResultDTO<CBT01200JournalHDParam>();
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
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<CBT01200JournalHDParam> poParameter)
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
                    var loCls = new CBT01200Cls();
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
