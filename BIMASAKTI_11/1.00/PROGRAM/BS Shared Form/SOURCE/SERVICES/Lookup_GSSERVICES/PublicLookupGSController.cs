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
    public class PublicLookupGSController : ControllerBase, IPublicLookup
    {
        private LoggerPublicLookup _Logger;
        private readonly ActivitySource _activitySource;

        public PublicLookupGSController(ILogger<LoggerPublicLookup> logger)
        {
            //Initial and Get Logger
            LoggerPublicLookup.R_InitializeLogger(logger);
            _Logger = LoggerPublicLookup.R_GetInstanceLogger();
            _activitySource = PublicLookupGSActivitySourceBase.R_InitializeAndGetActivitySource(nameof(PublicLookupGSController));
        }

        [HttpPost]
        public IAsyncEnumerable<GSL00100DTO> GSL00100GetSalesTaxList()
        {
            return GetGSL00100Stream();
        }
        private async IAsyncEnumerable<GSL00100DTO> GetGSL00100Stream()
        {
            using Activity activity = _activitySource.StartActivity("GSL00100GetSalesTaxList");
            var loEx = new R_Exception();
            List<GSL00100DTO> loRtn = null;
            _Logger.LogInfo("Start GSL00100GetSalesTaxList");

            try
            {
                var loCls = new PublicLookupCls();
                var poParameter = new GSL00100ParameterDTO();

                _Logger.LogInfo("Call Back Method GetALLSalesTax");
                loRtn = await loCls.GetALLSalesTax();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            _Logger.LogInfo("End GSL00100GetSalesTaxList");

            foreach (var item in loRtn)
            {
                yield return item;
            }
        }

        [HttpPost]
        public IAsyncEnumerable<GSL00110DTO> GSL00110GetTaxByDateList()
        {
            return GetGSL00110Stream();
        }
        private async IAsyncEnumerable<GSL00110DTO> GetGSL00110Stream()
        {
            using Activity activity = _activitySource.StartActivity("GSL00110GetTaxByDateList");
            var loEx = new R_Exception();
            List<GSL00110DTO> loRtn = null;
            _Logger.LogInfo("Start GSL00110GetTaxByDateList");

            try
            {
                var loCls = new PublicLookupCls();
                var poParameter = new GSL00110ParameterDTO();

                _Logger.LogInfo("Set Param GSL00110GetTaxByDateList");
                poParameter.CTAX_DATE = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CTAX_DATE);

                _Logger.LogInfo("Call Back Method GetALLTaxByDate");
                loRtn = await loCls.GetALLTaxByDate(poParameter);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            _Logger.LogInfo("End GSL00110GetTaxByDateList");

            foreach (var item in loRtn)
            {
                yield return item;
            }
        }

        [HttpPost]
        public IAsyncEnumerable<GSL00200DTO> GSL00200GetWithholdingTaxList()
        {
            return GetGSL00200Stream();
        }
        private async IAsyncEnumerable<GSL00200DTO> GetGSL00200Stream()
        {
            using Activity activity = _activitySource.StartActivity("GSL00200GetWithholdingTaxList");
            var loEx = new R_Exception();
            List<GSL00200DTO> loRtn = null;
            _Logger.LogInfo("Start GSL00200GetWithholdingTaxList");

            try
            {
                var loCls = new PublicLookupCls();
                var poParameter = new GSL00200ParameterDTO();

                _Logger.LogInfo("Set Param GSL00200GetWithholdingTaxList");
                poParameter.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CPROPERTY_ID);
                poParameter.CTAX_TYPE_LIST = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CTAX_TYPE_LIST);

                _Logger.LogInfo("Call Back Method GetALLWithholdingTax");
                loRtn = await loCls.GetALLWithholdingTax(poParameter);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GSL00200GetWithholdingTaxList");

            foreach (var item in loRtn)
            {
                yield return item;
            }
        }

        [HttpPost]
        public IAsyncEnumerable<GSL00300DTO> GSL00300GetCurrencyList()
        {
            return GetGSL00300Stream();
        }
        private async IAsyncEnumerable<GSL00300DTO> GetGSL00300Stream()
        {
            using Activity activity = _activitySource.StartActivity("GSL00300GetCurrencyList");
            var loEx = new R_Exception();
            List<GSL00300DTO> loRtn = null;
            _Logger.LogInfo("Start GSL00300GetCurrencyList");

            try
            {
                var loCls = new PublicLookupCls();
                GSL00300ParameterDTO loParam = new GSL00300ParameterDTO();

                _Logger.LogInfo("Call Back Method GetALLCurrency");
                loRtn = await loCls.GetALLCurrency();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GSL00300GetCurrencyList");

            foreach (var item in loRtn)
            {
                yield return item;
            }
        }

        [HttpPost]
        public IAsyncEnumerable<GSL00400DTO> GSL00400GetJournalGroupList()
        {
            return GetGSL00400Stream();
        }
        private async IAsyncEnumerable<GSL00400DTO> GetGSL00400Stream()
        {
            using Activity activity = _activitySource.StartActivity("GSL00400GetJournalGroupList");
            var loEx = new R_Exception();
            List<GSL00400DTO> loRtn = null;
            _Logger.LogInfo("Start GSL00400GetJournalGroupList");

            try
            {
                var loCls = new PublicLookupCls();
                var poParameter = new GSL00400ParameterDTO();

                _Logger.LogInfo("Set Param GSL00400GetJournalGroupList");
                poParameter.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CPROPERTY_ID);
                poParameter.CJRNGRP_TYPE = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CJRNGRP_TYPE);

                _Logger.LogInfo("Call Back Method GetALLJournalGroup");
                loRtn = await loCls.GetALLJournalGroup(poParameter);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GSL00400GetJournalGroupList");

            foreach (var item in loRtn)
            {
                yield return item;
            }
        }

        [HttpPost]
        public IAsyncEnumerable<GSL00500DTO> GSL00500GetGLAccountList()
        {
            return GetGSL00500Stream();
        }
        private async IAsyncEnumerable<GSL00500DTO> GetGSL00500Stream()
        {
            using Activity activity = _activitySource.StartActivity("GSL00500GetGLAccountList");
            var loEx = new R_Exception();
            List<GSL00500DTO> loRtn = null;
            _Logger.LogInfo("Start GSL00500GetGLAccountList");

            try
            {
                var loCls = new PublicLookupCls();
                var poParameter = new GSL00500ParameterDTO();

                _Logger.LogInfo("Set Param GSL00500GetGLAccountList");
                poParameter.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CPROPERTY_ID);
                poParameter.CDBCR = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CDBCR);
                poParameter.CBSIS = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CBSIS);
                poParameter.LUSER_RESTR = R_Utility.R_GetStreamingContext<bool>(ContextConstantPublicLookup.LUSER_RESTR);
                poParameter.CCENTER_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CCENTER_CODE);
                poParameter.CPROGRAM_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CPROGRAM_CODE);
                poParameter.LCENTER_RESTR = R_Utility.R_GetStreamingContext<bool>(ContextConstantPublicLookup.LCENTER_RESTR);
                poParameter.CGOA_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CGOA_CODE);

                _Logger.LogInfo("Call Back Method GetALLGLAccount");
                loRtn = await loCls.GetALLGLAccount(poParameter);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GSL00500GetGLAccountList");

            foreach (var item in loRtn)
            {
                yield return item;
            }
        }

        [HttpPost]
        public IAsyncEnumerable<GSL00510DTO> GSL00510GetCOAList()
        {
            return GetGSL00510Stream();
        }
        private async IAsyncEnumerable<GSL00510DTO> GetGSL00510Stream()
        {
            using Activity activity = _activitySource.StartActivity("GSL00510GetCOAList");
            var loEx = new R_Exception();
            List<GSL00510DTO> loRtn = null;
            _Logger.LogInfo("Start GSL00510GetCOAList");

            try
            {
                var loCls = new PublicLookupCls();
                var poParameter = new GSL00510ParameterDTO();

                _Logger.LogInfo("Set Param GSL00510GetCOAList");
                poParameter.CGLACCOUNT_TYPE = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CGLACCOUNT_TYPE);
                poParameter.LINACTIVE_COA = R_Utility.R_GetStreamingContext<bool>(ContextConstantPublicLookup.LINACTIVE_COA);

                _Logger.LogInfo("Call Back Method GetALLCOA");
                loRtn = await loCls.GetALLCOA(poParameter);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GSL00510GetCOAList");

            foreach (var item in loRtn)
            {
                yield return item;
            }
        }

        [HttpPost]
        public IAsyncEnumerable<GSL00520DTO> GSL00520GetGOACOAList()
        {
            return GetGSL00520Stream();
        }
        private async IAsyncEnumerable<GSL00520DTO> GetGSL00520Stream()
        {
            using Activity activity = _activitySource.StartActivity("GSL00520GetGOACOAList");
            var loEx = new R_Exception();
            List<GSL00520DTO> loRtn = null;
            _Logger.LogInfo("Start GSL00520GetGOACOAList");

            try
            {
                var loCls = new PublicLookupCls();
                var poParameter = new GSL00520ParameterDTO();

                _Logger.LogInfo("Set Param GSL00520GetGOACOAList");
                poParameter.CGOA_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CGOA_CODE);

                _Logger.LogInfo("Call Back Method GetALLGOACOA");
                loRtn = await loCls.GetALLGOACOA(poParameter);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GSL00520GetGOACOAList");

            foreach (var item in loRtn)
            {
                yield return item;
            }
        }

        [HttpPost]
        public IAsyncEnumerable<GSL00550DTO> GSL00550GetGOAList()
        {
            return GetGSL00550Stream();
        }
        private async IAsyncEnumerable<GSL00550DTO> GetGSL00550Stream()
        {
            using Activity activity = _activitySource.StartActivity("GSL00550GetGOAList");
            var loEx = new R_Exception();
            List<GSL00550DTO> loRtn = null;
            _Logger.LogInfo("Start GSL00550GetGOAList");

            try
            {
                var loCls = new PublicLookupCls();
                var poParameter = new GSL00550ParameterDTO();

                _Logger.LogInfo("Set Param GSL00550GetGOAList");

                _Logger.LogInfo("Call Back Method GetALLGOA");
                loRtn = await loCls.GetALLGOA();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GSL00550GetGOAList");

            foreach (var item in loRtn)
            {
                yield return item;
            }
        }

        [HttpPost]
        public IAsyncEnumerable<GSL00600DTO> GSL00600GetUnitTypeCategoryList()
        {
            return GetGSL00600Stream();
        }
        private async IAsyncEnumerable<GSL00600DTO> GetGSL00600Stream()
        {
            using Activity activity = _activitySource.StartActivity("GSL00600GetUnitTypeCategoryList");
            var loEx = new R_Exception();
            _Logger.LogInfo("Start GSL00600GetUnitTypeCategoryList");
            List<GSL00600DTO> loRtn = null;

            try
            {
                var loCls = new PublicLookupCls();
                var poParameter = new GSL00600ParameterDTO();

                _Logger.LogInfo("Set Param GSL00600GetUnitTypeCategoryList");
                poParameter.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CPROPERTY_ID);

                _Logger.LogInfo("Call Back Method GetALLUnitTypeCategory");
                loRtn = await loCls.GetALLUnitTypeCategory(poParameter);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GSL00600GetUnitTypeCategoryList");

            foreach (var item in loRtn)
            {
                yield return item;
            }
        }

        [HttpPost]
        public IAsyncEnumerable<GSL00700DTO> GSL00700GetDepartmentList()
        {
            return GetGSL00700Stream();
        }
        private async IAsyncEnumerable<GSL00700DTO> GetGSL00700Stream()
        {
            using Activity activity = _activitySource.StartActivity("GSL00700GetDepartmentList");
            var loEx = new R_Exception();
            List<GSL00700DTO> loRtn = null;
            _Logger.LogInfo("Start GSL00700GetDepartmentList");

            try
            {
                var loCls = new PublicLookupCls();
                var poParameter = new GSL00700ParameterDTO();

                _Logger.LogInfo("Set Param GSL00700GetDepartmentList");
                poParameter.CPROGRAM_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CPROGRAM_CODE);

                _Logger.LogInfo("Call Back Method GetALLDepartment");
                loRtn = await loCls.GetALLDepartment(poParameter);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GSL00700GetDepartmentList");

            foreach (var item in loRtn)
            {
                yield return item;
            }
        }

        [HttpPost]
        public IAsyncEnumerable<GSL00710DTO> GSL00710GetDepartmentPropertyList()
        {
            return GetGSL00710Stream();
        }
        private async IAsyncEnumerable<GSL00710DTO> GetGSL00710Stream()
        {
            using Activity activity = _activitySource.StartActivity("GSL00710GetDepartmentPropertyList");
            var loEx = new R_Exception();
            List<GSL00710DTO> loRtn = null;
            _Logger.LogInfo("Start GSL00710GetDepartmentPropertyList");

            try
            {
                var loCls = new PublicLookupCls();
                var poParameter = new GSL00710ParameterDTO();

                _Logger.LogInfo("Set Param GSL00710GetDepartmentPropertyList");
                poParameter.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CPROPERTY_ID);

                _Logger.LogInfo("Call Back Method GetALLDepartmentProperty");
                loRtn = await loCls.GetALLDepartmentProperty(poParameter);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GSL00710GetDepartmentPropertyList");

            foreach (var item in loRtn)
            {
                yield return item;
            }
        }

        [HttpPost]
        public IAsyncEnumerable<GSL00800DTO> GSL00800GetCurrencyTypeList()
        {
            return GetGSL00800Stream();
        }
        private async IAsyncEnumerable<GSL00800DTO> GetGSL00800Stream()
        {
            using Activity activity = _activitySource.StartActivity("GSL00800GetCurrencyTypeList");
            var loEx = new R_Exception();
            List<GSL00800DTO> loRtn = null;
            _Logger.LogInfo("Start GSL00800GetCurrencyTypeList");

            try
            {
                var loCls = new PublicLookupCls();
                var poParameter = new GSL00800ParameterDTO();

                _Logger.LogInfo("Call Back Method GetALLCurrencyRateType");
                loRtn = await loCls.GetALLCurrencyRateType();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }
            _Logger.LogInfo("End GSL00800GetCurrencyTypeList");
            loEx.ThrowExceptionIfErrors();

            foreach (var item in loRtn)
            {
                yield return item;
            }
        }

        [HttpPost]
        public IAsyncEnumerable<GSL00900DTO> GSL00900GetCenterList()
        {
            return GetGSL00900Stream();
        }
        private async IAsyncEnumerable<GSL00900DTO> GetGSL00900Stream()
        {
            using Activity activity = _activitySource.StartActivity("GSL00900GetCenterList");
            var loEx = new R_Exception();
            List<GSL00900DTO> loRtn = null;
            _Logger.LogInfo("Start GSL00900GetCenterList");

            try
            {
                var loCls = new PublicLookupCls();
                var poParameter = new GSL00900ParameterDTO();

                _Logger.LogInfo("Call Back Method GetALLCenter");
                loRtn = await loCls.GetALLCenter();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GSL00900GetCenterList");

            foreach (var item in loRtn)
            {
                yield return item;
            }
        }

        [HttpPost]
        public IAsyncEnumerable<GSL01000DTO> GSL01000GetUserList()
        {
            return GetGSL01000Stream();
        }
        private async IAsyncEnumerable<GSL01000DTO> GetGSL01000Stream()
        {
            using Activity activity = _activitySource.StartActivity("GSL01000GetUserList");
            var loEx = new R_Exception();
            List<GSL01000DTO> loRtn = null;
            _Logger.LogInfo("Start GSL01000GetUserList");

            try
            {
                var loCls = new PublicLookupCls();

                _Logger.LogInfo("Set Param GSL01000GetUserList");
                _Logger.LogInfo("Call Back Method GetALLUser");
                loRtn = await loCls.GetALLUser();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GSL01000GetUserList");

            foreach (var item in loRtn)
            {
                yield return item;
            }
        }

        [HttpPost]
        public IAsyncEnumerable<GSL01100DTO> GSL01100GetUserApprovalList()
        {
            return GetGSL01100Stream();
        }
        private async IAsyncEnumerable<GSL01100DTO> GetGSL01100Stream()
        {
            using Activity activity = _activitySource.StartActivity("GSL01100GetUserApprovalList");
            var loEx = new R_Exception();
            List<GSL01100DTO> loRtn = null;
            _Logger.LogInfo("Start GSL01100GetUserApprovalList");

            try
            {
                var loCls = new PublicLookupCls();
                var poParameter = new GSL01100ParameterDTO();

                _Logger.LogInfo("Set Param GSL01100GetUserApprovalList");
                poParameter.CPARAMETER_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CPARAMETER_ID);
                poParameter.CPROGRAM_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CPROGRAM_ID);

                _Logger.LogInfo("Call Back Method GetALLUserApproval");
                loRtn = await loCls.GetALLUserApproval(poParameter);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GSL01100GetUserApprovalList");

            foreach (var item in loRtn)
            {
                yield return item;
            }
        }

        [HttpPost]
        public IAsyncEnumerable<GSL01200DTO> GSL01200GetBankList()
        {
            return GetGSL01200Stream();
        }
        private async IAsyncEnumerable<GSL01200DTO> GetGSL01200Stream()
        {
            using Activity activity = _activitySource.StartActivity("GSL01200GetBankList");
            var loEx = new R_Exception();
            List<GSL01200DTO> loRtn = null;
            _Logger.LogInfo("Start GSL01200GetBankList");

            try
            {
                var loCls = new PublicLookupCls();
                var poParameter = new GSL01200ParameterDTO();

                _Logger.LogInfo("Set Param GSL01200GetBankList");
                poParameter.CCB_TYPE = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CCB_TYPE);

                _Logger.LogInfo("Call Back Method GetALLBank");
                loRtn = await loCls.GetALLBank(poParameter);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GSL01200GetBankList");

            foreach (var item in loRtn)
            {
                yield return item;
            }
        }

        [HttpPost]
        public IAsyncEnumerable<GSL01300DTO> GSL01300GetBankAccountList()
        {
            return GetGSL01300Stream();
        }
        private async IAsyncEnumerable<GSL01300DTO> GetGSL01300Stream()
        {
            using Activity activity = _activitySource.StartActivity("GSL01300GetBankAccountList");
            var loEx = new R_Exception();
            List<GSL01300DTO> loRtn = null;
            _Logger.LogInfo("Start GSL01300GetBankAccountList");

            try
            {
                var loCls = new PublicLookupCls();
                var poParameter = new GSL01300ParameterDTO();

                _Logger.LogInfo("Set Param GSL01300GetBankAccountList");
                poParameter.CBANK_TYPE = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CBANK_TYPE);
                poParameter.CCB_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CCB_CODE);
                poParameter.CDEPT_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CDEPT_CODE);

                _Logger.LogInfo("Call Back Method GetALLBankAccount");
                loRtn = await loCls.GetALLBankAccount(poParameter);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GSL01300GetBankAccountList");

            foreach (var item in loRtn)
            {
                yield return item;
            }
        }

        [HttpPost]
        public IAsyncEnumerable<GSL01400DTO> GSL01400GetOtherChargesList()
        {
            return GetGSL01400Stream();
        }
        private async IAsyncEnumerable<GSL01400DTO> GetGSL01400Stream()
        {
            using Activity activity = _activitySource.StartActivity("GSL01400GetOtherChargesList");
            var loEx = new R_Exception();
            List<GSL01400DTO> loRtn = null;
            _Logger.LogInfo("Start GSL01400GetOtherChargesList");

            try
            {
                var loCls = new PublicLookupCls();
                var poParameter = new GSL01400ParameterDTO();

                _Logger.LogInfo("Set Param GSL01400GetOtherChargesList");
                poParameter.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CPROPERTY_ID);
                poParameter.CCHARGES_TYPE_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CCHARGES_TYPE_ID);

                _Logger.LogInfo("Call Back Method GetALLOtherCharges");
                loRtn = await loCls.GetALLOtherCharges(poParameter);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GSL01400GetOtherChargesList");

            foreach (var item in loRtn)
            {
                yield return item;
            }
        }

        [HttpPost]
        public IAsyncEnumerable<GSL01500ResultDetailDTO> GSL01500GetCashDetailList()
        {
            return GetGSL01501Stream();
        }
        private async IAsyncEnumerable<GSL01500ResultDetailDTO> GetGSL01501Stream()
        {
            using Activity activity = _activitySource.StartActivity("GSL01500GetCashDetailList");
            var loEx = new R_Exception();
            List<GSL01500ResultDetailDTO> loRtn = null;
            _Logger.LogInfo("Start GSL01500GetCashDetailList");

            try
            {
                var loCls = new PublicLookupCls();
                var poParameter = new GSL01500ParameterDetailDTO();

                _Logger.LogInfo("Set Param GSL01500GetCashDetailList");
                poParameter.CCASH_FLOW_GROUP_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CCASH_FLOW_GROUP_CODE);

                _Logger.LogInfo("Call Back Method GetALLCashFlowDetail");
                loRtn = await loCls.GetALLCashFlowDetail(poParameter);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GSL01500GetCashDetailList");

            foreach (var item in loRtn)
            {
                yield return item;
            }
        }

        [HttpPost]
        public IAsyncEnumerable<GSL01500ResultGroupDTO> GSL01500GetCashFlowGroupList()
        {
            return GetGSL01500Stream();
        }
        private async IAsyncEnumerable<GSL01500ResultGroupDTO> GetGSL01500Stream()
        {
            using Activity activity = _activitySource.StartActivity("GSL01500GetCashFlowGroupList");
            var loEx = new R_Exception();
            List<GSL01500ResultGroupDTO> loRtn = null;
            _Logger.LogInfo("Start GSL01500GetCashFlowGroupList");

            try
            {
                var loCls = new PublicLookupCls();
                var poParameter = new GSL01500ParameterGroupDTO();

                _Logger.LogInfo("Call Back Method GetALLCashFlowGroup");
                loRtn = await loCls.GetALLCashFlowGroup(poParameter);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GSL01500GetCashFlowGroupList");

            foreach (var item in loRtn)
            {
                yield return item;
            }
        }

        [HttpPost]
        public IAsyncEnumerable<GSL01600DTO> GSL01600GetCashFlowGroupTypeList()
        {
            return GetGSL01600Stream();
        }
        private async IAsyncEnumerable<GSL01600DTO> GetGSL01600Stream()
        {
            using Activity activity = _activitySource.StartActivity("GSL01600GetCashFlowGroupTypeList");
            var loEx = new R_Exception();
            List<GSL01600DTO> loRtn = null;
            _Logger.LogInfo("Start GSL01600GetCashFlowGroupTypeList");

            try
            {
                var loCls = new PublicLookupCls();
                var poParameter = new GSL01600ParameterDTO();

                _Logger.LogInfo("Call Back Method GetALLCashFlowGruopType");
                loRtn = await loCls.GetALLCashFlowGruopType();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GSL01600GetCashFlowGroupTypeList");

            foreach (var item in loRtn)
            {
                yield return item;
            }
        }

        [HttpPost]
        public IAsyncEnumerable<GSL01700DTO> GSL01700GetCurrencyRateList()
        {
            return GetGSL01700Stream();
        }
        private async IAsyncEnumerable<GSL01700DTO> GetGSL01700Stream()
        {
            using Activity activity = _activitySource.StartActivity("GSL01700GetCurrencyRateList");
            var loEx = new R_Exception();
            List<GSL01700DTO> loRtn = null;
            _Logger.LogInfo("Start GSL01700GetCurrencyRateList");

            try
            {
                var loCls = new PublicLookupCls();
                var poParameter = new GSL01700DTOParameter();

                _Logger.LogInfo("Set Param GSL01700GetCurrencyRateList");
                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CUSER_ID = R_BackGlobalVar.USER_ID;
                poParameter.CRATETYPE_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CRATETYPE_CODE);
                poParameter.CRATE_DATE = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CRATE_DATE);

                _Logger.LogInfo("Call Back Method GetALLCurrencyRate");
                loRtn = await loCls.GetALLCurrencyRate(poParameter);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GSL01700GetCurrencyRateList");

            foreach (var item in loRtn)
            {
                yield return item;
            }
        }

        [HttpPost]
        public IAsyncEnumerable<GSL01701DTO> GSL01700GetRateTypeList()
        {
            return GetGSL01701Stream();
        }
        private async IAsyncEnumerable<GSL01701DTO> GetGSL01701Stream()
        {
            using Activity activity = _activitySource.StartActivity("GSL01700GetRateTypeList");
            var loEx = new R_Exception();
            List<GSL01701DTO> loRtn = null;
            var loParam = new GSL01700DTOParameter();
            _Logger.LogInfo("Start GSL01700GetRateTypeList");

            try
            {
                var loCls = new PublicLookupCls();

                _Logger.LogInfo("Call Back Method GetALLRateType");
                loRtn = await loCls.GetALLRateType();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GSL01700GetRateTypeList");

            foreach (var item in loRtn)
            {
                yield return item;
            }
        }

        [HttpPost]
        public IAsyncEnumerable<GSL01702DTO> GSL01700GetLocalAndBaseCurrencyList()
        {
            return GetGSL01702Stream();
        }
        private async IAsyncEnumerable<GSL01702DTO> GetGSL01702Stream()
        {
            using Activity activity = _activitySource.StartActivity("GSL01700GetLocalAndBaseCurrencyList");
            var loEx = new R_Exception();
            List<GSL01702DTO> loRtn = null;
            var loParam = new GSL01700DTOParameter();
            _Logger.LogInfo("Start GSL01700GetLocalAndBaseCurrencyList");

            try
            {
                var loCls = new PublicLookupCls();

                _Logger.LogInfo("Call Back Method GetALLLocalAndBaseCurrency");
                loRtn = await loCls.GetALLLocalAndBaseCurrency();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GSL01700GetLocalAndBaseCurrencyList");

            foreach (var item in loRtn)
            {
                yield return item;
            }
        }

        [HttpPost]
        public IAsyncEnumerable<GSL01800DTO> GSL01800GetCategoryList()
        {
            return GetGSL01800Stream();
        }
        private async IAsyncEnumerable<GSL01800DTO> GetGSL01800Stream()
        {
            using Activity activity = _activitySource.StartActivity("GSL01800GetCategoryList");
            var loEx = new R_Exception();
            List<GSL01800DTO> loRtn = null;
            _Logger.LogInfo("Start GSL01800GetCategoryList");

            try
            {
                var loCls = new PublicLookupCls();
                var poParameter = new GSL01800DTOParameter();

                _Logger.LogInfo("Set Param GSL01800GetCategoryList");
                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CUSER_ID = R_BackGlobalVar.USER_ID;
                poParameter.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CPROPERTY_ID);
                poParameter.CCATEGORY_TYPE = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CCATEGORY_TYPE);

                _Logger.LogInfo("Call Back Method GetALLCategory");
                loRtn = await loCls.GetALLCategory(poParameter);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GSL01800GetCategoryList");

            foreach (var item in loRtn)
            {
                yield return item;
            }
        }

        [HttpPost]
        public IAsyncEnumerable<GSL01900DTO> GSL01900GetLOBList()
        {
            return GetGSL01900Stream();
        }
        private async IAsyncEnumerable<GSL01900DTO> GetGSL01900Stream()
        {
            using Activity activity = _activitySource.StartActivity("GSL01900GetLOBList");
            var loEx = new R_Exception();
            List<GSL01900DTO> loRtn = null;
            _Logger.LogInfo("Start GSL01900GetLOBList");

            try
            {

                var loCls = new PublicLookupCls();

                _Logger.LogInfo("Call Back Method GetALLLOB");
                loRtn = await loCls.GetALLLOB();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GSL01900GetLOBList");

            foreach (var item in loRtn)
            {
                yield return item;
            }
        }

        [HttpPost]
        public IAsyncEnumerable<GSL02000CountryDTO> GSL02000GetCountryGeographyList()
        {
            return GetGSL0200Stream();
        }
        private async IAsyncEnumerable<GSL02000CountryDTO> GetGSL0200Stream()
        {
            using Activity activity = _activitySource.StartActivity("GSL02000GetCountryGeographyList");
            var loEx = new R_Exception();
            List<GSL02000CountryDTO> loRtn = null;
            _Logger.LogInfo("Start GSL02000GetCountryGeographyList");

            try
            {

                var loCls = new PublicLookupCls();

                _Logger.LogInfo("Call Back Method GetALLCountryGeography");
                loRtn = await loCls.GetALLCountryGeography();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GSL02000GetCountryGeographyList");

            foreach (var item in loRtn)
            {
                yield return item;
            }
        }

        [HttpPost]
        public IAsyncEnumerable<GSL02000CityDTO> GSL02000GetCityGeographyList()
        {
            return GetGSL02000Stream();
        }
        private async IAsyncEnumerable<GSL02000CityDTO> GetGSL02000Stream()
        {
            using Activity activity = _activitySource.StartActivity("GSL02000GetCityGeographyList");
            var loEx = new R_Exception();
            List<GSL02000CityDTO> loRtn = null;
            _Logger.LogInfo("Start GSL02000GetCityGeographyList");

            try
            {
                var poParameter = new GSL02000CityDTO();

                _Logger.LogInfo("Set Param GSL02000GetCityGeographyList");
                var loCls = new PublicLookupCls();
                poParameter.CUSER_ID = R_BackGlobalVar.USER_ID;
                poParameter.CCOUNTRY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CCOUNTRY_ID);

                _Logger.LogInfo("Call Back Method GetALLCityGeography");
                loRtn = await loCls.GetALLCityGeography(poParameter);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GSL02000GetCityGeographyList");

            foreach (var item in loRtn)
            {
                yield return item;
            }
        }

        [HttpPost]
        public IAsyncEnumerable<GSL02100DTO> GSL02100GetPaymentTermList()
        {
            return GetGSL02100Stream();
        }
        private async IAsyncEnumerable<GSL02100DTO> GetGSL02100Stream()
        {
            using Activity activity = _activitySource.StartActivity("GSL02100GetPaymentTermList");
            var loEx = new R_Exception();
            List<GSL02100DTO> loRtn = null;
            _Logger.LogInfo("Start GSL02100GetPaymentTermList");

            try
            {
                var poParameter = new GSL02100ParameterDTO();

                _Logger.LogInfo("Set Param GSL02100GetPaymentTermList");
                var loCls = new PublicLookupCls();
                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CPROPERTY_ID);
                poParameter.CUSER_ID = R_BackGlobalVar.USER_ID;

                _Logger.LogInfo("Call Back Method GetALLPaymentTerm");
                loRtn = await loCls.GetALLPaymentTerm(poParameter);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GSL02100GetPaymentTermList");

            foreach (var item in loRtn)
            {
                yield return item;
            }
        }

        [HttpPost]
        public IAsyncEnumerable<GSL02200DTO> GSL02200GetBuildingList()
        {
            return GetGSL02200Stream();
        }
        private async IAsyncEnumerable<GSL02200DTO> GetGSL02200Stream()
        {
            using Activity activity = _activitySource.StartActivity("GSL02200GetBuildingList");
            var loEx = new R_Exception();
            List<GSL02200DTO> loRtn = null;
            _Logger.LogInfo("Start GSL02200GetBuildingList");

            try
            {
                var poParameter = new GSL02200ParameterDTO();

                _Logger.LogInfo("Set Param GSL02100GetPaymentTermList");
                var loCls = new PublicLookupCls();
                poParameter.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CPROPERTY_ID);
                poParameter.LAGREEMENT = R_Utility.R_GetStreamingContext<bool>(ContextConstantPublicLookup.LAGREEMENT);

                _Logger.LogInfo("Call Back Method GetALLBuilding");
                loRtn = await loCls.GetALLBuilding(poParameter);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GSL02200GetBuildingList");

            foreach (var item in loRtn)
            {
                yield return item;
            }
        }

        [HttpPost]
        public IAsyncEnumerable<GSL02300DTO> GSL02300GetBuildingUnitList()
        {
            return GetGSL02300Stream();
        }
        private async IAsyncEnumerable<GSL02300DTO> GetGSL02300Stream()
        {
            using Activity activity = _activitySource.StartActivity("GSL02300GetBuildingUnitList");
            var loEx = new R_Exception();
            List<GSL02300DTO> loRtn = null;
            _Logger.LogInfo("Start GSL02300GetBuildingUnitList");

            try
            {
                var poParameter = new GSL02300ParameterDTO();

                _Logger.LogInfo("Set Param GSL02300GetBuildingUnitList");
                var loCls = new PublicLookupCls();
                poParameter.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CPROPERTY_ID);
                poParameter.CBUILDING_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CBUILDING_ID);
                poParameter.CFLOOR_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CFLOOR_ID);
                poParameter.LAGREEMENT = R_Utility.R_GetStreamingContext<bool>(ContextConstantPublicLookup.LAGREEMENT);
                poParameter.CUNIT_TYPE_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CUNIT_TYPE_ID);
                poParameter.CLEASE_STATUS_LIST = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CLEASE_STATUS_LIST);
                poParameter.CPROGRAM_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CPROGRAM_ID);
                poParameter.CTRANS_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CTRANS_CODE);
                poParameter.CREF_NO = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CREF_NO);
                poParameter.CUNIT_CATEGORY_LIST = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CUNIT_CATEGORY_LIST);
                poParameter.CUNIT_TYPE_CATEGORY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CUNIT_TYPE_CATEGORY_ID);

                _Logger.LogInfo("Call Back Method GetALLBuilding");
                loRtn = await loCls.GetALLBuildingUnit(poParameter);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GSL02300GetBuildingUnitList");

            foreach (var item in loRtn)
            {
                yield return item;
            }
        }

        [HttpPost]
        public IAsyncEnumerable<GSL02400DTO> GSL02400GetFloorList()
        {
            return GetGSL02400Stream();
        }
        private async IAsyncEnumerable<GSL02400DTO> GetGSL02400Stream()
        {
            using Activity activity = _activitySource.StartActivity("GSL02400GetFloorList");
            var loEx = new R_Exception();
            List<GSL02400DTO> loRtn = null;
            _Logger.LogInfo("Start GSL02400GetFloorList");

            try
            {
                var poParameter = new GSL02400ParameterDTO();

                _Logger.LogInfo("Set Param GSL02400GetFloorList");
                var loCls = new PublicLookupCls();
                poParameter.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CPROPERTY_ID);
                poParameter.CBUILDING_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CBUILDING_ID);
                poParameter.LAGREEMENT = R_Utility.R_GetStreamingContext<bool>(ContextConstantPublicLookup.LAGREEMENT);
                poParameter.CPROGRAM_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CPROGRAM_ID);
                poParameter.CTRANS_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CTRANS_CODE);
                poParameter.CREF_NO = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CREF_NO);

                _Logger.LogInfo("Call Back Method GetALLFloor");
                loRtn = await loCls.GetALLFloor(poParameter);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GSL02400GetFloorList");

            foreach (var item in loRtn)
            {
                yield return item;
            }
        }

        [HttpPost]
        public IAsyncEnumerable<GSL02500DTO> GSL02500GetCBList()
        {
            return GetGSL02500Stream();
        }
        private async IAsyncEnumerable<GSL02500DTO> GetGSL02500Stream()
        {
            using Activity activity = _activitySource.StartActivity("GSL02500GetCBList");
            var loEx = new R_Exception();
            List<GSL02500DTO> loRtn = null;
            _Logger.LogInfo("Start GSL02500GetCBList");

            try
            {
                var poParameter = new GSL02500ParameterDTO();

                _Logger.LogInfo("Set Param GSL02500GetCBList");
                var loCls = new PublicLookupCls();
                poParameter.CDEPT_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CDEPT_CODE);
                poParameter.CCB_TYPE = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CCB_TYPE);
                poParameter.CBANK_TYPE = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CBANK_TYPE);
                poParameter.CCURRENCY_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CCURRENCY_CODE);

                _Logger.LogInfo("Call Back Method GetALLCB");
                loRtn = await loCls.GetALLCB(poParameter);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GSL02500GetCBList");

            foreach (var item in loRtn)
            {
                yield return item;
            }
        }

        [HttpPost]
        public IAsyncEnumerable<GSL02600DTO> GSL02600GetCBAccountList()
        {
            return GetGSL02600Stream();
        }
        private async IAsyncEnumerable<GSL02600DTO> GetGSL02600Stream()
        {
            using Activity activity = _activitySource.StartActivity("GSL02600GetCBAccountList");
            var loEx = new R_Exception();
            List<GSL02600DTO> loRtn = null;
            _Logger.LogInfo("Start GSL02600GetCBAccountList");

            try
            {
                var poParameter = new GSL02600ParameterDTO();

                _Logger.LogInfo("Set Param GSL02600GetCBAccountList");
                var loCls = new PublicLookupCls();
                poParameter.CDEPT_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CDEPT_CODE);
                poParameter.CCB_TYPE = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CCB_TYPE);
                poParameter.CBANK_TYPE = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CBANK_TYPE);
                poParameter.CCB_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CCB_CODE);
                poParameter.CCURRENCY_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CCURRENCY_CODE);

                _Logger.LogInfo("Call Back Method GetALLCBAccount");
                loRtn = await loCls.GetALLCBAccount(poParameter);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GSL02600GetCBAccountList");

            foreach (var item in loRtn)
            {
                yield return item;
            }
        }

        [HttpPost]
        public IAsyncEnumerable<GSL02700DTO> GSL02700GetOtherUnitList()
        {
            return GetGSL02700Stream();
        }
        private async IAsyncEnumerable<GSL02700DTO> GetGSL02700Stream()
        {
            using Activity activity = _activitySource.StartActivity("GSL02700GetOtherUnitList");
            var loEx = new R_Exception();
            List<GSL02700DTO> loRtn = null;
            _Logger.LogInfo("Start GSL02700GetOtherUnitList");

            try
            {
                var poParameter = new GSL02700ParameterDTO();

                _Logger.LogInfo("Set Param GSL02700GetOtherUnitList");
                var loCls = new PublicLookupCls();
                poParameter.CBUILDING_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CBUILDING_ID);
                poParameter.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CPROPERTY_ID);
                poParameter.LEVENT = R_Utility.R_GetStreamingContext<bool>(ContextConstantPublicLookup.LEVENT);
                poParameter.LAGREEMENT = R_Utility.R_GetStreamingContext<bool>(ContextConstantPublicLookup.LAGREEMENT);
                poParameter.LBOTH_EVENT_CASUAL = R_Utility.R_GetStreamingContext<bool>(ContextConstantPublicLookup.LBOTH_EVENT_CASUAL);
                poParameter.CDEPT_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CDEPT_CODE);
                poParameter.CTRANS_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CTRANS_CODE);
                poParameter.CREF_NO = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CREF_NO);
                poParameter.CLEASE_STATUS_LIST = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CLEASE_STATUS_LIST);

                _Logger.LogInfo("Call Back Method GetALLOtherUnit");
                loRtn = await loCls.GetALLOtherUnit(poParameter);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GSL02700GetOtherUnitList");

            foreach (var item in loRtn)
            {
                yield return item;
            }
        }

        [HttpPost]
        public IAsyncEnumerable<GSL02800DTO> GSL02800GetOtherUnitMasterList()
        {
            return GetGSL02800Stream();
        }
        private async IAsyncEnumerable<GSL02800DTO> GetGSL02800Stream()
        {
            using Activity activity = _activitySource.StartActivity("GSL02800GetOtherUnitList");
            var loEx = new R_Exception();
            List<GSL02800DTO> loRtn = null;
            _Logger.LogInfo("Start GSL02800GetOtherUnitList");

            try
            {
                var poParameter = new GSL02800ParameterDTO();

                _Logger.LogInfo("Set Param GSL02800GetOtherUnitList");
                var loCls = new PublicLookupCls();
                poParameter.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CPROPERTY_ID);
                poParameter.LEVENT = R_Utility.R_GetStreamingContext<bool>(ContextConstantPublicLookup.LEVENT);

                _Logger.LogInfo("Call Back Method GetALLOtherUnit");
                loRtn = await loCls.GetALLOtherUnitMaster(poParameter);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GSL02800GetOtherUnitList");

            foreach (var item in loRtn)
            {
                yield return item;
            }
        }

        [HttpPost]
        public IAsyncEnumerable<GSL02510DTO> GSL02510GetCashBankList()
        {
            return GetGSL02510Stream();
        }
        private async IAsyncEnumerable<GSL02510DTO> GetGSL02510Stream()
        {
            using Activity activity = _activitySource.StartActivity("GSL02510GetCashBankList");
            var loEx = new R_Exception();
            List<GSL02510DTO> loRtn = null;
            _Logger.LogInfo("Start GSL02510GetCashBankList");

            try
            {
                var poParameter = new GSL02510ParameterDTO();

                _Logger.LogInfo("Set Param GSL02510GetCashBankList");
                var loCls = new PublicLookupCls();
                poParameter.CCB_TYPE = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CCB_TYPE);

                _Logger.LogInfo("Call Back Method GetALLCashBank");
                loRtn = await loCls.GetALLCashBank(poParameter);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GSL02510GetCashBankList");

            foreach (var item in loRtn)
            {
                yield return item;
            }
        }

        [HttpPost]
        public IAsyncEnumerable<GSL02900DTO> GSL02900GetSupplierList()
        {
            return GetGSL02900Stream();
        }
        private async IAsyncEnumerable<GSL02900DTO> GetGSL02900Stream()
        {
            using Activity activity = _activitySource.StartActivity("GSL02900GetSupplierList");
            var loEx = new R_Exception();
            List<GSL02900DTO> loRtn = null;
            _Logger.LogInfo("Start GSL02900GetSupplierList");

            try
            {
                var poParameter = new GSL02900ParameterDTO();

                _Logger.LogInfo("Set Param GSL02900GetSupplierList");
                var loCls = new PublicLookupCls();
                poParameter.CFILTER_SP = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CFILTER_SP);
                poParameter.CSTATUS_LIST = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CSTATUS_LIST);
                poParameter.CSUPPLIER_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CSUPPLIER_ID);

                _Logger.LogInfo("Call Back Method GetALLSupplier");
                loRtn = await loCls.GetALLSupplier(poParameter);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GSL02900GetSupplierList");

            foreach (var item in loRtn)
            {
                yield return item;
            }
        }

        [HttpPost]
        public IAsyncEnumerable<GSL02910DTO> GSL02910GetSupplierInfoList()
        {
            return GetGSL02910Stream();
        }
        private async IAsyncEnumerable<GSL02910DTO> GetGSL02910Stream()
        {
            using Activity activity = _activitySource.StartActivity("GSL02910GetSupplierInfoList");
            var loEx = new R_Exception();
            List<GSL02910DTO> loRtn = null;
            _Logger.LogInfo("Start GSL02910GetSupplierInfoList");

            try
            {
                var poParameter = new GSL02910ParameterDTO();

                _Logger.LogInfo("Set Param GSL02910GetSupplierInfoList");
                var loCls = new PublicLookupCls();
                poParameter.CSUPPLIER_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CSUPPLIER_ID);

                _Logger.LogInfo("Call Back Method GetALLSupplierInfo");
                loRtn = await loCls.GetALLSupplierInfo(poParameter);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GSL02910GetSupplierInfoList");

            foreach (var item in loRtn)
            {
                yield return item;
            }
        }

        [HttpPost]
        public IAsyncEnumerable<GSL03000DTO> GSL03000GetProductList()
        {
            return GetGSL03000Stream();
        }
        private async IAsyncEnumerable<GSL03000DTO> GetGSL03000Stream()
        {
            using Activity activity = _activitySource.StartActivity("GSL03000GetProductList");
            var loEx = new R_Exception();
            List<GSL03000DTO> loRtn = null;
            _Logger.LogInfo("Start GSL03000GetProductList");

            try
            {
                var poParameter = new GSL03000ParameterDTO();

                _Logger.LogInfo("Set Param GSL03000GetProductList");
                var loCls = new PublicLookupCls();
                poParameter.CCATEGORY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CCATEGORY_ID);
                poParameter.CTAXABLE_TYPE = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CTAXABLE_TYPE);
                poParameter.CACTIVE_TYPE = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CACTIVE_TYPE);
                poParameter.CTAX_DATE = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CTAX_DATE);
                poParameter.CBUYSELL = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CBUYSELL);
                poParameter.CINOUT = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CINOUT);

                _Logger.LogInfo("Call Back Method GetALLProduct");
                loRtn = await loCls.GetALLProduct(poParameter);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GSL03000GetProductList");

            foreach (var item in loRtn)
            {
                yield return item;
            }
        }

        [HttpPost]
        public IAsyncEnumerable<GSL03010DTO> GSL03010GetProductUnitList()
        {
            return GetGSL03010Stream();
        }
        private async IAsyncEnumerable<GSL03010DTO> GetGSL03010Stream()
        {
            using Activity activity = _activitySource.StartActivity("GSL03010GetProductUnitList");
            var loEx = new R_Exception();
            List<GSL03010DTO> loRtn = null;
            _Logger.LogInfo("Start GSL03010GetProductUnitList");

            try
            {
                var poParameter = new GSL03010ParameterDTO();

                _Logger.LogInfo("Set Param GSL03010GetProductUnitList");
                var loCls = new PublicLookupCls();
                poParameter.CPRODUCT_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CPRODUCT_ID);

                _Logger.LogInfo("Call Back Method GetALLProduct");
                loRtn = await loCls.GetALLProductUnit(poParameter);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GSL03010GetProductUnitList");

            foreach (var item in loRtn)
            {
                yield return item;
            }
        }

        [HttpPost]
        public IAsyncEnumerable<GSL03020DTO> GSL03020GetProductUOMList()
        {
            return GetGSL03020Stream();
        }
        private async IAsyncEnumerable<GSL03020DTO> GetGSL03020Stream()
        {
            using Activity activity = _activitySource.StartActivity("GSL03020GetProductUOMList");
            var loEx = new R_Exception();
            List<GSL03020DTO> loRtn = null;
            _Logger.LogInfo("Start GSL03020GetProductUOMList");

            try
            {
                var poParameter = new GSL03020ParameterDTO();

                _Logger.LogInfo("Set Param GSL03020GetProductUOMList");
                var loCls = new PublicLookupCls();
                poParameter.CPRODUCT_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CPRODUCT_ID);

                _Logger.LogInfo("Call Back Method GetALLProduct");
                loRtn = await loCls.GetALLProductUOM(poParameter);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GSL03020GetProductUOMList");

            foreach (var item in loRtn)
            {
                yield return item;
            }
        }

        [HttpPost]
        public IAsyncEnumerable<GSL03100DTO> GSL03100GetExpenditureList()
        {
            return GetGSL03100Stream();
        }
        private async IAsyncEnumerable<GSL03100DTO> GetGSL03100Stream()
        {
            using Activity activity = _activitySource.StartActivity("GSL03100GetExpenditureList");
            var loEx = new R_Exception();
            List<GSL03100DTO> loRtn = null;
            _Logger.LogInfo("Start GSL03100GetExpenditureList");

            try
            {
                var poParameter = new GSL03100ParameterDTO();

                _Logger.LogInfo("Set Param GSL03100GetExpenditureList");
                var loCls = new PublicLookupCls();
                poParameter.CCATEGORY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CCATEGORY_ID);
                poParameter.CTAXABLE_TYPE = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CTAXABLE_TYPE);
                poParameter.CACTIVE_TYPE = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CACTIVE_TYPE);
                poParameter.CTAX_DATE = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CTAX_DATE);

                _Logger.LogInfo("Call Back Method GetALLExpenditure");
                loRtn = await loCls.GetALLExpenditure(poParameter);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GSL03100GetExpenditureList");

            foreach (var item in loRtn)
            {
                yield return item;
            }
        }

        [HttpPost]
        public IAsyncEnumerable<GSL03200DTO> GSL03200GetProductAllocationList()
        {
            return GetGSL03200Stream();
        }
        private async IAsyncEnumerable<GSL03200DTO> GetGSL03200Stream()
        {
            using Activity activity = _activitySource.StartActivity("GSL03200GetProductAllocationList");
            var loEx = new R_Exception();
            List<GSL03200DTO> loRtn = null;
            _Logger.LogInfo("Start GSL03200GetProductAllocationList");

            try
            {
                var poParameter = new GSL03200ParameterDTO();

                _Logger.LogInfo("Set Param GSL03200GetProductAllocationList");
                var loCls = new PublicLookupCls();
                poParameter.CACTIVE_TYPE = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CACTIVE_TYPE);
                poParameter.CDEPT_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CDEPT_CODE);
                poParameter.CALLOC_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CALLOC_ID);

                _Logger.LogInfo("Call Back Method GetALLProductAllocation");
                loRtn = await loCls.GetALLProductAllocation(poParameter);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GSL03200GetProductAllocationList");

            foreach (var item in loRtn)
            {
                yield return item;
            }
        }

        [HttpPost]
        public IAsyncEnumerable<GSL03300DTO> GSL03300GetTaxChargesList()
        {
            return GetGSL03300Stream();
        }
        private async IAsyncEnumerable<GSL03300DTO> GetGSL03300Stream()
        {
            using Activity activity = _activitySource.StartActivity("GSL03300GetTaxChargesList");
            var loEx = new R_Exception();
            List<GSL03300DTO> loRtn = null;
            _Logger.LogInfo("Start GSL03300GetTaxChargesList");

            try
            {
                var poParameter = new GSL03300ParameterDTO();

                _Logger.LogInfo("Set Param GSL03300GetTaxChargesList");
                var loCls = new PublicLookupCls();
                poParameter.CREF_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CREF_CODE);
                poParameter.CREF_TYPE = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CREF_TYPE);

                _Logger.LogInfo("Call Back Method GetALLTaxCharges");
                loRtn = await loCls.GetALLTaxCharges(poParameter);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GSL03300GetTaxChargesList");

            foreach (var item in loRtn)
            {
                yield return item;
            }
        }

        [HttpPost]
        public IAsyncEnumerable<GSL03400DTO> GSL03400GetDigitalSignList()
        {
            return GetGSL03400Stream();
        }
        private async IAsyncEnumerable<GSL03400DTO> GetGSL03400Stream()
        {
            using Activity activity = _activitySource.StartActivity("GSL03400GetDigitalSignList");
            var loEx = new R_Exception();
            List<GSL03400DTO> loRtn = null;
            _Logger.LogInfo("Start GSL03400GetDigitalSignList");

            try
            {
                _Logger.LogInfo("Set Param GSL03400GetDigitalSignList");
                var loCls = new PublicLookupCls();

                _Logger.LogInfo("Call Back Method GetALLDigitalSign");
                loRtn = await loCls.GetALLDigitalSign();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GSL03400GetDigitalSignList");

            foreach (var item in loRtn)
            {
                yield return item;
            }
        }

        [HttpPost]
        public IAsyncEnumerable<GSL03500DTO> GSL03500GetWarehouseList()
        {
            return GetGSL03500Stream();
        }
        private async IAsyncEnumerable<GSL03500DTO> GetGSL03500Stream()
        {
            using Activity activity = _activitySource.StartActivity("GSL03500GetWarehouseList");
            var loEx = new R_Exception();
            List<GSL03500DTO> loRtn = null;
            _Logger.LogInfo("Start GSL03500GetWarehouseList");

            try
            {
                _Logger.LogInfo("Set Param GSL03500GetWarehouseList");
                var poParameter = new GSL03500ParameterDTO();
                poParameter.CACTIVE_TYPE = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CACTIVE_TYPE);
                poParameter.CWAREHOUSE_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CWAREHOUSE_ID);
                var loCls = new PublicLookupCls();

                _Logger.LogInfo("Call Back Method GetALLWarehouse");
                loRtn = await loCls.GetALLWarehouse(poParameter);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GSL03500GetWarehouseList");

            foreach (var item in loRtn)
            {
                yield return item;
            }
        }

        [HttpPost]
        public IAsyncEnumerable<GSL03600DTO> GSL03600GetCompanyList()
        {
            return GetGSL03600Stream();
        }
        private async IAsyncEnumerable<GSL03600DTO> GetGSL03600Stream()
        {
            using Activity activity = _activitySource.StartActivity("GSL03600GetCompanyList");
            var loEx = new R_Exception();
            List<GSL03600DTO> loRtn = null;
            _Logger.LogInfo("Start GSL03600GetCompanyList");

            try
            {
                var loCls = new PublicLookupCls();

                _Logger.LogInfo("Call Back Method GetALLWarehouse");
                loRtn = await loCls.GetALLCompany();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GSL03600GetCompanyList");

            foreach (var item in loRtn)
            {
                yield return item;
            }
        }

        [HttpPost]
        public IAsyncEnumerable<GSL03700DTO> GSL03700GetMessageList()
        {
            return GetGSL03700Stream();
        }
        private async IAsyncEnumerable<GSL03700DTO> GetGSL03700Stream()
        {
            using Activity activity = _activitySource.StartActivity("GSL03700GetMessageList");
            var loEx = new R_Exception();
            List<GSL03700DTO> loRtn = null;
            _Logger.LogInfo("Start GSL03700GetMessageList");

            try
            {
                var loCls = new PublicLookupCls();
                var loParameter = new GSL03700ParameterDTO();
                loParameter.CMESSAGE_TYPE = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CMESSAGE_TYPE);

                _Logger.LogInfo("Call Back Method GetALLMessage");
                loRtn = await loCls.GetALLMessage(loParameter);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GSL03700GetMessageList");

            foreach (var item in loRtn)
            {
                yield return item;
            }
        }

    }
}
