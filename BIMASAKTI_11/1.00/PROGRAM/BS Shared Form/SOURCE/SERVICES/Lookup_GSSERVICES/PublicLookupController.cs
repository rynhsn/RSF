using Lookup_GSCOMMON;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSLBACK;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using R_BackEnd;
using R_Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lookup_GSSERVICES
{
    [ApiController]
    [Route("api/[controller]/[action]"), AllowAnonymous]
    public class PublicLookupController : ControllerBase, IPublicLookup
    {
        [HttpPost]
        public IAsyncEnumerable<GSL00100DTO> GSL00100GetSalesTaxList()
        {
            var loEx = new R_Exception();
            IAsyncEnumerable<GSL00100DTO> loRtn = null;

            try
            {
                var loCls = new PublicLookupCls();
                var poParameter = new GSL00100ParameterDTO();

                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CUSER_ID = R_BackGlobalVar.USER_ID;

                var loResult = loCls.GetALLSalesTax(poParameter);

                loRtn = GetStream<GSL00100DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<GSL00200DTO> GSL00200GetWithholdingTaxList()
        {
            var loEx = new R_Exception();
            IAsyncEnumerable<GSL00200DTO> loRtn = null;

            try
            {
                var loCls = new PublicLookupCls();
                var poParameter = new GSL00200ParameterDTO();

                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CPROPERTY_ID);
                poParameter.CTAX_TYPE_LIST = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CTAX_TYPE_LIST);

                var loResult = loCls.GetALLWithholdingTax(poParameter);

                loRtn = GetStream<GSL00200DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<GSL00300DTO> GSL00300GetCurrencyList()
        {
            var loEx = new R_Exception();
            IAsyncEnumerable<GSL00300DTO> loRtn = null;

            try
            {
                var loCls = new PublicLookupCls();
                GSL00300ParameterDTO loParam = new GSL00300ParameterDTO();

                loParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                var loResult = loCls.GetALLCurrency(loParam);

                loRtn = GetStream<GSL00300DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<GSL00400DTO> GSL00400GetJournalGroupList()
        {
            var loEx = new R_Exception();
            IAsyncEnumerable<GSL00400DTO> loRtn = null;

            try
            {
                var loCls = new PublicLookupCls();
                var poParameter =  new GSL00400ParameterDTO();

                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CPROPERTY_ID);
                poParameter.CJRNGRP_TYPE = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CJRNGRP_TYPE);

                var loResult = loCls.GetALLJournalGroup(poParameter);

                loRtn = GetStream<GSL00400DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<GSL00500DTO> GSL00500GetGLAccountList()
        {
            var loEx = new R_Exception();
            IAsyncEnumerable<GSL00500DTO> loRtn = null;

            try
            {
                var loCls = new PublicLookupCls();
                var poParameter = new GSL00500ParameterDTO();

                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CUSER_ID = R_BackGlobalVar.USER_ID;
                poParameter.CUSER_LANGUAGE = R_BackGlobalVar.CULTURE;
                poParameter.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CPROPERTY_ID);
                poParameter.CDBCR = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CDBCR);
                poParameter.CBSIS = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CBSIS);
                poParameter.LUSER_RESTR = R_Utility.R_GetStreamingContext<bool>(ContextConstantPublicLookup.LUSER_RESTR);
                poParameter.CCENTER_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CCENTER_CODE);
                poParameter.CPROGRAM_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CPROGRAM_CODE);
                poParameter.LCENTER_RESTR = R_Utility.R_GetStreamingContext<bool>(ContextConstantPublicLookup.LCENTER_RESTR);

                var loResult = loCls.GetALLGLAccount(poParameter);

                loRtn = GetStream<GSL00500DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<GSL00510DTO> GSL00510GetCOAList()
        {
            var loEx = new R_Exception();
            IAsyncEnumerable<GSL00510DTO> loRtn = null;

            try
            {
                var loCls = new PublicLookupCls();
                var poParameter = new GSL00510ParameterDTO();

                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CUSER_ID = R_BackGlobalVar.USER_ID;
                poParameter.CGLACCOUNT_TYPE = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CGLACCOUNT_TYPE);

                var loResult = loCls.GetALLCOA(poParameter);

                loRtn = GetStream<GSL00510DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<GSL00520DTO> GSL00520GetGOACOAList()
        {
            var loEx = new R_Exception();
            IAsyncEnumerable<GSL00520DTO> loRtn = null;

            try
            {
                var loCls = new PublicLookupCls();
                var poParameter = new GSL00520ParameterDTO();

                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CGOA_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CGOA_CODE);


                var loResult = loCls.GetALLGOACOA(poParameter);

                loRtn = GetStream<GSL00520DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<GSL00550DTO> GSL00550GetGOAList()
        {
            var loEx = new R_Exception();
            IAsyncEnumerable<GSL00550DTO> loRtn = null;

            try
            {
                var loCls = new PublicLookupCls();
                var poParameter = new GSL00550ParameterDTO();

                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;

                var loResult = loCls.GetALLGOA(poParameter);

                loRtn = GetStream<GSL00550DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<GSL00600DTO> GSL00600GetUnitTypeCategoryList()
        {
            var loEx = new R_Exception();
            IAsyncEnumerable<GSL00600DTO> loRtn = null;

            try
            {
                var loCls = new PublicLookupCls();
                var poParameter = new GSL00600ParameterDTO();

                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CUSER_ID = R_BackGlobalVar.USER_ID;
                poParameter.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CPROPERTY_ID);

                var loResult = loCls.GetALLUnitTypeCategory(poParameter);

                loRtn = GetStream<GSL00600DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<GSL00700DTO> GSL00700GetDepartmentList()
        {
            var loEx = new R_Exception();
            IAsyncEnumerable<GSL00700DTO> loRtn = null;

            try
            {
                var loCls = new PublicLookupCls();
                var poParameter = new GSL00700ParameterDTO();

                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CUSER_ID = R_BackGlobalVar.USER_ID;

                var loResult = loCls.GetALLDepartment(poParameter);

                loRtn = GetStream<GSL00700DTO>(loResult);

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<GSL00800DTO> GSL00800GetCurrencyTypeList()
        {
            var loEx = new R_Exception();
            IAsyncEnumerable<GSL00800DTO> loRtn = null;

            try
            {
                var loCls = new PublicLookupCls();
                var poParameter = new GSL00800ParameterDTO();

                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CUSER_ID = R_BackGlobalVar.USER_ID;

                var loResult = loCls.GetALLCurrencyRateType(poParameter);

                loRtn = GetStream<GSL00800DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<GSL00900DTO> GSL00900GetCenterList()
        {
            var loEx = new R_Exception();
            IAsyncEnumerable<GSL00900DTO> loRtn = null;

            try
            {
                var loCls = new PublicLookupCls();
                var poParameter = new GSL00900ParameterDTO();

                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CUSER_ID = R_BackGlobalVar.USER_ID;

                var loResult = loCls.GetALLCenter(poParameter);

                loRtn = GetStream<GSL00900DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<GSL01000DTO> GSL01000GetUserList()
        {
            var loEx = new R_Exception();
            IAsyncEnumerable<GSL01000DTO> loRtn = null;

            try
            {
                var loCls = new PublicLookupCls();

                var loResult = loCls.GetALLUser();

                loRtn = GetStream<GSL01000DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<GSL01100DTO> GSL01100GetUserApprovalList()
        {
            var loEx = new R_Exception();
            IAsyncEnumerable<GSL01100DTO> loRtn = null;

            try
            {
                var loCls = new PublicLookupCls();
                var poParameter = new GSL01100ParameterDTO();

                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CTRANSACTION_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CTRANSACTION_CODE);

                var loResult = loCls.GetALLUserApproval(poParameter);

                loRtn = GetStream<GSL01100DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<GSL01200DTO> GSL01200GetBankList()
        {
            var loEx = new R_Exception();
            IAsyncEnumerable<GSL01200DTO> loRtn = null;

            try
            {
                var loCls = new PublicLookupCls();
                var poParameter = new GSL01200ParameterDTO();

                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CCB_TYPE = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CCB_TYPE);

                var loResult = loCls.GetALLBank(poParameter);

                loRtn = GetStream<GSL01200DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }
        
        [HttpPost]
        public IAsyncEnumerable<GSL01300DTO> GSL01300GetBankAccountList()
        {
            var loEx = new R_Exception();
            IAsyncEnumerable<GSL01300DTO> loRtn = null;

            try
            {
                var loCls = new PublicLookupCls();
                var poParameter = new GSL01300ParameterDTO();

                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CBANK_TYPE = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CBANK_TYPE);
                poParameter.CCB_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CCB_CODE);
                poParameter.CDEPT_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CDEPT_CODE);

                var loResult = loCls.GetALLBankAccount(poParameter);

                loRtn = GetStream<GSL01300DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<GSL01400DTO> GSL01400GetOtherChargesList()
        {
            var loEx = new R_Exception();
            IAsyncEnumerable<GSL01400DTO> loRtn = null;

            try
            {
                var loCls = new PublicLookupCls();
                var poParameter = new GSL01400ParameterDTO();

                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CUSER_LOGIN_ID = R_BackGlobalVar.USER_ID;
                poParameter.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CPROPERTY_ID);
                poParameter.CCHARGES_TYPE_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CCHARGES_TYPE_ID);

                var loResult = loCls.GetALLOtherCharges(poParameter);

                loRtn = GetStream<GSL01400DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<GSL01500ResultDetailDTO> GSL01500GetCashDetailList()
        {
            var loEx = new R_Exception();
            IAsyncEnumerable<GSL01500ResultDetailDTO> loRtn = null;

            try
            {
                var loCls = new PublicLookupCls();
                var poParameter = new GSL01500ParameterDetailDTO();

                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CUSER_ID = R_BackGlobalVar.USER_ID;
                poParameter.CCASH_FLOW_GROUP_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CCASH_FLOW_GROUP_CODE);


                var loResult = loCls.GetALLCashFlowDetail(poParameter);

                loRtn = GetStream<GSL01500ResultDetailDTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<GSL01500ResultGroupDTO> GSL01500GetCashFlowGroupList()
        {
            var loEx = new R_Exception();
            IAsyncEnumerable<GSL01500ResultGroupDTO> loRtn = null;

            try
            {
                var loCls = new PublicLookupCls();
                var poParameter = new GSL01500ParameterGroupDTO();

                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CUSER_ID = R_BackGlobalVar.USER_ID;

                var loResult = loCls.GetALLCashFlowGroup(poParameter);

                loRtn = GetStream<GSL01500ResultGroupDTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<GSL01600DTO> GSL01600GetCashFlowGroupTypeList()
        {
            var loEx = new R_Exception();
            IAsyncEnumerable<GSL01600DTO> loRtn = null;

            try
            {
                var loCls = new PublicLookupCls();
                var poParameter = new GSL01600ParameterDTO();

                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CUSER_ID = R_BackGlobalVar.USER_ID;

                var loResult = loCls.GetALLCashFlowGruopType(poParameter);

                loRtn = GetStream<GSL01600DTO>(loResult);

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<GSL01700DTO> GSL01700GetCurrencyRateList()
        {
            var loEx = new R_Exception();
            IAsyncEnumerable<GSL01700DTO> loRtn = null;

            try
            {
                var loCls = new PublicLookupCls();
                var poParameter = new GSL01700DTOParameter();

                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CUSER_ID = R_BackGlobalVar.USER_ID;
                poParameter.CRATETYPE_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CRATETYPE_CODE);
                poParameter.CRATE_DATE = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CRATE_DATE);

                var loResult = loCls.GetALLCurrencyRate(poParameter);

                loRtn = GetStream<GSL01700DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }
        
        [HttpPost]
        public IAsyncEnumerable<GSL01701DTO> GSL01700GetRateTypeList()
        {
            var loEx = new R_Exception();
            IAsyncEnumerable<GSL01701DTO> loRtn = null;
            var loParam = new GSL01700DTOParameter();

            try
            {
                var loCls = new PublicLookupCls();
                loParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;

                var loResult = loCls.GetALLRateType(loParam);

                loRtn = GetStream<GSL01701DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<GSL01702DTO> GSL01700GetLocalAndBaseCurrencyList()
        {
            var loEx = new R_Exception();
            IAsyncEnumerable<GSL01702DTO> loRtn = null;
            var loParam = new GSL01700DTOParameter();

            try
            {
                var loCls = new PublicLookupCls();
                loParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;

                var loResult = loCls.GetALLLocalAndBaseCurrency(loParam);

                loRtn = GetStream<GSL01702DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<GSL01800DTO> GSL01800GetCategoryList()
        {
            var loEx = new R_Exception();
            IAsyncEnumerable<GSL01800DTO> loRtn = null;

            try
            {
                var loCls = new PublicLookupCls();
                var poParameter = new GSL01800DTOParameter();

                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CUSER_ID = R_BackGlobalVar.USER_ID;
                poParameter.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CPROPERTY_ID);
                poParameter.CCATEGORY_TYPE = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CCATEGORY_TYPE);

                var loResult = loCls.GetALLCategory(poParameter);

                loRtn = GetStream<GSL01800DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<GSL01900DTO> GSL01900GetLOBList()
        {
            var loEx = new R_Exception();
            IAsyncEnumerable<GSL01900DTO> loRtn = null;

            try
            {
                var poParameter = new GSL01900DTOParameter();

                var loCls = new PublicLookupCls();
                poParameter.CLOB_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CLOB_CODE);

                var loResult = loCls.GetALLLOB(poParameter);

                loRtn = GetStream<GSL01900DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        private async IAsyncEnumerable<T> GetStream<T>(List<T> poParam)
        {
            foreach (var item in poParam)
            {
                yield return item;
            }
        }

    }
}
