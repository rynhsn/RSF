using BlazorClientHelper;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using Lookup_GSModel.ViewModel;
using Lookup_PMCOMMON.DTOs;
using Lookup_PMFRONT;
using Lookup_PMModel.ViewModel.LML00500;
using Microsoft.AspNetCore.Components;
using PMR00220COMMON;
using PMR00220COMMON.DTO_s;
using PMR00220FrontResources;
using PMR00220MODEL.View_Models;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_BlazorFrontEnd.Interfaces;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;

namespace PMR00220FRONT
{
    public partial class PMR00220 : R_Page
    {
        private PMR00220ViewModel _viewModel = new();

        private R_Conductor _conductorRef;
        [Inject] IClientHelper _clientHelper { get; set; }
        [Inject] private R_ILocalizer<Resources_Dummy_Class> _localizer { get; set; }
        [Inject] private R_IReport _reportService { get; set; }
        private R_RadioGroup<ReportTypeDTO, string> _radioReportType;
        protected override async Task R_Init_From_Master(object poParameter)
        {

            R_Exception loEx = new R_Exception();
            try
            {
                await _viewModel.InitProcess(_localizer);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }

        #region PropertyDropdown
        public void ComboboxPropertyValueChanged(string poParam)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                _viewModel._ReportParam.CPROPERTY_ID = string.IsNullOrWhiteSpace(poParam) ? "" : poParam;
                _viewModel._ReportParam.CFROM_SALESMAN_ID = "";
                _viewModel._ReportParam.CFROM_SALESMAN_NAME = "";
                _viewModel._ReportParam.CTO_SALESMAN_ID = "";
                _viewModel._ReportParam.CTO_SALESMAN_NAME = "";
                _viewModel._ReportParam.CFROM_DEPARTMENT_ID = "";
                _viewModel._ReportParam.CFROM_DEPARTMENT_NAME = "";
                _viewModel._ReportParam.CTO_DEPARTMENT_ID = "";
                _viewModel._ReportParam.CTO_DEPARTMENT_NAME = "";
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }
        #endregion

        #region lookupFromDept
        private void BeforeOpen_lookupFromDept(R_BeforeOpenLookupEventArgs eventArgs)
        {
            eventArgs.Parameter = new GSL00700ParameterDTO();
            eventArgs.TargetPageType = typeof(GSL00700);
        }
        private async Task AfterOpen_lookupFromDeptAsync(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loTempResult = (GSL00700DTO)eventArgs.Result;
            if (loTempResult == null)
            {
                var loValidate = await R_MessageBox.Show("", _localizer["_validationDeptFromResult"], R_eMessageBoxButtonType.OK);
                return;
            }
            _viewModel._ReportParam.CFROM_DEPARTMENT_ID = loTempResult.CDEPT_CODE;
            _viewModel._ReportParam.CFROM_DEPARTMENT_NAME = loTempResult.CDEPT_CODE;
        }
        private async Task OnLostFocus_LookupFromDept()
        {
            var loEx = new R_Exception();

            try
            {
                if (!string.IsNullOrWhiteSpace(_viewModel._ReportParam.CFROM_DEPARTMENT_ID))
                {

                    LookupGSL00700ViewModel loLookupViewModel = new LookupGSL00700ViewModel(); //use GSL's model
                    var loParam = new GSL00700ParameterDTO // use match param as GSL's dto, send as type in search texbox
                    {
                        CSEARCH_TEXT = _viewModel._ReportParam.CFROM_DEPARTMENT_ID, // property that bindded to search textbox
                    };
                    var loResult = await loLookupViewModel.GetDepartment(loParam); //retrive single record 

                    //show result & show name/related another fields
                    if (loResult == null)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                                typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                                "_ErrLookup01"));
                        _viewModel._ReportParam.CFROM_DEPARTMENT_NAME = ""; //kosongin bind textbox name kalo gaada
                        goto EndBlock;
                    }
                    _viewModel._ReportParam.CFROM_DEPARTMENT_ID = loResult.CDEPT_CODE;
                    _viewModel._ReportParam.CFROM_DEPARTMENT_NAME = loResult.CDEPT_NAME; //assign bind textbox name kalo ada
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        EndBlock:
            R_DisplayException(loEx);

        }
        #endregion

        #region lookupFromDept
        private void BeforeOpen_lookupToDept(R_BeforeOpenLookupEventArgs eventArgs)
        {
            eventArgs.Parameter = new GSL00700ParameterDTO();
            eventArgs.TargetPageType = typeof(GSL00700);
        }
        private async Task AfterOpen_lookupToDeptAsync(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loTempResult = (GSL00700DTO)eventArgs.Result;
            if (loTempResult == null)
            {
                var loValidate = await R_MessageBox.Show("", _localizer["_validationDeptToResult"], R_eMessageBoxButtonType.OK);
                return;
            }
            _viewModel._ReportParam.CTO_DEPARTMENT_ID = loTempResult.CDEPT_CODE;
            _viewModel._ReportParam.CTO_DEPARTMENT_NAME = loTempResult.CDEPT_CODE;
        }
        private async Task OnLostFocus_LookupToDept()
        {
            var loEx = new R_Exception();

            try
            {
                if (!string.IsNullOrWhiteSpace(_viewModel._ReportParam.CTO_DEPARTMENT_ID))
                {

                    LookupGSL00700ViewModel loLookupViewModel = new LookupGSL00700ViewModel(); //use GSL's model
                    var loParam = new GSL00700ParameterDTO // use match param as GSL's dto, send as type in search texbox
                    {
                        CSEARCH_TEXT = _viewModel._ReportParam.CTO_DEPARTMENT_ID, // property that bindded to search textbox
                    };

                    var loResult = await loLookupViewModel.GetDepartment(loParam); //retrive single record 

                    //show result & show name/related another fields
                    if (loResult == null)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                                typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                                "_ErrLookup01"));
                        _viewModel._ReportParam.CTO_DEPARTMENT_NAME = ""; //kosongin bind textbox name kalo gaada
                        goto EndBlock;
                    }
                    _viewModel._ReportParam.CTO_DEPARTMENT_ID = loResult.CDEPT_CODE;
                    _viewModel._ReportParam.CTO_DEPARTMENT_NAME = loResult.CDEPT_NAME; //assign bind textbox name kalo ada
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        EndBlock:
            R_DisplayException(loEx);

        }
        #endregion

        #region lookupFromSalesman
        private void BeforeOpen_lookupFromSalesman(R_BeforeOpenLookupEventArgs eventArgs)
        {
            eventArgs.Parameter = new LML00500ParameterDTO()
            {
                CPROPERTY_ID = _viewModel._ReportParam.CPROPERTY_ID,
                CCOMPANY_ID = _clientHelper.CompanyId,
                CUSER_ID = _clientHelper.UserId
            };
            eventArgs.TargetPageType = typeof(LML00500);
        }
        private async Task AfterOpen_lookupFromSalesmanAsync(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loTempResult = (LML00500DTO)eventArgs.Result;
            if (loTempResult == null)
            {
                var loValidate = await R_MessageBox.Show("", _localizer["_validationSalesmanFromResult"], R_eMessageBoxButtonType.OK);
                return;
            }
            _viewModel._ReportParam.CFROM_SALESMAN_ID = loTempResult.CSALESMAN_ID;
            _viewModel._ReportParam.CFROM_SALESMAN_NAME = loTempResult.CSALESMAN_NAME;
        }
        private async Task OnLostFocus_LookupFromSalesman()
        {
            var loEx = new R_Exception();

            try
            {
                if (!string.IsNullOrWhiteSpace(_viewModel._ReportParam.CFROM_SALESMAN_ID))
                {
                    LookupLML00500ViewModel loLookupViewModel = new LookupLML00500ViewModel();
                    var param = new LML00500ParameterDTO
                    {
                        CCOMPANY_ID = _clientHelper.CompanyId,
                        CPROPERTY_ID = _viewModel._ReportParam.CPROPERTY_ID,
                        CUSER_ID = _clientHelper.UserId,
                        CSEARCH_TEXT = _viewModel._ReportParam.CFROM_SALESMAN_ID,
                    };
                    LML00500DTO loResult = await loLookupViewModel.GetSalesman(param);


                    if (loResult == null)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                                typeof(Lookup_PMFrontResources.Resources_Dummy_Class_LookupPM),
                                "_ErrLookup01"));
                        _viewModel._ReportParam.CFROM_SALESMAN_ID = "";
                        _viewModel._ReportParam.CFROM_SALESMAN_NAME = "";
                    }
                    else
                    {
                        _viewModel._ReportParam.CFROM_SALESMAN_ID = loResult.CSALESMAN_ID;
                        _viewModel._ReportParam.CFROM_SALESMAN_NAME = loResult.CSALESMAN_NAME;
                    }

                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        #endregion

        #region lookupFromSalesman
        private void BeforeOpen_lookupToSalesman(R_BeforeOpenLookupEventArgs eventArgs)
        {
            eventArgs.Parameter = new LML00500ParameterDTO()
            {
                CPROPERTY_ID = _viewModel._ReportParam.CPROPERTY_ID,
                CCOMPANY_ID = _clientHelper.CompanyId,
                CUSER_ID = _clientHelper.UserId,
            };
            eventArgs.TargetPageType = typeof(LML00500);
        }
        private async Task AfterOpen_lookupToSalesmanAsync(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loTempResult = (LML00500DTO)eventArgs.Result;
            if (loTempResult == null)
            {
                var loValidate = await R_MessageBox.Show("", _localizer["_validationSalesmanFromResult"], R_eMessageBoxButtonType.OK);
                return;
            }
            _viewModel._ReportParam.CTO_SALESMAN_ID = loTempResult.CSALESMAN_ID;
            _viewModel._ReportParam.CTO_SALESMAN_NAME = loTempResult.CSALESMAN_NAME;
        }
        private async Task OnLostFocus_LookupToSalesman()
        {
            var loEx = new R_Exception();

            try
            {
                if (!string.IsNullOrWhiteSpace(_viewModel._ReportParam.CTO_SALESMAN_ID))
                {
                    LookupLML00500ViewModel loLookupViewModel = new LookupLML00500ViewModel();
                    var param = new LML00500ParameterDTO
                    {
                        CCOMPANY_ID = _clientHelper.CompanyId,
                        CPROPERTY_ID = _viewModel._ReportParam.CPROPERTY_ID,
                        CUSER_ID = _clientHelper.UserId,
                        CSEARCH_TEXT = _viewModel._ReportParam.CTO_SALESMAN_ID,
                    };
                    LML00500DTO loResult = await loLookupViewModel.GetSalesman(param);


                    if (loResult == null)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                                typeof(Lookup_PMFrontResources.Resources_Dummy_Class_LookupPM),
                                "_ErrLookup01"));
                        _viewModel._ReportParam.CTO_SALESMAN_ID = "";
                        _viewModel._ReportParam.CTO_SALESMAN_NAME = "";
                    }
                    else
                    {
                        _viewModel._ReportParam.CTO_SALESMAN_ID = loResult.CSALESMAN_ID;
                        _viewModel._ReportParam.CTO_SALESMAN_NAME = loResult.CSALESMAN_NAME;
                    }

                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        #endregion

        #region PeriodOnchange
        public async Task NumOnChanged_FromPeriod()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                _viewModel._fromPeriods = new ObservableCollection<PeriodDtDTO>(
                    await _viewModel.GetPeriodDtAsync(
                        string.IsNullOrWhiteSpace(_viewModel._YearFromPeriod.ToString())
                        ? _viewModel._InitToday.Year.ToString()
                        : _viewModel._YearFromPeriod.ToString())
                    );
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        public async Task NumOnChanged_ToPeriod()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                _viewModel._toPeriods = new ObservableCollection<PeriodDtDTO>(
                    await _viewModel.GetPeriodDtAsync(
                        string.IsNullOrWhiteSpace(_viewModel._YearToPeriod.ToString())
                        ? _viewModel._InitToday.Year.ToString()
                        : _viewModel._YearToPeriod.ToString()));
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        #endregion

        #region print
        private async Task OnclickBtn_Print()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                var loData = GenerateParam();
                Validation(loData);
                await GeneratePrintAsync(loData);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private PMR00220ParamDTO GenerateParam()
        {
            R_Exception loEx = new R_Exception();
            PMR00220ParamDTO loParam = new();
            try
            {
                loParam = new PMR00220ParamDTO()
                {
                    CLANG_ID = _clientHelper.Culture.Name,
                    CCOMPANY_ID = _clientHelper.CompanyId,
                    CREPORT_CULTURE = _clientHelper.ReportCulture.ToString(),
                    CUSER_ID = _clientHelper.UserId,
                    CPROPERTY_ID = _viewModel._ReportParam.CPROPERTY_ID,
                    CPROPERTY_NAME = _viewModel._properties.Where(x => x.CPROPERTY_ID == _viewModel._ReportParam.CPROPERTY_ID).FirstOrDefault().CPROPERTY_NAME,
                    CFROM_DEPARTMENT_ID = _viewModel._ReportParam.CFROM_DEPARTMENT_ID,
                    CFROM_DEPARTMENT_NAME = _viewModel._ReportParam.CFROM_DEPARTMENT_NAME,
                    CTO_DEPARTMENT_ID = _viewModel._ReportParam.CTO_DEPARTMENT_ID,
                    CTO_DEPARTMENT_NAME = _viewModel._ReportParam.CTO_DEPARTMENT_NAME,
                    CFROM_SALESMAN_ID = _viewModel._ReportParam.CFROM_SALESMAN_ID,
                    CFROM_SALESMAN_NAME = _viewModel._ReportParam.CFROM_SALESMAN_NAME,
                    CTO_SALESMAN_ID = _viewModel._ReportParam.CTO_SALESMAN_ID,
                    CTO_SALESMAN_NAME = _viewModel._ReportParam.CTO_SALESMAN_NAME,
                    CFROM_PERIOD = _viewModel._YearFromPeriod + _viewModel._MonthFromPeriod, //yyyyMM
                    CTO_PERIOD = _viewModel._YearToPeriod + _viewModel._MonthToPeriod, //yyyyMM
                    LIS_OUTSTANDING = _viewModel._ReportParam.LIS_OUTSTANDING,
                    CREPORT_TYPE = _viewModel._ReportType
                };
                DateTime loFromDate = DateTime.ParseExact(loParam.CFROM_PERIOD, "yyyyMM", CultureInfo.InvariantCulture);
                DateTime loToDate = DateTime.ParseExact(loParam.CTO_PERIOD, "yyyyMM", CultureInfo.InvariantCulture);
                loParam.CPERIOD_DISPLAY = (loFromDate != loToDate)
                    ? $"{loFromDate.ToString("MMMM yyyy", CultureInfo.InvariantCulture)} – {loToDate.ToString("MMMM yyyy", CultureInfo.InvariantCulture)}"
                    : $"{loFromDate.ToString("MMMM yyyy", CultureInfo.InvariantCulture)}";
                loParam.CREPORT_TYPE_DISPLAY = _viewModel._ReportType == "1" ? _localizer["_radioSummary"] : _localizer["_radioDetail"];
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            return loParam;
        }
        private void Validation(PMR00220ParamDTO poParam)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                //validation
                if (string.IsNullOrWhiteSpace(poParam.CPROPERTY_ID))
                {
                    loEx.Add("", _localizer["_validationEmptyProperty"]);

                }
                if (string.IsNullOrWhiteSpace(poParam.CFROM_DEPARTMENT_ID) || string.IsNullOrWhiteSpace(poParam.CTO_DEPARTMENT_ID))
                {
                    loEx.Add("", _localizer["_validationEmptyDept"]);

                }
                if (string.IsNullOrWhiteSpace(poParam.CFROM_SALESMAN_ID) || string.IsNullOrWhiteSpace(poParam.CTO_SALESMAN_ID))
                {
                    loEx.Add("", _localizer["_validationEmptySalesman"]);

                }
                if (int.Parse(poParam.CTO_PERIOD) < int.Parse(poParam.CFROM_PERIOD))
                {
                    loEx.Add("", _localizer["_validationHigherPeriod"]);

                }
                if (loEx.HasError)
                {
                    loEx.ThrowExceptionIfErrors();
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private async Task GeneratePrintAsync(PMR00220ParamDTO poParam)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                if (poParam.CREPORT_TYPE == "2")
                {
                    await LOIEvent_PrintDetailAsync(poParam);
                }
                else
                {
                    await LOIEvent_PrintSummaryAsync(poParam);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private async Task LOIEvent_PrintSummaryAsync(PMR00220ParamDTO poParam)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                await _reportService.GetReport(
                    "R_DefaultServiceUrlPM",
                    "PM",
                    "rpt/PMR00221Print/DownloadResultPrintPost",
                    "rpt/PMR00221Print/LOIEventSummary_ReportListGet", poParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private async Task LOIEvent_PrintDetailAsync(PMR00220ParamDTO poParam)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                await _reportService.GetReport(
                    "R_DefaultServiceUrlPM",
                    "PM",
                    "rpt/PMR00222Print/DownloadResultPrintPost",
                    "rpt/PMR00222Print/LOIEventDetail_ReportListGet", poParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        #endregion

        #region Popup save as

        private void BeforeOpen_PopupSaveAsAsync(R_BeforeOpenPopupEventArgs eventArgs)
        {
            eventArgs.PageTitle = _localizer["_title_saveas"];
            eventArgs.TargetPageType = typeof(PMR00221);
        }

        private async Task AfterOpen_PopupSaveAsAsync(R_AfterOpenPopupEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                string lsReportType = (string)eventArgs.Result;
                if (!string.IsNullOrWhiteSpace(lsReportType))
                {
                    string[] lcResultSaveAs = lsReportType.Split(',');
                    var loPrintParam = GenerateParam();
                    loPrintParam.CREPORT_FILENAME = lcResultSaveAs[0];
                    loPrintParam.CREPORT_FILEEXT = lcResultSaveAs[1];
                    Validation(loPrintParam);
                    await GeneratePrintAsync(loPrintParam);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        #endregion


    }
}

