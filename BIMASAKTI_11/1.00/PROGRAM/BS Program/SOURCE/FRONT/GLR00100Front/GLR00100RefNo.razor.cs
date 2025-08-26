using BlazorClientHelper;
using GLR00100Common.DTOs;
using GLR00100Model.ViewModel;
using Lookup_GLCOMMON.DTOs.GLL00110;
using Lookup_GLFRONT;
using Lookup_GLModel.ViewModel.GLL00110;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using Lookup_GSModel.ViewModel;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_BlazorFrontEnd.Interfaces;

namespace GLR00100Front;

public partial class GLR00100RefNo
{
    private GLR00100ViewModel _viewModel = new GLR00100ViewModel();
    private R_ComboBox<GLR00100TransCodeDTO, string> ComboTransCode { get; set; }
    private R_TextBox TextFromDept { get; set; }
    private R_TextBox TextToDept { get; set; }

    private R_TextBox TextFromRef { get; set; }
    private R_TextBox TextToRef { get; set; }

    [Inject] private R_IReport _reportService { get; set; }
    [Inject] private IClientHelper _clientHelper { get; set; }

    protected override async Task R_Init_From_Master(object eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            await _viewModel.Init();
            await ComboTransCode.FocusAsync();
            await _viewModel.GetTransCodeList();
            await _setDefaultDept();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }


    private async Task OnLostFocusLookupFromDept()
    {
        var loEx = new R_Exception();

        var loLookupViewModel = new LookupGSL00700ViewModel();
        try
        {
            if (_viewModel.ReportParam.CFROM_DEPT_CODE == null ||
                _viewModel.ReportParam.CFROM_DEPT_CODE.Trim().Length <= 0)
            {
                _viewModel.ReportParam.CFROM_DEPT_NAME = "";
                _viewModel.ReportParam.CFROM_REF_NO = "";
                _viewModel.ReportParam.CTO_REF_NO = "";
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

            await _setDefaultRefNo();
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
        var loEx = new R_Exception();

        try
        {
            eventArgs.TargetPageType = typeof(GSL00700);
            eventArgs.Parameter = new GSL00700ParameterDTO();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        R_DisplayException(loEx);
    }

    private async Task AfterLookupFromDept(R_AfterOpenLookupEventArgs eventArgs)
    {
        var loEx = new R_Exception();
        try
        {
            var loTempResult = (GSL00700DTO)eventArgs.Result;
            if (loTempResult == null)
                return;

            _viewModel.ReportParam.CFROM_DEPT_CODE = loTempResult.CDEPT_CODE;
            _viewModel.ReportParam.CFROM_DEPT_NAME = loTempResult.CDEPT_NAME;

            await _setDefaultRefNo();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task OnLostFocusLookupToDept()
    {
        var loEx = new R_Exception();

        var loLookupViewModel = new LookupGSL00700ViewModel();
        try
        {
            if (_viewModel.ReportParam.CTO_DEPT_CODE == null ||
                _viewModel.ReportParam.CTO_DEPT_CODE.Trim().Length <= 0)
            {
                _viewModel.ReportParam.CTO_DEPT_NAME = "";
                _viewModel.ReportParam.CFROM_REF_NO = "";
                _viewModel.ReportParam.CTO_REF_NO = "";
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

            await _setDefaultRefNo();
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
        var loEx = new R_Exception();
        try
        {
            eventArgs.TargetPageType = typeof(GSL00700);
            eventArgs.Parameter = new GSL00700ParameterDTO();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        R_DisplayException(loEx);
    }

    private async Task AfterLookupToDept(R_AfterOpenLookupEventArgs eventArgs)
    {
        var loEx = new R_Exception();
        try
        {
            var loTempResult = (GSL00700DTO)eventArgs.Result;
            if (loTempResult == null)
                return;

            _viewModel.ReportParam.CTO_DEPT_CODE = loTempResult.CDEPT_CODE;
            _viewModel.ReportParam.CTO_DEPT_NAME = loTempResult.CDEPT_NAME;

            await _setDefaultRefNo();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task<bool> _validateDataBeforePrint()
    {
        var loEx = new R_Exception();
        var loReturn = true;
        try
        {
            if (_viewModel.ReportParam.CTRANS_CODE == null ||
                _viewModel.ReportParam.CTRANS_CODE.Trim().Length <= 0)
            {
                var leMsg = await R_MessageBox.Show("Warning", "Please choose Transaction Code",
                    R_eMessageBoxButtonType.OK);
                await ComboTransCode.FocusAsync();
                loReturn = false;
            }


            _viewModel.ReportParam.CREPORT_TYPE = _localizer["BASED_ON_REF_NO"];
            _viewModel.ReportParam.CCURRENCY_TYPE_NAME = _viewModel.RadioCurrencyType.Find(x => x.Key == _viewModel.ReportParam.CCURRENCY_TYPE).Value;
            _viewModel.ReportParam.CTRANSACTION_NAME = _viewModel.TransCodeList?.Find(x => x.CTRANS_CODE == _viewModel.ReportParam.CTRANS_CODE).CTRANSACTION_NAME;
            if (_viewModel.ReportParam.CPERIOD_TYPE == "P")
            {
                _viewModel.ReportParam.CFROM_PERIOD = _viewModel.YearPeriod + _viewModel.FromPeriod + _viewModel.SuffixPeriod;
                _viewModel.ReportParam.CTO_PERIOD = _viewModel.YearPeriod + _viewModel.ToPeriod + _viewModel.SuffixPeriod;
            }
            else
            {
                _viewModel.ReportParam.CFROM_PERIOD = _viewModel.DateFrom?.ToString("yyyyMMdd");
                _viewModel.ReportParam.CTO_PERIOD = _viewModel.DateTo?.ToString("yyyyMMdd");
            }
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        R_DisplayException(loEx);
        return loReturn;
    }

    private async Task OnLostFocusLookupFromRef()
    {
        var loEx = new R_Exception();

        var loLookupViewModel = new LookupGLL00110ViewModel();
        try
        {
            if (!await _validateDataBeforePrint()) return;

            if (_viewModel.ReportParam.CFROM_REF_NO == null ||
                _viewModel.ReportParam.CFROM_REF_NO.Trim().Length <= 0) return;

            var fromDate = (_viewModel.ReportParam.CPERIOD_TYPE == "P")
                ? _viewModel.YearPeriod + _viewModel.FromPeriod
                : _viewModel.DateFrom?.ToString("yyyyMMdd");
            var toDate = (_viewModel.ReportParam.CPERIOD_TYPE == "P")
                ? _viewModel.YearPeriod + _viewModel.ToPeriod
                : _viewModel.DateTo?.ToString("yyyyMMdd");

            var param = new GLL00110ParameterGetRecordDTO
            {
                CTRANS_CODE = _viewModel.ReportParam.CTRANS_CODE,
                CFROM_DEPT_CODE = _viewModel.ReportParam.CFROM_DEPT_CODE,
                CTO_DEPT_CODE = _viewModel.ReportParam.CTO_DEPT_CODE,
                CFROM_DATE = fromDate,
                CTO_DATE = toDate,
                CSEARCH_TEXT = _viewModel.ReportParam.CFROM_REF_NO,
            };

            GLL00110DTO loResult = null;

            loResult = await loLookupViewModel.GLL00110ReferenceNoLookUpByPeriod(param);

            if (loResult == null)
            {
                loEx.Add(R_FrontUtility.R_GetError(
                    typeof(Lookup_GLFrontResources.Resources_Dummy_Class),
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
        R_DisplayException(loEx);
    }

    private async Task BeforeLookupFromRef(R_BeforeOpenLookupEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            if (!await _validateDataBeforePrint()) return;

            var fromDate = (_viewModel.ReportParam.CPERIOD_TYPE == "P")
                ? _viewModel.YearPeriod + _viewModel.FromPeriod
                : _viewModel.DateFrom?.ToString("yyyyMMdd");
            var toDate = (_viewModel.ReportParam.CPERIOD_TYPE == "P")
                ? _viewModel.YearPeriod + _viewModel.ToPeriod
                : _viewModel.DateTo?.ToString("yyyyMMdd");

            eventArgs.Parameter = new GLL00110ParameterDTO
            {
                CTRANS_CODE = _viewModel.ReportParam.CTRANS_CODE,
                CFROM_DEPT_CODE = _viewModel.ReportParam.CFROM_DEPT_CODE,
                CTO_DEPT_CODE = _viewModel.ReportParam.CTO_DEPT_CODE,
                CFROM_DATE = fromDate,
                CTO_DATE = toDate,
            };

            eventArgs.TargetPageType = typeof(GLL00110);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

    EndBlock:
        R_DisplayException(loEx);
    }

    private void AfterLookupFromRef(R_AfterOpenLookupEventArgs eventArgs)
    {
        var loEx = new R_Exception();
        try
        {
            var loTempResult = (GLL00110DTO)eventArgs.Result;
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

    private async Task OnLostFocusLookupToRef()
    {
        var loEx = new R_Exception();

        var loLookupViewModel = new LookupGLL00110ViewModel();
        try
        {
            if (!await _validateDataBeforePrint()) return;

            if (_viewModel.ReportParam.CTO_REF_NO == null ||
                _viewModel.ReportParam.CTO_REF_NO.Trim().Length <= 0) return;

            var fromDate = (_viewModel.ReportParam.CPERIOD_TYPE == "P")
                ? _viewModel.YearPeriod + _viewModel.FromPeriod
                : _viewModel.DateFrom?.ToString("yyyyMMdd");
            var toDate = (_viewModel.ReportParam.CPERIOD_TYPE == "P")
                ? _viewModel.YearPeriod + _viewModel.ToPeriod
                : _viewModel.DateTo?.ToString("yyyyMMdd");

            var param = new GLL00110ParameterGetRecordDTO
            {
                CTRANS_CODE = _viewModel.ReportParam.CTRANS_CODE,
                CFROM_DEPT_CODE = _viewModel.ReportParam.CFROM_DEPT_CODE,
                CTO_DEPT_CODE = _viewModel.ReportParam.CTO_DEPT_CODE,
                CFROM_DATE = fromDate,
                CTO_DATE = toDate,
                CSEARCH_TEXT = _viewModel.ReportParam.CTO_REF_NO
            };

            GLL00110DTO loResult = null;

            loResult = await loLookupViewModel.GLL00110ReferenceNoLookUpByPeriod(param);

            if (loResult == null)
            {
                loEx.Add(R_FrontUtility.R_GetError(
                    typeof(Lookup_GLFrontResources.Resources_Dummy_Class),
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
        R_DisplayException(loEx);
    }

    private async Task BeforeLookupToRef(R_BeforeOpenLookupEventArgs eventArgs)
    {
        var loEx = new R_Exception();
        try
        {
            if (!await _validateDataBeforePrint()) return;

            var fromDate = (_viewModel.ReportParam.CPERIOD_TYPE == "P")
                ? _viewModel.YearPeriod + _viewModel.FromPeriod
                : _viewModel.DateFrom?.ToString("yyyyMMdd");
            var toDate = (_viewModel.ReportParam.CPERIOD_TYPE == "P")
                ? _viewModel.YearPeriod + _viewModel.ToPeriod
                : _viewModel.DateTo?.ToString("yyyyMMdd");

            eventArgs.Parameter = new GLL00110ParameterDTO
            {
                CTRANS_CODE = _viewModel.ReportParam.CTRANS_CODE,
                CFROM_DEPT_CODE = _viewModel.ReportParam.CFROM_DEPT_CODE,
                CTO_DEPT_CODE = _viewModel.ReportParam.CTO_DEPT_CODE,
                CFROM_DATE = fromDate,
                CTO_DATE = toDate,
            };

            eventArgs.TargetPageType = typeof(GLL00110);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

    EndBlock:
        R_DisplayException(loEx);
    }

    private void AfterLookupToRef(R_AfterOpenLookupEventArgs eventArgs)
    {
        var loEx = new R_Exception();
        try
        {
            var loTempResult = (GLL00110DTO)eventArgs.Result;
            if (loTempResult == null)
                return;

            _viewModel.ReportParam.CTO_REF_NO = loTempResult.CREF_NO;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }


    private async Task OnChangeYear(object eventArgs)
    {
        var loEx = new R_Exception();
        try
        {
            await _viewModel.GetPeriodDTList(eventArgs.ToString());
            await _setDefaultRefNo();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task CheckPeriodFrom(string value)
    {
        var loEx = new R_Exception();
        try
        {

            if (string.IsNullOrEmpty(value)) return;

            _viewModel.FromPeriod = value;
            if (int.Parse(_viewModel.FromPeriod) > int.Parse(_viewModel.ToPeriod))
            {
                _viewModel.ToPeriod = _viewModel.FromPeriod;
            }

            await _setDefaultRefNo();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        await R_DisplayExceptionAsync(loEx);
    }

    private async Task CheckPeriodTo(string value)
    {
        var loEx = new R_Exception();
        try
        {
            if (string.IsNullOrEmpty(value)) return;

            _viewModel.ToPeriod = value;
            if (int.Parse(_viewModel.ToPeriod) < int.Parse(_viewModel.FromPeriod))
            {
                _viewModel.FromPeriod = _viewModel.ToPeriod;
            }

            await _setDefaultRefNo();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        await R_DisplayExceptionAsync(loEx);
    }

    //private void ResetRefNo()
    //{
    //    _viewModel.ReportParam.CFROM_REF_NO = "";
    //    _viewModel.ReportParam.CTO_REF_NO = "";
    //}

    private async Task OnChangeByType(object eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            _viewModel.ChangeByType((string)eventArgs);
            await _setDefaultRefNo();
            await _setDefaultRefNo();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        await R_DisplayExceptionAsync(loEx);
    }

    private async Task OnClickPrint()
    {
        var loEx = new R_Exception();
        try
        {
            if (!await _validateDataBeforePrint()) return;

            var loParam = _viewModel.ReportParam;
            loParam.CCOMPANY_ID = _clientHelper.CompanyId;
            loParam.CUSER_ID = _clientHelper.UserId;
            loParam.CLANGUAGE_ID = _clientHelper.Culture.TwoLetterISOLanguageName;
            loParam.CREPORT_CULTURE = _clientHelper.ReportCulture;
            loParam.LIS_PRINT = true;
            loParam.CREPORT_FILENAME = "";
            loParam.CREPORT_FILETYPE = "";
            await _reportService.GetReport(
                "R_DefaultServiceUrlGL",
                "GL",
                "rpt/GLR00100PrintBasedOnRefNo/ActivityReportBasedOnRefNoPost",
                "rpt/GLR00100PrintBasedOnRefNo/ActivityReportBasedOnRefNoGet",
                loParam
            );
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
            if (!await _validateDataBeforePrint()) return;

            var loParam = _viewModel.ReportParam;
            eventArgs.Parameter = loParam;
            eventArgs.PageTitle = _localizer["SAVE_AS"];
            eventArgs.TargetPageType = typeof(GLR00100PopupSaveAs);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

    EndBlock:
        loEx.ThrowExceptionIfErrors();
    }


    private async Task _setDefaultDept()
    {
        var loEx = new R_Exception();

        try
        {
            var loLookupViewModel = new LookupGSL00700ViewModel();
            var loParameter = new GSL00700ParameterDTO();

            await loLookupViewModel.GetDepartmentList(loParameter);
            if (loLookupViewModel.DepartmentGrid.Count > 0)
            {
                _viewModel.ReportParam.CFROM_DEPT_CODE =
                    loLookupViewModel.DepartmentGrid.FirstOrDefault()?.CDEPT_CODE;
                _viewModel.ReportParam.CFROM_DEPT_NAME = loLookupViewModel.DepartmentGrid
                    .Where(x => x.CDEPT_CODE == _viewModel.ReportParam.CFROM_DEPT_CODE)
                    .Select(x => x.CDEPT_NAME).FirstOrDefault() ?? string.Empty;

                _viewModel.ReportParam.CTO_DEPT_CODE = loLookupViewModel.DepartmentGrid.LastOrDefault()?.CDEPT_CODE;
                _viewModel.ReportParam.CTO_DEPT_NAME = loLookupViewModel.DepartmentGrid
                    .Where(x => x.CDEPT_CODE == _viewModel.ReportParam.CTO_DEPT_CODE)
                    .Select(x => x.CDEPT_NAME).LastOrDefault() ?? string.Empty;
            }
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }


    private async Task _setDefaultRefNo()
    {
        var loEx = new R_Exception();

        try
        {
            var fromDate = (_viewModel.ReportParam.CPERIOD_TYPE == "P")
                ? _viewModel.YearPeriod + _viewModel.FromPeriod
                : _viewModel.DateFrom?.ToString("yyyyMMdd");
            var toDate = (_viewModel.ReportParam.CPERIOD_TYPE == "P")
                ? _viewModel.YearPeriod + _viewModel.ToPeriod
                : _viewModel.DateTo?.ToString("yyyyMMdd");

            var loLookupViewModel = new LookupGLL00110ViewModel();
            var loParameter = new GLL00110ParameterDTO()
            {
                CTRANS_CODE = _viewModel.ReportParam.CTRANS_CODE,
                CFROM_DEPT_CODE = _viewModel.ReportParam.CFROM_DEPT_CODE,
                CTO_DEPT_CODE = _viewModel.ReportParam.CTO_DEPT_CODE,
                CFROM_DATE = fromDate,
                CTO_DATE = toDate
            };

            await loLookupViewModel.GLL00110ReferenceNoLookUpByPeriod(loParameter);
            if (loLookupViewModel.loList.Count > 0)
            {
                _viewModel.ReportParam.CFROM_REF_NO = loLookupViewModel.loList.FirstOrDefault()?.CREF_NO;
                _viewModel.ReportParam.CTO_REF_NO = loLookupViewModel.loList.LastOrDefault()?.CREF_NO;
            }
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }


    private async Task ValueChangeTransCode(string val)
    {
        var loEx = new R_Exception();
        try
        {
            if (string.IsNullOrEmpty(val)) return;

            _viewModel.ReportParam.CTRANS_CODE = string.IsNullOrEmpty(val) ? "" : val;
            await _setDefaultRefNo();


        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        await R_DisplayExceptionAsync(loEx);
    }

}