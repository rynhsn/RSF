using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
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
    public partial class PopupMoveTenant : R_Page
    {
        private R_ConductorGrid _conTenantToMoveRef;
        private R_Grid<TenantGridDTO> _gridTenantToMove;
        private PMM03710ViewModel _viewModelTC = new();
        private int _pageSizeTenantToMove = 10;
        [Inject] private R_ILocalizer<PMM03700FRONTResources.Resources_Dummy_Class> _localizer { get; set; }

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                _viewModelTC.TenantClass = R_FrontUtility.ConvertObjectToObject<TenantClassificationDTO>(poParameter);
                _viewModelTC._propertyId = _viewModelTC.TenantClass.CPROPERTY_ID;
                _viewModelTC._tenantClassificationGroupId = _viewModelTC.TenantClass.CTENANT_CLASSIFICATION_GROUP_ID;
                _viewModelTC._tenantClassificationId = _viewModelTC.TenantClass.CTENANT_CLASSIFICATION_ID;
                await _viewModelTC.GetTenantClassListForMove();
                await _viewModelTC.GetTenantClassRecordForMove(_viewModelTC.TenantClass);
                await _gridTenantToMove.R_RefreshGrid(poParameter);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private async Task TenantToMove_ServiceGetListRecordAsync(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<TenantClassificationDTO>(eventArgs.Parameter);
                await _viewModelTC.GetTenantListToMove(loParam);
                eventArgs.ListEntityResult = _viewModelTC.TenantToMoveList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        #region batch

        private async Task MoveTenant_BeforeSaveBatchAsync(R_BeforeSaveBatchEventArgs eventArgs)
        {
            R_Exception loEx = new();
            try
            {
                if (string.IsNullOrWhiteSpace(_viewModelTC._toTenantClassificationId))
                {
                    await R_MessageBox.Show("", _localizer["_val_moveTenant1"], R_eMessageBoxButtonType.OK);
                    eventArgs.Cancel = true;
                }
                if (await R_MessageBox.Show("", _localizer["_msg_confirmMoveTenant"], R_eMessageBoxButtonType.YesNo) == R_eMessageBoxResult.No)
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

        private async Task MoveTenant_ServiceSaveBatchAsync(R_ServiceSaveBatchEventArgs eventArgs)
        {
            R_Exception loEx = new();
            try
            {
                _viewModelTC._fromTenantClassificationId = _viewModelTC.TenantClassForMoveTenant.CTENANT_CLASSIFICATION_ID;
                await _viewModelTC.MoveTenant();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private async Task MoveTenant_AfterSaveBatchAsync(R_AfterSaveBatchEventArgs eventArgs)
        {
            R_Exception loEx = new();
            try
            {
                var loMsg = await R_MessageBox.Show("", _localizer["_msg_movetenantSuccess"], R_eMessageBoxButtonType.OK);
                await Close(true, _viewModelTC.TenantClass);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private void CheckBoxSelectValueChanged(R_CheckBoxSelectValueChangedEventArgs eventArgs)
        {
            //implement only
        }

        #endregion batch

        #region ProcesButton

        public async Task Button_OnClickOkAsync()
        {
            var loEx = new R_Exception();
            try
            {
                await _gridTenantToMove.R_SaveBatch();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        public async Task Button_OnClickCloseAsync()
        {
            await Close(true, _viewModelTC.TenantClass);
        }

        #endregion ProcesButton
    }
}