using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using TXB00200Common.DTOs;
using TXB00200Front.DTOs;
using TXB00200Model.ViewModel;

namespace TXB00200Front;

public partial class TXB00200PopupToDoList
{
    private TXB00200ViewModel _viewModel = new();
    private R_Conductor _conductorRef;
    private R_Grid<TXB00200SoftClosePeriodToDoListDTO> _gridRef;

    private string CurrentPeriod;
    [Inject] private IJSRuntime JS { get; set; }
    [Inject] private R_IExcel ExcelInject { get; set; }
    
    
    protected override async Task R_Init_From_Master(object eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            //refresh form
            var loParam = R_FrontUtility.ConvertObjectToObject<TXB00200PopupParamDTO>(eventArgs);
            CurrentPeriod = loParam.CurrentPeriod;
            _viewModel.ValidateHasError(loParam.ErrorList);
            await _gridRef.R_RefreshGrid(null);
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
            // var lcParam = (string)eventArgs.Parameter;
            // await _viewModel.GetClosePeriodToDoList(lcParam);
            
            eventArgs.ListEntityResult = _viewModel.SoftClosePeriodToDoList;
            
            // if (_viewModel.SoftClosePeriodToDoList.Count > 0)
            // {
            //     await R_MessageBox.Show("Message",@_localizer["MSG_TODO_LIST_FOUND"], R_eMessageBoxButtonType.OK);
            // }
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
            var loByte = ExcelInject.R_WriteToExcel(_viewModel.ExcelDataSetToDoList);
            var saveFileName = $"SOFT_CLOSE_TX_{CurrentPeriod}_ERROR_LIST.xlsx";
            await JS.downloadFileFromStreamHandler(saveFileName, loByte);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }
    
        loEx.ThrowExceptionIfErrors();
    }
}