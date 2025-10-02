using BlazorClientHelper;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using Lookup_GSModel.ViewModel;
using Lookup_PMCOMMON.DTOs;
using Lookup_PMCOMMON.DTOs.PopUpSaveAs;
using Lookup_PMFRONT;
using Lookup_PMModel.ViewModel.LML00500;
using Microsoft.AspNetCore.Components;
using PMR00150COMMON.Utility_Report;
using PMR00150MODEL.ViewModel;
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

namespace PMR00150FRONT
{
    public partial class PMR00150 : R_Page
    {
        private readonly PMR00150ViewModel _viewModel = new();
        private R_Conductor? _conductorRef;
        [Inject] IClientHelper? clientHelper { get; set; }
        [Inject] private R_IReport? _reportService { get; set; }

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                // _viewModel.GetReportType();
                await _viewModel.GetPropertyList();
                if (_viewModel._lPropertyExist)
                {
                    await _viewModel.GetInitialProcess();
                    _viewModel.GetMonth();

                }

                await _setDefaultDeptfrom();
                await _setDefaultDeptto();
                await _setDefaultSalesfrom();
                await _setDefaultSalesto();

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task OnChangedProperty(object? poParam)
        {
            R_Exception loEx = new ();
            string lsProperty = (string)poParam!;
            try
            {
                _viewModel.PropertyCode = lsProperty!;

                await _setDefaultDeptfrom();
                await _setDefaultDeptto();
                await _setDefaultSalesfrom();
                await _setDefaultSalesto();

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private async Task OnPrint()
        {
            R_Exception loException = new ();
            try
            {
                var loParam = GetParamAndValidation();
                loParam.LIS_PRINT = true;
                loParam.CREPORT_FILENAME = "";
                loParam.CREPORT_FILETYPE = "";

                if (loParam.CREPORT_TYPE == "1") //SUMMARY
                {
                    await _reportService.GetReport(
                  "R_DefaultServiceUrlPM",
                  "PM",
                  "rpt/PMR00150SummaryReport/SummaryReportPost",
                  "rpt/PMR00150SummaryReport/SummaryReportGet",
                  loParam);
                }
                else if (loParam.CREPORT_TYPE == "2") //DETAIL
                {
                    await _reportService.GetReport(
                  "R_DefaultServiceUrlPM",
                  "PM",
                   "rpt/PMR00150DetailReport/DetailReportPost",
                  "rpt/PMR00150DetailReport/DetailReportGet",
                  loParam);
                }

            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }
        private PMR00150DBParamDTO GetParamAndValidation()
        {
            R_Exception loException = new ();
            PMR00150DBParamDTO loReturn = new();
            try
            {
                string lcPropertyName = _viewModel.PropertyList.FirstOrDefault(item => item.CPROPERTY_ID == _viewModel.PropertyCode)?.CPROPERTY_NAME!;

                string lcPeriodMonthNameFrom = _viewModel.GetMonthList!.First(x => x.Id == _viewModel.lcPeriodMonthFrom).Name!;
                string lcPeriodMonthNameTo = _viewModel.GetMonthList!.First(x => x.Id == _viewModel.lcPeriodMonthTo).Name!;
                PMR00150DBParamDTO param = new ()
                {
                    CCOMPANY_ID = clientHelper!.CompanyId,
                    CUSER_ID = clientHelper!.UserId,
                    CLANG_ID = clientHelper.Culture.ToString(),
                    CPROPERTY_ID = _viewModel.PropertyCode,
                    CPROPERTY_NAME = lcPropertyName,

                    CFROM_DEPARTMENT_ID = _viewModel.lcDeptCodeFrom,
                    CFROM_DEPARTMENT_NAME = _viewModel.lcDeptNameFrom,

                    CTO_DEPARTMENT_ID = _viewModel.lcDeptCodeTo,
                    CTO_DEPARTMENT_NAME = _viewModel.lcDeptNameTo,

                    CFROM_SALESMAN_ID = _viewModel.lcSalesmanCodeFrom,
                    CFROM_SALESMAN_NAME = _viewModel.lcSalesmanNameFrom,

                    CTO_SALESMAN_ID = _viewModel.lcSalesmanCodeTo,
                    CTO_SALESMAN_NAME = _viewModel.lcSalesmanNameTo,

                    CFROM_PERIOD = _viewModel.lnPeriodYearFrom + _viewModel.lcPeriodMonthFrom,
                    CTO_PERIOD = _viewModel.lnPeriodYearTo + _viewModel.lcPeriodMonthTo,
                    CFROM_PERIOD_NAME = $"{lcPeriodMonthNameFrom} {_viewModel.lnPeriodYearFrom}",
                    CTO_PERIOD_NAME = $"{lcPeriodMonthNameTo} {_viewModel.lnPeriodYearTo}",
                    CREPORT_TYPE = _viewModel.lcReportType
                };
                param.CREPORT_NAME = _viewModel.GetReportTypeList.Where(item => item.Id == param.CREPORT_TYPE)
                    .Select(item => item.Name).FirstOrDefault()!;
                _viewModel.ValidationFieldEmpty(param); //ForValidation

                loReturn = param;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
            return loReturn;
        }

        #region Save As
        private void BeforeOpen_PopupSaveAsAsync(R_BeforeOpenPopupEventArgs eventArgs)
        {
            //InitializePrintParam();
           // var loParam = GetParamAndValidation();
            eventArgs.PageTitle = _localizer["_titleSaveAs"];
            eventArgs.TargetPageType = typeof(PopUpSaveAs);
        }
        private async Task AfterOpen_PopupSaveAs(R_AfterOpenPopupEventArgs eventArgs)
        {
            R_Exception loEx = new ();
            try
            {
                var loReturnPopUp = R_FrontUtility.ConvertObjectToObject<SaveAsDTO>(eventArgs.Result);

                PMR00150DBParamDTO loParam = GetParamAndValidation();
                loParam.LIS_PRINT = false;
                loParam.CREPORT_FILENAME = loReturnPopUp.CREPORT_FILENAME;
                loParam.CREPORT_FILETYPE = loReturnPopUp.CREPORT_FILETYPE;

                if (loParam.CREPORT_TYPE == "1") //SUMMARY
                {
                    await _reportService.GetReport(
                  "R_DefaultServiceUrlPM",
                  "PM",
                  "rpt/PMR00150SummaryReport/SummaryReportPost",
                  "rpt/PMR00150SummaryReport/SummaryReportGet",
                  loParam);
                }
                else if (loParam.CREPORT_TYPE == "2") //DETAIL
                {
                    await _reportService.GetReport(
                  "R_DefaultServiceUrlPM",
                  "PM",
                   "rpt/PMR00150DetailReport/DetailReportPost",
                  "rpt/PMR00150DetailReport/DetailReportGet",
                  loParam);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            await Task.CompletedTask;
        }
        #endregion


        #region lookupDeptFrom
        private R_Lookup? R_LookupBtnDeptFrom;
        private R_TextBox? R_TextBoxBtnDeptFrom;
        private void Before_Open_lookupDeptFrom(R_BeforeOpenLookupEventArgs eventArgs)
        {
            var param = new GSL00710ParameterDTO
            {
                CPROPERTY_ID = _viewModel.PropertyCode
            };
            eventArgs.Parameter = param;
            eventArgs.TargetPageType = typeof(GSL00710);
        }
        private void After_Open_lookupDeptFrom(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loTempResult = (GSL00710DTO)eventArgs.Result;
            if (loTempResult == null)
            {
                return;
            }
            _viewModel.lcDeptCodeFrom = loTempResult.CDEPT_CODE;
            _viewModel.lcDeptNameFrom = loTempResult.CDEPT_NAME;
        }
        private async Task OnLostFocusDeptFrom()
        {
            R_Exception loEx = new R_Exception();

            try
            {

                if (string.IsNullOrWhiteSpace(_viewModel.lcDeptCodeFrom))
                {
                    _viewModel.lcDeptCodeFrom = "";
                    _viewModel.lcDeptNameFrom = "";
                    return;
                }


                LookupGSL00710ViewModel loLookupViewModel = new LookupGSL00710ViewModel();
                var param = new GSL00710ParameterDTO
                {
                    CPROPERTY_ID = _viewModel.PropertyCode,
                    CSEARCH_TEXT = _viewModel.lcDeptCodeFrom,
                };
                var loResult = await loLookupViewModel.GetDepartmentProperty(param);

                if (loResult == null)
                {
                    await R_TextBoxBtnDeptTo.FocusAsync();
                    loEx.Add(R_FrontUtility.R_GetError(
                            typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                            "_ErrLookup01"));
                    _viewModel.lcDeptCodeFrom = "";
                    _viewModel.lcDeptNameFrom = "";
                }
                else
                {
                    _viewModel.lcDeptCodeFrom = loResult.CDEPT_CODE;
                    _viewModel.lcDeptNameFrom = loResult.CDEPT_NAME;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        private async Task _setDefaultDeptfrom()
        {
            var loEx = new R_Exception();
                
            try
            {
                var loLookupViewModel = new LookupGSL00710ViewModel();
                var loParameter = new GSL00710ParameterDTO();
                loParameter.CPROPERTY_ID = _viewModel.PropertyCode;
                await loLookupViewModel.GetDepartmentPropertyList(loParameter);
                if (loLookupViewModel.DepartmentPropertyGrid.Count > 0)
                {
                    _viewModel.lcDeptCodeFrom =
                        loLookupViewModel.DepartmentPropertyGrid.FirstOrDefault()?.CDEPT_CODE;
                    _viewModel.lcDeptNameFrom = loLookupViewModel.DepartmentPropertyGrid
                        .Where(x => x.CDEPT_CODE == _viewModel.lcDeptCodeFrom)
                        .Select(x => x.CDEPT_NAME).FirstOrDefault() ?? string.Empty;

                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        #endregion
        #region lookupDeptTo
        private R_Lookup? R_LookupBtnDeptTo;
        private R_TextBox? R_TextBoxBtnDeptTo;
        private void Before_Open_lookupDeptTo(R_BeforeOpenLookupEventArgs eventArgs)
        {
            var param = new GSL00710ParameterDTO
            {
                CPROPERTY_ID = _viewModel.PropertyCode
            };
            eventArgs.Parameter = param;
            eventArgs.TargetPageType = typeof(GSL00710);
        }
        private void After_Open_lookupDeptTo(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loTempResult = (GSL00710DTO)eventArgs.Result;
            if (loTempResult == null)
            {
                return;
            }
            _viewModel.lcDeptCodeTo = loTempResult.CDEPT_CODE;
            _viewModel.lcDeptNameTo = loTempResult.CDEPT_NAME;
        }
        private async Task OnLostFocusDeptTo()
        {
            R_Exception loEx = new R_Exception();

            try
            {

                if (string.IsNullOrWhiteSpace(_viewModel.lcDeptCodeTo))
                {
                    _viewModel.lcDeptCodeTo = "";
                    _viewModel.lcDeptNameTo = "";
                    return;
                }


                LookupGSL00710ViewModel loLookupViewModel = new LookupGSL00710ViewModel();
                var param = new GSL00710ParameterDTO
                {
                    CPROPERTY_ID = _viewModel.PropertyCode,
                    CSEARCH_TEXT = _viewModel.lcDeptCodeTo,
                };
                var loResult = await loLookupViewModel.GetDepartmentProperty(param);

                if (loResult == null)
                {
                    await R_TextBoxBtnDeptTo.FocusAsync();
                    loEx.Add(R_FrontUtility.R_GetError(
                            typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                            "_ErrLookup01"));
                    _viewModel.lcDeptCodeTo = "";
                    _viewModel.lcDeptNameTo = "";
                }
                else
                {
                    _viewModel.lcDeptCodeTo = loResult.CDEPT_CODE;
                    _viewModel.lcDeptNameTo = loResult.CDEPT_NAME;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        private async Task _setDefaultDeptto()
        {
            var loEx = new R_Exception();

            try
            {
                var loLookupViewModel = new LookupGSL00710ViewModel();
                var loParameter = new GSL00710ParameterDTO();
                loParameter.CPROPERTY_ID = _viewModel.PropertyCode;

                await loLookupViewModel.GetDepartmentPropertyList(loParameter);
                if (loLookupViewModel.DepartmentPropertyGrid.Count > 0)
                {
                    _viewModel.lcDeptCodeTo =
                        loLookupViewModel.DepartmentPropertyGrid.LastOrDefault()?.CDEPT_CODE;
                    _viewModel.lcDeptNameTo= loLookupViewModel.DepartmentPropertyGrid
                        .Where(x => x.CDEPT_CODE == _viewModel.lcDeptCodeTo)
                        .Select(x => x.CDEPT_NAME).LastOrDefault() ?? string.Empty;

                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        #endregion

        #region lookupSalesmanFrom
        private R_Lookup? R_LookupBtnSalesmanFrom;
        private R_TextBox? R_TextBoxBtnSalesmanFrom;
        private void Before_Open_lookupSalesmanFrom(R_BeforeOpenLookupEventArgs eventArgs)
        {
            var param = new LML00500ParameterDTO
            {
                //    CUSER_ID = clientHelper.UserId,
                //    CCOMPANY_ID = clientHelper.CompanyId,
                CPROPERTY_ID = _viewModel.PropertyCode
            };
            eventArgs.Parameter = param;
            eventArgs.TargetPageType = typeof(LML00500);
        }
        private void After_Open_lookupSalesmanFrom(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loTempResult = (LML00500DTO)eventArgs.Result;
            if (loTempResult == null)
            {
                return;
            }
            _viewModel.lcSalesmanCodeFrom = loTempResult.CSALESMAN_ID!;
            _viewModel.lcSalesmanNameFrom = loTempResult.CSALESMAN_NAME!;
        }
        private async Task OnLostFocusSalesmanFrom()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                if (string.IsNullOrWhiteSpace(_viewModel.lcSalesmanCodeFrom))
                {
                    _viewModel.lcSalesmanCodeFrom = "";
                    _viewModel.lcSalesmanNameFrom = "";
                    return;
                }
                LookupLML00500ViewModel loLookupViewModel = new LookupLML00500ViewModel();
                var param = new LML00500ParameterDTO
                {
                    CUSER_ID = clientHelper.UserId,
                    CCOMPANY_ID = clientHelper.CompanyId,
                    CPROPERTY_ID = _viewModel.PropertyCode,
                    CSEARCH_TEXT = _viewModel.lcSalesmanCodeFrom

                };
                var loResult = await loLookupViewModel.GetSalesman(param);



                if (loResult == null)
                {
                    await R_TextBoxBtnSalesmanTo.FocusAsync();
                    loEx.Add(R_FrontUtility.R_GetError(
                            typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                            "_ErrLookup01"));
                    _viewModel.lcSalesmanCodeFrom = "";
                    _viewModel.lcSalesmanNameFrom = "";
                }
                else
                {
                    _viewModel.lcSalesmanCodeFrom = loResult.CSALESMAN_ID!;
                    _viewModel.lcSalesmanNameFrom = loResult.CSALESMAN_NAME!;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        private async Task _setDefaultSalesfrom()
        {
            var loEx = new R_Exception();

            try
            {
                var loLookupViewModel = new LookupLML00500ViewModel();
                var loParameter = new LML00500ParameterDTO();
                loParameter.CPROPERTY_ID = _viewModel.PropertyCode;
                await loLookupViewModel.GetSalesmanList(loParameter);
                if (loLookupViewModel.SalesmanList.Count > 0)
                {
                    _viewModel.lcSalesmanCodeFrom =
                        loLookupViewModel.SalesmanList.FirstOrDefault()?.CSALESMAN_ID;
                    _viewModel.lcSalesmanNameFrom= loLookupViewModel.SalesmanList
                        .Where(x => x.CSALESMAN_ID == _viewModel.lcSalesmanCodeFrom)
                        .Select(x => x.CSALESMAN_NAME).FirstOrDefault() ?? string.Empty;

                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }


        #endregion
        #region lookupSalesmanTo
        private R_Lookup? R_LookupBtnSalesmanTo;
        private R_TextBox? R_TextBoxBtnSalesmanTo;
        private void Before_Open_lookupSalesmanTo(R_BeforeOpenLookupEventArgs eventArgs)
        {
            var param = new LML00500ParameterDTO
            {
                CUSER_ID = clientHelper.UserId,
                CCOMPANY_ID = clientHelper.CompanyId,
                CPROPERTY_ID = _viewModel.PropertyCode
            };
            eventArgs.Parameter = param;
            eventArgs.TargetPageType = typeof(LML00500);
        }
        private void After_Open_lookupSalesmanTo(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loTempResult = (LML00500DTO)eventArgs.Result;
            if (loTempResult == null)
            {
                return;
            }
            _viewModel.lcSalesmanCodeTo = loTempResult.CSALESMAN_ID!;
            _viewModel.lcSalesmanNameTo = loTempResult.CSALESMAN_NAME!;
        }
        private async Task OnLostFocusSalesmanTo()
        {
            R_Exception loEx = new R_Exception();

            try
            {

                if (string.IsNullOrWhiteSpace(_viewModel.lcSalesmanCodeTo))
                {
                    _viewModel.lcSalesmanCodeTo = "";
                    _viewModel.lcSalesmanNameTo = "";
                    return;
                }


                LookupLML00500ViewModel loLookupViewModel = new LookupLML00500ViewModel();
                var param = new LML00500ParameterDTO
                {
                    CUSER_ID = clientHelper.UserId,
                    CCOMPANY_ID = clientHelper.CompanyId,
                    CPROPERTY_ID = _viewModel.PropertyCode,
                    CSEARCH_TEXT = _viewModel.lcSalesmanCodeTo
                };
                var loResult = await loLookupViewModel.GetSalesman(param);

                if (loResult == null)
                {
                    await R_TextBoxBtnSalesmanTo.FocusAsync();
                    loEx.Add(R_FrontUtility.R_GetError(
                            typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                            "_ErrLookup01"));
                    _viewModel.lcSalesmanCodeTo = "";
                    _viewModel.lcSalesmanNameTo = "";
                }
                else
                {
                    _viewModel.lcSalesmanCodeTo = loResult.CSALESMAN_ID!;
                    _viewModel.lcSalesmanNameTo = loResult.CSALESMAN_NAME!;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private async Task _setDefaultSalesto()
        {
            var loEx = new R_Exception();

            try
            {
                var loLookupViewModel = new LookupLML00500ViewModel();
                var loParameter = new LML00500ParameterDTO();
                loParameter.CPROPERTY_ID = _viewModel.PropertyCode;

                await loLookupViewModel.GetSalesmanList(loParameter);
                if (loLookupViewModel.SalesmanList.Count > 0)
                {
                    _viewModel.lcSalesmanCodeTo =
                        loLookupViewModel.SalesmanList.LastOrDefault()?.CSALESMAN_ID;
                    _viewModel.lcSalesmanNameTo = loLookupViewModel.SalesmanList
                        .Where(x => x.CSALESMAN_ID == _viewModel.lcSalesmanCodeTo)
                        .Select(x => x.CSALESMAN_NAME).LastOrDefault() ?? string.Empty;


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
