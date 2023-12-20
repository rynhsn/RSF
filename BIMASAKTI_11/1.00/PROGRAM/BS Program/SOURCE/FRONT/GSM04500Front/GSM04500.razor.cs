using GSM04500Common.DTOs;
using GSM04500Model.ViewModel;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;

namespace GSM04500Front;

public partial class GSM04500 : R_Page
{
    
    private GSM04500JournalGroupViewModel _viewModel = new();
    private R_ConductorGrid _conductorRef;
    private R_Grid<GSM04500JournalGroupDTO> _gridRef;
    
    protected override async Task R_Init_From_Master(object poParam)
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
    
    private async Task GetGridList(R_ServiceGetListRecordEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            await _viewModel.GetGridList();
            eventArgs.ListEntityResult = _viewModel.GetGridList();
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
            var loParam = R_FrontUtility.ConvertObjectToObject<GSM04500JournalGroupDTO>(eventArgs.Data);

            await _viewModel.GetEntity(loParam);
            eventArgs.Result = _viewModel.Entity;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }
    
    
    private async Task Save(R_ServiceSaveEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            var loParam = R_FrontUtility.ConvertObjectToObject<GSM04500JournalGroupDTO>(eventArgs.Data);
            await _viewModel.SaveEntity(loParam, (eCRUDMode)eventArgs.ConductorMode);
            eventArgs.Result = _viewModel.Entity;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task Delete(R_ServiceDeleteEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            var loParam = R_FrontUtility.ConvertObjectToObject<GSM04500JournalGroupDTO>(eventArgs.Data);
            await _viewModel.DeleteEntity(loParam);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }
        
        loEx.ThrowExceptionIfErrors();
    }

}