using BlazorClientHelper;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using Lookup_GSModel.ViewModel;
using Microsoft.AspNetCore.Components;
using PMR00460Common.DTOs;
using PMR00460Model.ViewModel;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_BlazorFrontEnd.Interfaces;

namespace PMR00460Front;

public partial class PMR00460 : R_Page
{
    private PMR00460ViewModel _viewModel = new();

    [Inject] private R_IReport _reportService { get; set; }
    [Inject] private IClientHelper _clientHelper { get; set; }
    public R_ComboBox<PMR00460PropertyDTO, string> _comboPropertyRef { get; set; }

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

    private void ValueChangedProperty(string value)
    {
        var loEx = new R_Exception();
        try
        {
            if (string.IsNullOrEmpty(value))
            {
                value = _viewModel.ReportParam.CPROPERTY_ID;
            }

            if (_viewModel.ReportParam.CPROPERTY_ID != value)
            {
                _viewModel.ReportParam.CPROPERTY_ID = value;
                _viewModel.ReportParam.CFROM_BUILDING_ID = "";
                _viewModel.ReportParam.CFROM_BUILDING_NAME = "";
                _viewModel.ReportParam.CTO_BUILDING_ID = "";
                _viewModel.ReportParam.CTO_BUILDING_NAME = "";
            }
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private void ValueChangedFromPeriodMonth(string value)
    {
        var loEx = new R_Exception();
        try
        {
            if (string.IsNullOrEmpty(value))
            {
                value = _viewModel.ReportParam.CFROM_PERIOD_MONTH;
            }

            _viewModel.ReportParam.CFROM_PERIOD_MONTH = value;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private void ValueChangedToPeriodMonth(string value)
    {
        var loEx = new R_Exception();
        try
        {
            if (string.IsNullOrEmpty(value))
            {
                value = _viewModel.ReportParam.CTO_PERIOD_MONTH;
            }

            _viewModel.ReportParam.CTO_PERIOD_MONTH = value;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }


    private async Task OnLostFocusFromBuilding(object eventArgs)
    {
        var loEx = new R_Exception();

        var loLookupViewModel = new LookupGSL02200ViewModel();
        try
        {
            if (string.IsNullOrEmpty(_viewModel.ReportParam.CFROM_BUILDING_ID))
            {
                _viewModel.ReportParam.CFROM_BUILDING_NAME = "";
                return;
            }

            var param = new GSL02200ParameterDTO
            {
                CPROPERTY_ID = _viewModel.ReportParam.CPROPERTY_ID,
                CSEARCH_TEXT = _viewModel.ReportParam.CFROM_BUILDING_ID
            };

            GSL02200DTO loResult = null;

            loResult = await loLookupViewModel.GetBuilding(param);

            if (loResult == null)
            {
                loEx.Add(R_FrontUtility.R_GetError(
                    typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                    "_ErrLookup01"));
                _viewModel.ReportParam.CFROM_BUILDING_ID = "";
                _viewModel.ReportParam.CFROM_BUILDING_NAME = "";
                goto EndBlock;
            }

            _viewModel.ReportParam.CFROM_BUILDING_ID = loResult.CBUILDING_ID;
            _viewModel.ReportParam.CFROM_BUILDING_NAME = loResult.CBUILDING_NAME;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        EndBlock:
        R_DisplayException(loEx);
    }

    private void BeforeLookupFromBuilding(R_BeforeOpenLookupEventArgs eventArgs)
    {
        eventArgs.TargetPageType = typeof(GSL02200);
        eventArgs.Parameter = new GSL02200ParameterDTO()
        {
            CPROPERTY_ID = _viewModel.ReportParam.CPROPERTY_ID
        };
    }

    private void AfterLookupFromBuilding(R_AfterOpenLookupEventArgs eventArgs)
    {
        var loEx = new R_Exception();
        try
        {
            var loTempResult = (GSL02200DTO)eventArgs.Result;
            if (loTempResult == null)
                return;

            _viewModel.ReportParam.CFROM_BUILDING_ID = loTempResult.CBUILDING_ID;
            _viewModel.ReportParam.CFROM_BUILDING_NAME = loTempResult.CBUILDING_NAME;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task OnLostFocusToBuilding(object eventArgs)
    {
        var loEx = new R_Exception();

        var loLookupViewModel = new LookupGSL02200ViewModel();
        try
        {
            if (string.IsNullOrEmpty(_viewModel.ReportParam.CTO_BUILDING_ID))
            {
                _viewModel.ReportParam.CTO_BUILDING_NAME = "";
                return;
            }

            var param = new GSL02200ParameterDTO
            {
                CPROPERTY_ID = _viewModel.ReportParam.CPROPERTY_ID,
                CSEARCH_TEXT = _viewModel.ReportParam.CTO_BUILDING_ID
            };

            GSL02200DTO loResult = null;

            loResult = await loLookupViewModel.GetBuilding(param);

            if (loResult == null)
            {
                loEx.Add(R_FrontUtility.R_GetError(
                    typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                    "_ErrLookup01"));
                _viewModel.ReportParam.CTO_BUILDING_ID = "";
                _viewModel.ReportParam.CTO_BUILDING_NAME = "";
                goto EndBlock;
            }

            _viewModel.ReportParam.CTO_BUILDING_ID = loResult.CBUILDING_ID;
            _viewModel.ReportParam.CTO_BUILDING_NAME = loResult.CBUILDING_NAME;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        EndBlock:
        R_DisplayException(loEx);
    }

    private void BeforeLookupToBuilding(R_BeforeOpenLookupEventArgs eventArgs)
    {
        eventArgs.TargetPageType = typeof(GSL02200);
        eventArgs.Parameter = new GSL02200ParameterDTO()
        {
            CPROPERTY_ID = _viewModel.ReportParam.CPROPERTY_ID
        };
    }

    private void AfterLookupToBuilding(R_AfterOpenLookupEventArgs eventArgs)
    {
        var loEx = new R_Exception();
        try
        {
            var loTempResult = (GSL02200DTO)eventArgs.Result;
            if (loTempResult == null)
                return;

            _viewModel.ReportParam.CTO_BUILDING_ID = loTempResult.CBUILDING_ID;
            _viewModel.ReportParam.CTO_BUILDING_NAME = loTempResult.CBUILDING_NAME;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

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
                    "R_DefaultServiceUrlPM",
                    "PM",
                    "rpt/PMR00460Print/ActivityReportPost",
                    "rpt/PMR00460Print/ActivityReportGet",
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
            _viewModel.ValidateDataBeforePrint();

            var loParam = _viewModel.ReportParam;
            eventArgs.Parameter = loParam;
            eventArgs.PageTitle = _localizer["SaveAs"];
            eventArgs.TargetPageType = typeof(PMR00460PopupSaveAs);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        EndBlock:
        loEx.ThrowExceptionIfErrors();
    }
}