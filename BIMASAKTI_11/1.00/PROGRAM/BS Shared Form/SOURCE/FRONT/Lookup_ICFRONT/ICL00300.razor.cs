using BlazorClientHelper;
using Lookup_ICCOMMON.DTOs.ICL00300;
using Lookup_ICModel.ViewModel.ICL00300;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Exceptions;

namespace Lookup_ICFRONT;

public partial class ICL00300 : R_Page
{
    private LookupICL00300ViewModel _viewModel = new();
    private R_Grid<ICL00300DTO> GridRef;
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
            _viewModel.ParameterLookup = (ICL00300ParameterDTO)poParameter;
            await _viewModel.GetInitialTransactionLookupList();
            cperiod = _viewModel.ParameterLookup.CPERIOD;
            _viewModel.TransactionLookupEntity.RadioButton = string.IsNullOrEmpty(cperiod) ? "A" : "P";

            await OnchangedPeriod();
            
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
            await _viewModel.GetTransactionList();
            eventArgs.ListEntityResult = _viewModel.TransactionLookupGrid;
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
            if (_viewModel.TransactionLookupEntity.RadioButton == "A")
            {
                _viewModel.TransactionLookupEntity.VAR_GSM_PERIOD = 0;
                _viewModel.TransactionLookupEntity.Month = "";
                _viewModel.TransactionLookupEntity.CPERIOD = "";
            }
            else if (_viewModel.TransactionLookupEntity.RadioButton == "P")

            {
                if (!string.IsNullOrEmpty(cperiod) && cperiod.Length >= 6)
                {
                    _viewModel.TransactionLookupEntity.VAR_GSM_PERIOD = int.Parse(cperiod.Substring(0, 4));
                    _viewModel.TransactionLookupEntity.Month = cperiod.Substring(4, 2);
                }
                else
                {
                    _viewModel.TransactionLookupEntity.VAR_GSM_PERIOD = DateTime.Now.Year;
                    _viewModel.TransactionLookupEntity.Month = DateTime.Now.ToString("MM");
                }
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
            if (_viewModel.TransactionLookupEntity.RadioButton != "A")
            {
                _viewModel.TransactionLookupEntity.CPERIOD =
                    _viewModel.TransactionLookupEntity.VAR_GSM_PERIOD.ToString() +
                    _viewModel.TransactionLookupEntity.Month;
            }
            else
            {
                _viewModel.TransactionLookupEntity.CPERIOD = "";
            }


            if (_viewModel.TransactionLookupEntity.RadioButton == "P" &&
                _viewModel.TransactionLookupEntity.VAR_GSM_PERIOD == null)
            {
                await R_MessageBox.Show("Error", "Period Year is required!!", R_eMessageBoxButtonType.OK);
            }

            if (_viewModel.TransactionLookupEntity.RadioButton == "P" &&
                _viewModel.TransactionLookupEntity.Month == null)
            {
                await R_MessageBox.Show("Error", "Period Month is required!!", R_eMessageBoxButtonType.OK);
            }

            await _viewModel.GetTransactionList();


            if (_viewModel.TransactionLookupGrid.Count == 0)
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
        if (_viewModel.TransactionLookupGrid.Count == 0)
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
        ButtonOk.Enabled = _viewModel.TransactionLookupGrid.Count != 0;
    }

    public async Task Button_OnClickCloseAsync()
    {
        await this.Close(true, null);
    }
}