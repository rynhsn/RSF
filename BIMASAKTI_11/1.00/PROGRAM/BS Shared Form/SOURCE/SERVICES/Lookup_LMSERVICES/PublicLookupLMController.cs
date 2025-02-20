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
using Lookup_PMCOMMON.DTOs.LML01400;
using Lookup_PMCOMMON.DTOs.LML01500;
using Lookup_PMCOMMON.DTOs.LML01600;
using Lookup_PMCOMMON.DTOs.LML01700;
using Lookup_PMCOMMON.DTOs.LML01800;
using Lookup_PMCOMMON.DTOs.LML01900;
using Lookup_PMCOMMON.DTOs.UtilityDTO;

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
                poParameter.LACCRUAL = R_Utility.R_GetStreamingContext<bool>(ContextConstantPublicLookup.LACCRUAL);

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
                poParameter.LACTIVE = R_Utility.R_GetStreamingContext<bool>(ContextConstantPublicLookup.LACTIVE_ONLY);

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
                poParameter.CINV_PRD = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CINV_PRD);

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
                //CR -- 14 Nov 2024
                poParameter.CUSER_ID = R_BackGlobalVar.USER_ID;
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
                poParameter.CLEASE_MODE = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CLEASE_MODE);
                poParameter.CTENANT_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CTENANT_ID);

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
        public IAsyncEnumerable<LML01400DTO> LML01400AgreementUnitChargesList()
        {
            string lcMethodName = nameof(LML01400AgreementUnitChargesList);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _loggerLookup.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            var loEx = new R_Exception();
            IAsyncEnumerable<LML01400DTO> loRtn = null;
            List<LML01400DTO> loReturnTemp;

            try
            {
                var loCls = new PublicLookupLMCls();
                var poParameter = new LML01400ParameterDTO();
                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CPROPERTY_ID);
                poParameter.CDEPT_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CDEPT_CODE);
                poParameter.CTRANS_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CTRANS_CODE);
                poParameter.CREF_NO = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CREF_NO);
                poParameter.CSEQ_NO = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CSEQ_NO);
                poParameter.CUSER_ID = R_BackGlobalVar.USER_ID;

                _loggerLookup.LogInfo(string.Format("Get Parameter {0} on Controller", lcMethodName));
                _loggerLookup.LogDebug("DbParameter {@Parameter} ", poParameter);
                _loggerLookup.LogInfo($"Call method {0}", lcMethodName);

                loReturnTemp = loCls.AgreementUnitCharges(poParameter);
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
        public IAsyncEnumerable<LML01500DTO> LML01500SLACategoryList()
        {
            string lcMethodName = nameof(LML01500SLACategoryList);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _loggerLookup.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            var loEx = new R_Exception();
            IAsyncEnumerable<LML01500DTO> loRtn = null;
            List<LML01500DTO> loReturnTemp;

            try
            {
                var loCls = new PublicLookupLMCls();
                var poParameter = new LML01500ParameterDTO();
                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CPROPERTY_ID);
                poParameter.CCATEGORY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CCATEGORY_ID);

                _loggerLookup.LogInfo(string.Format("Get Parameter {0} on Controller", lcMethodName));
                _loggerLookup.LogDebug("DbParameter {@Parameter} ", poParameter);
                _loggerLookup.LogInfo($"Call method {0}", lcMethodName);

                loReturnTemp = loCls.SLACategory(poParameter);
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
        public IAsyncEnumerable<LML01600DTO> LML01600SLACallTypeList()
        {
            string lcMethodName = nameof(LML01600SLACallTypeList);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _loggerLookup.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            var loEx = new R_Exception();
            IAsyncEnumerable<LML01600DTO> loRtn = null;
            List<LML01600DTO> loReturnTemp;

            try
            {
                var loCls = new PublicLookupLMCls();
                var poParameter = new LML01600ParameterDTO();
                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CPROPERTY_ID);
                poParameter.CCALL_TYPE_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CCALL_TYPE_ID);

                _loggerLookup.LogInfo(string.Format("Get Parameter {0} on Controller", lcMethodName));
                _loggerLookup.LogDebug("DbParameter {@Parameter} ", poParameter);
                _loggerLookup.LogInfo($"Call method {0}", lcMethodName);

                loReturnTemp = loCls.SLACallType(poParameter);
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
        public IAsyncEnumerable<LML01700DTO> LML01700CancelReceiptFromCustomerList()
        {
            string lcMethodName = nameof(LML01700CancelReceiptFromCustomerList);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _loggerLookup.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            var loEx = new R_Exception();
            IAsyncEnumerable<LML01700DTO> loRtn = null;
            List<LML01700DTO> loReturnTemp;

            try
            {
                var loCls = new PublicLookupLMCls();
                var poParameter = new LML01700ParameterDTO();
                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CPROPERTY_ID);
                poParameter.CUSER_ID = R_BackGlobalVar.USER_ID;
                poParameter.CPERIOD = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CPERIOD);
                poParameter.CCUSTOMER_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CCUSTOMER_ID);
                poParameter.CREC_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CREC_ID);
                poParameter.CDEPT_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CDEPT_CODE);
                poParameter.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;

                _loggerLookup.LogInfo(string.Format("Get Parameter {0} on Controller", lcMethodName));
                _loggerLookup.LogDebug("DbParameter {@Parameter} ", poParameter);
                _loggerLookup.LogInfo($"Call method {0}", lcMethodName);

                loReturnTemp = loCls.LML01700CancelReceiptFromCustomer(poParameter);
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

        public IAsyncEnumerable<LML01700DTO> LML01700PrerequisiteCustReceiptList()
        {
            string lcMethodName = nameof(LML01700PrerequisiteCustReceiptList);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _loggerLookup.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            var loEx = new R_Exception();
            IAsyncEnumerable<LML01700DTO> loRtn = null;
            List<LML01700DTO> loReturnTemp;

            try
            {
                var loCls = new PublicLookupLMCls();
                var poParameter = new LML01700ParameterDTO();
                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CRECEIPT_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CRECEIPT_ID);
                poParameter.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;

                _loggerLookup.LogInfo(string.Format("Get Parameter {0} on Controller", lcMethodName));
                _loggerLookup.LogDebug("DbParameter {@Parameter} ", poParameter);
                _loggerLookup.LogInfo($"Call method {0}", lcMethodName);

                loReturnTemp = loCls.LML01700PrerequisiteCustReceipt(poParameter);
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
        [HttpPost]
        public IAsyncEnumerable<LML01800DTO> LML01800UnitTenantList()
        {
            string lcMethodName = nameof(LML01800UnitTenantList);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _loggerLookup.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            var loEx = new R_Exception();
            IAsyncEnumerable<LML01800DTO> loRtn = null;
            List<LML01800DTO> loReturnTemp;

            try
            {
                var loCls = new PublicLookupLMCls();
                var poParameter = new LML01800ParameterDTO();
                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CPROPERTY_ID);
                poParameter.CBUILDING_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CBUILDING_ID);
                poParameter.CFLOOR_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CFLOOR_ID);
                poParameter.CUNIT_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CUNIT_ID);
                poParameter.CTENANT_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CTENANT_ID);
                poParameter.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;
                poParameter.CUSER_ID = R_BackGlobalVar.USER_ID;

                _loggerLookup.LogInfo(string.Format("Get Parameter {0} on Controller", lcMethodName));
                _loggerLookup.LogDebug("DbParameter {@Parameter} ", poParameter);
                _loggerLookup.LogInfo($"Call method {0}", lcMethodName);

                loReturnTemp = loCls.LML01800UnitTenant(poParameter);
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
        public IAsyncEnumerable<LML01900DTO> LML01900StaffList()
        {
            string lcMethodName = nameof(LML01900StaffList);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _loggerLookup.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            var loEx = new R_Exception();
            IAsyncEnumerable<LML01900DTO> loRtn = null;
            List<LML01900DTO> loReturnTemp;

            try
            {
                var loCls = new PublicLookupLMCls();
                var poParameter = new LML01900ParamaterDTO();
                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CPROPERTY_ID);
                poParameter.CSUPERVISOR_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CSUPERVISOR_ID);
                poParameter.LSUPERVISOR = R_Utility.R_GetStreamingContext<bool>(ContextConstantPublicLookup.LSUPERVISOR);
                poParameter.CDEPT_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CDEPT_CODE);
                poParameter.CACTIVE_INACTIVE = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CACTIVE_INACTIVE);
                poParameter.CSTAFF_TYPE_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CSTAFF_TYPE);
                poParameter.CSTAFF_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CSTAFF_ID);
                poParameter.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;
                poParameter.CUSER_ID = R_BackGlobalVar.USER_ID;

                _loggerLookup.LogInfo(string.Format("Get Parameter {0} on Controller", lcMethodName));
                _loggerLookup.LogDebug("DbParameter {@Parameter} ", poParameter);
                _loggerLookup.LogInfo($"Call method {0}", lcMethodName);

                loReturnTemp = loCls.LML01900Staff(poParameter);
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
        public IAsyncEnumerable<PropertyDTO> PropertyList()
        {
            string lcMethodName = nameof(PropertyList);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _loggerLookup.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            var loEx = new R_Exception();
            IAsyncEnumerable<PropertyDTO> loRtn = null;
            List<PropertyDTO> loReturnTemp;

            try
            {
                var loCls = new PublicLookupLMCls();
                var poParameter = new ParameterBaseDTO();
                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CUSER_ID = R_BackGlobalVar.USER_ID;

                _loggerLookup.LogInfo(string.Format("Get Parameter {0} on Controller", lcMethodName));
                _loggerLookup.LogDebug("DbParameter {@Parameter} ", poParameter);
                _loggerLookup.LogInfo($"Call method {0}", lcMethodName);

                loReturnTemp = loCls.PropertyList(poParameter);
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
        public IAsyncEnumerable<BuildingDTO> BuildingList()
        {
            string lcMethodName = nameof(BuildingList);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _loggerLookup.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            var loEx = new R_Exception();
            IAsyncEnumerable<BuildingDTO> loRtn = null;
            List<BuildingDTO> loReturnTemp;

            try
            {
                var loCls = new PublicLookupLMCls();
                var poParameter = new ParameterBaseDTO();
                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CPROPERTY_ID);
                poParameter.CUSER_ID = R_BackGlobalVar.USER_ID;

                _loggerLookup.LogInfo(string.Format("Get Parameter {0} on Controller", lcMethodName));
                _loggerLookup.LogDebug("DbParameter {@Parameter} ", poParameter);
                _loggerLookup.LogInfo($"Call method {0}", lcMethodName);

                loReturnTemp = loCls.BuildingList(poParameter);
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
        public IAsyncEnumerable<FloorDTO> FloorList()
        {
            string lcMethodName = nameof(FloorList);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _loggerLookup.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            var loEx = new R_Exception();
            IAsyncEnumerable<FloorDTO> loRtn = null;
            List<FloorDTO> loReturnTemp;

            try
            {
                var loCls = new PublicLookupLMCls();
                var poParameter = new ParameterBaseDTO();
                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CPROPERTY_ID);
                poParameter.CBUILDING_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CBUILDING_ID);
                poParameter.CUSER_ID = R_BackGlobalVar.USER_ID;

                _loggerLookup.LogInfo(string.Format("Get Parameter {0} on Controller", lcMethodName));
                _loggerLookup.LogDebug("DbParameter {@Parameter} ", poParameter);
                _loggerLookup.LogInfo($"Call method {0}", lcMethodName);

                loReturnTemp = loCls.FloorList(poParameter);
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
        public IAsyncEnumerable<UnitDTO> UnitList()
        {
            string lcMethodName = nameof(UnitList);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _loggerLookup.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            var loEx = new R_Exception();
            IAsyncEnumerable<UnitDTO> loRtn = null;
            List<UnitDTO> loReturnTemp;

            try
            {
                var loCls = new PublicLookupLMCls();
                var poParameter = new ParameterBaseDTO();
                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CPROPERTY_ID);
                poParameter.CBUILDING_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CBUILDING_ID);
                poParameter.CFLOOR_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CFLOOR_ID);
                poParameter.CUSER_ID = R_BackGlobalVar.USER_ID;

                _loggerLookup.LogInfo(string.Format("Get Parameter {0} on Controller", lcMethodName));
                _loggerLookup.LogDebug("DbParameter {@Parameter} ", poParameter);
                _loggerLookup.LogInfo($"Call method {0}", lcMethodName);

                loReturnTemp = loCls.UnitList(poParameter);
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
        public IAsyncEnumerable<DepartmentDTO> DepartmentList()
        {
            string lcMethodName = nameof(DepartmentList);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _loggerLookup.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            var loEx = new R_Exception();
            IAsyncEnumerable<DepartmentDTO> loRtn = null;
            List<DepartmentDTO> loReturnTemp;

            try
            {
                var loCls = new PublicLookupLMCls();
                var poParameter = new ParameterBaseDTO();
                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CUSER_ID = R_BackGlobalVar.USER_ID;

                _loggerLookup.LogInfo(string.Format("Get Parameter {0} on Controller", lcMethodName));
                _loggerLookup.LogDebug("DbParameter {@Parameter} ", poParameter);
                _loggerLookup.LogInfo($"Call method {0}", lcMethodName);

                loReturnTemp = loCls.DepartmentList(poParameter);
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
        public IAsyncEnumerable<SupervisorDTO> SupervisorList()
        {
            string lcMethodName = nameof(SupervisorList);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _loggerLookup.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            var loEx = new R_Exception();
            IAsyncEnumerable<SupervisorDTO> loRtn = null;
            List<SupervisorDTO> loReturnTemp;

            try
            {
                var loCls = new PublicLookupLMCls();
                var poParameter = new ParameterBaseDTO();
                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CPROPERTY_ID);
                poParameter.CUSER_ID = R_BackGlobalVar.USER_ID;

                _loggerLookup.LogInfo(string.Format("Get Parameter {0} on Controller", lcMethodName));
                _loggerLookup.LogDebug("DbParameter {@Parameter} ", poParameter);
                _loggerLookup.LogInfo($"Call method {0}", lcMethodName);

                loReturnTemp = loCls.SupervisorList(poParameter);
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
        public IAsyncEnumerable<StaffTypeDTO> StaffTypeList()
        {
            string lcMethodName = nameof(StaffTypeList);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _loggerLookup.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            var loEx = new R_Exception();
            IAsyncEnumerable<StaffTypeDTO> loRtn = null;
            List<StaffTypeDTO> loReturnTemp;

            try
            {
                var loCls = new PublicLookupLMCls();
                var poParameter = new ParameterBaseDTO();
                poParameter.CAPPLICATION = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CAPPLICATION);
                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CCLASS_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CCLASS_ID);
                poParameter.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;

                _loggerLookup.LogInfo(string.Format("Get Parameter {0} on Controller", lcMethodName));
                _loggerLookup.LogDebug("DbParameter {@Parameter} ", poParameter);
                _loggerLookup.LogInfo($"Call method {0}", lcMethodName);

                loReturnTemp = loCls.StaffTypeList(poParameter);
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