using BlazorClientHelper;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using Lookup_GSModel.ViewModel;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using PMR02600Common.Params;
using PMR02600Model.ViewModel;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_BlazorFrontEnd.Interfaces;

namespace PMR02600Front;

public partial class PMR02600
{
    private PMR02600ViewModel _viewModel = new();

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

    #region Lookup From Building

    private async Task OnLostFocusFromBuilding()
    {
        var loEx = new R_Exception();

        var loLookupViewModel = new LookupGSL02200ViewModel();
        try
        {
            if (_viewModel.ReportParam.CFROM_BUILDING == null ||
                _viewModel.ReportParam.CFROM_BUILDING.Trim().Length <= 0)
            {
                _viewModel.ReportParam.CFROM_BUILDING_NAME = "";
                return;
            }

            var param = new GSL02200ParameterDTO
            {
                CPROPERTY_ID = _viewModel.ReportParam.CPROPERTY,
                CSEARCH_TEXT = _viewModel.ReportParam.CFROM_BUILDING
            };

            var loResult = await loLookupViewModel.GetBuilding(param);

            if (loResult == null)
            {
                loEx.Add(R_FrontUtility.R_GetError(
                    typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                    "_ErrLookup01"));
                _viewModel.ReportParam.CFROM_BUILDING = "";
                _viewModel.ReportParam.CFROM_BUILDING_NAME = "";
                goto EndBlock;
            }

            _viewModel.ReportParam.CFROM_BUILDING = loResult.CBUILDING_ID;
            _viewModel.ReportParam.CFROM_BUILDING_NAME = loResult.CBUILDING_NAME;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        EndBlock:
        await R_DisplayExceptionAsync(loEx);
    }

    private void BeforeOpenLookupFromBuilding(R_BeforeOpenLookupEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            var loParameter = new GSL02200ParameterDTO
            {
                CPROPERTY_ID = _viewModel.ReportParam.CPROPERTY
            };

            eventArgs.Parameter = loParameter;
            eventArgs.TargetPageType = typeof(GSL02200);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private void AfterOpenLookupFromBuilding(R_AfterOpenLookupEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            if (eventArgs.Result == null) return;

            var loTempResult = (GSL02200DTO)eventArgs.Result;
            _viewModel.ReportParam.CFROM_BUILDING = loTempResult.CBUILDING_ID;
            _viewModel.ReportParam.CFROM_BUILDING_NAME = loTempResult.CBUILDING_NAME;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    #endregion


    #region Lookup To Building

    private async Task OnLostFocusToBuilding()
    {
        var loEx = new R_Exception();

        var loLookupViewModel = new LookupGSL02200ViewModel();
        try
        {
            if (_viewModel.ReportParam.CTO_BUILDING == null || _viewModel.ReportParam.CTO_BUILDING.Trim().Length <= 0)
            {
                _viewModel.ReportParam.CTO_BUILDING_NAME = "";
                return;
            }

            var param = new GSL02200ParameterDTO
            {
                CPROPERTY_ID = _viewModel.ReportParam.CPROPERTY,
                CSEARCH_TEXT = _viewModel.ReportParam.CTO_BUILDING
            };

            var loResult = await loLookupViewModel.GetBuilding(param);

            if (loResult == null)
            {
                loEx.Add(R_FrontUtility.R_GetError(
                    typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                    "_ErrLookup01"));
                _viewModel.ReportParam.CTO_BUILDING = "";
                _viewModel.ReportParam.CTO_BUILDING_NAME = "";
                goto EndBlock;
            }

            _viewModel.ReportParam.CTO_BUILDING = loResult.CBUILDING_ID;
            _viewModel.ReportParam.CTO_BUILDING_NAME = loResult.CBUILDING_NAME;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        EndBlock:
        await R_DisplayExceptionAsync(loEx);
    }

    private void BeforeOpenLookupToBuilding(R_BeforeOpenLookupEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            var loParameter = new GSL02200ParameterDTO
            {
                CPROPERTY_ID = _viewModel.ReportParam.CPROPERTY
            };

            eventArgs.Parameter = loParameter;
            eventArgs.TargetPageType = typeof(GSL02200);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private void AfterOpenLookupToBuilding(R_AfterOpenLookupEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            if (eventArgs.Result == null) return;

            var loTempResult = (GSL02200DTO)eventArgs.Result;
            _viewModel.ReportParam.CTO_BUILDING = loTempResult.CBUILDING_ID;
            _viewModel.ReportParam.CTO_BUILDING_NAME = loTempResult.CBUILDING_NAME;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    #endregion

    private void _validateDataBeforePrint()
    {
        var loEx = new R_Exception();

        try
        {
            var PropertyIsNull = string.IsNullOrEmpty(_viewModel.ReportParam.CPROPERTY);
            var BuildingIsNull = string.IsNullOrEmpty(_viewModel.ReportParam.CFROM_BUILDING) ||
                                 string.IsNullOrEmpty(_viewModel.ReportParam.CTO_BUILDING);
            var PeriodIsNull = _viewModel.ReportParam.DPERIOD == null;

            if (PropertyIsNull)
            {
                loEx.Add("Error", _localizer["PLEASESELECTPROPERTY"]);
            }

            if (BuildingIsNull)
            {
                loEx.Add("Error", _localizer["PLEASESELECTBUILDING"]);
            }

            if (PeriodIsNull)
            {
                loEx.Add("Error", _localizer["PLEASESELECTCUTOFFDATE"]);
            }
            
            _viewModel.ReportParam.CPROPERTY_NAME = _viewModel.PropertyList
                ?.Find(x => x.CPROPERTY_ID == _viewModel.ReportParam.CPROPERTY)
                .CPROPERTY_NAME;
            _viewModel.ReportParam.CPERIOD = _viewModel.ReportParam.DPERIOD?.ToString("yyyyMMdd");
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
                    "R_DefaultServiceUrlPM",
                    "PM",
                    "rpt/PMR02600Print/ActivityReportPost",
                    "rpt/PMR02600Print/ActivityReportGet",
                    loParam
                );
            }
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

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
            eventArgs.TargetPageType = typeof(PMR02600PopupSaveAs);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        EndBlock:
        loEx.ThrowExceptionIfErrors();
    }
}