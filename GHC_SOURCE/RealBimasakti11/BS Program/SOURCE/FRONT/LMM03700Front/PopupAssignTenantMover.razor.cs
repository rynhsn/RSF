using Microsoft.AspNetCore.Components;
using PMM03700COMMON.DTO_s;
using PMM03700MODEL;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_BlazorFrontEnd.Interfaces;

namespace PMM03700FRONT
{
    public partial class PopupAssignTenantMover : R_Page
    {
        private R_ConductorGrid _conAvailableTenant;
        private R_Grid<TenantDTO> _gridAvailableTenant;

        private R_ConductorGrid _conSelectedTenant;
        private R_Grid<TenantDTO> _gridSelectedTenant;

        private PMM03710ViewModel _viewModelTC = new PMM03710ViewModel();

        private string _moveLeftAll = "<<";
        private string _moveLeft = "<";
        private string _moveRightAll = ">>";
        private string _moveRight = ">";

        [Inject] private R_ILocalizer<PMM03700FRONTResources.Resources_Dummy_Class> _localizer { get; set; }

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();
            try
            {
                _viewModelTC.TenantClass = (TenantClassificationDTO)poParameter;
                _viewModelTC._propertyId = _viewModelTC.TenantClass.CPROPERTY_ID;
                _viewModelTC._tenantClassificationGroupId = _viewModelTC.TenantClass.CTENANT_CLASSIFICATION_GROUP_ID;
                _viewModelTC._tenantClassificationId = _viewModelTC.TenantClass.CTENANT_CLASSIFICATION_ID;
                await _gridAvailableTenant.R_RefreshGrid(poParameter);
                await _gridSelectedTenant.R_RefreshGrid(poParameter);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }

        private async Task AvailableTenant_GetList(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<TenantGridDTO>(eventArgs.Parameter);
                await _viewModelTC.GetTenantToAssignList(loParam);
                eventArgs.ListEntityResult = _viewModelTC.AvailableTenantList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private async Task SelectedTenant_GetList(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<TenantGridDTO>(eventArgs.Parameter);
                await _viewModelTC.GetSelectedTenantList(loParam);
                eventArgs.ListEntityResult = _viewModelTC.SelectedTenantList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private void R_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            eventArgs.Result = eventArgs.Data;
        }

        #region Save Batch

        private async Task SelectedTenant_BeforeSaveBatchAsync(R_BeforeSaveBatchEventArgs eventArgs)
        {
            R_Exception loEx = new();
            try
            {
                if (await R_MessageBox.Show("", _localizer["_msg_confirmAssign"], R_eMessageBoxButtonType.YesNo) == R_eMessageBoxResult.No)
                {
                    eventArgs.Cancel = true;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private async Task SelectedTenant_ServiceSaveBatchAsync(R_ServiceSaveBatchEventArgs eventArgs)
        {
            R_Exception loEx = new();
            try
            {
                await _viewModelTC.AssignTenantProcess();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private async Task SelectedTenant_AfterSaveBatchAsync(R_AfterSaveBatchEventArgs eventArgs)
        {
            R_Exception loEx = new();
            try
            {
                var loMsg = await R_MessageBox.Show("", _localizer["_msg_assignSuccess"], R_eMessageBoxButtonType.OK);
                await Close(true, _viewModelTC.TenantClass);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        #endregion Save Batch

        #region drop

        private bool _isMove = false;

        private void R_GridRowBeforeDrop(R_GridDragDropBeforeDropEventArgs<TenantDTO> eventArgs)
        {
            _isMove = eventArgs.Items.Count > 0;
        }

        private void R_GridRowAfterDrop(R_GridDragDropAfterDropEventArgs<TenantDTO> eventArgs)
        {
            _isMove = eventArgs.Items.Count > 0;
        }

        #endregion drop

        #region mover

        private void MoveTo_SelectedTenantList()
        {
            _isMove = true;
            _gridAvailableTenant.R_MoveToTargetGrid();
        }

        private void MoveAllTo_SelectedTenantList()
        {
            _isMove = true;
            _gridAvailableTenant.R_MoveAllToTargetGrid();
        }

        private void MoveTo_AvailableTenantList()
        {
            _isMove = true;
            _gridSelectedTenant.R_MoveToTargetGrid();
        }

        private void MoveAllTo_AvailableTenantList()
        {
            _isMove = true;
            _gridSelectedTenant.R_MoveAllToTargetGrid();
        }

        #endregion mover

        #region process ~ close

        private async Task BtnProcess()
        {
            var loEx = new R_Exception();

            try
            {
                await _gridSelectedTenant.R_SaveBatch();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private async Task BtnClose()
        {
            R_Exception loEx = new();

            try
            {
                if (_isMove)
                {
                    var Discard = await R_MessageBox.Show("", _localizer["_msg_discard"], R_eMessageBoxButtonType.YesNo);
                    switch (Discard)
                    {
                        case R_eMessageBoxResult.Yes:
                            await Close(true, null);
                            break;
                    }
                }
                else
                {
                    await Close(true, null);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        #endregion process ~ close
    }
}