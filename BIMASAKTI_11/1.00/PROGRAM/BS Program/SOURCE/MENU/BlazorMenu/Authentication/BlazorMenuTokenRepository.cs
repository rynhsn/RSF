using Blazored.LocalStorage;
using BlazorMenu.Constants.Storage;
using R_AuthenticationEnumAndInterface;
using System.IdentityModel.Tokens.Jwt;

namespace BlazorMenu.Authentication
{
    public class BlazorMenuTokenRepository : R_ITokenRepository
    {
        private readonly ISyncLocalStorageService _localStorageService;
        private string _RefreshTokenRequest = R_AuthenticationEnumerationConstant.R_DEFAULT_API_REFRESH_TOKEN_REQUEST_ENDPOINT;
        private double _RefreshTokenSpareInSeconds = R_AuthenticationEnumerationConstant.R_DEFAULT_API_REFRESH_TOKEN_SPARE_IN_SECONDS;

        public BlazorMenuTokenRepository(
            ISyncLocalStorageService localStorageService)
        {
            _localStorageService = localStorageService;
        }

        public void R_SetToken(string pcToken)
        {
            _localStorageService.SetItemAsString(StorageConstants.AuthToken, pcToken);
        }

        public string R_GetRefreshTokenRequest()
        {
            return _RefreshTokenRequest;
        }

        public bool R_IsTokenExpired()
        {
            JwtSecurityTokenHandler loTokenHandler = new JwtSecurityTokenHandler();
            JwtSecurityToken loSecurityToken = (JwtSecurityToken)loTokenHandler.ReadToken(R_GetToken());

            var llValid = ((loSecurityToken.ValidTo - DateTime.UtcNow).TotalSeconds < _RefreshTokenSpareInSeconds);
            return llValid;
        }

        public string R_GetToken()
        {
            return _localStorageService.GetItemAsString(StorageConstants.AuthToken);
        }
    }
}
