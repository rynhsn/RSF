using CBB00200Common.DTOs;
using CBB00200Front.DTOs;
using CBB00200Model.ViewModel;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Controls.Popup;
using R_BlazorFrontEnd.Exceptions;

namespace CBB00200Front;

public partial class CBB00200 : R_Page
{
    private CBB00200ViewModel _viewModel = new();
    [Inject] private R_PopupService PopupService { get; set; }

    protected override async Task R_Init_From_Master(object eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            //refresh form
            await _viewModel.GetSystemParam();
            if (_viewModel.SystemParam.LPRD_END_FLAG)
            {
                await R_MessageBox.Show("Message", @_localizer["MSG_CONFLICT"]);
                await CloseProgramAsync();
            }
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task OnClickProcess(MouseEventArgs eventArgs)
    {
        var loEx = new R_Exception();
        try
        {
            await _viewModel.GetSystemParam();

            if (_viewModel.SystemParam.LPRD_END_FLAG)
            {
                await R_MessageBox.Show("Message", @_localizer["MSG_CONFLICT"]);
                return;
            }
            
            if (int.Parse(_viewModel.SystemParam.CCURRENT_PERIOD) >= int.Parse(_viewModel.SystemParam.CSOFT_PERIOD))
            {
                await R_MessageBox.Show("Message", @_localizer["MSG_REQUIRE"]);
                return;
            }

            var leAnswer =
                await R_MessageBox.Show("Message", @_localizer["MSG_CONFIRM"], R_eMessageBoxButtonType.YesNo);
            if (leAnswer == R_eMessageBoxResult.No) return;

            // var loResult = await _viewModel.ClosePeriod(_viewModel.SystemParam.CCURRENT_PERIOD);
            await _viewModel.SoftClosePeriod(_viewModel.SystemParam.CCURRENT_PERIOD);

            if (_viewModel.SoftClosePeriodErrorList.Count > 0)
            {
                CBB00200PopupParamDTO loParam = new()
                {
                    SoftClosePeriodErrorList = _viewModel.SoftClosePeriodErrorList
                };
                
                await PopupService.Show(typeof(CBB00200PopupToDoList), loParam);
            }
            else
            {
                await R_MessageBox.Show("Message", @_localizer["MSG_SUCCESS"]);
            }

            await _viewModel.GetSystemParam();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }
}