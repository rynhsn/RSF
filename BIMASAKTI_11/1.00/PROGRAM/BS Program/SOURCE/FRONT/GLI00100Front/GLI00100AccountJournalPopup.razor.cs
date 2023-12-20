using GLI00100Common.DTOs;
using GLI00100Model.ViewModel;
using Microsoft.AspNetCore.Components.Web;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;

namespace GLI00100Front;

public partial class GLI00100AccountJournalPopup : R_Page
{
    private GLI00100AccountJournalViewModel _viewModel = new();

    private R_ConductorGrid _conductorRef;
    private R_Grid<GLI00100TransactionGridDTO> _gridRef = new();

    protected override async Task R_Init_From_Master(object poParameter)
    {
        var loEx = new R_Exception();

        try
        {
            var loParam = (GLI00100PopupParamsDTO)poParameter;
            _viewModel.PopupParams = loParam;
            await _viewModel.GetHeader();
            await _viewModel.GetList();
            // if (_viewModel.DataList.Count > 0)
            // {
                await _gridRef.R_RefreshGrid(null);
            // }
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
            eventArgs.ListEntityResult = _viewModel.DataList;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task RefreshList(MouseEventArgs eventArgs)
    {
        await _gridRef.R_RefreshGrid(null);
    }

    private async Task CloseEvent(MouseEventArgs eventArgs)
    {
        await this.Close(true, null);
    }

    private void BeforeOpenPopupTransaction(R_BeforeOpenPopupEventArgs eventArgs)
    {
        eventArgs.Parameter = _viewModel.PopupParamsJournal;
        eventArgs.TargetPageType = typeof(GLI00100TransactionPopup);
    }

    private void Display(R_DisplayEventArgs eventArgs)
    {
        var loData = (GLI00100TransactionGridDTO)eventArgs.Data;
        _viewModel.PopupParamsJournal.CREC_ID = loData.CREC_ID;
    }
}