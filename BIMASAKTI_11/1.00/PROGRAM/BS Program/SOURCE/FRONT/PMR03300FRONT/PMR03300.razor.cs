using BlazorClientHelper;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using Lookup_GSModel.ViewModel;
using Lookup_PMCOMMON.DTOs;
using Lookup_PMCOMMON.DTOs.LML02000;
using Lookup_PMFRONT;
using Lookup_PMModel.ViewModel.LML00600;
using Lookup_PMModel.ViewModel.LML02000;
using Microsoft.AspNetCore.Components;
using PMR03300COMMON.Params;
using PMR03300FrontResources;
using PMR03300MODEL.ViewModel;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_BlazorFrontEnd.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMR03300FRONT
{
    public partial class PMR03300 :R_Page
    {
        private PMR03300ViewModel _viewModel = new PMR03300ViewModel();
        private R_ConductorGrid _conductorGrid;
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
                    await _viewModel.GetSystemParam();
                    _viewModel.PoReportParam.CPROPERTY_NAME = "";
                    _viewModel.PoReportParam.CFR_PERIOD = string.Empty;
                    _viewModel.PoReportParam.CTO_PERIOD = string.Empty;
                    _viewModel.PoReportParam.CCURRENCY_TYPE = string.Empty;
                    _viewModel.PoReportParam.CFILTER_BY = string.Empty;
                    _viewModel.PoReportParam.CFR_CODE = string.Empty;
                    _viewModel.CFR_CODE_NAME = string.Empty;
                    _viewModel.PoReportParam.CTO_CODE = string.Empty;
                    _viewModel.CTO_CODE_NAME = string.Empty;
                    _viewModel.PoReportParam.LSUPPRESS = false;
                    await _setDefaultLookup();
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
                _viewModel.LEnableToCode= value == "CUSTOMER_CATEGORY" ? false : true;
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
                    if(_viewModel.PoReportParam.CFILTER_BY == "CUSTOMER_ID" || _viewModel.PoReportParam.CFILTER_BY == "CUSTOMER_NAME")
                    {
                        LookupLML00600ViewModel loLookupViewModel = new LookupLML00600ViewModel();
                        var loParam = new LML00600ParameterDTO
                        {
                            CSEARCH_TEXT = _viewModel.PoReportParam.CFR_CODE,
                            CPROPERTY_ID = _viewModel.PoReportParam.CPROPERTY_ID,
                            CCUSTOMER_TYPE="01"
                        };
                        var loResult = await loLookupViewModel.GetTenant(loParam);

                        if (loResult == null)
                        {
                            loEx.Add(R_FrontUtility.R_GetError(
                                    typeof(Lookup_PMFrontResources.Resources_Dummy_Class_LookupPM),
                                    "_ErrLookup01"));
                            _viewModel.PoReportParam.CFR_CODE = "";
                            _viewModel.CFR_CODE_NAME = "";
                            goto EndBlock;
                        }
                        _viewModel.PoReportParam.CFR_CODE = loResult.CTENANT_ID??"";
                        _viewModel.CFR_CODE_NAME = loResult.CTENANT_NAME??"";
                    }
                    else if (_viewModel.PoReportParam.CFILTER_BY == "CUSTOMER_CATEGORY")
                    {
                        LookupLML02000ViewModel loLookupViewModel = new LookupLML02000ViewModel();
                        var loParam = new LML02000ParameterDTO
                        {
                            CSEARCH_TEXT = _viewModel.PoReportParam.CFR_CODE,
                            CPROPERTY_ID = _viewModel.PoReportParam.CPROPERTY_ID
                        };
                        var loResult = await loLookupViewModel.LML02000TenantCategory(loParam);

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
                            CJRNGRP_TYPE = "20"
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

                if (_viewModel.PoReportParam.CFILTER_BY == "CUSTOMER_ID" || _viewModel.PoReportParam.CFILTER_BY == "CUSTOMER_NAME")
                {
                    LookupLML00600ViewModel loLookupViewModel = new LookupLML00600ViewModel();
                    var loParam = new LML00600ParameterDTO
                    {
                        CPROPERTY_ID = _viewModel.PoReportParam.CPROPERTY_ID,
                        CCUSTOMER_TYPE = "01"
                    };
                    await loLookupViewModel.GetTenantList(loParam);
                    if (loLookupViewModel.TenantList.Count > 0)
                    {
                        _viewModel.PoReportParam.CFR_CODE = loLookupViewModel.TenantList.FirstOrDefault()?.CTENANT_ID;
                        _viewModel.CFR_CODE_NAME = loLookupViewModel.TenantList
                            .Where(x => x.CTENANT_ID == _viewModel.PoReportParam.CFR_CODE)
                            .Select(x => x.CTENANT_NAME).FirstOrDefault() ?? string.Empty;
                        _viewModel.PoReportParam.CTO_CODE = loLookupViewModel.TenantList.LastOrDefault()?.CTENANT_ID;
                        _viewModel.CTO_CODE_NAME = loLookupViewModel.TenantList
                            .Where(x => x.CTENANT_ID == _viewModel.PoReportParam.CTO_CODE)
                            .Select(x => x.CTENANT_NAME).FirstOrDefault() ?? string.Empty;
                    }
                } else if (_viewModel.PoReportParam.CFILTER_BY == "CUSTOMER_CATEGORY")
                {

                    LookupLML02000ViewModel loLookupViewModel = new LookupLML02000ViewModel();
                    var loParam = new LML02000ParameterDTO
                    {
                        CSEARCH_TEXT = _viewModel.PoReportParam.CFR_CODE,
                        CPROPERTY_ID = _viewModel.PoReportParam.CPROPERTY_ID
                    };
                    await loLookupViewModel.LML02000TenantCategoryList(loParam);
                    if (loLookupViewModel.TenantCategoryListResult.Count > 0)
                    {
                        _viewModel.PoReportParam.CFR_CODE = loLookupViewModel.TenantCategoryListResult.FirstOrDefault()?.CCATEGORY_ID;
                        _viewModel.CFR_CODE_NAME = loLookupViewModel.TenantCategoryListResult
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
                        CJRNGRP_TYPE = "20"
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
            if (_viewModel.PoReportParam.CFILTER_BY == "CUSTOMER_ID" || _viewModel.PoReportParam.CFILTER_BY == "CUSTOMER_NAME")
            {
                var loParam = new LML00600ParameterDTO()
                {
                    CPROPERTY_ID = _viewModel.PoReportParam.CPROPERTY_ID,
                    CCUSTOMER_TYPE = "01"
                };
                eventArgs.Parameter = loParam;
                eventArgs.TargetPageType = typeof(LML00600);
            }
            else if (_viewModel.PoReportParam.CFILTER_BY == "CUSTOMER_CATEGORY")
            {
                var loParam = new LML02000ParameterDTO()
                {
                    CPROPERTY_ID = _viewModel.PoReportParam.CPROPERTY_ID
                };
                eventArgs.Parameter = loParam;
                eventArgs.TargetPageType = typeof(LML02000);
            }
            else if (_viewModel.PoReportParam.CFILTER_BY == "JOURNAL_GROUP")
            {
                var loParam = new GSL00400ParameterDTO()
                {
                    CPROPERTY_ID = _viewModel.PoReportParam.CPROPERTY_ID,
                    CJRNGRP_TYPE = "20"
                };
                eventArgs.Parameter = loParam;
                eventArgs.TargetPageType = typeof(GSL00400);
            }
        }
        private void AfterOpen_lookupFromCustomer(R_AfterOpenLookupEventArgs eventArgs)
        {

            if (_viewModel.PoReportParam.CFILTER_BY == "CUSTOMER_ID" || _viewModel.PoReportParam.CFILTER_BY == "CUSTOMER_NAME")
            {
                var loTempResult = (LML00600DTO)eventArgs.Result;
                if (loTempResult != null)
                {
                    _viewModel.PoReportParam.CFR_CODE = loTempResult.CTENANT_ID??"";
                    _viewModel.CFR_CODE_NAME = loTempResult.CTENANT_NAME ?? "";
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
            else if (_viewModel.PoReportParam.CFILTER_BY == "CUSTOMER_CATEGORY")
            {
                var loTempResult = (LML02000DTO)eventArgs.Result;
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
                    if (_viewModel.PoReportParam.CFILTER_BY == "CUSTOMER_ID" || _viewModel.PoReportParam.CFILTER_BY == "CUSTOMER_NAME")
                    {
                        LookupLML00600ViewModel loLookupViewModel = new LookupLML00600ViewModel();
                        var loParam = new LML00600ParameterDTO
                        {
                            CSEARCH_TEXT = _viewModel.PoReportParam.CTO_CODE,
                            CPROPERTY_ID = _viewModel.PoReportParam.CPROPERTY_ID,
                            CCUSTOMER_TYPE = "01"
                        };
                        var loResult = await loLookupViewModel.GetTenant(loParam);

                        if (loResult == null)
                        {
                            loEx.Add(R_FrontUtility.R_GetError(
                                    typeof(Lookup_PMFrontResources.Resources_Dummy_Class_LookupPM),
                                    "_ErrLookup01"));
                            _viewModel.PoReportParam.CTO_CODE = "";
                            _viewModel.CTO_CODE_NAME = "";
                            goto EndBlock;
                        }
                        _viewModel.PoReportParam.CTO_CODE = loResult.CTENANT_ID ?? "";
                        _viewModel.CTO_CODE_NAME = loResult.CTENANT_NAME ?? "";
                    }
                    else if (_viewModel.PoReportParam.CFILTER_BY == "CUSTOMER_CATEGORY")
                    {
                        LookupLML02000ViewModel loLookupViewModel = new LookupLML02000ViewModel();
                        var loParam = new LML02000ParameterDTO
                        {
                            CSEARCH_TEXT = _viewModel.PoReportParam.CTO_CODE,
                            CPROPERTY_ID = _viewModel.PoReportParam.CPROPERTY_ID
                        };
                        var loResult = await loLookupViewModel.LML02000TenantCategory(loParam);

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
                            CJRNGRP_TYPE = "20"
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
            if (_viewModel.PoReportParam.CFILTER_BY == "CUSTOMER_ID" || _viewModel.PoReportParam.CFILTER_BY == "CUSTOMER_NAME")
            {
                var loParam = new LML00600ParameterDTO()
                {
                    CPROPERTY_ID = _viewModel.PoReportParam.CPROPERTY_ID,
                    CCUSTOMER_TYPE = "01"
                };
                eventArgs.Parameter = loParam;
                eventArgs.TargetPageType = typeof(LML00600);
            }
            else if (_viewModel.PoReportParam.CFILTER_BY == "CUSTOMER_CATEGORY")
            {
                var loParam = new LML02000ParameterDTO()
                {
                    CPROPERTY_ID = _viewModel.PoReportParam.CPROPERTY_ID
                };
                eventArgs.Parameter = loParam;
                eventArgs.TargetPageType = typeof(LML02000);
            }
            else if (_viewModel.PoReportParam.CFILTER_BY == "JOURNAL_GROUP")
            {
                var loParam = new GSL00400ParameterDTO()
                {
                    CPROPERTY_ID = _viewModel.PoReportParam.CPROPERTY_ID,
                    CJRNGRP_TYPE = "20"
                };
                eventArgs.Parameter = loParam;
                eventArgs.TargetPageType = typeof(GSL00400);
            }
        }

        private void AfterOpen_lookupToCustomer(R_AfterOpenLookupEventArgs eventArgs)
        {

            if (_viewModel.PoReportParam.CFILTER_BY == "CUSTOMER_ID" || _viewModel.PoReportParam.CFILTER_BY == "CUSTOMER_NAME")
            {
                var loTempResult = (LML00600DTO)eventArgs.Result;
                if (loTempResult != null)
                {
                    _viewModel.PoReportParam.CTO_CODE = loTempResult.CTENANT_ID ?? "";
                    _viewModel.CTO_CODE_NAME = loTempResult.CTENANT_NAME ?? "";
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
            else if (_viewModel.PoReportParam.CFILTER_BY == "CUSTOMER_CATEGORY")
            {
                var loTempResult = (LML02000DTO)eventArgs.Result;
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

        private void validationPrint(PMR03300ReportParamDTO poParam)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                if (string.IsNullOrEmpty(poParam.CFR_CODE)&&poParam.CFILTER_BY== "CUSTOMER_ID")
                {
                    loEx.Add("", _localizer["Validation_From_Customer_Id"]);
                }
                if (string.IsNullOrEmpty(poParam.CFR_CODE) && poParam.CFILTER_BY == "CUSTOMER_NAME")
                {
                    loEx.Add("", _localizer["Validation_From_Customer_Name"]);
                }
                if (string.IsNullOrEmpty(poParam.CFR_CODE) && poParam.CFILTER_BY == "CUSTOMER_CATEGORY")
                {
                    loEx.Add("", _localizer["Validation_From_Customer_Category"]);
                }
                if (string.IsNullOrEmpty(poParam.CFR_CODE) && poParam.CFILTER_BY == "JOURNAL_GROUP")
                {
                    loEx.Add("", _localizer["Validation_From_Journal_Group"]);
                }


                if (string.IsNullOrEmpty(poParam.CTO_CODE) && poParam.CFILTER_BY == "CUSTOMER_ID")
                {
                    loEx.Add("", _localizer["Validation_To_Customer_Id"]);
                }
                if (string.IsNullOrEmpty(poParam.CTO_CODE) && poParam.CFILTER_BY == "CUSTOMER_NAME")
                {
                    loEx.Add("", _localizer["Validation_To_Customer_Name"]);
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

        private PMR03300ReportParamDTO InitializeParameters(PMR03300ReportParamDTO loReturn)
        {
            R_Exception loEx = new R_Exception();
            PMR03300ReportParamDTO loRtn = new();
            try
            {
                var prop = _viewModel.PropertyList?.FirstOrDefault(x => x.CPROPERTY_ID == loRtn.CPROPERTY_ID);
                loRtn.CCOMPANY_ID = ClientHelper.CompanyId ?? string.Empty;
                loRtn.CUSER_ID = ClientHelper.UserId ?? string.Empty;
                loRtn.CREPORT_CULTURE = ClientHelper.ReportCulture.ToString();
                loRtn.CPROPERTY_ID = _viewModel.PoReportParam.CPROPERTY_ID ?? string.Empty;
                loRtn.CPROPERTY_NAME = prop?.CPROPERTY_NAME ?? "";
                loRtn.CFR_PERIOD = _viewModel._IFromYear.ToString() + "" + _viewModel._CFromMonth.ToString();
                loRtn.CFR_PERIOD_DISPLAY = _viewModel._IFromYear.ToString() + "-" + _viewModel._CFromMonth.ToString();
                loRtn.CTO_PERIOD = _viewModel._IToYear.ToString() + "" + _viewModel._CToMonth.ToString();
                loRtn.CTO_PERIOD_DISPLAY = _viewModel._IToYear.ToString() + "-" + _viewModel._CToMonth.ToString();
                loRtn.CCURRENCY_TYPE = _viewModel.PoReportParam.CCURRENCY_TYPE ?? string.Empty;
                loRtn.CCURRENCY_TYPE_NAME = _viewModel.TypeList.FirstOrDefault(x => x.Key == _viewModel.PoReportParam.CCURRENCY_TYPE).Value ?? "Local Currency";
                loRtn.CFILTER_BY = _viewModel.PoReportParam.CFILTER_BY ?? string.Empty;
                loRtn.CFILTER_BY_NAME = _viewModel.FilterByList.FirstOrDefault(x => x.CCODE == _viewModel.PoReportParam.CFILTER_BY).CNAME ?? "Customer ID";
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

        private async Task Generate_Report(PMR03300ReportParamDTO param)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                PMR03300ReportParamDTO loParam = InitializeParameters(param);
                validationPrint(loParam);
                if(loParam.CFILTER_BY== "CUSTOMER_ID" || loParam.CFILTER_BY == "CUSTOMER_NAME")
                {
                    await _reportService.GetReport(
                        "R_DefaultServiceUrlPM",
                        "PM",
                        "rpt/PMR03300PrintCustomerId/DownloadResultPrintPost",
                        "rpt/PMR03300PrintCustomerId/CustomerId_ReportListGet",
                        loParam);
                }
                if (loParam.CFILTER_BY == "CUSTOMER_CATEGORY")
                {
                    await _reportService.GetReport(
                        "R_DefaultServiceUrlPM",
                        "PM",
                        "rpt/PMR03300PrintCustomerCategory/DownloadResultPrintPost",
                        "rpt/PMR03300PrintCustomerCategory/CustomerCategory_ReportListGet",
                        loParam);
                }
                if (loParam.CFILTER_BY == "JOURNAL_GROUP")
                {
                    await _reportService.GetReport(
                        "R_DefaultServiceUrlPM",
                        "PM",
                        "rpt/PMR03300PrintJournalGroup/DownloadResultPrintPost",
                        "rpt/PMR03300PrintJournalGroup/JournalGroup_ReportListGet",
                        loParam);
                }
                //await _reportService.GetReport(
                //        "R_DefaultServiceUrlPM",
                //        "PM",
                //        "rpt/PMR03300Print/DownloadResultPrintPost",
                //        "rpt/PMR03300Print/UserActivitySummary_ReportListGet",
                //        loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private async Task OnclickBtn_Print(PMR03300ReportParamDTO poParam)
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
            eventArgs.TargetPageType = typeof(PMR03300PopUpSaveAs);
        }

        private async Task AfterOpen_PopupSaveAs(R_AfterOpenPopupEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                if (eventArgs.Success)
                {
                    // Ambil parameter dari popup
                    var loReturn = R_FrontUtility.ConvertObjectToObject<PMR03300ReportParamDTO>(eventArgs.Result);

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
