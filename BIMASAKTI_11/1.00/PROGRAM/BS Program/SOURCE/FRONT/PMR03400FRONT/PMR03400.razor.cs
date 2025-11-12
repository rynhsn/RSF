using BlazorClientHelper;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using Lookup_GSModel.ViewModel;
using Lookup_PMCOMMON.DTOs;
using Lookup_PMFRONT;
using Lookup_PMModel.ViewModel.LML00500;
using Lookup_PMModel.ViewModel.LML00600;
using Microsoft.AspNetCore.Components;
using PMR03400COMMON;
using PMR03400COMMON.DTO_s;
using PMR03400FrontResources;
using PMR03400MODEL.ViewModel;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_BlazorFrontEnd.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMR03400FRONT
{
    public partial class PMR03400 : R_Page
    {
        private PMR03400ViewModel _viewModel = new();

        private R_Conductor _conductorRef;
        [Inject] IClientHelper _clientHelper { get; set; }
        [Inject] private R_ILocalizer<Resources_Dummy_Class> _localizer { get; set; }
        [Inject] private R_IReport _reportService { get; set; }

        protected override async Task R_Init_From_Master(object poParameter)
        {

            R_Exception loEx = new R_Exception();
            try
            {
                await _viewModel.GetPropertyAsync();

                //set default data
                if (_viewModel._properties.Count > 0)
                {
                   _viewModel._ReportParam.CPROPERTY_ID = _viewModel._properties[0].CPROPERTY_ID;
                    await _viewModel.GetSystemParam(_viewModel._properties[0].CPROPERTY_ID);

                    if (_viewModel.SystemParam != null)
                    {
                        _viewModel._YearFromPeriod = int.Parse(_viewModel.SystemParam.CCURRENT_PERIOD_YY);
                        _viewModel._YearToPeriod = int.Parse(_viewModel.SystemParam.CCURRENT_PERIOD_YY);

                        //get period
                        var loCurrentPeriodFrom = await _viewModel.GetPeriodDtAsync(_viewModel.SystemParam.CCURRENT_PERIOD_YY);
                        _viewModel._fromPeriods = new ObservableCollection<PeriodDtDTO>(loCurrentPeriodFrom);
                        _viewModel._MonthFromPeriod = _viewModel.SystemParam.CCURRENT_PERIOD_MM;

                        var loCurrentPeriodTo = await _viewModel.GetPeriodDtAsync(_viewModel.SystemParam.CCURRENT_PERIOD_YY);
                        _viewModel._toPeriods = new ObservableCollection<PeriodDtDTO>(loCurrentPeriodTo);
                        _viewModel._MonthToPeriod = _viewModel.SystemParam.CCURRENT_PERIOD_MM;
                    }
                }
                _viewModel._ReportParam.CCURRENCY_TYPE = "L";

                await _setDefaultCustomer();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }


        #region PropertyComboBox
        public async Task ComboboxPropertyValueChanged(string poParam)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                _viewModel._ReportParam.CPROPERTY_ID = poParam;
                await _viewModel.GetSystemParam(poParam);
                if (_viewModel.SystemParam != null)
                {
                    _viewModel._YearFromPeriod = int.Parse(_viewModel.SystemParam.CCURRENT_PERIOD_YY);
                    _viewModel._MonthFromPeriod = _viewModel.SystemParam.CCURRENT_PERIOD_MM;
                    _viewModel._YearToPeriod = int.Parse(_viewModel.SystemParam.CCURRENT_PERIOD_YY);
                    _viewModel._MonthToPeriod = _viewModel.SystemParam.CCURRENT_PERIOD_MM;

                    //get period
                    var loCurrentPeriodFrom = await _viewModel.GetPeriodDtAsync(_viewModel.SystemParam.CCURRENT_PERIOD_YY);
                    _viewModel._fromPeriods = new ObservableCollection<PeriodDtDTO>(loCurrentPeriodFrom);
                    var loCurrentPeriodTo = await _viewModel.GetPeriodDtAsync(_viewModel.SystemParam.CCURRENT_PERIOD_YY);
                    _viewModel._toPeriods = new ObservableCollection<PeriodDtDTO>(loCurrentPeriodTo);
                }
                else
                {
                    _viewModel._YearFromPeriod = 0;
                    _viewModel._MonthFromPeriod = "";
                    _viewModel._YearToPeriod = 0;
                    _viewModel._MonthToPeriod = "";
                }
                await _setDefaultCustomer();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }
        #endregion

        #region lookupFromCustomer
        private void BeforeOpen_lookupFromCustomer(R_BeforeOpenLookupEventArgs eventArgs)
        {
            eventArgs.Parameter = new LML00600ParameterDTO()
            {
                CPROPERTY_ID = _viewModel._ReportParam.CPROPERTY_ID ?? "",
                CCUSTOMER_TYPE = "01"
            };
            eventArgs.TargetPageType = typeof(LML00600);
        }
        private async Task AfterOpen_lookupFromCustomerAsync(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loTempResult = (LML00600DTO)eventArgs.Result;
            if (loTempResult == null)
            {
                var loValidate = await R_MessageBox.Show("", _localizer["_validationDeptFromResult"], R_eMessageBoxButtonType.OK);
                return;
            }
            _viewModel._ReportParam.CFR_CODE = loTempResult.CTENANT_ID;
            _viewModel._ReportParam.CFR_CODE_NAME = loTempResult.CTENANT_NAME;
        }
        private async Task OnLostFocus_LookupFromCustomer()
        {
            var loEx = new R_Exception();

            try
            {
                if (!string.IsNullOrWhiteSpace(_viewModel._ReportParam.CFR_CODE))
                {

                    LookupLML00600ViewModel loLookupViewModel = new LookupLML00600ViewModel(); //use GSL's model
                    var loParam = new LML00600ParameterDTO // use match param as GSL's dto, send as type in search texbox
                    {
                        CSEARCH_TEXT = _viewModel._ReportParam.CFR_CODE, // property that bindded to search textbox
                    };
                    var loResult = await loLookupViewModel.GetTenant(loParam); //retrive single record

                    //show result & show name/related another fields
                    if (loResult == null)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                                typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                                "_ErrLookup01"));
                        _viewModel._ReportParam.CFR_CODE_NAME = ""; //kosongin bind textbox name kalo gaada
                        goto EndBlock;
                    }
                    _viewModel._ReportParam.CFR_CODE = loResult.CTENANT_ID;
                    _viewModel._ReportParam.CFR_CODE_NAME = loResult.CTENANT_NAME; //assign bind textbox name kalo ada
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

        #region lookupToCustomer
        private void BeforeOpen_lookupToCustomer(R_BeforeOpenLookupEventArgs eventArgs)
        {
            eventArgs.Parameter = new LML00600ParameterDTO()
            {
                CPROPERTY_ID = _viewModel._ReportParam.CPROPERTY_ID ?? "",
                CCUSTOMER_TYPE = "01"
            };
            eventArgs.TargetPageType = typeof(LML00600);
        }
        private async Task AfterOpen_lookupToCustomerAsync(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loTempResult = (LML00600DTO)eventArgs.Result;
            if (loTempResult == null)
            {
                var loValidate = await R_MessageBox.Show("", _localizer["_validationDeptToResult"], R_eMessageBoxButtonType.OK);
                return;
            }
            _viewModel._ReportParam.CTO_CODE = loTempResult.CTENANT_ID;
            _viewModel._ReportParam.CTO_CODE_NAME = loTempResult.CTENANT_NAME;
        }
        private async Task OnLostFocus_LookupToCustomer()
        {
            var loEx = new R_Exception();

            try
            {
                if (!string.IsNullOrWhiteSpace(_viewModel._ReportParam.CTO_CODE))
                {

                    LookupLML00600ViewModel loLookupViewModel = new LookupLML00600ViewModel(); //use GSL's model
                    var loParam = new LML00600ParameterDTO // use match param as GSL's dto, send as type in search texbox
                    {
                        CSEARCH_TEXT = _viewModel._ReportParam.CTO_CODE, // property that bindded to search textbox
                    };

                    var loResult = await loLookupViewModel.GetTenant(loParam); //retrive single record 

                    //show result & show name/related another fields
                    if (loResult == null)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                                typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                                "_ErrLookup01"));
                        _viewModel._ReportParam.CTO_CODE_NAME = ""; //kosongin bind textbox name kalo gaada
                        goto EndBlock;
                    }
                    _viewModel._ReportParam.CTO_CODE = loResult.CTENANT_ID;
                    _viewModel._ReportParam.CTO_CODE_NAME = loResult.CTENANT_NAME; //assign bind textbox name kalo ada
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

        #region defaultValue

        private async Task _setDefaultCustomer()
        {
            var loEx = new R_Exception();

            try
            {
                if (string.IsNullOrEmpty(_viewModel._ReportParam.CPROPERTY_ID)) return;

                var loLookupViewModel = new LookupLML00600ViewModel();
                var param = new LML00600ParameterDTO
                {
                    CPROPERTY_ID = _viewModel._ReportParam.CPROPERTY_ID,
                    CCUSTOMER_TYPE = "01"
                };
                await loLookupViewModel.GetTenantList(param);
                if (loLookupViewModel.TenantList.Count > 0)
                {
                    _viewModel._ReportParam.CFR_CODE = loLookupViewModel.TenantList.FirstOrDefault()?.CTENANT_ID;
                    _viewModel._ReportParam.CFR_CODE_NAME = loLookupViewModel.TenantList.FirstOrDefault().CTENANT_NAME ?? string.Empty;
                    _viewModel._ReportParam.CTO_CODE = loLookupViewModel.TenantList.LastOrDefault()?.CTENANT_ID;
                    _viewModel._ReportParam.CTO_CODE_NAME = loLookupViewModel.TenantList.LastOrDefault()?.CTENANT_NAME ?? string.Empty;
                }
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

        private PMR03400ParamDTO GenerateParam()
        {
            R_Exception loEx = new R_Exception();
            PMR03400ParamDTO loParam = new();
            try
            {
                loParam = new PMR03400ParamDTO()
                {
                    CLANGUAGE_ID = _clientHelper.Culture.Name,
                    CCOMPANY_ID = _clientHelper.CompanyId,
                    CREPORT_CULTURE = _clientHelper.ReportCulture.ToString(),
                    CUSER_ID = _clientHelper.UserId,
                    CPROPERTY_ID = _viewModel._ReportParam.CPROPERTY_ID,
                    CPROPERTY_NAME = _viewModel._properties.Where(x => x.CPROPERTY_ID == _viewModel._ReportParam.CPROPERTY_ID).FirstOrDefault().CPROPERTY_NAME,
                    CFR_CODE = _viewModel._ReportParam.CFR_CODE,
                    CFR_CODE_NAME = _viewModel._ReportParam.CFR_CODE_NAME,
                    CTO_CODE = _viewModel._ReportParam.CTO_CODE,
                    CTO_CODE_NAME = _viewModel._ReportParam.CTO_CODE_NAME,
                    
                    CFR_PERIOD = _viewModel._YearFromPeriod + _viewModel._MonthFromPeriod, //yyyyMM
                    CTO_PERIOD = _viewModel._YearToPeriod + _viewModel._MonthToPeriod, //yyyyMM
                    LDESC = _viewModel._ReportParam.LDESC,
                    LPROFORMA = _viewModel._ReportParam.LPROFORMA
                };
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            return loParam;
        }

        private void Validation(PMR03400ParamDTO poParam)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                //validation
                if (string.IsNullOrWhiteSpace(poParam.CPROPERTY_ID))
                {
                    loEx.Add("", _localizer["_validationEmptyProperty"]);

                }
                if (string.IsNullOrWhiteSpace(poParam.CFR_CODE))
                {
                    loEx.Add("", _localizer["_validationEmptyFromCustomer"]);

                }
                if (string.IsNullOrWhiteSpace(poParam.CTO_CODE))
                {
                    loEx.Add("", _localizer["_validationEmptyToCustomer"]);

                }
                if (int.Parse(poParam.CTO_PERIOD) < int.Parse(poParam.CFR_PERIOD))
                {
                    loEx.Add("", _localizer["_validationHigherPeriod"]);

                }
                if (_viewModel._YearFromPeriod == 0 || string.IsNullOrEmpty(_viewModel._MonthFromPeriod))
                {
                    loEx.Add("", _localizer["_validationEmptyFromPeriod"]);
                }
                if (_viewModel._YearToPeriod == 0 || string.IsNullOrEmpty(_viewModel._MonthToPeriod))
                {
                    loEx.Add("", _localizer["_validationEmptyToPeriod"]);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private async Task GeneratePrintAsync(PMR03400ParamDTO poParam)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                await _reportService.GetReport(
                    "R_DefaultServiceUrlPM",
                    "PM",
                    "rpt/PMR03400Print/DownloadResultPrintPost",
                    "rpt/PMR03400Print/CustomerLedger_ReportListGet", poParam);
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
            eventArgs.TargetPageType = typeof(PMR03401);
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
