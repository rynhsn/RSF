using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using R_AuthenticationBack;
using R_AuthenticationEnumAndInterface;
using R_Common;
using R_CommonFrontBackAPI;
using R_SecurityTokenBack;
using R_SecurityTokenCommon;

namespace R_SecurityTokenService
{
    [ApiController]
    [Route("api/[controller]/[action]"), AllowAnonymous]
    public class AuthController : ControllerBase, R_IAPIToken
    {
        private readonly R_TokenUtility _tokenUtility;

        private const string TOKEN = "Token";
        private const string TOKEN_EXPIRES = "TokenExpiresInMin";
        private const string REFRESH_TOKEN_EXPIRES = "RefreshTokenExpiresInDay";
        private const string REFRESH_TOKEN = "RefreshToken";

        public AuthController(R_TokenUtility tokenUtility)
        {
            _tokenUtility = tokenUtility;
        }

        [HttpPost]
        public R_RefreshTokenResultDTO R_RefreshToken()
        {
            var loEx = new R_Exception();
            R_RefreshTokenResultDTO loResult = null;

            try
            {
                var lcRefreshToken = Request.Cookies[REFRESH_TOKEN];
                var lcIpAddress = _tokenUtility.R_GetIpAddress();

                var loTokenCls = new R_SecurityTokenCls();
                var loUser = loTokenCls.R_GetUserByRefreshToken(lcRefreshToken);

                if (loUser == null)
                    throw new Exception("Invalid refresh token");

                if (loUser.CREFRESH_TOKEN == null)
                    throw new Exception("Invalid refresh token");

                if (!loUser.CTOKEN_IP_ADDRESS.Equals(lcIpAddress, StringComparison.InvariantCultureIgnoreCase))
                    throw new Exception("Invalid IP Address");

                if (loUser.DREFRESH_TOKEN_UTC_EXPIRES < DateTime.Now)
                    throw new Exception("Token expired");

                var loTokenParameter = new R_CreateTokenParameterDTO
                {
                    Key = R_ConfigurationUtility.R_GetAppSettings<string>(TOKEN),
                    ExpiresInMin = R_ConfigurationUtility.R_GetAppSettings<int>(TOKEN_EXPIRES),
                    UserToken = new R_UserTokenDTO() { CompanyId = loUser.CCOMPANY_ID, UserId = loUser.CUSER_ID, UserRole = "" }
                };

                var lcToken = _tokenUtility.R_CreateToken(loTokenParameter);

                R_CreateRefreshTokenResultDTO newRefreshToken = _tokenUtility.R_CreateRefreshToken(R_ConfigurationUtility.R_GetAppSettings<int>(REFRESH_TOKEN_EXPIRES));

                var loParamToken = new ParameterDTO
                {
                    CUSER_ID = loUser.CUSER_ID,
                    CCOMPANY_ID = loUser.CCOMPANY_ID,
                    CREFRESH_TOKEN = newRefreshToken.Token,
                    DREFRESH_TOKEN_UTC_CREATED = newRefreshToken.Created,
                    DREFRESH_TOKEN_UTC_EXPIRES = newRefreshToken.Expires,
                    CTOKEN_IP_ADDRESS = lcIpAddress
                };

                SetRefreshToken(loParamToken);

                loResult = new R_RefreshTokenResultDTO { data = new R_RefreshTokenResultData { token = lcToken } };
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        private void SetRefreshToken(ParameterDTO poParam)
        {
            var loEx = new R_Exception();

            try
            {
                var loTokenCls = new R_SecurityTokenCls();
                loTokenCls.R_UpdateRefreshToken(poParam);

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
        }
    }
}
