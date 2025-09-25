using BlazorClientHelper;
using Lookup_APCOMMON.DTOs.APL00100;
using Lookup_APCOMMON.DTOs.APL00300;
using Lookup_APCOMMON.DTOs.APL00500;
using Lookup_APModel.ViewModel.APL00500;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Exceptions;

namespace Lookup_APFRONT;

public partial class APL00500 : R_Page
{
    private LookupAPL00500ViewModel _viewModel = new LookupAPL00500ViewModel();
    private R_Grid<APL00500DTO> GridRef;
    private R_Conductor _conductorRef;
    private R_ConductorGrid _conGrid;
    [Inject] private IClientHelper ClientHelper { get; set; }
    public R_Button ButtonOk { get; set; }

    protected override async Task R_Init_From_Master(object poParameter)
    {
        var loEx = new R_Exception();

        try
        {
            _viewModel.ParameterLookup = (APL00500ParameterDTO)poParameter;
            _viewModel.GetInitialTransactionLookup();
            // ButtonEnable();
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
            await _viewModel.GetTransactionLookup();
            eventArgs.ListEntityResult = _viewModel.TransactionLookupGrid;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        R_DisplayException(loEx);
    }
    // private Task R_BeforeOpenLookUp(R_BeforeOpenLookupEventArgs eventArgs)
    // {
    //     var loEx = new R_Exception();
    //
    //     try
    //     {
    //         eventArgs.Parameter = new APL00100ParameterDTO()
    //         {
    //             CCOMPANY_ID = ClientHelper.CompanyId,
    //             CLANGUAGE_ID = ClientHelper.Culture.ToString(),
    //             CPROPERTY_ID = _viewModel.ParameterLookup.CPROPERTY_ID,
    //         };
    //         eventArgs.TargetPageType = typeof(APL00100);
    //     }
    //     catch (Exception ex)
    //     {
    //         loEx.Add(ex);
    //     }
    //
    //     loEx.ThrowExceptionIfErrors();
    //     return Task.CompletedTask;
    // }
    //
    // private void R_AfterOpenLookUp(R_AfterOpenLookupEventArgs eventArgs)
    // {
    //     var loEx = new R_Exception();
    //
    //     try
    //     {
    //         var loData = (APL00100DTO)eventArgs.Result;
    //         if (loData == null)
    //             return;
    //
    //         _viewModel.TransactionLookupEntity.CSUPPLIER_ID = loData.CSUPPLIER_ID;
    //         _viewModel.TransactionLookupEntity.CSUPPLIER_NAME = loData.CSUPPLIER_NAME;
    //     }
    //     catch (Exception ex)
    //     {
    //         loEx.Add(ex);
    //     }
    //
    //     loEx.ThrowExceptionIfErrors();
    //     //return Task.CompletedTask;
    // }

    public async Task OnchangedPeriod()
    {
        var loEx = new R_Exception();

        try
        {
            if (_viewModel.TransactionLookupEntity.RadioButton == "A")
            {
                _viewModel.TransactionLookupEntity.CPERIOD = "";
            }
            else
            {
                _viewModel.TransactionLookupEntity.VAR_GSM_PERIOD = DateTime.Now.Year;
                _viewModel.TransactionLookupEntity.Month = DateTime.Now.ToString("MM");
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

            await _viewModel.GetTransactionLookup();


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
            // || _viewModel.TransactionLookupEntity.CSUPPLIER_ID != "";
        // ButtonOk.Enabled = _viewModel.TransactionLookupEntity.CSUPPLIER_ID != "";

        // if (_viewModel.TransactionLookupEntity.CSUPPLIER_ID == "")
        // {
        //     ButtonOk.Enabled = false;
        // }
        // else
        // {
        //     ButtonOk.Enabled = true;
        // }
    }

    public async Task Button_OnClickCloseAsync()
    {
        await this.Close(true, null);
    }
}