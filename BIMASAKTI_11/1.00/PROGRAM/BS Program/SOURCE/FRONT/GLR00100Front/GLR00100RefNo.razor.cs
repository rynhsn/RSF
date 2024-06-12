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

    private void AfterLookupFromDept(R_AfterOpenLookupEventArgs eventArgs)
    {
        var loEx = new R_Exception();
        try
        {
            var loTempResult = (GSL00700DTO)eventArgs.Result;
            if (loTempResult == null)
                return;

            _viewModel.ReportParam.CFROM_DEPT_CODE = loTempResult.CDEPT_CODE;
            _viewModel.ReportParam.CFROM_DEPT_NAME = loTempResult.CDEPT_NAME;
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

    private void AfterLookupToDept(R_AfterOpenLookupEventArgs eventArgs)
    {
        var loEx = new R_Exception();
        try
        {
            var loTempResult = (GSL00700DTO)eventArgs.Result;
            if (loTempResult == null)
                return;

            _viewModel.ReportParam.CTO_DEPT_CODE = loTempResult.CDEPT_CODE;
            _viewModel.ReportParam.CTO_DEPT_NAME = loTempResult.CDEPT_NAME;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task<bool> HasTransCode()
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
            if (!await HasTransCode()) return;
            
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
                CSEARCH_TEXT = _viewModel.ReportParam.CFROM_REF_NO
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
            if (!await HasTransCode()) return;
            
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
            if (!await HasTransCode()) return;
            
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
            if (!await HasTransCode()) return;
            
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

            ResetRefNo();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private void CheckPeriodFrom(object obj)
    {
        var lcData = (string)obj;
        if (_viewModel.FromPeriod == null) return;
        if (int.Parse(lcData) > int.Parse(_viewModel.ToPeriod))
        {
            _viewModel.ToPeriod = lcData;
        }

        ResetRefNo();
    }

    private void CheckPeriodTo(object obj)
    {
        var lcData = (string)obj;
        if (_viewModel.ToPeriod == null) return;
        if (int.Parse(lcData) < int.Parse(_viewModel.FromPeriod))
        {
            _viewModel.FromPeriod = lcData;
        }

        ResetRefNo();
    }

    private void ResetRefNo()
    {
        _viewModel.ReportParam.CFROM_REF_NO = "";
        _viewModel.ReportParam.CTO_REF_NO = "";
    }

    private void OnChangeByType(object eventArgs)
    {
        _viewModel.ChangeByType((string)eventArgs);
        ResetRefNo();
    }

    private async Task OnClickPrint()
    {
        var loEx = new R_Exception();
        try
        {
            // if (_viewModel.ReportParam.CFROM_DEPT_CODE == null ||
            //     _viewModel.ReportParam.CFROM_DEPT_CODE.Trim().Length <= 0)
            // {
            //     var loMsg = await R_MessageBox.Show("Warning", "Please fill From Department",
            //         R_eMessageBoxButtonType.OK);
            //     await TextFromDept.FocusAsync();
            //     return;
            // }
            //
            // if (_viewModel.ReportParam.CTO_DEPT_CODE == null ||
            //     _viewModel.ReportParam.CTO_DEPT_CODE.Trim().Length <= 0)
            // {
            //     var loMsg = await R_MessageBox.Show("Warning", "Please fill To Department",
            //         R_eMessageBoxButtonType.OK);
            //     await TextToDept.FocusAsync();
            //     return;
            // }
            //
            // if (_viewModel.ReportParam.CFROM_REF_NO == null ||
            //     _viewModel.ReportParam.CFROM_REF_NO.Trim().Length <= 0)
            // {
            //     var loMsg = await R_MessageBox.Show("Warning", "Please fill From Reference No.",
            //         R_eMessageBoxButtonType.OK);
            //     await TextFromRef.FocusAsync();
            //     return;
            // }
            //
            // if (_viewModel.ReportParam.CTO_REF_NO == null ||
            //     _viewModel.ReportParam.CTO_REF_NO.Trim().Length <= 0)
            // {
            //     var loMsg = await R_MessageBox.Show("Warning", "Please fill To Reference No.",
            //         R_eMessageBoxButtonType.OK);
            //     await TextToRef.FocusAsync();
            //     return;
            // }
            
            
            if (!await HasTransCode()) return;

            var loParam = _viewModel.ReportParam;
            loParam.CCOMPANY_ID = _clientHelper.CompanyId;
            loParam.CUSER_ID = _clientHelper.UserId;
            loParam.CLANGUAGE_ID = _clientHelper.Culture.TwoLetterISOLanguageName;
            loParam.CREPORT_CULTURE = _clientHelper.ReportCulture;
            loParam.CREPORT_TYPE = _localizer["BASED_ON_REF_NO"];
            if (loParam.CPERIOD_TYPE == "P")
            {
                loParam.CFROM_PERIOD = _viewModel.YearPeriod + _viewModel.FromPeriod + _viewModel.SuffixPeriod;
                loParam.CTO_PERIOD = _viewModel.YearPeriod + _viewModel.ToPeriod + _viewModel.SuffixPeriod;
            }
            else
            {
                loParam.CFROM_PERIOD = _viewModel.DateFrom?.ToString("yyyyMMdd");
                loParam.CTO_PERIOD = _viewModel.DateTo?.ToString("yyyyMMdd");
            }

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
}