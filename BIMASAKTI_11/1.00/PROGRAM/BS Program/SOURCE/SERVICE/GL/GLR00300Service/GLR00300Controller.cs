using GLR00300Back;
using GLR00300Common;
using GLR00300Common.Logs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System.Diagnostics;

namespace GLR00300Service
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class GLR00300Controller : ControllerBase, IGLR00300
    {
        private LoggerGLR00300 _loggerGLR00300;
        private readonly ActivitySource _activitySource;
        public GLR00300Controller(ILogger<GLR00300Controller> logger)
        {
            LoggerGLR00300.R_InitializeLogger(logger);
            _loggerGLR00300 = LoggerGLR00300.R_GetInstanceLogger();
            _activitySource = GLR00300Activity.R_InitializeAndGetActivitySource(nameof(GLR00300Controller));
        }

        [HttpPost]
        public R_ServiceGetRecordResultDTO<GLR00300DTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<GLR00300DTO> poParameter)
        {
            throw new NotImplementedException();
        }
        [HttpPost]
        public R_ServiceSaveResultDTO<GLR00300DTO> R_ServiceSave(R_ServiceSaveParameterDTO<GLR00300DTO> poParameter)
        {
            throw new NotImplementedException();
        }
        [HttpPost]
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<GLR00300DTO> poParameter)
        {
            throw new NotImplementedException();
        }
        [HttpPost]
        public GLR00300InitialProcess InitialProcess()
        {
            string lcMethodName = nameof(InitialProcess);
            using Activity activity = _activitySource.StartActivity(lcMethodName);
            _loggerGLR00300.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            R_Exception loException = new R_Exception();
            GLR00300DBParameter loDbParameter = new();
            GLR00300InitialProcess loReturn = null;
            try
            {
                var loCls = new GLR00300Cls();
                loDbParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loDbParameter.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;
                _loggerGLR00300.LogInfo("Call method InitialProcess on Controller");

                loReturn = loCls.InitialProcess(loDbParameter);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _loggerGLR00300.LogError(loException);
            }
        EndBlock:
            loException.ThrowExceptionIfErrors();
            _loggerGLR00300.LogInfo(string.Format("END process method {0} on Controller", lcMethodName));
            return loReturn;
        }

        [HttpPost]
        public GenericList<GLR00300GetPeriod> GetPeriod(GLR00300DBParameterDTO poParam)
        {
            string lcMethodName = nameof(GetPeriod);
            using Activity activity = _activitySource.StartActivity(lcMethodName);
            _loggerGLR00300.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            R_Exception loException = new R_Exception();
            GLR00300DBParameterDTO loDbParameter = new();
            GenericList<GLR00300GetPeriod> loReturn = null;
            try
            {
                var loCls = new GLR00300Cls();
                loDbParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loDbParameter.PERIOD_YEAR = poParam.PERIOD_YEAR;

                _loggerGLR00300.LogInfo(string.Format("Get Parameter {0} on Controller", lcMethodName));
                _loggerGLR00300.LogDebug("DbParameter {@Parameter} ", loDbParameter);

                _loggerGLR00300.LogInfo("Call method GetPeriodList on Controller");
                var loReturnTemp = loCls.GetPeriodList(loDbParameter);
                loReturn = new GenericList<GLR00300GetPeriod> { Data = loReturnTemp };
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _loggerGLR00300.LogError(loException);
            }
            EndBlock:
            loException.ThrowExceptionIfErrors();
            _loggerGLR00300.LogInfo(string.Format("END process method {0} on Controller", lcMethodName));

            return loReturn;
        }

        [HttpPost]
        public GenericList<GLR00300DTO> GetTrialBalanceType()
        {
            string lcMethodName = nameof(GetTrialBalanceType);
            using Activity activity = _activitySource.StartActivity(lcMethodName);
            _loggerGLR00300.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            R_Exception loException = new R_Exception();
            GLR00300DBParameter loDbParameter = new();
            GenericList<GLR00300DTO> loReturn = null;
            try
            {
                var loCls = new GLR00300Cls();
                loDbParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loDbParameter.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;

                _loggerGLR00300.LogInfo("Call method GetTrialBalanceTypeList on Controller");
                var loReturnTemp = loCls.GetTrialBalanceTypeList(loDbParameter);
                loReturn = new GenericList<GLR00300DTO> { Data = loReturnTemp };
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _loggerGLR00300.LogError(loException);
            }
        EndBlock:
            loException.ThrowExceptionIfErrors();
            _loggerGLR00300.LogInfo(string.Format("END process method {0} on Controller", lcMethodName));
           
            return loReturn;
        }
        [HttpPost]
        public GenericList<GLR00300DTO> GetPrintMethodType()
        {
            string lcMethodName = nameof(GetPrintMethodType);
            using Activity activity = _activitySource.StartActivity(lcMethodName);
            _loggerGLR00300.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            R_Exception loException = new R_Exception();
            GLR00300DBParameter loDbParameter = new();
            GenericList<GLR00300DTO> loReturn = null;
            try
            {
                var loCls = new GLR00300Cls();
                loDbParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loDbParameter.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;

                _loggerGLR00300.LogInfo("Call method GetPrintMethodTypeList on Controller");
                var loReturnTemp = loCls.GetPrintMethodTypeList(loDbParameter);
                loReturn = new GenericList<GLR00300DTO> { Data = loReturnTemp };
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _loggerGLR00300.LogError(loException);
            }
        EndBlock:
            loException.ThrowExceptionIfErrors();
            _loggerGLR00300.LogInfo(string.Format("END process method {0} on Controller", lcMethodName));

            return loReturn;
        }
        [HttpPost]
        public GenericList<GLR00300BudgetNoDTO> GetBudgetNo(GLR00300DBParameterDTO loParam)
        {
            string lcMethodName = nameof(GetBudgetNo);
            using Activity activity = _activitySource.StartActivity(lcMethodName);
            _loggerGLR00300.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            R_Exception loException = new R_Exception();
            GLR00300DBParameter loDbParameter = new();
            GenericList<GLR00300BudgetNoDTO> loReturn = null;
            try
            {
                var loCls = new GLR00300Cls();
                loDbParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loDbParameter.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;
                loDbParameter.CYEAR = loParam.PERIOD_YEAR;
                loDbParameter.CCURRENCY_TYPE = loParam.CURRENCY_TYPE;

                _loggerGLR00300.LogInfo(string.Format("Get Parameter {0} on Controller", lcMethodName));
                _loggerGLR00300.LogDebug("DbParameter {@Parameter} ", loDbParameter);

                _loggerGLR00300.LogInfo("Call method GetBudgetNoList on Controller");
                var loReturnTemp = loCls.GetBudgetNoList(loDbParameter);
                loReturn = new GenericList<GLR00300BudgetNoDTO> { Data = loReturnTemp };
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _loggerGLR00300.LogError(loException);
            }
        EndBlock:
            loException.ThrowExceptionIfErrors();
            _loggerGLR00300.LogInfo(string.Format("END process method {0} on Controller", lcMethodName));
            
            return loReturn;
        }
    }
}