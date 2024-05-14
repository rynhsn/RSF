using Lookup_GSCOMMON;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSCOMMON.Loggers;
using Lookup_GSLBACK;
using Microsoft.AspNetCore.Authorization;
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

namespace Lookup_GSSERVICES
{
    [ApiController]
    [Route("api/[controller]/[action]"), AllowAnonymous]
    public class PublicLookupGSRecordController : ControllerBase, IPublicRecordLookup
    {
        private LoggerPublicLookup _Logger;
        private readonly ActivitySource _activitySource;

        public PublicLookupGSRecordController(ILogger<LoggerPublicLookup> logger)
        {
            //Initial and Get Logger
            LoggerPublicLookup.R_InitializeLogger(logger);
            _Logger = LoggerPublicLookup.R_GetInstanceLogger();
            _activitySource = PublicLookupGSActivitySourceBase.R_InitializeAndGetActivitySource(nameof(PublicLookupGSRecordController));
        }

        [HttpPost]
        public GSLGenericRecord<GSL00100DTO> GSL00100GetSalesTax(GSL00100ParameterDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("GSL00100GetSalesTax");
            var loEx = new R_Exception();
            GSLGenericRecord<GSL00100DTO> loRtn = new();
            _Logger.LogInfo("Start GSL00100GetSalesTax");

            try
            {
                var loCls = new PublicLookupCls();

                _Logger.LogInfo("Call Back Method GetALLSalesTax");
                var loResult = loCls.GetALLSalesTax();

                _Logger.LogInfo("Filter Search by text GSL00100GetSalesTax");
                loRtn.Data = loResult.Find(x => x.CTAX_ID.Trim().ToUpper() == poEntity.CSEARCH_TEXT.ToUpper().Trim());
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            _Logger.LogInfo("End GSL00100GetSalesTax");
            return loRtn;
        }

        [HttpPost]
        public GSLGenericRecord<GSL00110DTO> GSL00110GetTaxByDate(GSL00110ParameterDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("GSL00110GetTaxByDate");
            var loEx = new R_Exception();
            GSLGenericRecord<GSL00110DTO> loRtn = new();
            _Logger.LogInfo("Start GSL00110GetTaxByDate");

            try
            {
                var loCls = new PublicLookupCls();

                _Logger.LogInfo("Call Back Method GetALLTaxByDate");
                var loResult = loCls.GetALLTaxByDate(poEntity);

                _Logger.LogInfo("Filter Search by text GSL00110GetTaxByDate");
                loRtn.Data = loResult.Find(x => x.CTAX_ID.Trim().ToUpper() == poEntity.CSEARCH_TEXT.ToUpper().Trim());
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            _Logger.LogInfo("End GSL00110GetTaxByDate");
            return loRtn;
        }

        [HttpPost]
        public GSLGenericRecord<GSL00200DTO> GSL00200GetWithholdingTax(GSL00200ParameterDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("GSL00200GetWithholdingTax");
            var loEx = new R_Exception();
            GSLGenericRecord<GSL00200DTO> loRtn = new();
            _Logger.LogInfo("Start GSL00200GetWithholdingTax");

            try
            {
                var loCls = new PublicLookupCls();

                _Logger.LogInfo("Call Back Method GetALLWithholdingTax");
                var loResult = loCls.GetALLWithholdingTax(poEntity);

                _Logger.LogInfo("Filter Search by text GSL00200GetWithholdingTax");
                loRtn.Data = loResult.Find(x => x.CTAX_ID.Trim().ToUpper() == poEntity.CSEARCH_TEXT.ToUpper().Trim());
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            _Logger.LogInfo("End GSL00200GetWithholdingTax");
            return loRtn;
        }

        [HttpPost]
        public GSLGenericRecord<GSL00300DTO> GSL00300GetCurrency(GSL00300ParameterDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("GSL00300GetCurrency");
            var loEx = new R_Exception();
            GSLGenericRecord<GSL00300DTO> loRtn = new();
            _Logger.LogInfo("Start GSL00300GetCurrency");

            try
            {
                var loCls = new PublicLookupCls();

                _Logger.LogInfo("Call Back Method GetALLCurrency");
                var loResult = loCls.GetALLCurrency();

                _Logger.LogInfo("Filter Search by text GSL00300GetCurrency");
                loRtn.Data = loResult.Find(x => x.CCURRENCY_CODE.Trim().ToUpper() == poEntity.CSEARCH_TEXT.ToUpper().Trim());
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            _Logger.LogInfo("End GSL00300GetCurrency");
            return loRtn;
        }

        [HttpPost]
        public GSLGenericRecord<GSL00400DTO> GSL00400GetJournalGroup(GSL00400ParameterDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("GSL00400GetJournalGroup");
            var loEx = new R_Exception();
            GSLGenericRecord<GSL00400DTO> loRtn = new();
            _Logger.LogInfo("Start GSL00400GetJournalGroup");

            try
            {
                var loCls = new PublicLookupCls();

                _Logger.LogInfo("Call Back Method GetALLJournalGroup");
                var loResult = loCls.GetALLJournalGroup(poEntity);

                _Logger.LogInfo("Filter Search by text GSL00400GetJournalGroup");
                loRtn.Data = loResult.Find(x => x.CJRNGRP_CODE.Trim().ToUpper() == poEntity.CSEARCH_TEXT.ToUpper().Trim());
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            _Logger.LogInfo("End GSL00400GetJournalGroup");
            return loRtn;
        }

        [HttpPost]
        public GSLGenericRecord<GSL00500DTO> GSL00500GetGLAccount(GSL00500ParameterDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("GSL00500GetGLAccount");
            var loEx = new R_Exception();
            GSLGenericRecord<GSL00500DTO> loRtn = new();
            _Logger.LogInfo("Start GSL00500GetGLAccount");

            try
            {
                var loCls = new PublicLookupCls();

                _Logger.LogInfo("Call Back Method GetALLGLAccount");
                var loResult = loCls.GetALLGLAccount(poEntity);

                _Logger.LogInfo("Filter Search by text GSL00500GetGLAccount");
                loRtn.Data = loResult.Find(x => x.CGLACCOUNT_NO.Trim().ToUpper() == poEntity.CSEARCH_TEXT.ToUpper().Trim());
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            _Logger.LogInfo("End GSL00500GetGLAccount");
            return loRtn;
        }

        [HttpPost]
        public GSLGenericRecord<GSL00510DTO> GSL00510GetCOA(GSL00510ParameterDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("GSL00510GetCOA");
            var loEx = new R_Exception();
            GSLGenericRecord<GSL00510DTO> loRtn = new();
            _Logger.LogInfo("Start GSL00510GetCOA");

            try
            {
                var loCls = new PublicLookupCls();

                _Logger.LogInfo("Call Back Method GetALLCOA");
                var loResult = loCls.GetALLCOA(poEntity);

                _Logger.LogInfo("Filter Search by text GSL00510GetCOA");
                loRtn.Data = loResult.Find(x => x.CGLACCOUNT_NO.Trim().ToUpper() == poEntity.CSEARCH_TEXT.ToUpper().Trim());
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            _Logger.LogInfo("End GSL00510GetCOA");
            return loRtn;
        }

        [HttpPost]
        public GSLGenericRecord<GSL00520DTO> GSL00520GetGOACOA(GSL00520ParameterDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("GSL00520GetGOACOA");
            var loEx = new R_Exception();
            GSLGenericRecord<GSL00520DTO> loRtn = new();
            _Logger.LogInfo("Start GSL00520GetGOACOA");

            try
            {
                var loCls = new PublicLookupCls();

                _Logger.LogInfo("Call Back Method GetALLGOACOA");
                var loResult = loCls.GetALLGOACOA(poEntity);

                _Logger.LogInfo("Filter Search by text GSL00520GetGOACOA");
                loRtn.Data = loResult.Find(x => x.CGLACCOUNT_NO.Trim().ToUpper() == poEntity.CSEARCH_TEXT.ToUpper().Trim());
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            _Logger.LogInfo("End GSL00520GetGOACOA");
            return loRtn;
        }

        [HttpPost]
        public GSLGenericRecord<GSL00550DTO> GSL00550GetGOA(GSL00550ParameterDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("GSL00550GetGOA");
            var loEx = new R_Exception();
            GSLGenericRecord<GSL00550DTO> loRtn = new();
            _Logger.LogInfo("Start GSL00550GetGOA");

            try
            {
                var loCls = new PublicLookupCls();

                _Logger.LogInfo("Call Back Method GetALLGOA");
                var loResult = loCls.GetALLGOA();

                _Logger.LogInfo("Filter Search by text GSL00550GetGOA");
                loRtn.Data = loResult.Find(x => x.CGOA_CODE.Trim().ToUpper() == poEntity.CSEARCH_TEXT.ToUpper().Trim());
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            _Logger.LogInfo("End GSL00550GetGOA");
            return loRtn;
        }

        [HttpPost]
        public GSLGenericRecord<GSL00600DTO> GSL00600GetUnitTypeCategory(GSL00600ParameterDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("GSL00600GetUnitTypeCategory");
            var loEx = new R_Exception();
            GSLGenericRecord<GSL00600DTO> loRtn = new();
            _Logger.LogInfo("Start GSL00600GetUnitTypeCategory");

            try
            {
                var loCls = new PublicLookupCls();

                _Logger.LogInfo("Call Back Method GetALLUnitTypeCategory");
                var loResult = loCls.GetALLUnitTypeCategory(poEntity);

                _Logger.LogInfo("Filter Search by text GSL00600GetUnitTypeCategory");
                loRtn.Data = loResult.Find(x => x.CUNIT_TYPE_CATEGORY_ID.Trim().ToUpper() == poEntity.CSEARCH_TEXT.ToUpper().Trim());
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            _Logger.LogInfo("End GSL00600GetUnitTypeCategory");
            return loRtn;
        }

        [HttpPost]
        public GSLGenericRecord<GSL00700DTO> GSL00700GetDepartment(GSL00700ParameterDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("GSL00700GetDepartment");
            var loEx = new R_Exception();
            GSLGenericRecord<GSL00700DTO> loRtn = new();
            _Logger.LogInfo("Start GSL00700GetDepartment");

            try
            {
                var loCls = new PublicLookupCls();

                _Logger.LogInfo("Call Back Method GetALLDepartment");
                var loResult = loCls.GetALLDepartment(poEntity);

                _Logger.LogInfo("Filter Search by text GSL00700GetDepartment");
                loRtn.Data = loResult.Find(x => x.CDEPT_CODE.Trim().ToUpper() == poEntity.CSEARCH_TEXT.ToUpper().Trim());
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            _Logger.LogInfo("End GSL00700GetDepartment");
            return loRtn;
        }

        [HttpPost]
        public GSLGenericRecord<GSL00710DTO> GSL00710GetDepartmentProperty(GSL00710ParameterDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("GSL00710GetDepartmentProperty");
            var loEx = new R_Exception();
            GSLGenericRecord<GSL00710DTO> loRtn = new();
            _Logger.LogInfo("Start GSL00710GetDepartmentProperty");

            try
            {
                var loCls = new PublicLookupCls();

                _Logger.LogInfo("Call Back Method GetALLDepartmentProperty");
                var loResult = loCls.GetALLDepartmentProperty(poEntity);

                _Logger.LogInfo("Filter Search by text GSL00710GetDepartmentProperty");
                loRtn.Data = loResult.Find(x => x.CDEPT_CODE.Trim().ToUpper() == poEntity.CSEARCH_TEXT.ToUpper().Trim());
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            _Logger.LogInfo("End GSL00710GetDepartmentProperty");
            return loRtn;
        }

        [HttpPost]
        public GSLGenericRecord<GSL00800DTO> GSL00800GetCurrencyType(GSL00800ParameterDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("GSL00800GetCurrencyType");
            var loEx = new R_Exception();
            GSLGenericRecord<GSL00800DTO> loRtn = new();
            _Logger.LogInfo("Start GSL00800GetCurrencyType");

            try
            {
                var loCls = new PublicLookupCls();

                _Logger.LogInfo("Call Back Method GetALLCurrencyRateType");
                var loResult = loCls.GetALLCurrencyRateType();

                _Logger.LogInfo("Filter Search by text GSL00800GetCurrencyType");
                loRtn.Data = loResult.Find(x => x.CRATETYPE_CODE.Trim().ToUpper() == poEntity.CSEARCH_TEXT.ToUpper().Trim());
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            _Logger.LogInfo("End GSL00800GetCurrencyType");
            return loRtn;
        }

        [HttpPost]
        public GSLGenericRecord<GSL00900DTO> GSL00900GetCenter(GSL00900ParameterDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("GSL00900GetCenter");
            var loEx = new R_Exception();
            GSLGenericRecord<GSL00900DTO> loRtn = new();
            _Logger.LogInfo("Start GSL00900GetCenter");

            try
            {
                var loCls = new PublicLookupCls();

                _Logger.LogInfo("Call Back Method GetALLCenter");
                var loResult = loCls.GetALLCenter();

                _Logger.LogInfo("Filter Search by text GSL00900GetCenter");
                loRtn.Data = loResult.Find(x => x.CCENTER_CODE.Trim().ToUpper() == poEntity.CSEARCH_TEXT.ToUpper().Trim());
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            _Logger.LogInfo("End GSL00900GetCenter");
            return loRtn;
        }

        [HttpPost]
        public GSLGenericRecord<GSL01000DTO> GSL01000GetUser(GSL01000ParameterDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("GSL01000GetUser");
            var loEx = new R_Exception();
            GSLGenericRecord<GSL01000DTO> loRtn = new();
            _Logger.LogInfo("Start GSL01000GetUser");

            try
            {
                var loCls = new PublicLookupCls();

                _Logger.LogInfo("Call Back Method GetALLUser");
                var loResult = loCls.GetALLUser();

                _Logger.LogInfo("Filter Search by text GSL01000GetUser");
                loRtn.Data = loResult.Find(x => x.CUSER_ID.Trim().ToUpper() == poEntity.CSEARCH_TEXT.ToUpper().Trim());
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            _Logger.LogInfo("End GSL01000GetUser");
            return loRtn;
        }

        [HttpPost]
        public GSLGenericRecord<GSL01100DTO> GSL01100GetUserApproval(GSL01100ParameterDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("GSL01100GetUserApproval");
            var loEx = new R_Exception();
            GSLGenericRecord<GSL01100DTO> loRtn = new();
            _Logger.LogInfo("Start GSL01100GetUserApproval");

            try
            {
                var loCls = new PublicLookupCls();

                _Logger.LogInfo("Call Back Method GetALLUserApproval");
                var loResult = loCls.GetALLUserApproval(poEntity);

                _Logger.LogInfo("Filter Search by text GSL01100GetUserApproval");
                loRtn.Data = loResult.Find(x => x.CUSER_ID.Trim().ToUpper() == poEntity.CSEARCH_TEXT.ToUpper().Trim());
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            _Logger.LogInfo("End GSL01100GetUserApproval");
            return loRtn;
        }

        [HttpPost]
        public GSLGenericRecord<GSL01200DTO> GSL01200GetBank(GSL01200ParameterDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("GSL01200GetBank");
            var loEx = new R_Exception();
            GSLGenericRecord<GSL01200DTO> loRtn = new();
            _Logger.LogInfo("Start GSL01200GetBank");

            try
            {
                var loCls = new PublicLookupCls();

                _Logger.LogInfo("Call Back Method GetALLBank");
                var loResult = loCls.GetALLBank(poEntity);

                _Logger.LogInfo("Filter Search by text GSL01200GetBank");
                loRtn.Data = loResult.Find(x => x.CCB_CODE.Trim().ToUpper() == poEntity.CSEARCH_TEXT.ToUpper().Trim());
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            _Logger.LogInfo("End GSL01200GetBank");
            return loRtn;
        }

        [HttpPost]
        public GSLGenericRecord<GSL01300DTO> GSL01300GetBankAccount(GSL01300ParameterDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("GSL01300GetBankAccount");
            var loEx = new R_Exception();
            GSLGenericRecord<GSL01300DTO> loRtn = new();
            _Logger.LogInfo("Start GSL01300GetBankAccount");

            try
            {
                var loCls = new PublicLookupCls();

                _Logger.LogInfo("Call Back Method GetALLBankAccount");
                var loResult = loCls.GetALLBankAccount(poEntity);

                _Logger.LogInfo("Filter Search by text GSL01300GetBankAccount");
                loRtn.Data = loResult.Find(x => x.CCB_ACCOUNT_NO.Trim().ToUpper() == poEntity.CSEARCH_TEXT.ToUpper().Trim());
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            _Logger.LogInfo("End GSL01300GetBankAccount");
            return loRtn;
        }

        [HttpPost]
        public GSLGenericRecord<GSL01400DTO> GSL01400GetOtherCharges(GSL01400ParameterDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("GSL01400GetOtherCharges");
            var loEx = new R_Exception();
            GSLGenericRecord<GSL01400DTO> loRtn = new();
            _Logger.LogInfo("Start GSL01400GetOtherCharges");

            try
            {
                var loCls = new PublicLookupCls();

                _Logger.LogInfo("Call Back Method GetALLOtherCharges");
                var loResult = loCls.GetALLOtherCharges(poEntity);

                _Logger.LogInfo("Filter Search by text GSL01400GetOtherCharges");
                loRtn.Data = loResult.Find(x => x.CCHARGES_ID.Trim().ToUpper() == poEntity.CSEARCH_TEXT.ToUpper().Trim());
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            _Logger.LogInfo("End GSL01400GetOtherCharges");
            return loRtn;
        }

        [HttpPost]
        public GSLGenericRecord<GSL01600DTO> GSL01600GetCashFlowGroupType(GSL01600ParameterDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("GSL01600GetCashFlowGroupType");
            var loEx = new R_Exception();
            GSLGenericRecord<GSL01600DTO> loRtn = new();
            _Logger.LogInfo("Start GSL01600GetCashFlowGroupType");

            try
            {
                var loCls = new PublicLookupCls();

                _Logger.LogInfo("Call Back Method GetALLCashFlowGruopType");
                var loResult = loCls.GetALLCashFlowGruopType();

                _Logger.LogInfo("Filter Search by text GSL01600GetCashFlowGroupType");
                loRtn.Data = loResult.Find(x => x.CCASH_FLOW_GROUP_CODE.Trim().ToUpper() == poEntity.CSEARCH_TEXT.ToUpper().Trim());
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            _Logger.LogInfo("End GSL01600GetCashFlowGroupType");
            return loRtn;
        }

        [HttpPost]
        public GSLGenericRecord<GSL01800DTO> GSL01800GetCategory(GSL01800DTOParameter poEntity)
        {
            using Activity activity = _activitySource.StartActivity("GSL01800GetCategory");
            var loEx = new R_Exception();
            GSLGenericRecord<GSL01800DTO> loRtn = new();
            _Logger.LogInfo("Start GSL01800GetCategory");

            try
            {
                var loCls = new PublicLookupCls();

                _Logger.LogInfo("Call Back Method GetALLCategory");
                var loResult = loCls.GetALLCategory(poEntity);

                _Logger.LogInfo("Filter Search by text GSL01800GetCategory");
                loRtn.Data = loResult.Find(x => x.CCATEGORY_ID.Trim().ToUpper() == poEntity.CSEARCH_TEXT.ToUpper().Trim());
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            _Logger.LogInfo("End GSL01800GetCategory");
            return loRtn;
        }

        [HttpPost]
        public GSLGenericRecord<GSL01900DTO> GSL01900GetLOB(GSL01900ParameterDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("GSL01900GetLOB");
            var loEx = new R_Exception();
            GSLGenericRecord<GSL01900DTO> loRtn = new();
            _Logger.LogInfo("Start GSL01900GetLOB");

            try
            {
                var loCls = new PublicLookupCls();

                _Logger.LogInfo("Call Back Method GetALLOtherCharges");
                var loResult = loCls.GetALLLOB();

                _Logger.LogInfo("Filter Search by text GSL01900GetLOB");
                loRtn.Data = loResult.Find(x => x.CLOB_CODE.Trim().ToUpper() == poEntity.CSEARCH_TEXT.ToUpper().Trim());
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            _Logger.LogInfo("End GSL01900GetLOB");
            return loRtn;
        }

        [HttpPost]
        public GSLGenericRecord<GSL02100DTO> GSL02100GetPaymentTerm(GSL02100ParameterDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("GSL02100GetPaymentTerm");
            var loEx = new R_Exception();
            GSLGenericRecord<GSL02100DTO> loRtn = new();
            _Logger.LogInfo("Start GSL02100GetPaymentTerm");

            try
            {
                var loCls = new PublicLookupCls();

                _Logger.LogInfo("Call Back Method GetALLCategory");
                var loResult = loCls.GetALLPaymentTerm(poEntity);

                _Logger.LogInfo("Filter Search by text GSL02100GetPaymentTerm");
                loRtn.Data = loResult.Find(x => x.CPAY_TERM_CODE.Trim().ToUpper() == poEntity.CSEARCH_TEXT.ToUpper().Trim());
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            _Logger.LogInfo("End GSL02100GetPaymentTerm");
            return loRtn;
        }

        [HttpPost]
        public GSLGenericRecord<GSL02200DTO> GSL02200GetBuilding(GSL02200ParameterDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("GSL02200GetBuilding");
            var loEx = new R_Exception();
            GSLGenericRecord<GSL02200DTO> loRtn = new();
            _Logger.LogInfo("Start GSL02200GetBuilding");

            try
            {
                var loCls = new PublicLookupCls();

                _Logger.LogInfo("Call Back Method GetALLBuilding");
                var loResult = loCls.GetALLBuilding(poEntity);

                _Logger.LogInfo("Filter Search by text GSL02200GetBuilding");
                loRtn.Data = loResult.Find(x => x.CBUILDING_ID.Trim().ToUpper() == poEntity.CSEARCH_TEXT.ToUpper().Trim());
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            _Logger.LogInfo("End GSL02200GetBuilding");
            return loRtn;
        }

        [HttpPost]
        public GSLGenericRecord<GSL02300DTO> GSL02300GetBuildingUnit(GSL02300ParameterDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("GSL02300GetBuildingUnit");
            var loEx = new R_Exception();
            GSLGenericRecord<GSL02300DTO> loRtn = new();
            _Logger.LogInfo("Start GSL02300GetBuildingUnit");

            try
            {
                var loCls = new PublicLookupCls();

                _Logger.LogInfo("Call Back Method GetALLBuildingUnit");
                var loResult = loCls.GetALLBuildingUnit(poEntity);

                _Logger.LogInfo("Filter Search by text GSL02300GetBuildingUnit");
                loRtn.Data = loResult.Find(x => x.CUNIT_ID.Trim().ToUpper() == poEntity.CSEARCH_TEXT.ToUpper().Trim());
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            _Logger.LogInfo("End GSL02300GetBuildingUnit");
            return loRtn;
        }

        [HttpPost]
        public GSLGenericRecord<GSL02400DTO> GSL02400GetFloor(GSL02400ParameterDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("GSL02400GetFloor");
            var loEx = new R_Exception();
            GSLGenericRecord<GSL02400DTO> loRtn = new();
            _Logger.LogInfo("Start GSL02400GetFloor");

            try
            {
                var loCls = new PublicLookupCls();

                _Logger.LogInfo("Call Back Method GetALLFloor");
                var loResult = loCls.GetALLFloor(poEntity);

                _Logger.LogInfo("Filter Search by text GSL02400GetFloor");
                loRtn.Data = loResult.Find(x => x.CFLOOR_ID.Trim().ToUpper() == poEntity.CSEARCH_TEXT.ToUpper().Trim());
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            _Logger.LogInfo("End GSL02400GetFloor");
            return loRtn;
        }

        [HttpPost]
        public GSLGenericRecord<GSL02500DTO> GSL02500GetCB(GSL02500ParameterDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("GSL02500GetCB");
            var loEx = new R_Exception();
            GSLGenericRecord<GSL02500DTO> loRtn = new();
            _Logger.LogInfo("Start GSL02500GetCB");

            try
            {
                var loCls = new PublicLookupCls();

                _Logger.LogInfo("Call Back Method GetALLCB");
                var loResult = loCls.GetALLCB(poEntity);

                _Logger.LogInfo("Filter Search by text GSL02500GetCB");
                loRtn.Data = loResult.Find(x => x.CCB_CODE.Trim().ToUpper() == poEntity.CSEARCH_TEXT.ToUpper().Trim());
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            _Logger.LogInfo("End GSL02500GetCB");
            return loRtn;
        }

        [HttpPost]
        public GSLGenericRecord<GSL02600DTO> GSL02600GetCBAccount(GSL02600ParameterDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("GSL02600GetCBAccount");
            var loEx = new R_Exception();
            GSLGenericRecord<GSL02600DTO> loRtn = new();
            _Logger.LogInfo("Start GSL02600GetCBAccount");

            try
            {
                var loCls = new PublicLookupCls();

                _Logger.LogInfo("Call Back Method GetALLCBAccount");
                var loResult = loCls.GetALLCBAccount(poEntity);

                _Logger.LogInfo("Filter Search by text GSL02600GetCBAccount");
                loRtn.Data = loResult.Find(x => x.CCB_ACCOUNT_NO.Trim().ToUpper() == poEntity.CSEARCH_TEXT.ToUpper().Trim());
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            _Logger.LogInfo("End GSL02600GetCBAccount");
            return loRtn;
        }
    }
}