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
                loParam.CUSER_LOGIN_ID = R_BackGlobalVar.USER_ID;

                var lcDecrypt = symmetricProvider.TextDecrypt(loParam.CPASSWORD, loParam.CUSER_ID);
                loParam.CPASSWORD = R_Utility.HashPassword(lcDecrypt, loParam.CUSER_ID);

                loCls.UsernameAndPasswordValidationMethod(loParam);
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
