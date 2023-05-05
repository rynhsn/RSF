using BlazorMenuCommon;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_ContextFrontEnd;

namespace BlazorMenu.Clients
{
    public class R_MenuServiceClient : IBlazorMenu
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrl";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/BlazorMenu";
        private readonly R_ContextHeader _contextHeader;

        public R_MenuServiceClient(R_ContextHeader contextHeader)
        {
            _contextHeader = contextHeader;
        }

        #region GetMenuAccess
        public BlazorMenuResultDTO<List<MenuProgramAccessDTO>> GetMenuAccess(ParamDTO poParam)
        {
            throw new NotImplementedException();
        }

        public async Task<BlazorMenuResultDTO<List<MenuProgramAccessDTO>>> GetMenuAccessAsync(ParamDTO poParam)
        {
            var loEx = new R_Exception();
            BlazorMenuResultDTO<List<MenuProgramAccessDTO>> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<BlazorMenuResultDTO<List<MenuProgramAccessDTO>>, ParamDTO>
                    (DEFAULT_SERVICEPOINT_NAME,
                    nameof(IBlazorMenu.GetMenuAccess),
                    poParam,
                    true,
                    true);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        #endregion

        #region GetMenu
        public BlazorMenuResultDTO<List<MenuListDTO>> GetMenu(ParamDTO poParam)
        {
            throw new NotImplementedException();
        }

        public async Task<BlazorMenuResultDTO<List<MenuListDTO>>> GetMenuAsync(ParamDTO poParam)
        {
            var loEx = new R_Exception();
            BlazorMenuResultDTO<List<MenuListDTO>> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<BlazorMenuResultDTO<List<MenuListDTO>>, ParamDTO>
                    (DEFAULT_SERVICEPOINT_NAME,
                    nameof(IBlazorMenu.GetMenu),
                    poParam,
                    true,
                    true);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        #endregion

        public BlazorMenuResultDTO<List<UserCompanyDTO>> GetCompanyList(string pcUserId, string pcCompanyId)
        {
            throw new NotImplementedException();
        }

        public BlazorMenuResultDTO<List<InfoDTO>> GetInfo(string pcAppId)
        {
            throw new NotImplementedException();
        }

        public BlazorMenuResultDTO<string> GetProgramImage(MenuDTO poMenuDTO)
        {
            throw new NotImplementedException();
        }

        public BlazorMenuResultDTO SaveHistory(string pcCompId, string pcUserId, string pcProgId, string pcAction)
        {
            throw new NotImplementedException();
        }
    }
}
