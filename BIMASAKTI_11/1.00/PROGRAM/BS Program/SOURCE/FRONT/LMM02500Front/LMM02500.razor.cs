using LMM02500Common.DTOs;
using LMM02500Model.ViewModel;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Exceptions;

namespace LMM02500Front;

public partial class LMM02500 : R_Page
{
    private LMM02500ListViewModel _viewModel = new();
    private R_Conductor _conductorRef;
    private R_Grid<LMM02500TenantGroupDTO> _gridRef = new();
    private R_TabStrip _tab;
    private bool _flagCombo = true;

    protected override async Task R_Init_From_Master(object eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            await _viewModel.Init();
            await _gridRef.R_RefreshGrid(null);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }
    
    private async Task GetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            await _viewModel.GetList();
            eventArgs.ListEntityResult = _viewModel.GridList;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task OnChangeParam(object value)
    {
        var loEx = new R_Exception();
        try
        {
            _viewModel.PropertyId = (string)value;
            await _gridRef.R_RefreshGrid(null);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }
        
        R_DisplayException(loEx);
    }
    
    private void GetRecord(R_ServiceGetRecordEventArgs eventArgs)
    {
        _viewModel.Entity = (LMM02500TenantGroupDTO)eventArgs.Data;
        eventArgs.Result = _viewModel.Entity;
    }
    
    private void OnChangeTab(R_TabStripActiveTabIndexChangingEventArgs eventArgs)
    {
        _flagCombo = eventArgs.TabStripTab.Title == "List";
    }
}