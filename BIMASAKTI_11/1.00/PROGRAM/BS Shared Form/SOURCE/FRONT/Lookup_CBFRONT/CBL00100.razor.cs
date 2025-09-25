using BlazorClientHelper;
using Lookup_CBCOMMON.DTOs.CBL00100;
using Lookup_CBModel.ViewModel;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Exceptions;

namespace Lookup_CBFRONT;

public partial class CBL00100 : R_Page
{
    private CBL00100ViewModel _viewModel = new();
    private R_Grid<CBL00100DTO> GridRef;
    private R_Conductor _conductorRef;
    private R_ConductorGrid _conGrid;
    private string cperiod = "";
    [Inject] private IClientHelper ClientHelper { get; set; }
    public R_Button ButtonOk { get; set; }

    protected override async Task R_Init_From_Master(object poParameter)
    {
        var loEx = new R_Exception();

        try
        {
            _viewModel.ParameterLookup = (CBL00100ParameterDTO)poParameter;
            await _viewModel.GetInitialProcess();
            cperiod = _viewModel.ParameterLookup.CPERIOD;

            _viewModel.ReceiptFromCustomerLookupEntity.RadioButton = string.IsNullOrWhiteSpace(cperiod) ? "A" : "P";

            if (_viewModel.ReceiptFromCustomerLookupEntity.RadioButton == "P")
            {
                _viewModel.ReceiptFromCustomerLookupEntity.VAR_GSM_PERIOD = int.Parse(cperiod.Substring(0, 4));
                _viewModel.ReceiptFromCustomerLookupEntity.Month = cperiod.Substring(4, 2);
            }

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
            await _viewModel.GetReceiptFromCustomerList();
            eventArgs.ListEntityResult = _viewModel.ReceiptFromCustomerLookupGrid;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        R_DisplayException(loEx);
    }

    public async Task OnchangedPeriod()
    {
        var loEx = new R_Exception();

        try
        {
            if (_viewModel.ReceiptFromCustomerLookupEntity.RadioButton == "A")
            {
                _viewModel.ReceiptFromCustomerLookupEntity.VAR_GSM_PERIOD = 0;
                _viewModel.ReceiptFromCustomerLookupEntity.Month = "";
                _viewModel.ReceiptFromCustomerLookupEntity.CPERIOD = "";
            }
            else if (_viewModel.ReceiptFromCustomerLookupEntity.RadioButton == "P")

            {
                _viewModel.ReceiptFromCustomerLookupEntity.VAR_GSM_PERIOD = DateTime.Now.Year;
                _viewModel.ReceiptFromCustomerLookupEntity.Month = DateTime.Now.ToString("MM");
            }
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    public async Task Refresh_Button()
    {
        var loEx = new R_Exception();

        try
        {
            if (_viewModel.ReceiptFromCustomerLookupEntity.RadioButton != "A")
            {
                _viewModel.ReceiptFromCustomerLookupEntity.CPERIOD =
                    _viewModel.ReceiptFromCustomerLookupEntity.VAR_GSM_PERIOD.ToString() +
                    _viewModel.ReceiptFromCustomerLookupEntity.Month;
            }
            else
            {
                _viewModel.ReceiptFromCustomerLookupEntity.CPERIOD = "";
            }


            if (_viewModel.ReceiptFromCustomerLookupEntity.RadioButton == "P" &&
                _viewModel.ReceiptFromCustomerLookupEntity.VAR_GSM_PERIOD == null)
            {
                await R_MessageBox.Show("Error", "Period Year is required!!", R_eMessageBoxButtonType.OK);
            }

            if (_viewModel.ReceiptFromCustomerLookupEntity.RadioButton == "P" &&
                _viewModel.ReceiptFromCustomerLookupEntity.Month == null)
            {
                await R_MessageBox.Show("Error", "Period Month is required!!", R_eMessageBoxButtonType.OK);
            }

            await _viewModel.GetReceiptFromCustomerList();


            if (_viewModel.ReceiptFromCustomerLookupGrid.Count == 0)
            {
                await R_MessageBox.Show("Error", "No data found!", R_eMessageBoxButtonType.OK);
                return;
            }

            // await ButtonEnable();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    public async Task Button_OnClickOkAsync()
    {
        if (_viewModel.ReceiptFromCustomerLookupGrid.Count == 0)
        {
            await R_MessageBox.Show("Error", "Data not found!", R_eMessageBoxButtonType.OK);
            return;
        }
        else
        {
            var loData = GridRef.GetCurrentData();
            await this.Close(true, loData);
        }
    }

    public async Task ButtonEnable()
    {
        ButtonOk.Enabled = _viewModel.ReceiptFromCustomerLookupGrid.Count != 0;
    }

    public async Task Button_OnClickCloseAsync()
    {
        await this.Close(true, null);
    }
}