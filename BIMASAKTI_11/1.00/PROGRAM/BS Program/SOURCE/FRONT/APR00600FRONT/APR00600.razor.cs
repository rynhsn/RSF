using APR00600COMMON.Params;
using APR00600FrontResources;
using APR00600MODEL.ViewModel;
using BlazorClientHelper;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using Lookup_GSModel.ViewModel;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_BlazorFrontEnd.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.ComponentModel;
using R_BlazorFrontEnd.Enums;

namespace APR00600FRONT
{
    public partial class APR00600 : R_Page
    {
        private APR00600ViewModel _viewModel = new APR00600ViewModel();
        private R_Conductor _conductorRef;
        private bool _enabledBtn = true;

        [Inject] IClientHelper ClientHelper { get; set; }
        [Inject] private R_ILocalizer<Resources_Dummy_Class> _localizer { get; set; }
        [Inject] private R_IReport _reportService { get; set; }

        protected override async Task R_Init_From_Master(object poParam)
        {
            var loEx = new R_Exception();

            try
            {
                await _viewModel.Init();
                await _setDefaultLookup();
               
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task _valueChangedProperty(string value)
        {
            var loEx = new R_Exception();
            try
            {

                if (!string.IsNullOrEmpty(value))
                {
                    if (value == _viewModel.PoReportParam.CPROPERTY_ID) return;


                    _viewModel.PoReportParam.CPROPERTY_ID = value;
                    _viewModel.PoReportParam.CPROPERTY_NAME = _viewModel.PropertyList.Find(x => x.CPROPERTY_ID == value)?.CPROPERTY_NAME ?? string.Empty;
                    //await _viewModel.GetSystemParam();
                    //_viewModel.PoReportParam.CPROPERTY_NAME = "";
                    //_viewModel.PoReportParam.CFR_PERIOD = string.Empty;
                    //_viewModel.PoReportParam.CTO_PERIOD = string.Empty;
                    //_viewModel.PoReportParam.CCURRENCY_TYPE = "L"; 
                    //_viewModel.PoReportParam.CFILTER_BY = "SUPPLIER_ID";
                    //_viewModel.PoReportParam.CFR_CODE = string.Empty;
                    //_viewModel.CFR_CODE_NAME = string.Empty;
                    //_viewModel.PoReportParam.CTO_CODE = string.Empty;
                    //_viewModel.CTO_CODE_NAME = string.Empty;
                    //_viewModel.PoReportParam.LSUPPRESS = false;
                    await _viewModel.Init();
                    await _setDefaultLookup();
                    await validationDisplay();
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);

            }
            loEx.ThrowExceptionIfErrors();
        }

        private async Task validationDisplay()
        {

            var loEx = new R_Exception();
            try
            {
                _viewModel.LEnableBtn = true;
                if (_viewModel._IFromYear == 0)
                {
                    _viewModel.LEnableBtn = false;
                }
                if (_viewModel._CFromMonth == "")
                {
                    _viewModel.LEnableBtn = false;

                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);

            }
            loEx.ThrowExceptionIfErrors();

            
        }

        private async Task _valueChangedFilterBy(string value)
        {
            R_Exception loEx = new R_Exception();
            var loOldValue = _viewModel.PoReportParam.CFILTER_BY;
            try
            {
                _viewModel.PoReportParam.CFILTER_BY = value;
                _viewModel.LEnableToCode = value == "SUPPLIER_CATEGORY" ? false : true;
                _viewModel.PoReportParam.CFR_CODE = "";
                _viewModel.CFR_CODE_NAME = "";
                _viewModel.PoReportParam.CTO_CODE = "";
                _viewModel.CTO_CODE_NAME = "";
                await _setDefaultLookup();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _viewModel.PoReportParam.CFILTER_BY = loOldValue;
            }
            R_DisplayException(loEx);
        }

        private async Task OnLostFocus_LookupFromCustomer()
        {
            var loEx = new R_Exception();

            try
            {
                if (!string.IsNullOrWhiteSpace(_viewModel.PoReportParam.CFR_CODE))
                {
                    if (_viewModel.PoReportParam.CFILTER_BY == "SUPPLIER_ID" || _viewModel.PoReportParam.CFILTER_BY == "SUPPLIER_NAME")
                    {
                        LookupGSL02900ViewModel loLookupViewModel = new LookupGSL02900ViewModel();
                        var loParam = new GSL02900ParameterDTO
                        {
                            CSEARCH_TEXT = _viewModel.PoReportParam.CFR_CODE,
                            CSTATUS_LIST = "0",
                            CSUPPLIER_ID = ""
                        };
                        var loResult = await loLookupViewModel.GetSupplier(loParam);

                        if (loResult == null)
                        {
                            loEx.Add(R_FrontUtility.R_GetError(
                                    typeof(Lookup_PMFrontResources.Resources_Dummy_Class_LookupPM),
                                    "_ErrLookup01"));
                            _viewModel.PoReportParam.CFR_CODE = "";
                            _viewModel.CFR_CODE_NAME = "";
                            goto EndBlock;
                        }
                        _viewModel.PoReportParam.CFR_CODE = loResult.CSUPPLIER_ID ?? "";
                        _viewModel.CFR_CODE_NAME = loResult.CSUPPLIER_NAME ?? "";
                    }
                    else if (_viewModel.PoReportParam.CFILTER_BY == "SUPPLIER_CATEGORY")
                    {
                        LookupGSL01800ViewModel loLookupViewModel = new LookupGSL01800ViewModel();
                        var loParam = new GSL01800DTOParameter
                        {
                            CSEARCH_TEXT = _viewModel.PoReportParam.CFR_CODE,
                            CPROPERTY_ID = _viewModel.PoReportParam.CPROPERTY_ID,
                            CCATEGORY_TYPE = "50"
                        };
                        var loResult = await loLookupViewModel.GetCategory(loParam);

                        if (loResult == null)
                        {
                            loEx.Add(R_FrontUtility.R_GetError(
                                    typeof(Lookup_PMFrontResources.Resources_Dummy_Class_LookupPM),
                                    "_ErrLookup01"));
                            _viewModel.PoReportParam.CFR_CODE = "";
                            _viewModel.CFR_CODE_NAME = "";
                            goto EndBlock;
                        }
                        _viewModel.PoReportParam.CFR_CODE = loResult.CCATEGORY_ID ?? "";
                        _viewModel.CFR_CODE_NAME = loResult.CCATEGORY_NAME ?? "";
                    }
                    else if (_viewModel.PoReportParam.CFILTER_BY == "JOURNAL_GROUP")
                    {
                        LookupGSL00400ViewModel loLookupViewModel = new LookupGSL00400ViewModel();
                        var loParam = new GSL00400ParameterDTO
                        {
                            CSEARCH_TEXT = _viewModel.PoReportParam.CFR_CODE,
                            CPROPERTY_ID = _viewModel.PoReportParam.CPROPERTY_ID,
                            CJRNGRP_TYPE = "50"
                        };
                        var loResult = await loLookupViewModel.GetJournalGroup(loParam);

                        if (loResult == null)
                        {
                            loEx.Add(R_FrontUtility.R_GetError(
                                    typeof(Lookup_PMFrontResources.Resources_Dummy_Class_LookupPM),
                                    "_ErrLookup01"));
                            _viewModel.PoReportParam.CFR_CODE = "";
                            _viewModel.CFR_CODE_NAME = "";
                            goto EndBlock;
                        }
                        _viewModel.PoReportParam.CFR_CODE = loResult.CJRNGRP_CODE ?? "";
                        _viewModel.CFR_CODE_NAME = loResult.CJRNGRP_NAME ?? "";
                    }

                }
                else
                {
                    _viewModel.PoReportParam.CFR_CODE = "";
                    _viewModel.CFR_CODE_NAME = "";

                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        EndBlock:
            R_DisplayException(loEx);

        }

        private async Task _setDefaultLookup()
        {
            var loEx = new R_Exception();

            try
            {
                if (string.IsNullOrEmpty(_viewModel.PoReportParam.CPROPERTY_ID)) return;

                if (_viewModel.PoReportParam.CFILTER_BY == "SUPPLIER_ID" || _viewModel.PoReportParam.CFILTER_BY == "SUPPLIER_NAME")
                {
                    LookupGSL02900ViewModel loLookupViewModel = new LookupGSL02900ViewModel();

                    loLookupViewModel.SupplierParameter = new GSL02900ParameterDTO { CSTATUS_LIST = "0", CSUPPLIER_ID = "" };
                    //;
                    //var loParam = new GSL02900ParameterDTO
                    //{
                    //    CSTATUS_LIST = "0",
                    //    CSUPPLIER_ID = ""
                    //};
                    await loLookupViewModel.GetSupplierList();
                    if (loLookupViewModel.SupplierGrid.Count > 0)
                    {
                        _viewModel.PoReportParam.CFR_CODE = loLookupViewModel.SupplierGrid.FirstOrDefault()?.CSUPPLIER_ID;
                        _viewModel.CFR_CODE_NAME = loLookupViewModel.SupplierGrid
                            .Where(x => x.CSUPPLIER_ID == _viewModel.PoReportParam.CFR_CODE)
                            .Select(x => x.CSUPPLIER_NAME).FirstOrDefault() ?? string.Empty;
                        _viewModel.PoReportParam.CTO_CODE = loLookupViewModel.SupplierGrid.LastOrDefault()?.CSUPPLIER_ID;
                        _viewModel.CTO_CODE_NAME = loLookupViewModel.SupplierGrid
                            .Where(x => x.CSUPPLIER_ID == _viewModel.PoReportParam.CTO_CODE)
                            .Select(x => x.CSUPPLIER_NAME).FirstOrDefault() ?? string.Empty;
                    }
                }
                else if (_viewModel.PoReportParam.CFILTER_BY == "SUPPLIER_CATEGORY")
                {

                    LookupGSL01800ViewModel loLookupViewModel = new LookupGSL01800ViewModel();
                    var loParam = new GSL01800DTOParameter
                    {
                        CPROPERTY_ID = _viewModel.PoReportParam.CPROPERTY_ID,
                        CCATEGORY_TYPE = "50"
                    };
                    await loLookupViewModel.GetCategoryList(loParam);
                    if (loLookupViewModel.ListResult.Count > 0)
                    {
                        _viewModel.PoReportParam.CFR_CODE = loLookupViewModel.ListResult.FirstOrDefault()?.CCATEGORY_ID;
                        _viewModel.CFR_CODE_NAME = loLookupViewModel.ListResult
                            .Where(x => x.CCATEGORY_ID == _viewModel.PoReportParam.CFR_CODE)
                            .Select(x => x.CCATEGORY_NAME).FirstOrDefault() ?? string.Empty;
                        _viewModel.PoReportParam.CTO_CODE = "";
                        _viewModel.CTO_CODE_NAME = "";
                    }
                }
                else if (_viewModel.PoReportParam.CFILTER_BY == "JOURNAL_GROUP")
                {
                    LookupGSL00400ViewModel loLookupViewModel = new LookupGSL00400ViewModel();
                    var loParam = new GSL00400ParameterDTO
                    {
                        CPROPERTY_ID = _viewModel.PoReportParam.CPROPERTY_ID,
                        CJRNGRP_TYPE = "50"
                    };
                    await loLookupViewModel.GetJournalGroupList(loParam);
                    if (loLookupViewModel.JournalGroupGrid.Count > 0)
                    {
                        _viewModel.PoReportParam.CFR_CODE = loLookupViewModel.JournalGroupGrid.FirstOrDefault()?.CJRNGRP_CODE;
                        _viewModel.CFR_CODE_NAME = loLookupViewModel.JournalGroupGrid
                            .Where(x => x.CJRNGRP_CODE == _viewModel.PoReportParam.CFR_CODE)
                            .Select(x => x.CJRNGRP_NAME).FirstOrDefault() ?? string.Empty;
                        _viewModel.PoReportParam.CTO_CODE = loLookupViewModel.JournalGroupGrid.LastOrDefault()?.CJRNGRP_CODE;
                        _viewModel.CTO_CODE_NAME = loLookupViewModel.JournalGroupGrid
                            .Where(x => x.CJRNGRP_CODE == _viewModel.PoReportParam.CTO_CODE)
                            .Select(x => x.CJRNGRP_NAME).FirstOrDefault() ?? string.Empty;
                    }
                }


            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void BeforeOpen_lookupFromCustomer(R_BeforeOpenLookupEventArgs eventArgs)
        {
            if (_viewModel.PoReportParam.CFILTER_BY == "SUPPLIER_ID" || _viewModel.PoReportParam.CFILTER_BY == "SUPPLIER_NAME")
            {
                var loParam = new GSL02900ParameterDTO()
                {
                    CSTATUS_LIST = "0",
                    CSUPPLIER_ID = ""
                };
                eventArgs.Parameter = loParam;
                eventArgs.TargetPageType = typeof(GSL02900);
            }
            else if (_viewModel.PoReportParam.CFILTER_BY == "SUPPLIER_CATEGORY")
            {
                var loParam = new GSL01800DTOParameter()
                {
                    CPROPERTY_ID = _viewModel.PoReportParam.CPROPERTY_ID,
                    CCATEGORY_TYPE = "50"
                };
                eventArgs.Parameter = loParam;
                eventArgs.TargetPageType = typeof(GSL01800);
            }
            else if (_viewModel.PoReportParam.CFILTER_BY == "JOURNAL_GROUP")
            {
                var loParam = new GSL00400ParameterDTO()
                {
                    CPROPERTY_ID = _viewModel.PoReportParam.CPROPERTY_ID,
                    CJRNGRP_TYPE = "50"
                };
                eventArgs.Parameter = loParam;
                eventArgs.TargetPageType = typeof(GSL00400);
            }
        }
        private void AfterOpen_lookupFromCustomer(R_AfterOpenLookupEventArgs eventArgs)
        {

            if (_viewModel.PoReportParam.CFILTER_BY == "SUPPLIER_ID" || _viewModel.PoReportParam.CFILTER_BY == "SUPPLIER_NAME")
            {
                var loTempResult = (GSL02900DTO)eventArgs.Result;
                if (loTempResult != null)
                {
                    _viewModel.PoReportParam.CFR_CODE = loTempResult.CSUPPLIER_ID ?? "";
                    _viewModel.CFR_CODE_NAME = loTempResult.CSUPPLIER_NAME ?? "";
                }
                else
                {
                    _viewModel.PoReportParam.CFR_CODE = "";
                    _viewModel.CFR_CODE_NAME = "";
                }
            }
            else if (_viewModel.PoReportParam.CFILTER_BY == "JOURNAL_GROUP")
            {
                var loTempResult = (GSL00400DTO)eventArgs.Result;
                if (loTempResult != null)
                {
                    _viewModel.PoReportParam.CFR_CODE = loTempResult.CJRNGRP_CODE;
                    _viewModel.CFR_CODE_NAME = loTempResult.CJRNGRP_NAME;
                }
                else
                {
                    _viewModel.PoReportParam.CFR_CODE = "";
                    _viewModel.CFR_CODE_NAME = "";
                }
            }
            else if (_viewModel.PoReportParam.CFILTER_BY == "SUPPLIER_CATEGORY")
            {
                var loTempResult = (GSL01800DTO)eventArgs.Result;
                if (loTempResult != null)
                {
                    _viewModel.PoReportParam.CFR_CODE = loTempResult.CCATEGORY_ID ?? "";
                    _viewModel.CFR_CODE_NAME = loTempResult.CCATEGORY_NAME ?? "";
                }
                else
                {
                    _viewModel.PoReportParam.CFR_CODE = "";
                    _viewModel.CFR_CODE_NAME = "";
                }
            }
        }


        private async Task OnLostFocus_LookupToCustomer()
        {
            var loEx = new R_Exception();

            try
            {
                if (!string.IsNullOrWhiteSpace(_viewModel.PoReportParam.CTO_CODE))
                {
                    if (_viewModel.PoReportParam.CFILTER_BY == "SUPPLIER_ID" || _viewModel.PoReportParam.CFILTER_BY == "SUPPLIER_NAME")
                    {
                        LookupGSL02900ViewModel loLookupViewModel = new LookupGSL02900ViewModel();
                        var loParam = new GSL02900ParameterDTO
                        {
                            CSEARCH_TEXT = _viewModel.PoReportParam.CTO_CODE,
                            CSTATUS_LIST = "0",
                            CSUPPLIER_ID = ""
                        };
                        var loResult = await loLookupViewModel.GetSupplier(loParam);

                        if (loResult == null)
                        {
                            loEx.Add(R_FrontUtility.R_GetError(
                                    typeof(Lookup_PMFrontResources.Resources_Dummy_Class_LookupPM),
                                    "_ErrLookup01"));
                            _viewModel.PoReportParam.CTO_CODE = "";
                            _viewModel.CTO_CODE_NAME = "";
                            goto EndBlock;
                        }
                        _viewModel.PoReportParam.CTO_CODE = loResult.CSUPPLIER_ID ?? "";
                        _viewModel.CTO_CODE_NAME = loResult.CSUPPLIER_NAME ?? "";
                    }
                    else if (_viewModel.PoReportParam.CFILTER_BY == "SUPPLIER_CATEGORY")
                    {
                        LookupGSL01800ViewModel loLookupViewModel = new LookupGSL01800ViewModel();
                        var loParam = new GSL01800DTOParameter
                        {
                            CSEARCH_TEXT = _viewModel.PoReportParam.CTO_CODE,
                            CPROPERTY_ID = _viewModel.PoReportParam.CPROPERTY_ID,
                            CCATEGORY_TYPE = "50"
                        };
                        var loResult = await loLookupViewModel.GetCategory(loParam);

                        if (loResult == null)
                        {
                            loEx.Add(R_FrontUtility.R_GetError(
                                    typeof(Lookup_PMFrontResources.Resources_Dummy_Class_LookupPM),
                                    "_ErrLookup01"));
                            _viewModel.PoReportParam.CTO_CODE = "";
                            _viewModel.CTO_CODE_NAME = "";
                            goto EndBlock;
                        }
                        _viewModel.PoReportParam.CTO_CODE = loResult.CCATEGORY_ID ?? "";
                        _viewModel.CTO_CODE_NAME = loResult.CCATEGORY_NAME ?? "";
                    }
                    else if (_viewModel.PoReportParam.CFILTER_BY == "JOURNAL_GROUP")
                    {
                        LookupGSL00400ViewModel loLookupViewModel = new LookupGSL00400ViewModel();
                        var loParam = new GSL00400ParameterDTO
                        {
                            CSEARCH_TEXT = _viewModel.PoReportParam.CTO_CODE,
                            CPROPERTY_ID = _viewModel.PoReportParam.CPROPERTY_ID,
                            CJRNGRP_TYPE = "50"
                        };
                        var loResult = await loLookupViewModel.GetJournalGroup(loParam);

                        if (loResult == null)
                        {
                            loEx.Add(R_FrontUtility.R_GetError(
                                    typeof(Lookup_PMFrontResources.Resources_Dummy_Class_LookupPM),
                                    "_ErrLookup01"));
                            _viewModel.PoReportParam.CTO_CODE = "";
                            _viewModel.CTO_CODE_NAME = "";
                            goto EndBlock;
                        }
                        _viewModel.PoReportParam.CTO_CODE = loResult.CJRNGRP_CODE ?? "";
                        _viewModel.CTO_CODE_NAME = loResult.CJRNGRP_NAME ?? "";
                    }

                }
                else
                {
                    _viewModel.PoReportParam.CTO_CODE = "";
                    _viewModel.CFR_CODE_NAME = "";

                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        EndBlock:
            R_DisplayException(loEx);

        }


        private void BeforeOpen_lookupToCustomer(R_BeforeOpenLookupEventArgs eventArgs)
        {
            if (_viewModel.PoReportParam.CFILTER_BY == "SUPPLIER_ID" || _viewModel.PoReportParam.CFILTER_BY == "SUPPLIER_NAME")
            {
                var loParam = new GSL02900ParameterDTO()
                {
                    CSTATUS_LIST = "0",
                    CSUPPLIER_ID = ""
                };
                eventArgs.Parameter = loParam;
                eventArgs.TargetPageType = typeof(GSL02900);
            }
            else if (_viewModel.PoReportParam.CFILTER_BY == "SUPPLIER_CATEGORY")
            {
                var loParam = new GSL01800DTOParameter()
                {
                    CPROPERTY_ID = _viewModel.PoReportParam.CPROPERTY_ID,
                    CCATEGORY_TYPE = "50"
                };
                eventArgs.Parameter = loParam;
                eventArgs.TargetPageType = typeof(GSL01800);
            }
            else if (_viewModel.PoReportParam.CFILTER_BY == "JOURNAL_GROUP")
            {
                var loParam = new GSL00400ParameterDTO()
                {
                    CPROPERTY_ID = _viewModel.PoReportParam.CPROPERTY_ID,
                    CJRNGRP_TYPE = "50"
                };
                eventArgs.Parameter = loParam;
                eventArgs.TargetPageType = typeof(GSL00400);
            }
        }

        private void AfterOpen_lookupToCustomer(R_AfterOpenLookupEventArgs eventArgs)
        {

            if (_viewModel.PoReportParam.CFILTER_BY == "SUPPLIER_ID" || _viewModel.PoReportParam.CFILTER_BY == "SUPPLIER_NAME")
            {
                var loTempResult = (GSL02900DTO)eventArgs.Result;
                if (loTempResult != null)
                {
                    _viewModel.PoReportParam.CTO_CODE = loTempResult.CSUPPLIER_ID ?? "";
                    _viewModel.CTO_CODE_NAME = loTempResult.CSUPPLIER_NAME ?? "";
                }
                else
                {
                    _viewModel.PoReportParam.CTO_CODE = "";
                    _viewModel.CTO_CODE_NAME = "";
                }
            }
            else if (_viewModel.PoReportParam.CFILTER_BY == "JOURNAL_GROUP")
            {
                var loTempResult = (GSL00400DTO)eventArgs.Result;
                if (loTempResult != null)
                {
                    _viewModel.PoReportParam.CTO_CODE = loTempResult.CJRNGRP_CODE;
                    _viewModel.CTO_CODE_NAME = loTempResult.CJRNGRP_NAME;
                }
                else
                {
                    _viewModel.PoReportParam.CTO_CODE = "";
                    _viewModel.CTO_CODE_NAME = "";
                }
            }
            else if (_viewModel.PoReportParam.CFILTER_BY == "SUPPLIER_CATEGORY")
            {
                var loTempResult = (GSL01800DTO)eventArgs.Result;
                if (loTempResult != null)
                {
                    _viewModel.PoReportParam.CTO_CODE = loTempResult.CCATEGORY_ID ?? "";
                    _viewModel.CTO_CODE_NAME = loTempResult.CCATEGORY_NAME ?? "";
                }
                else
                {
                    _viewModel.PoReportParam.CTO_CODE = "";
                    _viewModel.CTO_CODE_NAME = "";
                }
            }
        }

        private void validationPrint(APR00600ReportParamDTO poParam)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                if (string.IsNullOrEmpty(poParam.CFR_PERIOD) || string.IsNullOrEmpty(poParam.CFR_CODE))
                {
                    loEx.Add("", _localizer["Validation_From_Period"] + " " + poParam.CPROPERTY_NAME);
                }
                //if (string.IsNullOrEmpty(poParam.CTO_PERIOD))
                //{
                //    loEx.Add("", _localizer["Validation_To_Period"] + " " + poParam.CPROPERTY_NAME);
                //}

                if (string.IsNullOrEmpty(poParam.CFR_CODE) && poParam.CFILTER_BY == "SUPPLIER_ID")
                {
                    loEx.Add("", _localizer["Validation_From_SUPPLIER_ID"]);
                }
                if (string.IsNullOrEmpty(poParam.CFR_CODE) && poParam.CFILTER_BY == "SUPPLIER_NAME")
                {
                    loEx.Add("", _localizer["Validation_From_SUPPLIER_NAME"]);
                }
                if (string.IsNullOrEmpty(poParam.CFR_CODE) && poParam.CFILTER_BY == "SUPPLIER_CATEGORY")
                {
                    loEx.Add("", _localizer["Validation_From_SUPPLIER_CATEGORY"]);
                }
                if (string.IsNullOrEmpty(poParam.CFR_CODE) && poParam.CFILTER_BY == "JOURNAL_GROUP")
                {
                    loEx.Add("", _localizer["Validation_From_Journal_Group"]);
                }


                if (string.IsNullOrEmpty(poParam.CTO_CODE) && poParam.CFILTER_BY == "SUPPLIER_ID")
                {
                    loEx.Add("", _localizer["Validation_To_SUPPLIER_ID"]);
                }
                if (string.IsNullOrEmpty(poParam.CTO_CODE) && poParam.CFILTER_BY == "SUPPLIER_NAME")
                {
                    loEx.Add("", _localizer["Validation_To_SUPPLIER_NAME"]);
                }
                if (string.IsNullOrEmpty(poParam.CTO_CODE) && poParam.CFILTER_BY == "JOURNAL_GROUP")
                {
                    loEx.Add("", _localizer["Validation_To_Journal_Group"]);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private APR00600ReportParamDTO InitializeParameters(APR00600ReportParamDTO loReturn)
        {
            R_Exception loEx = new R_Exception();
            APR00600ReportParamDTO loRtn = new();
            try
            {
                var periodeFromYear= _viewModel._IFromYear==0? "" : _viewModel._IFromYear.ToString();
                var periodeToYear = _viewModel._IToYear == 0 ? "" : _viewModel._IToYear.ToString();
                var prop = _viewModel.PropertyList?.FirstOrDefault(x => x.CPROPERTY_ID == loRtn.CPROPERTY_ID);
                loRtn.CCOMPANY_ID = ClientHelper.CompanyId ?? string.Empty;
                loRtn.CUSER_ID = ClientHelper.UserId ?? string.Empty;
                loRtn.CREPORT_CULTURE = ClientHelper.ReportCulture.ToString();
                loRtn.CPROPERTY_ID = _viewModel.PoReportParam.CPROPERTY_ID ?? string.Empty;
                //loRtn.CPROPERTY_NAME = prop?.CPROPERTY_NAME ?? "";
                loRtn.CPROPERTY_NAME = _viewModel.PoReportParam.CPROPERTY_NAME;
                loRtn.CFR_PERIOD = periodeFromYear + "" + _viewModel._CFromMonth.ToString();
                loRtn.CFR_PERIOD_DISPLAY= periodeFromYear + "-" + _viewModel._CFromMonth.ToString();
                loRtn.CTO_PERIOD = periodeToYear + "" + _viewModel._CToMonth.ToString();
                loRtn.CTO_PERIOD_DISPLAY = periodeToYear + "-" + _viewModel._CToMonth.ToString();
                loRtn.CCURRENCY_TYPE = _viewModel.PoReportParam.CCURRENCY_TYPE ?? string.Empty;
                loRtn.CCURRENCY_TYPE_NAME =_viewModel.TypeList.FirstOrDefault(x => x.Key == _viewModel.PoReportParam.CCURRENCY_TYPE).Value ?? "Local Currency";
                loRtn.CFILTER_BY = _viewModel.PoReportParam.CFILTER_BY ?? string.Empty;
                loRtn.CFILTER_BY_NAME= _viewModel.FilterByList.FirstOrDefault(x => x.CCODE == _viewModel.PoReportParam.CFILTER_BY).CNAME ?? "Supplier ID";
                loRtn.CFR_CODE = _viewModel.PoReportParam.CFR_CODE ?? string.Empty;
                loRtn.CFR_CODE_NAME = _viewModel.CFR_CODE_NAME ?? string.Empty;
                loRtn.CTO_CODE = _viewModel.PoReportParam.CTO_CODE ?? string.Empty;
                loRtn.CTO_CODE_NAME = _viewModel.CTO_CODE_NAME ?? string.Empty;
                loRtn.LSUPPRESS = _viewModel.PoReportParam.LSUPPRESS;
                loRtn.CLANG_ID = ClientHelper.Culture?.TwoLetterISOLanguageName ?? string.Empty;
                loRtn.LIS_PRINT = true;
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

        private void ToDisplay(R_DisplayEventArgs eventArgs)
        {
            if (_viewModel._IFromYear == 0)
            {
                _viewModel.LEnableBtn = false;
            }
            

        }

        private async Task Generate_Report(APR00600ReportParamDTO param)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                APR00600ReportParamDTO loParam = InitializeParameters(param);
                validationPrint(loParam);
                if (loParam.CFILTER_BY == "SUPPLIER_ID" || loParam.CFILTER_BY == "SUPPLIER_NAME")
                {
                    await _reportService.GetReport(
                        "R_DefaultServiceUrlAP",
                        "AP",
                        "rpt/APR00600PrintSupplierID/DownloadResultPrintPost",
                        "rpt/APR00600PrintSupplierID/SupplierId_ReportListGet",
                        loParam);
                }
                if (loParam.CFILTER_BY == "SUPPLIER_CATEGORY")
                {
                    await _reportService.GetReport(
                        "R_DefaultServiceUrlAP",
                        "AP",
                        "rpt/APR00600PrintSupplierCategory/DownloadResultPrintPost",
                        "rpt/APR00600PrintSupplierCategory/SupplierCategory_ReportListGet",
                        loParam);
                }
                if (loParam.CFILTER_BY == "JOURNAL_GROUP")
                {
                    await _reportService.GetReport(
                        "R_DefaultServiceUrlAP",
                        "AP",
                        "rpt/APR00600PrintJournalGroup/DownloadResultPrintPost",
                        "rpt/APR00600PrintJournalGroup/JournalGroup_ReportListGet",
                        loParam);
                }
                //await _reportService.GetReport(
                //        "R_DefaultServiceUrlPM",
                //        "PM",
                //        "rpt/APR00600Print/DownloadResultPrintPost",
                //        "rpt/APR00600Print/UserActivitySummary_ReportListGet",
                //        loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private async Task OnclickBtn_Print(APR00600ReportParamDTO poParam)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                poParam = InitializeParameters(null);
                await Generate_Report(poParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        EndBlock:
            loEx.ThrowExceptionIfErrors();
        }


        private void BeforeOpen_PopupSaveAsAsync(R_BeforeOpenPopupEventArgs eventArgs)
        {
            eventArgs.PageTitle = _localizer["_title_saveas"];
            eventArgs.Parameter = InitializeParameters(null);
            eventArgs.TargetPageType = typeof(APR00600PopUpSaveAs);
        }

        private async Task AfterOpen_PopupSaveAs(R_AfterOpenPopupEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                if (eventArgs.Success)
                {
                    // Ambil parameter dari popup
                    var loReturn = R_FrontUtility.ConvertObjectToObject<APR00600ReportParamDTO>(eventArgs.Result);

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
