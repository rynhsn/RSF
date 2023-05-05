using BlazorClientHelper;
using BlazorMenu.Clients;
using BlazorMenuCommon;
using R_BlazorFrontEnd.Configurations;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.State;
using R_ContextFrontEnd;

namespace BlazorMenu.Services
{
    public class R_MenuService : R_IMenuService
    {
        private readonly IClientHelper _clientHelper;
        private readonly R_AccessStateContainer _stateContainer;
        private R_MenuServiceClient _clientWrapper = null;
        public Dictionary<string, string[]> MenuAccess { get; private set; }

        public string[] MenuIdList { get; private set; }

        public R_MenuService(IClientHelper clientHelper,
            R_AccessStateContainer stateContainer,
            R_ContextHeader contextHeader)
        {
            _clientHelper = clientHelper;
            _stateContainer = stateContainer;
            _clientWrapper = new R_MenuServiceClient(contextHeader);
        }

        public async Task SetMenuAccessAsync()
        {
            var loEx = new R_Exception();
            List<MenuProgramAccessDTO> loResult = null;

            try
            {
                var loParam = new ParamDTO
                {
                    CCOMPANY_ID = _clientHelper.CompanyId,
                    CUSER_ID = _clientHelper.UserId,
                    CLANGUAGE_ID = "en"
                };

                var loMenuAccess = await _clientWrapper.GetMenuAccessAsync(loParam);

                loResult = loMenuAccess.Data;

                MenuAccess = loResult.ToDictionary(x => x.CPROGRAM_ID, x => x.CACCESS_ID.Split(','));

                _stateContainer.SetValue(MenuAccess);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task<List<MenuListDTO>> GetMenuAsync()
        {
            var loEx = new R_Exception();
            List<MenuListDTO> loResult = null;

            try
            {
                var loParam = new ParamDTO
                {
                    CCOMPANY_ID = _clientHelper.CompanyId,
                    CUSER_ID = _clientHelper.UserId,
                    CLANGUAGE_ID = "en",
                    CMENU_ID = "",
                    CSUB_MENU_ID = "",
                    ILEVEL = 1,
                    CMODUL_ID = R_FrontConfig.R_GetConfigAsString("R_Md")
                };

                var loMenuList = await _clientWrapper.GetMenuAsync(loParam);

                loResult = loMenuList.Data;
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
