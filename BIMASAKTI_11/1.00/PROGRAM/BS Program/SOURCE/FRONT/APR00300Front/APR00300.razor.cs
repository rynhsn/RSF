using APR00300Common.DTOs;
using APR00300Model.ViewModel;
using BlazorClientHelper;
using Lookup_APCOMMON.DTOs.APL00100;
using Lookup_APFRONT;
using Lookup_APModel.ViewModel.APL00100;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_BlazorFrontEnd.Interfaces;

namespace APR00300Front;

public partial class APR00300 : R_Page
{
    private APR00300ViewModel _viewModel = new();

    [Inject] private R_IReport _reportService { get; set; }
    [Inject] private IClientHelper _clientHelper { get; set; }
    public R_ComboBox<APR00300PropertyDTO, string> _comboPropertyRef { get; set; }

    protected override async Task R_Init_From_Master(object poParam)
    {
        var loEx = new R_Exception();

        try
        {
            await _viewModel.Init();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    #region Lookup Supplier

    //Lookup From Supplier
    private async Task OnLostFocusFromSupplier(object eventArgs)
    {
        var loEx = new R_Exception();

        var loLookupViewModel = new LookupAPL00100ViewModel();
        try
        {
            if (string.IsNullOrEmpty(_viewModel.ReportParam.CFROM_SUPPLIER_ID))
            {
                _viewModel.ReportParam.CFROM_SUPPLIER_NAME = "";
                return;
            }

            var param = new APL00100ParameterDTO
            {
                CPROPERTY_ID = _viewModel.ReportParam.CPROPERTY_ID,
                CSEARCH_CODE = _viewModel.ReportParam.CFROM_SUPPLIER_ID
            };

            APL00100DTO loResult = null;

            loResult = await loLookupViewModel.GetSuplier(param);

            if (loResult == null)
            {
                loEx.Add(R_FrontUtility.R_GetError(
                    typeof(LookupAPFrontResources.Resources_Dummy_Class_LookupAP),
                    "_ErrLookup01"));
                _viewModel.ReportParam.CFROM_SUPPLIER_ID = "";
                _viewModel.ReportParam.CFROM_SUPPLIER_NAME = "";
                goto EndBlock;
            }

            _viewModel.ReportParam.CFROM_SUPPLIER_ID = loResult.CSUPPLIER_ID;
            _viewModel.ReportParam.CFROM_SUPPLIER_NAME = loResult.CSUPPLIER_NAME;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        EndBlock:
        R_DisplayException(loEx);
    }

    private void BeforeLookupFromSupplier(R_BeforeOpenLookupEventArgs eventArgs)
    {
        eventArgs.TargetPageType = typeof(APL00100);
        eventArgs.Parameter = new APL00100ParameterDTO
        {
            CPROPERTY_ID = _viewModel.ReportParam.CPROPERTY_ID
        };
    }

    private void AfterLookupFromSupplier(R_AfterOpenLookupEventArgs eventArgs)
    {
        var loEx = new R_Exception();
        try
        {
            var loTempResult = (APL00100DTO)eventArgs.Result;
            if (loTempResult == null)
                return;

            _viewModel.ReportParam.CFROM_SUPPLIER_ID = loTempResult.CSUPPLIER_ID;
            _viewModel.ReportParam.CFROM_SUPPLIER_NAME = loTempResult.CSUPPLIER_NAME;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }


    private async Task OnLostFocusToSupplier(object eventArgs)
    {
        var loEx = new R_Exception();

        var loLookupViewModel = new LookupAPL00100ViewModel();
        try
        {
            if (string.IsNullOrEmpty(_viewModel.ReportParam.CTO_SUPPLIER_ID))
            {
                _viewModel.ReportParam.CTO_SUPPLIER_NAME = "";
                return;
            }

            var param = new APL00100ParameterDTO
            {
                CPROPERTY_ID = _viewModel.ReportParam.CPROPERTY_ID,
                CSEARCH_CODE = _viewModel.ReportParam.CTO_SUPPLIER_ID
            };

            APL00100DTO loResult = null;

            loResult = await loLookupViewModel.GetSuplier(param);

            if (loResult == null)
            {
                loEx.Add(R_FrontUtility.R_GetError(
                    typeof(LookupAPFrontResources.Resources_Dummy_Class_LookupAP),
                    "_ErrLookup01"));
                _viewModel.ReportParam.CTO_SUPPLIER_ID = "";
                _viewModel.ReportParam.CTO_SUPPLIER_NAME = "";
                goto EndBlock;
            }

            _viewModel.ReportParam.CTO_SUPPLIER_ID = loResult.CSUPPLIER_ID;
            _viewModel.ReportParam.CTO_SUPPLIER_NAME = loResult.CSUPPLIER_NAME;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        EndBlock:
        R_DisplayException(loEx);
    }

    private void BeforeLookupToSupplier(R_BeforeOpenLookupEventArgs eventArgs)
    {
        eventArgs.TargetPageType = typeof(APL00100);
        eventArgs.Parameter = new APL00100ParameterDTO
        {
            CPROPERTY_ID = _viewModel.ReportParam.CPROPERTY_ID
        };
    }

    private void AfterLookupToSupplier(R_AfterOpenLookupEventArgs eventArgs)
    {
        var loEx = new R_Exception();
        try
        {
            var loTempResult = (APL00100DTO)eventArgs.Result;
            if (loTempResult == null)
                return;

            _viewModel.ReportParam.CTO_SUPPLIER_ID = loTempResult.CSUPPLIER_ID;
            _viewModel.ReportParam.CTO_SUPPLIER_NAME = loTempResult.CSUPPLIER_NAME;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    #endregion
    
    private async Task OnClickPrint()
    {
        var loEx = new R_Exception();

        try
        {
            _viewModel.ValidateDataBeforePrint();

            if (!loEx.HasError)
            {
                var loParam = _viewModel.ReportParam;
                loParam.CCOMPANY_ID = _clientHelper.CompanyId;
                loParam.CUSER_ID = _clientHelper.UserId;
                loParam.CLANG_ID = _clientHelper.CultureUI.TwoLetterISOLanguageName;
                loParam.LIS_PRINT = true;
                loParam.CREPORT_FILENAME = "";
                loParam.CREPORT_FILETYPE = "";
                await _reportService.GetReport(
                    "R_DefaultServiceUrlAP",
                    "AP",
                    "rpt/APR00300Print/ActivityReportPost",
                    "rpt/APR00300Print/ActivityReportGet",
                    loParam
                );
            }
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        EndBlock:
        loEx.ThrowExceptionIfErrors();
    }
    
    private void BeforeOpenPopupSaveAs(R_BeforeOpenPopupEventArgs eventArgs)
    {
        var loEx = new R_Exception();
        try
        {
            // _validateDataBeforePrint();

            var loParam = _viewModel.ReportParam;
            eventArgs.Parameter = loParam;
            eventArgs.PageTitle = _localizer["SaveAs"];
            eventArgs.TargetPageType = typeof(APR00300PopupSaveAs);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        EndBlock:
        loEx.ThrowExceptionIfErrors();
    }

    private async Task OnLostFocusProperty(object obj)
    {
        var loEx = new R_Exception();
        try
        {
            if (string.IsNullOrEmpty(_viewModel.ReportParam.CPROPERTY_ID))
            {
                await _comboPropertyRef.FocusAsync();
            }
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        EndBlock:
        R_DisplayException(loEx);
    }
}