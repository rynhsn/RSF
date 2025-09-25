using BlazorMenuBack;
using BlazorMenuCommon;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using R_Common;

namespace BlazorMenuService
{
    [ApiController]
    [Route("api/[controller]/[action]"), AllowAnonymous]
    public class BlazorMenuController : ControllerBase, IBlazorLogin, IBlazorMenu
    {
        #region IBlazorLogin Implementation
        [HttpPost]
        public BlazorMenuResultDTO<LoginDTO> Logon(LoginDTO poParameter)
        {
            var loEx = new R_Exception();
            var loRtn = new BlazorMenuResultDTO<LoginDTO>();

            try
            {
                var loCls = new LoginCls();

                var loResult = loCls.Logon(poParameter);

                loRtn.Data = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        [HttpPost]
        public BlazorMenuResultDTO UserLockingFlush(LoginDTO poParameter)
        {
            var loEx = new R_Exception();
            BlazorMenuResultDTO loRtn = null;

            try
            {
                var loCls = new LoginCls();

                loCls.UserLockingFlush(poParameter);

                loRtn = new BlazorMenuResultDTO();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }
        #endregion

        #region IBlazorMenu Implementation
        [HttpPost]
        public BlazorMenuResultDTO<List<MenuProgramAccessDTO>> GetMenuAccess(ParamDTO poParam)
        {
            var loEx = new R_Exception();
            var loRtn = new BlazorMenuResultDTO<List<MenuProgramAccessDTO>>();

            try
            {
                var loCls = new MenuCls();

                var loResult = loCls.GetMenuAccess(poParam);

                loRtn.Data = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        [HttpPost]
        public BlazorMenuResultDTO<List<MenuListDTO>> GetMenu(ParamDTO poParam)
        {
            var loEx = new R_Exception();
            var loRtn = new BlazorMenuResultDTO<List<MenuListDTO>>();

            try
            {
                var loCls = new MenuCls();

                var loResult = loCls.GetMenu(poParam);

                loRtn.Data = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        [HttpPost]
        public BlazorMenuResultDTO<string> GetProgramImage(MenuDTO poMenuDTO)
        {
            var loEx = new R_Exception();
            var loRtn = new BlazorMenuResultDTO<string>();

            try
            {
                var loCls = new MenuCls();

                var loResult = loCls.GetProgramImage(poMenuDTO);

                loRtn.Data = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        [HttpPost]
        public BlazorMenuResultDTO<List<UserCompanyDTO>> GetCompanyList(string pcUserId, string pcCompanyId)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public BlazorMenuResultDTO SaveHistory(string pcCompId, string pcUserId, string pcProgId, string pcAction)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public BlazorMenuResultDTO<List<InfoDTO>> GetInfo(string pcAppId)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}