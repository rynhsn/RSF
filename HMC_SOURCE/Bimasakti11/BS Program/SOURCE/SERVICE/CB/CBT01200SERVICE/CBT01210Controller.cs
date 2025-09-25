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
    public class CBT01210Controller : ControllerBase, ICBT01210
    {
        private LoggerCBT01200 _logger;

        private readonly ActivitySource _activitySource;

        public CBT01210Controller(ILogger<LoggerCBT01200> logger)
        {
            //Initial and Get Logger
            LoggerCBT01200.R_InitializeLogger(logger);
            _logger = LoggerCBT01200.R_GetInstanceLogger();
            _activitySource = CBT01200Activity.R_InitializeAndGetActivitySource(GetType().Name);

        }
        
        [HttpPost]
        public R_ServiceGetRecordResultDTO<CBT01210ParamDTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<CBT01210ParamDTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceGetRecord");

            _logger.LogInfo("Start - GetRecord");

            R_Exception loEx = new R_Exception();
            R_ServiceGetRecordResultDTO<CBT01210ParamDTO> loRtn = null;
            CBT01210Cls loCls;

            try
            {
                loCls = new CBT01210Cls();
                loRtn = new R_ServiceGetRecordResultDTO<CBT01210ParamDTO>();

                _logger.LogInfo("Set Parameter");
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
        public R_ServiceSaveResultDTO<CBT01210ParamDTO> R_ServiceSave(R_ServiceSaveParameterDTO<CBT01210ParamDTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceSave");
            _logger.LogInfo("Start - Save Transaction Record");
            R_Exception loEx = new R_Exception();
            R_ServiceSaveResultDTO<CBT01210ParamDTO> loRtn = null;
            CBT01210Cls loCls;

            try
            {
                loCls = new CBT01210Cls();
                loRtn = new R_ServiceSaveResultDTO<CBT01210ParamDTO>();
                _logger.LogInfo("Set Parameter");
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;

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
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<CBT01210ParamDTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            R_ServiceDeleteResultDTO loRtn = new R_ServiceDeleteResultDTO();
            var loEx = new R_Exception();
            try
            {
                _logger.LogInfo("Set Parameter");
                var loCls = new CBT01210Cls();
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

        [HttpPost]
        public IAsyncEnumerable<CBT01201DTO> GetJournalDetailList()
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            var loEx = new R_Exception();
            IAsyncEnumerable<CBT01201DTO> loRtn = null;

            try
            {
                var loPar = new CBT01210ParamDTO()
                {
                    CREC_ID = R_Utility.R_GetContext<string>(ContextConstant.CREC_ID),
                    CLANGUAGE_ID = R_BackGlobalVar.CULTURE
                };
                var loCls = new CBT01210Cls();
                var loTempRtn = loCls.GetJournalDetailList(loPar);
                ShowLogExecute();
                loRtn = StreamListData<CBT01201DTO>(loTempRtn);
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
