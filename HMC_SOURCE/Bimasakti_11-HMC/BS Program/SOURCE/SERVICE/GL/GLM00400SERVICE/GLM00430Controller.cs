using GLM00400BACK;
using GLM00400COMMON;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLM00400SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class GLM00430Controller : ControllerBase, IGLM00430
    {
        private LoggerGLM00430 _Logger;
        public GLM00430Controller(ILogger<LoggerGLM00430> logger)
        {
            //Initial and Get Logger
            LoggerGLM00430.R_InitializeLogger(logger);
            _Logger = LoggerGLM00430.R_GetInstanceLogger();
        }

        [HttpPost]
        public IAsyncEnumerable<GLM00430DTO> GetSourceAllocationAccountList()
        {
            var loEx = new R_Exception();
            IAsyncEnumerable<GLM00430DTO> loRtn = null;
            _Logger.LogInfo("Start GetSourceAllocationAccountList");

            try
            {
                var loCls = new GLM00430Cls();
                var poParam = new GLM00430DTO();

                _Logger.LogInfo("Set Param GetSourceAllocationAccountList");
                poParam.CUSER_LANGUAGE = R_BackGlobalVar.CULTURE;
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;

                _Logger.LogInfo("Call Back Method GetAllSourceAllocationAccount");
                var loResult = loCls.GetAllSourceAllocationAccount(poParam);
                
                _Logger.LogInfo("Call Stream Method Data GetSourceAllocationAccountList");
                loRtn = GetStream<GLM00430DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetSourceAllocationAccountList");

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<GLM00431DTO> GetAllocationAccountList()
        {
            var loEx = new R_Exception();
            IAsyncEnumerable<GLM00431DTO> loRtn = null;
            _Logger.LogInfo("Start GetAllocationAccountList");

            try
            {
                var loCls = new GLM00430Cls();
                var poParam = new GLM00431DTO();

                _Logger.LogInfo("Set Param GetAllocationAccountList");
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CUSER_LANGUAGE = R_BackGlobalVar.CULTURE;
                poParam.CREC_ID_ALLOCATION_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CREC_ID_ALLOCATION_ID);
                poParam.CUSER_ID = R_BackGlobalVar.USER_ID;

                _Logger.LogInfo("Call Back Method GetAllAllocationAccount");
                var loResult = loCls.GetAllAllocationAccount(poParam);

                _Logger.LogInfo("Call Stream Method Data GetAllocationAccountList");
                loRtn = GetStream<GLM00431DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetAllocationAccountList");

            return loRtn;
        }

        private async IAsyncEnumerable<T> GetStream<T>(List<T> poList)
        {
            foreach (var item in poList)
            {
                yield return item;
            }
        }

        [HttpPost]
        public GLM00431DTO SaveAllocationAccountList(GLM00431DTO poParam)
        {
            R_Exception loException = new R_Exception();
            GLM00431DTO loRtn = new GLM00431DTO();
            GLM00430Cls loCls = new GLM00430Cls();
            _Logger.LogInfo("Start SaveAllocationAccountList");

            try
            {
                _Logger.LogInfo("Set Param SaveAllocationAccountList");
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CUSER_ID = R_BackGlobalVar.USER_ID;

                _Logger.LogInfo("Call Back Method SaveAllocationAccount");
                loCls.SaveAllocationAccount(poParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _Logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _Logger.LogInfo("End SaveAllocationAccountList");

            return loRtn;
        }
    }
}
