using BlazorClientHelper;
using ICR00600Common;
using ICR00600Model.ViewModel;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using Lookup_GSModel.ViewModel;
using Lookup_ICCOMMON.DTOs.ICL00300;
using Lookup_ICFRONT;
using Lookup_ICModel.ViewModel.ICL00300;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_BlazorFrontEnd.Interfaces;

namespace ICR00600Front;

public partial class ICR00600 : R_Page
{
    
    private ICR00600ViewModel _viewModel = new();

    [Inject] private R_IReport _reportService { get; set; }
    [Inject] private IClientHelper _clientHelper { get; set; }

    protected override async Task R_Init_From_Master(object poParam)
    {
        var loEx = new R_Exception();

        try
        {
            await _viewModel.Init();
            await Task.CompletedTask;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }


    #region Lookup Dept

    private async Task OnLostFocusDept()
    {
        var loEx = new R_Exception();

        var loLookupViewModel = new LookupGSL00700ViewModel();
        try
        {
            if (_viewModel.ReportParam.CDEPT_CODE == null || _viewModel.ReportParam.CDEPT_CODE.Trim().Length <= 0)
            {
                _viewModel.ReportParam.CDEPT_NAME = "";
                return;
            }

            var param = new GSL00700ParameterDTO
            {
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
            var loParameter = new GSL00700ParameterDTO();

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

    #region Lookup Ref No

    private async Task OnLostFocusFromRefNo()
    {
        var loEx = new R_Exception();

        var loLookupViewModel = new LookupICL00300ViewModel();
        try
        {
            if (_viewModel.ReportParam.CFROM_REF_NO == null || _viewModel.ReportParam.CFROM_REF_NO.Trim().Length <= 0)
            {
                return;
            }

            var param = new ICL00300ParameterDTO
            {
                CPROPERTY_ID = _viewModel.ReportParam.CPROPERTY_ID!,
                CTRANS_CODE = ICR00600TransCode.CTRANS_CODE,
                CTRANS_NAME = _viewModel.TransCode.CTRANSACTION_NAME,
                CYEAR = _viewModel.ReportParam.IPERIOD_YEAR.ToString(),
                CPERIOD = _viewModel.ReportParam.CPERIOD_MONTH!,
                // CPERIOD = _viewModel.ReportParam.IPERIOD_YEAR + _viewModel.ReportParam.CPERIOD_MONTH,
                CDEPT_CODE = _viewModel.ReportParam.CDEPT_CODE!,
                CWAREHOUSE_ID = "",
                CALLOC_ID = "",
                CTRANS_STATUS = "30,80",
                CSEARCH_TEXT_ID = _viewModel.ReportParam.CFROM_REF_NO
            };

            var loResult = await loLookupViewModel.GetTransactionRecord(param);

            if (loResult == null)
            {
                loEx.Add(R_FrontUtility.R_GetError(
                    typeof(Lookup_ICFrontResources.Resources_Dummy_Class_LookupIC),
                    "_ErrLookup01"));
                _viewModel.ReportParam.CFROM_REF_NO = "";
                goto EndBlock;
            }

            _viewModel.ReportParam.CFROM_REF_NO = loResult.CREF_NO;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        EndBlock:
        await R_DisplayExceptionAsync(loEx);
    }


    private void BeforeOpenLookupFromRefNo(R_BeforeOpenLookupEventArgs eventArgs)
    {
        eventArgs.TargetPageType = typeof(ICL00300);
        eventArgs.Parameter = new ICL00300ParameterDTO
        {
            CPROPERTY_ID = _viewModel.ReportParam.CPROPERTY_ID!,
            CTRANS_CODE = ICR00600TransCode.CTRANS_CODE,
            CTRANS_NAME = _viewModel.TransCode.CTRANSACTION_NAME,
            CYEAR = _viewModel.ReportParam.IPERIOD_YEAR.ToString(),
            CPERIOD = _viewModel.ReportParam.CPERIOD_MONTH!,
            // CPERIOD = _viewModel.ReportParam.IPERIOD_YEAR + _viewModel.ReportParam.CPERIOD_MONTH,
            CDEPT_CODE = _viewModel.ReportParam.CDEPT_CODE!,
            CWAREHOUSE_ID = "",
            CALLOC_ID = "",
            CTRANS_STATUS = "30,80",
        };
    }

    private void AfterOpenLookupFromRefNo(R_AfterOpenLookupEventArgs eventArgs)
    {
        var loEx = new R_Exception();
        try
        {
            var loTempResult = (ICL00300DTO)eventArgs.Result;
            if (loTempResult == null)
                return;

            _viewModel.ReportParam.CFROM_REF_NO = loTempResult.CREF_NO;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task OnLostFocusToRefNo()
    {
        var loEx = new R_Exception();

        var loLookupViewModel = new LookupICL00300ViewModel();
        try
        {
            if (_viewModel.ReportParam.CTO_REF_NO == null || _viewModel.ReportParam.CTO_REF_NO.Trim().Length <= 0)
            {
                return;
            }

            var param = new ICL00300ParameterDTO
            {
                CPROPERTY_ID = _viewModel.ReportParam.CPROPERTY_ID!,
                CTRANS_CODE = ICR00600TransCode.CTRANS_CODE,
                CTRANS_NAME = _viewModel.TransCode.CTRANSACTION_NAME,
                CYEAR = _viewModel.ReportParam.IPERIOD_YEAR.ToString(),
                CPERIOD = _viewModel.ReportParam.CPERIOD_MONTH!,
                // CPERIOD = _viewModel.ReportParam.IPERIOD_YEAR + _viewModel.ReportParam.CPERIOD_MONTH,
                CDEPT_CODE = _viewModel.ReportParam.CDEPT_CODE!,
                CWAREHOUSE_ID = "",
                CALLOC_ID = "",
                CTRANS_STATUS = "30,80",
                CSEARCH_TEXT_ID = _viewModel.ReportParam.CTO_REF_NO
            };

            var loResult = await loLookupViewModel.GetTransactionRecord(param);

            if (loResult == null)
            {
                loEx.Add(R_FrontUtility.R_GetError(
                    typeof(Lookup_ICFrontResources.Resources_Dummy_Class_LookupIC),
                    "_ErrLookup01"));
                _viewModel.ReportParam.CTO_REF_NO = "";
                goto EndBlock;
            }

            _viewModel.ReportParam.CTO_REF_NO = loResult.CREF_NO;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        EndBlock:
        await R_DisplayExceptionAsync(loEx);
    }


    private void BeforeOpenLookupToRefNo(R_BeforeOpenLookupEventArgs eventArgs)
    {
        eventArgs.TargetPageType = typeof(ICL00300);
        eventArgs.Parameter = new ICL00300ParameterDTO
        {
            CPROPERTY_ID = _viewModel.ReportParam.CPROPERTY_ID!,
            CTRANS_CODE = ICR00600TransCode.CTRANS_CODE,
            CTRANS_NAME = _viewModel.TransCode.CTRANSACTION_NAME,
            CPERIOD = _viewModel.ReportParam.IPERIOD_YEAR + _viewModel.ReportParam.CPERIOD_MONTH,
            CDEPT_CODE = _viewModel.ReportParam.CDEPT_CODE!,
            CWAREHOUSE_ID = "",
            CALLOC_ID = "",
            CTRANS_STATUS = "30,80",
        };
    }

    private void AfterOpenLookupToRefNo(R_AfterOpenLookupEventArgs eventArgs)
    {
        var loEx = new R_Exception();
        try
        {
            var loTempResult = (ICL00300DTO)eventArgs.Result;
            if (loTempResult == null)
                return;

            _viewModel.ReportParam.CFROM_REF_NO = loTempResult.CREF_NO;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    #endregion


    private void BeforeOpenPopupSaveAs(R_BeforeOpenPopupEventArgs eventArgs)
    {
        var loEx = new R_Exception();
        try
        {
            _setParamBeforePrint();
            _validateDataBeforePrint();

            var loParam = _viewModel.ReportParam;
            eventArgs.Parameter = loParam;
            eventArgs.PageTitle = _localizer["SaveAs"];
            eventArgs.TargetPageType = typeof(ICR00600PopupSaveAs);
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
            _setParamBeforePrint();
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
                    "R_DefaultServiceUrlIC",
                    "IC",
                    "rpt/ICR00600Print/StockTakeActivityReportPost",
                    "rpt/ICR00600Print/StockTakeActivityReportGet",
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


    private void _validateDataBeforePrint()
    {
        var loEx = new R_Exception();

        try
        {
            var propertyIsNull = string.IsNullOrEmpty(_viewModel.ReportParam.CPROPERTY_ID);
            var deptIsNull = _viewModel.ReportParam.LDEPT && string.IsNullOrEmpty(_viewModel.ReportParam.CDEPT_CODE);

            if (propertyIsNull) loEx.Add("Error", _localizer["PleaseSelectProperty"]);
            if (deptIsNull) loEx.Add("Error", _localizer["PleaseSelectDepartment"]);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        EndBlock:
        loEx.ThrowExceptionIfErrors();
    }

    private void _setParamBeforePrint()
    {
        
        _viewModel.ReportParam.CPROPERTY_NAME = _viewModel.PropertyList
            .Find(x => x.CPROPERTY_ID == _viewModel.ReportParam.CPROPERTY_ID)?.CPROPERTY_NAME;
        _viewModel.ReportParam.COPTION_PRINT_NAME = _viewModel.OptionPrintType
            .Find(x => x.Key == _viewModel.ReportParam.COPTION_PRINT).Value;
        
        _viewModel.ReportParam.CPERIOD = _viewModel.ReportParam.IPERIOD_YEAR + _viewModel.ReportParam.CPERIOD_MONTH;
    }
}