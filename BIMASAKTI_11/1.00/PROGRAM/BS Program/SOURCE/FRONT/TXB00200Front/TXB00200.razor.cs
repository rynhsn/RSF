using Microsoft.AspNetCore.Components.Web;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Exceptions;
using TXB00200Model.ViewModel;

namespace TXB00200Front;

public partial class TXB00200 : R_Page
{
    private TXB00200ViewModel _viewModel = new();

    protected override async Task R_Init_From_Master(object poParameter)
    {
        var loEx = new R_Exception();

        try
        {
            await _viewModel.GetPropertyList();
            await _viewModel.GetPeriodList();
            await _viewModel.GetNextPeriod();
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
            await _viewModel.ProcessSoftPeriod();
            var loMsg = await R_MessageBox.Show("", _localizer["SUCCESS_SOFT_CLOSE"], R_eMessageBoxButtonType.OK);
            await _viewModel.GetNextPeriod();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }
}