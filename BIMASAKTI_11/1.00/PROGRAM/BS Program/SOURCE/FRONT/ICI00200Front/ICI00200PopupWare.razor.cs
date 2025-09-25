using ICI00200Common.DTOs;
using ICI00200Model.DTOs;
using ICI00200Model.ViewModel;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Exceptions;

namespace ICI00200Front;

public partial class ICI00200PopupWare
{
    private ICI00200PopupViewModel _viewModel = new();
    private R_Conductor _conductorRef;
    private R_Grid<ICI00200WarehouseDTO> _gridRef = new();
    
    protected override async Task R_Init_From_Master(object poParam)
    {
        var loEx = new R_Exception();

        try
        {
            _viewModel.Param = (ICI00200PopupDeptWareParamDTO) poParam;
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
            await _viewModel.GetWareList();
            eventArgs.ListEntityResult = _viewModel.WareList;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            var loData = (ICI00200WarehouseDTO)eventArgs.Data;
            await _viewModel.GetEntity(loData.CWAREHOUSE_ID, eICI00200PopupType.WARE);
            eventArgs.Result = _viewModel.DeptWareEntity;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }
}