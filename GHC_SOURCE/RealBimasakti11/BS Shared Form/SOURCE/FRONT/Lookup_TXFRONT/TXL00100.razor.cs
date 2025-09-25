using Lookup_TXCOMMON.DTOs.TXL00100;
using Lookup_TXModel.ViewModel.TXL00100;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Exceptions;

namespace Lookup_TXFRONT;

public partial class TXL00100 : R_Page
{
    private LookupTXL00100ViewModel _viewModel = new LookupTXL00100ViewModel();
    private R_Grid<TXL00100DTO>? _gridRef;

    protected override async Task R_Init_From_Master(object poParameter)
    {
        var loEx = new R_Exception();

        try
        {
            _viewModel.lHasData = false;
            await _gridRef!.R_RefreshGrid(poParameter);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    public async Task R_ServiceGetListRecordAsync(R_ServiceGetListRecordEventArgs eventArgs)
    {
        var loEx = new R_Exception();
        try
        {
            //var loParam = (TXL00100ParameterDTO)eventArgs.Parameter;
            await _viewModel.TXL00100BranchLookUp();
            eventArgs.ListEntityResult = _viewModel.loList;
            _viewModel.lHasData = _viewModel.loList.Any();

        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }
        loEx.ThrowExceptionIfErrors();
    }

    public async Task Button_OnClickOkAsync()
    {
        var loData = _gridRef!.GetCurrentData();
        await this.Close(true, loData);
    }

    public async Task Button_OnClickCloseAsync()
    {
        await this.Close(true, null);
    }
}
