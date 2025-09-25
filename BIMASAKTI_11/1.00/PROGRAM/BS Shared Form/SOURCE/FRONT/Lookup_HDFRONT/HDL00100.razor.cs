using Lookup_HDCOMMON.DTOs.HDL00100;
using Lookup_HDModel.ViewModel.HDL00100;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;

namespace Lookup_HDFRONT;

public partial class HDL00100 : R_Page
{
    private LookupHDL00100ViewModel _viewModel = new LookupHDL00100ViewModel();
    private R_Grid<HDL00100DTO> GridRef;
    private R_Grid<HDL00100DTO> GridRefChild;
    private R_ConductorGrid _conductorGridRef;
    private R_ConductorGrid _conductorGridRefInvoice;
    private R_Conductor _conductorRef;

    protected override async Task R_Init_From_Master(object poParameter)
    {
        var loEx = new R_Exception();

        try
        {
            _viewModel.ParameterLookup = (HDL00100ParameterDTO)poParameter;
           await GridRef.R_RefreshGrid(null);
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
            eventArgs.ListEntityResult = _viewModel.PriceListLookupHeader;


        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        R_DisplayException(loEx);
    }
    
    public async Task R_ServiceGetListRecordDetailAsync(R_ServiceGetListRecordEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {

            await _viewModel.GetPriceListDetail();
            eventArgs.ListEntityResult = _viewModel.PriceListLookupDetail;


        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        R_DisplayException(loEx);
    }
    
    public async Task R_DisplayHDL00100(R_DisplayEventArgs eventArgs)
    {
        var loEx = new R_Exception();
        try
        {
            if (eventArgs.ConductorMode == R_eConductorMode.Normal)
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<HDL00100DTO>(eventArgs.Data);

                _viewModel.ParameterLookup.CPRICELIST_ID = loParam.CPRICELIST_ID;
                await  GridRefChild.R_RefreshGrid(null);

            }
                
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }
    
    public async Task Button_OnClickOkAsync()
    {
        if (_viewModel.PriceListLookupHeader.Count == 0)
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