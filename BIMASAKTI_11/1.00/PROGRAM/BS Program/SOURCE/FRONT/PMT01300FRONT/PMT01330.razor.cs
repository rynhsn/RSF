using BlazorClientHelper;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using Lookup_GSModel.ViewModel;
using Microsoft.AspNetCore.Components;
using R_APICommonDTO;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Enums;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Controls.Tab;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_BlazorFrontEnd.Interfaces;
using R_CommonFrontBackAPI;
using R_LockingFront;
using PMT01300COMMON;
using PMT01300MODEL;
using System.Globalization;
using Lookup_PMFRONT;
using Lookup_PMCOMMON.DTOs;
using Lookup_PMModel.ViewModel.LML00200;
using System;
using GFF00900COMMON.DTOs;

namespace PMT01300FRONT
{
    public partial class PMT01330 : R_Page, R_ITabPage
    {
        #region ViewModel
        private PMT01330ViewModel _viewModel = new PMT01330ViewModel();
        #endregion

        #region Conductor
        private R_Conductor _conductorRef;
        private R_ConductorGrid _conductorGridRef;
        #endregion

        #region Grid
        private R_Grid<PMT01330DTO> _gridLOIChargeListRef;
        private R_Grid<PMT01300AgreementChargeCalUnitDTO> _gridLOIChargeCallChargeListRef;
        private R_Grid<PMT01300AgreementChargeCalUnitDTO> _gridLOIChargeCallChargeListDisplayRef;
        #endregion

        #region Inject
        [Inject] private R_ILocalizer<PMT01300FrontResources.Resources_Dummy_Class> _localizer { get; set; }
        [Inject] IClientHelper clientHelper { get; set; }
        #endregion

        #region Private Property
        private R_TextBox ChargesId_TextBox;
        private R_DatePicker<DateTime?> StartDate_DatePicker;
        private bool EnableNormalMode = false;
        private bool EnableHasHeaderData = false;
        private bool EnableGreaterClosesSts = false;
        private string LabelActiveInactive = "Active";
        private DateTime _StartDateTimeDefault;
        private DateTime _EndDateTimeDefault;
        #endregion

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                var loData = (PMT01310DTO)poParameter;
                EnableGreaterClosesSts = int.Parse(loData.CTRANS_STATUS_LOI) >= 80 == false;
                _viewModel.LOI_Unit = loData;

                await _viewModel.GetInitialVar();
                await _gridLOIChargeListRef.R_RefreshGrid(loData);
                if (_viewModel.LOIChargeGrid.Count <= 0)
                {
                    await _gridLOIChargeCallChargeListRef.R_RefreshGrid(loData);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        #region Locking
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlPM";
        private const string DEFAULT_MODULE_NAME = "PM";
        protected async override Task<bool> R_LockUnlock(R_LockUnlockEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            var llRtn = false;
            R_LockingFrontResult loLockResult = null;

            try
            {
                var loData = (PMT01330DTO)eventArgs.Data;

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
                        Program_Id = "PMT01330",
                        Table_Name = "PMT_AGREEMENT_CHARGE",
                        Key_Value = string.Join("|", clientHelper.CompanyId, loData.CPROPERTY_ID, loData.CDEPT_CODE, loData.CTRANS_CODE, loData.CREF_NO, loData.CSEQ_NO)
                    };

                    loLockResult = await loCls.R_Lock(loLockPar);
                }
                else
                {
                    var loUnlockPar = new R_ServiceLockingUnLockParameterDTO
                    {
                        Company_Id = clientHelper.CompanyId,
                        User_Id = clientHelper.UserId,
                        Program_Id = "PMT01330",
                        Table_Name = "PMT_AGREEMENT_CHARGE",
                        Key_Value = string.Join("|", clientHelper.CompanyId, loData.CPROPERTY_ID, loData.CDEPT_CODE, loData.CTRANS_CODE, loData.CREF_NO, loData.CSEQ_NO)
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
        #endregion

        #region Charge 
        private async Task Charge_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParameter = R_FrontUtility.ConvertObjectToObject<PMT01330DTO>(eventArgs.Parameter);
                await _viewModel.GetLOIChargeList(loParameter);

                eventArgs.ListEntityResult = _viewModel.LOIChargeGrid;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task Charge_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _viewModel.GetLOICharge((PMT01330DTO)eventArgs.Data);

                eventArgs.Result = _viewModel.LOI_Charge;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task Charge_Display(R_DisplayEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loData = (PMT01330DTO)eventArgs.Data;
                if (eventArgs.ConductorMode == R_BlazorFrontEnd.Enums.R_eConductorMode.Normal)
                {
                    if (string.IsNullOrWhiteSpace(loData.CCHARGES_ID) == false)
                    {
                        await _gridLOIChargeCallChargeListDisplayRef.R_RefreshGrid(loData);

                        if (loData.LACTIVE)
                        {
                            LabelActiveInactive = _localizer["_Inactive"];
                            _viewModel.StatusChange = false;
                        }
                        else
                        {
                            LabelActiveInactive = _localizer["_Active"];
                            _viewModel.StatusChange = true;
                        }

                        if (loData.DSTART_DATE != null)
                            _StartDateTimeDefault = loData.DSTART_DATE.Value;
                        if (loData.DEND_DATE != null)
                            _EndDateTimeDefault = loData.DEND_DATE.Value;
                    }
                }
                else if (eventArgs.ConductorMode == R_BlazorFrontEnd.Enums.R_eConductorMode.Edit)
                {
                    await _gridLOIChargeCallChargeListRef.R_RefreshGrid(loData);
                    await StartDate_DatePicker.FocusAsync();
                }
                if (eventArgs.ConductorMode == R_BlazorFrontEnd.Enums.R_eConductorMode.Add)
                {
                    await _gridLOIChargeCallChargeListRef.R_RefreshGrid(_viewModel.LOI_Unit);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task Charge_ServiceSave(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loData = (PMT01330DTO)eventArgs.Data;
                if (_viewModel.Data.LCAL_UNIT)
                {
                    if (_gridLOIChargeCallChargeListRef.DataSource.Count > 0)
                    {
                        loData.CHARGE_CALL_UNIT_LIST = new List<PMT01300AgreementChargeCalUnitDTO>(_gridLOIChargeCallChargeListRef.DataSource);
                    }
                    else
                    {
                        loData.CHARGE_CALL_UNIT_LIST = new List<PMT01300AgreementChargeCalUnitDTO>();
                    }
                }
                else
                {
                    loData.CHARGE_CALL_UNIT_LIST = new List<PMT01300AgreementChargeCalUnitDTO>();
                }

                await _viewModel.SaveLOICharge(
                    loData,
                    (eCRUDMode)eventArgs.ConductorMode);

                eventArgs.Result = _viewModel.LOI_Charge;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task Charge_ServiceDelete(R_ServiceDeleteEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _viewModel.DeleteLOICharge((PMT01330DTO)eventArgs.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task Grid_R_SetOther(R_SetEventArgs eventArgs)
        {
            EnableNormalMode = eventArgs.Enable;
            PMT01300LOICallBackParameterDTO loData = new PMT01300LOICallBackParameterDTO { CRUD_MODE = eventArgs.Enable };
            await InvokeTabEventCallbackAsync(loData);
        }
        private void Grid_SetHasData(R_SetEventArgs eventArgs)
        {
            EnableHasHeaderData = eventArgs.Enable;
        }
        private async Task Charge_AfterAdd(R_AfterAddEventArgs eventArgs)
        {
            var loData = (PMT01330DTO)eventArgs.Data;
            if (_viewModel.VAR_FEE_METHOD.Count > 0)
            {
                var loFirstDefaultData = _viewModel.VAR_FEE_METHOD.FirstOrDefault();
                loData.CFEE_METHOD = loFirstDefaultData.CCODE;
                loData.CINVOICE_PERIOD = loFirstDefaultData.CCODE;
            }
            else
            {
                loData.CFEE_METHOD = "";
                loData.CINVOICE_PERIOD = "";
            }

            loData.CBILLING_MODE = "01";
            loData.CUNIT_ID = _viewModel.LOI_Unit.CUNIT_ID;
            loData.CFLOOR_ID = _viewModel.LOI_Unit.CFLOOR_ID;
            loData.CBUILDING_ID = _viewModel.LOI_Unit.CBUILDING_ID;
            loData.CCHARGE_MODE = _viewModel.LOI_Unit.CCHARGE_MODE;
            loData.CDESCRIPTION = "";
            
            loData.CCURRENCY_CODE = _viewModel.LOI_Unit.CCURRENCY_CODE;

            _viewModel.oControlYMD.LYEAR = true;
            _viewModel.oControlYMD.LMONTH = true;
            _viewModel.oControlYMD.LMONTH = true;

            loData.DSTART_DATE = DateTime.Now;
            _StartDateTimeDefault = DateTime.Now;
            loData.DEND_DATE = DateTime.Now.AddYears(1);
            _EndDateTimeDefault = DateTime.Now.AddYears(1);

            loData.IYEARS = 1;
            await ChargesId_TextBox.FocusAsync();
            if (_gridLOIChargeCallChargeListRef.DataSource.Count > 0)
            {
                _gridLOIChargeCallChargeListRef.DataSource.Clear();
            }
            if (_gridLOIChargeCallChargeListDisplayRef.DataSource.Count > 0)
            {
                _gridLOIChargeCallChargeListDisplayRef.DataSource.Clear();
            }
        }
        private void Charge_AfterDelete()
        {
            if (_gridLOIChargeCallChargeListRef.DataSource.Count > 0)
            {
                _gridLOIChargeCallChargeListRef.DataSource.Clear();
            }
            if (_gridLOIChargeCallChargeListDisplayRef.DataSource.Count > 0)
            {
                _gridLOIChargeCallChargeListDisplayRef.DataSource.Clear();
            }
        }
        private void Charge_Validation(R_ValidationEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                bool lCancel;
                var loData = (PMT01330DTO)eventArgs.Data;

                lCancel = string.IsNullOrWhiteSpace(loData.CCHARGES_ID);
                if (lCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(PMT01300FrontResources.Resources_Dummy_Class),
                        "V021"));
                }
                lCancel = string.IsNullOrWhiteSpace(loData.CTAX_ID) && loData.LTAXABLE;
                if (lCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(PMT01300FrontResources.Resources_Dummy_Class),
                        "V029"));
                }

                if (loData.LBASED_OPEN_DATE == false)
                {
                    lCancel = loData.DSTART_DATE == null;
                    if (lCancel)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                            typeof(PMT01300FrontResources.Resources_Dummy_Class),
                            "V005"));
                    }

                    lCancel = loData.DEND_DATE == null;
                    if (lCancel)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                            typeof(PMT01300FrontResources.Resources_Dummy_Class),
                            "V006"));
                    }

                    if (loData.DEND_DATE != null && loData.DSTART_DATE != null)
                    {
                        int liStartDate = int.Parse(loData.DSTART_DATE.Value.ToString("yyyyMMdd"));
                        int liEndDate = int.Parse(loData.DEND_DATE.Value.ToString("yyyyMMdd"));

                        int liPlanEndDate = int.Parse(_viewModel.LOI_Unit.CEND_DATE);
                        int liPlanStartDate = int.Parse(_viewModel.LOI_Unit.CSTART_DATE);

                        lCancel = liStartDate > liEndDate;
                        if (lCancel)
                        {
                            loEx.Add(R_FrontUtility.R_GetError(
                                typeof(PMT01300FrontResources.Resources_Dummy_Class),
                                "V015"));
                        }

                        if (loData.CBILLING_MODE == "01")
                        {
                            if (liEndDate > liPlanEndDate)
                            {
                                loEx.Add(R_FrontUtility.R_GetError(
                                    typeof(PMT01300FrontResources.Resources_Dummy_Class),
                                    "V040"));
                            }
                        }
                        else
                        {
                            if (liStartDate < liPlanStartDate && liEndDate > liPlanEndDate)
                            {
                                loEx.Add(R_FrontUtility.R_GetError(
                                    typeof(PMT01300FrontResources.Resources_Dummy_Class),
                                    "V037"));
                            }
                        }

                        if (loData.CFEE_METHOD == "03")
                        {
                            lCancel = (loData.IYEARS == 0 && loData.IMONTHS == 0 && loData.IDAYS != 0);
                            if (lCancel == false)
                            {
                                loEx.Add(R_FrontUtility.R_GetError(
                                    typeof(PMT01300FrontResources.Resources_Dummy_Class),
                                    "V035"));
                            }
                        }
                    }
                }

                lCancel = string.IsNullOrWhiteSpace(loData.CCURRENCY_CODE);
                if (lCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(PMT01300FrontResources.Resources_Dummy_Class),
                        "V009"));
                }

                lCancel = string.IsNullOrWhiteSpace(loData.CFEE_METHOD);
                if (lCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(PMT01300FrontResources.Resources_Dummy_Class),
                        "V025"));
                }

                if (loData.LCAL_UNIT)
                {
                    if (loData.CCHARGES_TYPE == "04")
                    {
                        lCancel = loData.NFEE_AMT < 0 && _gridLOIChargeCallChargeListRef.DataSource.Count <= 0;
                        if (lCancel)
                        {
                            loEx.Add(R_FrontUtility.R_GetError(
                                typeof(PMT01300FrontResources.Resources_Dummy_Class),
                                "V039"));
                        }
                    }
                    else
                    {
                        lCancel = loData.NFEE_AMT <= 0 && _gridLOIChargeCallChargeListRef.DataSource.Count <= 0;
                        if (lCancel)
                        {
                            loEx.Add(R_FrontUtility.R_GetError(
                                typeof(PMT01300FrontResources.Resources_Dummy_Class),
                                "V038"));
                        }
                    }
                }
                else
                {
                    if(loData.CCHARGES_TYPE == "04")
                    {
                        lCancel = loData.NFEE_AMT < 0;
                        if (lCancel)
                        {
                            loEx.Add(R_FrontUtility.R_GetError(
                                typeof(PMT01300FrontResources.Resources_Dummy_Class),
                                "V036"));
                        }
                    }
                    else
                    {
                        lCancel = loData.NFEE_AMT <= 0;
                        if (lCancel)
                        {
                            loEx.Add(R_FrontUtility.R_GetError(
                                typeof(PMT01300FrontResources.Resources_Dummy_Class),
                                "V027"));
                        }
                    }
                }

                lCancel = loData.NINVOICE_AMT <= 0 && loData.CCHARGES_TYPE != "04";
                if (lCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(PMT01300FrontResources.Resources_Dummy_Class),
                        "V034"));
                }

                if (loData.NFEE_AMT < loData.NBOTTOM_PRICE)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                                typeof(PMT01300FrontResources.Resources_Dummy_Class),
                                "V041"));
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            eventArgs.Cancel = loEx.HasError;
        }
        private async Task Charge_BeforeCancel(R_BeforeCancelEventArgs eventArgs)
        {
            var res = await R_MessageBox.Show("", _localizer["Q04"],
                R_eMessageBoxButtonType.YesNo);

            eventArgs.Cancel = res == R_eMessageBoxResult.No;
        }
        #endregion

        #region Charges Call Unit 
        private async Task ChargesCallUnit_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParameter = R_FrontUtility.ConvertObjectToObject<PMT01300ParameterAgreementChargeCalUnitDTO>(eventArgs.Parameter);
                await _viewModel.GetChargeCallUnitList(loParameter);

                eventArgs.ListEntityResult = _viewModel.ChargeCalUnitGrid;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private void ChargesCallUnit_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            eventArgs.Result = eventArgs.Data;
        }
        private void ChargesCallUnit_Saving(R_SavingEventArgs eventArgs)
        {
            var loData = (PMT01300AgreementChargeCalUnitDTO)eventArgs.Data;
            loData.CFLOOR_ID = _viewModel.LOI_Unit.CFLOOR_ID;
            loData.CBUILDING_ID = _viewModel.LOI_Unit.CBUILDING_ID;
        }
        private async Task ChargesCallUnit_AfterSave(R_AfterSaveEventArgs eventArgs)
        {
            var loDataList = _gridLOIChargeCallChargeListRef.DataSource;
            decimal lnFeeArea = loDataList.Sum(x => x.NFEE_PER_AREA * x.NTOTAL_AREA);
            await FeeAmountDecimal_ValueChanged(lnFeeArea);
        }
        private void ChargesCallUnit_Before_Open_Grid_Lookup(R_BeforeOpenGridLookupColumnEventArgs eventArgs)
        {
            var loParam = R_FrontUtility.ConvertObjectToObject<GSL02300ParameterDTO>(_viewModel.LOI_Unit);
            loParam.CUNIT_CATEGORY_LIST = "02,03";
            loParam.CLEASE_STATUS_LIST = "01,02";
            var loListDataSeparator = _gridLOIChargeCallChargeListRef.DataSource.Select(x => x.CUNIT_ID);
            loParam.CREMOVE_DATA_UNIT_ID_SEPARATOR = string.Join(",", loListDataSeparator);
            loParam.CPROGRAM_ID = "PMT01300";
            loParam.CFLOOR_ID = "";
            loParam.LAGREEMENT = true;

            eventArgs.Parameter = loParam;
            eventArgs.TargetPageType = typeof(GSL02300);
        }
        private void ChargesCallUnit_After_Open_Grid_Lookup(R_AfterOpenGridLookupColumnEventArgs eventArgs)
        {
            var loTempResult = (GSL02300DTO)eventArgs.Result;
            var loData = (PMT01300AgreementChargeCalUnitDTO)eventArgs.ColumnData;
            if (loTempResult is null)
            {
                return;
            }

            loData.CUNIT_ID = loTempResult.CUNIT_ID;
            loData.NTOTAL_AREA = loTempResult.NGROSS_AREA_SIZE;
        }
        private async Task ChargesCallUnitDisplay_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParameter = R_FrontUtility.ConvertObjectToObject<PMT01300ParameterAgreementChargeCalUnitDTO>(eventArgs.Parameter);
                await _viewModel.GetChargeCallUnitListDisplay(loParameter);

                eventArgs.ListEntityResult = _viewModel.ChargeCalUnitDisplayGrid;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task ChargesCallUnit_AfterDelete()
        {
            var loDataList = _gridLOIChargeCallChargeListRef.DataSource;
            decimal lnFeeArea = loDataList.Sum(x => x.NFEE_PER_AREA * x.NTOTAL_AREA);
            await FeeAmountDecimal_ValueChanged(lnFeeArea);
        }
        #endregion

        #region Currency Lookup
        private async Task Currency_OnLostFocus()
        {
            var loEx = new R_Exception();

            try
            {
                var loData = (PMT01330DTO)_conductorRef.R_GetCurrentData();
                if (string.IsNullOrWhiteSpace(loData.CCURRENCY_CODE) == false)
                {
                    GSL00300ParameterDTO loParam = new GSL00300ParameterDTO()
                    {
                        CSEARCH_TEXT = loData.CCURRENCY_CODE
                    };

                    LookupGSL00300ViewModel loLookupViewModel = new LookupGSL00300ViewModel();

                    var loResult = await loLookupViewModel.GetCurrency(loParam);

                    if (loResult == null)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                                typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                                "_ErrLookup01"));
                        goto EndBlock;
                    }
                    loData.CCURRENCY_CODE = loResult.CCURRENCY_CODE;
                }
                else
                {
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        EndBlock:
            R_DisplayException(loEx);
        }
        private void Currency_Before_Open_Lookup(R_BeforeOpenLookupEventArgs eventArgs)
        {
            GSL00300ParameterDTO loParam = new GSL00300ParameterDTO()
            {
            };
            eventArgs.Parameter = loParam;
            eventArgs.TargetPageType = typeof(GSL00300);
        }
        private void Currency_After_Open_Lookup(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                GSL00300DTO loTempResult = (GSL00300DTO)eventArgs.Result;
                if (loTempResult == null)
                {
                    return;
                }
                var loData = (PMT01330DTO)_conductorRef.R_GetCurrentData();
                loData.CCURRENCY_CODE = loTempResult.CCURRENCY_CODE;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        #endregion

        #region Unit Charges Lookup
        private async Task Charges_OnLostFocus()
        {
            var loEx = new R_Exception();

            try
            {
                if (string.IsNullOrWhiteSpace(_viewModel.Data.CCHARGES_ID) == false)
                {
                    LML00200ParameterDTO loParam = new LML00200ParameterDTO()
                    {
                        CPROPERTY_ID = _viewModel.LOI_Unit.CPROPERTY_ID,
                        CSEARCH_TEXT = _viewModel.Data.CCHARGES_ID,
                        CCHARGE_TYPE_ID = "01,02,04,05"
                    };

                    LookupLML00200ViewModel loLookupViewModel = new LookupLML00200ViewModel();

                    var loResult = await loLookupViewModel.GetUnitCharges(loParam);

                    if (loResult == null)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                                typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                                "_ErrLookup01"));
                        _viewModel.Data.CCHARGES_NAME = "";
                        _viewModel.Data.CCHARGES_TYPE = "";
                        _viewModel.Data.CCHARGES_TYPE_DESCR = "";
                        _viewModel.Data.CINVGRP_CODE = "";
                        _viewModel.Data.CINVGRP_NAME = "";
                        _viewModel.Data.LTAXABLE = false;
                        _viewModel.Data.CTAX_ID = "";
                        _viewModel.Data.CTAX_NAME = "";
                        goto EndBlock;
                    }

                    _viewModel.Data.CCHARGES_ID = loResult.CCHARGES_ID;
                    _viewModel.Data.CCHARGES_NAME = loResult.CCHARGES_NAME;
                    _viewModel.Data.CCHARGES_TYPE = loResult.CCHARGES_TYPE;
                    _viewModel.Data.CINVGRP_CODE = loResult.CINVGRP_CODE;
                    _viewModel.Data.CINVGRP_NAME = loResult.CINVGRP_NAME;

                    _viewModel.Data.CCHARGES_TYPE_DESCR = loResult.CCHARGES_TYPE_DESCR;
                    _viewModel.Data.LTAXABLE = loResult.LTAXABLE;
                    _viewModel.Data.CTAX_ID = "";
                    _viewModel.Data.CTAX_NAME = "";
                }
                else
                {
                    _viewModel.Data.CCHARGES_NAME = "";
                    _viewModel.Data.CCHARGES_TYPE = "";
                    _viewModel.Data.CCHARGES_TYPE_DESCR = "";
                    _viewModel.Data.CINVGRP_CODE = "";
                    _viewModel.Data.CINVGRP_NAME = "";
                    _viewModel.Data.LTAXABLE = false;
                    _viewModel.Data.CTAX_ID = "";
                    _viewModel.Data.CTAX_NAME = "";
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        EndBlock:
            R_DisplayException(loEx);
        }
        private void R_Before_Open_LookupCharges(R_BeforeOpenLookupEventArgs eventArgs)
        {
            LML00200ParameterDTO loParam = new LML00200ParameterDTO()
            {
                CPROPERTY_ID = _viewModel.LOI_Unit.CPROPERTY_ID,
                CCHARGE_TYPE_ID = "01,02,04,05"
            };
            eventArgs.Parameter = loParam;
            eventArgs.TargetPageType = typeof(LML00200);
        }
        private void R_After_Open_LookupCharges(R_AfterOpenLookupEventArgs eventArgs)
        {
            LML00200DTO loTempResult = (LML00200DTO)eventArgs.Result;
            if (loTempResult == null)
            {
                return;
            }
            _viewModel.Data.CCHARGES_ID = loTempResult.CCHARGES_ID;
            _viewModel.Data.CCHARGES_NAME = loTempResult.CCHARGES_NAME;
            _viewModel.Data.CCHARGES_TYPE = loTempResult.CCHARGES_TYPE;
            _viewModel.Data.CCHARGES_TYPE_DESCR = loTempResult.CCHARGES_TYPE_DESCR;
            _viewModel.Data.LTAXABLE = loTempResult.LTAXABLE;
            _viewModel.Data.CINVGRP_CODE = loTempResult.CINVGRP_CODE;
            _viewModel.Data.CINVGRP_NAME = loTempResult.CINVGRP_NAME;
            _viewModel.Data.CTAX_ID = "";
            _viewModel.Data.CTAX_NAME = "";
        }
        #endregion

        #region Tax Lookup
        private async Task Tax_OnLostFocus()
        {
            var loEx = new R_Exception();

            try
            {
                if (string.IsNullOrWhiteSpace(_viewModel.Data.CTAX_ID) == false)
                {
                    GSL00110ParameterDTO loParam = new GSL00110ParameterDTO()
                    {
                        CSEARCH_TEXT = _viewModel.Data.CTAX_ID,
                        CTAX_DATE = DateTime.Now.ToString("yyyyMMdd"),
                    };

                    LookupGSL00110ViewModel loLookupViewModel = new LookupGSL00110ViewModel();

                    var loResult = await loLookupViewModel.GetTaxByDate(loParam);

                    if (loResult == null)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                                typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                                "_ErrLookup01"));
                        _viewModel.Data.CTAX_NAME = "";
                        goto EndBlock;
                    }

                    _viewModel.Data.CTAX_ID = loResult.CTAX_ID;
                    _viewModel.Data.CTAX_NAME = loResult.CTAX_NAME;
                }
                else
                {
                    _viewModel.Data.CTAX_NAME = "";
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        EndBlock:
            R_DisplayException(loEx);
        }
        private void R_Before_Open_LookupTax(R_BeforeOpenLookupEventArgs eventArgs)
        {
            GSL00110ParameterDTO loParam = new GSL00110ParameterDTO()
            {
                CTAX_DATE = DateTime.Now.ToString("yyyyMMdd"),
            };
            eventArgs.Parameter = loParam;
            eventArgs.TargetPageType = typeof(GSL00110);
        }
        private void R_After_Open_LookupTax(R_AfterOpenLookupEventArgs eventArgs)
        {
            GSL00110DTO loTempResult = (GSL00110DTO)eventArgs.Result;
            if (loTempResult == null)
            {
                return;
            }
            _viewModel.Data.CTAX_ID = loTempResult.CTAX_ID;
            _viewModel.Data.CTAX_NAME = loTempResult.CTAX_NAME;
        }
        #endregion

        #region Refresh Page
        public async Task RefreshTabPageAsync(object poParam)
        {
            var loEx = new R_Exception();

            try
            {
                if (poParam !=  null)
                {
                    var loData = (PMT01310DTO)poParam;
                    EnableGreaterClosesSts = int.Parse(loData.CTRANS_STATUS_LOI) >= 80 == false;
                    _viewModel.LOI_Unit = loData;

                    await _viewModel.GetInitialVar();
                    await _gridLOIChargeListRef.R_RefreshGrid(loData);
                }
                else
                {
                    _viewModel.LOI_Unit = new PMT01310DTO();
                    if (_gridLOIChargeListRef.DataSource.Count > 0)
                    {
                        _viewModel.R_SetCurrentData(null);
                        _gridLOIChargeListRef.DataSource.Clear();
                    }
                    if (_gridLOIChargeCallChargeListRef.DataSource.Count > 0)
                    {
                        _gridLOIChargeCallChargeListRef.DataSource.Clear();
                    }
                }

                EnableHasHeaderData = poParam != null;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        #endregion

        #region Value Change
        private async Task CallUnitBool_ValueChanged(bool poParam)
        {
            if (poParam == false)
            {
                if (_gridLOIChargeCallChargeListRef.DataSource.Count > 0)
                {
                    var loValidation = await R_MessageBox.Show("", _localizer["Q01"], R_eMessageBoxButtonType.YesNo);
                    if (loValidation == R_eMessageBoxResult.Yes)
                    {
                        _gridLOIChargeCallChargeListRef.DataSource.Clear();
                        _viewModel.Data.LCAL_UNIT = false;
                        _viewModel.Data.NFEE_AMT = 0;
                        _viewModel.Data.NINVOICE_AMT = 0;
                    }
                    else
                    {
                        return;
                    }
                }

                _viewModel.Data.LCAL_UNIT = false;
                _viewModel.Data.NFEE_AMT = 0;
                _viewModel.Data.NINVOICE_AMT = 0;
            }
            else
            {
                _viewModel.Data.NFEE_AMT = 0;
                _viewModel.Data.NINVOICE_AMT = 0;
                _viewModel.Data.LCAL_UNIT = true;
            }
        }
        private async Task FeeAmountDecimal_ValueChanged(decimal poParam)
        {
            R_Exception loException = new R_Exception();
           _viewModel.Data.NFEE_AMT = poParam;

            try
            {
                await InvoiceAmountCalculate();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            R_DisplayException(loException);
           
        }
        DateTime tStartDate = DateTime.Now.AddDays(-1);
        private async Task PlanStartDate_ValueChanged(DateTime? poParam)
        {
            R_Exception loException = new R_Exception();
            _viewModel.Data.DSTART_DATE = poParam;
            _StartDateTimeDefault = poParam.Value;

            try
            {
                var loData = _viewModel.Data;
                if (poParam.HasValue)
                {
                    DateTime adjustedValue = new DateTime(poParam.Value.Year, poParam.Value.Month, poParam.Value.Day, poParam.Value.Hour, 0, 0);
                    loData.DSTART_DATE = poParam;
                }

                tStartDate = poParam ?? DateTime.Now;

                if (loData.DEND_DATE == null)
                {
                    loData.DEND_DATE = loData.IYEARS == 0 && loData.IMONTHS == 0 && loData.IDAYS == 0
                        ? loData.DSTART_DATE
                        : loData.DSTART_DATE!.Value.AddYears(loData.IYEARS).AddMonths(loData.IMONTHS).AddDays(loData.IDAYS).AddDays(-1);
                }
                else
                {
                    CalculateYMD(poStartDate: loData.DSTART_DATE, poEndDate: loData.DEND_DATE, plStart: true);
                }
                await InvoiceAmountCalculate();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            R_DisplayException(loException);


        }
        private async Task YearInt_ValueChanged(int poParam)
        {
            var loData = _viewModel.Data;
            var llControl = _viewModel.oControlYMD;
            loData.IYEARS = poParam;

            if (llControl.LYEAR || llControl.LMONTH || llControl.LDAY)
            {
                if (loData.IYEARS == 0 && loData.IMONTHS == 0 && loData.IDAYS == 0)
                {
                    loData.DEND_DATE = loData.DSTART_DATE;
                }
                else
                {
                    loData.DEND_DATE = loData.DSTART_DATE!.Value.AddYears(loData.IYEARS).AddMonths(loData.IMONTHS).AddDays(loData.IDAYS).AddDays(-1);
                }
            }
            else
            {
                llControl.LYEAR = true;
                loData.DEND_DATE = loData.DSTART_DATE!.Value.AddYears(loData.IYEARS).AddDays(-1);
            }
            await InvoiceAmountCalculate();
        }
        private async Task MonthInt_ValueChanged(int poParam)
        {
            _viewModel.Data.IMONTHS = poParam;
            var loData = _viewModel.Data;
            var llControl = _viewModel.oControlYMD;

            if (llControl.LYEAR || llControl.LMONTH || llControl.LDAY)
            {
                if (loData.IYEARS == 0 && loData.IMONTHS == 0 && loData.IDAYS == 0)
                {
                    loData.DEND_DATE = loData.DSTART_DATE;
                }
                else
                {
                    loData.DEND_DATE = loData.DSTART_DATE!.Value.AddYears(loData.IYEARS).AddMonths(loData.IMONTHS).AddDays(loData.IDAYS).AddDays(-1);
                }
            }
            else
            {
                llControl.LMONTH = true;
                loData.DEND_DATE = loData.DSTART_DATE!.Value.AddMonths(loData.IMONTHS).AddDays(-1);
            }
            await InvoiceAmountCalculate();

        }
        private async Task DayInt_ValueChanged(int poParam)
        {
            _viewModel.Data.IDAYS = poParam;
            var loData = _viewModel.Data;
            var llControl = _viewModel.oControlYMD;

            if (llControl.LYEAR || llControl.LMONTH || llControl.LDAY)
            {
                if (loData.IYEARS == 0 && loData.IMONTHS == 0 && loData.IDAYS == 0)
                {
                    loData.DEND_DATE = loData.DSTART_DATE;
                }
                else
                {
                    loData.DEND_DATE = loData.DSTART_DATE!.Value.AddYears(loData.IYEARS).AddMonths(loData.IMONTHS).AddDays(loData.IDAYS).AddDays(-1);
                }
            }
            else
            {
                llControl.LYEAR = true;
                loData.DEND_DATE = loData.DSTART_DATE!.Value.AddDays(loData.IDAYS).AddDays(-1);
            }

            await InvoiceAmountCalculate();
        }
        private async Task PlanEndDate_ValueChanged(DateTime? poParam)
        {
            R_Exception loException = new R_Exception();
            _viewModel.Data.DEND_DATE = poParam;
            _EndDateTimeDefault = poParam.Value;

            try
            {
                var loData = _viewModel.Data;

                if (loData.DSTART_DATE == null)
                {
                    loData.DSTART_DATE = loData.IYEARS == 0 && loData.IMONTHS == 0 && loData.IDAYS == 0
                        ? loData.DEND_DATE
                        : loData.DEND_DATE!.Value.AddYears(loData.IYEARS).AddMonths(loData.IMONTHS).AddDays(loData.IDAYS).AddDays(-1);
                }
                else
                {
                    CalculateYMD(poStartDate: loData.DSTART_DATE, poEndDate: loData.DEND_DATE);
                }
                await InvoiceAmountCalculate();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            R_DisplayException(loException);
        }
        private async void BaseOpeningDate_ValueChange(bool poParam)
        {
            _viewModel.Data.LBASED_OPEN_DATE = poParam;
            if (poParam == true)
            {
                _viewModel.Data.DSTART_DATE = null;
                _viewModel.Data.DEND_DATE = null;
            }
            else
            {
                await PlanEndDate_ValueChanged(_EndDateTimeDefault);
                await PlanStartDate_ValueChanged(_StartDateTimeDefault);
            }
        }
        private async Task FeeMethodComboBox_ValueChange(string poParam)
        {
            _viewModel.Data.CFEE_METHOD = poParam;
            if (poParam == "04" || poParam == "03")
            {
                await PeriodMethodComboBox_ValueChange("06");
            }
            else
            {
                await InvoiceAmountCalculate();
            }
        }
        private async Task PeriodMethodComboBox_ValueChange(string poParam)
        {
            _viewModel.Data.CINVOICE_PERIOD = poParam;

            await InvoiceAmountCalculate();
        }
        #endregion

        #region Helper
        private void CalculateYMD(DateTime? poStartDate, DateTime? poEndDate, bool plStart = false)
        {
            R_Exception loException = new R_Exception();
            var loData = _viewModel.Data;

            try
            {
                if (poEndDate != null && poStartDate != null)
                {
                    if (loData.IYEARS == 0 && loData.IMONTHS == 0 && loData.IDAYS == 0)
                    {
                        loData.IDAYS = 1;
                        loData.IMONTHS = loData.IYEARS = 0;
                        if (plStart)
                            loData.DSTART_DATE = loData.DEND_DATE;
                        else
                            loData.DEND_DATE = loData.DSTART_DATE;
                    }
                    else
                    {
                        if (plStart)
                        {
                            loData.DEND_DATE = loData.DSTART_DATE!.Value.AddYears(loData.IYEARS).AddMonths(loData.IMONTHS).AddDays(loData.IDAYS).AddDays(-1);
                        }
                        else
                        {
                            loData.DSTART_DATE = loData.DEND_DATE!.Value.AddYears(-loData.IYEARS).AddMonths(-loData.IMONTHS).AddDays(-loData.IDAYS).AddDays(1);
                        }
                    }
                }
                else
                {
                    if (poStartDate != null)
                    {
                        loData.DEND_DATE = loData.DSTART_DATE!.Value.AddDays(1);
                        loData.IYEARS = loData.IMONTHS = 0;
                        loData.IDAYS = 2;
                    }
                }

            }

            //loData.IYEARS = dValueEndDate.Year - poStartDate!.Value.Year;
            //loData.IMONTHS = dValueEndDate.Month - poStartDate!.Value.Month;}
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            //EndBlocks:

            R_DisplayException(loException);
        }
        private async Task InvoiceAmountCalculate()
        {
            var loData = _viewModel.Data;
            decimal lnSummary = 0;
            decimal lnGrossAmt = loData.LCAL_UNIT ? 1 : _viewModel.LOI_Unit.NTOTAL_GROSS_AREA;

            decimal lnDays = ((loData.IDAYS * loData.NFEE_AMT * lnGrossAmt) / 30);
            decimal lnTenurePeriod = ((loData.IYEARS * 12) + loData.IMONTHS);
            decimal lnPeriodMode = loData.CINVOICE_PERIOD == "01" ? 1 :
                                  loData.CINVOICE_PERIOD == "02" ? 2 :
                                  loData.CINVOICE_PERIOD == "03" ? 3 :
                                  loData.CINVOICE_PERIOD == "04" ? 6 :
                                  loData.CINVOICE_PERIOD == "05" ? 12 :
                                  loData.CINVOICE_PERIOD == "06" ? lnTenurePeriod : 0;


            if (loData.NFEE_AMT > 0)
            {
                if (loData.IYEARS == 0)
                {
                    if (loData.CINVOICE_PERIOD != "06")
                    {
                        lnDays = loData.IMONTHS < lnPeriodMode ? lnDays : 0;
                    }
                    lnPeriodMode = loData.IMONTHS < lnPeriodMode ? loData.IMONTHS : lnPeriodMode;
                }

                if (loData.CFEE_METHOD == "01")
                {
                    lnSummary = (loData.NFEE_AMT * lnGrossAmt * lnPeriodMode) / 1;
                    loData.NINVOICE_AMT = lnSummary + lnDays ;
                }
                else if (loData.CFEE_METHOD == "02")
                {

                    lnSummary = (loData.NFEE_AMT * lnGrossAmt * lnPeriodMode) / 12;
                    loData.NINVOICE_AMT = lnSummary + lnDays ;
                }
                else if (loData.CFEE_METHOD == "03")
                {
                    loData.NINVOICE_AMT = loData.NFEE_AMT * loData.IDAYS * lnGrossAmt;
                }
                else
                {
                    loData.NINVOICE_AMT = ((loData.NFEE_AMT * lnGrossAmt * 1));
                }
            }
            else
            {
                loData.NINVOICE_AMT = 0;
            }

            await Task.CompletedTask;
        }
        #endregion

        #region RevenueSharing
        private async Task RevenueSharing_Before_Open_Popup(R_BeforeOpenPopupEventArgs eventArgs)
        {
            var loData = (PMT01330DTO)_conductorRef.R_GetCurrentData();
            await Task.CompletedTask;
            loData.CCHARGE_SEQ_NO = loData.CSEQ_NO;
            loData.LIS_CLOSE_STATUS = EnableGreaterClosesSts;
            eventArgs.Parameter = loData;
            eventArgs.TargetPageType = typeof(PMT01331);
        }
        #endregion

        #region BTN Process
        private async Task InvoicePlanBTN_OnClick()
        {
            var loEx = new R_Exception();

            try
            {
                PMT01300LOICallBackParameterDTO loData = new PMT01300LOICallBackParameterDTO { CRUD_MODE = _conductorRef.R_ConductorMode == R_BlazorFrontEnd.Enums.R_eConductorMode.Normal, TO_INVOICE_TAB = true, SELECTED_DATA_TAB_CHARGES = _gridLOIChargeListRef.CurrentSelectedData };
                await InvokeTabEventCallbackAsync(loData);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        #endregion

        #region Charge Active / Inactive
        private async Task Charge_Activate_Before_Open_Popup(R_BeforeOpenPopupEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            try
            {
                var loGetData = (PMT01330DTO)_conductorRef.R_GetCurrentData();
                var loValidateViewModel = new GFF00900Model.ViewModel.GFF00900ViewModel();
                loValidateViewModel.ACTIVATE_INACTIVE_ACTIVITY_CODE = "PMT01301"; //Uabh Approval Code sesuai Spec masing masing
                await loValidateViewModel.RSP_ACTIVITY_VALIDITYMethodAsync(); //Jika IAPPROVAL_CODE == 3, maka akan keluar RSP_ERROR disini

                //Jika Approval User ALL dan Approval Code 1, maka akan langsung menjalankan ActiveInactive
                if (loValidateViewModel.loRspActivityValidityList.FirstOrDefault().CAPPROVAL_USER == "ALL" && loValidateViewModel.loRspActivityValidityResult.Data.FirstOrDefault().IAPPROVAL_MODE == 1)
                {
                    var loParam = R_FrontUtility.ConvertObjectToObject<PMT01330ActiveInactiveDTO>(loGetData);
                    await _viewModel.ActiveInactiveProcessAsync(loParam);
                    await _conductorRef.R_SetCurrentData(loGetData);
                    return;
                }
                else //Disini Approval Code yang didapat adalah 2, yang berarti Active Inactive akan dijalankan jika User yang diinput ada di RSP_ACTIVITY_VALIDITY
                {
                    eventArgs.Parameter = new GFF00900ParameterDTO()
                    {
                        Data = loValidateViewModel.loRspActivityValidityList,
                        IAPPROVAL_CODE = "PMT01301" //Uabh Approval Code sesuai Spec masing masing
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
        private async Task Charge_Activate_After_Open_Popup(R_AfterOpenPopupEventArgs eventArgs)
        {

            R_Exception loException = new R_Exception();
            try
            {
                var loGetData = (PMT01330DTO)_conductorRef.R_GetCurrentData();

                if (eventArgs.Success == false)
                {
                    return;
                }

                bool result = (bool)eventArgs.Result;
                if (result == true)
                {
                    var loActiveData = await _viewModel.ActiveInactiveProcessAsync(loGetData);
                    await _conductorRef.R_SetCurrentData(loActiveData);
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }
        #endregion
    }
}
