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
    public class GLM00420Controller : ControllerBase, IGLM00420
    {
        private LoggerGLM00420 _Logger;

        public GLM00420Controller(ILogger<LoggerGLM00420> logger)
        {
            //Initial and Get Logger
            LoggerGLM00420.R_InitializeLogger(logger);
            _Logger = LoggerGLM00420.R_GetInstanceLogger();
        }

        [HttpPost]
        public IAsyncEnumerable<GLM00420DTO> GetSourceAllocationCenterList()
        {
            var loEx = new R_Exception();
            IAsyncEnumerable<GLM00420DTO> loRtn = null;
            _Logger.LogInfo("Start GetSourceAllocationCenterList");

            try
            {
                var loCls = new GLM00420Cls();
                var poParam = new GLM00420DTO();

                _Logger.LogInfo("Set Param GetSourceAllocationCenterList");
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CUSER_LANGUAGE = R_BackGlobalVar.CULTURE;
                poParam.CREC_ID_ALLOCATION_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CREC_ID_ALLOCATION_ID);

                _Logger.LogInfo("Call Back Method GetAllSourceAllocationCenter");
                var loResult = loCls.GetAllSourceAllocationCenter(poParam);

                _Logger.LogInfo("Call Stream Method Data GetSourceAllocationCenterList");
                loRtn = GetStream<GLM00420DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetSourceAllocationCenterList");

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<GLM00421DTO> GetAllocationCenterList()
        {
            var loEx = new R_Exception();
            IAsyncEnumerable<GLM00421DTO> loRtn = null;
            _Logger.LogInfo("Start GetAllocationCenterList");

            try
            {
                var loCls = new GLM00420Cls();
                var poParam = new GLM00421DTO();

                _Logger.LogInfo("Set Param GetAllocationCenterList");
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CUSER_LANGUAGE = R_BackGlobalVar.CULTURE;
                poParam.CREC_ID_ALLOCATION_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CREC_ID_ALLOCATION_ID);
                poParam.CUSER_ID = R_BackGlobalVar.USER_ID;

                _Logger.LogInfo("Call Back Method GetAllAllocationCenter");
                var loResult = loCls.GetAllAllocationCenter(poParam);

                _Logger.LogInfo("Call Stream Method Data GetAllocationCenterList");
                loRtn = GetStream<GLM00421DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetAllocationCenterList");

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
        public GLM00421DTO SaveAllocationCenterList(GLM00421DTO poParam)
        {
            R_Exception loException = new R_Exception();
            GLM00421DTO loRtn = new GLM00421DTO();
            GLM00420Cls loCls = new GLM00420Cls();
            _Logger.LogInfo("Start SaveAllocationCenterList");

            try
            {
                _Logger.LogInfo("Set Param SaveAllocationCenterList");
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CUSER_ID = R_BackGlobalVar.USER_ID;

                _Logger.LogInfo("Call Back Method SaveAllocationCenter");
                loCls.SaveAllocationCenter(poParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _Logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _Logger.LogInfo("End SaveAllocationCenterList");

            return loRtn;
        }
    }
}
