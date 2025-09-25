using BlazorClientHelper;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using Lookup_GSModel.ViewModel;
using Lookup_PMCOMMON.DTOs;
using Lookup_PMCOMMON.DTOs.LML01000;
using Lookup_PMFRONT;
using Lookup_PMModel.ViewModel.LML00500;
using Lookup_PMModel.ViewModel.LML00600;
using Lookup_PMModel.ViewModel.LML01000;
using Lookup_PMModel.ViewModel.LML01100;
using Microsoft.AspNetCore.Components;
using PMT00100COMMON.Booking;
using PMT00100MODEL.ViewModel;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PMT00100FRONT
{
    public partial class PMT00100Booking : R_Page
    {
        private readonly PMT00100BookingViewModel _viewModel = new();
        private R_Conductor? _conductor;
        [Inject] private IClientHelper? _clientHelper { get; set; }

        private R_Lookup? R_LookupDept;
        private R_Lookup? R_LookupTenant;
        private R_Lookup? R_LookupSalesman;
        private R_Lookup? R_LookupBillingRule;
        private R_Lookup? R_LookupTaxId;
        private R_Lookup? R_LookupCurrency;
        private R_TextBox? _componentTextBox;
        private bool _lEnableRefno;
        private bool _lViewMode;
        string billing_Rule_Type = "01";

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();
            try
            {
                var loTemp = poParameter;
                _viewModel.BookingParameter = R_FrontUtility.ConvertObjectToObject<PMT00100BookingDTO>(loTemp);
                string lcCALLER_ACTION = _viewModel.BookingParameter.CCALLER_ACTION!;
                _viewModel.LEnaledEditDelete = false;
                await _viewModel.GetVarTransactionCode();

                _lViewMode = lcCALLER_ACTION == "VIEW";

                if (lcCALLER_ACTION == "VIEW" || lcCALLER_ACTION == "BUTTON_VIEW")
                {
                    _viewModel.LEnaledEditDelete = new[] { "00", "10" }.Contains(_viewModel.BookingParameter.CTRANS_STATUS);
                    await _conductor.R_GetEntity(_viewModel.BookingParameter);
                }
                else if (lcCALLER_ACTION == "BUTTON_BOOKING")
                {
                    // NORMAL FROM PMT00100 itself
                    await _conductor.Add();
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        #region Button LOC
        private void BeforeOpenBookingLOC(R_BeforeOpenPopupEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                PMT00100BookingDTO loParam = R_FrontUtility.ConvertObjectToObject<PMT00100BookingDTO>(_viewModel.Data);
                eventArgs.Parameter = loParam;
                eventArgs.TargetPageType = typeof(PMT00100BookingLOC);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task AfterServiceBookingLOC(R_AfterOpenPopupEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                await Close(true, null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        #endregion
        private async Task ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            PMT00100BookingDTO loParam;

            try
            {
                loParam = new PMT00100BookingDTO();
                if (eventArgs.Data != null)
                {
                    loParam = R_FrontUtility.ConvertObjectToObject<PMT00100BookingDTO>(eventArgs.Data);
                }
                await _viewModel.GetEntity(loParam);
                eventArgs.Result = _viewModel.oEntityBooking;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void ServiceDisplay(R_DisplayEventArgs eventArgs)
        {
        }

        private async Task ServiceSave(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                PMT00100BookingDTO loParam = R_FrontUtility.ConvertObjectToObject<PMT00100BookingDTO>(eventArgs.Data);
                //TO SAVE LOO just Execute Maintain Agreement and Agreement Unit
                loParam.CMODE_CRUD = "1";
                await _viewModel.ServiceSave(loParam, (eCRUDMode)eventArgs.ConductorMode);
                eventArgs.Result = _viewModel.oEntityBooking;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task ServiceDelete(R_ServiceDeleteEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                bool plSuccess = false;
                var loData = (PMT00100BookingDTO)eventArgs.Data;
                if (_viewModel.oEntityBooking != null)
                {
                    await _viewModel.ServiceDelete(loData);
                    plSuccess = true;
                }
                await Close(plSuccess, null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task AfterDelete()
        {
            await R_MessageBox.Show("", @_localizer["MessageDelete"], R_eMessageBoxButtonType.OK);
        }
        private async void AfterSave(R_AfterSaveEventArgs eventArgs)
        {
            var loException = new R_Exception();
            try
            {
                await Close(true, "SUCCESS");

            }
            catch (Exception Ex)
            {
                loException.Add(Ex);
            }

            R_DisplayException(loException);
        }
        private void AfterAdd(R_AfterAddEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            var loData = (PMT00100BookingDTO)eventArgs.Data;

            try
            {
                _lEnableRefno = !_viewModel.VarTransaction.LINCREMENT_FLAG;
                loData.CPROPERTY_ID = _viewModel.BookingParameter.CPROPERTY_ID;
                loData.CTRANS_CODE = "802011";
                loData.CBUILDING_ID = _viewModel.BookingParameter.CBUILDING_ID;
                loData.CBUILDING_NAME = _viewModel.BookingParameter.CBUILDING_NAME;
                loData.CFLOOR_ID = _viewModel.BookingParameter.CFLOOR_ID;
                loData.CFLOOR_NAME = _viewModel.BookingParameter.CFLOOR_NAME;
                loData.CUNIT_ID = _viewModel.BookingParameter.CUNIT_ID;
                loData.CUNIT_NAME = _viewModel.BookingParameter.CUNIT_NAME;
                loData.CUNIT_TYPE_ID = _viewModel.BookingParameter.CUNIT_TYPE_ID;
                loData.CUNIT_TYPE_NAME = _viewModel.BookingParameter.CUNIT_TYPE_NAME;
                loData.NACTUAL_AREA_SIZE = 0;
                loData.NCOMMON_AREA_SIZE = 0;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            R_DisplayException(loException);
        }
        public async Task R_BeforeCancel(R_BeforeCancelEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();

            try
            {
                var res = await R_MessageBox.Show("", @_localizer["ValidationBeforeCancel"],
                    R_eMessageBoxButtonType.YesNo);

                if (res == R_eMessageBoxResult.No)
                {
                    eventArgs.Cancel = true;
                }
                else if (eventArgs.ConductorMode == R_eConductorMode.Add)
                {
                    await Close(false, false);
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            R_DisplayException(loException);
        }
        private void ServiceValidation(R_ValidationEventArgs eventArgs)
        {
            var loException = new R_Exception();

            try
            {
                var loData = (PMT00100BookingDTO)eventArgs.Data;
                _viewModel.ValidationFieldEmpty(loData);

            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            eventArgs.Cancel = loException.HasError;


            loException.ThrowExceptionIfErrors();
        }
        private void R_CheckEdit(R_CheckEditEventArgs eventArgs)
        {
            var loException = new R_Exception();
            //Task loTask;

            try
            {
                eventArgs.Allow = _viewModel.oEntityBooking.CTRANS_STATUS == "00";
            }
            catch (Exception Ex)
            {
                loException.Add(Ex);
            }

            R_DisplayException(loException);
        }
        private void R_CheckDelete(R_CheckDeleteEventArgs eventArgs)
        {
            var loException = new R_Exception();
            //Task loTask;

            try
            {
                eventArgs.Allow = _viewModel.oEntityBooking.CTRANS_STATUS == "00";
            }
            catch (Exception Ex)
            {
                loException.Add(Ex);
            }

            R_DisplayException(loException);
        }
        #region Lookup Button Department Lookup

        private void BeforeOpenLookUpDepartmentLookup(R_BeforeOpenLookupEventArgs eventArgs)
        {
            GSL00710ParameterDTO? param = null;
            if (!string.IsNullOrEmpty(_viewModel.BookingParameter.CPROPERTY_ID))
            {
                param = new GSL00710ParameterDTO
                {
                    CPROPERTY_ID = _viewModel.BookingParameter.CPROPERTY_ID,
                };
            }
            eventArgs.Parameter = param;
            eventArgs.TargetPageType = typeof(GSL00710);
        }

        private void AfterOpenLookUpDepartmentLookup(R_AfterOpenLookupEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            GSL00710DTO? loTempResult = null;
            try
            {
                loTempResult = (GSL00710DTO)eventArgs.Result;
                if (loTempResult == null)
                    return;
                _viewModel.Data.CDEPT_CODE = loTempResult.CDEPT_CODE;
                _viewModel.Data.CDEPT_NAME = loTempResult.CDEPT_NAME;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            R_DisplayException(loException);

        }

        private async Task OnLostFocusDepartment()
        {
            R_Exception loEx = new R_Exception();

            try
            {
                PMT00100BookingDTO loGetData = (PMT00100BookingDTO)_viewModel.Data;

                if (string.IsNullOrWhiteSpace(_viewModel.Data.CDEPT_CODE))
                {
                    loGetData.CDEPT_CODE = "";
                    loGetData.CDEPT_NAME = "";
                    return;
                }

                LookupGSL00710ViewModel loLookupViewModel = new LookupGSL00710ViewModel();
                GSL00710ParameterDTO loParam = new GSL00710ParameterDTO()
                {
                    CPROPERTY_ID = _viewModel.BookingParameter.CPROPERTY_ID!,
                    CSEARCH_TEXT = loGetData.CDEPT_CODE ?? "",
                };

                var loResult = await loLookupViewModel.GetDepartmentProperty(loParam);

                if (loResult == null)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                            typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                            "_ErrLookup01"));
                    loGetData.CDEPT_CODE = "";
                    loGetData.CDEPT_NAME = "";
                }
                else
                {
                    loGetData.CDEPT_CODE = loResult.CDEPT_CODE;
                    loGetData.CDEPT_NAME = loResult.CDEPT_NAME;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        #endregion
        #region Lookup Tenant
        private void BeforeOpenLookUpTenantLookup(R_BeforeOpenLookupEventArgs eventArgs)
        {
            LML00600ParameterDTO? param = null;
            if (!string.IsNullOrEmpty(_viewModel.BookingParameter.CPROPERTY_ID))
            {
                param = new LML00600ParameterDTO()
                {
                    CPROPERTY_ID = _viewModel.BookingParameter.CPROPERTY_ID,
                    CCUSTOMER_TYPE = "01",
                };
            }
            eventArgs.Parameter = param;
            eventArgs.TargetPageType = typeof(LML00600);
        }
        private void AfterOpenLookUpTenantLookupAsync(R_AfterOpenLookupEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            LML00600DTO? loTempResult = null;
            //PMT01100LOO_Offer_SelectedOfferDTO? loGetData = null;

            try
            {
                loTempResult = (LML00600DTO)eventArgs.Result;
                if (loTempResult == null)
                    return;
                //loGetData = (PMT01100LOO_Offer_SelectedOfferDTO)_conductorFullPMT01500Agreement.R_GetCurrentData();
                var loData = (PMT00100BookingDTO)_conductor!.R_GetCurrentData();
                _viewModel.Data.CTENANT_ID = loTempResult.CTENANT_ID;
                _viewModel.Data.CTENANT_NAME = loTempResult.CTENANT_NAME;

            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            R_DisplayException(loException);

        }
        private async Task OnLostFocusTenant()
        {
            R_Exception loEx = new R_Exception();

            try
            {
                PMT00100BookingDTO loGetData = _viewModel.Data;
                if (string.IsNullOrWhiteSpace(loGetData.CTENANT_ID))
                {
                    loGetData.CTENANT_ID = "";
                    loGetData.CTENANT_NAME = "";
                    return;
                }

                LookupLML00600ViewModel loLookupViewModel = new LookupLML00600ViewModel();
                LML00600ParameterDTO loParam = new LML00600ParameterDTO()
                {
                    CPROPERTY_ID = _viewModel.BookingParameter.CPROPERTY_ID,
                    CCUSTOMER_TYPE = "01",
                    CSEARCH_TEXT = loGetData.CTENANT_ID ?? "",
                };

                var loResult = await loLookupViewModel.GetTenant(loParam);
                var loData = (PMT00100BookingDTO)_conductor!.R_GetCurrentData();

                if (loResult == null)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                            typeof(Lookup_PMFrontResources.Resources_Dummy_Class_LookupPM),
                            "_ErrLookup01"));
                    loGetData.CTENANT_ID = "";
                    loGetData.CTENANT_NAME = "";
                }
                else
                {
                    loGetData.CTENANT_ID = loResult.CTENANT_ID;
                    loGetData.CTENANT_NAME = loResult.CTENANT_NAME;
                }

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        #endregion
        #region Lookup Button Salesman Lookup
        private void BeforeOpenLookUpSalesmanLookup(R_BeforeOpenLookupEventArgs eventArgs)
        {
            LML00500ParameterDTO? param = null;
            if (!string.IsNullOrEmpty(_viewModel.BookingParameter.CPROPERTY_ID))
            {
                param = new LML00500ParameterDTO
                {
                    CPROPERTY_ID = _viewModel.BookingParameter.CPROPERTY_ID,
                };
            }
            eventArgs.Parameter = param;
            eventArgs.TargetPageType = typeof(LML00500);
        }
        private void AfterOpenLookUpSalesmanLookup(R_AfterOpenLookupEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            LML00500DTO? loTempResult = null;
            //PMT01100LOO_Offer_SelectedOfferDTO? loGetData = null;


            try
            {
                loTempResult = (LML00500DTO)eventArgs.Result;
                if (loTempResult == null)
                    return;
                _viewModel.Data.CSALESMAN_ID = loTempResult.CSALESMAN_ID;
                _viewModel.Data.CSALESMAN_NAME = loTempResult.CSALESMAN_NAME;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            R_DisplayException(loException);

        }
        private async Task OnLostFocusSalesman()
        {
            R_Exception loEx = new R_Exception();

            try
            {
                PMT00100BookingDTO loGetData = (PMT00100BookingDTO)_viewModel.Data;

                if (string.IsNullOrWhiteSpace(_viewModel.Data.CSALESMAN_ID))
                {
                    loGetData.CSALESMAN_ID = "";
                    loGetData.CSALESMAN_NAME = "";
                    return;
                }

                LookupLML00500ViewModel loLookupViewModel = new LookupLML00500ViewModel();
                LML00500ParameterDTO loParam = new LML00500ParameterDTO()
                {
                    CPROPERTY_ID = _viewModel.BookingParameter.CPROPERTY_ID!,
                    CSEARCH_TEXT = loGetData.CSALESMAN_ID ?? "",
                };

                var loResult = await loLookupViewModel.GetSalesman(loParam);

                if (loResult == null)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                            typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                            "_ErrLookup01"));
                    loGetData.CSALESMAN_ID = "";
                    loGetData.CSALESMAN_NAME = "";
                    //await GLAccount_TextBox.FocusAsync();
                }
                else
                {
                    loGetData.CSALESMAN_ID = loResult.CSALESMAN_ID;
                    loGetData.CSALESMAN_NAME = loResult.CSALESMAN_NAME;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        #endregion
        #region Lookup Button Tax Code Lookup
        private void BeforeOpenLookUpTaxCodeLookup(R_BeforeOpenLookupEventArgs eventArgs)
        {
            GSL00110ParameterDTO? param = null;
            if (!string.IsNullOrEmpty(_viewModel.BookingParameter.CPROPERTY_ID))
            {
                param = new GSL00110ParameterDTO
                {
                    CCOMPANY_ID = _clientHelper.CompanyId,
                    CUSER_ID = _clientHelper.UserId,
                    CTAX_DATE = DateTime.Now.ToString("yyyyMMdd")
                };
            }
            eventArgs.Parameter = param;
            eventArgs.TargetPageType = typeof(GSL00110);
        }

        private void AfterOpenLookUpTaxCodeLookup(R_AfterOpenLookupEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            GSL00110DTO? loTempResult = null;
            //LMM01500AgreementDetailDTO? loGetData = null;


            try
            {
                loTempResult = (GSL00110DTO)eventArgs.Result;
                if (loTempResult == null)
                    return;

                //loGetData = (LMM01500AgreementDetailDTO)_conductorFullPMT02500Agreement.R_GetCurrentData();

                _viewModel.Data.CSTRATA_TAX_ID = loTempResult.CTAX_ID;
                _viewModel.Data.CSTRATA_TAX_NAME = loTempResult.CTAX_NAME;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            R_DisplayException(loException);

        }

        private async Task OnLostFocusTaxCode()
        {
            R_Exception loEx = new R_Exception();

            try
            {
                var loGetData = _viewModel.Data;

                if (string.IsNullOrWhiteSpace(_viewModel.Data.CSTRATA_TAX_ID))
                {
                    loGetData.CSTRATA_TAX_ID = "";
                    loGetData.CSTRATA_TAX_NAME = "";
                    return;
                }

                LookupGSL00110ViewModel loLookupViewModel = new LookupGSL00110ViewModel();
                GSL00110ParameterDTO loParam = new GSL00110ParameterDTO()
                {
                    CCOMPANY_ID = _clientHelper.CompanyId,
                    CUSER_ID = _clientHelper.UserId,
                    CTAX_DATE = DateTime.Now.ToString("yyyyMMdd"),
                    CSEARCH_TEXT = loGetData.CSTRATA_TAX_ID ?? "",
                };

                var loResult = await loLookupViewModel.GetTaxByDate(loParam);

                if (loResult == null)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                            typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                            "_ErrLookup01"));
                    loGetData.CSTRATA_TAX_ID = "";
                    loGetData.CSTRATA_TAX_NAME = "";
                    //await GLAccount_TextBox.FocusAsync();
                }
                else
                {
                    loGetData.CSTRATA_TAX_ID = loResult.CTAX_ID;
                    loGetData.CSTRATA_TAX_NAME = loResult.CTAX_NAME;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        #endregion
        #region Lookup Button BillingRule
        private void BeforeOpenLookUpBillingRuleLookup(R_BeforeOpenLookupEventArgs eventArgs)
        {
            LML01000ParameterDTO? param = null;
            if (!string.IsNullOrEmpty(_viewModel.BookingParameter.CPROPERTY_ID))
            {
                param = new LML01000ParameterDTO
                {
                    CPROPERTY_ID = _viewModel.BookingParameter.CPROPERTY_ID,
                    CUNIT_TYPE_CTG_ID = "",
                    CBILLING_RULE_TYPE = billing_Rule_Type,
                    LACTIVE_ONLY = true
                };
            }
            eventArgs.Parameter = param;
            eventArgs.TargetPageType = typeof(LML01000);
        }

        private void AfterOpenLookUpBillingRuleLookup(R_AfterOpenLookupEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            LML01000DTO? loTempResult = null;

            try
            {
                loTempResult = (LML01000DTO)eventArgs.Result;
                if (loTempResult == null)
                    return;


                _viewModel.Data.CBILLING_RULE_CODE = loTempResult.CBILLING_RULE_CODE;
                _viewModel.Data.CBILLING_RULE_NAME = loTempResult.CBILLING_RULE_NAME;
                _viewModel.Data.CBILLING_RULE_TYPE = billing_Rule_Type;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            R_DisplayException(loException);
        }

        private async Task OnLostBillingRule()
        {
            R_Exception loEx = new R_Exception();

            try
            {
                PMT00100BookingDTO loGetData = _viewModel.Data;

                if (string.IsNullOrWhiteSpace(_viewModel.Data.CBILLING_RULE_CODE))
                {
                    loGetData.CBILLING_RULE_CODE = "";
                    return;
                }

                LookupLML01000ViewModel loLookupViewModel = new LookupLML01000ViewModel();
                LML01000ParameterDTO loParam = new LML01000ParameterDTO()
                {
                    CPROPERTY_ID = _viewModel.BookingParameter.CPROPERTY_ID,
                    CUNIT_TYPE_CTG_ID = "",
                    CBILLING_RULE_TYPE = billing_Rule_Type,
                    LACTIVE_ONLY = true,
                    CSEARCH_TEXT = loGetData.CBILLING_RULE_CODE ?? "",
                };

                var loResult = await loLookupViewModel.GetBillingRule(loParam);

                if (loResult == null)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                            typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                            "_ErrLookup01"));
                    loGetData.CBILLING_RULE_CODE = "";
                    loGetData.CBILLING_RULE_NAME = "";
                }
                else
                {
                    loGetData.CBILLING_RULE_CODE = loResult.CBILLING_RULE_CODE;
                    loGetData.CBILLING_RULE_NAME = loResult.CBILLING_RULE_NAME;
                    loGetData.CBILLING_RULE_TYPE = billing_Rule_Type;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        #endregion
        #region Lookup Button Currency
        private void BeforeOpenLookUpCurrencyLookup(R_BeforeOpenLookupEventArgs eventArgs)
        {
            GSL00300ParameterDTO? param = null;
            if (!string.IsNullOrEmpty(_viewModel.BookingParameter.CPROPERTY_ID))
            {
                param = new GSL00300ParameterDTO
                {
                    CCOMPANY_ID = _clientHelper.CompanyId,
                    CUSER_ID = _clientHelper.UserId
                };
            }

            eventArgs.Parameter = param;
            eventArgs.TargetPageType = typeof(GSL00300);
        }

        private void AfterOpenLookUpCurrencyLookup(R_AfterOpenLookupEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            GSL00300DTO? loTempResult = null;

            try
            {
                loTempResult = (GSL00300DTO)eventArgs.Result;
                if (loTempResult == null)
                    return;


                _viewModel.Data.CCURRENCY_CODE = loTempResult.CCURRENCY_CODE;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            R_DisplayException(loException);
        }

        private async Task OnLostCurrencyRule()
        {
            R_Exception loEx = new R_Exception();

            try
            {
                PMT00100BookingDTO loGetData = _viewModel.Data;

                if (string.IsNullOrWhiteSpace(_viewModel.Data.CCURRENCY_CODE))
                {
                    loGetData.CCURRENCY_CODE = "";
                    return;
                }

                LookupGSL00300ViewModel loLookupViewModel = new LookupGSL00300ViewModel();
                GSL00300ParameterDTO loParam = new GSL00300ParameterDTO()
                {
                    CCOMPANY_ID = _clientHelper.CompanyId,
                    CUSER_ID = _clientHelper.UserId,
                    CSEARCH_TEXT = loGetData.CCURRENCY_CODE ?? "",
                };

                var loResult = await loLookupViewModel.GetCurrency(loParam);

                if (loResult == null)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                            typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                            "_ErrLookup01"));
                    loGetData.CCURRENCY_CODE = "";
                    //await GLAccount_TextBox.FocusAsync();
                }
                else
                {
                    loGetData.CCURRENCY_CODE = loResult.CCURRENCY_CODE;
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
