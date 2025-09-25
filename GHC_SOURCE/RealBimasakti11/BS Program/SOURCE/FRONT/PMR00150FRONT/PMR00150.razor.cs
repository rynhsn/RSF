using BlazorClientHelper;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using Lookup_GSModel.ViewModel;
using Lookup_PMCOMMON.DTOs;
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
        private PMR00150ViewModel _viewModel = new();
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
                await _viewModel.GetInitialProcess();
                _viewModel.GetMonth();


            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void OnChangedProperty(object? poParam)
        {
            var loEx = new R_Exception();
            string lsProperty = (string)poParam!;
            try
            {
                _viewModel.PropertyCode = lsProperty!;

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private async Task OnPrint()
        {
            R_Exception loException = new R_Exception();
            try
            {
                string lcPropertyName = _viewModel.PropertyList.FirstOrDefault(item => item.CPROPERTY_ID == _viewModel.PropertyCode)?.CPROPERTY_NAME!;
                PMR00150DBParamDTO param = new PMR00150DBParamDTO()
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
                };

                param.CREPORT_TYPE = _viewModel.lcReportType;
                param.CREPORT_NAME = _viewModel.GetReportTypeList.Where(item => item.Id == param.CREPORT_TYPE)
                    .Select(item => item.Name).FirstOrDefault()!;
                _viewModel.ValidationFieldEmpty(param); //ForValidation

                if (param.CREPORT_TYPE == "1") //SUMMARY
                {
                    await _reportService.GetReport(
                  "R_DefaultServiceUrlPM",
                  "PM",
                  "rpt/PMR00150SummaryReport/SummaryReportPost",
                  "rpt/PMR00150SummaryReport/SummaryReportGet",
                  param);
                }
                else if (param.CREPORT_TYPE == "2") //DETAIL
                {
                    await _reportService.GetReport(
                  "R_DefaultServiceUrlPM",
                  "PM",
                   "rpt/PMR00150DetailReport/DetailReportPost",
                  "rpt/PMR00150DetailReport/DetailReportGet",
                  param);
                }

            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }
        #region lookupDeptFrom
        private R_Lookup R_LookupBtnDeptFrom;
        private R_TextBox R_TextBoxBtnDeptFrom;
        private async Task Before_Open_lookupDeptFrom(R_BeforeOpenLookupEventArgs eventArgs)
        {
            var param = new GSL00700ParameterDTO
            {
                CUSER_ID = clientHelper.UserId,
                CCOMPANY_ID = clientHelper.CompanyId
            };
            eventArgs.Parameter = param;
            eventArgs.TargetPageType = typeof(GSL00700);
        }
        private void After_Open_lookupDeptFrom(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loTempResult = (GSL00700DTO)eventArgs.Result;
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


                LookupGSL00700ViewModel loLookupViewModel = new LookupGSL00700ViewModel();
                var param = new GSL00700ParameterDTO
                {
                    CUSER_ID = clientHelper!.UserId,
                    CCOMPANY_ID = clientHelper.CompanyId,
                    CSEARCH_TEXT = _viewModel.lcDeptCodeFrom,
                };
                var loResult = await loLookupViewModel.GetDepartment(param);

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
        #endregion
        #region lookupDeptTo
        private R_Lookup R_LookupBtnDeptTo;
        private R_TextBox R_TextBoxBtnDeptTo;
        private async Task Before_Open_lookupDeptTo(R_BeforeOpenLookupEventArgs eventArgs)
        {
            var param = new GSL00700ParameterDTO
            {
                CUSER_ID = clientHelper.UserId,
                CCOMPANY_ID = clientHelper.CompanyId
            };
            eventArgs.Parameter = param;
            eventArgs.TargetPageType = typeof(GSL00700);
        }
        private void After_Open_lookupDeptTo(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loTempResult = (GSL00700DTO)eventArgs.Result;
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


                LookupGSL00700ViewModel loLookupViewModel = new LookupGSL00700ViewModel();
                var param = new GSL00700ParameterDTO
                {
                    CUSER_ID = clientHelper!.UserId,
                    CCOMPANY_ID = clientHelper.CompanyId,
                    CSEARCH_TEXT = _viewModel.lcDeptCodeTo,
                };
                var loResult = await loLookupViewModel.GetDepartment(param);

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
        #endregion

        #region lookupSalesmanFrom
        private R_Lookup R_LookupBtnSalesmanFrom;
        private R_TextBox R_TextBoxBtnSalesmanFrom;
        private async Task Before_Open_lookupSalesmanFrom(R_BeforeOpenLookupEventArgs eventArgs)
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
            _viewModel.lcSalesmanCodeFrom = loTempResult.CSALESMAN_ID;
            _viewModel.lcSalesmanNameFrom = loTempResult.CSALESMAN_NAME;
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
                    _viewModel.lcSalesmanCodeFrom = loResult.CSALESMAN_ID;
                    _viewModel.lcSalesmanNameFrom = loResult.CSALESMAN_NAME;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        #endregion
        #region lookupSalesmanTo
        private R_Lookup R_LookupBtnSalesmanTo;
        private R_TextBox R_TextBoxBtnSalesmanTo;
        private async Task Before_Open_lookupSalesmanTo(R_BeforeOpenLookupEventArgs eventArgs)
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
            _viewModel.lcSalesmanCodeTo = loTempResult.CSALESMAN_ID;
            _viewModel.lcSalesmanNameTo = loTempResult.CSALESMAN_NAME;
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
        #endregion

    }
}
