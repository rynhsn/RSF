using Lookup_FABack;
using Lookup_FACommon;
using Lookup_FACommon.DTOs;
using Lookup_FACommon.Loggers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;
using System.Diagnostics;

namespace Lookup_FAServices
{

    [ApiController]
    [Route("api/[controller]/[action]"), AllowAnonymous]
    public class PublicLookupFAController : ControllerBase, IPublicLookupFA
    {
        private LoggerLookupFA _loggerLookup;
        private readonly ActivitySource _activitySource;
        public PublicLookupFAController(ILogger<PublicLookupFAController> logger)
        {

            LoggerLookupFA.R_InitializeLogger(logger);
            _loggerLookup = LoggerLookupFA.R_GetInstanceLogger();
            _activitySource = LookupFAActivity.R_InitializeAndGetActivitySource(nameof(PublicLookupFAController));
        }

        [HttpPost]
        public IAsyncEnumerable<FAL00100DTO> FAL00100TaxTypeLookup()
        {
            string lcMethodName = nameof(FAL00100TaxTypeLookup);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _loggerLookup.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));
            var loEx = new R_Exception();
            IAsyncEnumerable<FAL00100DTO> loRtn = null;
            List<FAL00100DTO> loReturnTemp;
            try
            {
                var loCls = new PublicLookupFACls();
                var poParameter = new FAL00100ParameterDTO();
                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CSTATUS = R_Utility.R_GetStreamingContext<string>(FAL00100ContextDTO.CSTATUS);
                poParameter.CTAX_TYPE_ID = R_Utility.R_GetStreamingContext<string>(FAL00100ContextDTO.CTAX_TYPE_ID);
                poParameter.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;
                _loggerLookup.LogInfo(string.Format("Get Parameter {0} on Controller", lcMethodName));
                _loggerLookup.LogDebug("DbParameter {@Parameter} ", poParameter);
                _loggerLookup.LogInfo("Call method FAL00100TaxTypeLookupDb");
                loReturnTemp = loCls.FAL00100TaxTypeLookupDb(poParameter);
                loRtn = GetStream(loReturnTemp);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            _loggerLookup.LogInfo(string.Format("END process method {0} on Controller", lcMethodName));
            return loRtn!;
        }

        [HttpPost]
        public IAsyncEnumerable<FAL00200DTO> FAL00200TaxCategoryLookup()
        {
            string lcMethodName = nameof(FAL00200TaxCategoryLookup);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _loggerLookup.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));
            var loEx = new R_Exception();
            IAsyncEnumerable<FAL00200DTO> loRtn = null;
            List<FAL00200DTO> loReturnTemp;
            try
            {
                var loCls = new PublicLookupFACls();
                var poParameter = new FAL00200ParameterDTO();
                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CSTATUS = R_Utility.R_GetStreamingContext<string>(FAL00200ContextDTO.CSTATUS);
                poParameter.CTAX_CATEGORY_ID = R_Utility.R_GetStreamingContext<string>(FAL00200ContextDTO.CTAX_CATEGORY_ID);
                poParameter.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;
                _loggerLookup.LogInfo(string.Format("Get Parameter {0} on Controller", lcMethodName));
                _loggerLookup.LogDebug("DbParameter {@Parameter} ", poParameter);
                _loggerLookup.LogInfo("Call method FAL00200TaxCategoryLookupDb");
                loReturnTemp = loCls.FAL00200TaxCategoryLookupDb(poParameter);
                loRtn = GetStream(loReturnTemp);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            _loggerLookup.LogInfo(string.Format("END process method {0} on Controller", lcMethodName));
            return loRtn!;
        }

        [HttpPost]
        public IAsyncEnumerable<FAL00300DTO> FAL00300AssetLookup()
        {
            string lcMethodName = nameof(FAL00300AssetLookup);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _loggerLookup.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));
            var loEx = new R_Exception();
            IAsyncEnumerable<FAL00300DTO> loRtn = null;
            List<FAL00300DTO> loReturnTemp;
            try
            {
                var loCls = new PublicLookupFACls();
                var poParameter = new FAL00300ParameterDTO();
                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CTRANS_CODE = R_Utility.R_GetStreamingContext<string>(FAL00300ContextDTO.CTRANS_CODE);
                poParameter.CASSET_CODE = R_Utility.R_GetStreamingContext<string>(FAL00300ContextDTO.CASSET_CODE);
                poParameter.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;
                _loggerLookup.LogInfo(string.Format("Get Parameter {0} on Controller", lcMethodName));
                _loggerLookup.LogDebug("DbParameter {@Parameter} ", poParameter);
                _loggerLookup.LogInfo("Call method FAL00300AssetLookup");
                loReturnTemp = loCls.FAL00300AssetLookupDb(poParameter);
                loRtn = GetStream(loReturnTemp);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            _loggerLookup.LogInfo(string.Format("END process method {0} on Controller", lcMethodName));
            return loRtn!;
        }

        #region Stream Data
        private async IAsyncEnumerable<T> GetStream<T>(List<T> poParam)
        {
            foreach (var item in poParam)
            {
                yield return item;
            }
        }

        #endregion
    }
}
