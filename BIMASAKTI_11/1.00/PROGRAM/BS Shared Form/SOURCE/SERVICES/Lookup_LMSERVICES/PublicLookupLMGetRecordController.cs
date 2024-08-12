using Lookup_PMBACK;
using Lookup_PMCOMMON;
using Lookup_PMCOMMON.DTOs;
using Lookup_PMCOMMON.DTOs.LML01000;
using Lookup_PMCOMMON.DTOs.LML01100;
using Lookup_PMCOMMON.DTOs.LML01300;
using Lookup_PMCOMMON.Logs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lookup_PMSERVICES
{
    [ApiController]
    [Route("api/[controller]/[action]"), AllowAnonymous]
    public class PublicLookupLMGetRecordController : ControllerBase, IGetRecordLookupLM
    {
        private LoggerLookupLM _loggerLookup;
        private readonly ActivitySource _activitySource;

        public PublicLookupLMGetRecordController(ILogger<LoggerLookupLM> logger)
        {
            //Initial and Get Logger
            LoggerLookupLM.R_InitializeLogger(logger);
            _loggerLookup = LoggerLookupLM.R_GetInstanceLogger();
            _activitySource = LookupLMActivity.R_InitializeAndGetActivitySource(nameof(PublicLookupLMGetRecordController));
        }

        [HttpPost]
        public LMLGenericRecord<LML00200DTO> LML00200UnitCharges(LML00200ParameterDTO poParam)
        {
            string lcMethodName = nameof(LML00200UnitCharges);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _loggerLookup.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            var loEx = new R_Exception();
            LMLGenericRecord<LML00200DTO> loReturn = new();
            try
            {
                var loCls = new PublicLookupLMCls();
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CUSER_ID = R_BackGlobalVar.USER_ID;
                _loggerLookup.LogInfo("Call method GetUnitCharges");
                var loTempList = loCls.GetAllUnitCharges(poParam);

                _loggerLookup.LogInfo("Filter Search by text Unit Charges");
#pragma warning disable CS8601 // Possible null reference assignment.
                loReturn.Data = loTempList.Find(x => x.CCHARGES_ID!.Equals(poParam.CSEARCH_TEXT!.Trim(), StringComparison.OrdinalIgnoreCase));
#pragma warning restore CS8601 // Possible null reference assignment.

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
        public LMLGenericRecord<LML00300DTO> LML00300Supervisor(LML00300ParameterDTO poParam)
        {
            string lcMethodName = nameof(LML00300Supervisor);
            using Activity activity = _activitySource.StartActivity(lcMethodName);
            _loggerLookup.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            var loEx = new R_Exception();
            LMLGenericRecord<LML00300DTO> loReturn = new();
            try
            {
                var loCls = new PublicLookupLMCls();
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CUSER_ID = R_BackGlobalVar.USER_ID;
                _loggerLookup.LogInfo("Call method GetSupervisor");
                var loTempList = loCls.GetAllSupervisor(poParam);

                _loggerLookup.LogInfo("Filter Search by text Supervisor");
#pragma warning disable CS8601 // Possible null reference assignment.
                loReturn.Data = loTempList.Find(x => x.CSUPERVISOR.Equals(poParam.CSEARCH_TEXT.Trim(), StringComparison.OrdinalIgnoreCase));

                //    loReturn.Data = loTempList.Find(x => x.CSUPERVISOR == poParam.CSEARCH_TEXT.Trim());
#pragma warning restore CS8601 // Possible null reference assignment.

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
        public LMLGenericRecord<LML00400DTO> LML00400UtilityCharges(LML00400ParameterDTO poParam)
        {
            string lcMethodName = nameof(LML00400UtilityCharges);
            using Activity activity = _activitySource.StartActivity(lcMethodName);
            _loggerLookup.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            var loEx = new R_Exception();
            LMLGenericRecord<LML00400DTO> loReturn = new();
            try
            {
                var loCls = new PublicLookupLMCls();
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CUSER_ID = R_BackGlobalVar.USER_ID;
                _loggerLookup.LogInfo("Call method GetUtilityCharges");
                var loTempList = loCls.GetAllUtilityCharges(poParam);

                _loggerLookup.LogInfo("Filter Search by text Utility Charges");
#pragma warning disable CS8601 // Possible null reference assignment.

                loReturn.Data = loTempList.Find(x => x.CCHARGES_ID.Equals(poParam.CSEARCH_TEXT.Trim(), StringComparison.OrdinalIgnoreCase));
#pragma warning restore CS8601 // Possible null reference assignment.

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
        public LMLGenericRecord<LML00500DTO> LML00500Salesman(LML00500ParameterDTO poParam)
        {

            string lcMethodName = nameof(LML00500Salesman);
            using Activity activity = _activitySource.StartActivity(lcMethodName);
            _loggerLookup.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            var loEx = new R_Exception();
            LMLGenericRecord<LML00500DTO> loReturn = new();
            try
            {
                var loCls = new PublicLookupLMCls();
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CUSER_ID = R_BackGlobalVar.USER_ID;
                _loggerLookup.LogInfo("Call method GetSalesman");
                var loTempList = loCls.GetAllSalesman(poParam);

                _loggerLookup.LogInfo("Filter Search by text Salesman");
#pragma warning disable CS8601 // Possible null reference assignment.
                loReturn.Data = loTempList.Find(x => x.CSALESMAN_ID.Equals(poParam.CSEARCH_TEXT.Trim(), StringComparison.OrdinalIgnoreCase));
#pragma warning restore CS8601 // Possible null reference assignment.

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
        public LMLGenericRecord<LML00600DTO> LML00600Tenant(LML00600ParameterDTO poParam)
        {

            string lcMethodName = nameof(LML00600Tenant);
            using Activity activity = _activitySource.StartActivity(lcMethodName);
            _loggerLookup.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            var loEx = new R_Exception();
            LMLGenericRecord<LML00600DTO> loReturn = new();
            try
            {
                var loCls = new PublicLookupLMCls();
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CUSER_ID = R_BackGlobalVar.USER_ID;
                _loggerLookup.LogInfo("Call method GetAllTenant");
                var loTempList = loCls.GetTenant(poParam);

                _loggerLookup.LogInfo("Filter Search by text Tenant");
#pragma warning disable CS8601 // Possible null reference assignment.
                loReturn.Data = loTempList.Find(x => x.CTENANT_ID.Equals(poParam.CSEARCH_TEXT.Trim(), StringComparison.OrdinalIgnoreCase));
#pragma warning restore CS8601 // Possible null reference assignment.

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
        public LMLGenericRecord<LML00700DTO> LML00700Discount(LML00700ParameterDTO poParam)
        {
            string lcMethodName = nameof(LML00700Discount);
            using Activity activity = _activitySource.StartActivity(lcMethodName);
            _loggerLookup.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            var loEx = new R_Exception();
            LMLGenericRecord<LML00700DTO> loReturn = new();
            try
            {
                var loCls = new PublicLookupLMCls();
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CUSER_ID = R_BackGlobalVar.USER_ID;
                _loggerLookup.LogInfo("Call method GetAllDiscount");
                var loTempList = loCls.GetDiscount(poParam);

                _loggerLookup.LogInfo("Filter Search by text Discount");
#pragma warning disable CS8601 // Possible null reference assignment.
                loReturn.Data = loTempList.Find(x => x.CDISCOUNT_CODE.Equals(poParam.CSEARCH_TEXT.Trim(), StringComparison.OrdinalIgnoreCase));
#pragma warning restore CS8601 // Possible null reference assignment.

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
        public LMLGenericRecord<LML00800DTO> LML00800Agreement(LML00800ParameterDTO poParam)
        {
            string lcMethodName = nameof(LML00800Agreement);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _loggerLookup.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            var loEx = new R_Exception();
            LMLGenericRecord<LML00800DTO> loReturn = new();
            try
            {
                var loCls = new PublicLookupLMCls();
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CLANG_ID = R_BackGlobalVar.CULTURE;
                _loggerLookup.LogInfo("Call method GetAllAgreement");
                var loTempList = loCls.GetAgreement(poParam);

                _loggerLookup.LogInfo("Filter Search by text Agreement");
#pragma warning disable CS8601 // Possible null reference assignment.
                loReturn.Data = loTempList.Find(x => x.CREF_NO!.Equals(poParam.CSEARCH_TEXT!.Trim(), StringComparison.OrdinalIgnoreCase));
#pragma warning restore CS8601 // Possible null reference assignment.

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
        public LMLGenericRecord<LML00900DTO> LML00900Transaction(LML00900ParameterDTO poParam)
        {
            string lcMethodName = nameof(LML00900Transaction);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _loggerLookup.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            var loEx = new R_Exception();
            LMLGenericRecord<LML00900DTO> loReturn = new();
            try
            {
                var loCls = new PublicLookupLMCls();
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CUSER_ID = R_BackGlobalVar.USER_ID;
                poParam.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;
                _loggerLookup.LogInfo($"Call method {0}", lcMethodName);
                var loTempList = loCls.GetTransaction(poParam);

                _loggerLookup.LogInfo("Filter Search by text Ref NO");
#pragma warning disable CS8601 // Possible null reference assignment.
                loReturn.Data = loTempList.Find(x => x.CREF_NO!.Equals(poParam.CSEARCH_TEXT!.Trim(), StringComparison.OrdinalIgnoreCase));
#pragma warning restore CS8601 // Possible null reference assignment.

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
        public LMLGenericRecord<LML01000DTO> LML01000BillingRule(LML01000ParameterDTO poParam)
        {
            string lcMethodName = nameof(LML01000BillingRule);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _loggerLookup.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            var loEx = new R_Exception();
            LMLGenericRecord<LML01000DTO> loReturn = new();
            try
            {
                var loCls = new PublicLookupLMCls();
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CUSER_ID = R_BackGlobalVar.USER_ID;
                _loggerLookup.LogInfo(string.Format("Call Method {0} ", lcMethodName));
                var loTempList = loCls.GetBillingRule(poParam);

                _loggerLookup.LogInfo("Searching with input form user");
#pragma warning disable CS8601 // Possible null reference assignment.
                loReturn.Data = loTempList.Find(x => x.CBILLING_RULE_CODE!.Equals(poParam.CSEARCH_TEXT!.Trim(), StringComparison.OrdinalIgnoreCase));
#pragma warning restore CS8601 // Possible null reference assignment.

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
        public LMLGenericRecord<LML01100DTO> LML01100TNC(LML01100ParameterDTO poParam)
        {
            string lcMethodName = nameof(LML01100TNC);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _loggerLookup.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            var loEx = new R_Exception();
            LMLGenericRecord<LML01100DTO> loReturn = new();
            try
            {
                var loCls = new PublicLookupLMCls();
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CUSER_ID = R_BackGlobalVar.USER_ID;
                _loggerLookup.LogInfo(string.Format("Call Method {0} ", lcMethodName));
                var loTempList = loCls.GetTNC(poParam);

                _loggerLookup.LogInfo("Searching with input form user");
#pragma warning disable CS8601 // Possible null reference assignment.
                loReturn.Data = loTempList.Find(x => x.CTC_CODE!.Equals(poParam.CSEARCH_TEXT!.Trim(), StringComparison.OrdinalIgnoreCase));
#pragma warning restore CS8601 // Possible null reference assignment.

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
        public LMLGenericRecord<LML01200DTO> LML01200InvoiceGroup(LML01200ParameterDTO poParam)
        {
            string lcMethodName = nameof(LML01200InvoiceGroup);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _loggerLookup.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            var loEx = new R_Exception();
            LMLGenericRecord<LML01200DTO> loReturn = new();
            try
            {
                var loCls = new PublicLookupLMCls();
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;
                _loggerLookup.LogInfo($"Call method {0}", lcMethodName);
                var loTempList = loCls.InvoiceGroup(poParam);

                _loggerLookup.LogInfo("Filter Search by text InvoiceGroupCode");
#pragma warning disable CS8601 // Possible null reference assignment.
                loReturn.Data = loTempList.Find(x => x.CINVGRP_CODE!.Equals(poParam.CSEARCH_TEXT!.Trim(), StringComparison.OrdinalIgnoreCase));
#pragma warning restore CS8601 // Possible null reference assignment.

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
        public LMLGenericRecord<LML01300DTO> LML01300LOIAgreement(LML01300ParameterDTO poParam)
        {
            string lcMethodName = nameof(LML01200InvoiceGroup);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _loggerLookup.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            var loEx = new R_Exception();
            LMLGenericRecord<LML01300DTO> loReturn = new();
            try
            {
                var loCls = new PublicLookupLMCls();
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;
                _loggerLookup.LogInfo($"Call method {0}", lcMethodName);
                var loTempList = loCls.LOIAgreemnet(poParam);

                _loggerLookup.LogInfo("Filter Search by text ID");
#pragma warning disable CS8601 // Possible null reference assignment.
                loReturn.Data = loTempList.Find(x => x.CREF_NO!.Equals(poParam.CSEARCH_TEXT!.Trim(), StringComparison.OrdinalIgnoreCase));
#pragma warning restore CS8601 // Possible null reference assignment.

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
