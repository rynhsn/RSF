using ICB00100Common.DTOs;
using ICB00100Front.DTOs;
using ICB00100Model.ViewModel;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;

namespace ICB00100Front;

public partial class ICB00100PopupToDoList : R_Page
{
    private ICB00100ViewModel _viewModel = new();
    private R_ConductorGrid _conductorRef;
    private R_Grid<ICB00100ValidateSoftCloseDTO> _gridRef;
    
    [Inject] private IJSRuntime JS { get; set; }
    [Inject] private R_IExcel ExcelInject { get; set; }

    protected override async Task R_Init_From_Master(object eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            //refresh form
            _viewModel.SystemParam = (ICB00100SystemParamDTO)eventArgs;
            var leMsg = await R_MessageBox.Show("", _localizer["MSG_TODO_LIST_TO_BE_RESOLVED_BEFORE_SOFT_CLOSING"]);
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
            await _viewModel.ValidateSoftPeriod();
            eventArgs.ListEntityResult = _viewModel.ValidateSoftCloseList;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task OnClickSaveToExcel(MouseEventArgs eventArgs)
    {
        var loEx = new R_Exception();
    
        try
        {
            var lcDate = DateTime.Now.ToString("yyyyMMdd_HHmm");
            var loByte = ExcelInject.R_WriteToExcel(_viewModel.ExcelDataSetToDoList);
            var saveFileName = $"IC_SOFT_CLOSE_TODO_LIST_{lcDate}.xlsx";
            await JS.downloadFileFromStreamHandler(saveFileName, loByte);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }
    
        loEx.ThrowExceptionIfErrors();
    }
}