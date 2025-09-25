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
using Lookup_PMCOMMON.DTOs.LML01000;
using Lookup_PMCOMMON.DTOs.LML01100;
using Lookup_PMCOMMON.DTOs.LML01300;

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

                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CUSER_ID = R_BackGlobalVar.USER_ID;
                poParameter.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CPROPERTY_ID);
                poParameter.CCHARGE_TYPE_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CCHARGE_TYPE_ID);
                poParameter.CTAXABLE_TYPE = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CTAXABLE_TYPE);
                poParameter.CACTIVE_TYPE = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CACTIVE_TYPE);
                poParameter.CTAX_DATE = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CTAX_DATE);

                _loggerLookup.LogInfo(string.Format("Get Parameter {0} on Controller", lcMethodName));
                _loggerLookup.LogDebug("DbParameter {@Parameter} ", poParameter);
                _loggerLookup.LogInfo("Call method GetAllUnitCharges");

                loReturnTemp = loCls.GetAllUnitCharges(poParameter);
                loRtn = GetStreaming(loReturnTemp);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _loggerLookup.LogError(ex);
            }

            loEx.ThrowExceptionIfErrors();
            _loggerLookup.LogInfo(string.Format("END process method {0} on Controller", lcMethodName));
            return loRtn!;
        }

        [HttpPost]
        public IAsyncEnumerable<LML00300DTO> LML00300SupervisorList()
        {
            string lcMethodName = nameof(LML00300SupervisorList);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _loggerLookup.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            var loEx = new R_Exception();
            IAsyncEnumerable<LML00300DTO> loRtn = null;
            List<LML00300DTO> loReturnTemp;

            try
            {
                var loCls = new PublicLookupLMCls();
                var poParameter = new LML00300ParameterDTO();
                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CUSER_ID = R_BackGlobalVar.USER_ID;
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
                _loggerLookup.LogError(ex);
            }
            loEx.ThrowExceptionIfErrors();
            _loggerLookup.LogInfo(string.Format("END process method {0} on Controller", lcMethodName));
            return loRtn!;
        }

        [HttpPost]
        public IAsyncEnumerable<LML00400DTO> LML00400UtilityChargesList()
        {
            string lcMethodName = nameof(LML00400UtilityChargesList);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _loggerLookup.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            var loEx = new R_Exception();
            IAsyncEnumerable<LML00400DTO> loRtn = null;
            List<LML00400DTO> loReturnTemp;

            try
            {
                var loCls = new PublicLookupLMCls();
                var poParameter = new LML00400ParameterDTO();
                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CUSER_ID = R_BackGlobalVar.USER_ID;
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
                _loggerLookup.LogError(ex);
            }

            loEx.ThrowExceptionIfErrors();
            _loggerLookup.LogInfo(string.Format("END process method {0} on Controller", lcMethodName));
            return loRtn!;
        }

        [HttpPost]
        public IAsyncEnumerable<LML00500DTO> LML00500SalesmanList()
        {
            string lcMethodName = nameof(LML00500SalesmanList);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _loggerLookup.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            var loEx = new R_Exception();
            IAsyncEnumerable<LML00500DTO> loRtn = null;
            List<LML00500DTO> loReturnTemp;

            try
            {
                var loCls = new PublicLookupLMCls();
                var poParameter = new LML00500ParameterDTO();
                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CUSER_ID = R_BackGlobalVar.USER_ID;
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
                _loggerLookup.LogError(ex);
            }
            loEx.ThrowExceptionIfErrors();
            _loggerLookup.LogInfo(string.Format("END process method {0} on Controller", lcMethodName));
            return loRtn!;
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
                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CUSER_ID = R_BackGlobalVar.USER_ID;
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
                _loggerLookup.LogError(ex);
            }
            loEx.ThrowExceptionIfErrors();
            _loggerLookup.LogInfo(string.Format("END process method {0} on Controller", lcMethodName));
            return loRtn!;
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
                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CUSER_ID = R_BackGlobalVar.USER_ID;
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
                _loggerLookup.LogError(ex);
            }
            loEx.ThrowExceptionIfErrors();
            _loggerLookup.LogInfo(string.Format("END process method {0} on Controller", lcMethodName));
            return loRtn!;
        }
        [HttpPost]
        public IAsyncEnumerable<LML00800DTO> LML00800AgreementList()
        {
            string lcMethodName = nameof(LML00800AgreementList);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _loggerLookup.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            var loEx = new R_Exception();
            IAsyncEnumerable<LML00800DTO> loRtn = null;
            List<LML00800DTO> loReturnTemp;

            try
            {
                var loCls = new PublicLookupLMCls();
                var poParameter = new LML00800ParameterDTO();
                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CPROPERTY_ID);
                poParameter.CDEPT_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CDEPT_CODE);
                poParameter.CAGGR_STTS = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CAGGR_STTS);
                poParameter.CLANG_ID = R_BackGlobalVar.CULTURE;
                //CR26/06/2024
                poParameter.CTRANS_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CTRANS_CODE);
                poParameter.CREF_NO = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CREF_NO);
                poParameter.CTENANT_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CTENANT_ID);
                poParameter.CBUILDING_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CBUILDING_ID);
                poParameter.CTRANS_STATUS = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CTRANS_STATUS);

                _loggerLookup.LogInfo(string.Format("Get Parameter {0} on Controller", lcMethodName));
                _loggerLookup.LogDebug("DbParameter {@Parameter} ", poParameter);
                _loggerLookup.LogInfo("Call method GetAllAgreement");

                loReturnTemp = loCls.GetAgreement(poParameter);
                loRtn = GetStreaming(loReturnTemp);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _loggerLookup.LogError(ex);
            }
            loEx.ThrowExceptionIfErrors();
            _loggerLookup.LogInfo(string.Format("END process method {0} on Controller", lcMethodName));
            return loRtn!;
        }
        [HttpPost]
        public IAsyncEnumerable<LML00900DTO> LML00900TransactionList()
        {
            string lcMethodName = nameof(LML00900TransactionList);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _loggerLookup.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            var loEx = new R_Exception();
            IAsyncEnumerable<LML00900DTO> loRtn = null;
            List<LML00900DTO> loReturnTemp;

            try
            {
                var loCls = new PublicLookupLMCls();
                var poParameter = new LML00900ParameterDTO();
                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CPROPERTY_ID);
                poParameter.CDEPT_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CDEPT_CODE);
                poParameter.CTENANT_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CTENANT_ID);            
                poParameter.CUSER_ID = R_BackGlobalVar.USER_ID;
                poParameter.CTRANS_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CTRANS_CODE);
                poParameter.CPERIOD = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CPERIOD);
                poParameter.LHAS_REMAINING = R_Utility.R_GetStreamingContext<bool>(ContextConstantPublicLookup.LHAS_REMAINING);
                poParameter.LNO_REMAINING = R_Utility.R_GetStreamingContext<bool>(ContextConstantPublicLookup.LNO_REMAINING);
                poParameter.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;
                poParameter.CCURRENCY_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CCURRENCY_CODE);

                _loggerLookup.LogInfo(string.Format("Get Parameter {0} on Controller", lcMethodName));
                _loggerLookup.LogDebug("DbParameter {@Parameter} ", poParameter);
                _loggerLookup.LogInfo($"Call method {0}", lcMethodName);

                loReturnTemp = loCls.GetTransaction(poParameter);
                loRtn = GetStreaming(loReturnTemp);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _loggerLookup.LogError(ex);
            }
            loEx.ThrowExceptionIfErrors();
            _loggerLookup.LogInfo(string.Format("END process method {0} on Controller", lcMethodName));
            return loRtn!;
        }
        [HttpPost]
        public IAsyncEnumerable<LML01000DTO> LML01000BillingRuleList()
        {
            string lcMethodName = nameof(LML01000BillingRuleList);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _loggerLookup.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            var loEx = new R_Exception();
            IAsyncEnumerable<LML01000DTO> loRtn = null;
            List<LML01000DTO> loReturnTemp;

            try
            {
                var loCls = new PublicLookupLMCls();
                var poParameter = new LML01000ParameterDTO();
                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CUSER_ID = R_BackGlobalVar.USER_ID;

                poParameter.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CPROPERTY_ID);
                poParameter.CBILLING_RULE_TYPE = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CBILLING_RULE_TYPE);
                poParameter.CUNIT_TYPE_CTG_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CUNIT_TYPE_CTG_ID);
                poParameter.LACTIVE_ONLY = R_Utility.R_GetStreamingContext<bool>(ContextConstantPublicLookup.LACTIVE_ONLY);

                _loggerLookup.LogInfo(string.Format("Get Parameter {0} on Controller", lcMethodName));
                _loggerLookup.LogDebug("DbParameter {@Parameter} ", poParameter);
                _loggerLookup.LogInfo(string.Format("Call Method {0} ", lcMethodName));

                loReturnTemp = loCls.GetBillingRule(poParameter);
                loRtn = GetStreaming(loReturnTemp);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _loggerLookup.LogError(ex);
            }
            loEx.ThrowExceptionIfErrors();
            _loggerLookup.LogInfo(string.Format("END process method {0} on Controller", lcMethodName));
            return loRtn!;
        }
        [HttpPost]
        public IAsyncEnumerable<LML01100DTO> LML01100TNCList()
        {
            string lcMethodName = nameof(LML01100TNCList);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _loggerLookup.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            var loEx = new R_Exception();
            IAsyncEnumerable<LML01100DTO> loRtn = null;
            List<LML01100DTO> loReturnTemp;

            try
            {
                var loCls = new PublicLookupLMCls();
                var poParameter = new LML01100ParameterDTO();
                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CUSER_ID = R_BackGlobalVar.USER_ID;
                poParameter.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CPROPERTY_ID);
                _loggerLookup.LogInfo(string.Format("Get Parameter {0} on Controller", lcMethodName));
                _loggerLookup.LogDebug("DbParameter {@Parameter} ", poParameter);
                _loggerLookup.LogInfo(string.Format("Call Method {0} ", lcMethodName));

                loReturnTemp = loCls.GetTNC(poParameter);
                loRtn = GetStreaming(loReturnTemp);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _loggerLookup.LogError(ex);
            }
            loEx.ThrowExceptionIfErrors();
            _loggerLookup.LogInfo(string.Format("END process method {0} on Controller", lcMethodName));
            return loRtn!;
        }

        [HttpPost]
        public IAsyncEnumerable<LML01200DTO> LML01200InvoiceGroupList()
        {
            string lcMethodName = nameof(LML01200InvoiceGroupList);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _loggerLookup.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            var loEx = new R_Exception();
            IAsyncEnumerable<LML01200DTO> loRtn = null;
            List<LML01200DTO> loReturnTemp;

            try
            {
                var loCls = new PublicLookupLMCls();
                var poParameter = new LML01200ParameterDTO();
                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CPROPERTY_ID);
                poParameter.CINVGRP_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CINVGRP_CODE);
                poParameter.CACTIVE_TYPE = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CACTIVE_TYPE);
                poParameter.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;

                _loggerLookup.LogInfo(string.Format("Get Parameter {0} on Controller", lcMethodName));
                _loggerLookup.LogDebug("DbParameter {@Parameter} ", poParameter);
                _loggerLookup.LogInfo($"Call method {0}", lcMethodName);

                loReturnTemp = loCls.InvoiceGroup(poParameter);
                loRtn = GetStreaming(loReturnTemp);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _loggerLookup.LogError(ex);
            }
            loEx.ThrowExceptionIfErrors();
            _loggerLookup.LogInfo(string.Format("END process method {0} on Controller", lcMethodName));
            return loRtn!;
        }
        [HttpPost]
        public IAsyncEnumerable<LML01300DTO> LML01300LOIAgreementList()
        {
            string lcMethodName = nameof(LML01300LOIAgreementList);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _loggerLookup.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            var loEx = new R_Exception();
            IAsyncEnumerable<LML01300DTO> loRtn = null;
            List<LML01300DTO> loReturnTemp;

            try
            {
                var loCls = new PublicLookupLMCls();
                var poParameter = new LML01300ParameterDTO();
                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CPROPERTY_ID);
                poParameter.CDEPT_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CDEPT_CODE);
                poParameter.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;

                _loggerLookup.LogInfo(string.Format("Get Parameter {0} on Controller", lcMethodName));
                _loggerLookup.LogDebug("DbParameter {@Parameter} ", poParameter);
                _loggerLookup.LogInfo($"Call method {0}", lcMethodName);

                loReturnTemp = loCls.LOIAgreemnet(poParameter);
                loRtn = GetStreaming(loReturnTemp);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _loggerLookup.LogError(ex);
            }
            loEx.ThrowExceptionIfErrors();
            _loggerLookup.LogInfo(string.Format("END process method {0} on Controller", lcMethodName));
            return loRtn!;
        }




        [HttpPost]
        public LML00900InitialProcessDTO InitialProcess()
        {
            string lcMethodName = nameof(InitialProcess);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _loggerLookup.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));
            R_Exception loException = new R_Exception();
            LML00900InitialProcessDTO? loReturn = null;
            try
            {
                var loCls = new PublicLookupLMCls();
                string loDbParameter = R_BackGlobalVar.COMPANY_ID;
                _loggerLookup.LogInfo("Call method IntialProcess on Controller");
                loReturn = loCls.GetInitialProcess(loDbParameter);
            }
            catch (Exception ex)
            {
                loException.Add(loException);
                _loggerLookup.LogError(ex);
            }
            loException.ThrowExceptionIfErrors();
            _loggerLookup.LogInfo(string.Format("END process method {0} on Controller", lcMethodName));

            return loReturn!;
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