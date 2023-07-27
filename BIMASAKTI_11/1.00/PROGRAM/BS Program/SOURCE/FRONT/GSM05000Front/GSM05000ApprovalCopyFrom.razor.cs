using GSM05000Common.DTOs;
using GSM05000Model.ViewModel;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Exceptions;

namespace GSM05000Front;

public partial class GSM05000ApprovalCopyFrom : R_Page
{
    private GSM05000ApprovalUserViewModel _viewModel = new();
    // private GSM05000ApprovalCopyDTO _copyFrom = new();
    
    protected override async Task R_Init_From_Master(object poParameter)
    {
        var loEx = new R_Exception();
        
        try
        {
            _viewModel.TempEntityForCopy = (GSM05000ApprovalCopyDTO)poParameter;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }
        
        loEx.ThrowExceptionIfErrors();
    }

    private Task BeforeOpenLookup(R_BeforeOpenLookupEventArgs eventArgs)
    {
        eventArgs.Parameter = _viewModel.TempEntityForCopy;
        eventArgs.TargetPageType = typeof(GSM05000ApprovalDeptLookup);
        return Task.CompletedTask;
    }

    private Task AfterOpenLookup(R_AfterOpenLookupEventArgs eventArgs)
    {
        var loData = (GSM05000ApprovalDepartmentDTO)eventArgs.Result;
        _viewModel.TempEntityForCopy.CDEPT_CODE_FROM = loData.CDEPT_CODE;
        _viewModel.TempEntityForCopy.CDEPT_NAME_FROM = loData.CDEPT_NAME;
        return Task.CompletedTask;
    }
    
    public async Task Button_OnClickProcessAsync()
    {
        var loData = _viewModel.TempEntityForCopy;
        await _viewModel.CopyFrom(loData);
        await this.Close(true, loData);
    }
    public async Task Button_OnClickCloseAsync()
    {
        await this.Close(true, null);
    }
}