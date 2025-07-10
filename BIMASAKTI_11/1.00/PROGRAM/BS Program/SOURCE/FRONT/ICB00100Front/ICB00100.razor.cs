using ICB00100Common.DTOs;
using ICB00100Front.DTOs;
using ICB00100Model.ViewModel;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Controls.Popup;
using R_BlazorFrontEnd.Exceptions;

namespace ICB00100Front;

public partial class ICB00100 : R_Page
{
    private ICB00100ViewModel _viewModel = new();
    private R_Conductor _conductorRef;

    [Inject] private R_PopupService PopupService { get; set; }

    protected override async Task R_Init_From_Master(object poParameter)
    {
        var loEx = new R_Exception();

        try
        {
            await _viewModel.Init();
            await _conductorRef.R_GetEntity(null);
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
            await _conductorRef.R_GetEntity(null);
            if (_viewModel.Data.LSOFT_CLOSING_FLAG)
            {
                await R_MessageBox.Show("", _localizer["MSG_SOFT_CLOSING_PROCESS_IN_PROGRESS"]);
                return;
            }

            var leMsg = await R_MessageBox.Show("", _localizer["CONFIRM_SOFT_CLOSING"], R_eMessageBoxButtonType.YesNo);
            if (leMsg == R_eMessageBoxResult.Yes)
            {
                await _viewModel.ValidateSoftPeriod();
                if (_viewModel.ValidateSoftCloseList.Count > 0)
                {
                    //buka popup
                    var loResult = PopupService.Show(typeof(ICB00100PopupToDoList), _viewModel.SystemParam);
                    return;
                }

                await _viewModel.ProcessSoftPeriod();
                //msg
                var leMsg2 = await R_MessageBox.Show("", _localizer["MSG_SOFT_CLOSING_PROCESSED_SUCCESSFULLY"]);

                await _viewModel.GetSystemParam();
                await _conductorRef.R_GetEntity(null);
            }
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
            await _viewModel.GetSystemParam();
            eventArgs.Result = _viewModel.SystemParam;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }
    
    private void Validation(R_ValidationEventArgs eventArgs)
    {
        var loEx = new R_Exception();
        try
        {
            var loData = (ICB00100SystemParamDTO)eventArgs.Data;
            var currentSoftPeriod = loData.ISOFT_PERIOD_YY + loData.CSOFT_PERIOD_MM;
            if (int.Parse(currentSoftPeriod) > int.Parse(_viewModel.SystemParam.CSOFT_PERIOD))
            {
                loEx.Add("", _localizer["MSG_NEW_SOFT_PERIOD_NOT_LATER_THAN_CURRENT_SOFT_PERIOD"]);
            }

            if (int.Parse(currentSoftPeriod) < int.Parse(_viewModel.SystemParam.CCURRENT_PERIOD))
            {
                loEx.Add("", _localizer["MSG_NEW_SOFT_PERIOD_NOT_EARLIER_THAN_CURRENT_SOFT_PERIOD"]);
            }
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
            eventArgs.Result = _viewModel.SystemParam;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private void Display(R_DisplayEventArgs eventArgs)
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

    private async Task OnChangedProperty(string value)
    {
        var loEx = new R_Exception();

        try
        {
            if (value == _viewModel.SelectedPropertyId) return;
            _viewModel.SelectedPropertyId = value;
            await _conductorRef.R_GetEntity(null);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }
    
    private async Task BeforeCancel(R_BeforeCancelEventArgs eventArgs)
    {
        var loEx = new R_Exception();
        try
        {
            var leMsg = await R_MessageBox.Show("", _localizer["MSG_UNSAVED_CHANGES"], R_eMessageBoxButtonType.YesNo);
            eventArgs.Cancel = leMsg == R_eMessageBoxResult.No;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }
}