using BlazorClientHelper;
using Microsoft.AspNetCore.Components;
using PMR02000MODEL.View_Models;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Interfaces;
using PMR02000FrontResources;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Helpers;
using Lookup_PMCOMMON.DTOs;
using Lookup_PMFRONT;
using Lookup_PMModel.ViewModel.LML00600;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSModel.ViewModel;
using Lookup_GSFRONT;
using System.Globalization;
using PMR02000COMMON.DTO_s.Print;
using PMR02000COMMON.DTO_s;

namespace PMR02000FRONT
{
    public partial class PMR02000 : R_Page
    {
        private PMR02000ViewModel _viewModel = new();

        [Inject] private IClientHelper _clientHelper { get; set; }

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

        public async Task ComboboxPropertyValueChanged(string poParam)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                _viewModel.ReportParam.CPROPERTY_ID = poParam ?? "";
                _viewModel.ReportParam.CFROM_CUSTOMER_ID = "";
                _viewModel.ReportParam.CFROM_CUSTOMER_NAME = "";
                _viewModel.ReportParam.CTO_CUSTOMER_ID = "";
                _viewModel.ReportParam.CTO_CUSTOMER_NAME = "";
                _viewModel.ReportParam.CFROM_JRNGRP_CODE = "";
                _viewModel.ReportParam.CFROM_JRNGRP_NAME = "";
                _viewModel.ReportParam.CTO_JRNGRP_CODE = "";
                _viewModel.ReportParam.CTO_JRNGRP_NAME = "";
                _viewModel.ReportParam.CFR_DEPT_CODE = "";
                _viewModel.ReportParam.CFR_DEPT_NAME = "";
                _viewModel.ReportParam.CTO_DEPT_CODE = "";
                _viewModel.ReportParam.CTO_DEPT_NAME = "";
                _viewModel.ReportParam.CTENANT_CATEGORY_ID = "";
                await _viewModel.GetCategoryTypeAsync(new CategoryTypeParamDTO()
                    { CPROPERTY_ID = _viewModel.ReportParam.CPROPERTY_ID });
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        public async void CheckBoxOnChanged_EnableFilterCustCtg()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                if (_viewModel._enableFilterCustCtg)
                {
                    await _viewModel.GetCategoryTypeAsync(new CategoryTypeParamDTO()
                        { CPROPERTY_ID = _viewModel.ReportParam.CPROPERTY_ID });
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private R_TextBox txtFromCustomer;

        private R_TextBox txtToCustomer;

        private void OnLostFocus_DataBasedOn()
        {
            var loEx = new R_Exception();
            try
            {
                switch (_viewModel._dataBasedOn)
                {
                    case "C":
                        _viewModel.ReportParam.CFROM_JRNGRP_CODE = "";
                        _viewModel.ReportParam.CFROM_JRNGRP_NAME = "";
                        _viewModel.ReportParam.CTO_JRNGRP_CODE = "";
                        _viewModel.ReportParam.CTO_JRNGRP_NAME = "";
                        break;

                    case "J":
                        _viewModel.ReportParam.CFROM_CUSTOMER_ID = "";
                        _viewModel.ReportParam.CFROM_CUSTOMER_NAME = "";
                        _viewModel.ReportParam.CTO_CUSTOMER_ID = "";
                        _viewModel.ReportParam.CTO_CUSTOMER_NAME = "";
                        break;

                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private async Task OnclickBtn_Print()
        {
            R_Exception loEx = new();
            try
            {
                //validation
                Validation();

                //retrive param
                var loParam = GenerateParam();

                //generate report
                await Generate_Report(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void Validation()
        {
            R_Exception loEx = new();
            try
            {
                if (string.IsNullOrWhiteSpace(_viewModel.ReportParam.CPROPERTY_ID))
                {
                    loEx.Add("", _localizer["_val_property"]);
                }

                if (_viewModel._dataBasedOn == "C" &&
                    (string.IsNullOrWhiteSpace(_viewModel.ReportParam.CFROM_CUSTOMER_ID) ||
                     string.IsNullOrWhiteSpace(_viewModel.ReportParam.CTO_CUSTOMER_ID)))
                {
                    loEx.Add("", _localizer["_val_cust"]);
                }

                if (_viewModel._dataBasedOn == "J" &&
                    (string.IsNullOrWhiteSpace(_viewModel.ReportParam.CFROM_JRNGRP_CODE) ||
                     string.IsNullOrWhiteSpace(_viewModel.ReportParam.CTO_JRNGRP_CODE)))
                {
                    loEx.Add("", _localizer["_val_jrnGrp"]);
                }

                if (_viewModel.ReportParam.CREMAINING_BASED_ON == "C" && _viewModel._dateCutOff == null)
                {
                    loEx.Add("", _localizer["_val_cutoff"]);
                }

                if ((string.IsNullOrWhiteSpace(_viewModel._MonthPeriod) || _viewModel._YearPeriod == 0))
                {
                    loEx.Add("", _localizer["_val_period"]);
                }

                if (_viewModel._enableFilterDept && (string.IsNullOrWhiteSpace(_viewModel.ReportParam.CFR_DEPT_CODE) ||
                                                     string.IsNullOrWhiteSpace(_viewModel.ReportParam.CTO_DEPT_CODE)))
                {
                    loEx.Add("", _localizer["_val_dept"]);
                }

                if (_viewModel._enableFilterTransType && string.IsNullOrWhiteSpace(_viewModel.ReportParam.CTRANS_CODE))
                {
                    loEx.Add("", _localizer["_val_transtype"]);
                }

                if (_viewModel._enableFilterCustCtg &&
                    string.IsNullOrWhiteSpace(_viewModel.ReportParam.CTENANT_CATEGORY_ID))
                {
                    loEx.Add("", _localizer["_val_custctg"]);
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

        private ReportParamDTO GenerateParam()
        {
            R_Exception loEx = new();
            ReportParamDTO loRtn = new();
            try
            {
                loRtn.CCOMPANY_ID = _clientHelper.CompanyId;
                loRtn.CUSER_ID = _clientHelper.UserId;
                loRtn.CREPORT_CULTURE = _clientHelper.ReportCulture.ToString();
                loRtn.CPROPERTY_ID = _viewModel.ReportParam.CPROPERTY_ID;
                loRtn.CPROPERTY_NAME = _viewModel.properties.FirstOrDefault(x => x.CPROPERTY_ID == loRtn.CPROPERTY_ID)
                    .CPROPERTY_NAME ?? "";
                loRtn.CLANGUAGE_ID = _clientHelper.Culture.TwoLetterISOLanguageName;
                loRtn.CBASED_ON = _viewModel._dataBasedOn;
                loRtn.CCURRENCY_TYPE_CODE = _viewModel.ReportParam.CCURRENCY_TYPE_CODE;
                loRtn.CFROM_CUSTOMER_ID = _viewModel.ReportParam.CFROM_CUSTOMER_ID;
                loRtn.CTO_CUSTOMER_ID = _viewModel.ReportParam.CTO_CUSTOMER_ID;
                loRtn.CFROM_JRNGRP_CODE = _viewModel.ReportParam.CFROM_JRNGRP_CODE;
                loRtn.CTO_JRNGRP_CODE = _viewModel.ReportParam.CTO_JRNGRP_CODE;
                loRtn.CPERIOD = _viewModel._YearPeriod.ToString() + _viewModel._MonthPeriod ?? "";
                loRtn.CREMAINING_BASED_ON = _viewModel.ReportParam.CREMAINING_BASED_ON;
                loRtn.CCUT_OFF_DATE =
                    loRtn.CREMAINING_BASED_ON == "C" ? _viewModel._dateCutOff.ToString("yyyyMMdd") : "";
                loRtn.CFR_DEPT_CODE = _viewModel.ReportParam.CFR_DEPT_CODE;
                loRtn.CTO_DEPT_CODE = _viewModel.ReportParam.CTO_DEPT_CODE;
                loRtn.CTRANS_CODE = _viewModel.ReportParam.CTRANS_CODE;
                loRtn.CTENANT_CATEGORY_ID = _viewModel.ReportParam.CTENANT_CATEGORY_ID;
                loRtn.CSORT_BY = _viewModel.ReportParam.CSORT_BY;
                loRtn.CDATA_BASED_ON_DISPLAY = _viewModel._dataBasedOn == "C"
                    ? _localizer["_radio_Customer"]
                    : _localizer["_radio_JournalGroup"];
                loRtn.CREMAINING_BASED_ON_DISPLAY = loRtn.CREMAINING_BASED_ON == "C"
                    ? _localizer["_radio_CutoffRemaining"]
                    : _localizer["_radio_LastRemaining"];
                loRtn.CREPORT_TYPE = _viewModel.ReportParam.CREPORT_TYPE;
                loRtn.CREPORT_TYPE_DISPLAY = _viewModel.ReportParam.CREPORT_TYPE == "S"
                    ? _localizer["_radio_Summary"]
                    : _localizer["_radio_Detail"];
                if (!string.IsNullOrWhiteSpace(loRtn.CCUT_OFF_DATE))
                {
                    loRtn.DDATE_CUTOFF = DateTime.ParseExact(loRtn.CCUT_OFF_DATE, "yyyyMMdd",
                        new CultureInfo(_clientHelper.ReportCulture));
                }

                loRtn.CTO_DEPT_CODE = _viewModel._enableFilterDept ? loRtn.CTO_DEPT_CODE : "";
                loRtn.CFR_DEPT_CODE = _viewModel._enableFilterDept ? loRtn.CFR_DEPT_CODE : "";
                loRtn.CTRANS_CODE = _viewModel._enableFilterTransType ? loRtn.CTRANS_CODE : "";
                loRtn.CTENANT_CATEGORY_ID = _viewModel._enableFilterCustCtg ? loRtn.CTENANT_CATEGORY_ID : "";
                loRtn.IS_TRANS_CURRENCY = _viewModel.ReportParam.CCURRENCY_TYPE_CODE == "T";
                loRtn.IS_BASE_CURRENCY = _viewModel.ReportParam.CCURRENCY_TYPE_CODE == "B";
                loRtn.IS_LOCAL_CURRENCY = _viewModel.ReportParam.CCURRENCY_TYPE_CODE == "L";
                loRtn.IS_DEPT_FILTER_ENABLED = _viewModel._enableFilterDept;
                loRtn.IS_TRANSTYPE_FILTER_ENABLED = _viewModel._enableFilterTransType;
                loRtn.CTRANSTYPE_FILTER_DISPLAY = loRtn.IS_TRANSTYPE_FILTER_ENABLED
                    ? _viewModel._transTypeList.FirstOrDefault(x => x.CTRANS_CODE == _viewModel.ReportParam.CTRANS_CODE)
                        .CTRANS_CODE_NAME
                    : "";
                loRtn.IS_CUSTCTG_FILTER_ENABLED = _viewModel._enableFilterCustCtg;
                loRtn.CUSTCTG_FILTER_DISPLAY = loRtn.IS_CUSTCTG_FILTER_ENABLED
                    ? _viewModel._categoryTypeList.FirstOrDefault(x => x.CCATEGORY_ID == loRtn.CTENANT_CATEGORY_ID)
                        .CCATEGORY_NAME
                    : "";
                loRtn.LIS_PRINT = _viewModel.ReportParam.LIS_PRINT;
                loRtn.CREPORT_FILENAME = _viewModel.ReportParam.CREPORT_FILENAME;
                loRtn.CREPORT_FILETYPE = _viewModel.ReportParam.CREPORT_FILETYPE;
                loRtn.CJRNGRP_DISPLAY =
                    _viewModel.ReportParam.CFROM_JRNGRP_NAME != _viewModel.ReportParam.CTO_JRNGRP_NAME
                        ? $"{_viewModel.ReportParam.CFROM_JRNGRP_NAME} ({_viewModel.ReportParam.CFROM_JRNGRP_CODE}) - {_viewModel.ReportParam.CTO_JRNGRP_NAME} ({_viewModel.ReportParam.CTO_JRNGRP_CODE})"
                        : $"{_viewModel.ReportParam.CFROM_JRNGRP_NAME} ({_viewModel.ReportParam.CFROM_JRNGRP_CODE})";
                loRtn.CCUSTOMER_DISPLAY =
                    _viewModel.ReportParam.CFROM_CUSTOMER_ID != _viewModel.ReportParam.CTO_CUSTOMER_ID
                        ? $"{_viewModel.ReportParam.CFROM_CUSTOMER_NAME} ({_viewModel.ReportParam.CFROM_CUSTOMER_ID}) - {_viewModel.ReportParam.CTO_CUSTOMER_NAME} ({_viewModel.ReportParam.CTO_CUSTOMER_ID})"
                        : $"{_viewModel.ReportParam.CFROM_CUSTOMER_NAME} ({_viewModel.ReportParam.CFROM_CUSTOMER_ID})";
                loRtn.CDEPT_DISPLAY = _viewModel.ReportParam.CFR_DEPT_CODE != _viewModel.ReportParam.CTO_DEPT_CODE
                    ? $"{_viewModel.ReportParam.CFR_DEPT_NAME} ({_viewModel.ReportParam.CFR_DEPT_CODE}) - {_viewModel.ReportParam.CTO_DEPT_NAME} ({_viewModel.ReportParam.CTO_DEPT_CODE})"
                    : $"{_viewModel.ReportParam.CFR_DEPT_NAME} ({_viewModel.ReportParam.CFR_DEPT_CODE})";
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }

        private async Task Generate_Report(ReportParamDTO poParam)
        {
            R_Exception loEx = new();
            try
            {
                if (poParam.CREPORT_TYPE == "S")
                {
                    await _reportService.GetReport(
                        "R_DefaultServiceUrlPM",
                        "PM",
                        "rpt/PMR02000Print/DownloadResultPrintPost",
                        "rpt/PMR02000Print/UserActivitySummary_ReportListGet",
                        poParam);
                }
                else if (poParam.CREPORT_TYPE == "D")
                {
                    await _reportService.GetReport(
                        "R_DefaultServiceUrlPM",
                        "PM",
                        "rpt/PMR02001Print/DownloadResultPrintPost",
                        "rpt/PMR02001Print/UserActivityDetail_ReportListGet",
                        poParam);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        #region Save as popup

        private void BeforeOpen_PopupSaveAsAsync(R_BeforeOpenPopupEventArgs eventArgs)
        {
            R_Exception loEx = new();
            try
            {
                Validation();
                eventArgs.PageTitle = _localizer["_title_saveas"];
                eventArgs.TargetPageType = typeof(PMR02001);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task AfterOpen_PopupSaveAs(R_AfterOpenPopupEventArgs eventArgs)
        {
            R_Exception loEx = new();
            try
            {
                string lsReportType = (string)eventArgs.Result;
                if (!string.IsNullOrWhiteSpace(lsReportType))
                {
                    string[] lcResultSaveAs = lsReportType.Split(',');
                    var loPrintParam = GenerateParam();
                    loPrintParam.LIS_PRINT = false;
                    loPrintParam.CREPORT_FILENAME = lcResultSaveAs[0];
                    loPrintParam.CREPORT_FILETYPE = lcResultSaveAs[1];
                    await Generate_Report(loPrintParam);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        #endregion Save as popup

        #region lookup & lostfocus

        private void BeforeOpen_lookupFromTenant(R_BeforeOpenLookupEventArgs eventArgs)
        {
            eventArgs.Parameter = new LML00600ParameterDTO()
            {
                CCOMPANY_ID = _clientHelper.CompanyId, CPROPERTY_ID = _viewModel.ReportParam.CPROPERTY_ID,
                CCUSTOMER_TYPE = "01", CUSER_ID = _clientHelper.UserId, CSEARCH_TEXT = ""
            };
            eventArgs.TargetPageType = typeof(LML00600);
        }

        private void AfterOpen_lookupFromTenant(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loTempResult = (LML00600DTO)eventArgs.Result;
            if (loTempResult != null)
            {
                _viewModel.ReportParam.CFROM_CUSTOMER_ID = loTempResult.CTENANT_ID ?? "";
                _viewModel.ReportParam.CFROM_CUSTOMER_NAME = loTempResult.CTENANT_NAME ?? "";
            }
        }

        private async Task OnLostFocus_LookupFromTenant()
        {
            var loEx = new R_Exception();

            try
            {
                if (!string.IsNullOrWhiteSpace(_viewModel.ReportParam.CFROM_CUSTOMER_ID))
                {
                    LookupLML00600ViewModel loLookupViewModel = new(); //use GSL's model
                    var loResult = await loLookupViewModel.GetTenant(new LML00600ParameterDTO
                    {
                        CCOMPANY_ID = _clientHelper.CompanyId,
                        CPROPERTY_ID = _viewModel.ReportParam.CPROPERTY_ID ?? "",
                        CCUSTOMER_TYPE = "01",
                        CUSER_ID = _clientHelper.UserId,
                        CSEARCH_TEXT = _viewModel.ReportParam.CFROM_CUSTOMER_ID,
                    }); //retrive single record
                    if (loResult == null)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                            typeof(Lookup_PMFrontResources.Resources_Dummy_Class_LookupPM),
                            "_ErrLookup01"));
                        _viewModel.ReportParam.CFROM_CUSTOMER_NAME = ""; //kosongin bind textbox name kalo gaada
                        //await txtFromCustomer.FocusAsync();
                        goto EndBlock;
                    }

                    _viewModel.ReportParam.CFROM_CUSTOMER_ID = loResult.CTENANT_ID ?? "";
                    _viewModel.ReportParam.CFROM_CUSTOMER_NAME =
                        loResult.CTENANT_NAME ?? ""; //assign bind textbox name kalo ada
                }
                else
                {
                    _viewModel.ReportParam.CFROM_CUSTOMER_NAME = ""; //kosongin bind textbox name kalo gaada
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
            {
                CCOMPANY_ID = _clientHelper.CompanyId, CPROPERTY_ID = _viewModel.ReportParam.CPROPERTY_ID,
                CCUSTOMER_TYPE = "01", CUSER_ID = _clientHelper.UserId, CSEARCH_TEXT = ""
            };
            eventArgs.TargetPageType = typeof(LML00600);
        }

        private async Task AfterOpen_lookupToTenantAsync(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loTempResult = (LML00600DTO)eventArgs.Result;
            if (loTempResult != null)
            {
                _viewModel.ReportParam.CTO_CUSTOMER_ID = loTempResult.CTENANT_ID;
                _viewModel.ReportParam.CTO_CUSTOMER_NAME = loTempResult.CTENANT_NAME;
            }
        }

        private async Task OnLostFocus_LookupToTenant()
        {
            var loEx = new R_Exception();
            try
            {
                if (!string.IsNullOrWhiteSpace(_viewModel.ReportParam.CTO_CUSTOMER_ID))
                {
                    LookupLML00600ViewModel loLookupViewModel = new LookupLML00600ViewModel(); //use GSL's model
                    var loParam =
                        new LML00600ParameterDTO // use match param as GSL's dto, send as type in search texbox
                        {
                            CCOMPANY_ID = _clientHelper.CompanyId,
                            CPROPERTY_ID = _viewModel.ReportParam.CPROPERTY_ID ?? "",
                            CCUSTOMER_TYPE = "01",
                            CUSER_ID = _clientHelper.UserId,
                            CSEARCH_TEXT =
                                _viewModel.ReportParam.CTO_CUSTOMER_ID, // property that bindded to search textbox
                        };
                    var loResult = await loLookupViewModel.GetTenant(loParam); //retrive single record

                    //show result & show name/related another fields
                    if (loResult == null)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                            typeof(Lookup_PMFrontResources.Resources_Dummy_Class_LookupPM),
                            "_ErrLookup01"));
                        _viewModel.ReportParam.CTO_CUSTOMER_NAME = ""; //kosongin bind textbox name kalo gaada
                        //await txtToCustomer.FocusAsync();
                        goto EndBlock;
                    }

                    _viewModel.ReportParam.CTO_CUSTOMER_ID = loResult.CTENANT_ID;
                    _viewModel.ReportParam.CTO_CUSTOMER_NAME =
                        loResult.CTENANT_NAME; //assign bind textbox name kalo ada
                }
                else
                {
                    _viewModel.ReportParam.CTO_CUSTOMER_NAME = ""; //kosongin bind textbox name kalo gaada
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            EndBlock:
            R_DisplayException(loEx);
        }

        private void BeforeOpen_lookupFromJrnGrp(R_BeforeOpenLookupEventArgs eventArgs)
        {
            eventArgs.Parameter = new GSL00400ParameterDTO()
            {
                CCOMPANY_ID = _clientHelper.CompanyId,
                CPROPERTY_ID = _viewModel.ReportParam.CPROPERTY_ID ?? "",
                CJRNGRP_TYPE = "20",
                CUSER_LOGIN_ID = _clientHelper.UserId,
                CSEARCH_TEXT = ""
            };
            eventArgs.TargetPageType = typeof(GSL00400);
        }

        private async Task AfterOpen_lookupFromJrnGrpAsync(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loTempResult = (GSL00400DTO)eventArgs.Result;
            if (loTempResult != null)
            {
                _viewModel.ReportParam.CFROM_JRNGRP_CODE = loTempResult.CJRNGRP_CODE;
                _viewModel.ReportParam.CFROM_JRNGRP_NAME = loTempResult.CJRNGRP_NAME;
            }
        }

        private async Task OnLostFocus_LookupFromJrnGrp()
        {
            var loEx = new R_Exception();

            try
            {
                if (!string.IsNullOrWhiteSpace(_viewModel.ReportParam.CFROM_JRNGRP_CODE))
                {
                    LookupGSL00400ViewModel loLookupViewModel = new LookupGSL00400ViewModel(); //use GSL's model
                    var loParam =
                        new GSL00400ParameterDTO // use match param as GSL's dto, send as type in search texbox
                        {
                            CCOMPANY_ID = _clientHelper.CompanyId,
                            CPROPERTY_ID = _viewModel.ReportParam.CPROPERTY_ID ?? "",
                            CJRNGRP_TYPE = "20",
                            CUSER_LOGIN_ID = _clientHelper.UserId,
                            CSEARCH_TEXT =
                                _viewModel.ReportParam.CFROM_JRNGRP_CODE, // property that bindded to search textbox
                        };
                    var loResult = await loLookupViewModel.GetJournalGroup(loParam); //retrive single record

                    //show result & show name/related another fields
                    if (loResult == null)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                            typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                            "_ErrLookup01"));
                        _viewModel.ReportParam.CFROM_JRNGRP_NAME = ""; //kosongin bind textbox name kalo gaada
                        goto EndBlock;
                    }

                    _viewModel.ReportParam.CFROM_JRNGRP_CODE = loResult.CJRNGRP_CODE;
                    _viewModel.ReportParam.CFROM_JRNGRP_NAME =
                        loResult.CJRNGRP_NAME; //assign bind textbox name kalo ada
                }
                else
                {
                    _viewModel.ReportParam.CFROM_JRNGRP_NAME = ""; //kosongin bind textbox name kalo gaada
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            EndBlock:
            R_DisplayException(loEx);
        }

        private void BeforeOpen_lookupToJrnGrp(R_BeforeOpenLookupEventArgs eventArgs)
        {
            eventArgs.Parameter = new GSL00400ParameterDTO()
            {
                CCOMPANY_ID = _clientHelper.CompanyId,
                CPROPERTY_ID = _viewModel.ReportParam.CPROPERTY_ID ?? "",
                CJRNGRP_TYPE = "20",
                CUSER_LOGIN_ID = _clientHelper.UserId,
                CSEARCH_TEXT = ""
            };
            eventArgs.TargetPageType = typeof(GSL00400);
        }

        private async Task AfterOpen_lookupToJrnGrpAsync(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loTempResult = (GSL00400DTO)eventArgs.Result;
            if (loTempResult != null)
            {
                _viewModel.ReportParam.CTO_JRNGRP_CODE = loTempResult.CJRNGRP_CODE;
                _viewModel.ReportParam.CTO_JRNGRP_NAME = loTempResult.CJRNGRP_NAME;
            }
        }

        private async Task OnLostFocus_LookupToJrnGrp()
        {
            var loEx = new R_Exception();

            try
            {
                if (!string.IsNullOrWhiteSpace(_viewModel.ReportParam.CTO_JRNGRP_CODE))
                {
                    LookupGSL00400ViewModel loLookupViewModel = new LookupGSL00400ViewModel(); //use GSL's model
                    var loParam =
                        new GSL00400ParameterDTO // use match param as GSL's dto, send as type in search texbox
                        {
                            CCOMPANY_ID = _clientHelper.CompanyId,
                            CPROPERTY_ID = _viewModel.ReportParam.CPROPERTY_ID ?? "",
                            CJRNGRP_TYPE = "20",
                            CUSER_LOGIN_ID = _clientHelper.UserId,
                            CSEARCH_TEXT =
                                _viewModel.ReportParam.CTO_JRNGRP_CODE, // property that bindded to search textbox
                        };
                    var loResult = await loLookupViewModel.GetJournalGroup(loParam); //retrive single record

                    //show result & show name/related another fields
                    if (loResult == null)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                            typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                            "_ErrLookup01"));
                        _viewModel.ReportParam.CTO_JRNGRP_NAME = ""; //kosongin bind textbox name kalo gaada
                        goto EndBlock;
                    }

                    _viewModel.ReportParam.CTO_JRNGRP_CODE = loResult.CJRNGRP_CODE;
                    _viewModel.ReportParam.CTO_JRNGRP_NAME = loResult.CJRNGRP_NAME; //assign bind textbox name kalo ada
                }
                else
                {
                    _viewModel.ReportParam.CTO_JRNGRP_NAME = ""; //kosongin bind textbox name kalo gaada
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            EndBlock:
            R_DisplayException(loEx);
        }

        private void BeforeOpen_lookupFromDept(R_BeforeOpenLookupEventArgs eventArgs)
        {
            eventArgs.Parameter = new GSL00710ParameterDTO()
            {
                CPROPERTY_ID = _viewModel.ReportParam.CPROPERTY_ID ?? "",
            };
            eventArgs.TargetPageType = typeof(GSL00710);
        }

        private async Task AfterOpen_lookupFromDeptAsync(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loTempResult = (GSL00710DTO)eventArgs.Result;
            if (loTempResult != null)
            {
                _viewModel.ReportParam.CFR_DEPT_CODE = loTempResult.CDEPT_CODE;
                _viewModel.ReportParam.CFR_DEPT_NAME = loTempResult.CDEPT_CODE;
            }
        }

        private async Task OnLostFocus_LookupFromDept()
        {
            var loEx = new R_Exception();

            try
            {
                if (!string.IsNullOrWhiteSpace(_viewModel.ReportParam.CFR_DEPT_CODE))
                {
                    LookupGSL00710ViewModel loLookupViewModel = new(); //use GSL's model
                    var loResult = await loLookupViewModel.GetDepartmentProperty(
                        new GSL00710ParameterDTO // use match param as GSL's dto, send as type in search texbox
                        {
                            CPROPERTY_ID = _viewModel.ReportParam.CPROPERTY_ID ?? "",
                            CSEARCH_TEXT = _viewModel.ReportParam.CFR_DEPT_CODE, // property that bindded to search textbox
                        }); //retrive single record

                    //show result & show name/related another fields
                    if (loResult == null)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                            typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                            "_ErrLookup01"));
                        _viewModel.ReportParam.CFR_DEPT_NAME = ""; //kosongin bind textbox name kalo gaada
                        goto EndBlock;
                    }

                    _viewModel.ReportParam.CFR_DEPT_CODE = loResult.CDEPT_CODE;
                    _viewModel.ReportParam.CFR_DEPT_NAME = loResult.CDEPT_NAME; //assign bind textbox name kalo ada
                }
                else
                {
                    _viewModel.ReportParam.CFR_DEPT_NAME = ""; //kosongin bind textbox name kalo gaada
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            EndBlock:
            R_DisplayException(loEx);
        }

        private void BeforeOpen_lookupToDept(R_BeforeOpenLookupEventArgs eventArgs)
        {
            eventArgs.Parameter = new GSL00710ParameterDTO()
            {
                CPROPERTY_ID = _viewModel.ReportParam.CPROPERTY_ID ?? ""
            };
            eventArgs.TargetPageType = typeof(GSL00710);
        }

        private async Task AfterOpen_lookupToDeptAsync(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loTempResult = (GSL00710DTO)eventArgs.Result;
            if (loTempResult != null)
            {
                _viewModel.ReportParam.CTO_DEPT_CODE = loTempResult.CDEPT_CODE;
                _viewModel.ReportParam.CTO_DEPT_NAME = loTempResult.CDEPT_CODE;
            }
        }

        private async Task OnLostFocus_LookupToDept()
        {
            var loEx = new R_Exception();

            try
            {
                if (!string.IsNullOrWhiteSpace(_viewModel.ReportParam.CTO_DEPT_CODE))
                {
                    LookupGSL00710ViewModel loLookupViewModel = new(); //use GSL's model

                    var loResult = await loLookupViewModel.GetDepartmentProperty(
                        new GSL00710ParameterDTO // use match param as GSL's dto, send as type in search texbox
                        {
                            CPROPERTY_ID = _viewModel.ReportParam.CPROPERTY_ID ?? "",
                            CSEARCH_TEXT =
                                _viewModel.ReportParam.CTO_DEPT_CODE, // property that bindded to search textbox
                        }); //retrive single record

                    //show result & show name/related another fields
                    if (loResult == null)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                            typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                            "_ErrLookup01"));
                        _viewModel.ReportParam.CTO_DEPT_NAME = ""; //kosongin bind textbox name kalo gaada
                        goto EndBlock;
                    }

                    _viewModel.ReportParam.CTO_DEPT_CODE = loResult.CDEPT_CODE;
                    _viewModel.ReportParam.CTO_DEPT_NAME = loResult.CDEPT_NAME; //assign bind textbox name kalo ada
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            EndBlock:
            R_DisplayException(loEx);
        }

        #endregion lookup & lostfocus
    }
}