
using BlazorClientHelper;
using GFF00900COMMON.DTOs;
using PMM06000COMMON;
using PMM06000Model.ViewModel;
using Lookup_PMCOMMON.DTOs;
using Lookup_PMFRONT;
using Lookup_PMModel.ViewModel.LML00200;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Enums;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Controls.Popup;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using R_LockingFront;
using System.ComponentModel.DataAnnotations;
using System.Transactions;

namespace PMM06000Front
{
    public partial class PMM06000
    {
        private R_Conductor _conductorPropertyRef;
        private LMM06000ViewModel BillingRuleViewModel = new();

        private R_ConductorGrid? _conductorUnitTypeRef;
        private R_Grid<LMM06000UnitTypeDTO> _gridUnitTypeRef;

        private R_Conductor? _conductorBillingRuleRef;
        private R_Grid<LMM06000BillingRuleDTO>? _gridBillingRuleRef;

        private string loLabel = "Activate";
        private R_TextBox FocusLabelAdd;
        private R_TextBox FocusLabelEdit;
        [Inject] IClientHelper clientHelper { get; set; }
        [Inject] public R_PopupService PopupService { get; set; }


        #region PropertyID
        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();
            try
            {
                await PropertyDropdown_ServiceGetListRecord(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task PropertyDropdown_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                await BillingRuleViewModel.GetPropertyList();
                await _gridUnitTypeRef.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }

        private async Task PropertyDropdown_OnChange(object poParam)
        {
            var loEx = new R_Exception();
            string lsProperty = (string)poParam;
            try
            {
                BillingRuleViewModel.PropertyValueContext = lsProperty;
                await _gridUnitTypeRef.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        #endregion

        #region EnableGrid
        private bool _gridEnabled = true;
        private void ServiceSetOther(R_SetEventArgs eventArgs)
        {
            var temp = BillingRuleViewModel.UnitTypeList.Count();
            if (BillingRuleViewModel.UnitTypeList.Count() < 1)
            {
                BillingRuleViewModel._IsButtonAddEnable = false;
            }
            else
            {
                BillingRuleViewModel._IsButtonAddEnable = (eventArgs.Enable);
            }
            _gridEnabled = eventArgs.Enable;

        }
        #endregion

        #region CheckboxChanging
        private void CheckboxBookingFee(object poParam)
        {
            var loEx = new R_Exception();
            bool lbCheckbox = (bool)poParam;
            try
            {
                #region BookingFee
                //when Booking fee have value
                if (!string.IsNullOrEmpty(BillingRuleViewModel.Data.CBOOKING_FEE_CHARGE_ID))
                {
                    BillingRuleViewModel._IsDataNull = false;

                    BillingRuleViewModel.TemporaryBillingRuleDetail = new LMM06000BillingRuleDetailDTO()
                    {
                        CBOOKING_FEE_CHARGE_ID = BillingRuleViewModel.Data.CBOOKING_FEE_CHARGE_ID,
                        CCHARGES_NAME = BillingRuleViewModel.Data.CCHARGES_NAME,
                        NMIN_BOOKING_FEE = BillingRuleViewModel.Data.NMIN_BOOKING_FEE,
                        LBOOKING_FEE_OVERWRITE = BillingRuleViewModel.Data.LBOOKING_FEE_OVERWRITE
                    };
                }
                if (!lbCheckbox)
                {
                    BillingRuleViewModel._plBookingFee = false;
                    BillingRuleViewModel.Data.CBOOKING_FEE_CHARGE_ID = "";
                    BillingRuleViewModel.Data.CCHARGES_NAME = "";
                    BillingRuleViewModel.Data.NMIN_BOOKING_FEE = 0;
                }
                else
                {
                    BillingRuleViewModel._plBookingFee = true;
                }
                //re assign value from user
                if (lbCheckbox && !BillingRuleViewModel._IsDataNull)
                {
                    BillingRuleViewModel._plBookingFee = true;
                    var loTempDt = BillingRuleViewModel.TemporaryBillingRuleDetail;

                    BillingRuleViewModel.Data.CBOOKING_FEE_CHARGE_ID = loTempDt.CBOOKING_FEE_CHARGE_ID;
                    BillingRuleViewModel.Data.CCHARGES_NAME = loTempDt.CCHARGES_NAME;
                    BillingRuleViewModel.Data.NMIN_BOOKING_FEE = loTempDt.NMIN_BOOKING_FEE;
                    BillingRuleViewModel.Data.LBOOKING_FEE_OVERWRITE = loTempDt.LBOOKING_FEE_OVERWRITE;
                }
                #endregion
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }
        private void CheckboxBankCredit(object poParam)
        {
            var loEx = new R_Exception();
            bool lbCheckbox = (bool)poParam;
            try
            {
                #region BankCredit
                if ((BillingRuleViewModel.Data.IBANK_CREDIT_PERCENTAGE) != 0 ||
                    (BillingRuleViewModel.Data.IBANK_CREDIT_INTERVAL) != 0)
                {
                    BillingRuleViewModel._IsDataNull = false;

                    BillingRuleViewModel.TemporaryBillingRuleDetail = new LMM06000BillingRuleDetailDTO()
                    {
                        IBANK_CREDIT_PERCENTAGE = BillingRuleViewModel.Data.IBANK_CREDIT_PERCENTAGE,
                        IBANK_CREDIT_INTERVAL = BillingRuleViewModel.Data.IBANK_CREDIT_INTERVAL
                    };
                }
                if (!lbCheckbox)
                {
                    BillingRuleViewModel._plBankCredit = false;
                    BillingRuleViewModel.Data.IBANK_CREDIT_PERCENTAGE = 0;
                    BillingRuleViewModel.Data.IBANK_CREDIT_INTERVAL = 0;
                }
                else
                {
                    BillingRuleViewModel._plBankCredit = true;
                }
                //re assign value from user
                if (lbCheckbox && !BillingRuleViewModel._IsDataNull)
                {
                    BillingRuleViewModel._plBankCredit = true;
                    var loTempDt = BillingRuleViewModel.TemporaryBillingRuleDetail;

                    BillingRuleViewModel.Data.IBANK_CREDIT_PERCENTAGE = loTempDt.IBANK_CREDIT_PERCENTAGE;
                    BillingRuleViewModel.Data.IBANK_CREDIT_INTERVAL = loTempDt.IBANK_CREDIT_INTERVAL;
                }
                #endregion
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        private void CheckboxWithDP(object poParam)
        {
            var loEx = new R_Exception();
            bool lbCheckbox = (bool)poParam;
            try
            {
                #region WithDP
                //when period have period
                if (!string.IsNullOrEmpty(BillingRuleViewModel.Data.CDP_PERIOD_MODE) ||
                    !string.IsNullOrEmpty(BillingRuleViewModel.Data.CDP_CHARGE_ID))
                {
                    BillingRuleViewModel._IsDataNull = false;

                    BillingRuleViewModel.TemporaryBillingRuleDetail = new LMM06000BillingRuleDetailDTO()
                    {
                        IDP_PERCENTAGE = BillingRuleViewModel.Data.IDP_PERCENTAGE,
                        IDP_INTERVAL = BillingRuleViewModel.Data.IDP_INTERVAL,
                        CDP_PERIOD_MODE = BillingRuleViewModel.Data.CDP_PERIOD_MODE,
                        CDP_CHARGE_ID = BillingRuleViewModel.Data.CDP_CHARGE_ID,
                        CDP_CHARGE_NAME = BillingRuleViewModel.Data.CDP_CHARGE_NAME
                    };
                }

                if (!lbCheckbox)
                {
                    BillingRuleViewModel._plWithDP = false;

                    BillingRuleViewModel.Data.IDP_PERCENTAGE = 0;
                    BillingRuleViewModel.Data.IDP_INTERVAL = 0;
                    BillingRuleViewModel.Data.CDP_PERIOD_MODE = "";
                    BillingRuleViewModel.Data.CDP_CHARGE_ID = "";
                    BillingRuleViewModel.Data.CDP_CHARGE_NAME = "";
                }
                //re assign value from user
                else
                {
                    BillingRuleViewModel._plWithDP = true;
                }
                if (lbCheckbox && !BillingRuleViewModel._IsDataNull)
                {
                    BillingRuleViewModel._plWithDP = true;

                    var loTempDt = BillingRuleViewModel.TemporaryBillingRuleDetail;
                    BillingRuleViewModel.Data.IDP_PERCENTAGE = loTempDt.IDP_PERCENTAGE;
                    BillingRuleViewModel.Data.IDP_INTERVAL = loTempDt.IDP_INTERVAL;
                    BillingRuleViewModel.Data.CDP_PERIOD_MODE = loTempDt.CDP_PERIOD_MODE;
                    BillingRuleViewModel.Data.CDP_CHARGE_ID = loTempDt.CDP_CHARGE_ID;
                    BillingRuleViewModel.Data.CDP_CHARGE_NAME = loTempDt.CDP_CHARGE_NAME;

                }
                #endregion
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        private void CheckboxInstallment(object poParam)
        {
            var loEx = new R_Exception();
            bool lbCheckbox = (bool)poParam;
            try
            {
                #region Installment
                //when this group box have value
                if (!string.IsNullOrEmpty(BillingRuleViewModel.Data.CINSTALLMENT_PERIOD_MODE) ||
                !string.IsNullOrEmpty(BillingRuleViewModel.Data.CINSTALLMENT_CHARGE_ID))
                {
                    BillingRuleViewModel._IsDataNull = false;

                    BillingRuleViewModel.TemporaryBillingRuleDetail = new LMM06000BillingRuleDetailDTO()
                    {
                        IINSTALLMENT_PERCENTAGE = BillingRuleViewModel.Data.IINSTALLMENT_PERCENTAGE,
                        IINSTALLMENT_INTERVAL = BillingRuleViewModel.Data.IINSTALLMENT_INTERVAL,
                        CINSTALLMENT_PERIOD_MODE = BillingRuleViewModel.Data.CINSTALLMENT_PERIOD_MODE,
                        CINSTALLMENT_CHARGE_ID = BillingRuleViewModel.Data.CINSTALLMENT_CHARGE_ID,
                        CINSTALLMENT_CHARGE_NAME = BillingRuleViewModel.Data.CINSTALLMENT_CHARGE_NAME
                    };
                }

                if (!lbCheckbox)
                {
                    BillingRuleViewModel._plInstallment = true;
                    BillingRuleViewModel.Data.IINSTALLMENT_PERCENTAGE = 0;
                    BillingRuleViewModel.Data.IINSTALLMENT_INTERVAL = 0;
                    BillingRuleViewModel.Data.CINSTALLMENT_PERIOD_MODE = "";
                    BillingRuleViewModel.Data.CINSTALLMENT_CHARGE_ID = "";
                    BillingRuleViewModel.Data.CINSTALLMENT_CHARGE_NAME = "";
                }
                else
                {
                    BillingRuleViewModel._plInstallment = true;
                }
                //re assign value from user
                if (lbCheckbox && !BillingRuleViewModel._IsDataNull)
                {
                    var loTempDt = BillingRuleViewModel.TemporaryBillingRuleDetail;
                    BillingRuleViewModel.Data.IINSTALLMENT_PERCENTAGE = loTempDt.IINSTALLMENT_PERCENTAGE;
                    BillingRuleViewModel.Data.IINSTALLMENT_INTERVAL = loTempDt.IINSTALLMENT_INTERVAL;
                    BillingRuleViewModel.Data.CINSTALLMENT_PERIOD_MODE = loTempDt.CINSTALLMENT_PERIOD_MODE;
                    BillingRuleViewModel.Data.CINSTALLMENT_CHARGE_ID = loTempDt.CINSTALLMENT_CHARGE_ID;
                    BillingRuleViewModel.Data.CINSTALLMENT_CHARGE_NAME = loTempDt.CINSTALLMENT_CHARGE_NAME;
                }
                #endregion
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        #endregion

        private void ServiceBeforeCancel(R_BeforeCancelEventArgs eventArgs)
        {
            //disable when user click edit then user click cancel not save
            ///1 BillingRuleViewModel._IsButtonAddEnable = true;

            BillingRuleViewModel._IsDataNull = true;
        }

        #region UnitType
        private async Task GetListRecordUnitType(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                await BillingRuleViewModel.GetAllUnitType();
                eventArgs.ListEntityResult = BillingRuleViewModel.UnitTypeList;
                if (BillingRuleViewModel.UnitTypeList.Count() < 1)
                {
                    //disable when, unit type didn't have data
                    BillingRuleViewModel._IsButtonAddEnable = false;
                    BillingRuleViewModel.UnitTypeValueContext = "";
                    await _gridBillingRuleRef.R_RefreshGrid(null);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task Grid_DisplayUnitType(R_DisplayEventArgs eventArgs)
        {
            if (eventArgs.ConductorMode == R_eConductorMode.Normal)
            {
                var loParam = (LMM06000UnitTypeDTO)eventArgs.Data;
                BillingRuleViewModel.PropertyValueContext = loParam.CPROPERTY_ID;
                BillingRuleViewModel.UnitTypeValueContext = loParam.CUNIT_TYPE_CATEGORY_ID;
                await _gridBillingRuleRef.R_RefreshGrid(null);
            }
        }
        #endregion

        #region BillingRuleSRATA
        private async Task GetListRecordBillingRules(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                await BillingRuleViewModel.GetAllBillingRule();
                await PropertyDropdown_GetPeriodeList(null);
                eventArgs.ListEntityResult = BillingRuleViewModel.BillingRuleList;

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task ServiceGetOneRecordBillingRule(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<LMM06000BillingRuleDetailDTO>(eventArgs.Data);

                await BillingRuleViewModel.GetBillingRuleOneRecord(loParam);

                eventArgs.Result = BillingRuleViewModel.BillingRuleDetail;
                var temp = (LMM06000BillingRuleDetailDTO)eventArgs.Result;

                //Set ActiveInactive Value
                BillingRuleViewModel.ActiveInactiveEntity.PROPERTY_ID = temp.CPROPERTY_ID;
                BillingRuleViewModel.ActiveInactiveEntity.CUNIT_TYPE_ID = temp.CUNIT_TYPE_CATEGORY_ID;
                BillingRuleViewModel.ActiveInactiveEntity.CBILLING_RULE_CODE = temp.CBILLING_RULE_CODE;

                if (loParam.LACTIVE)
                {
                    loLabel = "Inactive";
                    BillingRuleViewModel.ActiveInactiveEntity.LACTIVE = false;
                }
                else
                {
                    loLabel = "Activate";
                    BillingRuleViewModel.ActiveInactiveEntity.LACTIVE = true;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task ServiceR_Display(R_DisplayEventArgs eventArgs)
        {
            var loException = new R_Exception();

            try
            {
                switch (eventArgs.ConductorMode)
                {
                    case R_eConductorMode.Edit:
                        //Focus Async
                        await FocusLabelEdit.FocusAsync();
                        BillingRuleViewModel._plBookingFee = BillingRuleViewModel.Data.LBOOKING_FEE;
                        BillingRuleViewModel._plWithDP = BillingRuleViewModel.Data.LWITH_DP;
                        BillingRuleViewModel._plInstallment = BillingRuleViewModel.Data.LINSTALLMENT;
                        BillingRuleViewModel._plBankCredit = BillingRuleViewModel.Data.LBANK_CREDIT;
                        break;

                    case R_eConductorMode.Normal:
                        BillingRuleViewModel._plBookingFee = false;
                        BillingRuleViewModel._plWithDP = false;
                        BillingRuleViewModel._plInstallment = false;
                        BillingRuleViewModel._plBankCredit = false;
                        break;
                }

            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();
        }
        private async Task PropertyDropdown_GetPeriodeList(R_ServiceGetListRecordEventArgs args)
        {
            var loEx = new R_Exception();
            try
            {
                await BillingRuleViewModel.GetPeriodList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }
        #endregion

        #region BeforeLookup
        private R_Lookup R_Lookup_Unit_Charges_Button;
        //BEFORE LOOKUP INSTALLMENT DAN BOOKING FEE

        private void BeforeOpenLookUp_Unit_Charges(R_BeforeOpenLookupEventArgs eventArgs)
        {

            var param = new LML00200ParameterDTO()
            {
                CCOMPANY_ID = clientHelper.CompanyId,
                CUSER_ID = clientHelper.UserId,
                CPROPERTY_ID = BillingRuleViewModel.PropertyValueContext,
                CCHARGE_TYPE_ID = ""
            };
            eventArgs.Parameter = param;
            eventArgs.TargetPageType = typeof(LML00200);
        }

        //Before LookUp WITHDP
        private void BeforeOpenLookUp_Unit_Charges_WithDP(R_BeforeOpenLookupEventArgs eventArgs)
        {
            var param = new LML00200ParameterDTO()
            {
                CCOMPANY_ID = clientHelper.CompanyId,
                CUSER_ID = clientHelper.UserId,
                CPROPERTY_ID = BillingRuleViewModel.PropertyValueContext,
                CCHARGE_TYPE_ID = "02,06,07"
            };
            eventArgs.Parameter = param;
            eventArgs.TargetPageType = typeof(LML00200);
        }

        #endregion
        #region OnLostFocus

        private async Task LostFocusLookupBooking()
        {
            var loEx = new R_Exception();

            try
            {
                var loGetData = BillingRuleViewModel.Data;
                if (!string.IsNullOrWhiteSpace(loGetData.CBOOKING_FEE_CHARGE_ID))
                {
                    LookupLML00200ViewModel loLookupViewModel = new LookupLML00200ViewModel();
                    var param = new LML00200ParameterDTO
                    {
                        CCOMPANY_ID = clientHelper.CompanyId,
                        CPROPERTY_ID = BillingRuleViewModel.PropertyValueContext,
                        CUSER_ID = clientHelper.UserId,
                        CCHARGE_TYPE_ID = "",
                        CSEARCH_TEXT = BillingRuleViewModel.Data.CBOOKING_FEE_CHARGE_ID!,
                    };
                    var loResult = await loLookupViewModel.GetUnitCharges(param);

                    if (loResult == null)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                                typeof(Lookup_PMFrontResources.Resources_Dummy_Class_LookupPM),
                                "_ErrLookup01"));
                        loGetData.CBOOKING_FEE_CHARGE_ID = "";
                        loGetData.CCHARGES_NAME = "";
                    }
                    else
                    {
                        loGetData.CBOOKING_FEE_CHARGE_ID = loResult.CCHARGES_ID;
                        loGetData.CCHARGES_NAME = loResult.CCHARGES_NAME;
                    }
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        private async Task LostFocusLookupDP()
        {
            var loEx = new R_Exception();

            try
            {
                var loGetData = BillingRuleViewModel.Data;

                if (!string.IsNullOrWhiteSpace(loGetData.CDP_CHARGE_ID))
                {
                    LookupLML00200ViewModel loLookupViewModel = new LookupLML00200ViewModel();
                    var param = new LML00200ParameterDTO
                    {
                        CCOMPANY_ID = clientHelper.CompanyId,
                        CPROPERTY_ID = BillingRuleViewModel.PropertyValueContext,
                        CUSER_ID = clientHelper.UserId,
                        CCHARGE_TYPE_ID = "02,06,07",
                        CSEARCH_TEXT = BillingRuleViewModel.Data.CDP_CHARGE_ID!,
                    };
                    var loResult = await loLookupViewModel.GetUnitCharges(param);

                    if (loResult == null)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                                typeof(Lookup_PMFrontResources.Resources_Dummy_Class_LookupPM),
                                "_ErrLookup01"));
                        loGetData.CDP_CHARGE_ID = "";
                        loGetData.CDP_CHARGE_NAME = "";
                        //await GLAccount_TextBox.FocusAsync();
                    }
                    else
                    {
                        loGetData.CDP_CHARGE_ID = loResult.CCHARGES_ID;
                        loGetData.CDP_CHARGE_NAME = loResult.CCHARGES_NAME;
                    }
                }

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        private async Task LostFocusLookupInstallment()
        {
            var loEx = new R_Exception();

            try
            {
                var loGetData = BillingRuleViewModel.Data;
                if (!string.IsNullOrWhiteSpace(loGetData.CINSTALLMENT_CHARGE_ID))
                {
                    LookupLML00200ViewModel loLookupViewModel = new LookupLML00200ViewModel();
                    var param = new LML00200ParameterDTO
                    {
                        CCOMPANY_ID = clientHelper.CompanyId,
                        CPROPERTY_ID = BillingRuleViewModel.PropertyValueContext,
                        CUSER_ID = clientHelper.UserId,
                        CCHARGE_TYPE_ID = "",
                        CSEARCH_TEXT = BillingRuleViewModel.Data.CINSTALLMENT_CHARGE_ID!,
                    };
                    var loResult = await loLookupViewModel.GetUnitCharges(param);

                    if (loResult == null)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                                typeof(Lookup_PMFrontResources.Resources_Dummy_Class_LookupPM),
                                "_ErrLookup01"));
                        loGetData.CINSTALLMENT_CHARGE_ID = "";
                        loGetData.CINSTALLMENT_CHARGE_NAME = "";
                    }
                    else
                    {
                        loGetData.CINSTALLMENT_CHARGE_ID = loResult.CCHARGES_ID;
                        loGetData.CINSTALLMENT_CHARGE_NAME = loResult.CCHARGES_NAME;
                    }
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        #endregion

        #region AfterLOOKUP
        //After LookUp Booking Fee
        private void AfterOpenLookUp_Unit_ChargesBookingFee(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loTempResult = (LML00200DTO)eventArgs.Result;
            if (loTempResult == null)
                return;

            var loGetData = (LMM06000BillingRuleDetailDTO)_conductorBillingRuleRef.R_GetCurrentData();

            loGetData.CBOOKING_FEE_CHARGE_ID = loTempResult.CCHARGES_ID;
            loGetData.CCHARGES_NAME = loTempResult.CCHARGES_NAME;
        }

        //After Lookup Installment
        private void AfterOpenLookUp_Unit_ChargesWithInstallment(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loTempResult = (LML00200DTO)eventArgs.Result;
            if (loTempResult == null)
                return;

            var loGetData = (LMM06000BillingRuleDetailDTO)_conductorBillingRuleRef.R_GetCurrentData();

            loGetData.CINSTALLMENT_CHARGE_ID = loTempResult.CCHARGES_ID;
            loGetData.CINSTALLMENT_CHARGE_NAME = loTempResult.CCHARGES_NAME;

        }

        //After Lookup Unit Charges With DP
        private void AfterOpenLookUp_Unit_ChargesWithDP(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loTempResult = (LML00200DTO)eventArgs.Result;
            if (loTempResult == null)
                return;

            var loGetData = (LMM06000BillingRuleDetailDTO)_conductorBillingRuleRef.R_GetCurrentData();

            loGetData.CDP_CHARGE_ID = loTempResult.CCHARGES_ID;
            loGetData.CDP_CHARGE_NAME = loTempResult.CCHARGES_NAME;

        }
        #endregion



        #region ADD and Edit
        private async Task AfterAdd(R_AfterAddEventArgs eventArgs)
        {
            eventArgs.Data = new LMM06000BillingRuleDetailDTO()
            {
                CUNIT_TYPE_CATEGORY_ID = BillingRuleViewModel.UnitTypeValueContext,
                CPROPERTY_ID = BillingRuleViewModel.PropertyValueContext,
                LACTIVE = true,
                LBOOKING_FEE = false,
                LBOOKING_FEE_OVERWRITE = false,
                NMIN_BOOKING_FEE = 0,
                CBOOKING_FEE_CHARGE_ID = "",
                CDP_PERIOD_MODE = "",
                LWITH_DP = false,

                CDP_CHARGE_ID = "",
                LINSTALLMENT = false,
                CINSTALLMENT_CHARGE_ID = "",
                CINSTALLMENT_PERIOD_MODE = "",
                LBANK_CREDIT = false
            };
            // Focus Async
            await FocusLabelAdd.FocusAsync();

        }


        private void ServiceSaving(R_SavingEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {

                var loParam = (LMM06000BillingRuleDetailDTO)eventArgs.Data;
                if (!loParam.LBOOKING_FEE)
                {
                    loParam.CBOOKING_FEE_CHARGE_ID = "";
                }
                if (!loParam.LWITH_DP)
                {
                    loParam.IDP_PERCENTAGE = 0;
                    loParam.IDP_INTERVAL = 0;
                    loParam.CDP_PERIOD_MODE = "";
                    loParam.CDP_CHARGE_ID = "";
                }

                if (!loParam.LINSTALLMENT)
                {
                    loParam.IINSTALLMENT_PERCENTAGE = 0;
                    loParam.IINSTALLMENT_INTERVAL = 0;
                    loParam.CINSTALLMENT_PERIOD_MODE = "";
                    loParam.CINSTALLMENT_CHARGE_ID = "";
                }

                if (!loParam.LBANK_CREDIT)
                {
                    loParam.IBANK_CREDIT_PERCENTAGE = 0;
                    loParam.IBANK_CREDIT_INTERVAL = 0;
                }
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
                var loParam = (LMM06000BillingRuleDetailDTO)eventArgs.Data;
                await BillingRuleViewModel.SaveUnitType_BillingRule(loParam, (eCRUDMode)eventArgs.ConductorMode);
                eventArgs.Result = BillingRuleViewModel.BillingRuleDetail;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task R_Validation(R_ValidationEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            GFF00900ParameterDTO loParam = null;
            R_PopupResult loResult = null;
            LMM06000BillingRuleDetailDTO loData = null;
            bool Validated = false;
            try
            {
                loData = (LMM06000BillingRuleDetailDTO)eventArgs.Data;
                BillingRuleViewModel.ValidationFieldEmpty(loData);

                if (loData.LACTIVE == true && _conductorBillingRuleRef!.R_ConductorMode == R_eConductorMode.Add)
                {
                    var loValidateViewModel = new GFF00900Model.ViewModel.GFF00900ViewModel();
                    loValidateViewModel.ACTIVATE_INACTIVE_ACTIVITY_CODE =
                        "PMM06001"; //Uabh Approval Code sesuai Spec masing masing
                    await loValidateViewModel
                        .RSP_ACTIVITY_VALIDITYMethodAsync();

                    if (loValidateViewModel.loRspActivityValidityList.FirstOrDefault().CAPPROVAL_USER == "ALL" &&
                        loValidateViewModel.loRspActivityValidityResult.Data.FirstOrDefault().IAPPROVAL_MODE == 1)
                    {
                        eventArgs.Cancel = false;
                    }
                    else
                    {
                        loParam = new GFF00900ParameterDTO()
                        {
                            Data = loValidateViewModel.loRspActivityValidityList,
                            IAPPROVAL_CODE = "PMM06001"
                        };
                        loResult = await PopupService.Show(typeof(GFF00900FRONT.GFF00900), loParam);

                        if (loResult.Result != null)
                        {
                            if ((bool)loResult.Result == false)
                            {
                                Validated = true;
                            }
                        }
                        else
                        {
                            Validated = true;
                        }

                        if ((bool)loResult.Success == false)
                        {
                            Validated = true;
                        }

                        eventArgs.Cancel = Validated;
                    }
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        #endregion

        #region Delete
        public async Task ServiceDelete(R_ServiceDeleteEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = (LMM06000BillingRuleDetailDTO)eventArgs.Data;
                await BillingRuleViewModel.DeleteUnitType_BillingRule(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task ServiceAfterDelete()
        {
            await R_MessageBox.Show("", "Delete Success", R_eMessageBoxButtonType.OK);
        }
        #endregion


        #region Active/Inactive

        private async Task R_Before_Open_ActivateInactive(R_BeforeOpenPopupEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            await lockingButton(true);
            try
            {
                var loValidateViewModel = new GFF00900Model.ViewModel.GFF00900ViewModel();
                loValidateViewModel.ACTIVATE_INACTIVE_ACTIVITY_CODE = "PMM06001"; //Uabh Approval Code sesuai Spec masing masing
                await loValidateViewModel.RSP_ACTIVITY_VALIDITYMethodAsync(); //Jika IAPPROVAL_CODE == 3, maka akan keluar RSP_ERROR disini

                //Jika Approval User ALL dan Approval Code 1, maka akan langsung menjalankan ActiveInactive
                if (loValidateViewModel.loRspActivityValidityList.FirstOrDefault().CAPPROVAL_USER == "ALL" && loValidateViewModel.loRspActivityValidityResult.Data.FirstOrDefault().IAPPROVAL_MODE == 1)
                {
                    await BillingRuleViewModel.ActiveInactiveProcessAsync();
                    await _gridBillingRuleRef.R_RefreshGrid(null);
                    return;
                }
                else //Disini Approval Code yang didapat adalah 2, yang berarti Active Inactive akan dijalankan jika User yang diinput ada di RSP_ACTIVITY_VALIDITY
                {
                    eventArgs.Parameter = new GFF00900ParameterDTO()
                    {
                        Data = loValidateViewModel.loRspActivityValidityList,
                        IAPPROVAL_CODE = "PMM06001" //Uabh Approval Code sesuai Spec masing masing
                    };
                    eventArgs.TargetPageType = typeof(GFF00900FRONT.GFF00900);
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();
        }

        private async Task R_After_Open_ActivateInactive(R_AfterOpenPopupEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            try
            {
                if (eventArgs.Success == false)
                {
                    return;
                }
                bool result = (bool)eventArgs.Result;
                if (result == true)
                {
                    await BillingRuleViewModel.ActiveInactiveProcessAsync();
                    await _gridBillingRuleRef.R_RefreshGrid(null);
                }
                await lockingButton(false);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }
        #endregion

        #region UserLocking
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlPM";
        private const string DEFAULT_MODULE_NAME = "PM";
        protected async override Task<bool> R_LockUnlock(R_LockUnlockEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            var llRtn = false;
            R_LockingFrontResult loLockResult = null;

            try
            {
                var loData = (LMM06000BillingRuleDetailDTO)eventArgs.Data;

                var loCls = new R_LockingServiceClient(pcModuleName: DEFAULT_MODULE_NAME,
                    plSendWithContext: true,
                    plSendWithToken: true,
                    pcHttpClientName: DEFAULT_HTTP_NAME);

                if (eventArgs.Mode == R_eLockUnlock.Lock)
                {
                    var loLockPar = new R_ServiceLockingLockParameterDTO
                    {

                        Company_Id = clientHelper.CompanyId,
                        User_Id = clientHelper.UserId,
                        Program_Id = "PMM06000",
                        Table_Name = "PMM_BILLING_RULE",
                        Key_Value = string.Join("|", clientHelper.CompanyId, loData.CPROPERTY_ID, loData.CUNIT_TYPE_CATEGORY_ID, loData.CBILLING_RULE_CODE)
                    };

                    loLockResult = await loCls.R_Lock(loLockPar);
                }
                else
                {
                    var loUnlockPar = new R_ServiceLockingUnLockParameterDTO
                    {
                        Company_Id = clientHelper.CompanyId,
                        User_Id = clientHelper.UserId,
                        Program_Id = "PMM06000",
                        Table_Name = "PMM_BILLING_RULE",
                        Key_Value = string.Join("|", clientHelper.CompanyId, loData.CPROPERTY_ID, loData.CUNIT_TYPE_CATEGORY_ID, loData.CBILLING_RULE_CODE)
                    };

                    loLockResult = await loCls.R_UnLock(loUnlockPar);
                }

                llRtn = loLockResult.IsSuccess;
                if (!loLockResult.IsSuccess && loLockResult.Exception != null)
                    throw loLockResult.Exception;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return llRtn;
        }
        private async Task lockingButton(bool param)
        {
            var loEx = new R_Exception();
            R_LockingFrontResult loLockResult = null;
            try
            {
                var loData = BillingRuleViewModel.BillingRuleDetail;
                var loCls = new R_LockingServiceClient(pcModuleName: DEFAULT_MODULE_NAME,
                plSendWithContext: true,
                plSendWithToken: true,
                pcHttpClientName: DEFAULT_HTTP_NAME);
                if (param) // Lock
                {
                    var loLockPar = new R_ServiceLockingLockParameterDTO
                    {
                        Company_Id = clientHelper.CompanyId,
                        User_Id = clientHelper.UserId,
                        Program_Id = "PMM06000",
                        Table_Name = "PMM_BILLING_RULE",
                        Key_Value = string.Join("|", clientHelper.CompanyId, loData.CPROPERTY_ID, loData.CUNIT_TYPE_CATEGORY_ID, loData.CBILLING_RULE_CODE)
                    };
                    loLockResult = await loCls.R_Lock(loLockPar);
                }
                else // Unlock
                {
                    var loUnlockPar = new R_ServiceLockingUnLockParameterDTO
                    {
                        Company_Id = clientHelper.CompanyId,
                        User_Id = clientHelper.UserId,
                        Program_Id = "PMM06000",
                        Table_Name = "PMM_BILLING_RULE",
                        Key_Value = string.Join("|", clientHelper.CompanyId, loData.CPROPERTY_ID, loData.CUNIT_TYPE_CATEGORY_ID, loData.CBILLING_RULE_CODE)
                    };
                    loLockResult = await loCls.R_UnLock(loUnlockPar);
                }
                if (!loLockResult.IsSuccess && loLockResult.Exception != null)
                    throw loLockResult.Exception;
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
