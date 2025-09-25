using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using PMB05000Common.DTOs;
using PMB05000Model.ViewModel;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Exceptions;
using System.Collections.ObjectModel;

namespace PMB05000Front;

public partial class PMB05000PopupToDoList
{
    
    private PMB05000ViewModel _viewModel = new();
    private R_ConductorGrid _conductorRef;
    private R_Grid<PMB05000ValidateSoftCloseDTO> _gridRef;
    
    [Inject] private IJSRuntime JS { get; set; }
    [Inject] private R_IExcel ExcelInject { get; set; }

    protected override async Task R_Init_From_Master(object eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            //refresh form
            _viewModel.ValidateSoftCloseList = (ObservableCollection<PMB05000ValidateSoftCloseDTO>)eventArgs;
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
            //await _viewModel.ValidateSoftPeriod();
            _viewModel.SetExcelDataSetToDoList();
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
            var saveFileName = $"PM_SOFT_CLOSE_TODO_LIST_{lcDate}.xlsx";
            await JS.downloadFileFromStreamHandler(saveFileName, loByte);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }
    
        loEx.ThrowExceptionIfErrors();
    }
}