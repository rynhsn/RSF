using GSM05000Common.DTOs;
using GSM05000Model.ViewModel;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Exceptions;

namespace GSM05000Front;

public partial class GSM05000ApprovalDeptLookup
{
    private GSM05000ApprovalUserViewModel _viewModel = new();
    private R_Grid<GSM05000ApprovalDepartmentDTO> _grid;

    protected override async Task R_Init_From_Master(object poParameter)
    {
        var loEx = new R_Exception();

        try
        {
            // _viewModel.TempEntityForCopy = (GSM05000ApprovalCopyDTO)poParameter;
            _grid.R_RefreshGrid(poParameter);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();

    }

    private async Task GetList(R_ServiceGetListRecordEventArgs eventArgs)
    {
        var loEx = new R_Exception();
        
        try
        {
            var loParameter = (GSM05000ApprovalCopyDTO)eventArgs.Parameter;
            await _viewModel.LookupDepartment(loParameter);
            eventArgs.ListEntityResult = _viewModel.DepartmentLookup;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }
        
        loEx.ThrowExceptionIfErrors();
    }
    
    public async Task Button_OnClickOkAsync()
    {
        var loData = _grid.GetCurrentData();
        await this.Close(true, loData);
    }
    public async Task Button_OnClickCloseAsync()
    {
        await this.Close(true, null);
    }
}