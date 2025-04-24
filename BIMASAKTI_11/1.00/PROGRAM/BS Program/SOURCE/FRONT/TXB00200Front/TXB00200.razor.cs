using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Controls.Popup;
using R_BlazorFrontEnd.Exceptions;
using TXB00200Front.DTOs;
using TXB00200Model.ViewModel;

namespace TXB00200Front;

public partial class TXB00200 : R_Page
{
    private TXB00200ViewModel _viewModel = new();
    private R_Conductor _conductorRef;

    [Inject] private R_PopupService PopupService { get; set; }

    protected override async Task R_Init_From_Master(object poParameter)
    {
        var loEx = new R_Exception();

        try
        {
            await _conductorRef.R_GetEntity(null);
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
            //     await _viewModel.GetSystemParam();
            //
            //     if (_viewModel.SystemParam.LPRD_END_FLAG)
            //     {
            //         await R_MessageBox.Show("Message", @_localizer["MSG_CONFLICT"]);
            //         return;
            //     }
            //
            //     if (int.Parse(_viewModel.SystemParam.CCURRENT_PERIOD) >= int.Parse(_viewModel.SystemParam.CSOFT_PERIOD))
            //     {
            //         await R_MessageBox.Show("Message", @_localizer["MSG_REQUIRE"]);
            //         return;
            //     }
            //
            // var leAnswer =
            //     await R_MessageBox.Show("Message", @_localizer["MSG_CONFIRM"], R_eMessageBoxButtonType.YesNo);
            // if (leAnswer == R_eMessageBoxResult.No) return;

            await _viewModel.ProcessSoftClosePeriod();

            // await _viewModel.SoftClosePeriod(_viewModel.SystemParam.CCURRENT_PERIOD);

            if (_viewModel.SoftClosePeriodErrorList.Count > 0)
            {
                TXB00200PopupParamDTO loParam = new()
                {
                    CurrentPeriod = _viewModel.CurrentPeriod.CPERIOD_YEAR + _viewModel.CurrentPeriod.CPERIOD_MONTH,
                    ErrorList = _viewModel.SoftClosePeriodErrorList
                };

                await PopupService.Show(typeof(TXB00200PopupToDoList), loParam);
            }
            else
            {
                await R_MessageBox.Show("Message", @_localizer["SUCCESS_SOFT_CLOSE"]);
            }

            await _viewModel.GetCurrentPeriod();
            // await _viewModel.GetNextPeriod();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task GetRecord(R_ServiceGetRecordEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            await _viewModel.GetCurrentPeriod();
            eventArgs.Result = _viewModel.CurrentPeriod;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }


    private async Task Saving(R_ServiceSaveEventArgs eventArgs)
    {
        var loEx = new R_Exception();
        try
        {
            await _viewModel.UpdateSoftPeriod();
            await _viewModel.GetCurrentPeriod();
            // await _viewModel.GetNextPeriod();
            eventArgs.Result = _viewModel.CurrentPeriod;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task Display(R_DisplayEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            // await _viewModel.GetCurrentPeriod();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }
}