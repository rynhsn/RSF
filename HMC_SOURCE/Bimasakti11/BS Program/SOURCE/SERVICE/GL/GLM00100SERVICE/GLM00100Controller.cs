using GLM00100Back;
using GLM00100Back.DTOs;
using GLM00100Common;
using GLM00100Common.DTOs;
using GLM00100Common.Logs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;

namespace GLM00100Service
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GLM00100Controller : ControllerBase, IGLM00100
    {
        private LoggerGLM00100? _loggerGLM00100;

        public GLM00100Controller(ILogger<GLM00100Controller> logger)
        {
            LoggerGLM00100.R_InitializeLogger(logger);
            _loggerGLM00100 = LoggerGLM00100.R_GetInstanceLogger();
        }

        [HttpPost]
        public GLM00100CreateSystemParameterResultDTO CreateSystemParameter()
        {
            string? lcMethod = nameof(CreateSystemParameter);
            _loggerGLM00100.LogInfo(string.Format("Start Method {0}", lcMethod));
            var loEx = new R_Exception();
            var loRtn = new GLM00100CreateSystemParameterResultDTO();
            GLM00100CreateSystemParameterDbDTO loDbPar = new GLM00100CreateSystemParameterDbDTO();

            try
            {
                _loggerGLM00100.LogInfo(string.Format("Set the property of loDbPar Value in method {0}", nameof(R_ServiceGetRecord)));
                loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                if (!string.IsNullOrEmpty(loDbPar.CCOMPANY_ID))
                {
                    loDbPar.CUSER_ID = R_BackGlobalVar.USER_ID;
                    loDbPar.CSTARTING_YY = R_Utility.R_GetStreamingContext<string>(ContextConstantGLM00100CreateSystemParameter.CSTARTING_YY);
                    loDbPar.CSTARTING_MM = R_Utility.R_GetStreamingContext<string>(ContextConstantGLM00100CreateSystemParameter.CSTARTING_MM);
                }
                _loggerGLM00100.LogDebug("{@ObjectParameter}", loDbPar);

                _loggerGLM00100.LogInfo(string.Format("Initialize the loCls object as a new instance in method {0}", lcMethod));
                var loCls = new GLM00100Cls();
                _loggerGLM00100.LogDebug("{@ObjectGLM00100Cls}", loCls);

                _loggerGLM00100.LogInfo(string.Format("Perform the Create System Parameter using the CreateSystemParameter method of GLM00100Cls in method {0}", lcMethod));
                loCls.CreateSystemParameter(loDbPar);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            if (loEx.Haserror)
                _loggerGLM00100.LogError(loEx);

            loEx.ThrowExceptionIfErrors();

            _loggerGLM00100.LogInfo(string.Format("End Method {0}", lcMethod));

            return loRtn;
        }

        [HttpPost]
        public GLM00100ResultData GetCheckerSystemParameter()
        {
            string? lcMethod = nameof(GetCheckerSystemParameter);
            _loggerGLM00100.LogInfo(string.Format("Start Method {0}", lcMethod));
            var loEx = new R_Exception();
            var loRtn = new GLM00100ResultData();
            string lcPar = "";

            try
            {
                _loggerGLM00100.LogInfo(string.Format("Initialize the loCls object as a new instance in method {0}", lcMethod));
                var loCls = new GLM00100Cls();
                _loggerGLM00100.LogDebug("{@ObjectGLM00100Cls}", loCls);

                _loggerGLM00100.LogInfo(string.Format("Set the property of loDbPar Value in method {0}", lcMethod));
                lcPar = R_BackGlobalVar.COMPANY_ID;
                _loggerGLM00100.LogDebug("{@ObjectParameter}", lcPar);

                _loggerGLM00100.LogInfo(string.Format("Checking Parameter is Found or Not in method {0}", lcMethod));
                if (string.IsNullOrEmpty(lcPar))
                {
                    _loggerGLM00100.LogInfo(string.Format("Parameter System is not Found in method {0}", lcMethod));
                    loEx.Add("", "Company Id is Null");
                    goto EndBlocks;
                }
                _loggerGLM00100.LogInfo(string.Format("Parameter System is Found in method {0}", lcMethod));

                _loggerGLM00100.LogInfo(string.Format("Call the GetCheckerSystemParameter method of loCls and store the result in loRtn in method {0}", lcMethod));
                loRtn = loCls.GetCheckerSystemParameter(lcPar);
                _loggerGLM00100.LogDebug("{@ObjectReturn}", loRtn);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            EndBlocks:
            if (loEx.Haserror)
                _loggerGLM00100.LogError(loEx);

            loEx.ThrowExceptionIfErrors();

            _loggerGLM00100.LogInfo(string.Format("End Method {0}", lcMethod));
            return loRtn;
        }

        [HttpPost]
        public GLM00100GSMPeriod GetStartingPeriodYear()
        {
            string? lcMethod = nameof(GetStartingPeriodYear);
            _loggerGLM00100.LogInfo(string.Format("Start Method {0}", lcMethod));
            var loEx = new R_Exception();
            var loRtn = new GLM00100GSMPeriod();
            string lcPar = "";

            try
            {
                _loggerGLM00100.LogInfo(string.Format("Initialize the loCls object as a new instance in method {0}", lcMethod));
                var loCls = new GLM00100Cls();
                _loggerGLM00100.LogDebug("{@ObjectAPM00500DepartmentCls}", loCls);

                _loggerGLM00100.LogInfo(string.Format("Set the property of loDbPar Value in method {0}", lcMethod));
                lcPar = R_BackGlobalVar.COMPANY_ID;
                _loggerGLM00100.LogDebug("{@ObjectParameter}", lcPar);

                _loggerGLM00100.LogInfo(string.Format("Call the GetStartingPeriodYear method of loCls and store the result in loRtn in method {0}", lcMethod));
                loRtn = loCls.GetStartingPeriodYear(lcPar);
                _loggerGLM00100.LogDebug("{@ObjectReturn}", loRtn);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            if (loEx.Haserror)
                _loggerGLM00100.LogError(loEx);

            loEx.ThrowExceptionIfErrors();

            _loggerGLM00100.LogInfo(string.Format("End Method {0}", lcMethod));

            return loRtn;
        }

        [HttpPost]
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<GLM00100DTO> poParameter)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public R_ServiceGetRecordResultDTO<GLM00100DTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<GLM00100DTO> poParameter)
        {
            string? lcMethod = nameof(R_ServiceGetRecord);
            _loggerGLM00100.LogInfo(string.Format("Start Method {0}", lcMethod));
            var loEx = new R_Exception();
            var loRtn = new R_ServiceGetRecordResultDTO<GLM00100DTO>();

            try
            {
                _loggerGLM00100.LogInfo(string.Format("Initialize the loCls object as a new instance of APM00500DepartmentCls in method {0}", lcMethod));
                var loCls = new GLM00100Cls();
                _loggerGLM00100.LogDebug("{@ObjectGLM00100Cls}", loCls);

                _loggerGLM00100.LogInfo(string.Format("Set the property of poParameter.Entity Value in method {0}", lcMethod));
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                if (poParameter.Entity.CCOMPANY_ID != null)
                    poParameter.Entity.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;
                _loggerGLM00100.LogDebug("{@ObjectParameter}", poParameter.Entity);

                _loggerGLM00100.LogInfo(string.Format("Call the R_GetRecord method of loCls with poParameter.Entity and assign the result to loRtn.data in method {0}", lcMethod));
                loRtn.data = loCls.R_GetRecord(poParameter.Entity);
                _loggerGLM00100.LogDebug("{@ObjectReturn}", loRtn.data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            if (loEx.Haserror)
                _loggerGLM00100.LogError(loEx);

            loEx.ThrowExceptionIfErrors();

            _loggerGLM00100.LogInfo(string.Format("End Method {0}", lcMethod));

            return loRtn;
        }

        [HttpPost]
        public R_ServiceSaveResultDTO<GLM00100DTO> R_ServiceSave(R_ServiceSaveParameterDTO<GLM00100DTO> poParameter)
        {
            string? lcMethod = nameof(R_ServiceSave);
            _loggerGLM00100.LogInfo(string.Format("Start Method {0}", lcMethod));
            R_Exception loException = new R_Exception();
            R_ServiceSaveResultDTO<GLM00100DTO> loReturn = new();
            try
            {
                _loggerGLM00100.LogInfo(string.Format("Initialize the loCls object as a new instance GLM00100Cls of  in method {0}", lcMethod));
                GLM00100Cls loCls = new ();
                _loggerGLM00100.LogDebug("{@ObjectGLM00100Cls}", loCls);

                _loggerGLM00100.LogInfo(string.Format("Set the property of poParameter.Entity value in method {0}", lcMethod));
                poParameter.Entity.CCOMPANY_ID= R_BackGlobalVar.COMPANY_ID;
                if(poParameter.Entity.CCOMPANY_ID != null )
                {
                    poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
                    poParameter.Entity.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;
                }
                _loggerGLM00100.LogDebug("{@ObjectParameter}", poParameter.Entity);

                _loggerGLM00100.LogInfo(string.Format("Initialize the loReturn object with a new instance and set its data property using loCls.R_Save in method {0}", lcMethod));
                loReturn.data = loCls.R_Save(poParameter.Entity, poParameter.CRUDMode);
                _loggerGLM00100.LogDebug("{@ObjectReturn}", loReturn);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            if (loException.Haserror)
                _loggerGLM00100.LogError(loException);

            loException.ThrowExceptionIfErrors();

            _loggerGLM00100.LogInfo(string.Format("End Method {0}", lcMethod));

            return loReturn;
        }
    }
}
