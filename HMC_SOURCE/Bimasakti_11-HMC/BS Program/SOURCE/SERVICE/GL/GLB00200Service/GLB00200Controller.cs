using GLB00200Back;
using GLB00200Common;
using GLB00200Common.Logs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System.Diagnostics;

namespace GLB00200Service
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class GLB00200Controller : ControllerBase, IGLB00200
    {
        private LoggerGLB00200 _loggerGLB00200;
        private readonly ActivitySource _activitySource;
        public GLB00200Controller(ILogger<GLB00200Controller> logger)
        {
            LoggerGLB00200.R_InitializeLogger(logger);
            _loggerGLB00200 = LoggerGLB00200.R_GetInstanceLogger();
            _activitySource = GLB00200Activity.R_InitializeAndGetActivitySource(nameof(GLB00200Controller));

        }
        [HttpPost]
        public R_ServiceGetRecordResultDTO<GLB00200DTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<GLB00200DTO> poParameter)
        {
            throw new NotImplementedException();
        }
        [HttpPost]
        public R_ServiceSaveResultDTO<GLB00200DTO> R_ServiceSave(R_ServiceSaveParameterDTO<GLB00200DTO> poParameter)
        {
            throw new NotImplementedException();
        }
        [HttpPost]
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<GLB00200DTO> poParameter)
        {
            throw new NotImplementedException();
        }
        [HttpPost]
        public GLB00200InitalProcessDTO GetInitialProcess()
        {
            string lcMethodName = nameof(GetInitialProcess);
            using Activity activity = _activitySource.StartActivity(lcMethodName);
            _loggerGLB00200.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));
            R_Exception loException = new R_Exception();
            GLB00200DBParameter loDbParameter = new();
            GLB00200InitalProcessDTO loReturn = null;
            try
            {
                var loCls = new GLB00200Cls();
                loDbParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;

                _loggerGLB00200.LogInfo("Call method IntialProcess on Controller");

                loReturn = loCls.InitialProcess(loDbParameter);

            }
            catch (Exception ex)
            {
                loException.Add(loException);
                _loggerGLB00200.LogError(ex);
            }
            loException.ThrowExceptionIfErrors();
            _loggerGLB00200.LogInfo(string.Format("END process method {0} on Controller", lcMethodName));

            return loReturn!;
        }
        [HttpPost]
        public GLB00200JournalDetailListDTO DetailReversingJournalProcessList(GLB00200DTO loParam)
        {
            string lcMethodName = nameof(DetailReversingJournalProcessList);
            using Activity activity = _activitySource.StartActivity(lcMethodName);
            _loggerGLB00200.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));
            
            R_Exception loException = new R_Exception();
            GLB00200DBParameter loDbParameter;
            GLB00200JournalDetailListDTO loReturn = new();
            try
            {
                var loCls = new GLB00200Cls();
                loDbParameter = new GLB00200DBParameter();
                loDbParameter.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;
                loDbParameter.CREC_ID = loParam.CREC_ID;

                _loggerGLB00200.LogInfo(string.Format("Get Parameter {0} on Controller", lcMethodName));
                _loggerGLB00200.LogDebug("DbParameter {@Parameter} ", loDbParameter);

                _loggerGLB00200.LogInfo("Call method GetDetail_ReversingJournalList on Controller");
                var temp = loCls.GetDetail_ReversingJournalList(loDbParameter);
                loReturn.Data = temp;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _loggerGLB00200.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _loggerGLB00200.LogInfo(string.Format("END process method {0} on Controller", lcMethodName));

            return loReturn;
        }

       [HttpPost]
        public IAsyncEnumerable<GLB00200DTO> ReversingJournalProcessListStream()
        {
            string lcMethodName = nameof(ReversingJournalProcessListStream);
              using Activity activity = _activitySource.StartActivity(lcMethodName);
            _loggerGLB00200.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            R_Exception loException = new R_Exception();
            GLB00200DBParameter loDbParameter;
            List<GLB00200DTO> loReturnTemp;
            IAsyncEnumerable<GLB00200DTO> loRtn = null;
            try
            {
                loDbParameter = new GLB00200DBParameter();
                loDbParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loDbParameter.CUSER_ID = R_BackGlobalVar.USER_ID;
                loDbParameter.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;
                loDbParameter.CPERIOD = R_Utility.R_GetStreamingContext<string>(ContextConstant.CPERIOD);
                loDbParameter.CSEARCH_TEXT = R_Utility.R_GetStreamingContext<string>(ContextConstant.CSEARCH_TEXT);
              
                _loggerGLB00200.LogInfo(string.Format("Get Parameter {0} on Controller", lcMethodName));
                _loggerGLB00200.LogDebug("DbParameter {@Parameter} ", loDbParameter);
                
                var loCls = new GLB00200Cls();
                _loggerGLB00200.LogInfo("Call method ReversingJournalProcessList");
                loReturnTemp = loCls.ReversingJournalProcessList(loDbParameter);
                _loggerGLB00200.LogInfo("Call method to streaming data");
                loRtn = Get_ReversingJournalProcessList(loReturnTemp);

            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _loggerGLB00200.LogError(loException);
            }
            loException.ThrowExceptionIfErrors();
            _loggerGLB00200.LogInfo(string.Format("END process method {0} on Controller", lcMethodName));

            return loRtn;
        }
        private async IAsyncEnumerable<GLB00200DTO> Get_ReversingJournalProcessList(List<GLB00200DTO> poParameter)
        {
            foreach (var item in poParameter)
            {
                yield return item;
            }
        }
    }
}