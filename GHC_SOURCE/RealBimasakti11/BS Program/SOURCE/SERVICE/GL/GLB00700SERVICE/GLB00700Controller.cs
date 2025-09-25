using GLB00700BACK;
using GLB00700COMMON;
using GLB00700COMMON.DTO_s;
using GLB00700COMMON.DTO_s.Helper;
using GLB00700COMMON.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace GLB00700SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class GLB00700Controller : ControllerBase, IGLB00700
    {
        //variable & constructors
        private GLB00700Logger _logger;
        private readonly ActivitySource _activitySource;
        public GLB00700Controller(ILogger<GLB00700Logger> logger)
        {
            GLB00700Logger.R_InitializeLogger(logger);
            _logger = GLB00700Logger.R_GetInstanceLogger();
            _activitySource = GLB00700Activity.R_InitializeAndGetActivitySource(nameof(GLB00700Controller));
        }

        //methods
        [HttpPost]
        public GeneralAPIResultDTO<GLSysParamDTO> GetGLSysParamRecord()
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            var loEx = new R_Exception();
            GeneralAPIResultDTO<GLSysParamDTO> loRtn = new();
            ShowLogStart();
            try
            {
                var loCls = new GLB00700Cls();
                ShowLogExecute();
                loRtn.data = loCls.GetGLSysParamRecord(new GeneralParamDTO
                {
                    CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID,
                    CLANG_ID = R_BackGlobalVar.CULTURE,
                });
            }
            catch (Exception ex)
            {
                ShowLogError(loEx);
                loEx.Add(ex);
            }
            ShowLogEnd();
            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }
        [HttpPost]
        public GeneralAPIResultDTO<TodayDTO> GetTodayRecord()
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            var loEx = new R_Exception();
            GeneralAPIResultDTO<TodayDTO> loRtn = new();
            ShowLogStart();
            try
            {
                var loCls = new GLB00700Cls();
                ShowLogExecute();
                loRtn.data = loCls.GetTodayRecord(new GeneralParamDTO
                {
                    CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID,
                });
            }
            catch (Exception ex)
            {
                ShowLogError(loEx);
                loEx.Add(ex);
            }
            ShowLogEnd();
            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }
        [HttpPost]
        public GeneralAPIResultDTO<LastRateRevaluationDTO> GetLastRateRevaluationRecord(LastRateRevaluationParamDTO poParam)
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            var loEx = new R_Exception();
            GeneralAPIResultDTO<LastRateRevaluationDTO> loRtn = new();
            ShowLogStart();
            try
            {
                var loCls = new GLB00700Cls();
                ShowLogExecute();
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CUSER_ID = R_BackGlobalVar.USER_ID;
                loRtn.data = loCls.GetLastRateRevaluationRecord(poParam);
            }
            catch (Exception ex)
            {
                ShowLogError(loEx);
                loEx.Add(ex);
            }
            ShowLogEnd();
            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }

        //helpers
        private void ShowLogStart([CallerMemberName] string pcMethodCallerName = "") => _logger.LogInfo($"Starting {pcMethodCallerName} in {GetType().Name}");
        private void ShowLogExecute([CallerMemberName] string pcMethodCallerName = "") => _logger.LogInfo($"Executing cls method in {GetType().Name}.{pcMethodCallerName}");
        private void ShowLogEnd([CallerMemberName] string pcMethodCallerName = "") => _logger.LogInfo($"End {pcMethodCallerName} in {GetType().Name}");
        private void ShowLogError(Exception exception, [CallerMemberName] string pcMethodCallerName = "") => _logger.LogError(exception);

       
    }
}
