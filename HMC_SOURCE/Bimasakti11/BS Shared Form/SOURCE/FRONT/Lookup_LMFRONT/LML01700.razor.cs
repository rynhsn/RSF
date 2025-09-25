using Lookup_PMCOMMON.DTOs;
using Lookup_PMCOMMON.DTOs.LML01300;
using Lookup_PMCOMMON.DTOs.LML01600;
using Lookup_PMCOMMON.DTOs.LML01700;
using Lookup_PMModel.ViewModel.LML00600;
using Lookup_PMModel.ViewModel.LML00800;
using Lookup_PMModel.ViewModel.LML01600;
using Lookup_PMModel.ViewModel.LML01700;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lookup_PMFRONT
{
    public partial class LML01700 : R_Page
    {
        private LookupLML01700ViewModel _viewModel = new LookupLML01700ViewModel();
        private R_Grid<LML01700DTO> GridReceiptRef;
        private R_Grid<LML01700DTO> GridPrerequisiteRef;
        private int _pageSizeCancelReceipt = 8;
        private int _pageSizePrerequisite = 5;
        private R_Conductor? _conductor;


        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                _viewModel._Parameter = (LML01700ParameterDTO)poParameter;
                await _viewModel.GetInitialProcess();
                _viewModel.GetMonth();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task BtnRefresh()
        {
            var loEx = new R_Exception();
            try
            {
                _viewModel.ValidationFieldEmpty();
                LML01700ParameterDTO loParam = _viewModel._Parameter;

                string lcPeriodYear = string.IsNullOrWhiteSpace(_viewModel._PeriodYear.ToString()) ? "" : _viewModel._PeriodYear.ToString();
                var lPeriod = lcPeriodYear + _viewModel._PeriodMonth;

                loParam.CPERIOD = lPeriod;

                await GridReceiptRef!.R_RefreshGrid(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        public async Task R_ServiceGetListCancelReceipAsync(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = (LML01700ParameterDTO)eventArgs.Parameter;
                await _viewModel.GetCancelReceiptFromCustomerList(loParam);
                eventArgs.ListEntityResult = _viewModel._GetListCancelReceiptFromCustomer;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private async Task R_DisplayCancelReceiptAsync(R_DisplayEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            LML01700DTO loData = (LML01700DTO)eventArgs.Data;

            try
            {
                _viewModel._CancelReceiptFromCustomerData = loData;
                if (!string.IsNullOrEmpty(loData.CREC_ID))
                {
                    LML01700ParameterDTO loParam = new LML01700ParameterDTO
                    {
                        CRECEIPT_ID = loData.CREC_ID
                    };
                    await GridPrerequisiteRef.R_RefreshGrid(loParam);
                }
                else
                {
                    _viewModel._GetListPrerequisiteCustomer = new();
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task R_ServiceGetListPrerequisiteAsync(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = (LML01700ParameterDTO)eventArgs.Parameter;
                await _viewModel.GetLML01700PrerequisiteCustReceiptList(loParam);
                eventArgs.ListEntityResult = _viewModel._GetListPrerequisiteCustomer;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        public async Task Button_OnClickOkAsync()
        {
            var loData = GridReceiptRef.GetCurrentData();
            if (_viewModel._GetListPrerequisiteCustomer.Count < 1)
            {
                await this.Close(true, loData);
            }
            else
            {
                _viewModel.ValidationProcess();
            }
        }
        public async Task Button_OnClickCloseAsync()
        {
            await this.Close(true, null);
        }
        private void ChangePeriodMonth(object poParam)
        {
            var loEx = new R_Exception();
            string lcPeriodMonth = (string)poParam;
            try
            {
                _viewModel._PeriodMonth = lcPeriodMonth;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }
        #region Lookup Tenant
        private R_Lookup? R_LookupTenantLookup;
        private void BeforeOpenLookUpTenantLookup(R_BeforeOpenLookupEventArgs eventArgs)
        {
            LML00600ParameterDTO? param = null;
            if (!string.IsNullOrEmpty(_viewModel._Parameter.CPROPERTY_ID))
            {
                param = new LML00600ParameterDTO()
                {
                    CPROPERTY_ID = _viewModel._Parameter.CPROPERTY_ID,
                    CCUSTOMER_TYPE = "",
                };
            }
            eventArgs.Parameter = param;
            eventArgs.TargetPageType = typeof(LML00600);
        }
        private void AfterOpenLookUpTenantLookupAsync(R_AfterOpenLookupEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            LML00600DTO? loTempResult = null;
            try
            {
                loTempResult = (LML00600DTO)eventArgs.Result;
                var loGetData = _viewModel._Parameter;

                if (loTempResult == null)
                    return;
                _viewModel._Parameter.CCUSTOMER_ID = loTempResult.CTENANT_ID;
                _viewModel._Parameter.CCUSTOMER_NAME = loTempResult.CTENANT_NAME;

                loGetData.CLOI_AGRMT_ID = "";
                loGetData.CREC_ID = "";
                _viewModel._GetListCancelReceiptFromCustomer = new();
                _viewModel._GetListPrerequisiteCustomer = new();

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
                var loGetData = _viewModel._Parameter;
                if (string.IsNullOrWhiteSpace(loGetData.CCUSTOMER_ID))
                {
                    loGetData.CCUSTOMER_ID = "";
                    loGetData.CCUSTOMER_NAME = "";
                    loGetData.CLOI_AGRMT_ID = "";
                    return;
                }
                LookupLML00600ViewModel loLookupViewModel = new LookupLML00600ViewModel();
                LML00600ParameterDTO loParam = new LML00600ParameterDTO()
                {
                    CPROPERTY_ID = loGetData.CPROPERTY_ID!,
                    CCUSTOMER_TYPE = "",
                    CSEARCH_TEXT = loGetData.CCUSTOMER_ID ?? "",
                };
                var loResult = await loLookupViewModel.GetTenant(loParam);
                if (loResult == null)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                            typeof(Lookup_PMFrontResources.Resources_Dummy_Class_LookupPM),
                            "_ErrLookup01"));
                    loGetData.CCUSTOMER_ID = "";
                    loGetData.CCUSTOMER_NAME = "";
                }
                else
                {
                    loGetData.CCUSTOMER_ID = loResult.CTENANT_ID;
                    loGetData.CCUSTOMER_NAME = loResult.CTENANT_NAME;
                }

                loGetData.CLOI_AGRMT_ID = "";
                loGetData.CREC_ID = "";
                _viewModel._GetListCancelReceiptFromCustomer = new();
                _viewModel._GetListPrerequisiteCustomer = new();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }
        #endregion
        #region lookupAgreement
        private R_Lookup? R_LookupAgreementLookup;
        private void BeforeOpen_lookupAgreement(R_BeforeOpenLookupEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                var loData = _viewModel._Parameter;
                LML00800ParameterDTO? loParam = new();

                if (!string.IsNullOrEmpty(_viewModel._Parameter.CPROPERTY_ID))
                {
                    loParam = new LML00800ParameterDTO
                    {
                        CPROPERTY_ID = loData.CPROPERTY_ID!,
                        CDEPT_CODE = "",
                        CAGGR_STTS = "",
                        CTRANS_CODE = "991000",
                        CREF_NO = "",
                        CTENANT_ID = loData.CCUSTOMER_ID!,
                        CBUILDING_ID = "",
                        CTRANS_STATUS = "30,80",
                    };
                }
                if (string.IsNullOrEmpty(loParam.CTENANT_ID))
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                             typeof(Lookup_PMFrontResources.Resources_Dummy_Class_LookupPM),
                              "_ValidationTenant"));
                };
                if (!string.IsNullOrEmpty(loParam.CTENANT_ID) && !string.IsNullOrEmpty(loParam.CPROPERTY_ID))
                {
                    eventArgs.Parameter = loParam;
                    eventArgs.TargetPageType = typeof(LML00800);
                }

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();

        }
        private void AfterOpen_lookupAgreement(R_AfterOpenLookupEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                var loTempResult = (LML00800DTO)eventArgs.Result;
                if (loTempResult == null)
                {
                    return;
                }
                _viewModel._Parameter.CLOI_AGRMT_ID = loTempResult.CREF_NO;
                _viewModel._Parameter.CREC_ID = loTempResult.CREC_ID;
                _viewModel._GetListCancelReceiptFromCustomer = new();
                _viewModel._GetListPrerequisiteCustomer = new();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();

        }
        private async Task OnLostFocus_LookupAgreement()
        {
            var loEx = new R_Exception();

            try
            {
                var loData = _viewModel._Parameter;// ()_conductorRef.R_GetCurrentData();
                if (string.IsNullOrWhiteSpace(loData.CLOI_AGRMT_ID))
                {
                    loData.CLOI_AGRMT_ID = "";
                    loData.CREC_ID = "";
                    return;
                }
                LookupLML00800ViewModel loLookupViewModel = new LookupLML00800ViewModel();
                LML00800ParameterDTO loParam = new LML00800ParameterDTO()
                {
                    CPROPERTY_ID = loData.CPROPERTY_ID ?? "",
                    CDEPT_CODE = "",
                    CAGGR_STTS = "",
                    CTRANS_CODE = "991000",
                    CREF_NO = "",
                    CTENANT_ID = loData.CCUSTOMER_ID ?? "",
                    CBUILDING_ID = "",
                    CTRANS_STATUS = "30,80"
                };
                var loResult = await loLookupViewModel.GetAgreement(loParam);

                if (loResult == null)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                            typeof(Lookup_PMFrontResources.Resources_Dummy_Class_LookupPM),
                            "_ErrLookup01"));
                    loData.CLOI_AGRMT_ID = "";
                    loData.CREC_ID = "";
                }
                else
                {
                    loData.CLOI_AGRMT_ID = loResult.CREF_NO;
                    loData.CREC_ID = loResult.CREC_ID;
                }
                _viewModel._GetListCancelReceiptFromCustomer = new();
                _viewModel._GetListPrerequisiteCustomer = new();
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
