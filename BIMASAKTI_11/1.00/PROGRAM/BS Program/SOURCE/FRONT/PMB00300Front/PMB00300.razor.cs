using PMB00300Common.DTOs;
using PMB00300Model.ViewModel;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Exceptions;

namespace PMB00300Front;

public partial class PMB00300
{
    private PMB00300ViewModel _viewModel = new();
    private R_Conductor _conductorRef;
    private R_Grid<PMB00300RecalcDTO> _gridRef = new();

    protected override async Task R_Init_From_Master(object eventArgs)
    {
        var loEx = new R_Exception();
        try
        {
            await _viewModel.GetPropertyList();
            await _gridRef.R_RefreshGrid(null);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task OnChangeProperty(string? value)
    {
        var loEx = new R_Exception();
        try
        {
            _viewModel.Property.CPROPERTY_ID = value;
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
            await _viewModel.GetRecalcList();
            eventArgs.ListEntityResult = _viewModel.GridList;
            if (_viewModel.GridList.Count <= 0)
            {
                var leMsg = R_MessageBox.Show(_localizer["MSG"], @_localizer["NO_DATA_FOUND"]);
                return;
            }
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private void GetRecord(R_ServiceGetRecordEventArgs eventArgs)
    {
        var loEntity = (PMB00300RecalcDTO)eventArgs.Data;
        _viewModel.Entity = loEntity;
        eventArgs.Result = _viewModel.Entity;
    }

    private void BeforeOpenPopup(R_BeforeOpenPopupEventArgs eventArgs)
    {
        
        var loEx = new R_Exception();
        try
        {
            eventArgs.Parameter = _viewModel.Entity;
            eventArgs.TargetPageType = typeof(PMB00300ViewPopup);

        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }
    
    private async Task AfterOpenPopup(R_AfterOpenPopupEventArgs eventArgs)
    {
        
        var loEx = new R_Exception();
        try
        {
            if (eventArgs.Success && (bool)eventArgs.Result) await _gridRef.R_RefreshGrid(null); 

        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }
}