using PQM00500COMMON;
using PQM00500COMMON.DTO_s;
using PQM00500COMMON.DTO_s.Base;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace PQM00500MODEL.View_Model_s
{
    public class PQM00500ViewModel : R_ViewModel<MenuUserDTO>
    {
        //var
        private PQM00500Model _initModel = new PQM00500Model();
        private PQM00510Model _menuModel = new PQM00510Model();
        private PQM00511Model _userMenuModel = new PQM00511Model();
        public ObservableCollection<CompanyInfoDTO> CompanyList { get; set; } = new ObservableCollection<CompanyInfoDTO>();
        public ObservableCollection<UserDTO> UserList { get; set; } = new ObservableCollection<UserDTO>();
        public ObservableCollection<MenuDTO> MenuList { get; set; } = new ObservableCollection<MenuDTO>();
        public ObservableCollection<MenuUserDTO> UserMenuList { get; set; } = new ObservableCollection<MenuUserDTO>();
        public MenuUserDTO UserMenuRecord { get; set; } = new MenuUserDTO();
        public string SelectedCompanyId = string.Empty;
        public string SelectedMenuId = string.Empty;

        //methods
        public async Task GetList_CompanyAsync()
        {
            var loEx = new R_Exception();
            try
            {
                var loResult = await _initModel.GetList_CompanyInfoAsync();
                if (loResult != null) { CompanyList = new ObservableCollection<CompanyInfoDTO>(loResult); }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        public async Task GetList_UserAsync()
        {
            var loEx = new R_Exception();
            try
            {
                R_FrontContext.R_SetStreamingContext(PQM00500ContextConstant.CCOMPANY_ID, SelectedCompanyId);

                var loResult = await _initModel.GetList_UserAsync();
                if (loResult != null) { UserList = new ObservableCollection<UserDTO>(loResult); }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        public async Task GetList_MenuAsync()
        {
            var loEx = new R_Exception();
            try
            {
                R_FrontContext.R_SetStreamingContext(PQM00500ContextConstant.CCOMPANY_ID, SelectedCompanyId);
                R_FrontContext.R_SetStreamingContext(PQM00500ContextConstant.CMENU_ID, "");
                var loResult = await _menuModel.GetList_MenuAsync();
                if (loResult != null)
                {
                    MenuList = new ObservableCollection<MenuDTO>(loResult);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        public async Task GetList_MenuUserAsync()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                R_FrontContext.R_SetStreamingContext(PQM00500ContextConstant.CCOMPANY_ID, SelectedCompanyId);
                R_FrontContext.R_SetStreamingContext(PQM00500ContextConstant.CMENU_ID, SelectedMenuId);
                R_FrontContext.R_SetStreamingContext(PQM00500ContextConstant.CUSER_ID, "");
                var loResult = await _userMenuModel.GetList_UserMenuAsync();
                if (loResult != null)
                {
                    UserMenuList = new ObservableCollection<MenuUserDTO>(loResult);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        public async Task<UserDTO> GetRecord_UserAsync(string pcUserId)
        {
            var loEx = new R_Exception();
            var loRtn = new UserDTO();
            try
            {
                loRtn = await _initModel.GetRecord_UserAsync(new UserDTO()
                {
                    CUSER_ID = pcUserId,
                    CCOMPANY_ID = SelectedCompanyId,
                });
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }

        //crud user menu
        public async Task GetRecord_UserMenuAsync(MenuUserDTO poParam)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                var loResult = await _userMenuModel.R_ServiceGetRecordAsync(poParam);
                if (loResult != null)
                {
                    UserMenuRecord = loResult;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        public async Task SaveRecord_UserMenuAsync(MenuUserDTO poNewEntity, eCRUDMode peCRUDMode)
        {
            var loEx = new R_Exception();
            try
            {
                switch (peCRUDMode)
                {
                    case eCRUDMode.AddMode:
                        poNewEntity.CACTION_MODE = "A";
                        break;

                    case eCRUDMode.EditMode:
                        poNewEntity.CACTION_MODE = "U";
                        break;
                }
                var loResult = await _userMenuModel.R_ServiceSaveAsync(poNewEntity, peCRUDMode);
                UserMenuRecord = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task DeleteRecord_UserMenuAsync(MenuUserDTO poParam)
        {
            var loEx = new R_Exception();
            try
            {
                if (poParam != null)
                {
                    poParam.CACTION_MODE = "D";
                    await _userMenuModel.R_ServiceDeleteAsync(poParam);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
    }
}
