using APR00500Model.ViewModel;
using BlazorClientHelper;
using Lookup_APCOMMON.DTOs.APL00100;
using Lookup_APCOMMON.DTOs.APL00500;
using Lookup_APFRONT;
using Lookup_APModel.ViewModel.APL00100;
using Lookup_APModel.ViewModel.APL00500;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using Lookup_GSModel.ViewModel;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_BlazorFrontEnd.Interfaces;


namespace APR00500Front;

public partial class APR00500
{
    private APR00500ViewModel _viewModel = new();

    [Inject] private R_IReport _reportService { get; set; }
    [Inject] private IClientHelper _clientHelper { get; set; }

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

    private async Task OnLostFocusDept(object eventArgs)
    {
        var loEx = new R_Exception();

        var loLookupViewModel = new LookupGSL00710ViewModel();
        try
        {
            if (string.IsNullOrEmpty(_viewModel.ReportParam.CDEPT_CODE))
            {
                _viewModel.ReportParam.CDEPT_NAME = "";
                return;
            }

            var param = new GSL00710ParameterDTO
            {
                CPROPERTY_ID = _viewModel.ReportParam.CPROPERTY_ID,
                CSEARCH_TEXT = _viewModel.ReportParam.CDEPT_CODE
            };

            GSL00710DTO loResult = null;

            loResult = await loLookupViewModel.GetDepartmentProperty(param);

            if (loResult == null)
            {
                loEx.Add(R_FrontUtility.R_GetError(
                    typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                    "_ErrLookup01"));
                _viewModel.ReportParam.CDEPT_CODE = "";
                _viewModel.ReportParam.CDEPT_NAME = "";
                goto EndBlock;
            }

            _viewModel.ReportParam.CDEPT_CODE = loResult.CDEPT_CODE;
            _viewModel.ReportParam.CDEPT_NAME = loResult.CDEPT_NAME;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        EndBlock:
        R_DisplayException(loEx);
    }

    private void BeforeLookupDept(R_BeforeOpenLookupEventArgs eventArgs)
    {
        eventArgs.TargetPageType = typeof(GSL00710);
        eventArgs.Parameter = new GSL00710ParameterDTO
        {
            CPROPERTY_ID = _viewModel.ReportParam.CPROPERTY_ID
        };
    }

    private void AfterLookupDept(R_AfterOpenLookupEventArgs eventArgs)
    {
        var loEx = new R_Exception();
        try
        {
            var loTempResult = (GSL00710DTO)eventArgs.Result;
            if (loTempResult == null)
                return;

            _viewModel.ReportParam.CDEPT_CODE = loTempResult.CDEPT_CODE;
            _viewModel.ReportParam.CDEPT_NAME = loTempResult.CDEPT_NAME;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task OnLostFocusSupplier(object eventArgs)
    {
        var loEx = new R_Exception();

        var loLookupViewModel = new LookupAPL00100ViewModel();
        try
        {
            if (string.IsNullOrEmpty(_viewModel.ReportParam.CSUPPLIER_ID))
            {
                _viewModel.ReportParam.CSUPPLIER_NAME = "";
                return;
            }

            var param = new APL00100ParameterDTO
            {
                CPROPERTY_ID = _viewModel.ReportParam.CPROPERTY_ID,
                CSEARCH_CODE = _viewModel.ReportParam.CSUPPLIER_ID
            };

            APL00100DTO loResult = null;

            loResult = await loLookupViewModel.GetSuplier(param);

            if (loResult == null)
            {
                loEx.Add(R_FrontUtility.R_GetError(
                    typeof(LookupAPFrontResources.Resources_Dummy_Class_LookupAP),
                    "_ErrLookup01"));
                _viewModel.ReportParam.CSUPPLIER_ID = "";
                _viewModel.ReportParam.CSUPPLIER_NAME = "";
                goto EndBlock;
            }

            _viewModel.ReportParam.CSUPPLIER_ID = loResult.CSUPPLIER_ID;
            _viewModel.ReportParam.CSUPPLIER_NAME = loResult.CSUPPLIER_NAME;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        EndBlock:
        R_DisplayException(loEx);
    }

    private void BeforeLookupSupplier(R_BeforeOpenLookupEventArgs eventArgs)
    {
        eventArgs.TargetPageType = typeof(APL00100);
        eventArgs.Parameter = new APL00100ParameterDTO
        {
            CPROPERTY_ID = _viewModel.ReportParam.CPROPERTY_ID
        };
    }

    private void AfterLookupSupplier(R_AfterOpenLookupEventArgs eventArgs)
    {
        var loEx = new R_Exception();
        try
        {
            var loTempResult = (APL00100DTO)eventArgs.Result;
            if (loTempResult == null)
                return;

            _viewModel.ReportParam.CSUPPLIER_ID = loTempResult.CSUPPLIER_ID;
            _viewModel.ReportParam.CSUPPLIER_NAME = loTempResult.CSUPPLIER_NAME;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task<bool> _validasiRefNo()
    {
        if (string.IsNullOrEmpty(_viewModel.ReportParam.CPROPERTY_ID))
        {
            var loMsgProperty =
                await R_MessageBox.Show("", @_localizer["ERR_PROPERTY_EMPTY"], R_eMessageBoxButtonType.OK);
            return false;
        }

        if (_viewModel.CheckDept && string.IsNullOrEmpty(_viewModel.ReportParam.CDEPT_CODE))
        {
            var loMsgDept = await R_MessageBox.Show("", @_localizer["ERR_DEPT_EMPTY"], R_eMessageBoxButtonType.OK);
            return false;
        }

        if (_viewModel.CheckSupplier && string.IsNullOrEmpty(_viewModel.ReportParam.CSUPPLIER_ID))
        {
            var loMsgProperty = await R_MessageBox.Show("", @_localizer["ERR_SUPPL_EMPTY"], R_eMessageBoxButtonType.OK);
            return false;
        }

        return true;
    }

    private async Task OnLostFocusFromRefNo(object eventArgs)
    {
        var loEx = new R_Exception();

        var loLookupViewModel = new LookupAPL00500ViewModel();
        try
        {
            if (string.IsNullOrEmpty(_viewModel.ReportParam.CFROM_REFERENCE_NO))
            {
                return;
            }

            if (!await _validasiRefNo())
            {
                return;
            }

            var param = new APL00500ParameterDTO
            {
                CPROPERTY_ID = _viewModel.ReportParam.CPROPERTY_ID,
                CDEPT_CODE = _viewModel.ReportParam.CDEPT_CODE,
                CSUPPLIER_ID = _viewModel.ReportParam.CSUPPLIER_ID,
                CTRANS_CODE = "110010",
                CTRANS_NAME = _viewModel.TransCodeInfo.CTRANSACTION_NAME,
                LHAS_REMAINING = true,
                LNO_REMAINING = false,
                CSEARCH_CODE = _viewModel.ReportParam.CFROM_REFERENCE_NO
            };

            APL00500DTO loResult = null;

            loResult = await loLookupViewModel.GetTransactionLookup(param);

            if (loResult == null)
            {
                loEx.Add(R_FrontUtility.R_GetError(
                    typeof(LookupAPFrontResources.Resources_Dummy_Class_LookupAP),
                    "_ErrLookup01"));
                _viewModel.ReportParam.CFROM_REFERENCE_NO = "";
                goto EndBlock;
            }

            _viewModel.ReportParam.CFROM_REFERENCE_NO = loResult.CREF_NO;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        EndBlock:
        R_DisplayException(loEx);
    }

    private async Task BeforeLookupFromRefNo(R_BeforeOpenLookupEventArgs eventArgs)
    {
        if (!await _validasiRefNo())
        {
            return;
        }

        eventArgs.TargetPageType = typeof(APL00500);
        eventArgs.Parameter = new APL00500ParameterDTO
        {
            CPROPERTY_ID = _viewModel.ReportParam.CPROPERTY_ID,
            CDEPT_CODE = _viewModel.ReportParam.CDEPT_CODE,
            CSUPPLIER_ID = _viewModel.ReportParam.CSUPPLIER_ID,
            CTRANS_CODE = "110010",
            CTRANS_NAME = _viewModel.TransCodeInfo.CTRANSACTION_NAME,
            LHAS_REMAINING = true,
            LNO_REMAINING = false,
        };
    }

    private void AfterLookupFromRefNo(R_AfterOpenLookupEventArgs eventArgs)
    {
        var loEx = new R_Exception();
        try
        {
            var loTempResult = (APL00500DTO)eventArgs.Result;
            if (loTempResult == null)
                return;

            _viewModel.ReportParam.CFROM_REFERENCE_NO = loTempResult.CREF_NO;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task OnLostFocusToRefNo(object eventArgs)
    {
        var loEx = new R_Exception();

        var loLookupViewModel = new LookupAPL00500ViewModel();
        try
        {
            if (string.IsNullOrEmpty(_viewModel.ReportParam.CTO_REFERENCE_NO))
            {
                return;
            }

            if (!await _validasiRefNo())
            {
                return;
            }

            var param = new APL00500ParameterDTO
            {
                CPROPERTY_ID = _viewModel.ReportParam.CPROPERTY_ID,
                CDEPT_CODE = _viewModel.ReportParam.CDEPT_CODE,
                CSUPPLIER_ID = _viewModel.ReportParam.CSUPPLIER_ID,
                CTRANS_CODE = "110010",
                CTRANS_NAME = _viewModel.TransCodeInfo.CTRANSACTION_NAME,
                LHAS_REMAINING = true,
                LNO_REMAINING = false,
                CSEARCH_CODE = _viewModel.ReportParam.CTO_REFERENCE_NO
            };

            APL00500DTO loResult = null;

            loResult = await loLookupViewModel.GetTransactionLookup(param);

            if (loResult == null)
            {
                loEx.Add(R_FrontUtility.R_GetError(
                    typeof(LookupAPFrontResources.Resources_Dummy_Class_LookupAP),
                    "_ErrLookup01"));
                _viewModel.ReportParam.CTO_REFERENCE_NO = "";
                goto EndBlock;
            }

            _viewModel.ReportParam.CTO_REFERENCE_NO = loResult.CREF_NO;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        EndBlock:
        R_DisplayException(loEx);
    }

    private async Task BeforeLookupToRefNo(R_BeforeOpenLookupEventArgs eventArgs)
    {
        if (!await _validasiRefNo())
        {
            return;
        }

        eventArgs.TargetPageType = typeof(APL00500);
        eventArgs.Parameter = new APL00500ParameterDTO
        {
            CPROPERTY_ID = _viewModel.ReportParam.CPROPERTY_ID,
            CDEPT_CODE = _viewModel.ReportParam.CDEPT_CODE,
            CSUPPLIER_ID = _viewModel.ReportParam.CSUPPLIER_ID,
            CTRANS_CODE = "110010",
            CTRANS_NAME = _viewModel.TransCodeInfo.CTRANSACTION_NAME,
            LHAS_REMAINING = true,
            LNO_REMAINING = false,
        };
    }

    private void AfterLookupToRefNo(R_AfterOpenLookupEventArgs eventArgs)
    {
        var loEx = new R_Exception();
        try
        {
            var loTempResult = (APL00500DTO)eventArgs.Result;
            if (loTempResult == null)
                return;

            _viewModel.ReportParam.CTO_REFERENCE_NO = loTempResult.CREF_NO;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    #region OnLostFocusCurrency

    // private async Task OnLostFocusCurrency(object eventArgs)
    // {
    //     var loEx = new R_Exception();
    //
    //     var loLookupViewModel = new LookupGSL00300ViewModel();
    //     try
    //     {
    //         if (_viewModel.ReportParam.CCURRENCY == null ||
    //             _viewModel.ReportParam.CCURRENCY.Trim().Length <= 0)
    //         {
    //             _viewModel.ReportParam.CCURRENCY_NAME = "";
    //             return;
    //         }
    //
    //         var param = new GSL00300ParameterDTO
    //         {
    //             CSEARCH_TEXT = _viewModel.ReportParam.CDEPT_CODE
    //         };
    //
    //         GSL00300DTO loResult = null;
    //
    //         loResult = await loLookupViewModel.GetCurrency(param);
    //
    //         if (loResult == null)
    //         {
    //             loEx.Add(R_FrontUtility.R_GetError(
    //                 typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
    //                 "_ErrLookup01"));
    //             _viewModel.ReportParam.CCURRENCY = "";
    //             _viewModel.ReportParam.CCURRENCY_NAME = "";
    //             goto EndBlock;
    //         }
    //
    //         _viewModel.ReportParam.CCURRENCY = loResult.CCURRENCY_CODE;
    //         _viewModel.ReportParam.CCURRENCY_NAME = loResult.CCURRENCY_NAME;
    //     }
    //     catch (Exception ex)
    //     {
    //         loEx.Add(ex);
    //     }
    //
    //     EndBlock:
    //     R_DisplayException(loEx);
    // }

    #endregion

    private void BeforeLookupCurrency(R_BeforeOpenLookupEventArgs eventArgs)
    {
        eventArgs.TargetPageType = typeof(GSL00300);
        eventArgs.Parameter = new GSL00300ParameterDTO();
    }

    private void AfterLookupCurrency(R_AfterOpenLookupEventArgs eventArgs)
    {
        var loEx = new R_Exception();
        try
        {
            var loTempResult = (GSL00300DTO)eventArgs.Result;
            if (loTempResult == null)
                return;

            _viewModel.ReportParam.CCURRENCY = loTempResult.CCURRENCY_CODE;
            _viewModel.ReportParam.CCURRENCY_NAME = loTempResult.CCURRENCY_NAME;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private void _validateDataBeforePrint()
    {
        var loEx = new R_Exception();

        try
        {
            _viewModel.ReportParam.CPROPERTY_NAME = _viewModel.PropertyList
                ?.Find(x => x.CPROPERTY_ID == _viewModel.ReportParam.CPROPERTY_ID)
                ?.CPROPERTY_NAME;
            _viewModel.ReportParam.CCUT_OFF_DATE = _viewModel.ReportParam.DCUT_OFF_DATE?.ToString("yyyyMMdd");
            _viewModel.ReportParam.CFROM_PERIOD = _viewModel.ReportParam.IFROM_PERIOD_YY +
                                                  _viewModel.ReportParam.CFROM_PERIOD_MM;
            _viewModel.ReportParam.CTO_PERIOD = _viewModel.ReportParam.ITO_PERIOD_YY +
                                                _viewModel.ReportParam.CTO_PERIOD_MM;
            _viewModel.ReportParam.CFROM_REFERENCE_DATE =
                _viewModel.ReportParam.DFROM_REFERENCE_DATE?.ToString("yyyyMMdd");
            _viewModel.ReportParam.CTO_REFERENCE_DATE = _viewModel.ReportParam.DTO_REFERENCE_DATE?.ToString("yyyyMMdd");
            _viewModel.ReportParam.CFROM_DUE_DATE = _viewModel.ReportParam.DFROM_DUE_DATE?.ToString("yyyyMMdd");
            _viewModel.ReportParam.CTO_DUE_DATE = _viewModel.ReportParam.DTO_DUE_DATE?.ToString("yyyyMMdd");

            var propertyIsNull = string.IsNullOrEmpty(_viewModel.ReportParam.CPROPERTY_ID);
            var cutOffDateIsNull = string.IsNullOrEmpty(_viewModel.ReportParam.CCUT_OFF_DATE);
            var fromPeriodIsNull = string.IsNullOrEmpty(_viewModel.ReportParam.CFROM_PERIOD);
            var toPeriodIsNull = string.IsNullOrEmpty(_viewModel.ReportParam.CTO_PERIOD);
            var deptIsNull = _viewModel.CheckDept && string.IsNullOrEmpty(_viewModel.ReportParam.CDEPT_CODE);
            var fromRefDateIsNull = _viewModel.CheckRefDate &&
                                    string.IsNullOrEmpty(_viewModel.ReportParam.CFROM_REFERENCE_DATE);
            var toRefDateIsNull = _viewModel.CheckRefDate &&
                                  string.IsNullOrEmpty(_viewModel.ReportParam.CTO_REFERENCE_DATE);
            var fromDueDateIsNull =
                _viewModel.CheckDueDate && string.IsNullOrEmpty(_viewModel.ReportParam.CFROM_DUE_DATE);
            var toDueDateIsNull = _viewModel.CheckDueDate && string.IsNullOrEmpty(_viewModel.ReportParam.CTO_DUE_DATE);
            var supplierIsNull = _viewModel.CheckSupplier && string.IsNullOrEmpty(_viewModel.ReportParam.CSUPPLIER_ID);
            var fromRefNoIsNull =
                _viewModel.CheckRefNo && string.IsNullOrEmpty(_viewModel.ReportParam.CFROM_REFERENCE_NO);
            var toRefNoIsNull = _viewModel.CheckRefNo && string.IsNullOrEmpty(_viewModel.ReportParam.CTO_REFERENCE_NO);
            var currencyIsNull = _viewModel.CheckCurrency && string.IsNullOrEmpty(_viewModel.ReportParam.CCURRENCY);
            // var TotalAmountIsNull = _viewModel.CheckTotalAmt && _viewModel.ReportParam.NFROM_TOTAL_AMOUNT == 0 && _viewModel.ReportParam.NTO_TOTAL_AMOUNT == 0;
            // var RemainingAmountIsNull = _viewModel.CheckRemainingAmt && _viewModel.ReportParam.NFROM_REMAINING_AMOUNT == 0 && _viewModel.ReportParam.NTO_REMAINING_AMOUNT == 0;
            // var FromDaysLateIsNull = _viewModel.CheckDaysLate && _viewModel.ReportParam.IFROM_DAYS_LATE == 0;

            if (propertyIsNull) loEx.Add("Error", _localizer["ERR_PROPERTY_EMPTY"]);
            if (deptIsNull) loEx.Add("Error", _localizer["ERR_DEPT_EMPTY"]);
            if (supplierIsNull) loEx.Add("Error", _localizer["ERR_SUPPL_EMPTY"]);
            if (cutOffDateIsNull) loEx.Add("Error", _localizer["ERR_CUTOFF_EMPTY"]);
            if (fromPeriodIsNull) loEx.Add("Error", _localizer["ERR_FROMPERIOD_EMPTY"]);
            if (toPeriodIsNull) loEx.Add("Error", _localizer["ERR_TOPERIOD_EMPTY"]);
            if (fromRefDateIsNull) loEx.Add("Error", _localizer["ERR_FROMREFDATE_EMPTY"]);
            if (toRefDateIsNull) loEx.Add("Error", _localizer["ERR_TOREFDATE_EMPTY"]);
            if (fromDueDateIsNull) loEx.Add("Error", _localizer["ERR_FROMDUEDATE_EMPTY"]);
            if (toDueDateIsNull) loEx.Add("Error", _localizer["ERR_TODUEDATE_EMPTY"]);
            if (fromRefNoIsNull) loEx.Add("Error", _localizer["ERR_FROMREFNO_EMPTY"]);
            if (toRefNoIsNull) loEx.Add("Error", _localizer["ERR_TOREFNO_EMPTY"]);
            if (currencyIsNull) loEx.Add("Error", _localizer["ERR_CURRENCY_EMPTY"]);

            var periodFromGreaterThanTo = int.Parse(_viewModel.ReportParam.CFROM_PERIOD) >
                                          int.Parse(_viewModel.ReportParam.CTO_PERIOD);
            var refDateFromGreaterThanTo = int.Parse(_viewModel.ReportParam.CFROM_REFERENCE_DATE) >
                                           int.Parse(_viewModel.ReportParam.CTO_REFERENCE_DATE);
            var dueDateFromGreaterThanTo = int.Parse(_viewModel.ReportParam.CFROM_DUE_DATE) >
                                           int.Parse(_viewModel.ReportParam.CTO_DUE_DATE);
            var totalAmountFromGreaterThanTo = _viewModel.ReportParam.NFROM_TOTAL_AMOUNT > _viewModel.ReportParam.NTO_TOTAL_AMOUNT;
            var remainingAmountFromGreaterThanTo = _viewModel.ReportParam.NFROM_REMAINING_AMOUNT > _viewModel.ReportParam.NTO_REMAINING_AMOUNT;
            var daysLateFromGreaterThanTo = _viewModel.ReportParam.IFROM_DAYS_LATE > _viewModel.ReportParam.ITO_DAYS_LATE;

            if (periodFromGreaterThanTo) loEx.Add("Error", _localizer["ERR_FROMPERIOD_GREATERTHAN_TOPERIOD"]);
            if (refDateFromGreaterThanTo) loEx.Add("Error", _localizer["ERR_FROMREFDATE_GREATERTHAN_TOREFDATE"]);
            if (dueDateFromGreaterThanTo) loEx.Add("Error", _localizer["ERR_FROMDUEDATE_GREATERTHAN_TODUEDATE"]);
            if (totalAmountFromGreaterThanTo) loEx.Add("Error", _localizer["ERR_FROMTOTALAMOUNT_GREATERTHAN_TOTOTALAMOUNT"]);
            if (remainingAmountFromGreaterThanTo) loEx.Add("Error", _localizer["ERR_FROMREMAININGAMOUNT_GREATERTHAN_TOREMAININGAMOUNT"]);
            if (daysLateFromGreaterThanTo) loEx.Add("Error", _localizer["ERR_FROMDAYSLATE_GREATERTHAN_TODAYSLATE"]);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        EndBlock:
        loEx.ThrowExceptionIfErrors();
    }

    private async Task OnClickPrint()
    {
        var loEx = new R_Exception();

        try
        {
            _validateDataBeforePrint();

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
                    "rpt/APR00500Print/ActivityReportPost",
                    "rpt/APR00500Print/ActivityReportGet",
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


    private void BeforeOpenPopupSaveAs(R_BeforeOpenLookupEventArgs eventArgs)
    {
        var loEx = new R_Exception();
        try
        {
            _validateDataBeforePrint();

            var loParam = _viewModel.ReportParam;
            eventArgs.Parameter = loParam;
            eventArgs.PageTitle = _localizer["SAVE_AS"];
            eventArgs.TargetPageType = typeof(APR00500PopupSaveAs);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        EndBlock:
        loEx.ThrowExceptionIfErrors();
    }
}