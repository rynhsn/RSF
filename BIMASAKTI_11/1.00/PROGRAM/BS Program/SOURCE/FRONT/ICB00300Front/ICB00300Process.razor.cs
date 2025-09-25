using ICB00300Common.DTOs;
using ICB00300Common.Params;
using ICB00300Model.ViewModel;
using Microsoft.AspNetCore.Components.Web;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Exceptions;

namespace ICB00300Front;

public partial class ICB00300Process : R_Page
{
    private ICB00300ProcessViewModel _viewModel = new();
    private R_ConductorGrid _conductorRef;
    private R_Grid<ICB00300ProductDTO> _gridRef = new();
    private R_Button _processButton;
    
    private void StateChangeInvoke()
    {
        StateHasChanged();
    }

    #region HandleError

    private void DisplayErrorInvoke(R_Exception poException)
    {
        R_DisplayException(poException);
    }

    #endregion
    
    public async Task ShowSuccessInvoke()
    {
        _processButton.Enabled = true;
        await R_MessageBox.Show("", _localizer["RecalculateStockSuccessfully"], R_eMessageBoxButtonType.OK);
    }

    protected override async Task R_Init_From_Master(object poParameter)
    {
        var loEx = new R_Exception();

        try
        {
            _viewModel.Param = (ICB00300ProcessParam)poParameter;
            await _viewModel.GetProductList();
            _processButton.Enabled = _viewModel.TotalSelected > 0;
            
            _viewModel.StateChangeAction = StateChangeInvoke;
            _viewModel.DisplayErrorAction = DisplayErrorInvoke;
            _viewModel.ShowSuccessAction = async () => { await ShowSuccessInvoke(); };
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
            await _viewModel.GetProductList();
            eventArgs.ListEntityResult = _viewModel.ProductList;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }
    
    

    private async Task OnClickCancel()
    {
        await this.Close(false, false);
    }

    private async Task OnClickProcess()
    {
        var loEx = new R_Exception();

        try
        {
            _processButton.Enabled = false;
            await _gridRef.R_SaveBatch();
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
            var loDataList = (List<ICB00300ProductDTO>)eventArgs.Data;
            var loSelectedList = loDataList.Where(x => x.LSELECTED).ToList();
            
            if (_viewModel.TotalSelected == 0)
            {
                loEx.Add("Error", _localizer["PleaseSelectAtLeastOneRecord"]);
                goto EndBlock;
                // return;
            }
            
            await _viewModel.SaveBulk(loSelectedList);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        EndBlock:
        loEx.ThrowExceptionIfErrors();
    }

    private void CheckBoxSelectValueChanged(R_CheckBoxSelectValueChangedEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            _viewModel.TotalSelected = _gridRef.DataSource.Count(x => x.LSELECTED);            
            _processButton.Enabled = _viewModel.TotalSelected > 0;

        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }
}