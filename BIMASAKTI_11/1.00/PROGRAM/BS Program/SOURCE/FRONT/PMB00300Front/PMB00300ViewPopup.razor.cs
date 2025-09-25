using Microsoft.AspNetCore.Components.Web;
using PMB00300Common.DTOs;
using PMB00300Model.ViewModel;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Exceptions;

namespace PMB00300Front;

public partial class PMB00300ViewPopup : R_Page
{
    private PMB00300ViewModelPopupView _viewModel = new();
    private R_ConductorGrid _conductorRefCharges;
    private R_Grid<PMB00300RecalcChargesDTO> _gridRefCharges = new();

    private R_ConductorGrid _conductorRefRule;
    private R_Grid<PMB00300RecalcRuleDTO> _gridRefRule = new();

    protected override async Task R_Init_From_Master(object eventArgs)
    {
        var loEx = new R_Exception();
        try
        {
            _viewModel.Header = (PMB00300RecalcDTO)eventArgs;
            await _gridRefCharges.R_RefreshGrid(null);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task GetChargesListRecord(R_ServiceGetListRecordEventArgs eventArgs)
    {
        var loEx = new R_Exception();
        try
        {
            await _viewModel.GetRecalcChargesList();
            eventArgs.ListEntityResult = _viewModel.GridChargesList;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }


    private async Task GetRuleListRecord(R_ServiceGetListRecordEventArgs eventArgs)
    {
        var loEx = new R_Exception();
        try
        {
            await _viewModel.GetRecalcRuleList();
            eventArgs.ListEntityResult = _viewModel.GridRuleList;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task OnClickProcess()
    {
        var loEx = new R_Exception();
        try
        {
            var llReturn = await _viewModel.RecalcBillingRuleProcess();
            await _viewModel.RecalcBillingRuleProcess();
            // await this.Close(true, true);
            if (llReturn)
            {
                await this.Close(true, true);
            }
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task DisplayChargesList(R_DisplayEventArgs eventArgs)
    {
        var loEx = new R_Exception();
        try
        {
            _viewModel.ChargesEntity = (PMB00300RecalcChargesDTO)eventArgs.Data;
            await _gridRefRule.R_RefreshGrid(null);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }
}