using Lookup_GLCOMMON.DTOs.GLL00100;
using Lookup_GLModel.ViewModel.GLL00100;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Exceptions;

namespace Lookup_GLFRONT;

public partial class GLL00100 : R_Page
{
    private LookupGLL00100ViewModel _viewModel = new LookupGLL00100ViewModel();
    private R_Grid<GLL00100DTO>? _gridRef;

    protected override async Task R_Init_From_Master(object poParameter)
    {
        var loEx = new R_Exception();

        try
        {
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
            var loParam = (GLL00100ParameterDTO)eventArgs.Parameter;
            await _viewModel.GLL00100ReferenceNoLookUp(loParam);
            eventArgs.ListEntityResult = _viewModel.loList;
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