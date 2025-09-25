using GLF00100BACK;
using GLF00100COMMON;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;
using System.Diagnostics;

namespace GLF00100SERVICES
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class GLF00100Controller : ControllerBase, IGLF00100
    {
        private LoggerGLF00100 _Logger;
        private readonly ActivitySource _activitySource;
        public GLF00100Controller(ILogger<LoggerGLF00100> logger)
        {
            //Initial and Get Logger
            LoggerGLF00100.R_InitializeLogger(logger);
            _Logger = LoggerGLF00100.R_GetInstanceLogger();
            _activitySource = GLF00100ActivitySourceBase.R_InitializeAndGetActivitySource(nameof(GLF00100Controller));
        }

        [HttpPost]
        public GLF00100SingleResult<GLF00100InitialDTO> GetInfoCompany()
        {
            using Activity activity = _activitySource.StartActivity("GetInfoCompany");
            var loEx = new R_Exception();
            GLF00100SingleResult<GLF00100InitialDTO> loRtn = new GLF00100SingleResult<GLF00100InitialDTO>();
            _Logger.LogInfo("Start GetInfoCompany");

            try
            {
                var loCls = new GLF00100Cls();

                _Logger.LogInfo("Call Back Method GetInfoCompany");
                loRtn.Data = loCls.GetInfoCompany(R_BackGlobalVar.COMPANY_ID);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetInfoCompany");

            return loRtn;
        }

        [HttpPost]
        public GLF00100SingleResult<GLF00100DTO> GetJournalDetail(GLF00100ParameterDTO poParam)
        {
            using Activity activity = _activitySource.StartActivity("GetJournalDetail");
            var loEx = new R_Exception();
            GLF00100SingleResult<GLF00100DTO> loRtn = new GLF00100SingleResult<GLF00100DTO>();
            _Logger.LogInfo("Start GetJournalDetail");

            try
            {
                _Logger.LogInfo("Set Global Param GetJournalDetail");
                poParam.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CUSER_ID = R_BackGlobalVar.USER_ID;

                var loCls = new GLF00100Cls();
                _Logger.LogInfo("Call Back Method GetJournalDetail");
                loRtn.Data = loCls.GetJournalDetail(poParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetJournalDetail");

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<GLF00101DTO> GetJournalDetailList()
        {
            using Activity activity = _activitySource.StartActivity("GetJournalDetailList");
            var loEx = new R_Exception();
            IAsyncEnumerable<GLF00101DTO> loRtn = null;
            List<GLF00101DTO> loTempRtn = null;
            _Logger.LogInfo("Start GetJournalDetailList");

            try
            {
                var loCls = new GLF00100Cls();
                var poParam = new GLF00101DTO();

                _Logger.LogInfo("Set Param GetJournalDetailList");
                poParam.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;
                poParam.CJRN_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CJRN_ID);

                _Logger.LogInfo("Call Back Method GetAllJournalDetailList");
                loTempRtn = loCls.GetAllJournalDetailList(poParam);

                _Logger.LogInfo("Call Stream Method Data GetJournalDetailList");
                loRtn = GetListStreamData<GLF00101DTO>(loTempRtn);
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

        private async IAsyncEnumerable<T> GetListStreamData<T>(List<T> poParameter)
        {
            foreach (var item in poParameter)
            {
                yield return item;
            }
        }
    }
}