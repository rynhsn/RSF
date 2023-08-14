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
        public GSLGenericList<GSL00100DTO> GSL00100GetSalesTaxList(GSL00100ParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            GSLGenericList<GSL00100DTO> loRtn = null;

            try
            {
                var loCls = new PublicLookupCls();
                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CUSER_ID = R_BackGlobalVar.USER_ID;

                var loResult = loCls.GetALLSalesTax(poParameter);

                loRtn = new GSLGenericList<GSL00100DTO> { Data = loResult };
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        [HttpPost]
        public GSLGenericList<GSL00200DTO> GSL00200GetWithholdingTaxList(GSL00200ParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            GSLGenericList<GSL00200DTO> loRtn = null;

            try
            {
                var loCls = new PublicLookupCls();
                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;

                var loResult = loCls.GetALLWithholdingTax(poParameter);

                loRtn = new GSLGenericList<GSL00200DTO> { Data = loResult };
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        [HttpPost]
        public GSLGenericList<GSL00300DTO> GSL00300GetCurrencyList()
        {
            var loEx = new R_Exception();
            GSLGenericList<GSL00300DTO> loRtn = null;

            try
            {
                var loCls = new PublicLookupCls();
                GSL00300ParameterDTO loParam = new GSL00300ParameterDTO();

                loParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                var loResult = loCls.GetALLCurrency(loParam);


                loRtn = new GSLGenericList<GSL00300DTO> { Data = loResult };
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        [HttpPost]
        public GSLGenericList<GSL00400DTO> GSL00400GetJournalGroupList(GSL00400ParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            GSLGenericList<GSL00400DTO> loRtn = null;

            try
            {
                var loCls = new PublicLookupCls();
                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;

                var loResult = loCls.GetALLJournalGroup(poParameter);

                loRtn = new GSLGenericList<GSL00400DTO> { Data = loResult };
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        [HttpPost]
        public GSLGenericList<GSL00500DTO> GSL00500GetGLAccountList(GSL00500ParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            GSLGenericList<GSL00500DTO> loRtn = null;

            try
            {
                var loCls = new PublicLookupCls();
                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CUSER_ID = R_BackGlobalVar.USER_ID;

                var loResult = loCls.GetALLGLAccount(poParameter);

                loRtn = new GSLGenericList<GSL00500DTO> { Data = loResult };
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        [HttpPost]
        public GSLGenericList<GSL00510DTO> GSL00510GetCOAList(GSL00510ParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            GSLGenericList<GSL00510DTO> loRtn = null;

            try
            {
                var loCls = new PublicLookupCls();
                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CUSER_ID = R_BackGlobalVar.USER_ID;

                var loResult = loCls.GetALLCOA(poParameter);

                loRtn = new GSLGenericList<GSL00510DTO> { Data = loResult };
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        [HttpPost]
        public GSLGenericList<GSL00520DTO> GSL00520GetGOACOAList(GSL00520ParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            GSLGenericList<GSL00520DTO> loRtn = null;

            try
            {
                var loCls = new PublicLookupCls();
                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;

                var loResult = loCls.GetALLGOACOA(poParameter);

                loRtn = new GSLGenericList<GSL00520DTO> { Data = loResult };
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        [HttpPost]
        public GSLGenericList<GSL00550DTO> GSL00550GetGOAList(GSL00550ParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            GSLGenericList<GSL00550DTO> loRtn = null;

            try
            {
                var loCls = new PublicLookupCls();
                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;

                var loResult = loCls.GetALLGOA(poParameter);

                loRtn = new GSLGenericList<GSL00550DTO> { Data = loResult };
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        [HttpPost]
        public GSLGenericList<GSL00600DTO> GSL00600GetUnitTypeCategoryList(GSL00600ParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            GSLGenericList<GSL00600DTO> loRtn = null;

            try
            {
                var loCls = new PublicLookupCls();
                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CUSER_ID = R_BackGlobalVar.USER_ID;

                var loResult = loCls.GetALLUnitTypeCategory(poParameter);

                loRtn = new GSLGenericList<GSL00600DTO> { Data = loResult };
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        [HttpPost]
        public GSLGenericList<GSL00700DTO> GSL00700GetDepartmentList(GSL00700ParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            GSLGenericList<GSL00700DTO> loRtn = null;

            try
            {
                var loCls = new PublicLookupCls();
                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CUSER_ID = R_BackGlobalVar.USER_ID;

                var loResult = loCls.GetALLDepartment(poParameter);

                loRtn = new GSLGenericList<GSL00700DTO> { Data = loResult };
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        [HttpPost]
        public GSLGenericList<GSL00800DTO> GSL00800GetCurrencyTypeList(GSL00800ParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            GSLGenericList<GSL00800DTO> loRtn = null;

            try
            {
                var loCls = new PublicLookupCls();
                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CUSER_ID = R_BackGlobalVar.USER_ID;

                var loResult = loCls.GetALLCurrencyRateType(poParameter);

                loRtn = new GSLGenericList<GSL00800DTO> { Data = loResult };
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        [HttpPost]
        public GSLGenericList<GSL00900DTO> GSL00900GetCenterList(GSL00900ParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            GSLGenericList<GSL00900DTO> loRtn = null;

            try
            {
                var loCls = new PublicLookupCls();
                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CUSER_ID = R_BackGlobalVar.USER_ID;

                var loResult = loCls.GetALLCenter(poParameter);

                loRtn = new GSLGenericList<GSL00900DTO> { Data = loResult };
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        [HttpPost]
        public GSLGenericList<GSL01000DTO> GSL01000GetUserList()
        {
            var loEx = new R_Exception();
            GSLGenericList<GSL01000DTO> loRtn = null;

            try
            {
                var loCls = new PublicLookupCls();

                var loResult = loCls.GetALLUser();

                loRtn = new GSLGenericList<GSL01000DTO> { Data = loResult };
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        [HttpPost]
        public GSLGenericList<GSL01100DTO> GSL01100GetUserApprovalList(GSL01100ParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            GSLGenericList<GSL01100DTO> loRtn = null;

            try
            {
                var loCls = new PublicLookupCls();
                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;

                var loResult = loCls.GetALLUserApproval(poParameter);

                loRtn = new GSLGenericList<GSL01100DTO> { Data = loResult };
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        [HttpPost]
        public GSLGenericList<GSL01200DTO> GSL01200GetBankList(GSL01200ParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            GSLGenericList<GSL01200DTO> loRtn = null;

            try
            {
                var loCls = new PublicLookupCls();
                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;

                var loResult = loCls.GetALLBank(poParameter);

                loRtn = new GSLGenericList<GSL01200DTO> { Data = loResult };
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }
        
        [HttpPost]
        public GSLGenericList<GSL01300DTO> GSL01300GetBankAccountList(GSL01300ParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            GSLGenericList<GSL01300DTO> loRtn = null;

            try
            {
                var loCls = new PublicLookupCls();
                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;

                var loResult = loCls.GetALLBankAccount(poParameter);

                loRtn = new GSLGenericList<GSL01300DTO> { Data = loResult };
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        [HttpPost]
        public GSLGenericList<GSL01400DTO> GSL01400GetOtherChargesList(GSL01400ParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            GSLGenericList<GSL01400DTO> loRtn = null;

            try
            {
                var loCls = new PublicLookupCls();
                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CUSER_LOGIN_ID = R_BackGlobalVar.USER_ID;

                var loResult = loCls.GetALLOtherCharges(poParameter);

                loRtn = new GSLGenericList<GSL01400DTO> { Data = loResult };
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        [HttpPost]
        public GSLGenericList<GSL01500ResultDetailDTO> GSL01500GetCashDetailList(GSL01500ParameterDetailDTO poParameter)
        {
            var loEx = new R_Exception();
            GSLGenericList<GSL01500ResultDetailDTO> loRtn = null;

            try
            {
                var loCls = new PublicLookupCls();
                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CUSER_ID = R_BackGlobalVar.USER_ID;

                var loResult = loCls.GetALLCashFlowDetail(poParameter);

                loRtn = new GSLGenericList<GSL01500ResultDetailDTO> { Data = loResult };
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        [HttpPost]
        public GSLGenericList<GSL01500ResultGroupDTO> GSL01500GetCashFlowGroupList(GSL01500ParameterGroupDTO poParameter)
        {
            var loEx = new R_Exception();
            GSLGenericList<GSL01500ResultGroupDTO> loRtn = null;

            try
            {
                var loCls = new PublicLookupCls();
                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CUSER_ID = R_BackGlobalVar.USER_ID;

                var loResult = loCls.GetALLCashFlowGroup(poParameter);

                loRtn = new GSLGenericList<GSL01500ResultGroupDTO> { Data = loResult };
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        [HttpPost]
        public GSLGenericList<GSL01600DTO> GSL01600GetCashFlowGroupTypeList(GSL01600ParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            GSLGenericList<GSL01600DTO> loRtn = null;

            try
            {
                var loCls = new PublicLookupCls();
                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CUSER_ID = R_BackGlobalVar.USER_ID;

                var loResult = loCls.GetALLCashFlowGruopType(poParameter);

                loRtn = new GSLGenericList<GSL01600DTO> { Data = loResult };
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        [HttpPost]
        public GSLGenericList<GSL01700DTO> GSL01700GetCurrencyRateList(GSL01700DTOParameter poParameter)
        {
            var loEx = new R_Exception();
            GSLGenericList<GSL01700DTO> loRtn = null;

            try
            {
                var loCls = new PublicLookupCls();
                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CUSER_ID = R_BackGlobalVar.USER_ID;

                var loResult = loCls.GetALLCurrencyRate(poParameter);

                loRtn = new GSLGenericList<GSL01700DTO> { Data = loResult };
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }
        
        [HttpPost]
        public GSLGenericList<GSL01701DTO> GSL01700GetRateTypeList()
        {
            var loEx = new R_Exception();
            GSLGenericList<GSL01701DTO> loRtn = null;
            var loParam = new GSL01700DTOParameter();

            try
            {
                var loCls = new PublicLookupCls();
                loParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;

                var loResult = loCls.GetALLRateType(loParam);

                loRtn = new GSLGenericList<GSL01701DTO> { Data = loResult };
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        [HttpPost]
        public GSLGenericList<GSL01702DTO> GSL01700GetLocalAndBaseCurrencyList()
        {
            var loEx = new R_Exception();
            GSLGenericList<GSL01702DTO> loRtn = null;
            var loParam = new GSL01700DTOParameter();

            try
            {
                var loCls = new PublicLookupCls();
                loParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;

                var loResult = loCls.GetALLLocalAndBaseCurrency(loParam);

                loRtn = new GSLGenericList<GSL01702DTO> { Data = loResult };
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }
    }
}
