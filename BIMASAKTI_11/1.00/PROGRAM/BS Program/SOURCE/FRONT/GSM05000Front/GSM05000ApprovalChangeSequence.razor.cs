using GSM05000Common.DTOs;
using GSM05000Model.ViewModel;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Exceptions;

namespace GSM05000Front;

public partial class GSM05000ApprovalChangeSequence : R_Page
{
    
    private GSM05000ApprovalUserViewModel _viewModel = new();
    private R_ConductorGrid _conductor;
    private R_Grid<GSM05000ApprovalUserDTO> _grid;

    protected override async Task R_Init_From_Master(object poParameter)
    {
        var loEx = new R_Exception();
        
        try
        {
            var loArgs = (string)poParameter;
            _viewModel.ApproverEntity.CTRANS_CODE = loArgs;
            await _viewModel.GetDeptSeqList(loArgs);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }
        
        loEx.ThrowExceptionIfErrors();
    }

    private void Display(R_DisplayEventArgs eventArgs)
    {
        var loData = (GSM05000ApprovalUserDTO)eventArgs.Data;
        _viewModel.GetEnableMethod(loData);
    }

    private async Task GetList(R_ServiceGetListRecordEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            _viewModel.ApproverEntity.CDEPT_CODE = (string)eventArgs.Parameter;
            await _viewModel.GetUserSeqList(_viewModel.ApproverEntity);

            eventArgs.ListEntityResult = _viewModel.ApproverList;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    // private Task BeforeDrop(R_GridRowAfterDropEventArgs eventArgs)
    // {
    //     return Task.CompletedTask;
    // }
    //
    // private Task AfterDrop(R_GridRowBeforeDropEventArgs eventArgs)
    // {
    //     return Task.CompletedTask;
    // }

    private async Task ChangedDept(object value)
    {
        var lcValue = (string)value;
        _viewModel.DepartmentEntity.CDEPT_CODE = lcValue;
        await _grid.R_RefreshGrid(lcValue);
    }

    private async Task OnClickNext()
    {
        var loData = _grid.CurrentSelectedData;
        await _viewModel.SwapUpSeqMethod(poBtnClick: GetBtnClickUpOrDown.Up, loData);
        await _grid.R_MoveToNextRow();
    }

    private async Task OnClickPrevious()
    {
        var loData = _grid.CurrentSelectedData;
        await _viewModel.SwapUpSeqMethod(poBtnClick: GetBtnClickUpOrDown.Down, loData);
        await _grid.R_MoveToPreviousRow();
    }
    
    private async Task OnClickSave()
    {
        await _grid.R_SaveBatch();
        await this.Close(true, true);
    }
    
    public async Task OnClickCancel()
    {
        await this.Close(true, false);
    }

    private void BeforeSaveBatch(R_BeforeSaveBatchEventArgs eventArgs)
    {
        var loEx = new R_Exception();
        
        try
        {
            var loData = (List<GSM05000ApprovalUserDTO>)eventArgs.Data;
            loData.Select(x =>  x.ISEQUENCE = (loData.IndexOf(x) + 1)).ToList();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }
        
        loEx.ThrowExceptionIfErrors();
    }

    private async Task SaveBatch(R_ServiceSaveBatchEventArgs eventArgs)
    {
        var loEx = new R_Exception();
        
        try
        {
            var loData = (List<GSM05000ApprovalUserDTO>)eventArgs.Data;
            await _viewModel.UpdateSequence(loData);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }
    }
}