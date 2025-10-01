using APB00200MODEL.View_Model;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Interfaces;
using APB00200FrontResources;
using R_BlazorFrontEnd.Controls.Popup;
using R_BlazorFrontEnd.Controls.MessageBox;

namespace APB00200FRONT
{
    public partial class APB00200 : R_Page
    {
        //variables
        private APB00200ViewModel _viewModel = new();

        [Inject] private R_ILocalizer<Resources_Dummy_Class> _localizer { get; set; }
        [Inject] private R_PopupService _popupService { get; set; }

        //methods
        protected override async Task R_Init_From_Master(object poParameter)
        {
            R_Exception loEx = new();
            try
            {
                _viewModel.ShowSuccessPopup = async () =>
                {
                    await ShowSuccesPopupAsync();
                };
                _viewModel.ShowErrorPopup = async () =>
                {
                    await ShowErrorPopupAsync();
                };
                await _viewModel.InitialProcessAsync();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }

        private async Task OnClick_BtnProcessAsync()
        {
            R_Exception loEx = new();
            try
            {
                await _viewModel.ProcessCloseAPPeriodAsync();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private async Task ShowErrorPopupAsync()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                await _popupService.Show(typeof(APB00201), _viewModel.ClosePeriod);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            await Task.CompletedTask;
        }

        private async Task ShowSuccesPopupAsync()
        {
            R_Exception loEx = new();
            try
            {
                var loValidate = await R_MessageBox.Show("", _localizer["_msg_success"], R_eMessageBoxButtonType.OK);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            await Task.CompletedTask;
        }
    }
}