using LMM03700Common.DTO_s;
using LMM03700Model;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using System.Xml.Linq;

namespace LMM03700Front
{
    public partial class PopupAssignTenantMover : R_Page
    {
        private R_ConductorGrid _conAvailableTenant;
        private R_Grid<TenantDTO> _gridAvailableTenant;

        private R_ConductorGrid _conSelectedTenant;
        private R_Grid<TenantDTO> _gridSelectedTenant;

        private LMM03710ViewModel _viewModelTC = new LMM03710ViewModel();

        private string _moveLeftAll = "<<";
        private string _moveLeft = "<";
        private string _moveRightAll = ">>";
        private string _moveRight = ">";

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();
            try
            {
                _viewModelTC.TenantClass = (TenantClassificationDTO)poParameter;
                _viewModelTC._propertyId = _viewModelTC.TenantClass.CPROPERTY_ID;
                _viewModelTC._tenantClassificationGroupId= _viewModelTC.TenantClass.CTENANT_CLASSIFICATION_GROUP_ID;
                _viewModelTC._tenantClassificationId= _viewModelTC.TenantClass.CTENANT_CLASSIFICATION_ID;
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

        private void SelectedTenant_BeforeSaveBatch(R_BeforeSaveBatchEventArgs events)
        {

        }

        private async Task SelectedTenant_ServiceSaveBatchAsync(R_ServiceSaveBatchEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _viewModelTC.AssignTenantProcess();
                await this.Close(true, _viewModelTC.TenantClass);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task SelectedTenant_AfterSaveBatchAsync(R_AfterSaveBatchEventArgs eventArgs)
        {
            await this.Close(true, _viewModelTC.TenantClass);
        }

        #endregion

        #region drop

        private bool _isMove = false;

        private void R_GridRowBeforeDrop(R_GridRowBeforeDropEventArgs eventArgs)
        {
            _isMove = true;
        }

        private void R_GridRowAfterDrop(R_GridRowAfterDropEventArgs eventArgs)
        {
            _isMove = true;
        }

        #endregion

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
        #endregion

        #region process ~ close
        
        private bool _isProcessMove = false;

        private async Task BtnProcess()
        {
            var loEx = new R_Exception();

            try
            {
                _isProcessMove = true;
                await _conSelectedTenant.R_SaveBatch();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private async Task BtnClose()
        {
            var loEx = new R_Exception();

            try
            {
                if (_isMove && !_isProcessMove)
                {
                    var Discard = await R_MessageBox.Show("", "Discard changes? ", R_eMessageBoxButtonType.YesNo);
                    if (Discard == R_eMessageBoxResult.Yes)
                    {
                        await this.Close(true, null);
                    }
                }
                else
                {
                    await this.Close(true, null);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

        }

        #endregion
    }
}
