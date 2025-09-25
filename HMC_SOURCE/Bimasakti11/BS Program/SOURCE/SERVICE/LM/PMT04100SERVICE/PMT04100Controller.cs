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
    public class PMT04100Controller : ControllerBase, IPMT04100
    {
        private LoggerPMT04100 _Logger;
        private readonly ActivitySource _activitySource;
        public PMT04100Controller(ILogger<LoggerPMT04100> logger)
        {
            //Initial and Get Logger
            LoggerPMT04100.R_InitializeLogger(logger);
            _Logger = LoggerPMT04100.R_GetInstanceLogger();
            _activitySource = PMT04100ActivityInitSourceBase.R_InitializeAndGetActivitySource(nameof(PMT04100Controller));
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
        public IAsyncEnumerable<PMT04100DTO> GetJournalList()
        {
            using Activity activity = _activitySource.StartActivity("GetJournalList");
            var loEx = new R_Exception();
            IAsyncEnumerable<PMT04100DTO> loRtn = null;
            _Logger.LogInfo("Start GetJournalList");

            try
            {
                _Logger.LogInfo("Set Param GetJournalList");
                var loParam = new PMT04100ParamDTO();
                loParam.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CPROPERTY_ID);
                loParam.CDEPT_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstant.CDEPT_CODE);
                loParam.CCUSTOMER_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CCUSTOMER_ID);
                loParam.CPERIOD = R_Utility.R_GetStreamingContext<string>(ContextConstant.CPERIOD);
                loParam.CSTATUS = R_Utility.R_GetStreamingContext<string>(ContextConstant.CSTATUS);
                loParam.CSEARCH_TEXT = R_Utility.R_GetStreamingContext<string>(ContextConstant.CSEARCH_TEXT);

                _Logger.LogInfo("Call Back Method GetGSBCodeList");
                var loCls = new PMT04100Cls();
                var loTempRtn = loCls.GetJournalList(loParam);

                _Logger.LogInfo("Call Stream Method Data GetJournalList");
                loRtn = StreamListData<PMT04100DTO>(loTempRtn);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetJournalList");

            return loRtn;
        }

        [HttpPost]
        public PMT04100SingleResult<PMT04100DTO> GetJournalRecord(PMT04100DTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("GetJournalRecord");
            var loEx = new R_Exception();
            PMT04100SingleResult<PMT04100DTO> loRtn = new PMT04100SingleResult<PMT04100DTO>();
            _Logger.LogInfo("Start GetJournalRecord");

            try
            {
                var loCls = new PMT04100Cls();

                _Logger.LogInfo("Call Back Method GetJournalDisplay");
                loRtn.Data = loCls.GetJournalDisplay(poEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetJournalRecord");

            return loRtn;
        }

        [HttpPost]
        public PMT04100SingleResult<PMT04100DTO> SaveJournalRecord(PMT04100SaveParamDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("SaveJournalRecord");
            var loEx = new R_Exception();
            PMT04100SingleResult<PMT04100DTO> loRtn = new PMT04100SingleResult<PMT04100DTO>();
            _Logger.LogInfo("Start SaveJournalRecord");

            try
            {
                var loCls = new PMT04100Cls();

                _Logger.LogInfo("Call Back Method SaveJournal");
                loRtn.Data = loCls.SaveJournal(poEntity.Data, poEntity.CRUDMode);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End SaveJournalRecord");

            return loRtn;
        }

        [HttpPost]
        public PMT04100SingleResult<PMT04100UpdateStatusDTO> UpdateJournalStatus(PMT04100UpdateStatusDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("UpdateJournalStatus");
            var loEx = new R_Exception();
            PMT04100SingleResult<PMT04100UpdateStatusDTO> loRtn = new PMT04100SingleResult<PMT04100UpdateStatusDTO>();
            _Logger.LogInfo("Start UpdateJournalStatus");

            try
            {
                var loCls = new PMT04100Cls();

                _Logger.LogInfo("Call Back Method UpdateJournalStatus");
                loCls.UpdateJournalStatus(poEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End UpdateJournalStatus");

            return loRtn;
        }

        [HttpPost]
        public PMT04100SingleResult<PMT04100UpdateStatusDTO> SubmitCashReceipt(PMT04100UpdateStatusDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("SubmitCashReceipt");
            var loEx = new R_Exception();
            PMT04100SingleResult<PMT04100UpdateStatusDTO> loRtn = new PMT04100SingleResult<PMT04100UpdateStatusDTO>();
            _Logger.LogInfo("Start SubmitCashReceipt");

            try
            {
                var loCls = new PMT04100Cls();

                _Logger.LogInfo("Call Back Method SubmitCashReceiptSP");
                loCls.SubmitCashReceiptSP(poEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End SubmitCashReceipt");

            return loRtn;
        }
    }
}