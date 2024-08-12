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

        #region Stream Data
        private async IAsyncEnumerable<T> GetStream<T>(List<T> poParam)
        {
            foreach (var item in poParam)
            {
                yield return item;
            }
        }
        #endregion

        [HttpPost]
        public IAsyncEnumerable<GSL00100DTO> GSL00100GetSalesTaxList()
        {
            using Activity activity = _activitySource.StartActivity("GSL00100GetSalesTaxList");
            var loEx = new R_Exception();
            IAsyncEnumerable<GSL00100DTO> loRtn = null;
            _Logger.LogInfo("Start GSL00100GetSalesTaxList");

            try
            {
                var loCls = new PublicLookupCls();
                var poParameter = new GSL00100ParameterDTO();

                _Logger.LogInfo("Call Back Method GetALLSalesTax");
                var loResult = loCls.GetALLSalesTax();

                _Logger.LogInfo("Call Stream Method Data GSL00100GetSalesTaxList");
                loRtn = GetStream<GSL00100DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            _Logger.LogInfo("End GSL00100GetSalesTaxList");
            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<GSL00110DTO> GSL00110GetTaxByDateList()
        {
            using Activity activity = _activitySource.StartActivity("GSL00110GetTaxByDateList");
            var loEx = new R_Exception();
            IAsyncEnumerable<GSL00110DTO> loRtn = null;
            _Logger.LogInfo("Start GSL00110GetTaxByDateList");

            try
            {
                var loCls = new PublicLookupCls();
                var poParameter = new GSL00110ParameterDTO();

                _Logger.LogInfo("Set Param GSL00110GetTaxByDateList");
                poParameter.CTAX_DATE = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CTAX_DATE);

                _Logger.LogInfo("Call Back Method GetALLTaxByDate");
                var loResult = loCls.GetALLTaxByDate(poParameter);

                _Logger.LogInfo("Call Stream Method Data GSL00110GetTaxByDateList");
                loRtn = GetStream<GSL00110DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            _Logger.LogInfo("End GSL00110GetTaxByDateList");
            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<GSL00200DTO> GSL00200GetWithholdingTaxList()
        {
            using Activity activity = _activitySource.StartActivity("GSL00200GetWithholdingTaxList");
            var loEx = new R_Exception();
            IAsyncEnumerable<GSL00200DTO> loRtn = null;
            _Logger.LogInfo("Start GSL00200GetWithholdingTaxList");

            try
            {
                var loCls = new PublicLookupCls();
                var poParameter = new GSL00200ParameterDTO();

                _Logger.LogInfo("Set Param GSL00200GetWithholdingTaxList");
                poParameter.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CPROPERTY_ID);
                poParameter.CTAX_TYPE_LIST = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CTAX_TYPE_LIST);

                _Logger.LogInfo("Call Back Method GetALLWithholdingTax");
                var loResult = loCls.GetALLWithholdingTax(poParameter);

                _Logger.LogInfo("Call Stream Method Data GSL00200GetWithholdingTaxList");
                loRtn = GetStream<GSL00200DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GSL00200GetWithholdingTaxList");

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<GSL00300DTO> GSL00300GetCurrencyList()
        {
            using Activity activity = _activitySource.StartActivity("GSL00300GetCurrencyList");
            var loEx = new R_Exception();
            IAsyncEnumerable<GSL00300DTO> loRtn = null;
            _Logger.LogInfo("Start GSL00300GetCurrencyList");

            try
            {
                var loCls = new PublicLookupCls();
                GSL00300ParameterDTO loParam = new GSL00300ParameterDTO();

                _Logger.LogInfo("Call Back Method GetALLCurrency");
                var loResult = loCls.GetALLCurrency();

                _Logger.LogInfo("Call Stream Method Data GSL00300GetCurrencyList");
                loRtn = GetStream<GSL00300DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GSL00300GetCurrencyList");

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<GSL00400DTO> GSL00400GetJournalGroupList()
        {
            using Activity activity = _activitySource.StartActivity("GSL00400GetJournalGroupList");
            var loEx = new R_Exception();
            IAsyncEnumerable<GSL00400DTO> loRtn = null;
            _Logger.LogInfo("Start GSL00400GetJournalGroupList");

            try
            {
                var loCls = new PublicLookupCls();
                var poParameter =  new GSL00400ParameterDTO();

                _Logger.LogInfo("Set Param GSL00400GetJournalGroupList");
                poParameter.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CPROPERTY_ID);
                poParameter.CJRNGRP_TYPE = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CJRNGRP_TYPE);

                _Logger.LogInfo("Call Back Method GetALLJournalGroup");
                var loResult = loCls.GetALLJournalGroup(poParameter);

                _Logger.LogInfo("Call Stream Method Data GSL00400GetJournalGroupList");
                loRtn = GetStream<GSL00400DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GSL00400GetJournalGroupList");

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<GSL00500DTO> GSL00500GetGLAccountList()
        {
            using Activity activity = _activitySource.StartActivity("GSL00500GetGLAccountList");
            var loEx = new R_Exception();
            IAsyncEnumerable<GSL00500DTO> loRtn = null;
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
                var loResult = loCls.GetALLGLAccount(poParameter);

                _Logger.LogInfo("Call Stream Method Data GSL00500GetGLAccountList");
                loRtn = GetStream<GSL00500DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GSL00500GetGLAccountList");

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<GSL00510DTO> GSL00510GetCOAList()
        {
            using Activity activity = _activitySource.StartActivity("GSL00510GetCOAList");
            var loEx = new R_Exception();
            IAsyncEnumerable<GSL00510DTO> loRtn = null;
            _Logger.LogInfo("Start GSL00510GetCOAList");

            try
            {
                var loCls = new PublicLookupCls();
                var poParameter = new GSL00510ParameterDTO();

                _Logger.LogInfo("Set Param GSL00510GetCOAList");
                poParameter.CGLACCOUNT_TYPE = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CGLACCOUNT_TYPE);
                poParameter.LINACTIVE_COA = R_Utility.R_GetStreamingContext<bool>(ContextConstantPublicLookup.LINACTIVE_COA);

                _Logger.LogInfo("Call Back Method GetALLCOA");
                var loResult = loCls.GetALLCOA(poParameter);

                _Logger.LogInfo("Call Stream Method Data GSL00510GetCOAList");
                loRtn = GetStream<GSL00510DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GSL00510GetCOAList");

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<GSL00520DTO> GSL00520GetGOACOAList()
        {
            using Activity activity = _activitySource.StartActivity("GSL00520GetGOACOAList");
            var loEx = new R_Exception();
            IAsyncEnumerable<GSL00520DTO> loRtn = null;
            _Logger.LogInfo("Start GSL00520GetGOACOAList");

            try
            {
                var loCls = new PublicLookupCls();
                var poParameter = new GSL00520ParameterDTO();

                _Logger.LogInfo("Set Param GSL00520GetGOACOAList");
                poParameter.CGOA_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CGOA_CODE);

                _Logger.LogInfo("Call Back Method GetALLGOACOA");
                var loResult = loCls.GetALLGOACOA(poParameter);

                _Logger.LogInfo("Call Stream Method Data GSL00520GetGOACOAList");
                loRtn = GetStream<GSL00520DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GSL00520GetGOACOAList");

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<GSL00550DTO> GSL00550GetGOAList()
        {
            using Activity activity = _activitySource.StartActivity("GSL00550GetGOAList");
            var loEx = new R_Exception();
            IAsyncEnumerable<GSL00550DTO> loRtn = null;
            _Logger.LogInfo("Start GSL00550GetGOAList");

            try
            {
                var loCls = new PublicLookupCls();
                var poParameter = new GSL00550ParameterDTO();

                _Logger.LogInfo("Set Param GSL00550GetGOAList");

                _Logger.LogInfo("Call Back Method GetALLGOA");
                var loResult = loCls.GetALLGOA();

                _Logger.LogInfo("Call Stream Method Data GSL00550GetGOAList");
                loRtn = GetStream<GSL00550DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GSL00550GetGOAList");

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<GSL00600DTO> GSL00600GetUnitTypeCategoryList()
        {
            using Activity activity = _activitySource.StartActivity("GSL00600GetUnitTypeCategoryList");
            var loEx = new R_Exception();
            _Logger.LogInfo("Start GSL00600GetUnitTypeCategoryList");
            IAsyncEnumerable<GSL00600DTO> loRtn = null;

            try
            {
                var loCls = new PublicLookupCls();
                var poParameter = new GSL00600ParameterDTO();

                _Logger.LogInfo("Set Param GSL00600GetUnitTypeCategoryList");
                poParameter.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CPROPERTY_ID);

                _Logger.LogInfo("Call Back Method GetALLUnitTypeCategory");
                var loResult = loCls.GetALLUnitTypeCategory(poParameter);

                _Logger.LogInfo("Call Stream Method Data GSL00600GetUnitTypeCategoryList");
                loRtn = GetStream<GSL00600DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GSL00600GetUnitTypeCategoryList");

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<GSL00700DTO> GSL00700GetDepartmentList()
        {
            using Activity activity = _activitySource.StartActivity("GSL00700GetDepartmentList");
            var loEx = new R_Exception();
            IAsyncEnumerable<GSL00700DTO> loRtn = null;
            _Logger.LogInfo("Start GSL00700GetDepartmentList");

            try
            {
                var loCls = new PublicLookupCls();
                var poParameter = new GSL00700ParameterDTO();

                _Logger.LogInfo("Set Param GSL00700GetDepartmentList");
                poParameter.CPROGRAM_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CPROGRAM_CODE);

                _Logger.LogInfo("Call Back Method GetALLDepartment");
                var loResult = loCls.GetALLDepartment(poParameter);

                _Logger.LogInfo("Call Stream Method Data GSL00700GetDepartmentList");
                loRtn = GetStream<GSL00700DTO>(loResult);

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GSL00700GetDepartmentList");

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<GSL00710DTO> GSL00710GetDepartmentPropertyList()
        {
            using Activity activity = _activitySource.StartActivity("GSL00710GetDepartmentPropertyList");
            var loEx = new R_Exception();
            IAsyncEnumerable<GSL00710DTO> loRtn = null;
            _Logger.LogInfo("Start GSL00710GetDepartmentPropertyList");

            try
            {
                var loCls = new PublicLookupCls();
                var poParameter = new GSL00710ParameterDTO();

                _Logger.LogInfo("Set Param GSL00710GetDepartmentPropertyList");
                poParameter.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CPROPERTY_ID);

                _Logger.LogInfo("Call Back Method GetALLDepartmentProperty");
                var loResult = loCls.GetALLDepartmentProperty(poParameter);

                _Logger.LogInfo("Call Stream Method Data GSL00710GetDepartmentPropertyList");
                loRtn = GetStream<GSL00710DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GSL00710GetDepartmentPropertyList");

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<GSL00800DTO> GSL00800GetCurrencyTypeList()
        {
            using Activity activity = _activitySource.StartActivity("GSL00800GetCurrencyTypeList");
            var loEx = new R_Exception();
            IAsyncEnumerable<GSL00800DTO> loRtn = null;
            _Logger.LogInfo("Start GSL00800GetCurrencyTypeList");

            try
            {
                var loCls = new PublicLookupCls();
                var poParameter = new GSL00800ParameterDTO();

                _Logger.LogInfo("Call Back Method GetALLCurrencyRateType");
                var loResult = loCls.GetALLCurrencyRateType();

                _Logger.LogInfo("Call Stream Method Data GSL00800GetCurrencyTypeList");
                loRtn = GetStream<GSL00800DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }
            _Logger.LogInfo("End GSL00800GetCurrencyTypeList");
            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<GSL00900DTO> GSL00900GetCenterList()
        {
            using Activity activity = _activitySource.StartActivity("GSL00900GetCenterList");
            var loEx = new R_Exception();
            IAsyncEnumerable<GSL00900DTO> loRtn = null;
            _Logger.LogInfo("Start GSL00900GetCenterList");

            try
            {
                var loCls = new PublicLookupCls();
                var poParameter = new GSL00900ParameterDTO();

                _Logger.LogInfo("Call Back Method GetALLCenter");
                var loResult = loCls.GetALLCenter();

                _Logger.LogInfo("Call Stream Method Data GSL00900GetCenterList");
                loRtn = GetStream<GSL00900DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GSL00900GetCenterList");

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<GSL01000DTO> GSL01000GetUserList()
        {
            using Activity activity = _activitySource.StartActivity("GSL01000GetUserList");
            var loEx = new R_Exception();
            IAsyncEnumerable<GSL01000DTO> loRtn = null;
            _Logger.LogInfo("Start GSL01000GetUserList");

            try
            {
                var loCls = new PublicLookupCls();

                _Logger.LogInfo("Set Param GSL01000GetUserList");
                _Logger.LogInfo("Call Back Method GetALLUser");
                var loResult = loCls.GetALLUser();

                _Logger.LogInfo("Call Stream Method Data GSL01000GetUserList");
                loRtn = GetStream<GSL01000DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GSL01000GetUserList");

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<GSL01100DTO> GSL01100GetUserApprovalList()
        {
            using Activity activity = _activitySource.StartActivity("GSL01100GetUserApprovalList");
            var loEx = new R_Exception();
            IAsyncEnumerable<GSL01100DTO> loRtn = null;
            _Logger.LogInfo("Start GSL01100GetUserApprovalList");

            try
            {
                var loCls = new PublicLookupCls();
                var poParameter = new GSL01100ParameterDTO();

                _Logger.LogInfo("Set Param GSL01100GetUserApprovalList");
                poParameter.CPARAMETER_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CPARAMETER_ID);
                poParameter.CPROGRAM_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CPROGRAM_ID);

                _Logger.LogInfo("Call Back Method GetALLUserApproval");
                var loResult = loCls.GetALLUserApproval(poParameter);

                _Logger.LogInfo("Call Stream Method Data GSL01100GetUserApprovalList");
                loRtn = GetStream<GSL01100DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GSL01100GetUserApprovalList");

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<GSL01200DTO> GSL01200GetBankList()
        {
            using Activity activity = _activitySource.StartActivity("GSL01200GetBankList");
            var loEx = new R_Exception();
            IAsyncEnumerable<GSL01200DTO> loRtn = null;
            _Logger.LogInfo("Start GSL01200GetBankList");

            try
            {
                var loCls = new PublicLookupCls();
                var poParameter = new GSL01200ParameterDTO();

                _Logger.LogInfo("Set Param GSL01200GetBankList");
                poParameter.CCB_TYPE = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CCB_TYPE);

                _Logger.LogInfo("Call Back Method GetALLBank");
                var loResult = loCls.GetALLBank(poParameter);

                _Logger.LogInfo("Call Stream Method Data GSL01200GetBankList");
                loRtn = GetStream<GSL01200DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GSL01200GetBankList");

            return loRtn;
        }
        
        [HttpPost]
        public IAsyncEnumerable<GSL01300DTO> GSL01300GetBankAccountList()
        {
            using Activity activity = _activitySource.StartActivity("GSL01300GetBankAccountList");
            var loEx = new R_Exception();
            IAsyncEnumerable<GSL01300DTO> loRtn = null;
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
                var loResult = loCls.GetALLBankAccount(poParameter);

                _Logger.LogInfo("Call Stream Method Data GSL01300GetBankAccountList");
                loRtn = GetStream<GSL01300DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GSL01300GetBankAccountList");

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<GSL01400DTO> GSL01400GetOtherChargesList()
        {
            using Activity activity = _activitySource.StartActivity("GSL01400GetOtherChargesList");
            var loEx = new R_Exception();
            IAsyncEnumerable<GSL01400DTO> loRtn = null;
            _Logger.LogInfo("Start GSL01400GetOtherChargesList");

            try
            {
                var loCls = new PublicLookupCls();
                var poParameter = new GSL01400ParameterDTO();

                _Logger.LogInfo("Set Param GSL01400GetOtherChargesList");
                poParameter.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CPROPERTY_ID);
                poParameter.CCHARGES_TYPE_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CCHARGES_TYPE_ID);

                _Logger.LogInfo("Call Back Method GetALLOtherCharges");
                var loResult = loCls.GetALLOtherCharges(poParameter);

                _Logger.LogInfo("Call Stream Method Data GSL01400GetOtherChargesList");
                loRtn = GetStream<GSL01400DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GSL01400GetOtherChargesList");

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<GSL01500ResultDetailDTO> GSL01500GetCashDetailList()
        {
            using Activity activity = _activitySource.StartActivity("GSL01500GetCashDetailList");
            var loEx = new R_Exception();
            IAsyncEnumerable<GSL01500ResultDetailDTO> loRtn = null;
            _Logger.LogInfo("Start GSL01500GetCashDetailList");

            try
            {
                var loCls = new PublicLookupCls();
                var poParameter = new GSL01500ParameterDetailDTO();

                _Logger.LogInfo("Set Param GSL01500GetCashDetailList");
                poParameter.CCASH_FLOW_GROUP_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CCASH_FLOW_GROUP_CODE);

                _Logger.LogInfo("Call Back Method GetALLCashFlowDetail");
                var loResult = loCls.GetALLCashFlowDetail(poParameter);

                _Logger.LogInfo("Call Stream Method Data GSL01500GetCashDetailList");
                loRtn = GetStream<GSL01500ResultDetailDTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GSL01500GetCashDetailList");

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<GSL01500ResultGroupDTO> GSL01500GetCashFlowGroupList()
        {
            using Activity activity = _activitySource.StartActivity("GSL01500GetCashFlowGroupList");
            var loEx = new R_Exception();
            IAsyncEnumerable<GSL01500ResultGroupDTO> loRtn = null;
            _Logger.LogInfo("Start GSL01500GetCashFlowGroupList");

            try
            {
                var loCls = new PublicLookupCls();
                var poParameter = new GSL01500ParameterGroupDTO();

                _Logger.LogInfo("Call Back Method GetALLCashFlowGroup");
                var loResult = loCls.GetALLCashFlowGroup(poParameter);

                _Logger.LogInfo("Call Stream Method Data GSL01500GetCashFlowGroupList");
                loRtn = GetStream<GSL01500ResultGroupDTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GSL01500GetCashFlowGroupList");

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<GSL01600DTO> GSL01600GetCashFlowGroupTypeList()
        {
            using Activity activity = _activitySource.StartActivity("GSL01600GetCashFlowGroupTypeList");
            var loEx = new R_Exception();
            IAsyncEnumerable<GSL01600DTO> loRtn = null;
            _Logger.LogInfo("Start GSL01600GetCashFlowGroupTypeList");

            try
            {
                var loCls = new PublicLookupCls();
                var poParameter = new GSL01600ParameterDTO();

                _Logger.LogInfo("Call Back Method GetALLCashFlowGruopType");
                var loResult = loCls.GetALLCashFlowGruopType();

                _Logger.LogInfo("Call Stream Method Data GSL01600GetCashFlowGroupTypeList");
                loRtn = GetStream<GSL01600DTO>(loResult);

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GSL01600GetCashFlowGroupTypeList");

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<GSL01700DTO> GSL01700GetCurrencyRateList()
        {
            using Activity activity = _activitySource.StartActivity("GSL01700GetCurrencyRateList");
            var loEx = new R_Exception();
            IAsyncEnumerable<GSL01700DTO> loRtn = null;
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
                var loResult = loCls.GetALLCurrencyRate(poParameter);

                _Logger.LogInfo("Call Stream Method Data GSL01700GetCurrencyRateList");
                loRtn = GetStream<GSL01700DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GSL01700GetCurrencyRateList");

            return loRtn;
        }
        
        [HttpPost]
        public IAsyncEnumerable<GSL01701DTO> GSL01700GetRateTypeList()
        {
            using Activity activity = _activitySource.StartActivity("GSL01700GetRateTypeList");
            var loEx = new R_Exception();
            IAsyncEnumerable<GSL01701DTO> loRtn = null;
            var loParam = new GSL01700DTOParameter();
            _Logger.LogInfo("Start GSL01700GetRateTypeList");

            try
            {
                var loCls = new PublicLookupCls();

                _Logger.LogInfo("Call Back Method GetALLRateType");
                var loResult = loCls.GetALLRateType();

                _Logger.LogInfo("Call Stream Method Data GSL01700GetRateTypeList");
                loRtn = GetStream<GSL01701DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GSL01700GetRateTypeList");

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<GSL01702DTO> GSL01700GetLocalAndBaseCurrencyList()
        {
            using Activity activity = _activitySource.StartActivity("GSL01700GetLocalAndBaseCurrencyList");
            var loEx = new R_Exception();
            IAsyncEnumerable<GSL01702DTO> loRtn = null;
            var loParam = new GSL01700DTOParameter();
            _Logger.LogInfo("Start GSL01700GetLocalAndBaseCurrencyList");

            try
            {
                var loCls = new PublicLookupCls();

                _Logger.LogInfo("Call Back Method GetALLLocalAndBaseCurrency");
                var loResult = loCls.GetALLLocalAndBaseCurrency();

                _Logger.LogInfo("Call Stream Method Data GSL01700GetLocalAndBaseCurrencyList");
                loRtn = GetStream<GSL01702DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GSL01700GetLocalAndBaseCurrencyList");

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<GSL01800DTO> GSL01800GetCategoryList()
        {
            using Activity activity = _activitySource.StartActivity("GSL01800GetCategoryList");
            var loEx = new R_Exception();
            IAsyncEnumerable<GSL01800DTO> loRtn = null;
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
                var loResult = loCls.GetALLCategory(poParameter);

                _Logger.LogInfo("Call Stream Method Data GSL01800GetCategoryList");
                loRtn = GetStream<GSL01800DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GSL01800GetCategoryList");

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<GSL01900DTO> GSL01900GetLOBList()
        {
            using Activity activity = _activitySource.StartActivity("GSL01900GetLOBList");
            var loEx = new R_Exception();
            IAsyncEnumerable<GSL01900DTO> loRtn = null;
            _Logger.LogInfo("Start GSL01900GetLOBList");

            try
            {

                var loCls = new PublicLookupCls();

                _Logger.LogInfo("Call Back Method GetALLLOB");
                var loResult = loCls.GetALLLOB();

                _Logger.LogInfo("Call Stream Method Data GSL01900GetLOBList");
                loRtn = GetStream<GSL01900DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GSL01900GetLOBList");

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<GSL02000CountryDTO> GSL02000GetCountryGeographyList()
        {
            using Activity activity = _activitySource.StartActivity("GSL02000GetCountryGeographyList");
            var loEx = new R_Exception();
            IAsyncEnumerable<GSL02000CountryDTO> loRtn = null;
            _Logger.LogInfo("Start GSL02000GetCountryGeographyList");

            try
            {

                var loCls = new PublicLookupCls();

                _Logger.LogInfo("Call Back Method GetALLCountryGeography");
                var loResult = loCls.GetALLCountryGeography();

                _Logger.LogInfo("Call Stream Method Data GSL02000GetCountryGeographyList");
                loRtn = GetStream<GSL02000CountryDTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GSL02000GetCountryGeographyList");

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<GSL02000CityDTO> GSL02000GetCityGeographyList()
        {
            using Activity activity = _activitySource.StartActivity("GSL02000GetCityGeographyList");
            var loEx = new R_Exception();
            IAsyncEnumerable<GSL02000CityDTO> loRtn = null;
            _Logger.LogInfo("Start GSL02000GetCityGeographyList");

            try
            {
                var poParameter = new GSL02000CityDTO();

                _Logger.LogInfo("Set Param GSL02000GetCityGeographyList");
                var loCls = new PublicLookupCls();
                poParameter.CUSER_ID = R_BackGlobalVar.USER_ID;
                poParameter.CCOUNTRY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CCOUNTRY_ID);

                _Logger.LogInfo("Call Back Method GetALLCityGeography");
                var loResult = loCls.GetALLCityGeography(poParameter);

                _Logger.LogInfo("Call Stream Method Data GSL02000GetCityGeographyList");
                loRtn = GetStream<GSL02000CityDTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GSL02000GetCityGeographyList");

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<GSL02100DTO> GSL02100GetPaymentTermList()
        {
            using Activity activity = _activitySource.StartActivity("GSL02100GetPaymentTermList");
            var loEx = new R_Exception();
            IAsyncEnumerable<GSL02100DTO> loRtn = null;
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
                var loResult = loCls.GetALLPaymentTerm(poParameter);

                _Logger.LogInfo("Call Stream Method Data GSL02100GetPaymentTermList");
                loRtn = GetStream<GSL02100DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GSL02100GetPaymentTermList");

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<GSL02200DTO> GSL02200GetBuildingList()
        {
            using Activity activity = _activitySource.StartActivity("GSL02200GetBuildingList");
            var loEx = new R_Exception();
            IAsyncEnumerable<GSL02200DTO> loRtn = null;
            _Logger.LogInfo("Start GSL02200GetBuildingList");

            try
            {
                var poParameter = new GSL02200ParameterDTO();

                _Logger.LogInfo("Set Param GSL02100GetPaymentTermList");
                var loCls = new PublicLookupCls();
                poParameter.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CPROPERTY_ID);
                poParameter.LAGREEMENT = R_Utility.R_GetStreamingContext<bool>(ContextConstantPublicLookup.LAGREEMENT);

                _Logger.LogInfo("Call Back Method GetALLBuilding");
                var loResult = loCls.GetALLBuilding(poParameter);

                _Logger.LogInfo("Call Stream Method Data GSL02100GetPaymentTermList");
                loRtn = GetStream<GSL02200DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GSL02200GetBuildingList");

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<GSL02300DTO> GSL02300GetBuildingUnitList()
        {
            using Activity activity = _activitySource.StartActivity("GSL02300GetBuildingUnitList");
            var loEx = new R_Exception();
            IAsyncEnumerable<GSL02300DTO> loRtn = null;
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
                poParameter.CPROGRAM_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CPROGRAM_ID);
                poParameter.CTRANS_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CTRANS_CODE);
                poParameter.CREF_NO = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CREF_NO);

                _Logger.LogInfo("Call Back Method GetALLBuilding");
                var loResult = loCls.GetALLBuildingUnit(poParameter);

                _Logger.LogInfo("Call Stream Method Data GSL02300GetBuildingUnitList");
                loRtn = GetStream<GSL02300DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GSL02300GetBuildingUnitList");

            return loRtn;
        }
        
        [HttpPost]
        public IAsyncEnumerable<GSL02400DTO> GSL02400GetFloorList()
        {
            using Activity activity = _activitySource.StartActivity("GSL02400GetFloorList");
            var loEx = new R_Exception();
            IAsyncEnumerable<GSL02400DTO> loRtn = null;
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
                var loResult = loCls.GetALLFloor(poParameter);

                _Logger.LogInfo("Call Stream Method Data GSL02400GetFloorList");
                loRtn = GetStream<GSL02400DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GSL02400GetFloorList");

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<GSL02500DTO> GSL02500GetCBList()
        {
            using Activity activity = _activitySource.StartActivity("GSL02500GetCBList");
            var loEx = new R_Exception();
            IAsyncEnumerable<GSL02500DTO> loRtn = null;
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
                var loResult = loCls.GetALLCB(poParameter);

                _Logger.LogInfo("Call Stream Method Data GSL02500GetCBList");
                loRtn = GetStream<GSL02500DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GSL02500GetCBList");

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<GSL02600DTO> GSL02600GetCBAccountList()
        {
            using Activity activity = _activitySource.StartActivity("GSL02600GetCBAccountList");
            var loEx = new R_Exception();
            IAsyncEnumerable<GSL02600DTO> loRtn = null;
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
                var loResult = loCls.GetALLCBAccount(poParameter);

                _Logger.LogInfo("Call Stream Method Data GSL02600GetCBAccountList");
                loRtn = GetStream<GSL02600DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GSL02600GetCBAccountList");

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<GSL02700DTO> GSL02700GetOtherUnitList()
        {
            using Activity activity = _activitySource.StartActivity("GSL02700GetOtherUnitList");
            var loEx = new R_Exception();
            IAsyncEnumerable<GSL02700DTO> loRtn = null;
            _Logger.LogInfo("Start GSL02700GetOtherUnitList");

            try
            {
                var poParameter = new GSL02700ParameterDTO();

                _Logger.LogInfo("Set Param GSL02700GetOtherUnitList");
                var loCls = new PublicLookupCls();
                poParameter.CBUILDING_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CBUILDING_ID);
                poParameter.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CPROPERTY_ID);
                poParameter.LEVENT = R_Utility.R_GetStreamingContext<bool>(ContextConstantPublicLookup.LEVENT);

                _Logger.LogInfo("Call Back Method GetALLOtherUnit");
                var loResult = loCls.GetALLOtherUnit(poParameter);

                _Logger.LogInfo("Call Stream Method Data GSL02700GetOtherUnitList");
                loRtn = GetStream<GSL02700DTO>(loResult);
            }
            catch (Exception ex)
            {   
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GSL02700GetOtherUnitList");

            return loRtn;
        }
    }
}
