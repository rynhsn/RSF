using BlazorClientHelper;
using Lookup_CBCOMMON.DTOs.CBL00100;
using Lookup_CBCOMMON.DTOs.CBL00200;
using Lookup_CBModel.ViewModel;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Exceptions;

namespace Lookup_CBFRONT;

public partial class CBL00200 :  R_Page
{
    private CBL00200ViewModel _viewModel = new();
    private R_Grid<CBL00200DTO> GridRef;
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
            _viewModel.ParameterLookup = (CBL00200ParameterDTO)poParameter;
            await _viewModel.GetInitialProcess();
            cperiod = _viewModel.ParameterLookup.CPERIOD;

            _viewModel.CBJournalLookupEntity.RadioButton = string.IsNullOrWhiteSpace(cperiod) ? "A" : "P";

            if (_viewModel.CBJournalLookupEntity.RadioButton == "P")
            {
                _viewModel.CBJournalLookupEntity.VAR_GSM_PERIOD = int.Parse(cperiod.Substring(0, 4));
                _viewModel.CBJournalLookupEntity.Month = cperiod.Substring(4, 2);
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
            await _viewModel.GetCBJournalList();
            eventArgs.ListEntityResult = _viewModel.CBJournalLookupGrid;
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
            if (_viewModel.CBJournalLookupEntity.RadioButton == "A")
            {
                _viewModel.CBJournalLookupEntity.VAR_GSM_PERIOD = 0;
                _viewModel.CBJournalLookupEntity.Month = "";
                _viewModel.CBJournalLookupEntity.CPERIOD = "";
            }
            else if (_viewModel.CBJournalLookupEntity.RadioButton == "P")

            {
                _viewModel.CBJournalLookupEntity.VAR_GSM_PERIOD = DateTime.Now.Year;
                _viewModel.CBJournalLookupEntity.Month = DateTime.Now.ToString("MM");
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
            if (_viewModel.CBJournalLookupEntity.RadioButton != "A")
            {
                _viewModel.CBJournalLookupEntity.CPERIOD =
                    _viewModel.CBJournalLookupEntity.VAR_GSM_PERIOD.ToString() +
                    _viewModel.CBJournalLookupEntity.Month;
            }
            else
            {
                _viewModel.CBJournalLookupEntity.CPERIOD = "";
            }


            if (_viewModel.CBJournalLookupEntity.RadioButton == "P" &&
                _viewModel.CBJournalLookupEntity.VAR_GSM_PERIOD == null)
            {
                await R_MessageBox.Show("Error", "Period Year is required!!", R_eMessageBoxButtonType.OK);
            }

            if (_viewModel.CBJournalLookupEntity.RadioButton == "P" &&
                _viewModel.CBJournalLookupEntity.Month == null)
            {
                await R_MessageBox.Show("Error", "Period Month is required!!", R_eMessageBoxButtonType.OK);
            }

            await _viewModel.GetCBJournalList();


            if (_viewModel.CBJournalLookupGrid.Count == 0)
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
        if (_viewModel.CBJournalLookupGrid.Count == 0)
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
        ButtonOk.Enabled = _viewModel.CBJournalLookupGrid.Count != 0;
    }

    public async Task Button_OnClickCloseAsync()
    {
        await this.Close(true, null);
    }
    
}