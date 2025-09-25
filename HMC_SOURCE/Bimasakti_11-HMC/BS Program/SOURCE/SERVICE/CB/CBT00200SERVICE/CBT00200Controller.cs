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
    public class CBT00200Controller : ControllerBase, ICBT00200
    {
        private LoggerCBT00200 _Logger;
        private readonly ActivitySource _activitySource;
        public CBT00200Controller(ILogger<LoggerCBT00200> logger)
        {
            //Initial and Get Logger
            LoggerCBT00200.R_InitializeLogger(logger);
            _Logger = LoggerCBT00200.R_GetInstanceLogger();
            _activitySource = CBT00200ActivityInitSourceBase.R_InitializeAndGetActivitySource(nameof(CBT00200Controller));
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
        public IAsyncEnumerable<CBT00200DTO> GetJournalList()
        {
            using Activity activity = _activitySource.StartActivity("GetJournalList");
            var loEx = new R_Exception();
            IAsyncEnumerable<CBT00200DTO> loRtn = null;
            _Logger.LogInfo("Start GetJournalList");

            try
            {
                _Logger.LogInfo("Set Param GetJournalList");
                var loParam = new CBT00200ParamDTO();
                loParam.CDEPT_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstant.CDEPT_CODE);
                loParam.CPERIOD = R_Utility.R_GetStreamingContext<string>(ContextConstant.CPERIOD);
                loParam.CSTATUS = R_Utility.R_GetStreamingContext<string>(ContextConstant.CSTATUS);
                loParam.CSEARCH_TEXT = R_Utility.R_GetStreamingContext<string>(ContextConstant.CSEARCH_TEXT);

                _Logger.LogInfo("Call Back Method GetGSBCodeList");
                var loCls = new CBT00200Cls();
                var loTempRtn = loCls.GetJournalList(loParam);

                _Logger.LogInfo("Call Stream Method Data GetJournalList");
                loRtn = StreamListData<CBT00200DTO>(loTempRtn);
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
        public CBT00200SingleResult<CBT00200DTO> GetJournalRecord(CBT00200DTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("GetJournalRecord");
            var loEx = new R_Exception();
            CBT00200SingleResult<CBT00200DTO> loRtn = new CBT00200SingleResult<CBT00200DTO>();
            _Logger.LogInfo("Start GetJournalRecord");

            try
            {
                var loCls = new CBT00200Cls();

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
        public CBT00200SingleResult<CBT00200DTO> SaveJournalRecord(CBT00200SaveParamDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("SaveJournalRecord");
            var loEx = new R_Exception();
            CBT00200SingleResult<CBT00200DTO> loRtn = new CBT00200SingleResult<CBT00200DTO>();
            _Logger.LogInfo("Start SaveJournalRecord");

            try
            {
                var loCls = new CBT00200Cls();

                _Logger.LogInfo("Call Back Method SaveJournal");
                loRtn.Data = loCls.SaveJournal(poEntity.Data, poEntity.CRUDMode, poEntity.PARAM_CALLER);
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
        public CBT00200SingleResult<CBT00200UpdateStatusDTO> UpdateJournalStatus(CBT00200UpdateStatusDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("UpdateJournalStatus");
            var loEx = new R_Exception();
            CBT00200SingleResult<CBT00200UpdateStatusDTO> loRtn = new CBT00200SingleResult<CBT00200UpdateStatusDTO>();
            _Logger.LogInfo("Start UpdateJournalStatus");

            try
            {
                var loCls = new CBT00200Cls();

                _Logger.LogInfo("Call Back Method GetJournalDisplay");
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


    }
}