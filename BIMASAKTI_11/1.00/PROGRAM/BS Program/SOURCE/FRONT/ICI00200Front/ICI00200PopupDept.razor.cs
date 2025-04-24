using ICI00200Common.DTOs;
using ICI00200Model.DTOs;
using ICI00200Model.ViewModel;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Exceptions;

namespace ICI00200Front;

public partial class ICI00200PopupDept : R_Page
{
    private ICI00200PopupViewModel _viewModel = new();
    private R_Conductor _conductorRef;
    private R_Grid<ICI00200DeptDTO> _gridRef = new();
    
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
            await _viewModel.GetDeptList();
            eventArgs.ListEntityResult = _viewModel.DeptList;
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
            var loData = (ICI00200DeptDTO)eventArgs.Data;
            await _viewModel.GetEntity(loData.CDEPT_CODE, eICI00200PopupType.DEPT);
            eventArgs.Result = _viewModel.DeptWareEntity;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

}