using Lookup_PMBACK;
using Lookup_PMCOMMON;
using Lookup_PMCOMMON.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using R_BackEnd;
using R_Common;
using Lookup_PMCOMMON.Logs;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Lookup_PMSERVICES
{
    [ApiController]
    [Route("api/[controller]/[action]"), AllowAnonymous]
    public class PublicLookupLMController : ControllerBase, IPublicLookupLM
    {
        private LoggerLookupLM _loggerLookup;
        private readonly ActivitySource _activitySource;
        public PublicLookupLMController(ILogger<PublicLookupLMController> logger)
        {

            LoggerLookupLM.R_InitializeLogger(logger);
            _loggerLookup = LoggerLookupLM.R_GetInstanceLogger();
            _activitySource = LookupLMActivity.R_InitializeAndGetActivitySource(nameof(PublicLookupLMController));
        }
        
        [HttpPost]
        public IAsyncEnumerable<LML00200DTO> LML00200UnitChargesList()
        {
            string lcMethodName = nameof(LML00200UnitChargesList);
            using Activity activity = _activitySource.StartActivity(lcMethodName);
            _loggerLookup.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            var loEx = new R_Exception();
            IAsyncEnumerable<LML00200DTO> loRtn = null;
            List<LML00200DTO> loReturnTemp;
            try
            {
                var loCls = new PublicLookupLMCls();
                var poParameter = new LML00200ParameterDTO();

                poParameter.CCOMPANY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CCOMPANY_ID);
                poParameter.CUSER_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CUSER_ID);
                poParameter.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CPROPERTY_ID);
                poParameter.CCHARGE_TYPE_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CCHARGE_TYPE_ID);
                
                _loggerLookup.LogInfo(string.Format("Get Parameter {0} on Controller", lcMethodName));
                _loggerLookup.LogDebug("DbParameter {@Parameter} ", poParameter);
                _loggerLookup.LogInfo("Call method GetAllUnitCharges");

                loReturnTemp = loCls.GetAllUnitCharges(poParameter);
                loRtn = GetStreaming(loReturnTemp);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            _loggerLookup.LogInfo(string.Format("END process method {0} on Controller", lcMethodName));
            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<LML00300DTO> LML00300SupervisorList()
        {
            string lcMethodName = nameof(LML00300SupervisorList);
            using Activity activity = _activitySource.StartActivity(lcMethodName);
            _loggerLookup.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            var loEx = new R_Exception();
            IAsyncEnumerable<LML00300DTO> loRtn = null;
            List<LML00300DTO> loReturnTemp;

            try
            {
                var loCls = new PublicLookupLMCls();
                var poParameter = new LML00300ParameterDTO();
                poParameter.CCOMPANY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CCOMPANY_ID);
                poParameter.CUSER_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CUSER_ID);
                poParameter.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CPROPERTY_ID);

                _loggerLookup.LogInfo(string.Format("Get Parameter {0} on Controller", lcMethodName));
                _loggerLookup.LogDebug("DbParameter {@Parameter} ", poParameter);
                _loggerLookup.LogInfo("Call method GetAllSupervisor");

                loReturnTemp = loCls.GetAllSupervisor(poParameter);
                loRtn = GetStreaming(loReturnTemp);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            _loggerLookup.LogInfo(string.Format("END process method {0} on Controller", lcMethodName));
            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<LML00400DTO> LML00400UtilityChargesList()
        {
            string lcMethodName = nameof(LML00400UtilityChargesList);
            using Activity activity = _activitySource.StartActivity(lcMethodName);
            _loggerLookup.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            var loEx = new R_Exception();
            IAsyncEnumerable<LML00400DTO> loRtn = null;
            List<LML00400DTO> loReturnTemp;

            try
            {
                var loCls = new PublicLookupLMCls();
                var poParameter = new LML00400ParameterDTO();
                poParameter.CCOMPANY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CCOMPANY_ID);
                poParameter.CUSER_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CUSER_ID);
                poParameter.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CPROPERTY_ID);
                poParameter.CCHARGE_TYPE_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CCHARGE_TYPE_ID);

                _loggerLookup.LogInfo(string.Format("Get Parameter {0} on Controller", lcMethodName));
                _loggerLookup.LogDebug("DbParameter {@Parameter} ", poParameter);
                _loggerLookup.LogInfo("Call method GetAllUtilityCharges");

                loReturnTemp = loCls.GetAllUtilityCharges(poParameter);
                loRtn = GetStreaming(loReturnTemp);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            _loggerLookup.LogInfo(string.Format("END process method {0} on Controller", lcMethodName));
            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<LML00500DTO> LML00500SalesmanList()
        {
            string lcMethodName = nameof(LML00500SalesmanList);
            using Activity activity = _activitySource.StartActivity(lcMethodName);
            _loggerLookup.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            var loEx = new R_Exception();
            IAsyncEnumerable<LML00500DTO> loRtn = null;
            List<LML00500DTO> loReturnTemp;

            try
            {
                var loCls = new PublicLookupLMCls();
                var poParameter = new LML00500ParameterDTO();
                poParameter.CCOMPANY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CCOMPANY_ID);
                poParameter.CUSER_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CUSER_ID);
                poParameter.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CPROPERTY_ID);

                _loggerLookup.LogInfo(string.Format("Get Parameter {0} on Controller", lcMethodName));
                _loggerLookup.LogDebug("DbParameter {@Parameter} ", poParameter);
                _loggerLookup.LogInfo("Call method GetAllSalesman");

                loReturnTemp = loCls.GetAllSalesman(poParameter);
                loRtn = GetStreaming(loReturnTemp);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            _loggerLookup.LogInfo(string.Format("END process method {0} on Controller", lcMethodName));
            return loRtn;
        }
        [HttpPost]
        public IAsyncEnumerable<LML00600DTO> LML00600TenantList()
        {
            string lcMethodName = nameof(LML00600TenantList);
            using Activity activity = _activitySource.StartActivity(lcMethodName);
            _loggerLookup.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            var loEx = new R_Exception();
            IAsyncEnumerable<LML00600DTO> loRtn = null;
            List<LML00600DTO> loReturnTemp;

            try
            {
                var loCls = new PublicLookupLMCls();
                var poParameter = new LML00600ParameterDTO();
                poParameter.CCOMPANY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CCOMPANY_ID);
                poParameter.CUSER_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CUSER_ID);
                poParameter.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CPROPERTY_ID);
                poParameter.CCUSTOMER_TYPE = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CCUSTOMER_TYPE);

                _loggerLookup.LogInfo(string.Format("Get Parameter {0} on Controller", lcMethodName));
                _loggerLookup.LogDebug("DbParameter {@Parameter} ", poParameter);
                _loggerLookup.LogInfo("Call method GetAllTenant");

                loReturnTemp = loCls.GetTenant(poParameter);
                loRtn = GetStreaming(loReturnTemp);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            _loggerLookup.LogInfo(string.Format("END process method {0} on Controller", lcMethodName));
            return loRtn;
        }
        [HttpPost]
        public IAsyncEnumerable<LML00700DTO> LML00700DiscountList()
        {
            string lcMethodName = nameof(LML00700DiscountList);
            using Activity activity = _activitySource.StartActivity(lcMethodName);
            _loggerLookup.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            var loEx = new R_Exception();
            IAsyncEnumerable<LML00700DTO> loRtn = null;
            List<LML00700DTO> loReturnTemp;

            try
            {
                var loCls = new PublicLookupLMCls();
                var poParameter = new LML00700ParameterDTO();
                poParameter.CCOMPANY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CCOMPANY_ID);
                poParameter.CUSER_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CUSER_ID);
                poParameter.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CPROPERTY_ID);
                poParameter.CCHARGES_TYPE = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CCHARGE_TYPE_ID);

                _loggerLookup.LogInfo(string.Format("Get Parameter {0} on Controller", lcMethodName));
                _loggerLookup.LogDebug("DbParameter {@Parameter} ", poParameter);
                _loggerLookup.LogInfo("Call method GetAllDiscount");

                loReturnTemp = loCls.GetDiscount(poParameter);
                loRtn = GetStreaming(loReturnTemp);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            _loggerLookup.LogInfo(string.Format("END process method {0} on Controller", lcMethodName));
            return loRtn;
        }


        #region StreamingProcess
        private async IAsyncEnumerable<T> GetStreaming<T>(List<T> poParam)
        {
            foreach (var item in poParam)
            {
                yield return item;
            }
        }

        #endregion
    }
    
}