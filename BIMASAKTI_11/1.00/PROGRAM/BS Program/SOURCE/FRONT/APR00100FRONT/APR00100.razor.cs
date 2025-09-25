using BlazorClientHelper;
using Microsoft.AspNetCore.Components;
using APR00100MODEL.View_Models;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Interfaces;
using APR00100FrontResources;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Helpers;
using Lookup_PMCOMMON.DTOs;
using Lookup_PMFRONT;
using Lookup_PMModel.ViewModel.LML00600;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSModel.ViewModel;
using Lookup_GSFRONT;
using System.Globalization;
using APR00100COMMON.DTO_s.Print;
using Lookup_APCOMMON.DTOs.APL00100;

namespace APR00100FRONT
{
    public partial class APR00100 : R_Page
    {
        private APR00100ViewModel _viewModel = new();

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
                _viewModel.ReportParam.CPROPERTY_ID = string.IsNullOrWhiteSpace(poParam) ? "" : poParam;
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
                    case "1":
                        _viewModel.ReportParam.CFROM_JRNGRP_CODE = "";
                        _viewModel.ReportParam.CFROM_JRNGRP_NAME = "";
                        _viewModel.ReportParam.CTO_JRNGRP_CODE = "";
                        _viewModel.ReportParam.CTO_JRNGRP_NAME = "";
                        break;
                    case "2":
                        _viewModel.ReportParam.CFROM_SUPPLIER_ID = "";
                        _viewModel.ReportParam.CFROM_SUPPLIER_NAME = "";
                        _viewModel.ReportParam.CTO_SUPPLIER_ID = "";
                        _viewModel.ReportParam.CTO_SUPPLIER_NAME = "";
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

        private void BeforeOpen_lookupFromTenant(R_BeforeOpenLookupEventArgs eventArgs)
        {
            GSL02900ParameterDTO loParam = new GSL02900ParameterDTO()
            {
            };
            eventArgs.Parameter = loParam;
            eventArgs.TargetPageType = typeof(GSL02900);
        }

        private async Task AfterOpen_lookupFromTenantAsync(R_AfterOpenLookupEventArgs eventArgs)
        {
            GSL02900DTO loTempResult = (GSL02900DTO)eventArgs.Result;
            if (loTempResult == null)
            {
                return;
            }
            _viewModel.ReportParam.CFROM_SUPPLIER_ID = loTempResult.CSUPPLIER_ID;
            _viewModel.ReportParam.CFROM_SUPPLIER_NAME = loTempResult.CSUPPLIER_NAME;
        }

        private async Task OnLostFocus_LookupFromTenant()
        {
            var loEx = new R_Exception();

            try
            {
                if (string.IsNullOrWhiteSpace(_viewModel.ReportParam.CFROM_SUPPLIER_ID) == false)
                {
                    GSL02900ParameterDTO loParam = new GSL02900ParameterDTO()
                    {
                        CSEARCH_TEXT = _viewModel.ReportParam.CFROM_SUPPLIER_ID
                    };

                    LookupGSL02900ViewModel loLookupViewModel = new LookupGSL02900ViewModel();

                    var loResult = await loLookupViewModel.GetSupplier(loParam);

                    if (loResult == null)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                            typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                            "_ErrLookup01"));
                        _viewModel.ReportParam.CFROM_SUPPLIER_NAME = "";
                        goto EndBlock;
                    }
                    _viewModel.ReportParam.CFROM_SUPPLIER_ID = loResult.CSUPPLIER_ID;
                    _viewModel.ReportParam.CFROM_SUPPLIER_NAME = loResult.CSUPPLIER_NAME;
                }
                else
                {
                    _viewModel.ReportParam.CFROM_SUPPLIER_NAME = "";
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
            GSL02900ParameterDTO loParam = new GSL02900ParameterDTO()
            {
            };
            eventArgs.Parameter = loParam;
            eventArgs.TargetPageType = typeof(GSL02900);
   }

        private async Task AfterOpen_lookupToTenantAsync(R_AfterOpenLookupEventArgs eventArgs)
        {
            GSL02900DTO loTempResult = (GSL02900DTO)eventArgs.Result;
            if (loTempResult == null)
            {
                return;
            }
            _viewModel.ReportParam.CTO_SUPPLIER_ID = loTempResult.CSUPPLIER_ID;
            _viewModel.ReportParam.CTO_SUPPLIER_NAME = loTempResult.CSUPPLIER_NAME;
            
        }

        private async Task OnLostFocus_LookupToTenant()
        {
            var loEx = new R_Exception();

            try
            {
                if (string.IsNullOrWhiteSpace(_viewModel.ReportParam.CTO_SUPPLIER_ID) == false)
                {
                    GSL02900ParameterDTO loParam = new GSL02900ParameterDTO()
                    {
                        CSEARCH_TEXT = _viewModel.ReportParam.CTO_SUPPLIER_ID
                    };

                    LookupGSL02900ViewModel loLookupViewModel = new LookupGSL02900ViewModel();

                    var loResult = await loLookupViewModel.GetSupplier(loParam);

                    if (loResult == null)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                            typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                            "_ErrLookup01"));
                        _viewModel.ReportParam.CTO_SUPPLIER_NAME = "";
                        goto EndBlock;
                    }
                    _viewModel.ReportParam.CTO_SUPPLIER_ID = loResult.CSUPPLIER_ID;
                    _viewModel.ReportParam.CTO_SUPPLIER_NAME = loResult.CSUPPLIER_NAME;
                }
                else
                {
                    _viewModel.ReportParam.CTO_SUPPLIER_NAME = "";
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            EndBlock:
            R_DisplayException(loEx);
        }

        private R_TextBox txtFromJrnGrp;

        private R_TextBox txtToJrnGrp;

        private void BeforeOpen_lookupFromJrnGrp(R_BeforeOpenLookupEventArgs eventArgs)
        {
            eventArgs.Parameter = new GSL00400ParameterDTO()
            {
                CCOMPANY_ID = _clientHelper.CompanyId,
                CPROPERTY_ID = _viewModel.ReportParam.CPROPERTY_ID,
                CJRNGRP_TYPE = "50",
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
                    var loParam = new GSL00400ParameterDTO // use match param as GSL's dto, send as type in search texbox
                    {
                        CCOMPANY_ID = _clientHelper.CompanyId,
                        CPROPERTY_ID = _viewModel.ReportParam.CPROPERTY_ID,
                        CJRNGRP_TYPE = "50",
                        CUSER_LOGIN_ID = _clientHelper.UserId,
                        CSEARCH_TEXT = _viewModel.ReportParam.CFROM_JRNGRP_CODE, // property that bindded to search textbox
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
                    _viewModel.ReportParam.CFROM_JRNGRP_NAME = loResult.CJRNGRP_NAME; //assign bind textbox name kalo ada
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
                CPROPERTY_ID = _viewModel.ReportParam.CPROPERTY_ID,
                CJRNGRP_TYPE = "50",
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
                    var loParam = new GSL00400ParameterDTO // use match param as GSL's dto, send as type in search texbox
                    {
                        CCOMPANY_ID = _clientHelper.CompanyId,
                        CPROPERTY_ID = _viewModel.ReportParam.CPROPERTY_ID,
                        CJRNGRP_TYPE = "50",
                        CUSER_LOGIN_ID = _clientHelper.UserId,
                        CSEARCH_TEXT = _viewModel.ReportParam.CTO_JRNGRP_CODE, // property that bindded to search textbox
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
            eventArgs.Parameter = new GSL00700ParameterDTO();
            eventArgs.TargetPageType = typeof(GSL00700);
        }

        private async Task AfterOpen_lookupFromDeptAsync(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loTempResult = (GSL00700DTO)eventArgs.Result;
            if (loTempResult != null)
            {
                _viewModel.ReportParam.CFROM_DEPT_CODE = loTempResult.CDEPT_CODE;
                _viewModel.ReportParam.CFROM_DEPT_NAME = loTempResult.CDEPT_CODE;
            }
        }

        private async Task OnLostFocus_LookupFromDept()
        {
            var loEx = new R_Exception();

            try
            {
                if (!string.IsNullOrWhiteSpace(_viewModel.ReportParam.CFROM_DEPT_CODE))
                {

                    LookupGSL00700ViewModel loLookupViewModel = new LookupGSL00700ViewModel(); //use GSL's model
                    var loParam = new GSL00700ParameterDTO // use match param as GSL's dto, send as type in search texbox
                    {
                        CSEARCH_TEXT = _viewModel.ReportParam.CFROM_DEPT_CODE, // property that bindded to search textbox
                    };
                    var loResult = await loLookupViewModel.GetDepartment(loParam); //retrive single record 

                    //show result & show name/related another fields
                    if (loResult == null)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                                typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                                "_ErrLookup01"));
                        _viewModel.ReportParam.CFROM_DEPT_NAME = ""; //kosongin bind textbox name kalo gaada
                        goto EndBlock;
                    }
                    _viewModel.ReportParam.CFROM_DEPT_CODE = loResult.CDEPT_CODE;
                    _viewModel.ReportParam.CFROM_DEPT_NAME = loResult.CDEPT_NAME; //assign bind textbox name kalo ada
                }
                else
                {
                    _viewModel.ReportParam.CFROM_DEPT_NAME = ""; //kosongin bind textbox name kalo gaada

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
            eventArgs.Parameter = new GSL00700ParameterDTO();
            eventArgs.TargetPageType = typeof(GSL00700);
        }

        private async Task AfterOpen_lookupToDeptAsync(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loTempResult = (GSL00700DTO)eventArgs.Result;
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

                    LookupGSL00700ViewModel loLookupViewModel = new LookupGSL00700ViewModel(); //use GSL's model
                    var loParam =
                        new GSL00700ParameterDTO // use match param as GSL's dto, send as type in search texbox
                        {
                            CSEARCH_TEXT =
                                _viewModel.ReportParam.CTO_DEPT_CODE, // property that bindded to search textbox
                        };

                    var loResult = await loLookupViewModel.GetDepartment(loParam); //retrive single record 

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

        private async Task OnclickBtn_Print(ReportParamDTO poParam)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                // Initialize parameters using the InitializeParameters method
                poParam = InitializeParameters(null);

                // Validation
                if (string.IsNullOrWhiteSpace(poParam.CPROPERTY_ID))
                {
                    loEx.Add("", _localizer["_val_property"]);
                    goto EndBlock;
                }
                if (_viewModel._dataBasedOn == "1" && (string.IsNullOrWhiteSpace(poParam.CFROM_SUPPLIER_ID) || string.IsNullOrWhiteSpace(poParam.CTO_SUPPLIER_ID)))
                {
                    loEx.Add("", _localizer["_val_cust"]);
                    goto EndBlock;
                }
                if (_viewModel._dataBasedOn == "2" && (string.IsNullOrWhiteSpace(poParam.CFROM_JRNGRP_CODE) || string.IsNullOrWhiteSpace(poParam.CTO_JRNGRP_CODE)))
                {
                    loEx.Add("", _localizer["_val_jrnGrp"]);
                    goto EndBlock;
                }
                if (poParam.CREMAINING_BASED_ON == "1" && _viewModel._dateBasedOnCutOff == null)
                {
                    loEx.Add("", _localizer["_val_cutoff"]);
                    goto EndBlock;
                }
                if (poParam.CREMAINING_BASED_ON == "1" && (string.IsNullOrWhiteSpace(_viewModel._MonthPeriod) || _viewModel._YearPeriod == 0))
                {
                    loEx.Add("", _localizer["_val_period"]);
                    goto EndBlock;
                }
                if (_viewModel._enableFilterDept && (string.IsNullOrWhiteSpace(poParam.CFROM_DEPT_CODE) || string.IsNullOrWhiteSpace(poParam.CTO_DEPT_CODE)))
                {
                    loEx.Add("", _localizer["_val_dept"]);
                    goto EndBlock;
                }
                if (_viewModel._enableFilterTransType && string.IsNullOrWhiteSpace(poParam.CTRANSACTION_TYPE_CODE))
                {
                    loEx.Add("", _localizer["_val_transtype"]);
                    goto EndBlock;
                }
                if (_viewModel._enableFilterSuppCtg && string.IsNullOrWhiteSpace(poParam.CSUPPLIER_CATEGORY_CODE))
                {
                    loEx.Add("", _localizer["_val_custctg"]);
                    goto EndBlock;
                }

                // Set date-based values for display
                if (poParam.CREMAINING_BASED_ON == "1")
                {
                    poParam.CDATE_BASED_ON_DISPLAY = _viewModel._dateBasedOn switch
                    {
                        "1" => $"{_localizer["_label_DateBasedOn"]} : {_localizer["_radio_CutOff"]} ({FormatDate(poParam.CCUT_OFF, "yyyyMMdd", "d MMM yyyy")})",
                        "2" => $"{_localizer["_label_DateBasedOn"]} : {_localizer["_radio_Period"]} ({FormatDate(poParam.CPERIOD, "yyyyMM", "MMM yyyy")})",
                        _ => poParam.CDATE_BASED_ON_DISPLAY
                    };
                }

                else if (_viewModel._dataBasedOn == "2")
                {
                    poParam.CDATE_BASED_ON_DISPLAY = "";
                }

                // Clean up department and transaction type codes based on filters
                poParam.CTO_DEPT_CODE = _viewModel._enableFilterDept ? poParam.CTO_DEPT_CODE ?? string.Empty : string.Empty;
                poParam.CFROM_DEPT_CODE = _viewModel._enableFilterDept ? poParam.CFROM_DEPT_CODE ?? string.Empty : string.Empty;
                poParam.CTRANSACTION_TYPE_CODE = _viewModel._enableFilterTransType ? poParam.CTRANSACTION_TYPE_CODE ?? string.Empty : string.Empty;
                poParam.CSUPPLIER_CATEGORY_CODE = _viewModel._enableFilterSuppCtg ? poParam.CSUPPLIER_CATEGORY_CODE ?? string.Empty : string.Empty;

                // Build the currency code string based on selected currencies
                var currencyCodes = new List<string>();
                if (_viewModel.ReportParam.LTRANSACTION_CURRENCY) currencyCodes.Add("1");
                if (_viewModel.ReportParam.LBASE_CURRENCY) currencyCodes.Add("2");
                if (_viewModel.ReportParam.LLOCAL_CURRENCY) currencyCodes.Add("3");

                poParam.CCURRENCY_TYPE_CODE = string.Join(",", currencyCodes);

                // Generate the report using the initialized parameters
                await Generate_Report(poParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        EndBlock:
            loEx.ThrowExceptionIfErrors();
        }



    // Method for validation logic
        private R_Exception ValidateParameters()
        {
            R_Exception loEx = new R_Exception();

            if (string.IsNullOrWhiteSpace(_viewModel.ReportParam.CPROPERTY_ID))
                loEx.Add("", _localizer["_val_property"]);

            if (_viewModel._dataBasedOn == "1" && 
                (string.IsNullOrWhiteSpace(_viewModel.ReportParam.CFROM_SUPPLIER_ID) || string.IsNullOrWhiteSpace(_viewModel.ReportParam.CTO_SUPPLIER_ID)))
                loEx.Add("", _localizer["_val_cust"]);

            if (_viewModel._dataBasedOn == "2" && 
                (string.IsNullOrWhiteSpace(_viewModel.ReportParam.CFROM_JRNGRP_CODE) || string.IsNullOrWhiteSpace(_viewModel.ReportParam.CTO_JRNGRP_CODE)))
                loEx.Add("", _localizer["_val_jrnGrp"]);

            if (_viewModel.ReportParam.CREMAINING_BASED_ON == "1" && _viewModel._dateBasedOnCutOff == null)
                loEx.Add("", _localizer["_val_cutoff"]);

            if (_viewModel.ReportParam.CREMAINING_BASED_ON == "1" && 
                (string.IsNullOrWhiteSpace(_viewModel._MonthPeriod) || _viewModel._YearPeriod == 0))
                loEx.Add("", _localizer["_val_period"]);

            if (_viewModel._enableFilterDept && 
                (string.IsNullOrWhiteSpace(_viewModel.ReportParam.CFROM_DEPT_CODE) || string.IsNullOrWhiteSpace(_viewModel.ReportParam.CTO_DEPT_CODE)))
                loEx.Add("", _localizer["_val_dept"]);

            if (_viewModel._enableFilterTransType && string.IsNullOrWhiteSpace(_viewModel.ReportParam.CTRANSACTION_TYPE_CODE))
                loEx.Add("", _localizer["_val_transtype"]);

            if (_viewModel._enableFilterSuppCtg && string.IsNullOrWhiteSpace(_viewModel.ReportParam.CSUPPLIER_CATEGORY_CODE))
                loEx.Add("", _localizer["_val_custctg"]);

            return loEx;
        }

        // Method to set display fields
        private void SetDisplayFields()
        {
            _viewModel.ReportParam.CREMAINING_BASED_ON_DISPLAY = _viewModel.ReportParam.CREMAINING_BASED_ON == "1" 
                ? _localizer["_radio_CutoffRemaining"] 
                : _localizer["_radio_LastRemaining"];

            _viewModel.ReportParam.CSORT_BY_DISPLAY = _viewModel.ReportParam.CSORT_BY == "1" 
                ? _localizer["_radio_ShortByCustomer"] 
                : _localizer["_radio_ShortByDate"];

            _viewModel.ReportParam.CREPORT_TYPE_DISPLAY = _viewModel.ReportParam.CREPORT_TYPE == "1" 
                ? _localizer["_radio_Summary"] 
                : _localizer["_radio_Detail"];

            if (_viewModel.ReportParam.CREMAINING_BASED_ON == "1")
            {
                _viewModel.ReportParam.CDATE_BASED_ON_DISPLAY = _viewModel._dateBasedOn switch
                {
                    "1" => $"{_localizer["_label_DateBasedOn"]} : {_localizer["_radio_CutOff"]} ({FormatDate(_viewModel.ReportParam.CCUT_OFF, "yyyyMMdd", "d MMM yyyy")})",
                    "2" => $"{_localizer["_label_DateBasedOn"]} : {_localizer["_radio_Period"]} ({FormatDate(_viewModel.ReportParam.CPERIOD, "yyyyMM", "MMM yyyy")})",
                    _ => ""
                };
            }
        }
    
        public string FormatDate(string date, string inputFormat, string outputFormat)
        {
            if (DateTime.TryParseExact(date, inputFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDate))
            {
                return parsedDate.ToString(outputFormat, CultureInfo.InvariantCulture);
            }
            return date; // Return original date string if parsing fails
        }




         // Method to set currency codes
        private void SetCurrencyCodes()
        {
            var currencyCodes = new List<string>();

            if (_viewModel.ReportParam.LTRANSACTION_CURRENCY) currencyCodes.Add("1");
            if (_viewModel.ReportParam.LBASE_CURRENCY) currencyCodes.Add("2");
            if (_viewModel.ReportParam.LLOCAL_CURRENCY) currencyCodes.Add("3");

            _viewModel.ReportParam.CCURRENCY_TYPE_CODE = string.Join(",", currencyCodes);
        }


    
       private ReportParamDTO InitializeParameters(ReportParamDTO loReturn)
       {
           R_Exception loEx = new R_Exception();
           ReportParamDTO loRtn = new();
           try
           {
                loRtn.CCOMPANY_ID = _clientHelper.CompanyId ?? string.Empty;
                loRtn.CUSER_ID = _clientHelper.UserId ?? string.Empty;
                loRtn.CREPORT_CULTURE = _clientHelper.ReportCulture.ToString();
                loRtn.CPROPERTY_ID = _viewModel.ReportParam.CPROPERTY_ID ?? string.Empty;
                loRtn.CPROPERTY_NAME = _viewModel.properties.Where(x => x.CPROPERTY_ID == loRtn.CPROPERTY_ID).FirstOrDefault().CPROPERTY_NAME;
                loRtn.CFROM_SUPPLIER_ID = _viewModel.ReportParam.CFROM_SUPPLIER_ID ?? string.Empty;
                loRtn.CFROM_SUPPLIER_NAME = _viewModel.ReportParam.CFROM_SUPPLIER_NAME ?? string.Empty;
                loRtn.CTO_SUPPLIER_ID = _viewModel.ReportParam.CTO_SUPPLIER_ID ?? string.Empty;
                loRtn.CTO_SUPPLIER_NAME = _viewModel.ReportParam.CTO_SUPPLIER_NAME ?? string.Empty;
                loRtn.CFROM_JRNGRP_CODE = _viewModel.ReportParam.CFROM_JRNGRP_CODE ?? string.Empty;
                loRtn.CFROM_JRNGRP_NAME = _viewModel.ReportParam.CFROM_JRNGRP_NAME ?? string.Empty;
                loRtn.CTO_JRNGRP_CODE = _viewModel.ReportParam.CTO_JRNGRP_CODE ?? string.Empty;
                loRtn.CTO_JRNGRP_NAME = _viewModel.ReportParam.CTO_JRNGRP_NAME ?? string.Empty;
                loRtn.CREMAINING_BASED_ON = _viewModel.ReportParam.CREMAINING_BASED_ON ?? string.Empty;
                loRtn.CREMAINING_BASED_ON_DISPLAY = loRtn.CREMAINING_BASED_ON == "1" ? _localizer["_radio_CutoffRemaining"] : _localizer["_radio_LastRemaining"];
                loRtn.CDATE_BASED_ON_DISPLAY = _viewModel._dateBasedOn == "1" ? _localizer["_radio_CutOff"] : _localizer["_radio_Period"];
                // Modify these lines in the InitializeParameters method
                loRtn.CCUT_OFF = loRtn.CREMAINING_BASED_ON == "1" && _viewModel._dateBasedOn == "1"
                    ? _viewModel._dateBasedOnCutOff.ToString("yyyyMMdd")
                    : "";
                // Add a safe parsing mechanism
                loRtn.DDATE_CUTOFF = string.IsNullOrEmpty(loRtn.CCUT_OFF)
                    ? DateTime.MinValue
                    : DateTime.ParseExact(loRtn.CCUT_OFF, "yyyyMMdd", new CultureInfo(_clientHelper.ReportCulture));
                loRtn.CPERIOD = loRtn.CREMAINING_BASED_ON == "1" && _viewModel._dateBasedOn == "2"
                    ? _viewModel._YearPeriod.ToString() + _viewModel._MonthPeriod
                    : "";
                loRtn.CREPORT_TYPE = _viewModel.ReportParam.CREPORT_TYPE;
                loRtn.CREPORT_TYPE_DISPLAY = _viewModel.ReportParam.CREPORT_TYPE == "1" ? _localizer["_radio_Summary"] : _localizer["_radio_Detail"];
                loRtn.CSORT_BY = _viewModel.ReportParam.CSORT_BY;
                loRtn.CSORT_BY_DISPLAY = _viewModel.ReportParam.CSORT_BY == "1" ? _localizer["_radio_ShortByCustomer"] : _localizer["_radio_ShortByDate"];
                loRtn.CCURRENCY_TYPE_CODE = _viewModel.ReportParam.CCURRENCY_TYPE_CODE ?? string.Empty;
                loRtn.CFROM_DEPT_CODE = _viewModel._enableFilterDept ? _viewModel.ReportParam.CFROM_DEPT_CODE : "";
                loRtn.CTO_DEPT_CODE = _viewModel._enableFilterDept ? _viewModel.ReportParam.CTO_DEPT_CODE : "";
                loRtn.LALLOCATION = _viewModel.ReportParam.LALLOCATION;
                loRtn.CTRANSACTION_TYPE_CODE = _viewModel._enableFilterTransType ? _viewModel.ReportParam.CTRANSACTION_TYPE_CODE : "";
                loRtn.CSUPPLIER_CATEGORY_CODE = _viewModel._enableFilterSuppCtg ? _viewModel.ReportParam.CSUPPLIER_CATEGORY_CODE : "";
                loRtn.CLANG_ID = _clientHelper.Culture?.TwoLetterISOLanguageName ?? string.Empty;
                loRtn.LIS_PRINT = true;
                loRtn.LTRANSACTION_CURRENCY = _viewModel.ReportParam.LTRANSACTION_CURRENCY;
                loRtn.LBASE_CURRENCY = _viewModel.ReportParam.LBASE_CURRENCY;
                loRtn.LLOCAL_CURRENCY = _viewModel.ReportParam.LLOCAL_CURRENCY;
                loRtn.LTRANSACTION_TYPE = _viewModel._enableFilterTransType;
                loRtn.LSUPPLIER__CATEGORY = _viewModel._enableFilterSuppCtg;
                loRtn.LALLOCATION = _viewModel.ReportParam.LALLOCATION;
                loRtn.LDEPARTMENT = _viewModel._enableFilterDept;
                loRtn.CTRANSACTION_TYPE_CODE_NAME = _viewModel._transTypeList
                    .Where(x => x.CCODE == loRtn.CTRANSACTION_TYPE_CODE)
                    .FirstOrDefault()?.CDESC ?? string.Empty;

                loRtn.CSUPPLIER_CATEGORY_CODE_NAME = loRtn.CTRANSACTION_TYPE_CODE == null
                    ? string.Empty
                    : _viewModel._suppCattegoryList
                        .Where(x => x.CCODE == loRtn.CSUPPLIER_CATEGORY_CODE)
                        .FirstOrDefault()?.CDESC ?? string.Empty;


           }
           catch (Exception ex)
           {
               loEx.Add(ex);
           }
           loEx.ThrowExceptionIfErrors();

           if (loReturn != null)
           {
               loRtn.LIS_PRINT = loReturn.LIS_PRINT;
               loRtn.CREPORT_FILENAME = loReturn.CREPORT_FILENAME;
               loRtn.CREPORT_FILETYPE = loReturn.CREPORT_FILETYPE;
           }
           return loRtn;
        }


        private async Task Generate_Report(ReportParamDTO param)
        {
            R_Exception loEx = new R_Exception();
            
            try
            {
                ReportParamDTO loParam = InitializeParameters(param);

                if (loParam.CREPORT_TYPE == "1" && loParam.CSORT_BY == "1")
                {
                    await _reportService.GetReport(
                        "R_DefaultServiceUrlAP",
                        "AP",
                        "rpt/APR00100Print/DownloadResultPrintPost",
                        "rpt/APR00100Print/UserActivitySummary_ReportListGet",
                        loParam);
                }
                else if (loParam.CREPORT_TYPE == "1" && loParam.CSORT_BY == "2")
                {
                    await _reportService.GetReport(
                        "R_DefaultServiceUrlAP",
                        "AP",
                        "rpt/APR00101Print/DownloadResultPrintPost",
                        "rpt/APR00101Print/UserActivityDetail_ReportListGet",
                        loParam);
                }
                else if (loParam.CREPORT_TYPE == "2" && loParam.CSORT_BY == "1")
                {
                    await _reportService.GetReport(
                        "R_DefaultServiceUrlAP",
                        "AP",
                        "rpt/APR00102Print/DownloadResultPrintPost",
                        "rpt/APR00102Print/UserActivityDetail_ReportListGet",
                        loParam);
                }
                else if (loParam.CREPORT_TYPE == "2" && loParam.CSORT_BY == "2")
                {
                    await _reportService.GetReport(
                        "R_DefaultServiceUrlAP",
                        "AP",
                        "rpt/APR00103Print/DownloadResultPrintPost",
                        "rpt/APR00103Print/UserActivityDetail_ReportListGet",
                        loParam);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        
        private void BeforeOpen_PopupSaveAsAsync(R_BeforeOpenPopupEventArgs eventArgs)
        {
            eventArgs.PageTitle = _localizer["_title_saveas"];
            eventArgs.Parameter = InitializeParameters(null); // Menambahkan parameter untuk dikirim ke popup
            eventArgs.TargetPageType = typeof(PopUpSaveAs);
        }
        private async Task AfterOpen_PopupSaveAs(R_AfterOpenPopupEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                if (eventArgs.Success)
                {
                    // Ambil parameter dari popup
                    var loReturn = R_FrontUtility.ConvertObjectToObject<ReportParamDTO>(eventArgs.Result);

                    // Assign kembali loReturn ke CreatePrintParam
                    var loParam = InitializeParameters(loReturn);

                    // Lanjutkan dengan proses print
                    await Generate_Report(loParam);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            await Task.CompletedTask;
        }
    }
}
