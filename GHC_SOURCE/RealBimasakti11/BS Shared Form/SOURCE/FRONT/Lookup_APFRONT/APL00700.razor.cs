using BlazorClientHelper;
using Lookup_APCOMMON.DTOs.APL00500;
using Lookup_APCOMMON.DTOs.APL00600;
using Lookup_APCOMMON.DTOs.APL00700;
using Lookup_APModel.ViewModel.APL00600;
using Lookup_APModel.ViewModel.APL00700;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using Lookup_GSModel.ViewModel;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Exceptions;

namespace Lookup_APFRONT;

public partial class APL00700 : R_Page
{
    private LookupAPL00700ViewModel _viewModel = new LookupAPL00700ViewModel();
    private R_Grid<APL00700DTO> GridRef;
    private R_Conductor _conductorRef;
    private R_ConductorGrid _conGrid;
    [Inject] private IClientHelper ClientHelper { get; set; }
    public R_Button ButtonOk { get; set; }
    
    protected override async Task R_Init_From_Master(object poParameter)
    {
        var loEx = new R_Exception();

        try
            {
            _viewModel.ParameterLookup = (APL00700ParameterDTO)poParameter;
            _viewModel.GetInitialCancelPaymentToSupplierLookup();
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
            await _viewModel.GetCancelPaymentToSupplierLookup();
            eventArgs.ListEntityResult = _viewModel.CancelPaymentToSupplierGrid;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        R_DisplayException(loEx);
    }
    
    private Task R_BeforeOpenLookUpSupplier(R_BeforeOpenLookupEventArgs eventArgs)
    {
        var loEx = new R_Exception();
    
        try
        {
            eventArgs.Parameter = new GSL02900ParameterDTO()
            {
        
            };
            eventArgs.TargetPageType = typeof(GSL02900);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }
    
        loEx.ThrowExceptionIfErrors();
        return Task.CompletedTask;
    }
    private async Task LookupSupplierOnLostFocus()
    {
        var loEx = new R_Exception();
    
        try
        {
            if (_viewModel.SupplierLookupEntity.CSUPPLIER_ID != "")
            {
                var param = new GSL02900ParameterDTO()
                {
                    CSEARCH_TEXT = _viewModel.SupplierLookupEntity.CSUPPLIER_ID,
                };
    
                LookupGSL02900ViewModel loLookupViewModel = new LookupGSL02900ViewModel();
    
                var loResult = await loLookupViewModel.GetSupplier(param);
    
                if (loResult == null)
                {
                    loEx.Add(_localizer["Error"],
                        $"{_localizer["Supplier"]} {_viewModel.SupplierLookupEntity.CSUPPLIER_ID} {_localizer["Not Found"]}");
                    _viewModel.SupplierLookupEntity.CSUPPLIER_ID = "";
                    _viewModel.SupplierLookupEntity.CSUPPLIER_NAME = "";
                    goto EndBlock;
                }
    
                _viewModel.SupplierLookupEntity.CSUPPLIER_ID = loResult.CSUPPLIER_ID;
                _viewModel.SupplierLookupEntity.CSUPPLIER_NAME = loResult.CSUPPLIER_NAME;
            }
            else
            {
                _viewModel.SupplierLookupEntity.CSUPPLIER_ID = "";
                _viewModel.SupplierLookupEntity.CSUPPLIER_NAME = "";
            }
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }
    
        EndBlock:
        R_DisplayException(loEx);
    }
    
    private void R_AfterOpenLookUpSupplier(R_AfterOpenLookupEventArgs eventArgs)
    {
        var loEx = new R_Exception();
    
        try
        {
            var loData = (GSL02900DTO)eventArgs.Result;
            if (loData == null)
                return;
    
            _viewModel.SupplierLookupEntity.CSUPPLIER_ID = loData.CSUPPLIER_ID;
            _viewModel.SupplierLookupEntity.CSUPPLIER_NAME = loData.CSUPPLIER_NAME;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }
    
        loEx.ThrowExceptionIfErrors();
        //return Task.CompletedTask;
    }
    
     private Task R_BeforeOpenLookUpSchedulePayement(R_BeforeOpenLookupEventArgs eventArgs)
    {
        var loEx = new R_Exception();
    
        try
        {
            eventArgs.Parameter = new APL00600ParameterDTO()
            {
               CSUPPLIER_ID = _viewModel.SupplierLookupEntity.CSUPPLIER_ID,
        
            };
            eventArgs.TargetPageType = typeof(APL00600);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }
    
        loEx.ThrowExceptionIfErrors();
        return Task.CompletedTask;
    }
    private async Task LookupSchedulePayementOnLostFocus()
    {
        var loEx = new R_Exception();
    
        try
        {
            if (_viewModel.SchedulePaymentLookupEntity.CSCH_PAYMENT_ID != "")
            {
                var param = new APL00600ParameterDTO()
                {
                    CSEARCH_CODE = _viewModel.SchedulePaymentLookupEntity.CSCH_PAYMENT_ID,
                    CSUPPLIER_ID = _viewModel.SupplierLookupEntity.CSUPPLIER_ID
                };
    
                LookupAPL00600ViewModel loLookupViewModel = new LookupAPL00600ViewModel();
    
                var loResult = await loLookupViewModel.GetSchedulePayment(param);
    
                if (loResult == null)
                {
                    loEx.Add(_localizer["Error"],
                        $"{_localizer["Schedule Payment"]} {_viewModel.SchedulePaymentLookupEntity.CREF_NO} {_localizer["Not Found"]}");
                    _viewModel.SchedulePaymentLookupEntity.CSCH_PAYMENT_ID = "";
                    goto EndBlock;
                }
    
                _viewModel.SchedulePaymentLookupEntity.CSCH_PAYMENT_ID = loResult.CREF_NO;
            }
            else
            {
                _viewModel.SchedulePaymentLookupEntity.CSCH_PAYMENT_ID = "";
            }
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }
    
        EndBlock:
        R_DisplayException(loEx);
    }
    
    private void R_AfterOpenLookUpSchedulePayment(R_AfterOpenLookupEventArgs eventArgs)
    {
        var loEx = new R_Exception();
    
        try
        {
            var loData = (APL00600DTO)eventArgs.Result;
            if (loData == null)
                return;
    
            _viewModel.SchedulePaymentLookupEntity.CSCH_PAYMENT_ID = loData.CREF_NO;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }
    
        loEx.ThrowExceptionIfErrors();
        //return Task.CompletedTask;
    }

     public async Task Refresh_Button()
    {
        var loEx = new R_Exception();

        try
        {
            

            if (_viewModel.SupplierLookupEntity.CSUPPLIER_ID == null)
            {
                await R_MessageBox.Show("Error", "Please select Supplier!", R_eMessageBoxButtonType.OK);
                return;
            }
          
            await _viewModel.GetCancelPaymentToSupplierLookup();


            if (_viewModel.CancelPaymentToSupplierGrid.Count == 0)
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
        if (_viewModel.CancelPaymentToSupplierGrid.Count == 0)
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
    

    public async Task Button_OnClickCloseAsync()
    {
        await this.Close(true, null);
    }
}