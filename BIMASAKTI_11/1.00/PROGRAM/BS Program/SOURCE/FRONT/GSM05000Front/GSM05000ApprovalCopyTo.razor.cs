using GSM05000Common.DTOs;
using GSM05000Model.ViewModel;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;

namespace GSM05000Front;

public partial class GSM05000ApprovalCopyTo : R_Page
{
    private GSM05000ApprovalUserViewModel _viewModel = new();
    // private GSM05000ApprovalCopyDTO _copyTo = new();

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
    
    private async Task OnLostFocus()
    {
        var loEx = new R_Exception();

        try
        {
            var param = new GSM05000SearchTextDTO()
            {
                CSEARCH_TEXT = _viewModel.TempEntityForCopy.CDEPT_CODE_TO
            };

            var loResult = await _viewModel.LookupDepartmentRecord(param);

            if (loResult == null)
            {
                loEx.Add(R_FrontUtility.R_GetError(
                    typeof(GSM05000FrontResources.Resources_Dummy_Class),
                    "_ErrLookupDept"));
                _viewModel.TempEntityForCopy.CDEPT_NAME_TO= "";
                goto EndBlock;
            }

            _viewModel.TempEntityForCopy.CDEPT_NAME_TO = loResult.CDEPT_NAME;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        EndBlock:
        await R_DisplayExceptionAsync(loEx);
    }

    private Task BeforeOpenLookup(R_BeforeOpenLookupEventArgs eventArgs)
    {
        eventArgs.Parameter = _viewModel.TempEntityForCopy;
        eventArgs.TargetPageType = typeof(GSM05000ApprovalDeptLookup);
        return Task.CompletedTask;
    }

    private void AfterOpenLookup(R_AfterOpenLookupEventArgs eventArgs)
    {
        var loData = (GSM05000ApprovalDepartmentDTO)eventArgs.Result;
        if (loData == null)
            return;

        _viewModel.TempEntityForCopy.CDEPT_CODE_TO = loData.CDEPT_CODE;
        _viewModel.TempEntityForCopy.CDEPT_NAME_TO = loData.CDEPT_NAME;
    }

    public async Task Button_OnClickProcessAsync()
    {
        var loEx = new R_Exception();
        
        try
        {
            var loData = _viewModel.TempEntityForCopy;
            await _viewModel.CopyTo(loData);
            await this.Close(true, true);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }
        
        loEx.ThrowExceptionIfErrors();
    }

    public async Task Button_OnClickCloseAsync()
    {
        await this.Close(true, false);
    }
}