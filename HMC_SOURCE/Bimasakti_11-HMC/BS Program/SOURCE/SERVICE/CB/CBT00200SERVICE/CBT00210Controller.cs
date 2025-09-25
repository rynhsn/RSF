using CBT00200BACK;
using CBT00200COMMON;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_Common;
using R_CommonFrontBackAPI;
using System.Diagnostics;

namespace CBT00200SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class CBT00210Controller : ControllerBase, ICBT00210
    {
        private LoggerCBT00210 _Logger;
        private readonly ActivitySource _activitySource;
        public CBT00210Controller(ILogger<LoggerCBT00210> logger)
        {
            //Initial and Get Logger
            LoggerCBT00210.R_InitializeLogger(logger);
            _Logger = LoggerCBT00210.R_GetInstanceLogger();
            _activitySource = CBT00210ActivityInitSourceBase.R_InitializeAndGetActivitySource(nameof(CBT00210Controller));
        }
        #region Stream List Data
        private async IAsyncEnumerable<T> StreamListData<T>(List<T> poParameter)
        {
            foreach (var item in poParameter)
            {
                yield return item;
            }
        }

        [HttpPost]
        public IAsyncEnumerable<CBT00210DTO> GetJournalDetailList()
        {
            using Activity activity = _activitySource.StartActivity("GetJournalDetailList");
            var loEx = new R_Exception();
            IAsyncEnumerable<CBT00210DTO> loRtn = null;
            _Logger.LogInfo("Start GetJournalDetailList");

            try
            {
                _Logger.LogInfo("Set Param GetJournalDetailList");
                var loParam = new CBT00210DTO();
                loParam.CREC_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CREC_ID);

                _Logger.LogInfo("Call Back Method GetJournalDetailList");
                var loCls = new CBT00210Cls();
                var loTempRtn = loCls.GetJournalDetailList(loParam);

                _Logger.LogInfo("Call Stream Method Data GetJournalDetailList");
                loRtn = StreamListData<CBT00210DTO>(loTempRtn);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetJournalDetailList");

            return loRtn;
        }

        [HttpPost]
        public R_ServiceGetRecordResultDTO<CBT00210DTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<CBT00210DTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceGetRecord CBT00210");
            var loEx = new R_Exception();
            R_ServiceGetRecordResultDTO<CBT00210DTO> loRtn = new R_ServiceGetRecordResultDTO<CBT00210DTO>();
            _Logger.LogInfo("Start ServiceGetRecord CBT00210");

            try
            {
                var loCls = new CBT00210Cls();

                _Logger.LogInfo("Call Back Method R_GetRecord CBT00210Cls");
                loRtn.data = loCls.R_GetRecord(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End R_GetRecord CBT00210");

            return loRtn;
        }

        [HttpPost]
        public R_ServiceSaveResultDTO<CBT00210DTO> R_ServiceSave(R_ServiceSaveParameterDTO<CBT00210DTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceSave CBT00210");
            var loEx = new R_Exception();
            R_ServiceSaveResultDTO<CBT00210DTO> loRtn = new R_ServiceSaveResultDTO<CBT00210DTO>();
            _Logger.LogInfo("Start ServiceSave CBT00210");

            try
            {
                var loCls = new CBT00210Cls();

                _Logger.LogInfo("Call Back Method R_Save CBT00210Cls");
                loRtn.data = loCls.R_Save(poParameter.Entity, poParameter.CRUDMode);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End ServiceSave CBT00210");

            return loRtn;
        }

        [HttpPost]
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<CBT00210DTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceDelete CBT00210");
            var loEx = new R_Exception();
            R_ServiceDeleteResultDTO loRtn = new R_ServiceDeleteResultDTO();
            _Logger.LogInfo("Start R_ServiceDelete CBT00210");

            try
            {
                var loCls = new CBT00210Cls();

                _Logger.LogInfo("Call Back Method R_Delete CBT00210Cls");
                loCls.R_Delete(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End R_ServiceDelete CBT00210");

            return loRtn;
        }
        #endregion

    }
}