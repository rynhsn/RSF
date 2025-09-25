using GLT00100BACK;
using GLT00100COMMON;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_Common;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace GLT00100SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class GLT00100Controller : ControllerBase, IGLT00100
    {
        private LoggerGLT00100 _logger;

        private readonly ActivitySource _activitySource;

        public GLT00100Controller(ILogger<LoggerGLT00100> logger)
        {
            //Initial and Get Logger
            LoggerGLT00100.R_InitializeLogger(logger);
            _logger = LoggerGLT00100.R_GetInstanceLogger();
            _activitySource = GLT00100Activity.R_InitializeAndGetActivitySource(GetType().Name);

        }

        [HttpPost]
        public IAsyncEnumerable<GLT00101DTO> GetJournalDetailList()
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            var loEx = new R_Exception();
            IAsyncEnumerable<GLT00101DTO> loRtn = null;

            try
            {
                var loRecId = R_Utility.R_GetStreamingContext<string>(ContextConstant.CREC_ID);
                var loCls = new GLT00100Cls();
                var loTempRtn = loCls.GetJournalDetailList(loRecId);
                ShowLogExecute();
                loRtn = StreamListData<GLT00101DTO>(loTempRtn);
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
        public IAsyncEnumerable<GLT00100DTO> GetJournalList()
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            var loEx = new R_Exception();
            IAsyncEnumerable<GLT00100DTO> loRtn = null;

            try
            {
                var loParam = new GLT00100ParamDTO();
                loParam.CDEPT_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstant.CDEPT_CODE);
                loParam.CPERIOD = R_Utility.R_GetStreamingContext<string>(ContextConstant.CPERIOD);
                loParam.CSTATUS = R_Utility.R_GetStreamingContext<string>(ContextConstant.CSTATUS);
                loParam.CSEARCH_TEXT = R_Utility.R_GetStreamingContext<string>(ContextConstant.CSEARCH_TEXT);

                var loCls = new GLT00100Cls();

                ShowLogExecute();
                var loTempRtn = loCls.GetJournalList(loParam);

                loRtn = StreamListData<GLT00100DTO>(loTempRtn);
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
        public GLT00100RecordResult<GLT00100UpdateStatusDTO> UpdateJournalStatus(GLT00100UpdateStatusDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            var loEx = new R_Exception();
            GLT00100RecordResult<GLT00100UpdateStatusDTO> loRtn = new GLT00100RecordResult<GLT00100UpdateStatusDTO>();

            try
            {
                var loCls = new GLT00100Cls();

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
        public GLT00100RecordResult<GLT00100RapidApprovalValidationDTO> ValidationRapidApproval(GLT00100RapidApprovalValidationDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            var loEx = new R_Exception();
            GLT00100RecordResult<GLT00100RapidApprovalValidationDTO> loRtn = new GLT00100RecordResult<GLT00100RapidApprovalValidationDTO>();

            try
            {
                var loCls = new GLT00100Cls();

                ShowLogExecute();
                loRtn.Data = loCls.ValidationRapidAppro(poEntity);
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