using Lookup_ICCOMMON.DTOs.ICL00100;
using Lookup_ICModel.ViewModel.ICL00100;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Exceptions;

namespace Lookup_ICFRONT;

public partial class ICL00100 : R_Page
{
    private LookupICL00100ViewModel _viewModel = new LookupICL00100ViewModel();
    private R_Grid<ICL00100DTO> GridRef;
    private R_Conductor _conductorRef;

    protected override async Task R_Init_From_Master(object poParameter)
    {
        var loEx = new R_Exception();

        try
        {
            _viewModel.ParameterLookup = (ICL00100ParameterDTO)poParameter;
            GridRef.R_RefreshGrid(null);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
        await Task.CompletedTask;
    }
    public async Task R_ServiceGetListRecordAsync(R_ServiceGetListRecordEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {

            await _viewModel.GetRequestList();
            eventArgs.ListEntityResult = _viewModel.RequestLookup;


        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        R_DisplayException(loEx);
    }
    
    public async Task Button_OnClickOkAsync()
    {
        if (_viewModel.RequestLookup.Count == 0)
        {
            await R_MessageBox.Show("Error", @_localizer["Data Not Found"], R_eMessageBoxButtonType.OK);
            return;
        }
        else
        {
            var loData = GridRef.GetCurrentData();
            await this.Close(true, loData);
        }
    }
    public async Task Button_OnClickCloseAsync()
    {
        await this.Close(true, null);
    }

}
