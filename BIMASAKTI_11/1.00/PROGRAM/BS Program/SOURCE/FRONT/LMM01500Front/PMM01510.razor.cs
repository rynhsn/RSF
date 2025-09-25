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

namespace PMM01500FRONT
{
    public partial class PMM01510 : R_Page, R_ITabPage
    {
        private PMM01510ViewModel _BankAccountGrid_viewModel = new PMM01510ViewModel();

        private R_Grid<PMM01511DTO> _BankAccount_gridRef;

        private R_Conductor _BankAccountGrid_conductorRef;

        [Inject] IClientHelper clientHelper { get; set; }

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<PMM01510DTO>(poParameter);
                _BankAccountGrid_viewModel.InvGrpCode = loParam.CINVGRP_CODE;
                _BankAccountGrid_viewModel.InvGrpName = loParam.CINVGRP_NAME;
                _BankAccountGrid_viewModel.PropertyValueContext = loParam.CPROPERTY_ID;
                enableAdd = true;

                await _BankAccountGrid_viewModel.GetInvoiceTemplate(loParam.CPROPERTY_ID);
                await _BankAccount_gridRef.R_RefreshGrid(null);
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
                var loData = (PMM01511DTO)eventArgs.Data;

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
                        Program_Id = "PMM01510",
                        Table_Name = "PMM_INVGRP_BANK_ACC_DEPT",
                        Key_Value = string.Join("|", clientHelper.CompanyId, loData.CPROPERTY_ID, loData.CINVGRP_CODE,
                            loData.CBUILDING_ID, loData.CDEPT_CODE)
                    };

                    loLockResult = await loCls.R_Lock(loLockPar);
                }
                else
                {
                    var loUnlockPar = new R_ServiceLockingUnLockParameterDTO
                    {
                        Company_Id = clientHelper.CompanyId,
                        User_Id = clientHelper.UserId,
                        Program_Id = "PMM01510",
                        Table_Name = "PMM_INVGRP_BANK_ACC_DEPT",
                        Key_Value = string.Join("|", clientHelper.CompanyId, loData.CPROPERTY_ID, loData.CINVGRP_CODE,
                            loData.CBUILDING_ID, loData.CDEPT_CODE)
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

        #region Bank Account

        private async Task BankAccount_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _BankAccountGrid_viewModel.GetListTemplateBankAccount();

                eventArgs.ListEntityResult = _BankAccountGrid_viewModel.TemplateBankAccountGrid;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void BankAccount_ConvertToGridEntity(R_ConvertToGridEntityEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                eventArgs.GridData = R_FrontUtility.ConvertObjectToObject<PMM01511DTO>(eventArgs.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task BankAccount_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<PMM01511DTO>(eventArgs.Data);
                await _BankAccountGrid_viewModel.GetTemplateBankAccount(loParam);

                eventArgs.Result = _BankAccountGrid_viewModel.TemplateBankAccount;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private R_TextBox DeptLookup_TextBox;

        private async Task BankAccount_AfterAdd(R_AfterAddEventArgs eventArgs)
        {
            var loData = (PMM01510DTO)eventArgs.Data;
            await DeptLookup_TextBox.FocusAsync();
            if (_BankAccountGrid_viewModel.InvoiceTemplateList.Count > 0)
            {
                loData.CINVOICE_TEMPLATE =
                    _BankAccountGrid_viewModel.InvoiceTemplateList.FirstOrDefault().CTEMPLATE_NAME;
            }
        }

        private async Task BankAccount_Display(R_DisplayEventArgs eventArgs)
        {
            if (eventArgs.ConductorMode == R_eConductorMode.Edit)
            {
                await DeptLookup_TextBox.FocusAsync();
            }
        }

        private async Task BankAccount_Validation(R_ValidationEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _BankAccountGrid_viewModel.ValidationTemplateBankAccount((PMM01511DTO)eventArgs.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task BankAccount_ServiceSave(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                PMM01511DTO loData = (PMM01511DTO)eventArgs.Data;

                if (eventArgs.ConductorMode == R_eConductorMode.Add)
                {
                    loData.CPROPERTY_ID = _BankAccountGrid_viewModel.PropertyValueContext;
                    loData.CINVGRP_CODE = _BankAccountGrid_viewModel.InvGrpCode;
                }

                await _BankAccountGrid_viewModel.SaveTemplateBankAccount(loData, (eCRUDMode)eventArgs.ConductorMode);

                eventArgs.Result = _BankAccountGrid_viewModel.TemplateBankAccount;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task BankAccount_ServiceDelete(R_ServiceDeleteEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                PMM01511DTO loData = (PMM01511DTO)eventArgs.Data;
                await _BankAccountGrid_viewModel.DeleteTemplateBankAccount(loData);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private R_eFileSelectAccept[] accepts = { R_eFileSelectAccept.Doc };

        private async Task _BankAccount_InvTemplateUpload_OnChange(InputFileChangeEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loData = (PMM01511DTO)_BankAccountGrid_conductorRef.R_GetCurrentData();

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

        private bool _pageSupplierOnCRUDmode = true;

        private async Task BankAccount_SetOther(R_SetEventArgs eventArgs)
        {
            _pageSupplierOnCRUDmode = eventArgs.Enable;
            await InvokeTabEventCallbackAsync(eventArgs.Enable);
        }

        #endregion

        #region Building Lookup

        private async Task BankAccountBuilding_OnLostFocus()
        {
            var loEx = new R_Exception();

            try
            {
                if (_BankAccountGrid_viewModel.Data.CBUILDING_ID != null)
                {
                    if (string.IsNullOrWhiteSpace(_BankAccountGrid_viewModel.Data.CBUILDING_ID) == false)
                    {
                        var param = new GSL02200ParameterDTO
                        {
                            CPROPERTY_ID = _BankAccountGrid_viewModel.PropertyValueContext,
                            CSEARCH_TEXT = _BankAccountGrid_viewModel.Data.CBUILDING_ID
                        };

                        LookupGSL02200ViewModel loLookupViewModel = new LookupGSL02200ViewModel();

                        var loResult = await loLookupViewModel.GetBuilding(param);

                        if (loResult == null)
                        {
                            loEx.Add(R_FrontUtility.R_GetError(
                                typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                                "_ErrLookup01"));
                            _BankAccountGrid_viewModel.Data.CBUILDING_NAME = "";
                            goto EndBlock;
                        }

                        _BankAccountGrid_viewModel.Data.CBUILDING_ID = loResult.CBUILDING_ID;
                        _BankAccountGrid_viewModel.Data.CBUILDING_NAME = loResult.CBUILDING_NAME;
                    }
                    else
                    {
                        _BankAccountGrid_viewModel.Data.CBUILDING_NAME = "";
                    }
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            EndBlock:
            R_DisplayException(loEx);
        }

        private void Building_Before_Open_Lookup(R_BeforeOpenLookupEventArgs eventArgs)
        {
            var param = new GSL02200ParameterDTO() { CPROPERTY_ID = _BankAccountGrid_viewModel.PropertyValueContext };
            eventArgs.Parameter = param;
            eventArgs.TargetPageType = typeof(GSL02200);
        }

        private void BankAccount_Building_After_Open_Lookup(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loTempResult = (GSL02200DTO)eventArgs.Result;
            if (loTempResult == null)
            {
                return;
            }

            _BankAccountGrid_viewModel.Data.CBUILDING_ID = loTempResult.CBUILDING_ID;
            _BankAccountGrid_viewModel.Data.CBUILDING_NAME = loTempResult.CBUILDING_NAME;

            var loGetData = (PMM01511DTO)_BankAccountGrid_conductorRef.R_GetCurrentData();
        }

        #endregion

        #region Dept Lookup

        private async Task BankAccountDeptCode_OnLostFocus()
        {
            var loEx = new R_Exception();

            try
            {
                if (_BankAccountGrid_viewModel.Data.CDEPT_CODE != null)
                {
                    if (string.IsNullOrWhiteSpace(_BankAccountGrid_viewModel.Data.CDEPT_CODE) == false)
                    {
                        var param = new GSL00700ParameterDTO
                        {
                            CSEARCH_TEXT = _BankAccountGrid_viewModel.Data.CDEPT_CODE
                        };

                        LookupGSL00700ViewModel loLookupViewModel = new LookupGSL00700ViewModel();

                        var loResult = await loLookupViewModel.GetDepartment(param);

                        if (loResult == null)
                        {
                            loEx.Add(R_FrontUtility.R_GetError(
                                typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                                "_ErrLookup01"));
                            _BankAccountGrid_viewModel.Data.CDEPT_NAME = "";
                            //await GLAccount_TextBox.FocusAsync();
                            goto EndBlock;
                        }

                        _BankAccountGrid_viewModel.Data.CDEPT_CODE = loResult.CDEPT_CODE;
                        _BankAccountGrid_viewModel.Data.CDEPT_NAME = loResult.CDEPT_NAME;
                    }
                    else
                    {
                        _BankAccountGrid_viewModel.Data.CDEPT_NAME = "";
                    }
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

        private void BankAccount_Department_After_Open_Lookup(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loTempResult = (GSL00700DTO)eventArgs.Result;
            if (loTempResult == null)
            {
                return;
            }

            _BankAccountGrid_viewModel.Data.CDEPT_CODE = loTempResult.CDEPT_CODE;
            _BankAccountGrid_viewModel.Data.CDEPT_NAME = loTempResult.CDEPT_NAME;

            var loGetData = (PMM01511DTO)_BankAccountGrid_conductorRef.R_GetCurrentData();
        }

        #endregion

        #region Bank

        private async Task BankAccountBankCode_OnLostFocus()
        {
            var loEx = new R_Exception();

            try
            {
                if (string.IsNullOrWhiteSpace(_BankAccountGrid_viewModel.Data.CBANK_CODE) == false)
                {
                    var param = new GSL01200ParameterDTO
                    {
                        CCB_TYPE = "B",
                        CSEARCH_TEXT = _BankAccountGrid_viewModel.Data.CBANK_CODE
                    };

                    LookupGSL01200ViewModel loLookupViewModel = new LookupGSL01200ViewModel();

                    var loResult = await loLookupViewModel.GetBank(param);

                    if (loResult == null)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                            typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                            "_ErrLookup01"));
                        _BankAccountGrid_viewModel.Data.CBANK_NAME = "";
                        //await GLAccount_TextBox.FocusAsync();
                        goto EndBlock;
                    }

                    _BankAccountGrid_viewModel.Data.CBANK_CODE = loResult.CCB_CODE;
                    _BankAccountGrid_viewModel.Data.CBANK_NAME = loResult.CCB_NAME;
                }
                else
                {
                    _BankAccountGrid_viewModel.Data.CBANK_NAME = "";
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            EndBlock:
            R_DisplayException(loEx);
        }

        private void BankAccount_Bank_Before_Open_Lookup(R_BeforeOpenLookupEventArgs eventArgs)
        {
            var param = new GSL01200ParameterDTO()
            {
                CCB_TYPE = "B"
            };
            eventArgs.Parameter = param;
            eventArgs.TargetPageType = typeof(GSL01200);
        }

        private void BankAccount_Bank_After_Open_Lookup(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loTempResult = (GSL01200DTO)eventArgs.Result;
            if (loTempResult == null)
            {
                return;
            }

            _BankAccountGrid_viewModel.Data.CBANK_CODE = loTempResult.CCB_CODE;
            _BankAccountGrid_viewModel.Data.CBANK_NAME = loTempResult.CCB_NAME;
        }

        #endregion

        #region Bank Account

        private async Task BankAcount_OnLostFocus()
        {
            var loEx = new R_Exception();

            try
            {
                var loGetData = (PMM01511DTO)_BankAccountGrid_conductorRef.R_GetCurrentData();
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

        private async Task BankAccount_BankAccount_Before_Open_Lookup(R_BeforeOpenLookupEventArgs eventArgs)
        {
            var loGetData = (PMM01511DTO)_BankAccountGrid_conductorRef.R_GetCurrentData();

            if (string.IsNullOrWhiteSpace(loGetData.CBANK_CODE) || string.IsNullOrWhiteSpace(loGetData.CBANK_CODE))
            {
                await R_MessageBox.Show("",
                    R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "_NotifFillDeptAndBankCode"),
                    R_eMessageBoxButtonType.OK);
            }
            else
            {
                var param = new GSL01300ParameterDTO()
                {
                    CBANK_TYPE = "B",
                    CCB_CODE = loGetData.CBANK_CODE,
                    CDEPT_CODE = loGetData.CDEPT_CODE,
                };
                eventArgs.Parameter = param;
                eventArgs.TargetPageType = typeof(GSL01300);
            }
        }

        private void BankAccount_BankAccount_After_Open_Lookup(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loTempResult = (GSL01300DTO)eventArgs.Result;
            if (loTempResult == null)
            {
                return;
            }

            _BankAccountGrid_viewModel.Data.CBANK_ACCOUNT = loTempResult.CCB_ACCOUNT_NO;

            var loGetData = (PMM01511DTO)_BankAccountGrid_conductorRef.R_GetCurrentData();
        }

        #endregion

        #region Resfresh Tab Page

        private bool enableAdd = true;

        public async Task RefreshTabPageAsync(object poParam)
        {
            var loEx = new R_Exception();

            try
            {
                var loData = (PMM01500DTO)poParam;
                if (!string.IsNullOrWhiteSpace(loData.CINVGRP_CODE) && loData.LBY_DEPARTMENT)
                {
                    var loParam = R_FrontUtility.ConvertObjectToObject<PMM01510DTO>(poParam);
                    enableAdd = loData.LBY_DEPARTMENT;
                    _BankAccountGrid_viewModel.InvGrpCode = loParam.CINVGRP_CODE;
                    _BankAccountGrid_viewModel.InvGrpName = loParam.CINVGRP_NAME;
                    _BankAccountGrid_viewModel.PropertyValueContext = loParam.CPROPERTY_ID;

                    await _BankAccount_gridRef.R_RefreshGrid(null);
                    await _BankAccountGrid_viewModel.GetInvoiceTemplate(loParam.CPROPERTY_ID);
                }
                else
                {
                    _BankAccountGrid_viewModel.InvGrpCode = "";
                    _BankAccountGrid_viewModel.InvGrpName = "";
                    _BankAccountGrid_viewModel.PropertyValueContext = "";
                    enableAdd = false;
                    _BankAccountGrid_viewModel.R_SetCurrentData(null);
                    _BankAccount_gridRef.DataSource.Clear();
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