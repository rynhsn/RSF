using Lookup_HDCOMMON.DTOs.HDL00100;
using Lookup_HDCOMMON.DTOs.HDL00200;
using Lookup_HDModel.ViewModel.HDL00100;
using Lookup_HDModel.ViewModel.HDL00200;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;

namespace Lookup_HDFRONT;

public partial class HDL00200 : R_Page
{
    private LookupHDL00200ViewModel _viewModel = new LookupHDL00200ViewModel();
    private R_Grid<HDL00200DTO> GridRef;
    private R_Conductor _conductorRef;

    protected override async Task R_Init_From_Master(object poParameter)
    {
        var loEx = new R_Exception();

        try
        {
            _viewModel.ParameterLookup = (HDL00200ParameterDTO)poParameter;
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

            await _viewModel.GetPriceListHeader();
            eventArgs.ListEntityResult = _viewModel.PriceListLookup;


        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        R_DisplayException(loEx);
    }
    
    
    public async Task Button_OnClickOkAsync()
    {
        if (_viewModel.PriceListLookup.Count == 0)
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