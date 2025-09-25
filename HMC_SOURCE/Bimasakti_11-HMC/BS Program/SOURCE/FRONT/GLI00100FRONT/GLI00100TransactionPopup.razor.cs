using GLI00100Common.DTOs;
using GLI00100Model.ViewModel;
using Microsoft.AspNetCore.Components.Web;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Exceptions;

namespace GLI00100Front;

public partial class GLI00100TransactionPopup : R_Page
{
    private GLI00100TransactionViewModel _viewModel = new();
    private GLI00100ViewModel _mainViewModel = new();

    private R_ConductorGrid _conductorRef;
    private R_Grid<GLI00100JournalGridDTO> _gridRef = new();
    
    protected override async Task R_Init_From_Master(object poParameter)
    {
        var loEx = new R_Exception();

        try
        {
            var loParam = (GLI00100JournalParamDTO)poParameter;
            _viewModel.PopupParams = loParam;
            await _viewModel.GetGSMCompany();
            await _viewModel.GetHeader();
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
            eventArgs.ListEntityResult = _viewModel.DataList;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task CloseEvent(MouseEventArgs eventArgs)
    {
        await this.Close(true, null);
    }
}