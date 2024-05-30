using CBB00200Common.DTOs;
using CBB00200Model.ViewModel;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Exceptions;

namespace CBB00200Front;

public partial class CBB00200PopupToDoList : R_Page
{
    private CBB00200ViewModel _viewModel = new();
    private R_Conductor _conductorRef;
    private R_Grid<CBB00200ClosePeriodToDoListDTO> _gridRef;

    [Inject] private IJSRuntime JS { get; set; }
    [Inject] private R_IExcel ExcelInject { get; set; }
    
    protected override async Task R_Init_From_Master(object eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            //refresh form
            await _gridRef.R_RefreshGrid((string)eventArgs);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }
    
    private async Task ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
    {
        
        var loEx = new R_Exception();

        try
        {
            var lcParam = (string)eventArgs.Parameter;
            await _viewModel.GetClosePeriodToDoList(lcParam);
            eventArgs.ListEntityResult = _viewModel.ClosePeriodToDoList;
            
            if (_viewModel.ClosePeriodToDoList.Count > 0)
            {
                await R_MessageBox.Show("Message",@_localizer["MSG_TODO_LIST_FOUND"], R_eMessageBoxButtonType.OK);
            }
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }
    
    private async Task _saveToExcel()
    {
        var loEx = new R_Exception();
    
        try
        {
            var lcDate = DateTime.Now.ToString("yyyyMMdd_HHmm");
            var loByte = ExcelInject.R_WriteToExcel(_viewModel.ExcelDataSetToDoList);
            var saveFileName = $"TODO_CLOSE_PERIOD_{lcDate}.xlsx";
            await JS.downloadFileFromStreamHandler(saveFileName, loByte);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }
    
        loEx.ThrowExceptionIfErrors();
    }
}