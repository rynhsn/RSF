using BlazorClientHelper;
using ICR00100Model.ViewModel;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using Lookup_GSModel.ViewModel;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_BlazorFrontEnd.Interfaces;

namespace ICR00100Front;

public partial class ICR00100 : R_Page
{
    private ICR00100ViewModel _viewModel = new();

    [Inject] private R_IReport _reportService { get; set; }
    [Inject] private IClientHelper _clientHelper { get; set; }

    protected override async Task R_Init_From_Master(object poParam)
    {
        var loEx = new R_Exception();

        try
        {
            await _viewModel.Init();
            await _setDefaultWarehouse();
            await _setDefaultProduct();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }


    private async Task BeforeOpenPopupSaveAs(R_BeforeOpenLookupEventArgs eventArgs)
    {
        var loEx = new R_Exception();
        try
        {
            var isValid = ValidateBeforePrint();
            if (!isValid)
            {
                await R_DisplayExceptionAsync(loEx);
                return;
            }
            SetParamBeforePrint();

            var loParam = _viewModel.ReportParam;
            eventArgs.Parameter = loParam;
            eventArgs.PageTitle = _localizer["SaveAs"];
            eventArgs.TargetPageType = typeof(ICR00100PopupSaveAs);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

    EndBlock:
        loEx.ThrowExceptionIfErrors();
    }

    private async Task OnClickPrint(MouseEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            var isValid = ValidateBeforePrint();

            if (!isValid)
            {
                await R_DisplayExceptionAsync(loEx);
                return;
            }

            SetParamBeforePrint();

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
                    "R_DefaultServiceUrlIC",
                    "IC",
                    "rpt/ICR00100Print/ActivityReportPost",
                    "rpt/ICR00100Print/ActivityReportGet",
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

    private void SetParamBeforePrint()
    {
        _viewModel.ReportParam.CPERIOD = _viewModel.ReportParam.CDATE_FILTER == _localizer["PERIOD"]
            ? _viewModel.ReportParam.IPERIOD_YEAR + _viewModel.ReportParam.CPERIOD_MONTH
            : "";
        _viewModel.ReportParam.CFROM_DATE = _viewModel.ReportParam.CDATE_FILTER == _localizer["DATE"]
            ? _viewModel.ReportParam.DFROM_DATE?.ToString("yyyyMMdd")
            : "";
        _viewModel.ReportParam.CTO_DATE = _viewModel.ReportParam.CDATE_FILTER == _localizer["DATE"]
            ? _viewModel.ReportParam.DTO_DATE?.ToString("yyyyMMdd")
            : "";
        // _viewModel.ReportParam.CDEPT_CODE = _viewModel.ReportParam.LDEPARTMENT
        //     ? _viewModel.ReportParam.CDEPT_CODE
        //     : "";
        // _viewModel.ReportParam.CDEPT_NAME = _viewModel.ReportParam.LDEPARTMENT
        //     ? _viewModel.ReportParam.CDEPT_NAME
        //     : "";
        // _viewModel.ReportParam.CWAREHOUSE_CODE = _viewModel.ReportParam.LINC_FUTURE_TRANSACTION
        //     ? _viewModel.ReportParam.CWAREHOUSE_CODE
        //     : "";
        // _viewModel.ReportParam.CWAREHOUSE_NAME = _viewModel.ReportParam.LINC_FUTURE_TRANSACTION
        //     ? _viewModel.ReportParam.CWAREHOUSE_NAME
        //     : "";

        _viewModel.ReportParam.CPROPERTY_NAME = _viewModel.PropertyList
            .FirstOrDefault(x => x.CPROPERTY_ID == _viewModel.ReportParam.CPROPERTY_ID)?.CPROPERTY_NAME ?? "";

        _viewModel.ReportParam.CFILTER_DATA = _viewModel.ReportParam.CFILTER_BY == _localizer["CATEGORY"]
            ? _viewModel.ReportParam.CFILTER_DATA_CATEGORY
            : _viewModel.ReportParam.CFILTER_BY == _localizer["JOURNAL"]
                ? _viewModel.ReportParam.CFILTER_DATA_JOURNAL
                : "";
        _viewModel.ReportParam.CFILTER_DATA_NAME = _viewModel.ReportParam.CFILTER_BY == _localizer["CATEGORY"]
            ? _viewModel.ReportParam.CFILTER_DATA_CATEGORY_NAME
            : _viewModel.ReportParam.CFILTER_BY == _localizer["JOURNAL"]
                ? _viewModel.ReportParam.CFILTER_DATA_JOURNAL_NAME
                : "";

        _viewModel.ReportParam.CDATE_FILTER_DESC = _viewModel.ReportParam.CDATE_FILTER == _localizer["PERIOD"]
            ? _localizer["Period"]
            : _viewModel.ReportParam.CDATE_FILTER == _localizer["DATE"]
                ? _localizer["Date"]
                : "";

        _viewModel.ReportParam.CFILTER_BY_DESC = _viewModel.ReportParam.CFILTER_BY == _localizer["PROD"]
            ? _localizer["Product"]
            : _viewModel.ReportParam.CFILTER_BY == _localizer["CATEGORY"]
                ? _localizer["Category"]
                : _viewModel.ReportParam.CFILTER_BY == _localizer["JOURNAL"]
                    ? _localizer["JournalGroup"]
                    : "";

        _viewModel.ReportParam.COPTION_PRINT_DESC = _viewModel.ReportParam.COPTION_PRINT == _localizer["QTY"]
            ? _localizer["ByQtyUnit"]
            : _viewModel.ReportParam.COPTION_PRINT == _localizer["UNIT1"]
                ? _localizer["ByUnit1"]
                : "";
        // _viewModel.ReportParam.CBASE_ON_DESC = value dari list FilterBy
        // _viewModel.ReportParam.CBASE_ON_DESC = _viewModel.FilterBy
        //     .FirstOrDefault(x => x.Key == _viewModel.ReportParam.CFILTER_BY).Value;
    }


    #region Lookup Dept

    private async Task OnLostFocusDept()
    {
        var loEx = new R_Exception();

        var loLookupViewModel = new LookupGSL00700ViewModel();
        try
        {
            if (string.IsNullOrEmpty(_viewModel.ReportParam.CDEPT_CODE))
            {
                _viewModel.ReportParam.CDEPT_NAME = "";
                return;
            }

            var param = new GSL00700ParameterDTO
            {
                CPROGRAM_ID = "ICR00100",
                CSEARCH_TEXT = _viewModel.ReportParam.CDEPT_CODE
            };

            var loResult = await loLookupViewModel.GetDepartment(param);

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
        await R_DisplayExceptionAsync(loEx);
    }

    private void BeforeOpenLookupDept(R_BeforeOpenLookupEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            var loParameter = new GSL00700ParameterDTO()
            {
                CPROGRAM_ID = "ICR00100"
            };

            eventArgs.Parameter = loParameter;
            eventArgs.TargetPageType = typeof(GSL00700);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private void AfterOpenLookupDept(R_AfterOpenLookupEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            if (eventArgs.Result == null) return;

            var loTempResult = (GSL00700DTO)eventArgs.Result;
            _viewModel.ReportParam.CDEPT_CODE = loTempResult.CDEPT_CODE;
            _viewModel.ReportParam.CDEPT_NAME = loTempResult.CDEPT_NAME;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    #endregion


    #region Lookup Warehouse

    private async Task OnLostFocusWarehouse()
    {
        var loEx = new R_Exception();

        var loLookupViewModel = new LookupGSL03500ViewModel();
        try
        {
            if (string.IsNullOrEmpty(_viewModel.ReportParam.CWAREHOUSE_CODE))
            {
                _viewModel.ReportParam.CWAREHOUSE_NAME = "";
                return;
            }

            var param = new GSL03500ParameterDTO
            {
                CSEARCH_TEXT = _viewModel.ReportParam.CWAREHOUSE_CODE
            };

            var loResult = await loLookupViewModel.GetWarehouse(param);

            if (loResult == null)
            {
                loEx.Add(R_FrontUtility.R_GetError(
                    typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                    "_ErrLookup01"));
                _viewModel.ReportParam.CWAREHOUSE_CODE = "";
                _viewModel.ReportParam.CWAREHOUSE_NAME = "";
                goto EndBlock;
            }

            _viewModel.ReportParam.CWAREHOUSE_CODE = loResult.CWAREHOUSE_ID;
            _viewModel.ReportParam.CWAREHOUSE_NAME = loResult.CWAREHOUSE_NAME;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

    EndBlock:
        await R_DisplayExceptionAsync(loEx);
    }

    private void BeforeOpenLookupWarehouse(R_BeforeOpenLookupEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            var loParameter = new GSL03500ParameterDTO();

            eventArgs.Parameter = loParameter;
            eventArgs.TargetPageType = typeof(GSL03500);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private void AfterOpenLookupWarehouse(R_AfterOpenLookupEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            if (eventArgs.Result == null) return;

            var loTempResult = (GSL03500DTO)eventArgs.Result;
            _viewModel.ReportParam.CWAREHOUSE_CODE = loTempResult.CWAREHOUSE_ID;
            _viewModel.ReportParam.CWAREHOUSE_NAME = loTempResult.CWAREHOUSE_NAME;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    #endregion

    #region Lookup Product

    private async Task OnLostFocusFromProduct()
    {
        var loEx = new R_Exception();

        var loLookupViewModel = new LookupGSL03000ViewModel();
        try
        {
            if (string.IsNullOrEmpty(_viewModel.ReportParam.CFROM_PROD_ID))
            {
                _viewModel.ReportParam.CFROM_PROD_NAME = "";
                return;
            }

            var param = new GSL03000ParameterDTO
            {
                CSEARCH_TEXT = _viewModel.ReportParam.CFROM_PROD_ID
            };

            var loResult = await loLookupViewModel.GetProduct(param);

            if (loResult == null)
            {
                loEx.Add(R_FrontUtility.R_GetError(
                    typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                    "_ErrLookup01"));
                _viewModel.ReportParam.CFROM_PROD_ID = "";
                _viewModel.ReportParam.CFROM_PROD_NAME = "";
                goto EndBlock;
            }

            _viewModel.ReportParam.CFROM_PROD_ID = loResult.CPRODUCT_ID;
            _viewModel.ReportParam.CFROM_PROD_NAME = loResult.CPRODUCT_NAME;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

    EndBlock:
        await R_DisplayExceptionAsync(loEx);
    }

    private void BeforeOpenLookupFromProduct(R_BeforeOpenLookupEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            var loParameter = new GSL03000ParameterDTO();

            eventArgs.Parameter = loParameter;
            eventArgs.TargetPageType = typeof(GSL03000);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private void AfterOpenLookupFromProduct(R_AfterOpenLookupEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            if (eventArgs.Result == null) return;

            var loResult = (GSL03000DTO)eventArgs.Result;
            _viewModel.ReportParam.CFROM_PROD_ID = loResult.CPRODUCT_ID;
            _viewModel.ReportParam.CFROM_PROD_NAME = loResult.CPRODUCT_NAME;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task OnLostFocusToProduct()
    {
        var loEx = new R_Exception();

        var loLookupViewModel = new LookupGSL03000ViewModel();
        try
        {
            if (string.IsNullOrEmpty(_viewModel.ReportParam.CTO_PROD_ID))
            {
                _viewModel.ReportParam.CTO_PROD_NAME = "";
                return;
            }

            var param = new GSL03000ParameterDTO
            {
                CSEARCH_TEXT = _viewModel.ReportParam.CTO_PROD_ID
            };

            var loResult = await loLookupViewModel.GetProduct(param);

            if (loResult == null)
            {
                loEx.Add(R_FrontUtility.R_GetError(
                    typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                    "_ErrLookup01"));
                _viewModel.ReportParam.CTO_PROD_ID = "";
                _viewModel.ReportParam.CTO_PROD_NAME = "";
                goto EndBlock;
            }

            _viewModel.ReportParam.CTO_PROD_ID = loResult.CPRODUCT_ID;
            _viewModel.ReportParam.CTO_PROD_NAME = loResult.CPRODUCT_NAME;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

    EndBlock:
        await R_DisplayExceptionAsync(loEx);
    }

    private void BeforeOpenLookupToProduct(R_BeforeOpenLookupEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            var loParameter = new GSL03000ParameterDTO();

            eventArgs.Parameter = loParameter;
            eventArgs.TargetPageType = typeof(GSL03000);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private void AfterOpenLookupToProduct(R_AfterOpenLookupEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            if (eventArgs.Result == null) return;

            var loResult = (GSL03000DTO)eventArgs.Result;
            _viewModel.ReportParam.CTO_PROD_ID = loResult.CPRODUCT_ID;
            _viewModel.ReportParam.CTO_PROD_NAME = loResult.CPRODUCT_NAME;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    #endregion

    #region Lookup Category Product

    private async Task OnLostFocusCategoryProduct()
    {
        var loEx = new R_Exception();

        var loLookupViewModel = new LookupGSL00600ViewModel();
        try
        {
            if (string.IsNullOrEmpty(_viewModel.ReportParam.CFILTER_DATA_CATEGORY))
            {
                _viewModel.ReportParam.CFILTER_DATA_CATEGORY_NAME = "";
                return;
            }

            var param = new GSL00600ParameterDTO
            {
                CPROPERTY_ID = _viewModel.ReportParam.CPROPERTY_ID,
                CSEARCH_TEXT = _viewModel.ReportParam.CFILTER_DATA_CATEGORY
            };

            var loResult = await loLookupViewModel.GetUnitTypeCategory(param);

            if (loResult == null)
            {
                loEx.Add(R_FrontUtility.R_GetError(
                    typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                    "_ErrLookup01"));
                _viewModel.ReportParam.CFILTER_DATA_CATEGORY = "";
                _viewModel.ReportParam.CFILTER_DATA_CATEGORY_NAME = "";
                goto EndBlock;
            }

            _viewModel.ReportParam.CFILTER_DATA_CATEGORY = loResult.CUNIT_TYPE_CATEGORY_ID;
            _viewModel.ReportParam.CFILTER_DATA_CATEGORY_NAME = loResult.CUNIT_TYPE_CATEGORY_NAME;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

    EndBlock:
        await R_DisplayExceptionAsync(loEx);
    }

    private void BeforeOpenLookupCategoryProduct(R_BeforeOpenLookupEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            var loParameter = new GSL00600ParameterDTO()
            {
                CPROPERTY_ID = _viewModel.ReportParam.CPROPERTY_ID,
            };

            eventArgs.Parameter = loParameter;
            eventArgs.TargetPageType = typeof(GSL00600);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private void AfterOpenLookupCategoryProduct(R_AfterOpenLookupEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            if (eventArgs.Result == null) return;

            var loResult = (GSL00600DTO)eventArgs.Result;
            _viewModel.ReportParam.CFILTER_DATA_CATEGORY = loResult.CUNIT_TYPE_CATEGORY_ID;
            _viewModel.ReportParam.CFILTER_DATA_CATEGORY_NAME = loResult.CUNIT_TYPE_CATEGORY_NAME;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    #endregion

    #region Lookup Journal Group

    private async Task OnLostFocusJournalGroup()
    {
        var loEx = new R_Exception();

        var loLookupViewModel = new LookupGSL00400ViewModel();
        try
        {
            if (string.IsNullOrEmpty(_viewModel.ReportParam.CFILTER_DATA_JOURNAL))
            {
                _viewModel.ReportParam.CFILTER_DATA_JOURNAL_NAME = "";
                return;
            }

            var param = new GSL00400ParameterDTO
            {
                CPROPERTY_ID = _viewModel.ReportParam.CPROPERTY_ID,
                CSEARCH_TEXT = _viewModel.ReportParam.CFILTER_DATA_JOURNAL
            };

            var loResult = await loLookupViewModel.GetJournalGroup(param);

            if (loResult == null)
            {
                loEx.Add(R_FrontUtility.R_GetError(
                    typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                    "_ErrLookup01"));
                _viewModel.ReportParam.CFILTER_DATA_JOURNAL = "";
                _viewModel.ReportParam.CFILTER_DATA_JOURNAL_NAME = "";
                goto EndBlock;
            }

            _viewModel.ReportParam.CFILTER_DATA_JOURNAL = loResult.CJRNGRP_CODE;
            _viewModel.ReportParam.CFILTER_DATA_JOURNAL_NAME = loResult.CJRNGRP_NAME;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

    EndBlock:
        await R_DisplayExceptionAsync(loEx);
    }

    private void BeforeOpenLookupJournalGroup(R_BeforeOpenLookupEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            var loParameter = new GSL00400ParameterDTO()
            {
                CPROPERTY_ID = _viewModel.ReportParam.CPROPERTY_ID,
            };

            eventArgs.Parameter = loParameter;
            eventArgs.TargetPageType = typeof(GSL00400);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private void AfterOpenLookupJournalGroup(R_AfterOpenLookupEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            if (eventArgs.Result == null) return;

            var loResult = (GSL00400DTO)eventArgs.Result;
            _viewModel.ReportParam.CFILTER_DATA_JOURNAL = loResult.CJRNGRP_CODE;
            _viewModel.ReportParam.CFILTER_DATA_JOURNAL_NAME = loResult.CJRNGRP_NAME;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    #endregion

    private async Task ValueChangedProperty(string value)
    {
        var loEx = new R_Exception();

        try
        {
            _viewModel.ReportParam.CPROPERTY_ID = value;

            _viewModel.ResetParam();
            await _setDefaultWarehouse();
            await _setDefaultProduct();

        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task ValueChangedFilter(string value)
    {
        var loEx = new R_Exception();

        try
        {
            if (value == _viewModel.ReportParam.CFILTER_BY) return;
            _viewModel.ReportParam.CFROM_PROD_ID = "";
            _viewModel.ReportParam.CFROM_PROD_NAME = "";
            _viewModel.ReportParam.CTO_PROD_ID = "";
            _viewModel.ReportParam.CTO_PROD_NAME = "";
            _viewModel.ReportParam.CFILTER_DATA = "";
            _viewModel.ReportParam.CFILTER_DATA_NAME = "";
            _viewModel.ReportParam.CFILTER_DATA_CATEGORY = "";
            _viewModel.ReportParam.CFILTER_DATA_CATEGORY_NAME = "";
            _viewModel.ReportParam.CFILTER_DATA_JOURNAL = "";
            _viewModel.ReportParam.CFILTER_DATA_JOURNAL_NAME = "";

            _viewModel.ReportParam.CFILTER_BY = value;
            if (value == _localizer["PROD"])
            {
                _viewModel.ReportParam.CFILTER_BY_DESC = _viewModel.FilterByProduct.FirstOrDefault().Value;
                await _setDefaultProduct();
            }
            else if (value == _localizer["CATEGORY"])
            {
                _viewModel.ReportParam.CFILTER_BY_DESC = _viewModel.FilterByCategory.FirstOrDefault().Value;
                await _setDefaultCategory();
            }
            else if (value == _localizer["JOURNAL"])
            {
                _viewModel.ReportParam.CFILTER_BY_DESC = _viewModel.FilterByJournalGroup.FirstOrDefault().Value;
                await _setDefaultJournal();
            }

        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private void ValueChangedDateMode(string value)
    {
        if (_viewModel.ReportParam.CDATE_FILTER == value) return;
        if (value == _localizer["PERIOD"])
        {
            _viewModel.ReportParam.DFROM_DATE = null;
            _viewModel.ReportParam.DTO_DATE = null;
        }
        else if (value == _localizer["DATE"])
        {
            // _viewModel.ReportParam.IPERIOD_YEAR = DateTime.Now.Year;
            // _viewModel.ReportParam.CPERIOD_MONTH = DateTime.Now.ToString("MM");
            _viewModel.ReportParam.DFROM_DATE = DateTime.Now;
            _viewModel.ReportParam.DTO_DATE = DateTime.Now;
        }

        _viewModel.ReportParam.CDATE_FILTER = value;
    }

    private bool ValidateBeforePrint()
    {
        var loEx = new R_Exception();
        var isValid = false;
        try
        {
            if (string.IsNullOrEmpty(_viewModel.ReportParam.CPROPERTY_ID))
            {
                loEx.Add("Error", _localizer["PropertyCannotBeEmpty"]);
            }

            // if(_viewModel.ReportParam.LINC_FUTURE_TRANSACTION && string.IsNullOrEmpty(_viewModel.ReportParam.CWAREHOUSE_CODE))
            if (string.IsNullOrEmpty(_viewModel.ReportParam.CWAREHOUSE_CODE))
            {
                loEx.Add("Error", _localizer["WarehouseCannotBeEmpty"]);
            }

            // if(_viewModel.ReportParam.LDEPARTMENT && string.IsNullOrEmpty(_viewModel.ReportParam.CDEPT_CODE))
            // {
            //     loEx.Add("Error", _localizer["DepartmentCannotBeEmpty"]);
            // }

            if (_viewModel.ReportParam.CFILTER_BY == _localizer["PROD"] && string.IsNullOrEmpty(_viewModel.ReportParam.CFROM_PROD_ID))
            {
                loEx.Add("Error", _localizer["FromProductCannotBeEmpty"]);
            }

            if (_viewModel.ReportParam.CFILTER_BY == _localizer["PROD"] && string.IsNullOrEmpty(_viewModel.ReportParam.CTO_PROD_ID))
            {
                loEx.Add("Error", _localizer["ToProductCannotBeEmpty"]);
            }

            if (_viewModel.ReportParam.CFILTER_BY == _localizer["CATEGORY"] && string.IsNullOrEmpty(_viewModel.ReportParam.CFILTER_DATA_CATEGORY))
            {
                loEx.Add("Error", _localizer["CategoryCannotBeEmpty"]);
            }

            if (_viewModel.ReportParam.CFILTER_BY == _localizer["JOURNAL"] && string.IsNullOrEmpty(_viewModel.ReportParam.CFILTER_DATA_JOURNAL))
            {
                loEx.Add("Error", _localizer["JournalGroupCannotBeEmpty"]);
            }

            isValid = true;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
        return isValid;
    }


    private async Task _setDefaultWarehouse()
    {
        var loEx = new R_Exception();

        try
        {
            var loLookupViewModel = new LookupGSL03500ViewModel();
            var loParameter = new GSL03500ParameterDTO();

            await loLookupViewModel.GetWarehouseList();
            if (loLookupViewModel.WarehouseGrid.Count > 0)
            {
                _viewModel.ReportParam.CWAREHOUSE_CODE =
                    loLookupViewModel.WarehouseGrid.FirstOrDefault()?.CWAREHOUSE_ID;
                _viewModel.ReportParam.CWAREHOUSE_NAME = loLookupViewModel.WarehouseGrid
                    .Where(x => x.CWAREHOUSE_ID == _viewModel.ReportParam.CWAREHOUSE_CODE)
                    .Select(x => x.CWAREHOUSE_NAME).FirstOrDefault() ?? string.Empty;

            }
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task _setDefaultProduct()
    {
        var loEx = new R_Exception();

        try
        {
            var loLookupViewModel = new LookupGSL03000ViewModel();
            var loParameter = new GSL03000ParameterDTO();

            await loLookupViewModel.GetProductList();
            if (loLookupViewModel.ProductGrid.Count > 0)
            {
                _viewModel.ReportParam.CFROM_PROD_ID =
                    loLookupViewModel.ProductGrid.FirstOrDefault()?.CPRODUCT_ID;
                _viewModel.ReportParam.CFROM_PROD_NAME = loLookupViewModel.ProductGrid
                    .Where(x => x.CPRODUCT_ID == _viewModel.ReportParam.CFROM_PROD_ID)
                    .Select(x => x.CPRODUCT_NAME).FirstOrDefault() ?? string.Empty;

                _viewModel.ReportParam.CTO_PROD_ID =
                    loLookupViewModel.ProductGrid.LastOrDefault()?.CPRODUCT_ID;
                _viewModel.ReportParam.CTO_PROD_NAME = loLookupViewModel.ProductGrid
                    .Where(x => x.CPRODUCT_ID == _viewModel.ReportParam.CTO_PROD_ID)
                    .Select(x => x.CPRODUCT_NAME).FirstOrDefault() ?? string.Empty;

            }
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }


    private async Task _setDefaultCategory()
    {
        var loEx = new R_Exception();

        try
        {
            var loLookupViewModel = new LookupGSL00600ViewModel();
            var loParameter = new GSL00600ParameterDTO()
            {
                CPROPERTY_ID = _viewModel.ReportParam.CPROPERTY_ID
            };

            await loLookupViewModel.GetUnitTypeCategoryList(loParameter);
            if (loLookupViewModel.UnitTypeCategoryGrid.Count > 0)
            {
                _viewModel.ReportParam.CFILTER_DATA_CATEGORY =
                    loLookupViewModel.UnitTypeCategoryGrid.FirstOrDefault()?.CUNIT_TYPE_CATEGORY_ID;
                _viewModel.ReportParam.CWAREHOUSE_NAME = loLookupViewModel.UnitTypeCategoryGrid
                    .Where(x => x.CUNIT_TYPE_CATEGORY_ID == _viewModel.ReportParam.CFILTER_DATA_CATEGORY)
                    .Select(x => x.CUNIT_TYPE_CATEGORY_NAME).FirstOrDefault() ?? string.Empty;

            }
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task _setDefaultJournal()
    {
        var loEx = new R_Exception();

        try
        {
            var loLookupViewModel = new LookupGSL00400ViewModel();
            var loParameter = new GSL00400ParameterDTO()
            {
                CPROPERTY_ID = _viewModel.ReportParam.CPROPERTY_ID
            };

            await loLookupViewModel.GetJournalGroupList(loParameter);
            if (loLookupViewModel.JournalGroupGrid.Count > 0)
            {
                _viewModel.ReportParam.CFILTER_DATA_JOURNAL =
                    loLookupViewModel.JournalGroupGrid.FirstOrDefault()?.CJRNGRP_CODE;
                _viewModel.ReportParam.CFILTER_DATA_JOURNAL_NAME = loLookupViewModel.JournalGroupGrid
                    .Where(x => x.CJRNGRP_CODE == _viewModel.ReportParam.CFILTER_DATA_JOURNAL)
                    .Select(x => x.CJRNGRP_NAME).FirstOrDefault() ?? string.Empty;

            }
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }
}