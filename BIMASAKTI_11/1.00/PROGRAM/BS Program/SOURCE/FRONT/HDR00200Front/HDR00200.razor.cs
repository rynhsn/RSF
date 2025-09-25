using BlazorClientHelper;
using HDR00200Common.DTOs;
using HDR00200Model.ViewModel;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using Lookup_GSModel.ViewModel;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_BlazorFrontEnd.Interfaces;

namespace HDR00200Front;

public partial class HDR00200 : R_Page
{
    private HDR00200ViewModel _viewModel = new();

    [Inject] private R_IReport _reportService { get; set; }
    [Inject] private IClientHelper _clientHelper { get; set; }

    public R_ComboBox<HDR00200PropertyDTO, string> _comboPropertyRef { get; set; }

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

    private async Task ValueChangedProperty(string value)
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
                await _viewModel.ResetProcess();
            }
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }


    private async Task OnLostFocusFromBuilding(object obj)
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

            if (_viewModel.ReportParam.CFROM_BUILDING_ID != loTempResult.CBUILDING_ID)
            {
                _viewModel.ReportParam.CFROM_BUILDING_ID = loTempResult.CBUILDING_ID;
                _viewModel.ReportParam.CFROM_BUILDING_NAME = loTempResult.CBUILDING_NAME;
            }
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task OnLostFocusToBuilding(object obj)
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

            if (_viewModel.ReportParam.CTO_BUILDING_ID != loTempResult.CBUILDING_ID)
            {
                _viewModel.ReportParam.CTO_BUILDING_ID = loTempResult.CBUILDING_ID;
                _viewModel.ReportParam.CTO_BUILDING_NAME = loTempResult.CBUILDING_NAME;
            }
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }


    private async Task OnLostFocusFromDept(object obj)
    {
        var loEx = new R_Exception();

        var loLookupViewModel = new LookupGSL00700ViewModel();
        try
        {
            if (string.IsNullOrEmpty(_viewModel.ReportParam.CFROM_DEPT_CODE))
            {
                _viewModel.ReportParam.CFROM_DEPT_NAME = "";
                return;
            }

            var param = new GSL00700ParameterDTO
            {
                CSEARCH_TEXT = _viewModel.ReportParam.CFROM_DEPT_CODE
            };

            GSL00700DTO loResult = null;

            loResult = await loLookupViewModel.GetDepartment(param);

            if (loResult == null)
            {
                loEx.Add(R_FrontUtility.R_GetError(
                    typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                    "_ErrLookup01"));
                _viewModel.ReportParam.CFROM_DEPT_CODE = "";
                _viewModel.ReportParam.CFROM_DEPT_NAME = "";
                goto EndBlock;
            }

            _viewModel.ReportParam.CFROM_DEPT_CODE = loResult.CDEPT_CODE;
            _viewModel.ReportParam.CFROM_DEPT_NAME = loResult.CDEPT_NAME;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

    EndBlock:
        R_DisplayException(loEx);
    }

    private void BeforeLookupFromDept(R_BeforeOpenLookupEventArgs eventArgs)
    {
        eventArgs.TargetPageType = typeof(GSL00700);
        eventArgs.Parameter = new GSL00700ParameterDTO();
    }

    private void AfterLookupFromDept(R_AfterOpenLookupEventArgs eventArgs)
    {
        var loEx = new R_Exception();
        try
        {
            var loTempResult = (GSL00700DTO)eventArgs.Result;
            if (loTempResult == null)
                return;

            if (_viewModel.ReportParam.CFROM_DEPT_CODE != loTempResult.CDEPT_CODE)
            {
                _viewModel.ReportParam.CFROM_DEPT_CODE = loTempResult.CDEPT_CODE;
                _viewModel.ReportParam.CFROM_DEPT_NAME = loTempResult.CDEPT_NAME;
            }
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task OnLostFocusToDept(object obj)
    {
        var loEx = new R_Exception();

        var loLookupViewModel = new LookupGSL00700ViewModel();
        try
        {
            if (string.IsNullOrEmpty(_viewModel.ReportParam.CTO_DEPT_CODE))
            {
                _viewModel.ReportParam.CTO_DEPT_NAME = "";
                return;
            }

            var param = new GSL00700ParameterDTO
            {
                CSEARCH_TEXT = _viewModel.ReportParam.CTO_DEPT_CODE
            };

            GSL00700DTO loResult = null;

            loResult = await loLookupViewModel.GetDepartment(param);

            if (loResult == null)
            {
                loEx.Add(R_FrontUtility.R_GetError(
                    typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                    "_ErrLookup01"));
                _viewModel.ReportParam.CTO_DEPT_CODE = "";
                _viewModel.ReportParam.CTO_DEPT_NAME = "";
                goto EndBlock;
            }

            _viewModel.ReportParam.CTO_DEPT_CODE = loResult.CDEPT_CODE;
            _viewModel.ReportParam.CTO_DEPT_NAME = loResult.CDEPT_NAME;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

    EndBlock:
        R_DisplayException(loEx);
    }

    private void BeforeLookupToDept(R_BeforeOpenLookupEventArgs eventArgs)
    {
        eventArgs.TargetPageType = typeof(GSL00700);
        eventArgs.Parameter = new GSL00700ParameterDTO();
    }

    private void AfterLookupToDept(R_AfterOpenLookupEventArgs eventArgs)
    {
        var loEx = new R_Exception();
        try
        {
            var loTempResult = (GSL00700DTO)eventArgs.Result;
            if (loTempResult == null)
                return;

            if (_viewModel.ReportParam.CTO_DEPT_CODE != loTempResult.CDEPT_CODE)
            {
                _viewModel.ReportParam.CTO_DEPT_CODE = loTempResult.CDEPT_CODE;
                _viewModel.ReportParam.CTO_DEPT_NAME = loTempResult.CDEPT_NAME;
            }
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
                if (_viewModel.ReportParam.CREPORT_TYPE == "M")
                {
                    await _reportService.GetReport(
                        "R_DefaultServiceUrlHD",
                        "HD",
                        "rpt/HDR00200PrintMaintenance/ActivityReportPost",
                        "rpt/HDR00200PrintMaintenance/ActivityReportGet",
                        loParam
                    );
                }
                else if (_viewModel.ReportParam.CREPORT_TYPE == "C")
                {
                    if (_viewModel.ReportParam.CAREA == "01")
                    {
                        await _reportService.GetReport(
                            "R_DefaultServiceUrlHD",
                            "HD",
                            "rpt/HDR00200PrintPrivate/ActivityReportPost",
                            "rpt/HDR00200PrintPrivate/ActivityReportGet",
                            loParam
                        );
                    }
                    else if (_viewModel.ReportParam.CAREA == "02")
                    {
                        await _reportService.GetReport(
                            "R_DefaultServiceUrlHD",
                            "HD",
                            "rpt/HDR00200PrintPublic/ActivityReportPost",
                            "rpt/HDR00200PrintPublic/ActivityReportGet",
                            loParam
                        );
                    }
                }
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

            if (!loEx.HasError)
            {
                var loParam = _viewModel.ReportParam;
                eventArgs.Parameter = loParam;
                eventArgs.PageTitle = _localizer["SaveAs"];
                eventArgs.TargetPageType = typeof(HDR00200PopupSaveAs);
            }

        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

    EndBlock:
        loEx.ThrowExceptionIfErrors();
    }

    private void OnChangedTypeCare(object obj)
    {
        var llObject = obj as bool? ?? false;

        if (!llObject)
        {

            _viewModel.ReportParam.CFROM_BUILDING_ID = "";
            _viewModel.ReportParam.CFROM_BUILDING_NAME = "";
            _viewModel.ReportParam.CTO_BUILDING_ID = "";
            _viewModel.ReportParam.CTO_BUILDING_NAME = "";

        }
        else if (llObject)
        {

            _viewModel.ReportParam.LCOMPLAINT = true;
            _viewModel.ReportParam.LREQUEST = true;
            _viewModel.ReportParam.LINQUIRY = true;
            _viewModel.ReportParam.LHANDOVER = false;

            if (_viewModel.ReportParam.CAREA == "01")
            {
                _viewModel.ReportParam.CFROM_BUILDING_ID = _viewModel.DefaultParam.CFIRST_BUILDING_ID;
                _viewModel.ReportParam.CFROM_BUILDING_NAME = _viewModel.DefaultParam.CFIRST_BUILDING_NAME;
                _viewModel.ReportParam.CTO_BUILDING_ID = _viewModel.DefaultParam.CLAST_BUILDING_ID;
                _viewModel.ReportParam.CTO_BUILDING_NAME = _viewModel.DefaultParam.CLAST_BUILDING_NAME;
            }
            else if (_viewModel.ReportParam.CAREA == "02")
            {
                _viewModel.ReportParam.CFROM_BUILDING_ID = "";
                _viewModel.ReportParam.CFROM_BUILDING_NAME = "";
                _viewModel.ReportParam.CTO_BUILDING_ID = "";
                _viewModel.ReportParam.CTO_BUILDING_NAME = "";
            }

            _viewModel.ReportParam.CFROM_DEPT_CODE = "";
            _viewModel.ReportParam.CFROM_DEPT_NAME = "";
            _viewModel.ReportParam.CTO_DEPT_CODE = "";
            _viewModel.ReportParam.CTO_DEPT_NAME = "";
        }
    }


    private void ValueChangedTypeCare(string value)
    {
        
        if (value == "C")
        {
            _viewModel.ReportParam.LCOMPLAINT = true;
            _viewModel.ReportParam.LREQUEST = true;
            _viewModel.ReportParam.LINQUIRY = true;
            _viewModel.ReportParam.LHANDOVER = false;

            if (_viewModel.ReportParam.CAREA == "01")
            {
                _viewModel.ReportParam.CFROM_BUILDING_ID = _viewModel.DefaultParam.CFIRST_BUILDING_ID;
                _viewModel.ReportParam.CFROM_BUILDING_NAME = _viewModel.DefaultParam.CFIRST_BUILDING_NAME;
                _viewModel.ReportParam.CTO_BUILDING_ID = _viewModel.DefaultParam.CLAST_BUILDING_ID;
                _viewModel.ReportParam.CTO_BUILDING_NAME = _viewModel.DefaultParam.CLAST_BUILDING_NAME;
            }
            else if (_viewModel.ReportParam.CAREA == "02")
            {
                _viewModel.ReportParam.CFROM_BUILDING_ID = "";
                _viewModel.ReportParam.CFROM_BUILDING_NAME = "";
                _viewModel.ReportParam.CTO_BUILDING_ID = "";
                _viewModel.ReportParam.CTO_BUILDING_NAME = "";
            }

            _viewModel.DefaultParam.CFIRST_DEPT_CODE= _viewModel.ReportParam.CFROM_DEPT_CODE;
            _viewModel.DefaultParam.CFIRST_DEPT_NAME = _viewModel.ReportParam.CFROM_DEPT_NAME;
            _viewModel.DefaultParam.CLAST_DEPT_CODE= _viewModel.ReportParam.CTO_DEPT_CODE;
            _viewModel.DefaultParam.CLAST_DEPT_NAME = _viewModel.ReportParam.CTO_DEPT_NAME;

            _viewModel.ReportParam.CFROM_DEPT_CODE = "";
            _viewModel.ReportParam.CFROM_DEPT_NAME = "";
            _viewModel.ReportParam.CTO_DEPT_CODE = "";
            _viewModel.ReportParam.CTO_DEPT_NAME = "";
        }
        else if (value == "M")
        {

            if (_viewModel.ReportParam.CAREA == "01")
            {
                _viewModel.DefaultParam.CFIRST_BUILDING_ID = _viewModel.ReportParam.CFROM_BUILDING_ID;
                _viewModel.DefaultParam.CFIRST_BUILDING_NAME = _viewModel.ReportParam.CFROM_BUILDING_NAME;
                _viewModel.DefaultParam.CLAST_BUILDING_ID = _viewModel.ReportParam.CTO_BUILDING_ID;
                _viewModel.DefaultParam.CLAST_BUILDING_NAME = _viewModel.ReportParam.CTO_BUILDING_NAME;

                _viewModel.ReportParam.CFROM_BUILDING_ID = "";
                _viewModel.ReportParam.CFROM_BUILDING_NAME = "";
                _viewModel.ReportParam.CTO_BUILDING_ID = "";
                _viewModel.ReportParam.CTO_BUILDING_NAME = "";
            }

            _viewModel.ReportParam.CFROM_DEPT_CODE = _viewModel.DefaultParam.CFIRST_DEPT_CODE;
            _viewModel.ReportParam.CFROM_DEPT_NAME = _viewModel.DefaultParam.CFIRST_DEPT_NAME;
            _viewModel.ReportParam.CTO_DEPT_CODE = _viewModel.DefaultParam.CLAST_DEPT_CODE;
            _viewModel.ReportParam.CTO_DEPT_NAME = _viewModel.DefaultParam.CLAST_DEPT_NAME;

        }

        _viewModel.ReportParam.CREPORT_TYPE = value;
    }

    private void ValueChangedArea(string value)
    {
        if(value == "01")
        {
            _viewModel.ReportParam.CFROM_BUILDING_ID = _viewModel.DefaultParam.CFIRST_BUILDING_ID;
            _viewModel.ReportParam.CFROM_BUILDING_NAME = _viewModel.DefaultParam.CFIRST_BUILDING_NAME;
            _viewModel.ReportParam.CTO_BUILDING_ID = _viewModel.DefaultParam.CLAST_BUILDING_ID;
            _viewModel.ReportParam.CTO_BUILDING_NAME = _viewModel.DefaultParam.CLAST_BUILDING_NAME;
        }
        else if(value == "02")
        {
            _viewModel.DefaultParam.CFIRST_BUILDING_ID = _viewModel.ReportParam.CFROM_BUILDING_ID;
            _viewModel.DefaultParam.CFIRST_BUILDING_NAME = _viewModel.ReportParam.CFROM_BUILDING_NAME;
            _viewModel.DefaultParam.CLAST_BUILDING_ID = _viewModel.ReportParam.CTO_BUILDING_ID;
            _viewModel.DefaultParam.CLAST_BUILDING_NAME = _viewModel.ReportParam.CTO_BUILDING_NAME;
            _viewModel.ReportParam.CFROM_BUILDING_ID = "";
            _viewModel.ReportParam.CFROM_BUILDING_NAME = "";
            _viewModel.ReportParam.CTO_BUILDING_ID = "";
            _viewModel.ReportParam.CTO_BUILDING_NAME = "";
        }

        _viewModel.ReportParam.CAREA = value;
    }
}