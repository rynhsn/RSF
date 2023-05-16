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
    }
}
