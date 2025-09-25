using PMT04200Back;
using PMT04200Common;
using PMT04200Common.Loggers;
using PMT04200Back;
using PMT04200Common.Loggers;
using PMT04200Common.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_Common;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using PMT04200Common;
using PMT04200Back;
using R_BackEnd;
using R_CommonFrontBackAPI;

namespace PMT04200SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class PMT04200Controller : ControllerBase, IPMT04200
    {
        private LoggerPMT04200 _logger;

        private readonly ActivitySource _activitySource;

        public PMT04200Controller(ILogger<LoggerPMT04200> logger)
        {
            //Initial and Get Logger
            LoggerPMT04200.R_InitializeLogger(logger);
            _logger = LoggerPMT04200.R_GetInstanceLogger();
            _activitySource = PMT04200ActivityInitSourceBase.R_InitializeAndGetActivitySource(GetType().Name);

        }
        
        [HttpPost]
        public IAsyncEnumerable<PMT04200DTO> GetJournalList()
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            var loEx = new R_Exception();
            IAsyncEnumerable<PMT04200DTO> loRtn = null;

            try
            {
                var loParam = new PMT04200ParamDTO();
                loParam.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CPROPERTY_ID);
                loParam.CCUSTOMER_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CCUSTOMER_ID);
                loParam.CDEPT_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstant.CDEPT_CODE);
                loParam.CPERIOD = R_Utility.R_GetStreamingContext<string>(ContextConstant.CPERIOD);
                loParam.CSTATUS = R_Utility.R_GetStreamingContext<string>(ContextConstant.CSTATUS);
                loParam.CSEARCH_TEXT = R_Utility.R_GetStreamingContext<string>(ContextConstant.CSEARCH_TEXT);

                loParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParam.CUSER_ID = R_BackGlobalVar.USER_ID;
                loParam.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;

                var loCls = new PMT04200Cls();

                ShowLogExecute();
                var loTempRtn = loCls.GetJournalList(loParam);

                loRtn = StreamListData<PMT04200DTO>(loTempRtn);
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
        public IAsyncEnumerable<PMT04200AllocationGridDTO> GetAllocationList()
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            var loEx = new R_Exception();
            IAsyncEnumerable<PMT04200AllocationGridDTO> loRtn = null;

            try
            {
                var loParam = new PMT04200ParamDTO();
                loParam.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CPROPERTY_ID);
                loParam.CDEPT_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstant.CDEPT_CODE);
                loParam.CREF_NO = R_Utility.R_GetStreamingContext<string>(ContextConstant.CREF_NO);
                loParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParam.CUSER_ID = R_BackGlobalVar.USER_ID;
                loParam.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;

                var loCls = new PMT04200Cls();

                ShowLogExecute();
                var loTempRtn = loCls.GetJournalAllocationGridList(loParam);

                loRtn = StreamListData<PMT04200AllocationGridDTO>(loTempRtn);
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
        public PMT04200RecordResult<PMT04200DTO> GetJournalRecord(PMT04200DTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("GetJournalRecord");
            var loEx = new R_Exception();
            PMT04200RecordResult<PMT04200DTO> loRtn = new PMT04200RecordResult<PMT04200DTO>();
            _logger.LogInfo("Start GetJournalRecord");

            try
            {
                var loCls = new PMT04200Cls();
                poEntity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poEntity.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;
                _logger.LogInfo("Call Back Method GetJournalDisplay");
                loRtn.Data = loCls.GetTransactionDisplay(poEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _logger.LogInfo("End GetJournalRecord");

            return loRtn;
        }
        
        [HttpPost]
        public PMT04200RecordResult<PMT04200DTO> SaveJournalRecord(PMT04200SaveParamDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("SaveJournalRecord");
            var loEx = new R_Exception();
            PMT04200RecordResult<PMT04200DTO> loRtn = new PMT04200RecordResult<PMT04200DTO>();
            _logger.LogInfo("Start SaveJournalRecord");

            try
            {
                var loCls = new PMT04200Cls();
                poEntity.Data.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poEntity.Data.CUSER_ID = R_BackGlobalVar.USER_ID;
                poEntity.Data.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;

                _logger.LogInfo("Call Back Method SaveJournal");
                loRtn.Data = loCls.SaveJournal(poEntity.Data, poEntity.CRUDMode);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _logger.LogInfo("End SaveJournalRecord");

            return loRtn;
        }

        [HttpPost]
        public PMT04200RecordResult<PMT04200UpdateStatusDTO> UpdateJournalStatus(PMT04200UpdateStatusDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("UpdateJournalStatus");
            var loEx = new R_Exception();
            PMT04200RecordResult<PMT04200UpdateStatusDTO> loRtn = new PMT04200RecordResult<PMT04200UpdateStatusDTO>();
            _logger.LogInfo("Start UpdateJournalStatus");

            try
            {
                var loCls = new PMT04200Cls();

                _logger.LogInfo("Call Back Method UpdateJournalStatus");
                loCls.UpdateJournalStatus(poEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _logger.LogInfo("End UpdateJournalStatus");

            return loRtn;
        }

        [HttpPost]
        public PMT04200RecordResult<PMT04200UpdateStatusDTO> SubmitCashReceipt(PMT04200UpdateStatusDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("SubmitCashReceipt");
            var loEx = new R_Exception();
            PMT04200RecordResult<PMT04200UpdateStatusDTO> loRtn = new PMT04200RecordResult<PMT04200UpdateStatusDTO>();
            _logger.LogInfo("Start SubmitCashReceipt");

            try
            {
                var loCls = new PMT04200Cls();

                _logger.LogInfo("Call Back Method SubmitCashReceiptSP");
                loCls.SubmitCashReceiptSP(poEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _logger.LogInfo("End SubmitCashReceipt");

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
