using BlazorMenu.Clients;
using BlazorMenu.Models;
using BlazorMenuCommon;
using R_BlazorFrontEnd.Exceptions;

namespace BlazorMenu.Services
{
    public class R_LoginService : R_ILoginService
    {
        public async Task<LoginDTO> Login(LoginModel poParam)
        {
            var loEx = new R_Exception();
            LoginDTO loResult = null;
            R_LoginServiceClient loClientWrapper = new R_LoginServiceClient();

            try
            {
                var loParam = new LoginDTO
                {
                    CCOMPANY_ID = poParam.CompanyId,
                    CUSER_ID = poParam.UserId,
                    CUSER_PASSWORD = poParam.Password
                };

                var loLogin = await loClientWrapper.LogonAsync(loParam);

                loResult = loLogin.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
    }
}
