using BlazorClientHelper;
using GFF00900COMMON.DTOs;
using PMM01500COMMON;
using PMM01500FrontResources;
using PMM01500MODEL;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using Lookup_GSModel.ViewModel;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
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
using System;
using System.Security.Principal;
using System.Xml.Linq;
using System.Diagnostics.Tracing;

namespace PMM01500FRONT
{
    public partial class PMM01500 : R_Page
    {
        private PMM01500ViewModel _Genereal_viewModel = new PMM01500ViewModel();

        private R_Grid<PMM01501DTO> _Genereal_gridRef;

        private R_Conductor _Genereal_conductorRef;

        private string _Genereal_lcLabel = "Actived";
        [Inject] IClientHelper clientHelper { get; set; }
        [Inject] private R_PopupService PopupService { get; set; }

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                await _Genereal_viewModel.GetUniversalList();

                if (_Genereal_viewModel.PropertyList.Count > 0)
                {
                    PMM01500DTOPropety loParam = _Genereal_viewModel.PropertyList.FirstOrDefault();
                    _Genereal_viewModel.PropertyValueContext = loParam.CPROPERTY_ID;
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
                var loData = (PMM01500DTO)eventArgs.Data;

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
                        Program_Id = "PMM01500",
                        Table_Name = "PMM_INVGRP",
                        Key_Value = string.Join("|", clientHelper.CompanyId, loData.CPROPERTY_ID, loData.CINVGRP_CODE)
                    };

                    loLockResult = await loCls.R_Lock(loLockPar);
                }
                else
                {
                    var loUnlockPar = new R_ServiceLockingUnLockParameterDTO
                    {
                        Company_Id = clientHelper.CompanyId,
                        User_Id = clientHelper.UserId,
                        Program_Id = "PMM01500",
                        Table_Name = "PMM_INVGRP",
                        Key_Value = string.Join("|", clientHelper.CompanyId, loData.CPROPERTY_ID, loData.CINVGRP_CODE)
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

        private R_TabStrip _TabGeneral;
        private R_TabStrip _TabBankAccount;
        private R_TabPage _tabPagePinalty;
        private R_TabPage _tabPageOtherCharges;
        private R_TabPage _tabPageBankAccount;

        private async Task PropertyDropdown_OnChange(string poParam)
        {
            var loEx = new R_Exception();

            try
            {
                var lcPropertyId = string.IsNullOrWhiteSpace(poParam) ? "" : poParam;
                _Genereal_viewModel.PropertyValueContext = string.IsNullOrWhiteSpace(poParam) ? "" : poParam;

                await _Genereal_viewModel.GetStampRateList(lcPropertyId);
                await _Genereal_viewModel.GetInvoiceTemplate(lcPropertyId);

                await _Genereal_gridRef.R_RefreshGrid(null);

                if (_Genereal_conductorRef.R_ConductorMode == R_eConductorMode.Normal)
                {
                    var loData = (PMM01500DTO)_Genereal_conductorRef.R_GetCurrentData();
                    loData.LTabEnalbleDept = TabEnalbleDept && _Genereal_viewModel.InvoinceGroupGrid.Count > 0;
                    if (_TabGeneral.ActiveTab.Id == "Pinalty")
                    {
                        await _tabPagePinalty.InvokeRefreshTabPageAsync(loData);
                        return;
                    }
                    else if (_TabGeneral.ActiveTab.Id == "OtherCharges")
                    {
                        await _tabPageOtherCharges.InvokeRefreshTabPageAsync(loData);
                        return;
                    }

                    if (_TabBankAccount.ActiveTab.Id == "BankAccount")
                    {
                        await _tabPageBankAccount.InvokeRefreshTabPageAsync(loData);
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        #region TabStrip

        private bool _comboboxEnabled = true;

        private void R_TabEventCallback(object poValue)
        {
            _comboboxEnabled = (bool)poValue;
            _pageSupplierOnCRUDmode = !(bool)poValue;
        }

        private void _PinaltyTab_Before_Open_TabPage(R_BeforeOpenTabPageEventArgs eventArgs)
        {
            var loParam = _Genereal_gridRef.CurrentSelectedData;
            loParam.CPROPERTY_ID = _Genereal_viewModel.PropertyValueContext;
            eventArgs.TargetPageType = typeof(PMM01520);
            eventArgs.Parameter = loParam;
        }

        private void _OtherCharges_Before_Open_TabPage(R_BeforeOpenTabPageEventArgs eventArgs)
        {
            var loParam = _Genereal_gridRef.CurrentSelectedData;
            loParam.CPROPERTY_ID = _Genereal_viewModel.PropertyValueContext;
            eventArgs.TargetPageType = typeof(PMM01530);
            eventArgs.Parameter = loParam;
        }

        private void _BankAccount_Before_Open_TabPage(R_BeforeOpenTabPageEventArgs eventArgs)
        {
            var loParam = (PMM01500DTO)_Genereal_conductorRef.R_GetCurrentData();
            loParam.CPROPERTY_ID = _Genereal_viewModel.PropertyValueContext;
            eventArgs.TargetPageType = typeof(PMM01510);
            eventArgs.Parameter = loParam;
        }

        #endregion

        #region General

        public bool _enableComboCategory = true;
        public bool _pageSupplierOnCRUDmode = false;

        private void General_OnActiveTabIndexChanging(R_TabStripActiveTabIndexChangingEventArgs eventArgs)
        {
            eventArgs.Cancel = _pageSupplierOnCRUDmode;
        }

        private void Tempalte_OnActiveTabIndexChanging(R_TabStripActiveTabIndexChangingEventArgs eventArgs)
        {
            eventArgs.Cancel = _pageSupplierOnCRUDmode;
        }

        private async Task GroupGrid_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _Genereal_viewModel.GetInvoiceGroupList();

                eventArgs.ListEntityResult = _Genereal_viewModel.InvoinceGroupGrid;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task R_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<PMM01500DTO>(eventArgs.Data);
                await _Genereal_viewModel.GetInvoiceGroup(loParam);


                eventArgs.Result = _Genereal_viewModel.InvoiceGroup;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task R_ServiceSave(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _Genereal_viewModel.SaveInvoiceGroup((PMM01500DTO)eventArgs.Data,
                    (eCRUDMode)eventArgs.ConductorMode);

                eventArgs.Result = _Genereal_viewModel.InvoiceGroup;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task R_ServiceDelete(R_ServiceDeleteEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loData = (PMM01500DTO)eventArgs.Data;
                await _Genereal_viewModel.DeleteInvoiceGroup(loData);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task R_Before_Open_Popup_ActivateInactive(R_BeforeOpenPopupEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            try
            {
                var loGetData = (PMM01500DTO)_Genereal_viewModel.R_GetCurrentData();

                var loValidateViewModel = new GFF00900Model.ViewModel.GFF00900ViewModel();
                loValidateViewModel.ACTIVATE_INACTIVE_ACTIVITY_CODE =
                    "PMM01501"; //Uabh Approval Code sesuai Spec masing masing
                await loValidateViewModel
                    .RSP_ACTIVITY_VALIDITYMethodAsync(); //Jika IAPPROVAL_CODE == 3, maka akan keluar RSP_ERROR disini

                //Jika Approval User ALL dan Approval Code 1, maka akan langsung menjalankan ActiveInactive
                if (loValidateViewModel.loRspActivityValidityList.FirstOrDefault().CAPPROVAL_USER == "ALL" &&
                    loValidateViewModel.loRspActivityValidityResult.Data.FirstOrDefault().IAPPROVAL_MODE == 1)
                {
                    await _Genereal_viewModel.ActiveInactiveProcessAsync(loGetData);
                    await _Genereal_conductorRef.R_SetCurrentData(loGetData);
                    return;
                }
                else //Disini Approval Code yang didapat adalah 2, yang berarti Active Inactive akan dijalankan jika User yang diinput ada di RSP_ACTIVITY_VALIDITY
                {
                    eventArgs.Parameter = new GFF00900ParameterDTO()
                    {
                        Data = loValidateViewModel.loRspActivityValidityList,
                        IAPPROVAL_CODE = "PMM01501" //Uabh Approval Code sesuai Spec masing masing
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

        private async Task R_After_Open_Popup_ActivateInactive(R_AfterOpenPopupEventArgs eventArgs)
        {
            var loGetData = (PMM01500DTO)_Genereal_viewModel.R_GetCurrentData();

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
                    var loActiveData = await _Genereal_viewModel.ActiveInactiveProcessAsync(loGetData);
                    await _Genereal_conductorRef.R_SetCurrentData(loActiveData);
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();
        }

        private bool TabPinaltyChargesEnable;
        private R_TextBox InvGrpName_TextBox;

        private async Task R_Display(R_DisplayEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = (PMM01500DTO)eventArgs.Data;

                _pageSupplierOnCRUDmode = eventArgs.ConductorMode != R_eConductorMode.Normal;

                if (eventArgs.ConductorMode == R_eConductorMode.Normal)
                {
                    if (loParam.LACTIVE != true)
                    {
                        _Genereal_lcLabel = _localizer["_Active"];
                        _Genereal_viewModel.StatusChange = true;
                    }
                    else
                    {
                        _Genereal_lcLabel = _localizer["_Inactive"];
                        _Genereal_viewModel.StatusChange = false;
                    }
                }

                if (eventArgs.ConductorMode == R_eConductorMode.Edit)
                {
                    await InvGrpName_TextBox.FocusAsync();
                    ByDeptEnalble = !loParam.LBY_DEPARTMENT;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private R_TextBox InvGrpId_TextBox;

        private async Task R_AfterAdd(R_AfterAddEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loData = (PMM01500DTO)eventArgs.Data;
                loData.DUPDATE_DATE = DateTime.Now;
                loData.DCREATE_DATE = DateTime.Now;

                await InvGrpId_TextBox.FocusAsync();
                ByDeptEnalble = true;

                if (_Genereal_viewModel.InvoiceTemplateList.Count > 0)
                {
                    loData.CINVOICE_TEMPLATE = _Genereal_viewModel.InvoiceTemplateList.FirstOrDefault().CTEMPLATE_NAME;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void R_CheckAdd(R_CheckAddEventArgs eventArgs)
        {
            if (string.IsNullOrEmpty(_Genereal_viewModel.PropertyValueContext))
            {
                eventArgs.Allow = false;
            }
        }

        private bool GeneralButtonEnable = false;

        private async Task R_Validation(R_ValidationEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            R_PopupResult loResult = null;

            try
            {
                var loData = (PMM01500DTO)eventArgs.Data;
                await _Genereal_viewModel.ValidationInvoiceGrp(loData);

                if (eventArgs.ConductorMode == R_eConductorMode.Add)
                {
                    if (loData.LACTIVE)
                    {
                        var loGetData = loData;

                        var loValidateViewModel = new GFF00900Model.ViewModel.GFF00900ViewModel();
                        loValidateViewModel.ACTIVATE_INACTIVE_ACTIVITY_CODE =
                            "PMM01501"; //Uabh Approval Code sesuai Spec masing masing
                        await loValidateViewModel
                            .RSP_ACTIVITY_VALIDITYMethodAsync(); //Jika IAPPROVAL_CODE == 3, maka akan keluar RSP_ERROR disini

                        //Jika Approval User ALL dan Approval Code 1, maka akan langsung menjalankan ActiveInactive
                        if (loValidateViewModel.loRspActivityValidityList.FirstOrDefault().CAPPROVAL_USER == "ALL" &&
                            loValidateViewModel.loRspActivityValidityResult.Data.FirstOrDefault().IAPPROVAL_MODE == 1)
                        {
                        }
                        else //Disini Approval Code yang didapat adalah 2, yang berarti Active Inactive akan dijalankan jika User yang diinput ada di RSP_ACTIVITY_VALIDITY
                        {
                            var loPopupParam = new GFF00900ParameterDTO()
                            {
                                Data = loValidateViewModel.loRspActivityValidityList,
                                IAPPROVAL_CODE = "PMM01501" //Ubah Approval Code sesuai Spec masing masing
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
                                return;
                            }
                        }
                    }
                }
                else if (eventArgs.ConductorMode == R_eConductorMode.Edit)
                {
                    var loChekcData = await _Genereal_viewModel.CheckDataTab2(loData);
                    if ((_Genereal_viewModel.OldByDeptValue && !loData.LBY_DEPARTMENT) && loChekcData)
                    {
                        var loValidate = await R_MessageBox.Show("",
                            R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "_NotifByDeptAlert"),
                            R_eMessageBoxButtonType.YesNo);
                        loData.DeleteAllTabDept = loValidate == R_eMessageBoxResult.Yes;
                        eventArgs.Cancel = loValidate == R_eMessageBoxResult.No;
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private bool R_EnableTabHasData;
        private bool TabEnalbleDept;

        private bool InvGrpModeEnable = false;

        private void InvDue_OnChanged(string poParam)
        {
            _Genereal_viewModel.Data.CINVOICE_DUE_MODE = poParam;

            InvGrpModeEnable = poParam.ToString() == "02";
            if (poParam != "02")
            {
                _Genereal_viewModel.Data.CINVOICE_GROUP_MODE = "";
                IDueDaysEnable = false;
                _Genereal_viewModel.Data.IDUE_DAYS = 0;
                _Genereal_viewModel.Data.IFIXED_DUE_DATE = 0;
                _Genereal_viewModel.Data.ILIMIT_INVOICE_DATE = 0;
                _Genereal_viewModel.Data.IBEFORE_LIMIT_INVOICE_DATE = 0;
                _Genereal_viewModel.Data.IAFTER_LIMIT_INVOICE_DATE = 0;
            }
            else
            {
                _Genereal_viewModel.Data.CINVOICE_GROUP_MODE = "01";
                IDueDaysEnable = true;
            }

            ILimitDueDateEnable = false;
            IBeforeLimitEnable = false;
            IAfterLimitEnable = false;
            IFixDueDateEnable = false;
        }

        private void UseStamp_OnChange(bool poParam)
        {
            _Genereal_viewModel.Data.LUSE_STAMP = poParam;
            if (!poParam)
            {
                _Genereal_viewModel.Data.CSTAMP_CODE = "";
                _Genereal_viewModel.Data.CSTAMP_ADD_ID = "";
                _Genereal_viewModel.Data.CSTAMP_ADD_NAME = "";
            }
        }

        private void TaxExemption_OnChange(bool poParam)
        {
            _Genereal_viewModel.Data.LTAX_EXEMPTION = poParam;
            if (!poParam)
            {
                _Genereal_viewModel.Data.CTAX_EXEMPTION_CODE = "";
                _Genereal_viewModel.Data.CTAX_ADD_DESCR = "";
            }
        }

        private async Task TaxCode_OnChanged(string poParam)
        {
            var loEx = new R_Exception();

            try
            {
                _Genereal_viewModel.Data.CTAX_EXEMPTION_CODE = poParam;
                string lcParameter = poParam == "07" ? "_BS_PPN_ADD_DESCR_07" : "_BS_PPN_ADD_DESCR_08";

                await _Genereal_viewModel.GetAdditionalDescList(lcParameter);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private bool ByDeptEnalble = false;

        private void ByDept_OnChange(bool poParam)
        {
            _Genereal_viewModel.Data.LBY_DEPARTMENT = poParam;

            ByDeptEnalble = !poParam;

            if (poParam)
            {
                _Genereal_viewModel.Data.Data = null;
                _Genereal_viewModel.Data.FileName = null;
                _Genereal_viewModel.Data.FileExtension = null;

                _Genereal_viewModel.Data.CDEPT_NAME = "";
                _Genereal_viewModel.Data.CDEPT_CODE = "";
                _Genereal_viewModel.Data.CBANK_ACCOUNT = "";
                _Genereal_viewModel.Data.CBANK_CODE = "";
                _Genereal_viewModel.Data.CCB_NAME = "";
                _Genereal_viewModel.Data.FileNameExtension = "";
                _Genereal_viewModel.Data.CINVOICE_TEMPLATE = "";
            }

            if (!poParam && _Genereal_viewModel.InvoiceTemplateList.Count > 0)
            {
                _Genereal_viewModel.Data.CINVOICE_TEMPLATE =
                    _Genereal_viewModel.InvoiceTemplateList.FirstOrDefault().CTEMPLATE_NAME;
            }
        }

        private bool IDueDaysEnable = false;
        private bool IFixDueDateEnable = false;
        private bool ILimitDueDateEnable = false;
        private bool IBeforeLimitEnable = false;
        private bool IAfterLimitEnable = false;

        private void InvGrp_OnChanged(string poParam)
        {
            _Genereal_viewModel.Data.CINVOICE_GROUP_MODE = poParam;

            IDueDaysEnable = poParam.ToString() == "01";
            IFixDueDateEnable = poParam.ToString() == "02";
            ILimitDueDateEnable = poParam.ToString() == "03";
            IBeforeLimitEnable = poParam.ToString() == "03";
            IAfterLimitEnable = poParam.ToString() == "03";

            switch (poParam)
            {
                case "01":
                    _Genereal_viewModel.Data.IFIXED_DUE_DATE = 0;
                    _Genereal_viewModel.Data.ILIMIT_INVOICE_DATE = 0;
                    _Genereal_viewModel.Data.IBEFORE_LIMIT_INVOICE_DATE = 0;
                    _Genereal_viewModel.Data.IAFTER_LIMIT_INVOICE_DATE = 0;
                    break;
                case "02":
                    _Genereal_viewModel.Data.IDUE_DAYS = 0;
                    _Genereal_viewModel.Data.ILIMIT_INVOICE_DATE = 0;
                    _Genereal_viewModel.Data.IBEFORE_LIMIT_INVOICE_DATE = 0;
                    _Genereal_viewModel.Data.IAFTER_LIMIT_INVOICE_DATE = 0;
                    break;
                case "03":
                    _Genereal_viewModel.Data.IDUE_DAYS = 0;
                    _Genereal_viewModel.Data.IFIXED_DUE_DATE = 0;
                    break;
            }
        }

        private void General_AfterSave(R_AfterSaveEventArgs eventArgs)
        {
            InvGrpModeEnable = false;
            IDueDaysEnable = false;
            IFixDueDateEnable = false;
            ILimitDueDateEnable = false;
            IBeforeLimitEnable = false;
            IAfterLimitEnable = false;
            ByDeptEnalble = false;
            GeneralButtonEnable = false;
        }

        private void General_BeforeCancel(R_BeforeCancelEventArgs eventArgs)
        {
            InvGrpModeEnable = false;
            IDueDaysEnable = false;
            IFixDueDateEnable = false;
            ILimitDueDateEnable = false;
            IBeforeLimitEnable = false;
            IAfterLimitEnable = false;
            ByDeptEnalble = false;
            GeneralButtonEnable = false;
        }

        private void R_ConvertToGridEntity(R_ConvertToGridEntityEventArgs eventArgs)
        {
            var loData = R_FrontUtility.ConvertObjectToObject<PMM01501DTO>(eventArgs.Data);
            loData.CINVGRP_CODE_NAME = string.Format("{0} - {1}", loData.CINVGRP_CODE, loData.CINVGRP_NAME);

            eventArgs.GridData = loData;
        }

        private R_eFileSelectAccept[] accepts = { R_eFileSelectAccept.Doc };

        private async Task _General_InvTemplateUpload_OnChange(InputFileChangeEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loData = (PMM01500DTO)_Genereal_conductorRef.R_GetCurrentData();

                // Set Data
                loData.FileNameExtension = eventArgs.File.Name;
                var loMS = new MemoryStream();
                await eventArgs.File.OpenReadStream().CopyToAsync(loMS);
                loData.Data = loMS.ToArray();
                loData.FileExtension = Path.GetExtension(eventArgs.File.Name);
                loData.FileName = Path.GetFileNameWithoutExtension(eventArgs.File.Name);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        #endregion

        #region Lookup

        private async Task StampId_OnLostFocus()
        {
            var loEx = new R_Exception();

            try
            {
                if (string.IsNullOrWhiteSpace(_Genereal_viewModel.Data.CSTAMP_ADD_ID) == false)
                {
                    var param = new GSL01400ParameterDTO
                    {
                        CPROPERTY_ID = _Genereal_viewModel.PropertyValueContext,
                        CCHARGES_TYPE_ID = "A",
                        CSEARCH_TEXT = _Genereal_viewModel.Data.CSTAMP_ADD_ID
                    };

                    LookupGSL01400ViewModel loLookupViewModel = new LookupGSL01400ViewModel();

                    var loResult = await loLookupViewModel.GetOtherCharges(param);

                    if (loResult == null)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                            typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                            "_ErrLookup01"));
                        _Genereal_viewModel.Data.CSTAMP_ADD_NAME = "";
                        //await GLAccount_TextBox.FocusAsync();
                        goto EndBlock;
                    }

                    _Genereal_viewModel.Data.CSTAMP_ADD_ID = loResult.CCHARGES_ID;
                    _Genereal_viewModel.Data.CSTAMP_ADD_NAME = loResult.CCHARGES_NAME;
                }
                else
                {
                    _Genereal_viewModel.Data.CSTAMP_ADD_NAME = "";
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            EndBlock:
            R_DisplayException(loEx);
        }

        private void OtherCharges_Before_Open_Lookup(R_BeforeOpenLookupEventArgs eventArgs)
        {
            var param = new GSL01400ParameterDTO()
            {
                CPROPERTY_ID = _Genereal_viewModel.PropertyValueContext,
                CCHARGES_TYPE_ID = "A",
            };
            eventArgs.Parameter = param;
            eventArgs.TargetPageType = typeof(GSL01400);
        }

        private void OtherCharges_After_Open_Lookup(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loTempResult = (GSL01400DTO)eventArgs.Result;
            if (loTempResult == null)
            {
                return;
            }

            _Genereal_viewModel.Data.CSTAMP_ADD_ID = loTempResult.CCHARGES_ID;
            _Genereal_viewModel.Data.CSTAMP_ADD_NAME = loTempResult.CCHARGES_NAME;
        }

        private async Task GeneralInvoiceDeptCode_OnLostFocus(object poParam)
        {
            var loEx = new R_Exception();

            try
            {
                if (string.IsNullOrWhiteSpace(_Genereal_viewModel.Data.CINV_DEPT_CODE) == false)
                {
                    var param = new GSL00700ParameterDTO
                    {
                        CSEARCH_TEXT = _Genereal_viewModel.Data.CINV_DEPT_CODE
                    };

                    LookupGSL00700ViewModel loLookupViewModel = new LookupGSL00700ViewModel();

                    var loResult = await loLookupViewModel.GetDepartment(param);

                    if (loResult == null)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                            typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                            "_ErrLookup01"));
                        _Genereal_viewModel.Data.CINV_DEPT_NAME = "";
                        GeneralButtonEnable = false;
                        goto EndBlock;
                    }

                    _Genereal_viewModel.Data.CINV_DEPT_CODE = loResult.CDEPT_CODE;
                    _Genereal_viewModel.Data.CINV_DEPT_NAME = loResult.CDEPT_NAME;
                }
                else
                {
                    _Genereal_viewModel.Data.CINV_DEPT_NAME = "";
                    GeneralButtonEnable = false;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            EndBlock:
            R_DisplayException(loEx);
        }

        private void Invoice_Department_Before_Open_Lookup(R_BeforeOpenLookupEventArgs eventArgs)
        {
            var param = new GSL00700ParameterDTO();
            eventArgs.Parameter = param;
            eventArgs.TargetPageType = typeof(GSL00700);
        }

        private void General_Invoice_Department_After_Open_Lookup(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loTempResult = (GSL00700DTO)eventArgs.Result;
            if (loTempResult == null)
            {
                return;
            }

            _Genereal_viewModel.Data.CINV_DEPT_CODE = loTempResult.CDEPT_CODE;
            _Genereal_viewModel.Data.CINV_DEPT_NAME = loTempResult.CDEPT_NAME;
        }

        private async Task GeneralDeptCode_OnLostFocus(object poParam)
        {
            var loEx = new R_Exception();

            try
            {
                if (string.IsNullOrWhiteSpace(_Genereal_viewModel.Data.CDEPT_CODE) == false)
                {
                    var param = new GSL00700ParameterDTO
                    {
                        CSEARCH_TEXT = _Genereal_viewModel.Data.CDEPT_CODE
                    };

                    LookupGSL00700ViewModel loLookupViewModel = new LookupGSL00700ViewModel();

                    var loResult = await loLookupViewModel.GetDepartment(param);

                    if (loResult == null)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                            typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                            "_ErrLookup01"));
                        _Genereal_viewModel.Data.CDEPT_NAME = "";
                        //await GLAccount_TextBox.FocusAsync();
                        GeneralButtonEnable = false;
                        goto EndBlock;
                    }

                    _Genereal_viewModel.Data.CDEPT_CODE = loResult.CDEPT_CODE;
                    _Genereal_viewModel.Data.CDEPT_NAME = loResult.CDEPT_NAME;
                    GeneralButtonEnable = !string.IsNullOrEmpty(_Genereal_viewModel.Data.CDEPT_CODE) &&
                                          !string.IsNullOrEmpty(_Genereal_viewModel.Data.CBANK_CODE);
                }
                else
                {
                    _Genereal_viewModel.Data.CDEPT_NAME = "";
                    GeneralButtonEnable = false;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            EndBlock:
            R_DisplayException(loEx);
        }

        private void Department_Before_Open_Lookup(R_BeforeOpenLookupEventArgs eventArgs)
        {
            var param = new GSL00700ParameterDTO();
            eventArgs.Parameter = param;
            eventArgs.TargetPageType = typeof(GSL00700);
        }

        private void General_Department_After_Open_Lookup(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loTempResult = (GSL00700DTO)eventArgs.Result;
            if (loTempResult == null)
            {
                return;
            }

            _Genereal_viewModel.Data.CDEPT_CODE = loTempResult.CDEPT_CODE;
            _Genereal_viewModel.Data.CDEPT_NAME = loTempResult.CDEPT_NAME;
            GeneralButtonEnable = !string.IsNullOrEmpty(_Genereal_viewModel.Data.CDEPT_CODE) &&
                                  !string.IsNullOrEmpty(_Genereal_viewModel.Data.CBANK_CODE);
        }

        private async Task GeneralBankCode_OnLostFocus(object poParam)
        {
            var loEx = new R_Exception();

            try
            {
                if (string.IsNullOrWhiteSpace(_Genereal_viewModel.Data.CBANK_CODE) == false)
                {
                    var param = new GSL01200ParameterDTO
                    {
                        CCB_TYPE = "B",
                        CSEARCH_TEXT = _Genereal_viewModel.Data.CBANK_CODE
                    };

                    LookupGSL01200ViewModel loLookupViewModel = new LookupGSL01200ViewModel();

                    var loResult = await loLookupViewModel.GetBank(param);

                    if (loResult == null)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                            typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                            "_ErrLookup01"));
                        _Genereal_viewModel.Data.CCB_NAME = "";
                        GeneralButtonEnable = false;
                        //await GLAccount_TextBox.FocusAsync();
                        goto EndBlock;
                    }

                    _Genereal_viewModel.Data.CBANK_CODE = loResult.CCB_CODE;
                    _Genereal_viewModel.Data.CCB_NAME = loResult.CCB_NAME;
                    GeneralButtonEnable = !string.IsNullOrEmpty(_Genereal_viewModel.Data.CDEPT_CODE) &&
                                          !string.IsNullOrEmpty(_Genereal_viewModel.Data.CBANK_CODE);
                }
                else
                {
                    _Genereal_viewModel.Data.CCB_NAME = "";
                    GeneralButtonEnable = false;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            EndBlock:
            R_DisplayException(loEx);
        }

        private void Bank_Before_Open_Lookup(R_BeforeOpenLookupEventArgs eventArgs)
        {
            var loParam = new GSL01200ParameterDTO
            {
                CCB_TYPE = "B"
            };

            eventArgs.Parameter = loParam;
            eventArgs.TargetPageType = typeof(GSL01200);
        }

        private void General_Bank_After_Open_Lookup(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loTempResult = (GSL01200DTO)eventArgs.Result;
            if (loTempResult == null)
            {
                return;
            }

            _Genereal_viewModel.Data.CBANK_CODE = loTempResult.CCB_CODE;
            _Genereal_viewModel.Data.CCB_NAME = loTempResult.CCB_NAME;
            GeneralButtonEnable = !string.IsNullOrEmpty(_Genereal_viewModel.Data.CDEPT_CODE) &&
                                  !string.IsNullOrEmpty(_Genereal_viewModel.Data.CBANK_CODE);
        }

        private async Task GeneralBankAcount_OnLostFocus(object poParam)
        {
            var loEx = new R_Exception();

            try
            {
                var loGetData = (PMM01500DTO)_Genereal_conductorRef.R_GetCurrentData();
                if (string.IsNullOrWhiteSpace(loGetData.CBANK_ACCOUNT) == false)
                {
                    var param = new GSL01300ParameterDTO
                    {
                        CBANK_TYPE = "B",
                        CCB_CODE = loGetData.CBANK_CODE,
                        CDEPT_CODE = loGetData.CDEPT_CODE,
                        CSEARCH_TEXT = loGetData.CBANK_ACCOUNT
                    };

                    LookupGSL01300ViewModel loLookupViewModel = new LookupGSL01300ViewModel();

                    var loResult = await loLookupViewModel.GetBankAccount(param);

                    if (loResult == null)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                            typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                            "_ErrLookup01"));
                        goto EndBlock;
                    }

                    loGetData.CBANK_ACCOUNT = loResult.CCB_ACCOUNT_NO;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            EndBlock:
            R_DisplayException(loEx);
        }

        private void BankAccount_Before_Open_Lookup(R_BeforeOpenLookupEventArgs eventArgs)
        {
            var loGetData = (PMM01500DTO)_Genereal_conductorRef.R_GetCurrentData();

            var param = new GSL01300ParameterDTO()
            {
                CBANK_TYPE = "B",
                CCB_CODE = loGetData.CBANK_CODE,
                CDEPT_CODE = loGetData.CDEPT_CODE,
            };
            eventArgs.Parameter = param;
            eventArgs.TargetPageType = typeof(GSL01300);
        }

        private void BankAccount_After_Open_Lookup(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loTempResult = (GSL01300DTO)eventArgs.Result;
            if (loTempResult == null)
            {
                return;
            }

            var loGetData = (PMM01500DTO)_Genereal_conductorRef.R_GetCurrentData();
            loGetData.CBANK_ACCOUNT = loTempResult.CCB_ACCOUNT_NO;
        }

        private async Task Department_Desc_OnLostFocus(object poParam)
        {
            var loEx = new R_Exception();

            try
            {
                if (string.IsNullOrWhiteSpace(_Genereal_viewModel.Data.CTAX_ADD_DESCR) == false)
                {
                    var loGetData = (PMM01500DTO)_Genereal_conductorRef.R_GetCurrentData();
                    
                    var param = new GSL03300ParameterDTO
                    {
                        CREF_CODE = "",
                        CREF_TYPE = loGetData.CTAX_EXEMPTION_CODE == "07" ? "04" :
                            loGetData.CTAX_EXEMPTION_CODE == "08" ? "05" : "",
                        CSEARCH_TEXT = _Genereal_viewModel.Data.CTAX_ADD_DESCR
                    };

                    LookupGSL03300ViewModel loLookupViewModel = new LookupGSL03300ViewModel();

                    var loResult = await loLookupViewModel.GetTaxCharges(param);

                    if (loResult == null)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                            typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                            "_ErrLookup01"));
                        _Genereal_viewModel.Data.CTAX_ADD_DESCR = "";
                        goto EndBlock;
                    }

                    _Genereal_viewModel.Data.CTAX_ADD_DESCR = loResult.CREF_DESCRIPTION;
                }
                else
                {
                    _Genereal_viewModel.Data.CTAX_ADD_DESCR = "";
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            EndBlock:
            R_DisplayException(loEx);
        }

        private void Department_Desc_Before_Open_Lookup(R_BeforeOpenLookupEventArgs eventArgs)
        {
            var loGetData = (PMM01500DTO)_Genereal_conductorRef.R_GetCurrentData();
         
            var param = new GSL03300ParameterDTO()
            {
                CREF_CODE = "",
                CREF_TYPE = loGetData.CTAX_EXEMPTION_CODE == "07" ? "04" :
                    loGetData.CTAX_EXEMPTION_CODE == "08" ? "05" : "",
            };
            eventArgs.Parameter = param;
            eventArgs.TargetPageType = typeof(GSL03300);
        }

        private void Department_Desc_After_Open_Lookup(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loTempResult = (GSL03300DTO)eventArgs.Result;
            if (loTempResult == null)
            {
                return;
            }

            var loGetData = (PMM01500DTO)_Genereal_conductorRef.R_GetCurrentData();
            loGetData.CTAX_ADD_DESCR = loTempResult.CREF_DESCRIPTION;
        }
        #endregion
        
        
    }
}