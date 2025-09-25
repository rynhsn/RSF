using BlazorClientHelper;
using PMM01500COMMON;
using PMM01500MODEL;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using Lookup_GSModel.ViewModel;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Enums;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.Tab;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using R_LockingFront;
using System.Xml.Linq;
using Lookup_PMCOMMON.DTOs;
using Lookup_PMModel.ViewModel.LML00200;
using Lookup_PMFRONT;
using System;

namespace PMM01500FRONT
{
    public partial class PMM01520 : R_Page, R_ITabPage
    {
        private PMM01521ViewModel _InvPinaltyDate_viewModel = new PMM01521ViewModel();
        private R_Conductor _InvPinalty_conductorRef;
        [Inject] IClientHelper clientHelper { get; set; }
        private R_Grid<PMM01520DTO> _gridRef;

        #region private Property
        private bool EnablePinaltyNormalMode;
        private R_CheckBox EnbalePinalty_CheckBox;
        private R_DatePicker<DateTime?> DateTime_Picker;
        private bool EnableHasData = true;
        #endregion

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                await _InvPinaltyDate_viewModel.GetUniversalList();
                var loData = R_FrontUtility.ConvertObjectToObject<PMM01520DTO>(poParameter);
                _InvPinaltyDate_viewModel.InvPinalty = loData;
                _InvPinaltyDate_viewModel.InvGrpCode = loData.CINVGRP_CODE;
                _InvPinaltyDate_viewModel.InvGrpName = loData.CINVGRP_NAME;

                await _gridRef.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlPM";
        private const string DEFAULT_MODULE_NAME = "PM";
        protected async override Task<bool> R_LockUnlock(R_LockUnlockEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            var llRtn = false;
            R_LockingFrontResult loLockResult = null;

            try
            {
                var loData = (PMM01520DTO)eventArgs.Data;

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
                        Program_Id = "PMM01520",
                        Table_Name = "PMM_INVGRP",
                        Key_Value = string.Join("|", clientHelper.CompanyId, loData.CPROPERTY_ID, loData.CINVGRP_CODE, loData.CPENALTY_DATE)
                    };

                    loLockResult = await loCls.R_Lock(loLockPar);
                }
                else
                {
                    var loUnlockPar = new R_ServiceLockingUnLockParameterDTO
                    {
                        Company_Id = clientHelper.CompanyId,
                        User_Id = clientHelper.UserId,
                        Program_Id = "PMM01520",
                        Table_Name = "PMM_INVGRP",
                        Key_Value = string.Join("|", clientHelper.CompanyId, loData.CPROPERTY_ID, loData.CINVGRP_CODE, loData.CPENALTY_DATE)
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
        private async Task Grid_R_SetOther(R_SetEventArgs eventArgs)
        {
            EnablePinaltyNormalMode = eventArgs.Enable;
            await InvokeTabEventCallbackAsync(eventArgs.Enable);
        }
        public async Task RefreshTabPageAsync(object poParam)
        {
            var loData = R_FrontUtility.ConvertObjectToObject<PMM01520DTO>(poParam);
            EnableHasData = string.IsNullOrWhiteSpace(loData.CINVGRP_CODE) == false;
            if (string.IsNullOrWhiteSpace(loData.CINVGRP_CODE))
            {
                _InvPinaltyDate_viewModel.InvGrpCode = "";
                _InvPinaltyDate_viewModel.InvGrpName = "";
                _InvPinaltyDate_viewModel.R_SetCurrentData(null);
                _gridRef.DataSource.Clear();
            }
            else
            {
                _InvPinaltyDate_viewModel.InvPinalty = loData;
                _InvPinaltyDate_viewModel.InvGrpCode = loData.CINVGRP_CODE;
                _InvPinaltyDate_viewModel.InvGrpName = loData.CINVGRP_NAME;

                await _gridRef.R_RefreshGrid(null);
            }
        }

        #region Pinalty Form
        private async Task PinaltyDate_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _InvPinaltyDate_viewModel.GetInvoicePinaltyDateList();

                eventArgs.ListEntityResult = _InvPinaltyDate_viewModel.PinaltyDateGrid;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task Pinalty_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _InvPinaltyDate_viewModel.GetInvoicePinaltyDate((PMM01520DTO)eventArgs.Data);

                eventArgs.Result = _InvPinaltyDate_viewModel.InvPinaltyDate;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task Pinalty_Display(R_DisplayEventArgs eventArgs)
        {
            if (eventArgs.ConductorMode == R_BlazorFrontEnd.Enums.R_eConductorMode.Edit)
            {
                await EnbalePinalty_CheckBox.FocusAsync();
            }
        }
        private async Task Pinalty_AfterAdd(R_AfterAddEventArgs eventArgs)
        {
            var loData = (PMM01520DTO)eventArgs.Data;
            loData.CPROPERTY_ID = _InvPinaltyDate_viewModel.InvPinalty.CPROPERTY_ID;
            loData.CINVGRP_CODE = _InvPinaltyDate_viewModel.InvPinalty.CINVGRP_CODE;
            loData.CPENALTY_FEE_START_FROM = "";
            loData.DPENALTY_DATE = DateTime.Now;
            loData.LPENALTY = true;

            await DateTime_Picker.FocusAsync();
        }
        private void Pinalty_Validation(R_ValidationEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loData = (PMM01520DTO)eventArgs.Data;
                if (loData.DPENALTY_DATE == null)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(PMM01500FrontResources.Resources_Dummy_Class),
                        "1520"));
                }

                if (loData.LPENALTY)
                {
                    if (string.IsNullOrEmpty(loData.CPENALTY_UNIT_CHARGES_ID))
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                        typeof(PMM01500FrontResources.Resources_Dummy_Class),
                        "1513"));
                    }

                    if (string.IsNullOrEmpty(loData.CPENALTY_ADD_ID))
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                        typeof(PMM01500FrontResources.Resources_Dummy_Class),
                        "1514"));
                    }

                    if (string.IsNullOrEmpty(loData.CPENALTY_TYPE))
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                        typeof(PMM01500FrontResources.Resources_Dummy_Class),
                        "1515"));
                    }

                    if (string.IsNullOrEmpty(loData.CROUNDING_MODE))
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                        typeof(PMM01500FrontResources.Resources_Dummy_Class),
                        "1523"));
                    }

                    if (string.IsNullOrEmpty(loData.CCUTOFDATE_BY))
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                        typeof(PMM01500FrontResources.Resources_Dummy_Class),
                        "1516"));
                    }
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

        }
        private async Task Pinalty_BeforeEdit(R_BeforeEditEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loData = (PMM01520DTO)eventArgs.Data;
                if (int.Parse(loData.DPENALTY_DATE.Value.ToString("yyyyMMdd")) < int.Parse(DateTime.Now.ToString("yyyyMMdd")))
                {
                    await R_MessageBox.Show("", _localizer["V01"], R_BlazorFrontEnd.Controls.MessageBox.R_eMessageBoxButtonType.OK);
                    eventArgs.Cancel = true;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

        }
        private void Pinalty_Saving(R_SavingEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loData = (PMM01520DTO)eventArgs.Data;

                if (!string.IsNullOrEmpty(loData.CPENALTY_TYPE_CALC_BASEON_CalcBaseonMonth))
                {
                    loData.CPENALTY_TYPE_CALC_BASEON = loData.CPENALTY_TYPE_CALC_BASEON_CalcBaseonMonth;
                }
                else if (!string.IsNullOrEmpty(loData.CPENALTY_TYPE_CALC_BASEON_CalcBaseonDays))
                {
                    loData.CPENALTY_TYPE_CALC_BASEON = loData.CPENALTY_TYPE_CALC_BASEON_CalcBaseonDays;
                }
                if (loData.NPENALTY_TYPE_VALUE_MonthlyAmmount > 0)
                {
                    loData.NPENALTY_TYPE_VALUE = loData.NPENALTY_TYPE_VALUE_MonthlyAmmount;
                }
                else if (loData.NPENALTY_TYPE_VALUE_MonthlyPercentage > 0)
                {
                    loData.NPENALTY_TYPE_VALUE = loData.NPENALTY_TYPE_VALUE_MonthlyPercentage;
                }
                else if (loData.NPENALTY_TYPE_VALUE_DailyAmmount > 0)
                {
                    loData.NPENALTY_TYPE_VALUE = loData.NPENALTY_TYPE_VALUE_DailyAmmount;
                }
                else if (loData.NPENALTY_TYPE_VALUE_DailyPercentage > 0)
                {
                    loData.NPENALTY_TYPE_VALUE = loData.NPENALTY_TYPE_VALUE_DailyPercentage;
                }
                else if (loData.NPENALTY_TYPE_VALUE_OneTimeAmmount > 0)
                {
                    loData.NPENALTY_TYPE_VALUE = loData.NPENALTY_TYPE_VALUE_OneTimeAmmount;
                }
                else if (loData.NPENALTY_TYPE_VALUE_OneTimePercentage > 0)
                {
                    loData.NPENALTY_TYPE_VALUE = loData.NPENALTY_TYPE_VALUE_OneTimePercentage;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task Pinalty_ServiceSave(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _InvPinaltyDate_viewModel.SaveInvoicePinalty((PMM01520DTO)eventArgs.Data, (eCRUDMode)eventArgs.ConductorMode);

                eventArgs.Result = _InvPinaltyDate_viewModel.InvPinaltyDate;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task Pinalty_ServiceDelete(R_ServiceDeleteEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loData = (PMM01520DTO)eventArgs.Data;
                loData.CPROPERTY_ID = _InvPinaltyDate_viewModel.InvPinalty.CPROPERTY_ID;
                loData.CINVGRP_CODE = _InvPinaltyDate_viewModel.InvPinalty.CINVGRP_CODE;

                await _InvPinaltyDate_viewModel.DeleteInvoicePinalty(loData);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        #endregion


        private void Pinalty_RadioButtonOnChange(string poParam)
        {
            _InvPinaltyDate_viewModel.Data.CPENALTY_TYPE = poParam;
            var loData = (PMM01520DTO)_InvPinalty_conductorRef.R_GetCurrentData();
            if (poParam != "10")
                loData.NPENALTY_TYPE_VALUE_MonthlyAmmount = 0;

            if (poParam != "20")
                loData.NPENALTY_TYPE_VALUE_DailyAmmount = 0;

            if (poParam != "30")
                loData.NPENALTY_TYPE_VALUE_OneTimeAmmount = 0;

            if (poParam != "11")
            {
                loData.NPENALTY_TYPE_VALUE_MonthlyPercentage = 0;
                loData.CPENALTY_TYPE_CALC_BASEON_CalcBaseonMonth = "";
            }
            else
            {
                loData.CPENALTY_TYPE_CALC_BASEON_CalcBaseonMonth = "01";
            }

            if (poParam != "21")
            {
                loData.NPENALTY_TYPE_VALUE_DailyPercentage = 0;
                loData.CPENALTY_TYPE_CALC_BASEON_CalcBaseonDays = "";
            }
            else
            {
                loData.CPENALTY_TYPE_CALC_BASEON_CalcBaseonDays = "01";
            }
        }
        private void Pinalty_OnCheckedMinAmmount(bool poParam)
        {
            _InvPinaltyDate_viewModel.Data.LMIN_PENALTY_AMOUNT = poParam;
            if (poParam == false)
            {
                _InvPinaltyDate_viewModel.Data.NMIN_PENALTY_AMOUNT = 0;
            }
        }
        private void Pinalty_OnCheckedMaxAmmount(bool poParam)
        {
            _InvPinaltyDate_viewModel.Data.LMAX_PENALTY_AMOUNT = poParam;
            if (poParam == false)
            {
                _InvPinaltyDate_viewModel.Data.NMAX_PENALTY_AMOUNT = 0;
            }
        }

        private async Task PinaltyId_OnLostFocus()
        {
            var loEx = new R_Exception();

            try
            {
                var loData = (PMM01520DTO)_InvPinalty_conductorRef.R_GetCurrentData();
                if (string.IsNullOrWhiteSpace(loData.CPENALTY_ADD_ID) == false)
                {
                    var param = new GSL01400ParameterDTO
                    {
                        CPROPERTY_ID = _InvPinaltyDate_viewModel.InvPinalty.CPROPERTY_ID,
                        CCHARGES_TYPE_ID = "A",
                        CSEARCH_TEXT = loData.CPENALTY_ADD_ID
                    };

                    LookupGSL01400ViewModel loLookupViewModel = new LookupGSL01400ViewModel();

                    var loResult = await loLookupViewModel.GetOtherCharges(param);

                    if (loResult == null)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                                typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                                "_ErrLookup01"));
                        loData.CPENALTY_ADD_NAME = "";
                        goto EndBlock;
                    }
                    loData.CPENALTY_ADD_ID = loResult.CCHARGES_ID;
                    loData.CPENALTY_ADD_NAME = loResult.CCHARGES_NAME;
                }
                else
                {
                    loData.CPENALTY_ADD_NAME = "";
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        EndBlock:
            R_DisplayException(loEx);
        }
        private void Pinalty_OtherCharges_Before_Open_Lookup(R_BeforeOpenLookupEventArgs eventArgs)
        {
            var loParam = new GSL01400ParameterDTO { CPROPERTY_ID = _InvPinaltyDate_viewModel.InvPinalty.CPROPERTY_ID, CCHARGES_TYPE_ID = "A" };

            eventArgs.Parameter = loParam;
            eventArgs.TargetPageType = typeof(GSL01400);
        }
        private void Pinalty_OtherCharges_After_Open_Lookup(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loTempResult = (GSL01400DTO)eventArgs.Result;
                if (loTempResult == null)
                {
                    return;
                }

                var loData = (PMM01520DTO)_InvPinalty_conductorRef.R_GetCurrentData();

                loData.CPENALTY_ADD_ID = loTempResult.CCHARGES_ID;
                loData.CPENALTY_ADD_NAME = loTempResult.CCHARGES_NAME;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task ChargesId_OnLostFocus()
        {
            var loEx = new R_Exception();

            try
            {
                var loData = (PMM01520DTO)_InvPinalty_conductorRef.R_GetCurrentData();
                if (string.IsNullOrWhiteSpace(loData.CPENALTY_UNIT_CHARGES_ID) == false)
                {
                    var param = new LML00200ParameterDTO
                    {
                        CPROPERTY_ID = _InvPinaltyDate_viewModel.InvPinalty.CPROPERTY_ID,
                        CSEARCH_TEXT = loData.CPENALTY_UNIT_CHARGES_ID
                    };

                    LookupLML00200ViewModel loLookupViewModel = new LookupLML00200ViewModel();

                    var loResult = await loLookupViewModel.GetUnitCharges(param);

                    if (loResult == null)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                                typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                                "_ErrLookup01"));
                        loData.CPENALTY_CHARGES_DESCR = "";
                        goto EndBlock;
                    }
                    loData.CPENALTY_UNIT_CHARGES_ID = loResult.CCHARGES_ID;
                    loData.CPENALTY_CHARGES_DESCR = loResult.CCHARGES_NAME;
                }
                else
                {
                    loData.CPENALTY_CHARGES_DESCR = "";
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        EndBlock:
            R_DisplayException(loEx);
        }
        private void Charges_OtherCharges_Before_Open_Lookup(R_BeforeOpenLookupEventArgs eventArgs)
        {
            var loParam = new LML00200ParameterDTO { CPROPERTY_ID = _InvPinaltyDate_viewModel.InvPinalty.CPROPERTY_ID };

            eventArgs.Parameter = loParam;
            eventArgs.TargetPageType = typeof(LML00200);
        }
        private void Charges_OtherCharges_After_Open_Lookup(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loTempResult = (LML00200DTO)eventArgs.Result;
                if (loTempResult == null)
                {
                    return;
                }

                var loData = (PMM01520DTO)_InvPinalty_conductorRef.R_GetCurrentData();
                loData.CPENALTY_UNIT_CHARGES_ID = loTempResult.CCHARGES_ID;
                loData.CPENALTY_CHARGES_DESCR = loTempResult.CCHARGES_NAME;

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}
