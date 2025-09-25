using BlazorClientHelper;
using GFF00900COMMON.DTOs;
using PMM01000COMMON;
using PMM01000FrontResources;
using PMM01000MODEL;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using Lookup_GSModel.ViewModel;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Enums;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Controls.Popup;
using R_BlazorFrontEnd.Controls.Tab;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using R_LockingFront;

namespace PMM01000FRONT
{
    public partial class PMM01000 : R_Page
    {
        private PMM01000ViewModel _General_viewModel = new PMM01000ViewModel();
        private PMM01000UniversalViewModel _Universal_viewModel = new PMM01000UniversalViewModel();

        private R_Grid<PMM01000UniversalDTO> _General_gridRef;
        private R_Grid<PMM01000DTO> _UtilityCharges_gridRef;

        private R_Conductor _General_conductorRef;
        private R_Conductor _UtilityCharges_conductorRef;

        private string _Genereal_lcLabel = "";

        [Inject] IClientHelper clientHelper { get; set; }
        [Inject] private R_PopupService PopupService { get; set; }

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                await _General_viewModel.GetAllInitList();

                if (_General_viewModel.PropertyList.Count > 0)
                {
                    PMM01000DTOPropety loParam = _General_viewModel.PropertyList.FirstOrDefault();
                    _General_viewModel.PropertyValueContext = loParam.CPROPERTY_ID;
                    await PropertyDropdown_OnChange(loParam.CPROPERTY_ID);
                }
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
                var loData = (PMM01000DTO)eventArgs.Data;

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
                        Program_Id = "PMM01000",
                        Table_Name = "PMM_UTILITY_CHARGES",
                        Key_Value = string.Join("|", clientHelper.CompanyId, loData.CPROPERTY_ID, loData.CCHARGES_TYPE, loData.CCHARGES_ID)
                    };

                    loLockResult = await loCls.R_Lock(loLockPar);
                }
                else
                {
                    var loUnlockPar = new R_ServiceLockingUnLockParameterDTO
                    {
                        Company_Id = clientHelper.CompanyId,
                        User_Id = clientHelper.UserId,
                        Program_Id = "PMM01000",
                        Table_Name = "PMM_UTILITY_CHARGES",
                        Key_Value = string.Join("|", clientHelper.CompanyId, loData.CPROPERTY_ID, loData.CCHARGES_TYPE, loData.CCHARGES_ID)
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
        private R_TabPage _tabPageRate;
        private R_TabStripTab _tabStripTabRate;

        private async Task PropertyDropdown_OnChange(string poParam)
        {
            var loEx = new R_Exception();
            var loData = _General_gridRef.CurrentSelectedData;
            
            try
            {
                _General_viewModel.PropertyValueContext = string.IsNullOrWhiteSpace(poParam) ? "" : poParam;

                if (_General_viewModel.ChargesTypeGrid.Count > 0)
                {
                    await _UtilityCharges_gridRef.R_RefreshGrid(loData);
                }
                else
                {
                    await _General_gridRef.R_RefreshGrid(null);
                    return;
                }

                var loTempParamUtility = _UtilityCharges_conductorRef.R_GetCurrentData();
                if (_UtilityCharges_conductorRef.R_ConductorMode == R_eConductorMode.Normal)
                {
                    if (_TabGeneral.ActiveTab.Id == "Rate" )
                    {
                        await _tabPageRate.InvokeRefreshTabPageAsync(loTempParamUtility);
                    }
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private async Task ChargesTypeGrid_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _General_viewModel.GetChargesTypeList();

                eventArgs.ListEntityResult = _General_viewModel.ChargesTypeGrid;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void ChargesTypeGrid_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            eventArgs.Result = eventArgs.Data;
        }
        private async Task ChargesTypeGrid_Display(R_DisplayEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loData = (PMM01000UniversalDTO)eventArgs.Data;
                if (eventArgs.ConductorMode == R_eConductorMode.Normal)
                {
                    if (loData != null)
                    {
                        await _UtilityCharges_gridRef.R_RefreshGrid(loData);
                        _General_viewModel.hiddenDisplay = loData.CCODE == "09";
                    }
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        #region Utility 
        private R_TabStrip _TabGeneral;
        private R_TextBox ChargesName_TextBox;
        private async Task UtilityCharges_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loTempParam = (PMM01000UniversalDTO)eventArgs.Parameter;
                var loParam = new PMM01002DTO()
                {
                    CCHARGES_TYPE = loTempParam.CCODE
                };
                await _General_viewModel.GetChargesUtilityList(loParam);

                eventArgs.ListEntityResult = _General_viewModel.ChargesUtilityGrid;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task UtilityCharges_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<PMM01000DTO>(eventArgs.Data);
                await _General_viewModel.GetChargesUtility(loParam);

                eventArgs.Result = _General_viewModel.UtilityCharges;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task UtilityCharges_Display(R_DisplayEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                if (eventArgs.ConductorMode == R_eConductorMode.Normal)
                {
                    var loParam = (PMM01000DTO)eventArgs.Data;
                    if (!loParam.LACTIVE)
                    {
                        _Genereal_lcLabel = _localizer["_Active"];
                        _General_viewModel.StatusChange = true;
                    }
                    else
                    {
                        _Genereal_lcLabel = _localizer["_Inactive"];
                        _General_viewModel.StatusChange = false;
                    }

                }
                if (eventArgs.ConductorMode == R_eConductorMode.Edit)
                {
                    await ChargesName_TextBox.FocusAsync();
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }


        private async Task OtherTaxID_OnLostFocus()
        {
            var loEx = new R_Exception();

            try
            {
                var loData = (PMM01000DTO)_UtilityCharges_conductorRef.R_GetCurrentData();
                if (string.IsNullOrWhiteSpace(loData.COTHER_TAX_ID) == false)
                {
                    var param = new GSL00100ParameterDTO
                    {
                        CSEARCH_TEXT = loData.COTHER_TAX_ID
                    };

                    LookupGSL00100ViewModel loLookupViewModel = new LookupGSL00100ViewModel();

                    var loResult = await loLookupViewModel.GetSalesTax(param);

                    if (loResult == null)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                                typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                                "_ErrLookup01"));
                        loData.CTAX_OTHER_NAME = "";
                        goto EndBlock;
                    }
                    loData.COTHER_TAX_ID = loResult.CTAX_ID;
                    loData.CTAX_OTHER_NAME = loResult.CTAX_NAME;
                }
                else
                {
                    loData.CTAX_OTHER_NAME = "";
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        EndBlock:
            R_DisplayException(loEx);
        }
        private void UtilityCharges_OtherTax_Before_Open_Lookup(R_BeforeOpenLookupEventArgs eventArgs)
        {
            var param = new GSL00100ParameterDTO();

            eventArgs.Parameter = param;
            eventArgs.TargetPageType = typeof(GSL00100);
        }

        private void UtilityCharges_OtherTax_After_Open_Lookup(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loTempResult = (GSL00100DTO)eventArgs.Result;
            if (loTempResult == null)
            {
                return;
            }
            var loData = (PMM01000DTO)_UtilityCharges_conductorRef.R_GetCurrentData();

            loData.COTHER_TAX_ID = loTempResult.CTAX_ID;
            loData.CTAX_OTHER_NAME = loTempResult.CTAX_NAME;
        }

        private async Task WitholdTaxID_OnLostFocus()
        {
            var loEx = new R_Exception();

            try
            {
                var loData = (PMM01000DTO)_UtilityCharges_conductorRef.R_GetCurrentData();
                if (string.IsNullOrWhiteSpace(loData.CWITHHOLDING_TAX_ID) == false)
                {
                    var param = new GSL00200ParameterDTO
                    {
                        CPROPERTY_ID = _General_viewModel.PropertyValueContext,
                        CTAX_TYPE_LIST = loData.CWITHHOLDING_TAX_TYPE,
                        CSEARCH_TEXT = loData.CWITHHOLDING_TAX_ID
                    };

                    LookupGSL00200ViewModel loLookupViewModel = new LookupGSL00200ViewModel();

                    var loResult = await loLookupViewModel.GetWithholdingTax(param);

                    if (loResult == null)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                                typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                                "_ErrLookup01"));
                        loData.CWITHHOLDING_TAX_NAME = "";
                        loData.NTAX_PERCENTAGE_WITHHOLDING = 0;
                        goto EndBlock;
                    }
                    loData.CWITHHOLDING_TAX_ID = loResult.CTAX_ID;
                    loData.CWITHHOLDING_TAX_NAME = loResult.CTAX_NAME;
                    loData.NTAX_PERCENTAGE_WITHHOLDING = loResult.NTAX_PERCENTAGE;
                }
                else
                {
                    loData.CWITHHOLDING_TAX_NAME = "";
                    loData.NTAX_PERCENTAGE_WITHHOLDING = 0;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        EndBlock:
            R_DisplayException(loEx);
        }
        private void UtilityCharges_WithholdingTax_Before_Open_Lookup(R_BeforeOpenLookupEventArgs eventArgs)
        {
            var param = new GSL00200ParameterDTO();
            var loData = (PMM01000DTO)_UtilityCharges_conductorRef.R_GetCurrentData();

            param.CPROPERTY_ID = _General_viewModel.PropertyValueContext;
            param.CTAX_TYPE_LIST = loData.CWITHHOLDING_TAX_TYPE;

            eventArgs.Parameter = param;
            eventArgs.TargetPageType = typeof(GSL00200);
        }

        private void UtilityCharges_WithholdingTax_After_Open_Lookup(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loTempResult = (GSL00200DTO)eventArgs.Result;
            if (loTempResult == null)
            {
                return;
            }
            var loData = (PMM01000DTO)_UtilityCharges_conductorRef.R_GetCurrentData();

            loData.CWITHHOLDING_TAX_ID = loTempResult.CTAX_ID;
            loData.CWITHHOLDING_TAX_NAME = loTempResult.CTAX_NAME;
            loData.NTAX_PERCENTAGE_WITHHOLDING = loTempResult.NTAX_PERCENTAGE;
        }
        private async Task JrnlCode_OnLostFocus()
        {
            var loEx = new R_Exception();

            try
            {
                var loData = (PMM01000DTO)_UtilityCharges_conductorRef.R_GetCurrentData();
                if (string.IsNullOrWhiteSpace(loData.CUTILITY_JRNGRP_CODE) == false)
                {
                    var param = new GSL00400ParameterDTO
                    {
                        CPROPERTY_ID = _General_viewModel.PropertyValueContext,
                        CJRNGRP_TYPE = "11",
                        CSEARCH_TEXT = loData.CUTILITY_JRNGRP_CODE
                    };

                    LookupGSL00400ViewModel loLookupViewModel = new LookupGSL00400ViewModel();

                    var loResult = await loLookupViewModel.GetJournalGroup(param);

                    if (loResult == null)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                                typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                                "_ErrLookup01"));
                        loData.CUTILITY_JRNGRP_NAME = "";
                        loData.LACCRUAL = false;
                        _General_viewModel.Accrual = false;
                        goto EndBlock;
                    }
                    loData.CUTILITY_JRNGRP_CODE = loResult.CJRNGRP_CODE;
                    loData.CUTILITY_JRNGRP_NAME = loResult.CJRNGRP_NAME;
                    loData.LACCRUAL = loResult.LACCRUAL;
                    _General_viewModel.Accrual = loResult.LACCRUAL;
                }
                else
                {
                    loData.CUTILITY_JRNGRP_NAME = "";
                    loData.LACCRUAL = false;
                    _General_viewModel.Accrual = false;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        EndBlock:
            R_DisplayException(loEx);
        }
        private void UtilityCharges_JournalGrp_Before_Open_Lookup(R_BeforeOpenLookupEventArgs eventArgs)
        {
            var param = new GSL00400ParameterDTO()
            {
                CPROPERTY_ID = _General_viewModel.PropertyValueContext,
                CJRNGRP_TYPE = "11"
            };

            eventArgs.Parameter = param;
            eventArgs.TargetPageType = typeof(GSL00400);
        }

        private void UtilityCharges_JournalGrp_After_Open_Lookup(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loTempResult = (GSL00400DTO)eventArgs.Result;
            if (loTempResult == null)
            {
                return;
            }

            var loData = (PMM01000DTO)_UtilityCharges_conductorRef.R_GetCurrentData();
            loData.CUTILITY_JRNGRP_CODE = loTempResult.CJRNGRP_CODE;
            loData.CUTILITY_JRNGRP_NAME = loTempResult.CJRNGRP_NAME;
            loData.LACCRUAL = loTempResult.LACCRUAL;
            _General_viewModel.Accrual = loTempResult.LACCRUAL;
        }

        private bool TaxExemptionEnable = false;
        private void Taxable_OnChnaged(bool poParam)
        {
            _General_viewModel.Data.LTAXABLE = poParam;

            TaxExemptionEnable = (bool)poParam;
            var loData = (PMM01000DTO)_UtilityCharges_conductorRef.R_GetCurrentData();

            TaxExemptionCodeEnable = (bool)poParam && loData.LTAX_EXEMPTION;
            if (!(bool)poParam)
            {
                loData.LTAX_EXEMPTION = false;
                loData.CTAX_EXEMPTION_CODE = "";
                loData.NTAX_EXEMPTION_PCT= 0;
            }
        }

        private bool TaxExemptionCodeEnable = false;
        private void TaxExemption_OnChnaged(bool poParam)
        {
            _General_viewModel.Data.LTAX_EXEMPTION = poParam;

            TaxExemptionCodeEnable = (bool)poParam;
            var loData = (PMM01000DTO)_UtilityCharges_conductorRef.R_GetCurrentData();

            if (!(bool)poParam)
            {
                loData.CTAX_EXEMPTION_CODE = "";
                loData.NTAX_EXEMPTION_PCT= 0;
            }
        }

        private bool OtherTaxLookupEnable = false;
        private void OtherTax_OnChnaged(bool poParam)
        {
            _General_viewModel.Data.LOTHER_TAX = poParam;

            OtherTaxLookupEnable = (bool)poParam;
            var loData = (PMM01000DTO)_UtilityCharges_conductorRef.R_GetCurrentData();

            if (!(bool)poParam)
            {
                loData.COTHER_TAX_ID = "";
                loData.CTAX_OTHER_NAME = "";
            }
        }

        private bool WithholdingEnable = false;
        private void WithholdingTax_OnChange(bool poParam)
        {
            _General_viewModel.Data.LWITHHOLDING_TAX = poParam;

            WithholdingEnable = (bool)poParam;

            if (!(bool)poParam)
            {
                _General_viewModel.Data.CWITHHOLDING_TAX_TYPE = "";
                _General_viewModel.Data.CWITHHOLDING_TAX_ID = "";
                _General_viewModel.Data.CWITHHOLDING_TAX_NAME = "";
            }
        }
        private void WithholdingTaxType_OnChange(string poParam)
        {
            _General_viewModel.Data.CWITHHOLDING_TAX_TYPE = poParam;
        }

        private bool _EnableAddEdit = false;

        private void UtilityCharges_SetAdd(R_SetEventArgs eventArgs)
        {
            _EnableAddEdit = eventArgs.Enable;
        }

        private void UtilityCharges_SetEdit(R_SetEventArgs eventArgs)
        {
            _EnableAddEdit = eventArgs.Enable;
        }

        private void UtilityCharges_CheckAdd(R_CheckAddEventArgs eventArgs)
        {
            eventArgs.Allow = !string.IsNullOrEmpty(_General_viewModel.PropertyValueContext);
        }

        private async Task UtilityCharges_Validation(R_ValidationEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            R_PopupResult loResult = null;

            try
            {
                
                var loData = (PMM01000DTO)eventArgs.Data;
                await _General_viewModel.ValidationUtility(loData);

                if (eventArgs.ConductorMode == R_eConductorMode.Edit)
                {
                    if (!_General_viewModel.Accrual)
                    {
                        if (loData.LACCRUAL)
                        {
                            var loValidate = await R_MessageBox.Show("", R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "_NotifDisableAccrualMethod"), R_eMessageBoxButtonType.YesNo);

                            eventArgs.Cancel = loValidate == R_eMessageBoxResult.No;
                        }
                    }
                }
                else if (eventArgs.ConductorMode == R_eConductorMode.Add)
                {
                    if (loData.LACTIVE)
                    {
                        var loGetData = loData;

                        var loValidateViewModel = new GFF00900Model.ViewModel.GFF00900ViewModel();
                        loValidateViewModel.ACTIVATE_INACTIVE_ACTIVITY_CODE = "PMM01001"; //Uabh Approval Code sesuai Spec masing masing
                        await loValidateViewModel.RSP_ACTIVITY_VALIDITYMethodAsync(); //Jika IAPPROVAL_CODE == 3, maka akan keluar RSP_ERROR disini

                        //Jika Approval User ALL dan Approval Code 1, maka akan langsung menjalankan ActiveInactive
                        if (loValidateViewModel.loRspActivityValidityList.FirstOrDefault().CAPPROVAL_USER == "ALL" && loValidateViewModel.loRspActivityValidityResult.Data.FirstOrDefault().IAPPROVAL_MODE == 1)
                        {
                        }
                        else //Disini Approval Code yang didapat adalah 2, yang berarti Active Inactive akan dijalankan jika User yang diinput ada di RSP_ACTIVITY_VALIDITY
                        {
                            var loPopupParam = new GFF00900ParameterDTO()
                            {
                                Data = loValidateViewModel.loRspActivityValidityList,
                                IAPPROVAL_CODE = "PMM01001" //Uabh Approval Code sesuai Spec masing masing
                            };
                            loResult = await PopupService.Show(typeof(GFF00900FRONT.GFF00900), loPopupParam);

                            if (loResult.Success == false)
                            {
                                eventArgs.Cancel = true;
                                return;
                            }


                            bool result = (bool)loResult.Result;
                            if (result == true)
                            {
                            }
                            else
                            {
                                eventArgs.Cancel = true;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void Grid_R_SetOther(R_SetEventArgs eventArgs)
        {
            _comboboxEnabled = eventArgs.Enable;
            _pageSupplierOnCRUDmode = !eventArgs.Enable;
        }

        private R_TextBox ChargesId_TextBox;
        private async Task UtilityCharges_AfterAdd(R_AfterAddEventArgs eventArgs)
        {
            var loTypeData = _General_gridRef.CurrentSelectedData;
            var loData = (PMM01000DTO)eventArgs.Data;
            loData.CCHARGES_TYPE = loTypeData.CCODE;
            _General_viewModel.Accrual = false;
            await ChargesId_TextBox.FocusAsync();
        }

        private async Task UtilityCharges_ServiceSave(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loTypeData = _General_gridRef.CurrentSelectedData;
                PMM01000DTO loData = (PMM01000DTO)eventArgs.Data;
                if (eventArgs.ConductorMode == R_eConductorMode.Add)
                {
                    loData.CCHARGES_TYPE = loTypeData.CCODE;
                    loData.CPROPERTY_ID = _General_viewModel.PropertyValueContext;
                }
                await _General_viewModel.SaveChargesUtility((PMM01000DTO)eventArgs.Data, (eCRUDMode)eventArgs.ConductorMode);

                eventArgs.Result = _General_viewModel.UtilityCharges;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task UtilityCharges_BeforeDelete(R_BeforeDeleteEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            R_eMessageBoxResult loValidate;
            try
            {
                var loCheckData = await _General_viewModel.ValidateBeforeDelete((PMM01000DTO)eventArgs.Data);
                if (loCheckData.LHAS_DETAIL)
                {
                    loValidate = await R_MessageBox.Show("", R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "_NotifBeforeDelete"), R_eMessageBoxButtonType.YesNo);
                    eventArgs.Cancel = loValidate == R_eMessageBoxResult.No;
                }
                else
                {
                    loValidate = await R_MessageBox.Show("", R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "_NotifBeforeDeleteDefault"), R_eMessageBoxButtonType.YesNo);
                    eventArgs.Cancel = loValidate == R_eMessageBoxResult.No;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task UtilityCharges_ServiceDelete(R_ServiceDeleteEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _General_viewModel.DeleteChargesUtility((PMM01000DTO)eventArgs.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private bool CopyNewBtnEnable = false;
        private bool ActiveInactiveBtnEnable = false;
        private bool PrintBtnEnable = false;
        private bool SetHasData = false;
        private bool SetHasDataTab = false;
        private void UtilityCharges_SetHasData(R_SetEventArgs eventArgs)
        {
            CopyNewBtnEnable = eventArgs.Enable;
            ActiveInactiveBtnEnable = eventArgs.Enable;
            PrintBtnEnable = eventArgs.Enable;
            SetHasData = eventArgs.Enable;
            SetHasDataTab = eventArgs.Enable && _General_viewModel.Data.CCHARGES_TYPE != "09";
        }

        private async Task UtilityCharges_R_Before_Open_Popup_ActivateInactive(R_BeforeOpenPopupEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            try
            {
                var loGetData = (PMM01000DTO)_UtilityCharges_conductorRef.R_GetCurrentData();

                var loValidateViewModel = new GFF00900Model.ViewModel.GFF00900ViewModel();
                loValidateViewModel.ACTIVATE_INACTIVE_ACTIVITY_CODE = "PMM01001"; //Uabh Approval Code sesuai Spec masing masing
                await loValidateViewModel.RSP_ACTIVITY_VALIDITYMethodAsync(); //Jika IAPPROVAL_CODE == 3, maka akan keluar RSP_ERROR disini

                //Jika Approval User ALL dan Approval Code 1, maka akan langsung menjalankan ActiveInactive
                if (loValidateViewModel.loRspActivityValidityList.FirstOrDefault().CAPPROVAL_USER == "ALL" && loValidateViewModel.loRspActivityValidityResult.Data.FirstOrDefault().IAPPROVAL_MODE == 1)
                {
                    await _General_viewModel.ActiveInactiveProcessAsync(loGetData);
                    await _UtilityCharges_conductorRef.R_SetCurrentData(loGetData);
                    return;
                }
                else //Disini Approval Code yang didapat adalah 2, yang berarti Active Inactive akan dijalankan jika User yang diinput ada di RSP_ACTIVITY_VALIDITY
                {
                    eventArgs.Parameter = new GFF00900ParameterDTO()
                    {
                        Data = loValidateViewModel.loRspActivityValidityList,
                        IAPPROVAL_CODE = "PMM01001" //Uabh Approval Code sesuai Spec masing masing
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

        private async Task UtilityCharges_R_After_Open_Popup_ActivateInactive(R_AfterOpenPopupEventArgs eventArgs)
        {
            var loGetData = (PMM01000DTO)_UtilityCharges_conductorRef.R_GetCurrentData();

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
                    var loActiveData = await _General_viewModel.ActiveInactiveProcessAsync(loGetData);
                    await _UtilityCharges_conductorRef.R_SetCurrentData(loActiveData);
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        private void UtilityCharges_R_Before_Open_Popup_Print(R_BeforeOpenPopupEventArgs eventArgs)
        {
            var loGetData = (PMM01000DTO)_General_viewModel.R_GetCurrentData();

            R_Exception loException = new R_Exception();
            try
            {
                bool lCancel;

                lCancel = string.IsNullOrEmpty(_General_viewModel.PropertyValueContext);
                if (lCancel)
                {
                    loException.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "1000"));
                }
                else
                {
                    var loParam = _General_viewModel.PropertyList.FirstOrDefault(p => p.CPROPERTY_ID == _General_viewModel.PropertyValueContext);

                    eventArgs.Parameter = loParam;
                    eventArgs.TargetPageType = typeof(PMM01002);
                }

            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();

        }

        private async Task UtilityCharges_R_After_Open_Popup_Print(R_AfterOpenPopupEventArgs eventArgs)
        {
            var loGetData = (PMM01000DTO)_General_viewModel.R_GetCurrentData();

            R_Exception loException = new R_Exception();
            try
            {

            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }


        private void UtilityCharges_CopyFromBtn_Before_Open_Popup(R_BeforeOpenPopupEventArgs eventArgs)
        {
            var loData = (PMM01000DTO)_General_viewModel.R_GetCurrentData();
            var loParam = new PMM01003DTO()
            {
                CPROPERTY_ID = loData.CPROPERTY_ID,
                CCHARGES_TYPE = loData.CCHARGES_TYPE,
                CCURRENT_CHARGES_ID = loData.CCHARGES_ID,
                CCURRENT_CHARGES_NAME = loData.CCHARGES_NAME,

            };

            eventArgs.Parameter = loParam;
            eventArgs.TargetPageType = typeof(PMM01003);
        }

        private async Task UtilityCharges_CopyFromBtn_After_Open_Popup(R_AfterOpenPopupEventArgs eventArgs)
        {
            var loTempData = (PMM01003DTO)eventArgs.Result;

            R_Exception loException = new R_Exception();
            try
            {
                if (eventArgs.Result == null)
                {
                    return;
                }

                var loData = new PMM01000DTO()
                {
                    CCHARGES_ID = loTempData.CNEW_CHARGES_ID,
                    CCHARGES_TYPE = loTempData.CCHARGES_TYPE,
                    CPROPERTY_ID = loTempData.CPROPERTY_ID,
                };

                var loTempParam = _General_gridRef.CurrentSelectedData;
                await _UtilityCharges_gridRef.R_RefreshGrid(loTempParam);

                await _UtilityCharges_conductorRef.R_GetEntity(loData);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        #endregion
        private string _ChargesTypeVal = "";
        private string _ChargesIDVall = "";

        private void _General_Before_Open_TabPage(R_BeforeOpenTabPageEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            try
            {
                var loTempParam = _General_gridRef.CurrentSelectedData;
                var loTempParamUtility = (PMM01000DTO)_UtilityCharges_conductorRef.R_GetCurrentData();
                loTempParamUtility.CCHARGES_TYPE_DESCR = loTempParam.CDESCRIPTION;

                eventArgs.Parameter = loTempParamUtility;

                if (loTempParam.CCODE == "01" || loTempParam.CCODE == "02")
                {
                    eventArgs.TargetPageType = typeof(PMM01010);
                }
                else if (loTempParam.CCODE == "03" || loTempParam.CCODE == "04")
                {
                    eventArgs.TargetPageType = typeof(PMM01020);
                }
                else if (loTempParam.CCODE == "05" || loTempParam.CCODE == "06")
                {
                    eventArgs.TargetPageType = typeof(PMM01030);
                }
                else if (loTempParam.CCODE == "07")
                {
                    eventArgs.TargetPageType = typeof(PMM01040);
                }
                else if (loTempParam.CCODE == "08")
                {
                    eventArgs.TargetPageType = typeof(PMM01050);
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
            
        }

        private bool _comboboxEnabled = true;
        private bool _pageSupplierOnCRUDmode = false;
        private void R_TabEventCallback(object poValue)
        {
            _comboboxEnabled = (bool)poValue;
            _pageSupplierOnCRUDmode = !(bool)poValue;
        }

        private void OnActiveTabIndexChanging(R_TabStripActiveTabIndexChangingEventArgs eventArgs)
        {
            eventArgs.Cancel = _pageSupplierOnCRUDmode;
        }

        private void UtilityCharges_BeforeCancel(R_BeforeCancelEventArgs eventArgs)
        {
            TaxExemptionEnable = false;
            TaxExemptionCodeEnable = false;
            OtherTaxLookupEnable = false;
            WithholdingEnable = false;
        }

        private void UtilityCharges_AfterSave(R_AfterSaveEventArgs eventArgs)
        {
            TaxExemptionEnable = false;
            TaxExemptionCodeEnable = false;
            OtherTaxLookupEnable = false;
            WithholdingEnable = false;
        }

    }
}
