using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Exceptions;
using PQM00500COMMON.DTO_s;
using PQM00500MODEL.View_Model_s;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Interfaces;
using PQM00500COMMON;
using R_BlazorFrontEnd.Helpers;

namespace PQM00500FRONT
{
    public partial class PQM00501 : R_Page
    {
        //var
        private PQM00500ViewModel _viewModelUserMenu = new();
        [Inject] private R_ILocalizer<PQM00500FrontResources.Resources_Dummy_Class> _localizer { get; set; }
        private R_Grid<UserDTO> _gridUser;
        private int _pageSizeGridUserPopup = PQM00500ContextConstant.GRID_PAGESIZE_POPUPUSER;

        //methods
        protected override async Task R_Init_From_Master(object poParameter)
        {
            R_Exception loEx = new();
            try
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<UserDTO>(poParameter);
                if (loParam != null)
                {
                    _viewModelUserMenu.SelectedCompanyId = loParam.CUSER_ID!;
                    await _gridUser.R_RefreshGrid(null);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        public async Task GridUser_GetListAsync(R_ServiceGetListRecordEventArgs eventArgs)
        {
            R_Exception loEx = new();
            try
            {
                await _viewModelUserMenu.GetList_UserAsync();
                eventArgs.ListEntityResult = _viewModelUserMenu.UserList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        public async Task Button_OnClickOkAsync()
        {
            R_Exception loEx = new();
            try
            {
                var loData = _gridUser.GetCurrentData();
                await Close(true, loData);

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        public async Task Button_OnClickCloseAsync()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                await Close(true, null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
    }
}
