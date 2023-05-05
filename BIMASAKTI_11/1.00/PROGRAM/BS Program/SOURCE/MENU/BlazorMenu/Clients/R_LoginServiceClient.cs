using BlazorMenuCommon;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;

namespace BlazorMenu.Clients
{
    public class R_LoginServiceClient : IBlazorLogin
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrl";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/BlazorMenu";

        #region Logon
        public BlazorMenuResultDTO<LoginDTO> Logon(LoginDTO poParameter)
        {
            throw new NotImplementedException();
        }

        public async Task<BlazorMenuResultDTO<LoginDTO>> LogonAsync(LoginDTO poParameter)
        {
            var loEx = new R_Exception();
            BlazorMenuResultDTO<LoginDTO> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;

                loResult = await R_HTTPClientWrapper.R_APIRequestObject<BlazorMenuResultDTO<LoginDTO>, LoginDTO>(DEFAULT_SERVICEPOINT_NAME, nameof(IBlazorLogin.Logon), poParameter, true, false);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        #endregion

        #region UserLockingFlush
        public BlazorMenuResultDTO UserLockingFlush(LoginDTO poParameter)
        {
            throw new NotImplementedException();
        }

        public async Task<BlazorMenuResultDTO> UserLockingFlushAsync(LoginDTO poParameter)
        {
            var loEx = new R_Exception();
            BlazorMenuResultDTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;

                loResult = await R_HTTPClientWrapper.R_APIRequestObject<BlazorMenuResultDTO, LoginDTO>(DEFAULT_SERVICEPOINT_NAME, nameof(IBlazorLogin.UserLockingFlush), poParameter, true, false);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        #endregion
    }
}
