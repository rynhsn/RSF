using BlazorClientHelper;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using Lookup_GSModel.ViewModel;
using Lookup_PMCOMMON.DTOs.LML01000;
using Lookup_PMFRONT;
using Lookup_PMModel.ViewModel.LML01000;
using Microsoft.AspNetCore.Components;
using PMT00100COMMON.Booking;
using PMT00100COMMON.ChangeUnit;
using PMT00100MODEL.ViewModel;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMT00100FRONT
{
    public partial class PMT00100ChangeUnit : R_Page
    {
        private PMT00100ChangeUnitViewModel _viewModel = new();
        private R_Conductor? _conductor;
        [Inject] private IClientHelper? _clientHelper { get; set; }
        private R_Lookup? R_LookupBillingRule;
        private R_Lookup? R_LookupFloor;
        private R_Lookup? R_LookupUnit;
        private R_Lookup? R_LookupBuilding;
        string billing_Rule_Type = "01";

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();
            try
            {
                var loTemp = poParameter;
                _viewModel.Parameter = R_FrontUtility.ConvertObjectToObject<PMT00100ChangeUnitDTO>(loTemp);
                await _conductor.R_GetEntity(_viewModel.Parameter);
                await _conductor.Add();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            PMT00100ChangeUnitDTO loParam;

            try
            {
                loParam = new PMT00100ChangeUnitDTO();
                if (eventArgs.Data != null)
                {
                    loParam = R_FrontUtility.ConvertObjectToObject<PMT00100ChangeUnitDTO>(eventArgs.Data);
                }
                await _viewModel.GetEntity(loParam);
                eventArgs.Result = _viewModel.ChangeUnitValue;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task ServiceSave(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<PMT00100ChangeUnitDTO>(eventArgs.Data);
                await _viewModel.ServiceSave(loParam, (eCRUDMode)eventArgs.ConductorMode);
                eventArgs.Result = _viewModel.ChangeUnitValue;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
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
            var loTempData = (PMT00100ChangeUnitDTO)eventArgs.Data;
            try
            {
                loTempData.CPROPERTY_ID = _viewModel.Parameter.CPROPERTY_ID;
                loTempData.CDEPT_CODE = _viewModel.Parameter.CDEPT_CODE;
                loTempData.CTRANS_CODE = "802011";
                loTempData.CREF_NO = _viewModel.Parameter.CREF_NO;

                loTempData.CUNIT_TYPE_ID = _viewModel.Parameter.CUNIT_TYPE_ID;
                loTempData.CUNIT_TYPE_NAME = _viewModel.Parameter.CUNIT_TYPE_NAME;

                loTempData.NGROSS_AREA_SIZE = _viewModel.ChangeUnitValue.NGROSS_AREA_SIZE;
                loTempData.NNET_AREA_SIZE = _viewModel.ChangeUnitValue.NNET_AREA_SIZE;
                loTempData.NSELL_PRICE = _viewModel.ChangeUnitValue.NSELL_PRICE;
                loTempData.CBILLING_RULE_TYPE = "";//_viewModel.Parameter.CBILLING_RULE_TYPE;
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
                else
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
                var loData = (PMT00100ChangeUnitDTO)eventArgs.Data;
                _viewModel.ValidationFieldEmpty(loData);

            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            eventArgs.Cancel = loException.HasError;


            loException.ThrowExceptionIfErrors();
        }

        #region Lookup Button Building
        private void BeforeOpenLookUpBuilding(R_BeforeOpenLookupEventArgs eventArgs)
        {
            GSL02200ParameterDTO? param = null;
            if (!string.IsNullOrEmpty(_viewModel.Parameter.CPROPERTY_ID))
            {
                param = new GSL02200ParameterDTO
                {
                    CPROPERTY_ID = _viewModel.Parameter.CPROPERTY_ID,
                };
            }
            eventArgs.Parameter = param;
            eventArgs.TargetPageType = typeof(GSL02200);
        }

        private void AfterOpenLookUpBuilding(R_AfterOpenLookupEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            GSL02200DTO? loTempResult = null;

            try
            {
                loTempResult = (GSL02200DTO)eventArgs.Result;
                if (loTempResult == null)
                    return;


                _viewModel.Data.CBUILDING_ID = loTempResult.CBUILDING_ID;
                _viewModel.Data.CBUILDING_NAME = loTempResult.CBUILDING_NAME;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            R_DisplayException(loException);
        }

        private async Task OnLostBuilding()
        {
            R_Exception loEx = new R_Exception();

            try
            {
                PMT00100ChangeUnitDTO loGetData = _viewModel.Data;

                if (string.IsNullOrWhiteSpace(_viewModel.Data.CBUILDING_ID))
                {
                    loGetData.CBUILDING_ID = "";
                    return;
                }

                LookupGSL02200ViewModel loLookupViewModel = new LookupGSL02200ViewModel();
                GSL02200ParameterDTO loParam = new GSL02200ParameterDTO()
                {
                    CPROPERTY_ID = _viewModel.Parameter.CPROPERTY_ID,
                    CSEARCH_TEXT = loGetData.CBUILDING_ID ?? "",
                };

                var loResult = await loLookupViewModel.GetBuilding(loParam);

                if (loResult == null)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                            typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                            "_ErrLookup01"));
                    loGetData.CBUILDING_ID = "";
                    loGetData.CBILLING_RULE_NAME = "";
                }
                else
                {
                    loGetData.CBUILDING_ID = loResult.CBUILDING_ID;
                    loGetData.CBUILDING_NAME = loResult.CBUILDING_NAME;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        #endregion
        #region Lookup Button Unit
        private void BeforeOpenLookUpUnit(R_BeforeOpenLookupEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var param = new GSL02300ParameterDTO()
                {
                    CPROPERTY_ID = _viewModel.Parameter.CPROPERTY_ID!,
                    CBUILDING_ID = _viewModel.Data.CBUILDING_ID ?? "",
                    CUNIT_CATEGORY_LIST = "01,03",
                    CLEASE_STATUS_LIST = "01,02",
                    CREF_NO = _viewModel.Parameter.CREF_NO ?? "",
                    CPROGRAM_ID = "",
                    CTRANS_CODE ="802011",
                };
                eventArgs.Parameter = param;
                eventArgs.TargetPageType = typeof(GSL02300);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

        }
        private void AfterOpenLookUpUnit(R_AfterOpenLookupEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            GSL02300DTO? loTempResult = null;

            try
            {
                loTempResult = (GSL02300DTO)eventArgs.Result;
                if (loTempResult == null)
                    return;


                _viewModel.Data.CUNIT_ID = loTempResult.CUNIT_ID;
                _viewModel.Data.CUNIT_NAME = loTempResult.CUNIT_NAME;
                _viewModel.Data.CFLOOR_ID = loTempResult.CFLOOR_ID;
                _viewModel.Data.CFLOOR_NAME = loTempResult.CFLOOR_NAME;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            R_DisplayException(loException);
        }
        private async Task OnLostUnit()
        {
            R_Exception loEx = new R_Exception();

            try
            {
                PMT00100ChangeUnitDTO loGetData = _viewModel.Data;

                if (string.IsNullOrWhiteSpace(_viewModel.Data.CUNIT_ID))
                {
                    loGetData.CUNIT_ID = "";
                    return;
                }

                LookupGSL02300ViewModel loLookupViewModel = new LookupGSL02300ViewModel();
                GSL02300ParameterDTO loParam = new GSL02300ParameterDTO()
                {
                    CPROPERTY_ID = _viewModel.Parameter.CPROPERTY_ID!,
                    CBUILDING_ID = _viewModel.Parameter.CBUILDING_ID ?? "",
                    CUNIT_CATEGORY_LIST = "01,03",
                    CLEASE_STATUS_LIST = "01,02",
                    CREF_NO = _viewModel.Parameter.CREF_NO ?? "",
                    CPROGRAM_ID = "",
                    CTRANS_CODE = "802011",
                    CSEARCH_TEXT = loGetData.CUNIT_ID ?? "",

                };

                var loResult = await loLookupViewModel.GetBuildingUnit(loParam);

                if (loResult == null)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                            typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                            "_ErrLookup01"));
                    loGetData.CUNIT_ID = "";
                    loGetData.CUNIT_NAME = "";
                    loGetData.CFLOOR_ID = "";
                    loGetData.CFLOOR_NAME = "";
                }
                else
                {
                    loGetData.CUNIT_ID = loResult.CUNIT_ID;
                    loGetData.CUNIT_NAME = loResult.CUNIT_NAME;
                    _viewModel.Data.CFLOOR_ID = loResult.CFLOOR_ID;
                    _viewModel.Data.CFLOOR_NAME = loResult.CFLOOR_NAME;
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
            if (!string.IsNullOrEmpty(_viewModel.Parameter.CPROPERTY_ID))
            {
                param = new LML01000ParameterDTO
                {
                    CPROPERTY_ID = _viewModel.Parameter.CPROPERTY_ID,
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
                //_viewModel.Data.CBILLING_RULE_TYPE = loTempResult.CBi;
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
                PMT00100ChangeUnitDTO loGetData = _viewModel.Data;

                if (string.IsNullOrWhiteSpace(_viewModel.Data.CBILLING_RULE_CODE))
                {
                    loGetData.CBILLING_RULE_CODE = "";
                    return;
                }

                LookupLML01000ViewModel loLookupViewModel = new LookupLML01000ViewModel();
                LML01000ParameterDTO loParam = new LML01000ParameterDTO()
                {
                    CPROPERTY_ID = _viewModel.Parameter.CPROPERTY_ID,
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
