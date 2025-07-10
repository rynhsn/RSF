using BlazorClientHelper;
using Lookup_PMCOMMON.DTOs;
using Lookup_PMFRONT;
using Lookup_PMModel.ViewModel.LML00600;
using Microsoft.AspNetCore.Components;
using PMR02400COMMON;
using PMR02400COMMON.DTO_s;
using PMR02400FrontResources;
using PMR02400MODEL;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_BlazorFrontEnd.Interfaces;
using System.Globalization;

namespace PMR02400FRONT
{
    public partial class PMR02400 : R_Page
    {
        private PMR02400ViewModel _viewModel = new();

        private R_Conductor _conductorRef;
        [Inject] IClientHelper _clientHelper { get; set; }
        [Inject] private R_ILocalizer<Resources_Dummy_Class> _localizer { get; set; }
        [Inject] private R_IReport _reportService { get; set; }

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

        public void ComboboxPropertyValueChanged(string poParam)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                _viewModel._ReportParam.CPROPERTY_ID = string.IsNullOrWhiteSpace(poParam) ? "" : poParam;
                _viewModel._ReportParam.CPROPERTY_NAME = _viewModel._properties.FirstOrDefault(x => x.CPROPERTY_ID == _viewModel._ReportParam.CPROPERTY_ID).CPROPERTY_NAME;
                _viewModel._ReportParam.CFROM_CUSTOMER_NAME = "";
                _viewModel._ReportParam.CTO_CUSTOMER_NAME = "";
                _viewModel._ReportParam.CFR_CUSTOMER = "";
                _viewModel._ReportParam.CTO_CUSTOMER = "";
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }

        //lookup
        private void BeforeOpen_lookupFromTenant(R_BeforeOpenLookupEventArgs eventArgs)
        {
            eventArgs.Parameter = new LML00600ParameterDTO()
            { CCOMPANY_ID = _clientHelper.CompanyId, CPROPERTY_ID = _viewModel._ReportParam.CPROPERTY_ID, CCUSTOMER_TYPE = "01", CUSER_ID = _clientHelper.UserId, CSEARCH_TEXT = "" };
            eventArgs.TargetPageType = typeof(LML00600);
        }

        private async Task AfterOpen_lookupFromTenantAsync(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loTempResult = (LML00600DTO)eventArgs.Result;
            if (loTempResult != null)
            {
                _viewModel._ReportParam.CFR_CUSTOMER = loTempResult.CTENANT_ID;
                _viewModel._ReportParam.CFROM_CUSTOMER_NAME = loTempResult.CTENANT_NAME;
            }
        }

        private async Task OnLostFocus_LookupFromTenant()
        {
            var loEx = new R_Exception();

            try
            {
                if (!string.IsNullOrWhiteSpace(_viewModel._ReportParam.CFR_CUSTOMER))
                {

                    LookupLML00600ViewModel loLookupViewModel = new LookupLML00600ViewModel(); //use GSL's model
                    var loParam = new LML00600ParameterDTO // use match param as GSL's dto, send as type in search texbox
                    {
                        CCOMPANY_ID = _clientHelper.CompanyId,
                        CPROPERTY_ID = _viewModel._ReportParam.CPROPERTY_ID,
                        CCUSTOMER_TYPE = "01",
                        CUSER_ID = _clientHelper.UserId,
                        CSEARCH_TEXT = _viewModel._ReportParam.CFR_CUSTOMER, // property that bindded to search textbox
                    };
                    var loResult = await loLookupViewModel.GetTenant(loParam); //retrive single record 

                    //show result & show name/related another fields
                    if (loResult == null)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                                typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                                "_ErrLookup01"));
                        _viewModel._ReportParam.CFROM_CUSTOMER_NAME = ""; //kosongin bind textbox name kalo gaada
                        goto EndBlock;
                    }
                    _viewModel._ReportParam.CFR_CUSTOMER = loResult.CTENANT_ID;
                    _viewModel._ReportParam.CFROM_CUSTOMER_NAME = loResult.CTENANT_NAME; //assign bind textbox name kalo ada
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        EndBlock:
            R_DisplayException(loEx);

        }

        private void BeforeOpen_lookupToTenant(R_BeforeOpenLookupEventArgs eventArgs)
        {
            eventArgs.Parameter = new LML00600ParameterDTO()
            { CCOMPANY_ID = _clientHelper.CompanyId, CPROPERTY_ID = _viewModel._ReportParam.CPROPERTY_ID, CCUSTOMER_TYPE = "01", CUSER_ID = _clientHelper.UserId, CSEARCH_TEXT = "" };
            eventArgs.TargetPageType = typeof(LML00600);
        }

        private async Task AfterOpen_lookupToTenantAsync(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loTempResult = (LML00600DTO)eventArgs.Result;
            if (loTempResult != null)
            {
                _viewModel._ReportParam.CTO_CUSTOMER = loTempResult.CTENANT_ID;
                _viewModel._ReportParam.CTO_CUSTOMER_NAME = loTempResult.CTENANT_NAME;
            }
        }

        private async Task OnLostFocus_LookupToTenant()
        {
            var loEx = new R_Exception();

            try
            {
                if (!string.IsNullOrWhiteSpace(_viewModel._ReportParam.CTO_CUSTOMER))
                {

                    LookupLML00600ViewModel loLookupViewModel = new LookupLML00600ViewModel(); //use GSL's model
                    var loParam = new LML00600ParameterDTO // use match param as GSL's dto, send as type in search texbox
                    {
                        CCOMPANY_ID = _clientHelper.CompanyId,
                        CPROPERTY_ID = _viewModel._ReportParam.CPROPERTY_ID,
                        CCUSTOMER_TYPE = "01",
                        CUSER_ID = _clientHelper.UserId,
                        CSEARCH_TEXT = _viewModel._ReportParam.CTO_CUSTOMER, // property that bindded to search textbox
                    };
                    var loResult = await loLookupViewModel.GetTenant(loParam); //retrive single record 

                    //show result & show name/related another fields
                    if (loResult == null)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                                typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                                "_ErrLookup01"));
                        _viewModel._ReportParam.CTO_CUSTOMER_NAME = ""; //kosongin bind textbox name kalo gaada
                        goto EndBlock;
                    }
                    _viewModel._ReportParam.CTO_CUSTOMER = loResult.CTENANT_ID;
                    _viewModel._ReportParam.CTO_CUSTOMER_NAME = loResult.CTENANT_NAME; //assign bind textbox name kalo ada
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        EndBlock:
            R_DisplayException(loEx);

        }

        //validation
        private async Task Validation_PenaltyRpt()
        {
            var loEx = new R_Exception();
            //validation
            if (string.IsNullOrWhiteSpace(_viewModel._ReportParam.CPROPERTY_ID))
            {
                loEx.Add("", _localizer["_val1"]);
            }
            if (_viewModel._DateBasedOn == "I" & _viewModel._DateCutOff == null)
            {
                loEx.Add("", _localizer["_val2"]);
            }

            if (string.IsNullOrEmpty(_viewModel._MonthFromPeriod) || string.IsNullOrEmpty(_viewModel._MonthToPeriod) || _viewModel._YearFromPeriod == 0 || _viewModel._YearToPeriod == 0)
            {
                loEx.Add("", _localizer["_val3"]);
            }

            if (_viewModel._DateBasedOn == "P" & int.Parse(_viewModel._YearFromPeriod + _viewModel._MonthFromPeriod) > int.Parse(_viewModel._YearToPeriod + _viewModel._MonthToPeriod))
            {
                loEx.Add("", _localizer["_val4"]);
            }

            if (string.IsNullOrWhiteSpace(_viewModel._ReportParam.CFR_CUSTOMER) || string.IsNullOrWhiteSpace(_viewModel._ReportParam.CTO_CUSTOMER))
            {
                loEx.Add("", _localizer["_val5"]);
            }
            loEx.ThrowExceptionIfErrors();
            await Task.CompletedTask;
        }

        //generate param
        private async Task GenerateParam_PenaltyRpt()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                //combine data
                _viewModel._ReportParam.CREPORT_OPTION = _viewModel._DateBasedOn;
                _viewModel._ReportParam.CFR_CPERIOD = _viewModel._YearFromPeriod + _viewModel._MonthFromPeriod;
                _viewModel._ReportParam.CTO_CPERIOD = _viewModel._YearToPeriod + _viewModel._MonthToPeriod;
                _viewModel._ReportParam.LIS_BASED_ON_CUTOFF = _viewModel._DateBasedOn == "I" ? true : false;
                _viewModel._ReportParam.CCUT_OFF_DATE = _viewModel._DateBasedOn == "I" ? _viewModel._DateCutOff.ToString("yyyyMMdd") : "";
                
                //set based on display
                _viewModel._ReportParam.CREPORT_OPTION_TEXT=_viewModel._DateBasedOn=="I" ? _localizer["_rpt_option1"] : _localizer["_rpt_option2"];
                if (_viewModel._ReportParam.LIS_BASED_ON_CUTOFF)
                {
                    _viewModel._ReportParam.CBASED_ON_DISPLAY = DateTime.TryParseExact(_viewModel._ReportParam.CCUT_OFF_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out DateTime poCutOffDate)
                        ? poCutOffDate.ToString("dd MMM yyyy", CultureInfo.InvariantCulture)
                        : "";

                }
                else
                {
                    var fromPeriod = DateTime.TryParseExact(_viewModel._ReportParam.CFR_CPERIOD, "yyyyMM", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime fromDate);
                    var toPeriod = DateTime.TryParseExact(_viewModel._ReportParam.CTO_CPERIOD, "yyyyMM", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime toDate);
                    _viewModel._ReportParam.CBASED_ON_DISPLAY = fromPeriod != toPeriod ? $"{fromDate.ToString("MMM yyyy", CultureInfo.InvariantCulture)} - {toDate.ToString("MMM yyyy", CultureInfo.InvariantCulture)}" : $"{fromDate.ToString("MMM yyyy", CultureInfo.InvariantCulture)}";
                }

                //set crrency display
                switch (_viewModel._ReportParam.CCURRENCY_TYPE)
                {
                    case "T":
                        _viewModel._ReportParam.CREPORT_CURRENCY_TYPE_DISPLAY = _localizer["_radio_trans_curr"];
                        break;
                    case "L":
                        _viewModel._ReportParam.CREPORT_CURRENCY_TYPE_DISPLAY = _localizer["_radio_curr_local"];
                        break;
                    case "B":
                        _viewModel._ReportParam.CREPORT_CURRENCY_TYPE_DISPLAY = _localizer["_radio_curr_base"];
                        break;
                }
                //set globalvar
                _viewModel._ReportParam.CCOMPANY_ID = _clientHelper.CompanyId;
                _viewModel._ReportParam.CUSER_ID = _clientHelper.UserId;

                _viewModel._ReportParam.CREPORT_TYPE_DISPLAY = _viewModel._ReportParam.CREPORT_TYPE == "S" ? _localizer["_radio_Summary"] : _localizer["_radio_Detail"];
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            await Task.CompletedTask;
        }

        //print
        private async Task OnclickBtn_Print()
        {
            var loEx = new R_Exception();

            try
            {
                await Validation_PenaltyRpt();
                await GenerateParam_PenaltyRpt();

                string lcPostEndpoint = "";
                string lcGetReprotEndpoint = "";

                switch (_viewModel._ReportParam.CREPORT_TYPE)
                {
                    case "S":
                        lcPostEndpoint = "rpt/PMR02410Print/DownloadResultPrintPost";
                        lcGetReprotEndpoint = "rpt/PMR02410Print/PenaltySummary_ReportListGet";
                        break;
                    case "D":
                        lcPostEndpoint = "rpt/PMR02420Print/DownloadResultPrintPost";
                        lcGetReprotEndpoint = "rpt/PMR02420Print/PenaltyDetail_ReportListGet";
                        break;
                }
                await GenerateReport(lcPostEndpoint, lcGetReprotEndpoint, _viewModel._ReportParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task GenerateReport(string pcPostUrl, string pcGetUrl, PMR02400ParamDTO poParam)
        {
            var loEx = new R_Exception();

            try
            {
                await _reportService.GetReport(
                    PMR02400ContextConstant.DEFAULT_HTTP_NAME,
                    PMR02400ContextConstant.DEFAULT_MODULE,
                    pcPostUrl,
                    pcGetUrl,
                    poParam
                    );
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        //save as
        private async Task BeforePopup_SaveAs(R_BeforeOpenPopupEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                await Validation_PenaltyRpt();
                await GenerateParam_PenaltyRpt();
                eventArgs.Parameter = _viewModel._ReportParam;
                eventArgs.TargetPageType = typeof(SaveAsPopup);
                eventArgs.PageTitle = _localizer["_pageTitleSaveAs"];
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private void AfterPopup_SaveAs(R_AfterOpenPopupEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

    }
}
