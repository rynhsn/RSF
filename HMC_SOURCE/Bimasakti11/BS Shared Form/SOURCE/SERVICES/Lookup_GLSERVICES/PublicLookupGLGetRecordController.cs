using Lookup_GLBACK;
using Lookup_GLCOMMON;
using Lookup_GLCOMMON.DTOs;
using Lookup_GLCOMMON.DTOs.GLL00100;
using Lookup_GLCOMMON.DTOs.GLL00110;
using Lookup_GLCOMMON.Loggers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;
using System.Diagnostics;

namespace Lookup_GLSERVICES
{
    [ApiController]
    [Route("api/[controller]/[action]"), AllowAnonymous]
    public class PublicLookupGLGetRecordController : ControllerBase, IPublicLookupGetRecordGL
    {
        private LoggerLookupGL _loggerLookup;
        private readonly ActivitySource _activitySource;

        public PublicLookupGLGetRecordController(ILogger<LoggerLookupGL> logger)
        {
            //Initial and Get Logger
            LoggerLookupGL.R_InitializeLogger(logger);
            _loggerLookup = LoggerLookupGL.R_GetInstanceLogger();
            _activitySource = LookupGLActivity.R_InitializeAndGetActivitySource(nameof(PublicLookupGLGetRecordController));
        }


        [HttpPost]
        public GLLGenericRecord<GLL00100DTO> GLL00100ReferenceNoLookUp(GLL00100ParameterGetRecordDTO poParameter)
        {
            string lcMethodName = nameof(GLL00100ReferenceNoLookUp);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _loggerLookup.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            var loEx = new R_Exception();
            GLLGenericRecord<GLL00100DTO> loReturn = new();
            try
            {
                var loCls = new PublicLookupGLCls();
                _loggerLookup.LogInfo("Call method GLL00100ReferenceNoLookUp");
                var loParam = R_Utility.R_ConvertObjectToObject<GLL00100ParameterGetRecordDTO, GLL00100ParameterDTO>(poParameter);
                loParam.CUSER_ID = R_BackGlobalVar.USER_ID;
                loParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParam.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;
                var loTempList = loCls.GLL00100ReferenceNoLookUpDb(loParam);

                _loggerLookup.LogInfo("Filter Search by text");
                loReturn.Data = loTempList
                    .Find(x => x.CREF_NO!
                        .Equals(poParameter.CSEARCH_TEXT!
                        .Trim(),
                        StringComparison.OrdinalIgnoreCase))!;

            }
            catch (Exception ex)
            {

                loEx.Add(ex);
                _loggerLookup.LogError(loEx);
            }
            loEx.ThrowExceptionIfErrors();
            _loggerLookup.LogInfo(string.Format("END process method {0} on Controller", lcMethodName));
            return loReturn;
        }


        [HttpPost]
        public GLLGenericRecord<GLL00110DTO> GLL00100ReferenceNoLookUpByPeriod(GLL00110ParameterGetRecordDTO poParameter)
        {
            string lcMethodName = nameof(GLL00100ReferenceNoLookUpByPeriod);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _loggerLookup.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            var loEx = new R_Exception();
            GLLGenericRecord<GLL00110DTO> loReturn = new();
            try
            {
                var loCls = new PublicLookupGLCls();
                _loggerLookup.LogInfo("Call method GLL00100ReferenceNoLookUpByPeriod");
               
                var loParam = R_Utility.R_ConvertObjectToObject<GLL00110ParameterGetRecordDTO, GLL00110ParameterDTO>(poParameter);
                loParam.CUSER_ID = R_BackGlobalVar.USER_ID;
                loParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParam.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;
                var loTempList = loCls.GLL00110ReferenceNoLookUpByPeriodDb(loParam);

                _loggerLookup.LogInfo("Filter Search by text");
                loReturn.Data = loTempList
                    .Find(x => x.CREF_NO!
                        .Equals(poParameter.CSEARCH_TEXT!
                        .Trim(),
                        StringComparison.OrdinalIgnoreCase))!;

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _loggerLookup.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            _loggerLookup.LogInfo(string.Format("END process method {0} on Controller", lcMethodName));
            return loReturn;
        }
    }

}