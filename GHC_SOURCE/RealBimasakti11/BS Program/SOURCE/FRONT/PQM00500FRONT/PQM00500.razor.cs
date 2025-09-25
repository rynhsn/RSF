using BlazorClientHelper;
using Microsoft.AspNetCore.Components;
using PQM00500COMMON;
using PQM00500COMMON.DTO_s;
using PQM00500MODEL.View_Model_s;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Enums;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_BlazorFrontEnd.Interfaces;
using R_CommonFrontBackAPI;
using R_LockingFront;
using System.Threading.Tasks;

namespace PQM00500FRONT
{
    public partial class PQM00500 : R_Page
    {
        //var
        private PQM00500ViewModel _viewModelUserMenu = new();
        private R_Grid<MenuDTO> _gridMenu;
        private R_Grid<MenuUserDTO> _gridUserMenu;
        private R_ConductorGrid _conUserMenu;
        private R_ConductorGrid _conMenu;
        [Inject] private IClientHelper _clientHelper { get; set; }
        [Inject] private R_ILocalizer<PQM00500FrontResources.Resources_Dummy_Class> _localizer { get; set; }
        private bool _companyComboboxEnabled = true; //to disable combobox while crudmode
        private bool _enabledGridMenu = true; //check after gridlookup on valuechange
        private int _pageSizeGridMenu = PQM00500ContextConstant.GRID_PAGESIZE_MENU;
        private int _pageSizeGridMenuUser = PQM00500ContextConstant.GRID_PAGESIZE_USERMENU;

        //methods -override
        protected override async Task R_Init_From_Master(object poParameter)
        {
            R_Exception loEx = new();
            try
            {
                await _viewModelUserMenu.GetList_CompanyAsync();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }
        protected override async Task<bool> R_LockUnlock(R_LockUnlockEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            var llRtn = false;
            R_LockingFrontResult loLockResult = null;

            try
            {

                var loData = R_FrontUtility.ConvertObjectToObject<MenuDTO>(eventArgs.Data);

                var loCls = new R_LockingServiceClient(pcModuleName: PQM00500ContextConstant.DEFAULT_MODULE,
                    plSendWithContext: true,
                    plSendWithToken: true,
                    pcHttpClientName: PQM00500ContextConstant.DEFAULT_HTTP_NAME);

                if (eventArgs.Mode == R_eLockUnlock.Lock)
                {
                    var loLockPar = new R_ServiceLockingLockParameterDTO
                    {
                        Company_Id = _clientHelper.CompanyId,
                        User_Id = _clientHelper.UserId,
                        Program_Id = PQM00500ContextConstant.CPROGRAM_ID,
                        Table_Name = PQM00500ContextConstant.TABLE_NAME,
                        Key_Value = string.Join("|", loData.CCOMPANY_ID, loData.CMENU_ID)
                    };

                    loLockResult = await loCls.R_Lock(loLockPar);
                }
                else
                {
                    var loUnlockPar = new R_ServiceLockingUnLockParameterDTO
                    {
                        Company_Id = _clientHelper.CompanyId,
                        User_Id = _clientHelper.UserId,
                        Program_Id = PQM00500ContextConstant.CPROGRAM_ID,
                        Table_Name = PQM00500ContextConstant.TABLE_NAME,
                        Key_Value = string.Join("|", loData.CCOMPANY_ID, loData.CMENU_ID)
                    };

                    loLockResult = await loCls.R_UnLock(loUnlockPar);
                }

                llRtn = loLockResult.IsSuccess;
                if (!loLockResult.IsSuccess && loLockResult.Exception != null)
                    throw loLockResult.Exception;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return llRtn;
        }

        //methods -form
        public async Task ComboboxCompanyValueChanged(string poParam)
        {
            R_Exception loEx = new();
            try
            {
                _viewModelUserMenu.SelectedCompanyId = poParam ?? "";
                await _gridMenu.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);


        }

        //methods -gridevents -menu
        public async Task GridMenu_GetListAsync(R_ServiceGetListRecordEventArgs eventArgs)
        {
            R_Exception loEx = new();
            try
            {
                await _viewModelUserMenu.GetList_MenuAsync();
                eventArgs.ListEntityResult = _viewModelUserMenu.MenuList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        public async Task GridMenu_DisplayAsync(R_DisplayEventArgs eventArgs)
        {
            R_Exception loEx = new();
            try
            {
                if (eventArgs.Data != null)
                {
                    _viewModelUserMenu.SelectedMenuId = R_FrontUtility.ConvertObjectToObject<MenuDTO>(eventArgs.Data).CMENU_ID;
                    await _gridUserMenu.R_RefreshGrid(null);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }


        //methods -gridevents -usermenu
        public async Task GridMenuUser_GetListAsync(R_ServiceGetListRecordEventArgs eventArgs)
        {
            R_Exception loEx = new();
            try
            {
                await _viewModelUserMenu.GetList_MenuUserAsync();
                eventArgs.ListEntityResult = _viewModelUserMenu.UserMenuList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        public async Task GridMenuUser_GetRecordAsync(R_ServiceGetRecordEventArgs eventArgs)
        {
            R_Exception loEx = new();
            try
            {
                await _viewModelUserMenu.GetRecord_UserMenuAsync(R_FrontUtility.ConvertObjectToObject<MenuUserDTO>(eventArgs.Data));
                eventArgs.Result = _viewModelUserMenu.UserMenuRecord;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        public void GridMenuUser_AfterAdd(R_AfterAddEventArgs eventArgs)
        {
            R_Exception loEx = new();
            try
            {
                if (eventArgs.Data is MenuUserDTO loData)
                {
                    loData.CUPDATE_BY = _clientHelper.UserId;
                    loData.CCREATE_BY = _clientHelper.UserId;
                    loData.DCREATE_DATE = DateTime.Now;
                    loData.DUPDATE_DATE = DateTime.Now;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        public void GridMenuUser_SetOther(R_SetEventArgs eventArgs)
        {
            R_Exception loEx = new();
            try
            {
                _companyComboboxEnabled = eventArgs.Enable;
                _enabledGridMenu = eventArgs.Enable;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private void GridMenuUser_BeforeOpenGridLookupColumn(R_BeforeOpenGridLookupColumnEventArgs eventArgs)
        {
            switch (eventArgs.ColumnName)
            {
                case nameof(MenuUserDTO.CUSER_ID):
                    eventArgs.Parameter = new UserDTO()
                    {
                        CUSER_ID = _viewModelUserMenu.SelectedCompanyId ?? "",
                    };
                    eventArgs.TargetPageType = typeof(PQM00501);
                    break;
                default:
                    break;
            }
        }
        private void GridMenuUser_AfterOpenGridLookupColumn(R_AfterOpenGridLookupColumnEventArgs eventArgs)
        {
            R_Exception loEx = new();
            try
            {
                if (eventArgs.ColumnData is MenuUserDTO loGetData)
                {
                    switch (eventArgs.ColumnName)
                    {
                        case nameof(MenuUserDTO.CUSER_ID):
                            {
                                var loTempResult = (UserDTO)eventArgs.Result;
                                loGetData.CUSER_ID = loTempResult.CUSER_ID ?? "";
                                loGetData.CUSER_NAME = loTempResult.CUSER_NAME ?? "";
                                break;
                            }
                    }
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private async Task GridMenuUser_CellLostFocusedAsync(R_CellLostFocusedEventArgs eventArgs)
        {
            R_Exception loEx = new();
            try
            {
                var loColumn = eventArgs.ColumnName;
                var loData = (MenuUserDTO)eventArgs.CurrentRow;
                switch (loColumn)
                {
                    case nameof(MenuUserDTO.CUSER_ID):
                        string lcSearchText = (eventArgs.Value as string)?.ToUpper() ?? "";
                        if (!string.IsNullOrWhiteSpace(lcSearchText))
                        {
                            var loResult = await _viewModelUserMenu.GetRecord_UserAsync(lcSearchText);
                            loData.CUSER_ID = loResult?.CUSER_ID ?? "";
                            loData.CUSER_NAME = loResult?.CUSER_NAME ?? "";

                            if (loResult == null)
                                loEx.Add(R_FrontUtility.R_GetError(typeof(PQM00500FrontResources.Resources_Dummy_Class), "_msg_errlookup1"));
                        }
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private async Task GridMenuUser_ValidationAsync(R_ValidationEventArgs eventArgs)
        {
            R_Exception loEx = new();
            try
            {
                var loData = R_FrontUtility.ConvertObjectToObject<MenuUserDTO>(eventArgs.Data);
                if (string.IsNullOrWhiteSpace(loData.CUSER_ID))
                {
                    loEx.Add("", _localizer["_val1"]);
                }
                if (string.IsNullOrWhiteSpace(loData.CUSER_NAME))
                {
                    var loResult = await _viewModelUserMenu.GetRecord_UserAsync(loData.CUSER_ID.ToUpper());
                    if (loResult == null)
                    {
                        loEx.Add("", _localizer["_msg_errlookup1"]);
                    }
                }
                eventArgs.Cancel = loEx.HasError;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private async Task GridMenuUser_SavingAsync(R_SavingEventArgs eventArgs)
        {
            R_Exception loEx = new();
            try
            {
                if (eventArgs.Data is MenuUserDTO loData)
                {
                    loData.CCOMPANY_ID = _viewModelUserMenu.SelectedCompanyId;
                    loData.CMENU_ID = _viewModelUserMenu.SelectedMenuId;
                    if (string.IsNullOrWhiteSpace(loData.CUSER_NAME))
                    {
                        var loResult = await _viewModelUserMenu.GetRecord_UserAsync(loData.CUSER_ID.ToUpper());
                        loData.CUSER_ID = loResult.CUSER_ID ?? "";
                        loData.CUSER_NAME = loResult.CUSER_NAME ?? "";
                    }
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private async Task GridMenuUser_SaveAsync(R_ServiceSaveEventArgs eventArgs)
        {
            R_Exception loEx = new();
            try
            {
                var lodata = eventArgs.Data as MenuUserDTO;
                await _viewModelUserMenu.SaveRecord_UserMenuAsync(lodata, (eCRUDMode)_conUserMenu.R_ConductorMode);
                eventArgs.Result = _viewModelUserMenu.UserMenuRecord;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private async Task GridMenuUser_BeforeDeleteAsync(R_BeforeDeleteEventArgs eventArgs)
        {
            R_Exception loEx = new();
            try
            {
                eventArgs.Cancel = await R_MessageBox.Show(_localizer["_title_popupconfirmation"], _localizer["_msg_deletemenuuser"], R_BlazorFrontEnd.Controls.MessageBox.R_eMessageBoxButtonType.OKCancel) == R_BlazorFrontEnd.Controls.MessageBox.R_eMessageBoxResult.Cancel;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private async Task GridMenuUser_DeleteAsync(R_ServiceDeleteEventArgs eventArgs)
        {
            R_Exception loEx = new();
            try
            {
                await _viewModelUserMenu.DeleteRecord_UserMenuAsync(R_FrontUtility.ConvertObjectToObject<MenuUserDTO>(eventArgs.Data));
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

    }
}
