using GLTR00100BACK;
using GLTR00100COMMON;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;

namespace GLTR00100SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class GLTR00100Controller : ControllerBase, IGLTR00100
    {
        private LoggerGLTR00100 _Logger;
        public GLTR00100Controller(ILogger<LoggerGLTR00100> logger)
        {
            //Initial and Get Logger
            LoggerGLTR00100.R_InitializeLogger(logger);
            _Logger = LoggerGLTR00100.R_GetInstanceLogger();
        }

        [HttpPost]
        public GLTR00100Record<GLTR00100DTO> GetGLJournal(GLTR00100DTO poParam)
        {
            var loEx = new R_Exception();
            GLTR00100Record<GLTR00100DTO> loRtn = null;
            _Logger.LogInfo("Start GetGLJournal");

            try
            {
                loRtn = new GLTR00100Record<GLTR00100DTO>();
                _Logger.LogInfo("Set Param GetGLJournal");
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CUSER_ID = R_BackGlobalVar.USER_ID;
                poParam.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;

                var loCls = new GLTR00100Cls();

                _Logger.LogInfo("Call Back Method GetGLJournalTransaction");
                loRtn.Data = loCls.GetGLJournalTransaction(poParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetGLJournal");

            return loRtn;
        }

        [HttpPost]
        public GLTR00100InitialDTO GetInitialVar()
        {
            var loEx = new R_Exception();
            GLTR00100InitialDTO loRtn = null;
            _Logger.LogInfo("Start GetInitialVar");

            try
            {
                var poParam = new GLTR00100InitialDTO();
                _Logger.LogInfo("Set Param GetInitialVar");
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CUSER_ID = R_BackGlobalVar.USER_ID;
                poParam.CUSER_LANGUAGE = R_BackGlobalVar.CULTURE;

                var loCls = new GLTR00100Cls();

                _Logger.LogInfo("Call Back Method GetInitial");
                loRtn = loCls.GetInitial(poParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetInitialVar");

            return loRtn;
        }
    }
}