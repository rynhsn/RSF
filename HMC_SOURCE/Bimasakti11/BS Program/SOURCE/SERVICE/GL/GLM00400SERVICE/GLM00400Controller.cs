using GLM00400BACK;
using GLM00400COMMON;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLM00400SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class GLM00400Controller : ControllerBase, IGLM00400
    {
        private LoggerGLM00400 _Logger;
        public GLM00400Controller(ILogger<LoggerGLM00400> logger)
        {
            //Initial and Get Logger
            LoggerGLM00400.R_InitializeLogger(logger);
            _Logger = LoggerGLM00400.R_GetInstanceLogger();
        }

        [HttpPost]
        public GLM00400InitialDTO GetInitialVar(GLM00400InitialDTO poParam)
        {
            var loEx = new R_Exception();
            GLM00400InitialDTO loRtn = null;
            _Logger.LogInfo("Start GetInitialVar");

            try
            {
                _Logger.LogInfo("Set Param GetInitialVar");
                loRtn = new GLM00400InitialDTO();
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CUSER_ID = R_BackGlobalVar.USER_ID;

                var loCls = new GLM00400Cls();

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

        [HttpPost]
        public GLM00400GLSystemParamDTO GetSystemParam(GLM00400GLSystemParamDTO poParam)
        {
            var loEx = new R_Exception();
            GLM00400GLSystemParamDTO loRtn = null;
            _Logger.LogInfo("Start GetSystemParam");

            try
            {
                _Logger.LogInfo("Set Param GetSystemParam");
                loRtn = new GLM00400GLSystemParamDTO();
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CUSER_LANGUAGE = R_BackGlobalVar.CULTURE;

                var loCls = new GLM00400Cls();

                _Logger.LogInfo("Call Back Method GetSystemParam");
                loRtn = loCls.GetSystemParam(poParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetSystemParam");

            return loRtn;

        }


        [HttpPost]
        public IAsyncEnumerable<GLM00400DTO> GetAllocationJournalHDList()
        {
            var loEx = new R_Exception();
            IAsyncEnumerable<GLM00400DTO> loRtn = null;
            _Logger.LogInfo("Start GetAllocationJournalHDList");

            try
            {
                var loCls = new GLM00400Cls();
                var poParam = new GLM00400DTO();

                _Logger.LogInfo("Set Param GetAllocationJournalHDList");
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CUSER_LANGUAGE = R_BackGlobalVar.CULTURE;

                _Logger.LogInfo("Call Back Method GetAllAllocationJournalHD");
                var loTempRtn = loCls.GetAllAllocationJournalHD(poParam);

                _Logger.LogInfo("Call Stream Method Data GetAllocationJournalHDList");
                loRtn = GetAllocationJournalHDListStream(loTempRtn);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetAllocationJournalHDList");

            return loRtn;
        }
        private async IAsyncEnumerable<GLM00400DTO> GetAllocationJournalHDListStream(List<GLM00400DTO> poParameter)
        {
            foreach (GLM00400DTO item in poParameter)
            {
                yield return item;
            }
        }
    }
}
