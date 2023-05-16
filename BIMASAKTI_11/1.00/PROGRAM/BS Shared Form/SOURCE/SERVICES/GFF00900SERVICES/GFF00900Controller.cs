using GFF00900BACK;
using GFF00900COMMON;
using GFF00900COMMON.DTOs;
using Microsoft.AspNetCore.Mvc;
using R_BackEnd;
using R_Common;
using R_CrossPlatformSecurity;

namespace GFF00900SERVICES
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class GFF00900Controller : ControllerBase, IGFF00900
    {
        private readonly R_ISymmetricProvider symmetricProvider;

        public GFF00900Controller(R_ISymmetricProvider symmetricProvider)
        {
            this.symmetricProvider = symmetricProvider;
        }

        [HttpPost]
        public RSP_ACTIVITY_VALIDITYResultDTO RSP_ACTIVITY_VALIDITYMethod()
        {
            R_Exception loException = new R_Exception();
            RSP_ACTIVITY_VALIDITYResultDTO loRtn = new RSP_ACTIVITY_VALIDITYResultDTO();
            RSP_ACTIVITY_VALIDITYParameterDTO loContext = new RSP_ACTIVITY_VALIDITYParameterDTO();
            GFF00900Cls loCls = new GFF00900Cls();

            try
            {
                loContext.ACTIVITY_CODE = R_Utility.R_GetContext<string>(ContextConstant.ACTIVITY_CODE_CONTEXT);
                loContext.COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loRtn.Data = loCls.RSP_ACTIVITY_VALIDITYMethod(loContext);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();

            return loRtn;
        }
/*
        public RSP_CREATE_ACTIVITY_APPROVAL_LOGResultDTO RSP_CREATE_ACTIVITY_APPROVAL_LOGMethod()
        {
            R_Exception loException = new R_Exception();
            RSP_CREATE_ACTIVITY_APPROVAL_LOGParameterDTO loParam = new RSP_CREATE_ACTIVITY_APPROVAL_LOGParameterDTO();
            RSP_CREATE_ACTIVITY_APPROVAL_LOGResultDTO loRtn = new RSP_CREATE_ACTIVITY_APPROVAL_LOGResultDTO();
            GFF00900Cls loCls = new GFF00900Cls();

            try
            {
                loParam.P_CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParam.P_CACTIVITY_USER_ID = R_BackGlobalVar.USER_ID;

                loCls.RSP_CREATE_ACTIVITY_APPROVAL_LOGMethod(loParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();

            return loRtn;
        }
*/
        [HttpPost]
        public ValidationResultDTO UsernameAndPasswordValidationMethod()
        {
            R_Exception loException = new R_Exception();
            GFF00900DTO loParam = new GFF00900DTO();
            ValidationResultDTO loRtn = new ValidationResultDTO();
            GFF00900Cls loCls = new GFF00900Cls();

            try
            {
                loParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParam.CUSER_ID = R_Utility.R_GetContext<string>(ContextConstant.VALIDATION_USER_CONTEXT);
                loParam.CPASSWORD = R_Utility.R_GetContext<string>(ContextConstant.VALIDATION_PASSWORD_CONTEXT);
                loParam.CACTION_CODE = R_Utility.R_GetContext<string>(ContextConstant.VALIDATION_ACTION_CODE_CONTEXT);
                loParam.DETAIL_ACTION = R_Utility.R_GetContext<string>(ContextConstant.ACTION_DETAIL_CONTEXT);
                loParam.CUSER_LOGIN_ID = R_BackGlobalVar.USER_ID;

                var lcDecrypt = symmetricProvider.TextDecrypt(loParam.CPASSWORD, loParam.CUSER_ID);
                loParam.CPASSWORD = R_Utility.HashPassword(lcDecrypt, loParam.CUSER_ID);

                loCls.UsernameAndPasswordValidationMethod(loParam);

                loCls.RSP_CREATE_ACTIVITY_APPROVAL_LOGMethod(loParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();

            return loRtn;
        }
    }
}
