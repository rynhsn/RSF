using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using R_AuthenticationBack;
using R_Common;
using R_CommonFrontBackAPI;
using R_CrossPlatformSecurity;
using R_SecurityPolicyBack;
using R_SecurityPolicyCommon;
using R_SecurityTokenBack;
using R_SecurityTokenCommon;

namespace R_SecurityPolicyService
{
    [ApiController]
    [Route("api/[controller]/[action]"), AllowAnonymous]
    public class R_SecurityPolicyController : ControllerBase, IR_SecurityPolicy
    {
        private const string TOKEN = "Token";
        private const string TOKEN_EXPIRES = "TokenExpiresInMin";
        private const string REFRESH_TOKEN_EXPIRES = "RefreshTokenExpiresInDay";
        private const string REFRESH_TOKEN = "RefreshToken";
        private readonly R_TokenUtility _tokenUtility;
        private readonly R_ISymmetricProvider _symmetricProvider;

        public R_SecurityPolicyController(
            R_TokenUtility tokenUtility,
            R_ISymmetricProvider symmetricProvider)
        {
            _tokenUtility = tokenUtility;
            _symmetricProvider = symmetricProvider;
        }

        [HttpPost]
        public SecurityPolicyGenericResultDTO<R_SecurityPolicyParameterDTO> R_GetSecurityPolicyParameter()
        {
            var loEx = new R_Exception();
            SecurityPolicyGenericResultDTO<R_SecurityPolicyParameterDTO> loRtn = null;

            try
            {
                var loCls = new R_SecurityPolicyCls();

                var loSecurityParameter = loCls.R_GetSecurityPolicyParameter();

                loRtn = new SecurityPolicyGenericResultDTO<R_SecurityPolicyParameterDTO>() { Data = loSecurityParameter };
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        [HttpPost]
        public SecurityPolicyGenericResultDTO<R_SecurityPolicyTokenDTO> R_SecurityPolicyLogon(R_SecurityPolicyDTO poParameter)
        {
            var loEx = new R_Exception();
            SecurityPolicyGenericResultDTO<R_SecurityPolicyTokenDTO> loRtn = null;

            try
            {
                var lcDecryptedPassword = _symmetricProvider.TextDecrypt(poParameter.CUSER_PASSWORD, poParameter.CUSER_ID);
                var lcHashPassword = R_Utility.HashPassword(lcDecryptedPassword, poParameter.CUSER_ID);

                poParameter.CUSER_PASSWORD = lcHashPassword;

                var loCls = new R_SecurityPolicyCls();
                var llRtn = loCls.R_SecurityPolicyLogon(poParameter);

                if (!llRtn)
                    return new SecurityPolicyGenericResultDTO<R_SecurityPolicyTokenDTO>() { Data = new R_SecurityPolicyTokenDTO { CTOKEN = "" } };

                var loTokenResult = R_ConfigurationUtility.R_GetSection<R_TokenConfigurationResultDTO>(R_TokenConfiguration.TOKEN_SECTION_NAME);

                var loTokenParameter = new R_CreateTokenParameterDTO
                {
                    Key = loTokenResult.Token,
                    ExpiresInMin = loTokenResult.TokenExpiresInMin,
                    UserToken = new R_UserTokenDTO() { CompanyId = poParameter.CCOMPANY_ID.Trim(), UserId = poParameter.CUSER_ID, UserRole = "" }
                };

                var lcToken = _tokenUtility.R_CreateToken(loTokenParameter);

                var newRefreshToken = _tokenUtility.R_CreateRefreshToken(R_ConfigurationUtility.R_GetAppSettings<int>(REFRESH_TOKEN_EXPIRES));

                var loParamToken = new ParameterDTO
                {
                    CCOMPANY_ID = poParameter.CCOMPANY_ID,
                    CUSER_ID = poParameter.CUSER_ID,
                    CREFRESH_TOKEN = newRefreshToken.Token,
                    DREFRESH_TOKEN_UTC_CREATED = newRefreshToken.Created,
                    DREFRESH_TOKEN_UTC_EXPIRES = newRefreshToken.Expires,
                    CTOKEN_IP_ADDRESS = _tokenUtility.R_GetIpAddress()
                };

                var lcTokenId = SetRefreshToken(loParamToken);

                loRtn = new SecurityPolicyGenericResultDTO<R_SecurityPolicyTokenDTO>
                {
                    Data = new R_SecurityPolicyTokenDTO
                    {
                        CTOKEN = lcToken,
                        CTOKEN_ID = lcTokenId
                    }
                };
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        //[HttpPost]
        //public R_RefreshTokenResultDTO R_RefreshToken()
        //{
        //    var loEx = new R_Exception();
        //    R_RefreshTokenResultDTO loResult = null;

        //    try
        //    {
        //        var lcRefreshToken = Request.Cookies[REFRESH_TOKEN];
        //        var lcIpAddress = _tokenUtility.R_GetIpAddress();
        //        //get from context
        //        //var lcCompanyId = R_Context._GetInternalContext(R_InternalContextVarEnumerator.COMPANY_ID).ToString().Trim();
        //        //var lcUserId = R_Context._GetInternalContext(R_InternalContextVarEnumerator.USER_ID).ToString().Trim();

        //        var loTokenCls = new R_SecurityTokenCls();
        //        var loUser = loTokenCls.R_GetUserByRefreshToken(lcRefreshToken);

        //        if (loUser.CREFRESH_TOKEN == null)
        //            throw new Exception("Invalid refresh token");

        //        if (!loUser.CTOKEN_IP_ADDRESS.Equals(lcIpAddress, StringComparison.InvariantCultureIgnoreCase))
        //            throw new Exception("Invalid IP Address");

        //        if (loUser.DREFRESH_TOKEN_UTC_EXPIRES < DateTime.Now)
        //            throw new Exception("Token expired");

        //        var loTokenParameter = new R_CreateTokenParameterDTO
        //        {
        //            Key = R_ConfigurationUtility.R_GetAppSettings<string>(TOKEN),
        //            ExpiresInMin = R_ConfigurationUtility.R_GetAppSettings<int>(TOKEN_EXPIRES),
        //            UserToken = new R_UserTokenDTO() { CompanyId = loUser.CCOMPANY_ID, UserId = loUser.CUSER_ID, UserRole = "" }
        //        };

        //        var lcToken = _tokenUtility.R_CreateToken(loTokenParameter);

        //        R_CreateRefreshTokenResultDTO newRefreshToken = _tokenUtility.R_CreateRefreshToken(R_ConfigurationUtility.R_GetAppSettings<int>(REFRESH_TOKEN_EXPIRES));

        //        var loParamToken = new ParameterDTO
        //        {
        //            CUSER_ID = loUser.CUSER_ID,
        //            CCOMPANY_ID = loUser.CCOMPANY_ID,
        //            CREFRESH_TOKEN = newRefreshToken.Token,
        //            DREFRESH_TOKEN_UTC_CREATED = newRefreshToken.Created,
        //            DREFRESH_TOKEN_UTC_EXPIRES = newRefreshToken.Expires,
        //            CTOKEN_IP_ADDRESS = lcIpAddress
        //        };

        //        SetRefreshToken(loParamToken);

        //        loResult = new R_RefreshTokenResultDTO { data = new R_RefreshTokenResultData { token = lcToken } };
        //    }
        //    catch (Exception ex)
        //    {
        //        loEx.Add(ex);
        //    }

        //    loEx.ThrowExceptionIfErrors();

        //    return loResult;
        //}

        private string SetRefreshToken(ParameterDTO poParam)
        {
            var loEx = new R_Exception();
            string lcResult = "";

            try
            {
                var loTokenCls = new R_SecurityTokenCls();
                lcResult = loTokenCls.R_UpdateRefreshToken(poParam);

                var cookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    Expires = poParam.DREFRESH_TOKEN_UTC_EXPIRES,
                    IsEssential = true
                };

                Response.Cookies.Append(REFRESH_TOKEN, poParam.CREFRESH_TOKEN, cookieOptions);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return lcResult;
        }
    }
}