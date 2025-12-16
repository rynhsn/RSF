using BlazorClientHelper;
using FAT00100Common.DTOs;
using FAT00100Model.VMs;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Enums;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.Popup;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Controls.Tab;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_BlazorFrontEnd.Interfaces;
using R_CommonFrontBackAPI;
using FAT00100FrontResources;
using R_LockingFront;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FAT00100Front
{
    public partial class FAT00100 : R_Page
    {
        private readonly FAT00100ViewModel _VM = new FAT00100ViewModel();
        private R_Conductor? _conductorRef;
        private R_ConductorGrid? _conductorGridRef;
        private R_Grid<FAT00100GetDataGridResultDTO>? _gridRef;
        private R_TabStrip? _tabStripRef;
        private R_TabStripTab? _tabAssetList;
        private R_TabPage? _tabPageAssetList;

        // Period filter properties
        public int PeriodFromYear { get; set; } = 2023;
        public string PeriodFromMonth { get; set; } = "01";
        public int PeriodToYear { get; set; } = 2025;
        public string PeriodToMonth { get; set; } = "12";

        // Status filter property
        public string SelectedStatus { get; set; } = "ALL";

        // Track previous department code value to avoid unnecessary lookups (equivalent to lctxtDepartmentCode in net4)
        private string _previousDeptCode = string.Empty;

        [Inject] private IClientHelper ClientHelper { get; set; } = default!;
        [Inject] private R_ILocalizer<FAT00100FrontResources.Resources_Dummy_Class> Localizer { get; set; } = default!;
        [Inject] public R_PopupService PopupService { get; set; } = default!;

        // Source radio button list for FA/PJ
        public class SourceOptionDTO
        {
            public string CVALUE { get; set; } = string.Empty;
            public string CDESCRIPTION { get; set; } = string.Empty;
        }

        private List<SourceOptionDTO> _sourceList = new List<SourceOptionDTO>
        {
            new SourceOptionDTO { CVALUE = FAT00100ViewModel.DEFAULT_SOURCE_MODULE_FA, CDESCRIPTION = FAT00100ViewModel.DEFAULT_SOURCE_MODULE_FA },
            new SourceOptionDTO { CVALUE = FAT00100ViewModel.DEFAULT_SOURCE_MODULE_PJ, CDESCRIPTION = FAT00100ViewModel.DEFAULT_SOURCE_MODULE_PJ }
        };

        public List<SourceOptionDTO> SourceList => _sourceList;

        // Status option DTO for combobox
        public class StatusOptionDTO
        {
            public string CVALUE { get; set; } = string.Empty;
            public string CDESCRIPTION { get; set; } = string.Empty;
        }

        private List<StatusOptionDTO> _statusList = new List<StatusOptionDTO>
        {
            new StatusOptionDTO { CVALUE = "ALL", CDESCRIPTION = "All" },
            new StatusOptionDTO { CVALUE = "DRAFT", CDESCRIPTION = "Draft" },
            new StatusOptionDTO { CVALUE = "OPEN", CDESCRIPTION = "Open" },
            new StatusOptionDTO { CVALUE = "INPROGRESS", CDESCRIPTION = "InProgress" },
            new StatusOptionDTO { CVALUE = "APPROVED", CDESCRIPTION = "Approved" },
            new StatusOptionDTO { CVALUE = "CLOSED", CDESCRIPTION = "Closed" }
        };

        public List<StatusOptionDTO> StatusList => _statusList;

        // Document Date property (converts between string and DateTime?)
        public DateTime? DocumentDate
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_VM.CurrentRecord.CDOCUMENT_DATE))
                    return null;
                return R_FrontUtility.R_ConvertToDateTime(_VM.CurrentRecord.CDOCUMENT_DATE);
            }
            set
            {
                if (value.HasValue)
                    _VM.CurrentRecord.CDOCUMENT_DATE = value.Value.ToString("yyyy-MM-dd");
                else
                    _VM.CurrentRecord.CDOCUMENT_DATE = string.Empty;
            }
        }

        // Display properties for audit trail and currency
        public string CreateDateDisplay
        {
            get
            {
                if (_VM.CurrentRecord.DCREATE_DATE != default)
                    return _VM.CurrentRecord.DCREATE_DATE.ToString("dd-MMM-yyyy HH:mm");
                return string.Empty;
            }
        }

        public string UpdateDateDisplay
        {
            get
            {
                if (_VM.CurrentRecord.DUPDATE_DATE != default)
                    return _VM.CurrentRecord.DUPDATE_DATE.ToString("dd-MMM-yyyy HH:mm");
                return string.Empty;
            }
        }

        public string LocalCurrencyDisplay
        {
            get
            {
                if (_VM.CurrentRecord.NLBASE_RATE_AMOUNT > 0 && _VM.CurrentRecord.NLCURRENCY_RATE_AMOUNT > 0)
                {
                    return $"{_VM.CurrentRecord.NLBASE_RATE_AMOUNT:F6} {_VM.CurrentRecord.CCURRENCY_CODE} = {_VM.CurrentRecord.NLCURRENCY_RATE_AMOUNT:F6} {_VM.CurrentRecord.CCURRENCY_CODE}";
                }
                return string.Empty;
            }
        }

        public string BaseCurrencyDisplay
        {
            get
            {
                if (_VM.CurrentRecord.NBBASE_RATE_AMOUNT > 0 && _VM.CurrentRecord.NBCURRENCY_RATE_AMOUNT > 0)
                {
                    return $"{_VM.CurrentRecord.NBBASE_RATE_AMOUNT:F6} {_VM.CurrentRecord.CCURRENCY_CODE} = {_VM.CurrentRecord.NBCURRENCY_RATE_AMOUNT:F6} {_VM.CurrentRecord.CCURRENCY_CODE}";
                }
                return string.Empty;
            }
        }

        protected override async Task R_Init_From_Master(object? poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                ClientHelper.Set_CompanyId("HGRBH");
                ClientHelper.Set_UserId("ZF");
                // Extract parameters from poParameter if available
                string lcReferenceNo = string.Empty;
                string lcDeptCode = string.Empty;

                if (poParameter is FAT00100DTO loParameter)
                {
                    lcReferenceNo = loParameter.CREFERENCE_NO ?? string.Empty;
                    lcDeptCode = loParameter.CDEPT_CODE ?? string.Empty;
                }

                // Call GetInitialProcessAsync to initialize form data
                await _VM.GetInitialProcessAsync(
                    ClientHelper.CompanyId,
                    ClientHelper.UserId,
                    lcReferenceNo,
                    lcDeptCode,
                    FAT00100ViewModel.DEFAULT_PJ_TRANSACTION_CODE, // CPJ_TRANS_CODE (hardcoded as per VB.NET)
                    FAT00100ViewModel.DEFAULT_TRANSACTION_CODE  // CTRANSACTION_CODE (hardcoded as per VB.NET)
                );

                // Update CurrentRecord with transaction code and description from initial process
                if (_VM.InitialProcessData != null)
                {
                    _VM.CurrentRecord.CTRANSACTION_CODE = FAT00100ViewModel.DEFAULT_TRANSACTION_CODE;
                    _VM.CurrentRecord.CTRANSACTION_NAME = _VM.InitialProcessData.CFILTER_TRANS_DESC ?? string.Empty;

                    // Set department code from initial process if not provided in parameter
                    if (string.IsNullOrWhiteSpace(lcDeptCode) && !string.IsNullOrWhiteSpace(_VM.InitialProcessData.CTRANS_DEPT_CODE))
                    {
                        _VM.CurrentRecord.CDEPT_CODE = _VM.InitialProcessData.CTRANS_DEPT_CODE;
                    }
                    else if (!string.IsNullOrWhiteSpace(lcDeptCode))
                    {
                        _VM.CurrentRecord.CDEPT_CODE = lcDeptCode;
                    }
                }

                // Initialize period month combo
                if (!string.IsNullOrEmpty(_VM.SoftPeriod))
                {
                    await _VM.GetComboPeriodMonthAsync(ClientHelper.CompanyId, string.Empty, _VM.SoftPeriod);
                }

                // Initialize period values from SoftPeriod if available
                if (!string.IsNullOrEmpty(_VM.SoftPeriod) && _VM.SoftPeriod.Length >= 6)
                {
                    PeriodFromYear = int.Parse(_VM.SoftPeriod.Substring(0, 4));
                    PeriodFromMonth = _VM.SoftPeriod.Substring(4, 2);
                    PeriodToYear = int.Parse(_VM.SoftPeriod.Substring(0, 4));
                    PeriodToMonth = _VM.SoftPeriod.Substring(4, 2);
                }

                // Initialize status combobox (default value)
                SelectedStatus = "ALL";
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        #region Department Lookup Handlers

        private void btnDepartmentLookup_R_Before_Open_Lookup(R_BeforeOpenLookupEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                // TODO: Replace with actual lookup page type when available
                // For now using a generic parameter DTO structure
                var loParam = new
                {
                    CCOMPANY_ID = ClientHelper.CompanyId,
                    CLANG_ID = ClientHelper.CultureUI.TwoLetterISOLanguageName,
                    CLOOKUP_SENDER_FLAG = "GSL00500",
                    LACTIVE = true
                };

                eventArgs.Parameter = loParam;
                // TODO: Set actual lookup page type
                // eventArgs.TargetPageType = typeof(GSL00500Page);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void btnDepartmentLookup_R_After_Open_Lookup(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                if (eventArgs.Result == null) return;

                // TODO: Replace with actual DTO type when available (e.g., GSL00500DTO)
                // For now using dynamic to handle the result
                dynamic loResult = eventArgs.Result;
                
                // Update department code and name
                // Adjust property names based on actual DTO structure
                _VM.PoDeptCode = loResult.cDeptCode?.ToString().Trim() ?? string.Empty;
                _VM.PoDeptName = loResult.cDeptDesc?.ToString().Trim() ?? string.Empty;
                
                // Update previous value to avoid unnecessary validation on LostFocus
                // (equivalent to lctxtDepartmentCode = txtDepartmentCode.Text in net4)
                _previousDeptCode = _VM.PoDeptCode;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task txtDepartmentCode_OnLostFocused()
        {
            var loEx = new R_Exception();

            try
            {
                // Check if value has changed (equivalent to lctxtDepartmentCode.Trim <> txtDepartmentCode.Text.Trim in net4)
                if (_previousDeptCode.Trim() == _VM.PoDeptCode?.Trim())
                {
                    // Value hasn't changed, no need to process
                    return;
                }

                // If department code is empty, clear name and error
                if (string.IsNullOrWhiteSpace(_VM.PoDeptCode))
                {
                    _VM.PoDeptName = string.Empty;
                    _previousDeptCode = string.Empty;
                    return;
                }

                // TODO: Call lookup service to get department description (equivalent to LookUpDepartmentMasterDescList in net4)
                // For now, we'll validate the department code
                // In net4: loRtn = New GENERAL_PubServiceGateway().LookUpDepartmentMasterDescList(loParam)
                // If loRtn Is Nothing: show error PS001, clear department name
                // If loRtn.lEveryoneFlag = False: validate using ValidateDeptCode
                //   - If validation returns 0: show error PS003
                //   - If validation returns > 0: set department description, clear error
                // If loRtn.lEveryoneFlag = True: set department description, clear error

                // Validate department code using ViewModel method (equivalent to ValidateDeptCode in net4)
                // Note: In net4, this validation only happens if lEveryoneFlag = False
                // For now, we'll always validate (can be enhanced later to check lEveryoneFlag)
                var liResult = await _VM.ValidateDeptCodeAsync(ClientHelper.CompanyId, _VM.PoDeptCode, ClientHelper.UserId);
                
                if (liResult == 0)
                {
                    // Department validation failed (equivalent to PS003 in net4 when lEveryoneFlag = False)
                    // Note: PS001 is shown when lookup returns Nothing, PS003 is shown when validation fails
                    loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "PS003"));
                    // Don't clear the code, just show error (matching net4 behavior)
                }
                else
                {
                    // Validation passed - department description should be populated from lookup
                    // TODO: Get department description from lookup service if not already populated
                    // In net4: txtDepartmentDesc.Text = loRtn.cDeptDesc
                    // For now, description will be populated from lookup button
                }

                // Update previous value (equivalent to lctxtDepartmentCode = txtDepartmentCode.Text in net4)
                _previousDeptCode = _VM.PoDeptCode;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        #endregion

        #region FR Department Lookup Handlers

        private void btnFRDepartmentLookup_R_Before_Open_Lookup(R_BeforeOpenLookupEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                // TODO: Replace with actual lookup page type when available
                // For now using a generic parameter DTO structure
                var loParam = new
                {
                    CCOMPANY_ID = ClientHelper.CompanyId,
                    CLANG_ID = ClientHelper.CultureUI.TwoLetterISOLanguageName,
                    CLOOKUP_SENDER_FLAG = "GSL00500",
                    LACTIVE = true
                };

                eventArgs.Parameter = loParam;
                // TODO: Set actual lookup page type
                // eventArgs.TargetPageType = typeof(GSL00500Page);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void btnFRDepartmentLookup_R_After_Open_Lookup(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                if (eventArgs.Result == null) return;

                // TODO: Replace with actual DTO type when available (e.g., GSL00500DTO)
                // For now using dynamic to handle the result
                dynamic loResult = eventArgs.Result;
                
                // Update FR department code and name
                // Adjust property names based on actual DTO structure
                _VM.Data.CFR_DEPT_CODE = loResult.cDeptCode?.ToString().Trim() ?? string.Empty;
                _VM.Data.CFR_DEPT_NAME = loResult.cDeptDesc?.ToString().Trim() ?? string.Empty;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        #endregion

        #region Supplier Lookup Handlers

        private void btnSupplierLookup_R_Before_Open_Lookup(R_BeforeOpenLookupEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                // TODO: Replace with actual lookup page type when available
                var loParam = new
                {
                    CCOMPANY_ID = ClientHelper.CompanyId,
                    CLANG_ID = ClientHelper.CultureUI.TwoLetterISOLanguageName,
                    CLOOKUP_SENDER_FLAG = "GSL04200"
                };

                eventArgs.Parameter = loParam;
                // TODO: Set actual lookup page type
                // eventArgs.TargetPageType = typeof(GSL04200Page);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void btnSupplierLookup_R_After_Open_Lookup(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                if (eventArgs.Result == null) return;

                // TODO: Replace with actual DTO type when available (e.g., GSL04200DTO)
                dynamic loResult = eventArgs.Result;
                
                // Update supplier code and name
                // Adjust property names based on actual DTO structure
                _VM.PoSupplierId = loResult.cSupplierId?.ToString().Trim() ?? string.Empty;
                _VM.PoSupplierName = loResult.cSupplierName?.ToString().Trim() ?? string.Empty;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task txtSupplierCode_OnLostFocused()
        {
            var loEx = new R_Exception();

            try
            {
                if (string.IsNullOrWhiteSpace(_VM.PoSupplierId))
                {
                    _VM.PoSupplierName = string.Empty;
                    await Task.CompletedTask;
                    return;
                }

                // TODO: Implement supplier validation if ViewModel method exists
                // For now, description will be populated from lookup
                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        #endregion

        #region Refresh Button Handler

        private async Task btnRefresh_OnClick()
        {
            var loEx = new R_Exception();

            try
            {
                // Build period strings
                string lcPeriodFrom = $"{PeriodFromYear}{PeriodFromMonth.PadLeft(2, '0')}";
                string lcPeriodTo = $"{PeriodToYear}{PeriodToMonth.PadLeft(2, '0')}";

                // Convert selected status to backend parameters
                string lcStatusDraft = FAT00100ViewModel.STATUS_FLAG_DISABLED;
                string lcStatusOpen = FAT00100ViewModel.STATUS_FLAG_DISABLED;
                string lcStatusApproved = FAT00100ViewModel.STATUS_FLAG_DISABLED;
                string lcStatusClosed = FAT00100ViewModel.STATUS_FLAG_DISABLED;

                if (SelectedStatus == "ALL")
                {
                    lcStatusDraft = FAT00100ViewModel.STATUS_FLAG_ENABLED;
                    lcStatusOpen = FAT00100ViewModel.STATUS_FLAG_ENABLED;
                    lcStatusApproved = FAT00100ViewModel.STATUS_FLAG_ENABLED;
                    lcStatusClosed = FAT00100ViewModel.STATUS_FLAG_ENABLED;
                }
                else if (SelectedStatus == "DRAFT")
                {
                    lcStatusDraft = FAT00100ViewModel.STATUS_FLAG_ENABLED;
                }
                else if (SelectedStatus == "OPEN")
                {
                    lcStatusOpen = FAT00100ViewModel.STATUS_FLAG_ENABLED;
                }
                else if (SelectedStatus == "INPROGRESS")
                {
                    lcStatusOpen = FAT00100ViewModel.STATUS_FLAG_ENABLED;
                }
                else if (SelectedStatus == "APPROVED")
                {
                    lcStatusApproved = FAT00100ViewModel.STATUS_FLAG_ENABLED;
                }
                else if (SelectedStatus == "CLOSED")
                {
                    lcStatusClosed = FAT00100ViewModel.STATUS_FLAG_ENABLED;
                }

                // Call GetDataGridAsync with filter parameters
                await _VM.GetDataGridAsync(
                    ClientHelper.CompanyId,
                    ClientHelper.CultureUI.TwoLetterISOLanguageName,
                    _VM.PoDeptCode,
                    _VM.CurrentRecord.CTRANSACTION_CODE,
                    "",//_VM.CurrentRecord.CREFERENCE_NO,
                    _VM.PoSupplierId,
                    lcPeriodFrom,
                    lcPeriodTo,

                    lcStatusDraft,
                    lcStatusOpen,
                    lcStatusApproved,
                    lcStatusClosed
                );

                // Refresh grid
                if (_gridRef is not null)
                    await _gridRef.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        #endregion

        #region Grid Service Handler

        private async Task Grid_R_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                // Build period strings
                string lcPeriodFrom = $"{PeriodFromYear}{PeriodFromMonth.PadLeft(2, '0')}";
                string lcPeriodTo = $"{PeriodToYear}{PeriodToMonth.PadLeft(2, '0')}";

                // Convert selected status to backend parameters
                string lcStatusDraft = FAT00100ViewModel.STATUS_FLAG_DISABLED;
                string lcStatusOpen = FAT00100ViewModel.STATUS_FLAG_DISABLED;
                string lcStatusApproved = FAT00100ViewModel.STATUS_FLAG_DISABLED;
                string lcStatusClosed = FAT00100ViewModel.STATUS_FLAG_DISABLED;

                if (SelectedStatus == "ALL")
                {
                    lcStatusDraft = FAT00100ViewModel.STATUS_FLAG_ENABLED;
                    lcStatusOpen = FAT00100ViewModel.STATUS_FLAG_ENABLED;
                    lcStatusApproved = FAT00100ViewModel.STATUS_FLAG_ENABLED;
                    lcStatusClosed = FAT00100ViewModel.STATUS_FLAG_ENABLED;
                }
                else if (SelectedStatus == "DRAFT")
                {
                    lcStatusDraft = FAT00100ViewModel.STATUS_FLAG_ENABLED;
                }
                else if (SelectedStatus == "OPEN")
                {
                    lcStatusOpen = FAT00100ViewModel.STATUS_FLAG_ENABLED;
                }
                else if (SelectedStatus == "INPROGRESS")
                {
                    lcStatusOpen = FAT00100ViewModel.STATUS_FLAG_ENABLED;
                }
                else if (SelectedStatus == "APPROVED")
                {
                    lcStatusApproved = FAT00100ViewModel.STATUS_FLAG_ENABLED;
                }
                else if (SelectedStatus == "CLOSED")
                {
                    lcStatusClosed = FAT00100ViewModel.STATUS_FLAG_ENABLED;
                }

                // Call GetDataGridAsync with filter parameters
                await _VM.GetDataGridAsync(
                    ClientHelper.CompanyId,
                    ClientHelper.CultureUI.TwoLetterISOLanguageName,
                    _VM.PoDeptCode,
                    _VM.CurrentRecord.CTRANSACTION_CODE,
                    "",//_VM.CurrentRecord.CREFERENCE_NO,
                    _VM.PoSupplierId,
                    lcPeriodFrom,
                    lcPeriodTo,
                    lcStatusDraft,
                    lcStatusOpen,
                    lcStatusApproved,
                    lcStatusClosed
                );

                // Set grid data
                eventArgs.ListEntityResult = _VM.DataGridList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        #endregion

        #region Transaction Type Lookup Handlers

        private void btnTransactionTypeLookup_R_Before_Open_Lookup(R_BeforeOpenLookupEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                // TODO: Replace with actual lookup page type when available
                var loParam = new
                {
                    CCOMPANY_ID = ClientHelper.CompanyId,
                    CLANG_ID = ClientHelper.CultureUI.TwoLetterISOLanguageName,
                    CLOOKUP_SENDER_FLAG = "FAT00100"
                };

                eventArgs.Parameter = loParam;
                // TODO: Set actual lookup page type
                // eventArgs.TargetPageType = typeof(TransactionTypeLookupPage);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void btnTransactionTypeLookup_R_After_Open_Lookup(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                if (eventArgs.Result == null) return;

                // TODO: Replace with actual DTO type when available
                dynamic loResult = eventArgs.Result;
                
                // Update transaction type code and name
                _VM.Data.CTRANSACTION_CODE = loResult.cTransactionCode?.ToString().Trim() ?? string.Empty;
                _VM.Data.CTRANSACTION_NAME = loResult.cTransactionName?.ToString().Trim() ?? string.Empty;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        #endregion

        #region Currency Lookup Handlers

        private void btnCurrencyLookup_R_Before_Open_Lookup(R_BeforeOpenLookupEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                // TODO: Replace with actual lookup page type when available
                var loParam = new
                {
                    CCOMPANY_ID = ClientHelper.CompanyId,
                    CLANG_ID = ClientHelper.CultureUI.TwoLetterISOLanguageName,
                    CLOOKUP_SENDER_FLAG = "GSL00100"
                };

                eventArgs.Parameter = loParam;
                // TODO: Set actual lookup page type
                // eventArgs.TargetPageType = typeof(CurrencyLookupPage);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void btnCurrencyLookup_R_After_Open_Lookup(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                if (eventArgs.Result == null) return;

                // TODO: Replace with actual DTO type when available
                dynamic loResult = eventArgs.Result;
                
                // Update currency code and name
                _VM.Data.CCURRENCY_CODE = loResult.cCurrencyCode?.ToString().Trim() ?? string.Empty;
                _VM.Data.CCURRENCY_NAME = loResult.cCurrencyName?.ToString().Trim() ?? string.Empty;
                
                // Set currency rate codes to currency code (equivalent to txtLocalCurrBaseRateCurrCode.Text = txtCurrency.Text in net4)
                if (!string.IsNullOrWhiteSpace(_VM.Data.CCURRENCY_CODE))
                {
                    _VM.Data.CLOCAL_CURRENCY_CODE = _VM.Data.CCURRENCY_CODE;
                    _VM.Data.CBASE_CURRENCY_CODE = _VM.Data.CCURRENCY_CODE;
                }
                
                // TODO: Trigger currency rate calculation if ViewModel method exists
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task txtCurrencyCode_OnLostFocused()
        {
            var loEx = new R_Exception();

            try
            {
                if (string.IsNullOrWhiteSpace(_VM.Data.CCURRENCY_CODE))
                {
                    _VM.Data.CCURRENCY_NAME = string.Empty;
                    _VM.Data.NLBASE_RATE_AMOUNT = 0;
                    _VM.Data.NLCURRENCY_RATE_AMOUNT = 0;
                    _VM.Data.NBBASE_RATE_AMOUNT = 0;
                    _VM.Data.NBCURRENCY_RATE_AMOUNT = 0;
                    _VM.Data.CLOCAL_CURRENCY_CODE = string.Empty;
                    _VM.Data.CBASE_CURRENCY_CODE = string.Empty;
                    return;
                }

                // Set currency rate codes to currency code (equivalent to txtLocalCurrBaseRateCurrCode.Text = txtCurrency.Text in net4)
                _VM.Data.CLOCAL_CURRENCY_CODE = _VM.Data.CCURRENCY_CODE;
                _VM.Data.CBASE_CURRENCY_CODE = _VM.Data.CCURRENCY_CODE;

                // TODO: Implement currency validation and rate calculation if ViewModel method exists
                // For now, description will be populated from lookup
                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        #endregion

        #region PJ Transaction Lookup Handlers

        private void btnPJTransactionLookup_R_Before_Open_Lookup(R_BeforeOpenLookupEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                // TODO: Replace with actual lookup page type when available
                var loParam = new
                {
                    CCOMPANY_ID = ClientHelper.CompanyId,
                    CLANG_ID = ClientHelper.CultureUI.TwoLetterISOLanguageName,
                    CLOOKUP_SENDER_FLAG = "PJ00100"
                };

                eventArgs.Parameter = loParam;
                // TODO: Set actual lookup page type
                // eventArgs.TargetPageType = typeof(PJTransactionLookupPage);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void btnPJTransactionLookup_R_After_Open_Lookup(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                if (eventArgs.Result == null) return;

                // TODO: Replace with actual DTO type when available
                dynamic loResult = eventArgs.Result;
                
                // Update PJ transaction number
                _VM.CurrentRecord.CFR_REFERENCE_NO = loResult.cReferenceNo?.ToString().Trim() ?? string.Empty;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        #endregion

        #region Conductor Handlers

        private async Task Conductor_R_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                // Extract selected record from grid (from Navigator grid selection)
                var loGridRow = R_FrontUtility.ConvertObjectToObject<FAT00100GetDataGridResultDTO>(eventArgs.Data);

                if (loGridRow != null && !string.IsNullOrWhiteSpace(loGridRow.CREFERENCE_NO))
                {
                    // Convert grid row to FAT00100DTO (following GSM02000 pattern)
                    var loParam = new FAT00100DTO
                    {
                        CCOMPANY_ID = ClientHelper.CompanyId,
                        CLANG_ID = ClientHelper.CultureUI.TwoLetterISOLanguageName,
                        CDEPT_CODE = loGridRow.CDEPT_CODE ?? string.Empty,
                        CFILTER_TRANS_CODE = FAT00100ViewModel.DEFAULT_TRANSACTION_CODE, // CFILTER_TRANS_CODE (hardcoded as per initialization)
                        CREFERENCE_NO = loGridRow.CREFERENCE_NO
                    };

                    // Call ViewModel GetEntity method (following GSM02000 pattern)
                    await _VM.GetEntity(loParam);
                    eventArgs.Result = _VM.CurrentRecord;
                }
                else
                {
                    // If no valid grid row, return empty DTO
                    eventArgs.Result = new FAT00100DTO();
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task Conductor_R_Display(R_DisplayEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                // Update status display label (handled by binding)
                // Populate lookup descriptions (handled by lookup handlers)
                // Update currency rate display fields
                if (!string.IsNullOrWhiteSpace(_VM.CurrentRecord.CCURRENCY_CODE))
                {
                    // TODO: Calculate and set currency display fields if ViewModel method exists
                    // _VM.CurrentRecord.CLOCAL_CURRENCY_DISPLAY = ...;
                    // _VM.CurrentRecord.CBASE_CURRENCY_DISPLAY = ...;
                }

                // Format audit trail dates for display (handled by properties)
                // Currency display fields are handled by properties

                // Refresh Asset List tab page with current data (equivalent to InvokeRefreshTabPageAsync in PMM01000)
                // Check if conductor is in Normal mode and Asset List tab is active
                if (_conductorRef != null && _conductorRef.R_ConductorMode == R_eConductorMode.Normal)
                {
                    // Check if Asset List tab is active (equivalent to _TabGeneral.ActiveTab.Id == "Rate" in PMM01000)
                    if (_tabStripRef?.ActiveTab?.Id == nameof(FAT00100AssetList) && _tabPageAssetList != null)
                    {
                        // Store reference to avoid null reference warning
                        var loTabPageAssetList = _tabPageAssetList;

                        // Get current data from conductor (equivalent to loTempParamUtility in PMM01000)
                        var loTempParam = _conductorRef!.R_GetCurrentData();

                        // Convert to FAT00100DTO for Asset List tab parameter
                        FAT00100DTO loParam;
                        if (loTempParam is FAT00100DTO loDTO)
                        {
                            loParam = loDTO;
                        }
                        else if (loTempParam != null)
                        {
                            // Convert if needed - loTempParam is not null here due to the if condition
                            loParam = R_FrontUtility.ConvertObjectToObject<FAT00100DTO>(loTempParam) ?? new FAT00100DTO();
                        }
                        else
                        {
                            // Use CurrentRecord if no data from conductor
                            loParam = _VM?.CurrentRecord ?? new FAT00100DTO();
                        }

                        // Refresh Asset List tab page with current data
                        // loTabPageAssetList is not null here due to the null check above
                        await loTabPageAssetList!.InvokeRefreshTabPageAsync(loParam);
                    }
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        /// <summary>
        /// Grid display handler - called when grid row is selected
        /// </summary>
        private void Grid_R_Display(R_DisplayEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                // Grid display logic handled by conductor
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void Conductor_R_AfterAdd(R_AfterAddEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                // Extract entity from eventArgs.Data
                var loEntity = (FAT00100DTO)eventArgs.Data;

                // Store filter supplier ID before clearing (equivalent to txtSuppIDFilter.Text in net4)
                string lcFilterSupplierId = _VM.PoSupplierId;

                // Clear form fields (equivalent to clearing local variables in net4)
                // Set values on both loEntity (eventArgs.Data) and _VM.Data for consistency
                loEntity.CSUPPLIER_ID = string.Empty;
                loEntity.CSUPPLIER_NAME = string.Empty;
                loEntity.CDEPT_CODE = string.Empty;
                loEntity.CCURRENCY_CODE = string.Empty;
                loEntity.CCURRENCY_NAME = string.Empty;
                loEntity.CINFO_SEQNO = string.Empty;
                loEntity.CREFERENCE_NO = string.Empty;

                // Set transaction date to current date
                loEntity.DTRANSACTION_DATE = DateTime.Now;
                loEntity.CTRANSACTION_DATE = DateTime.Now.ToString("yyyyMMdd");

                // Set document date to transaction date
                loEntity.DDOCUMENT_DATE = loEntity.DTRANSACTION_DATE;
                loEntity.CDOCUMENT_DATE = loEntity.CTRANSACTION_DATE;

                // Set FA source as default (CFR_MODULE = "FA") - equivalent to rdbFA.IsChecked = True
                loEntity.CFR_MODULE = FAT00100ViewModel.DEFAULT_SOURCE_MODULE_FA;

                // Set hardcoded transaction code (equivalent to filter transaction code in net4)
                loEntity.CTRANSACTION_CODE = FAT00100ViewModel.DEFAULT_TRANSACTION_CODE;
                // Note: Transaction name should be populated from InitialProcessData if available
                if (_VM.InitialProcessData != null && !string.IsNullOrWhiteSpace(_VM.InitialProcessData.CFILTER_TRANS_DESC))
                {
                    loEntity.CTRANSACTION_NAME = _VM.InitialProcessData.CFILTER_TRANS_DESC;
                }

                // Set supplier code from filter (equivalent to txtSupplierCode.Text = txtSuppIDFilter.Text in net4)
                if (!string.IsNullOrWhiteSpace(lcFilterSupplierId))
                {
                    loEntity.CSUPPLIER_ID = lcFilterSupplierId;
                    // Note: Supplier name should be populated from lookup validation if needed
                    // In net4, this triggers txtSupplierCode_LostFocus which validates and populates name
                }

                // Set currency to local currency code (equivalent to txtCurrency.Text = pcLocalCurrencyCode in net4)
                if (!string.IsNullOrWhiteSpace(_VM.LocalCurrencyCode))
                {
                    loEntity.CCURRENCY_CODE = _VM.LocalCurrencyCode;
                    // Note: Currency name should be populated from lookup validation if needed
                    // In net4, this triggers txtCurrency_LostFocus which validates and populates name
                }

                // Set default currency rate codes (equivalent to txtLocalCurrCurrencyRateCurrCode.Text = pcLocalCurrencyCode in net4)
                loEntity.CLOCAL_CURRENCY_CODE = _VM.LocalCurrencyCode ?? string.Empty;
                loEntity.CBASE_CURRENCY_CODE = _VM.BaseCurrencyCode ?? string.Empty;

                // Clear nested DTOs
                loEntity.oCP = new List<FAT00100CPDTO>();
                loEntity.oSupp = null;
                _VM.ContactPersonList.Clear();
                _VM.SupplierInfo = new FAT00100GetGSM_SUPPLIER_INFOResultDTO();

                // Clear reference number for new record
                loEntity.CREFERENCE_NO = string.Empty;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void Conductor_R_Saving(R_SavingEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                // Get entity directly from eventArgs.Data (following example pattern)
                var loEntity = (FAT00100DTO)eventArgs.Data;

                // Set System Fields from ClientHelper (equivalent to net4 conMain_R_Saving lines 808-815)
                loEntity.CLANG_ID = ClientHelper.CultureUI.TwoLetterISOLanguageName; // CFOREIGN_LANGUAGE in net4
                loEntity.CFILTER_TRANS_CODE = _VM.CurrentRecord.CTRANSACTION_CODE; // from filter, txtTransactionTypeCodeFilter equivalent
                loEntity.CCREATE_BY = ClientHelper.UserId;
                loEntity.CUPDATE_BY = ClientHelper.UserId;
                loEntity.CCOMPANY_ID = ClientHelper.CompanyId;
                loEntity.CUSER_ID = ClientHelper.UserId;
                // CDEPT_CODE should come from filter (PoDeptCode) if available, otherwise from CurrentRecord
                loEntity.CDEPT_CODE = !string.IsNullOrWhiteSpace(_VM.PoDeptCode) ? _VM.PoDeptCode : loEntity.CDEPT_CODE; // from filter, txtDepartmentCodeFilter equivalent
                loEntity.CTRANSACTION_CODE = _VM.CurrentRecord.CTRANSACTION_CODE; // from filter

                // Set Flags from ViewModel (equivalent to net4 lines 816, 819)
                // Use IncrementFlag property which is set from InitialProcessData in GetInitialProcessAsync (equivalent to plIncrementFlag in net4)
                loEntity.LINCREMENT_FLAG = _VM.IncrementFlag; // plIncrementFlag in net4 (line 117: plIncrementFlag = loRtn._LINCREMENT_FLAG)
                loEntity.LINCREMENT_FLAG = true; // testing hardcore true
                // LONETIME_FLAG - already in entity from CurrentRecord, no need to set

                // Set Dates (convert DateTime? to string "yyyyMMdd") - equivalent to net4 lines 817-818
                if (loEntity.DTRANSACTION_DATE.HasValue)
                {
                    loEntity.CTRANSACTION_DATE = loEntity.DTRANSACTION_DATE.Value.ToString("yyyyMMdd");
                }
                else
                {
                    loEntity.CTRANSACTION_DATE = string.Empty;
                }

                if (loEntity.DDOCUMENT_DATE.HasValue)
                {
                    loEntity.CDOCUMENT_DATE = loEntity.DDOCUMENT_DATE.Value.ToString("yyyyMMdd");
                }
                else
                {
                    loEntity.CDOCUMENT_DATE = string.Empty;
                }

                // Handle FA/PJ Source Selection (equivalent to net4 lines 821-828)
                if (loEntity.CFR_MODULE == FAT00100ViewModel.DEFAULT_SOURCE_MODULE_FA)
                {
                    // If FA: clear FR fields
                    loEntity.CFR_DEPT_CODE = string.Empty;
                    loEntity.CFR_TRANSACTION_CODE = string.Empty;
                    loEntity.CFR_REFERENCE_NO = string.Empty;
                }
                // If PJ: keep values from UI (already bound in razor)

                // Calculate LGLLINK (equivalent to net4 lines 830-836)
                string lcGlinkDate = _VM.InitialProcessData?.CGLLINK_DATE ?? string.Empty;
                loEntity.LGLLINK = (string.Compare(lcGlinkDate, loEntity.CTRANSACTION_DATE) <= 0);

                // Set Status for Add Mode (equivalent to net4 lines 838-842)
                if (eventArgs.ConductorMode == R_eConductorMode.Add)
                {
                    loEntity.CSTATUS = FAT00100ViewModel.DEFAULT_STATUS_DRAFT; // Draft
                    loEntity.CGL_TRF_STATUS = FAT00100ViewModel.DEFAULT_GL_TRF_STATUS;
                }

                // Handle Transaction Period (equivalent to net4 line 847)
                // Set CTRANSACTION_PRD = First 6 characters of CTRANSACTION_DATE (YYYYMM format)
                if (!string.IsNullOrEmpty(loEntity.CTRANSACTION_DATE) && loEntity.CTRANSACTION_DATE.Length >= 6)
                {
                    loEntity.CTRANSACTION_PRD = loEntity.CTRANSACTION_DATE.Substring(0, 6);
                }
                else
                {
                    loEntity.CTRANSACTION_PRD = string.Empty;
                }

                // Prepare Supplier Info (oSupp and oCP) - equivalent to net4 lines 848-858
                if (string.IsNullOrWhiteSpace(loEntity.CINFO_SEQNO))
                {
                    // If empty: Create new FAT00100SuppDTO
                    loEntity.oSupp = new FAT00100SuppDTO
                    {
                        CCOMPANY_ID = ClientHelper.CompanyId,
                        CSUPPLIER_ID = loEntity.CSUPPLIER_ID, // from entity (already bound from UI)
                        CSUPPLIER_NAME = loEntity.CSUPPLIER_NAME // from entity (already bound from UI)
                    };
                    loEntity.oCP = new List<FAT00100CPDTO>();
                }
                else
                {
                    // If not empty: Get supplier info and contact list from ViewModel
                    // Note: This should be called asynchronously, but R_Saving is synchronous
                    // The supplier info should already be loaded in ViewModel from previous operations
                    if (_VM.SupplierInfo != null && !string.IsNullOrWhiteSpace(_VM.SupplierInfo.CSUPPLIER_ID))
                    {
                        // Map SupplierInfo to oSupp
                        loEntity.oSupp = new FAT00100SuppDTO
                        {
                            CCOMPANY_ID = _VM.SupplierInfo.CCOMPANY_ID,
                            CSUPPLIER_ID = _VM.SupplierInfo.CSUPPLIER_ID,
                            CINFO_SEQNO = _VM.SupplierInfo.CINFO_SEQNO,
                            CSUPPLIER_NAME = _VM.SupplierInfo.CSUPPLIER_NAME,
                            CADDRESS = _VM.SupplierInfo.CADDRESS,
                            CPOSTAL_CODE = _VM.SupplierInfo.CPOSTAL_CODE,
                            CCITY = _VM.SupplierInfo.CCITY,
                            CCOUNTRY_CODE = _VM.SupplierInfo.CCOUNTRY_CODE,
                            CSTATE_CODE = _VM.SupplierInfo.CSTATE_CODE,
                            CPHONE_1 = _VM.SupplierInfo.CPHONE_1,
                            CPHONE_2 = _VM.SupplierInfo.CPHONE_2,
                            CPHONE_3 = _VM.SupplierInfo.CPHONE_3,
                            CFAX_NO1 = _VM.SupplierInfo.CFAX_NO1,
                            CFAX_NO2 = _VM.SupplierInfo.CFAX_NO2,
                            CFAX_NO3 = _VM.SupplierInfo.CFAX_NO3,
                            CEMAIL_1 = _VM.SupplierInfo.CEMAIL_1,
                            CEMAIL_2 = _VM.SupplierInfo.CEMAIL_2,
                            CEMAIL_3 = _VM.SupplierInfo.CEMAIL_3,
                            CTAX_REG_TP = _VM.SupplierInfo.CTAX_REG_TP,
                            CTAX_NAME = _VM.SupplierInfo.CTAX_NAME,
                            CTAX_REGISTER_ID = _VM.SupplierInfo.CTAX_REGISTER_ID,
                            DTAX_REGISTER_DATE = _VM.SupplierInfo.DTAX_REGISTER_DATE,
                            CTAX_BUSINESS_TYPE = _VM.SupplierInfo.CTAX_BUSINESS_TYPE,
                            CTAX_BUSINESS_NAME = _VM.SupplierInfo.CTAX_BUSINESS_NAME
                        };
                    }

                    // Set oCP from ContactPersonList
                    if (_VM.ContactPersonList != null && _VM.ContactPersonList.Count > 0)
                    {
                        loEntity.oCP = new List<FAT00100CPDTO>(_VM.ContactPersonList);
                    }
                    else
                    {
                        loEntity.oCP = new List<FAT00100CPDTO>();
                    }
                }

                // Debug: Verify the value is set correctly
                System.Diagnostics.Debug.WriteLine($"[FAT00100 R_Saving] loEntity.LINCREMENT_FLAG = {loEntity.LINCREMENT_FLAG}");
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task Conductor_R_ServiceSave(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                // Get entity directly from eventArgs.Data (following example pattern)
                var loParam = (FAT00100DTO)eventArgs.Data;
                
                System.Diagnostics.Debug.WriteLine($"[FAT00100 R_ServiceSave] loParam.LINCREMENT_FLAG = {loParam.LINCREMENT_FLAG}");
                
                // Set CompanyId, UserId, LangId from ClientHelper
                
                loParam.CCOMPANY_ID = ClientHelper.CompanyId;
                loParam.CUSER_ID = ClientHelper.UserId;
                loParam.CLANG_ID = ClientHelper.CultureUI.TwoLetterISOLanguageName;

                // Call ViewModel save method
                await _VM.SaveRecordAsync(
                    loParam, 
                    (eCRUDMode)eventArgs.ConductorMode, 
                    ClientHelper.CompanyId, 
                    ClientHelper.CultureUI.TwoLetterISOLanguageName
                );
                
                // Set result - this will update the conductor's bound entity with the generated CREFERENCE_NO
                // The result entity from backend contains the generated CREFERENCE_NO when LINCREMENT_FLAG = True
                // The conductor will update the Data property (read-only) from this result
                eventArgs.Result = _VM.CurrentRecord;

                // Refresh grid after save
                if (_gridRef != null)
                    await _gridRef.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task Conductor_R_ServiceDelete(R_ServiceDeleteEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                // Get entity directly from eventArgs.Data (following example pattern)
                var loParam = (FAT00100DTO)eventArgs.Data;

                // Call ViewModel delete method
                await _VM.DeleteRecordAsync(
                    loParam,
                    ClientHelper.CompanyId,
                    ClientHelper.CultureUI.TwoLetterISOLanguageName
                );

                // Refresh grid after delete
                if (_gridRef != null)
                    await _gridRef.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void Conductor_R_Validation(R_ValidationEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                // Get entity from conductor's current data (contains all bound UI values)
                // Note: R_GetCurrentData() returns the current entity with all bound values from the form
                // This ensures we have all the data entered by the user, even if eventArgs.Data is empty
                FAT00100DTO loEntity;
                if (_conductorRef != null)
                {
                    var loCurrentData = _conductorRef.R_GetCurrentData();
                    if (loCurrentData != null && loCurrentData is FAT00100DTO)
                    {
                        loEntity = (FAT00100DTO)loCurrentData;
                    }
                    else if (eventArgs.Data != null && eventArgs.Data is FAT00100DTO)
                    {
                        // Fallback to eventArgs.Data if conductor data is not available
                        loEntity = (FAT00100DTO)eventArgs.Data;
                    }
                    else
                    {
                        // Fallback to CurrentRecord if both are not available
                        loEntity = _VM.CurrentRecord;
                    }
                }
                else if (eventArgs.Data != null && eventArgs.Data is FAT00100DTO)
                {
                    loEntity = (FAT00100DTO)eventArgs.Data;
                }
                else
                {
                    // Fallback to CurrentRecord if conductor is not available
                    loEntity = _VM.CurrentRecord;
                }

                // Add mode: Validate transaction number when LINCREMENT_FLAG is false
                if (eventArgs.ConductorMode == R_eConductorMode.Add)
                {
                    // Use IncrementFlag property which is set from InitialProcessData in GetInitialProcessAsync (equivalent to plIncrementFlag in net4)
                    bool llIncrementFlag = _VM.IncrementFlag;
                    if (!llIncrementFlag)
                    {
                        if (string.IsNullOrWhiteSpace(loEntity.CREFERENCE_NO))
                        {
                            loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "PS009"));
                        }
                    }
                }

                // Validate transaction date
                if (loEntity.DTRANSACTION_DATE == null)
                {
                    loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "PS006"));
                }

                // Validate currency
                if (string.IsNullOrWhiteSpace(loEntity.CCURRENCY_CODE))
                {
                    loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "PS007"));
                }

                // Validate PJ fields when PJ source is selected
                if (loEntity.CFR_MODULE == FAT00100ViewModel.DEFAULT_SOURCE_MODULE_PJ)
                {
                    if (string.IsNullOrWhiteSpace(loEntity.CFR_DEPT_CODE) || string.IsNullOrWhiteSpace(loEntity.CFR_REFERENCE_NO))
                    {
                        loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "PS008"));
                    }
                }

                // Validate document date <= transaction date
                string lcDocumentDate = string.Empty;
                string lcTransactionDate = string.Empty;

                if (loEntity.DDOCUMENT_DATE.HasValue)
                {
                    lcDocumentDate = loEntity.DDOCUMENT_DATE.Value.ToString("yyyyMMdd");
                }
                else if (!string.IsNullOrWhiteSpace(loEntity.CDOCUMENT_DATE))
                {
                    lcDocumentDate = loEntity.CDOCUMENT_DATE;
                }

                if (loEntity.DTRANSACTION_DATE.HasValue)
                {
                    lcTransactionDate = loEntity.DTRANSACTION_DATE.Value.ToString("yyyyMMdd");
                }
                else if (!string.IsNullOrWhiteSpace(loEntity.CTRANSACTION_DATE))
                {
                    lcTransactionDate = loEntity.CTRANSACTION_DATE;
                }

                if (!string.IsNullOrEmpty(lcDocumentDate) && !string.IsNullOrEmpty(lcTransactionDate))
                {
                    if (string.Compare(lcDocumentDate, lcTransactionDate) > 0)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "PS013"));
                    }
                }

                // Validate required fields (existing validations)
                if (string.IsNullOrWhiteSpace(loEntity.CDEPT_CODE))
                {
                    loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "PS003"));
                }
                if (string.IsNullOrWhiteSpace(loEntity.CSUPPLIER_ID))
                {
                    // Note: Supplier validation - check net4 for correct error code if different
                    loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "PS010"));
                }

                // Validate transaction date >= soft period (PS004 in net4 - line 1275-1277)
                if (!string.IsNullOrEmpty(lcTransactionDate) && !string.IsNullOrWhiteSpace(_VM.SoftPeriod))
                {
                    string lcTransactionPrd = lcTransactionDate.Length >= 6 ? lcTransactionDate.Substring(0, 6) : string.Empty;
                    if (!string.IsNullOrEmpty(lcTransactionPrd) && string.Compare(lcTransactionPrd, _VM.SoftPeriod) < 0)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "PS004"));
                    }
                }

                // TODO: PJ transaction validation (PS037) - requires async call to ValidatePJTrans
                // This should be handled in a separate validation step or made async if framework supports it
                // If LCHANGE_DESC is false and PJ is selected, need to call ValidatePJTrans service

                if (loEx.HasError)
                {
                    eventArgs.Cancel = true;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void OnSourceChanged(string pcNewValue)
        {
            var loEx = new R_Exception();

            try
            {
                // MANDATORY: Update CFR_MODULE property manually when using ValueChanged (cannot use @bind-Value)
                _VM.Data.CFR_MODULE = pcNewValue;

                // Check if conductor is in Add or Edit mode
                if (_conductorRef != null)
                {
                    var leMode = _conductorRef.R_ConductorMode;
                    bool llIsAddOrEdit = (leMode == R_eConductorMode.Add || leMode == R_eConductorMode.Edit);
                    bool llChangeDesc = _VM.Data.LCHANGE_DESC;

                    if (llIsAddOrEdit)
                    {
                        if (pcNewValue == FAT00100ViewModel.DEFAULT_SOURCE_MODULE_PJ)
                        {
                            // PJ selected - equivalent to rdbPJ.CheckStateChanged in net4
                            // SourceOptional() logic: Enable PJ fields, disable/clear FA fields
                            if (!llChangeDesc)
                            {
                                // Set PJ transaction code and name (equivalent to txtTransactionTypeCode.Text = "420010" in net4)
                                _VM.Data.CFR_TRANSACTION_CODE = FAT00100ViewModel.DEFAULT_PJ_TRANSACTION_CODE;
                                _VM.Data.CFR_TRANSACTION_NAME = _VM.PJTransDesc;
                            }
                            else
                            {
                                // When LCHANGE_DESC is true, clear transaction code
                                _VM.Data.CFR_TRANSACTION_CODE = string.Empty;
                                _VM.Data.CFR_TRANSACTION_NAME = string.Empty;
                            }

                            // Clear FA-related fields (equivalent to disabling them in net4)
                            // Supplier fields
                            _VM.Data.CSUPPLIER_ID = string.Empty;
                            _VM.Data.CSUPPLIER_NAME = string.Empty;
                            _VM.PoSupplierId = string.Empty;
                            _VM.PoSupplierName = string.Empty;
                            _VM.Data.CINFO_SEQNO = string.Empty;
                            
                            // Document fields
                            _VM.Data.CDOCUMENT_NO = string.Empty;
                            _VM.Data.CDOCUMENT_DATE = string.Empty;
                            _VM.Data.DDOCUMENT_DATE = null;
                            
                            // Currency fields
                            _VM.Data.CCURRENCY_CODE = string.Empty;
                            _VM.Data.CCURRENCY_NAME = string.Empty;
                            _VM.Data.NLBASE_RATE_AMOUNT = 0;
                            _VM.Data.NLCURRENCY_RATE_AMOUNT = 0;
                            _VM.Data.NBBASE_RATE_AMOUNT = 0;
                            _VM.Data.NBCURRENCY_RATE_AMOUNT = 0;
                            
                            // Clear nested DTOs
                            _VM.Data.oCP = new List<FAT00100CPDTO>();
                            _VM.Data.oSupp = null;
                            _VM.ContactPersonList.Clear();
                            _VM.SupplierInfo = new FAT00100GetGSM_SUPPLIER_INFOResultDTO();
                        }
                        else if (pcNewValue == FAT00100ViewModel.DEFAULT_SOURCE_MODULE_FA)
                        {
                            // FA selected - equivalent to rdbFA.CheckStateChanged in net4
                            // SourceOptional() logic: Enable FA fields, disable/clear PJ fields
                            
                            // Clear PJ transaction code and name (equivalent to txtTransactionTypeCode.Text = "" in net4)
                            _VM.Data.CFR_TRANSACTION_CODE = string.Empty;
                            _VM.Data.CFR_TRANSACTION_NAME = string.Empty;
                            
                            // Clear PJ-related fields
                            _VM.Data.CFR_DEPT_CODE = string.Empty;
                            _VM.Data.CFR_DEPT_NAME = string.Empty;
                            _VM.Data.CFR_REFERENCE_NO = string.Empty;
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

        #endregion

        #region Button Event Handlers

        #region Detail Popup Handlers

        private async Task btnDetail_OnClick()
        {
            var loEx = new R_Exception();

            try
            {
                // Get current entity from conductor
                var loEntity = _VM.CurrentRecord;

                if (loEntity == null || string.IsNullOrWhiteSpace(loEntity.CREFERENCE_NO))
                {
                    loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "PS009"));
                    loEx.ThrowExceptionIfErrors();
                    return;
                }

                // Create parameter DTO for FAT0010002
                var loParam = new FAT0010002DTO
                {
                    CDEPT_CODE = loEntity.CDEPT_CODE ?? string.Empty,
                    CTRANSACTION_CODE = loEntity.CTRANSACTION_CODE ?? string.Empty,
                    CREFERENCE_NO = loEntity.CREFERENCE_NO,
                    CSTATUS = loEntity.CSTATUS ?? string.Empty,
                    CMODE = "T", // T=Transaction, V=View
                    CLOCAL_CURRENCY_CODE = _VM.LocalCurrencyCode ?? string.Empty,
                    CBASE_CURRENCY_CODE = _VM.BaseCurrencyCode ?? string.Empty,
                    LASSET_INCREMENT_FLAG = _VM.IncrementFlag,
                    LJRNGRP_MODE = _VM.JrngrpMode,
                    LDEPT_MODE = _VM.DeptMode,
                    CASSET_DEPT_CODE = _VM.DefaultAssetDeptCode ?? string.Empty,
                    LGLLINK = _VM.GLLink,
                    CGLLINK_DATE = _VM.GlinkDate ?? string.Empty
                };

                // Create popup settings with large size
                var loPopupSettings = new R_PopupSettings
                {
                    PageTitle = Localizer["_Detail"],
                    WithLock = true,
                    Page = this,
                    Width = "100%"
                };

                // Show popup using PopupService
                var loResult = await PopupService.Show(typeof(FAT0010002), loParam, poPopupSettings: loPopupSettings);

                // Handle result if popup was successful
                if (loResult.Success && loResult.Result != null)
                {
                    // Refresh conductor with updated data if needed
                    if (_conductorRef != null)
                    {
                        var loRefreshParam = new FAT00100DTO
                        {
                            CCOMPANY_ID = ClientHelper.CompanyId,
                            CLANG_ID = ClientHelper.CultureUI.TwoLetterISOLanguageName,
                            CDEPT_CODE = _VM.PoDeptCode,
                            CFILTER_TRANS_CODE = FAT00100ViewModel.DEFAULT_TRANSACTION_CODE,
                            CREFERENCE_NO = _VM.CurrentRecord.CREFERENCE_NO
                        };

                        await _VM.GetEntity(loRefreshParam);
                        await _conductorRef.R_GetEntity(loRefreshParam);
                    }

                    // Refresh grid if needed
                    if (_gridRef != null)
                    {
                        await _gridRef.R_RefreshGrid(null);
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

        private async Task btnSubmit_OnClick()
        {
            var loEx = new R_Exception();

            try
            {
                // Get current entity from conductor
                var loEntity = _VM.CurrentRecord;

                // Validate that we have a reference number
                if (string.IsNullOrWhiteSpace(loEntity.CREFERENCE_NO))
                {
                    loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "PS009"));
                    R_DisplayException(loEx);
                    return;
                }

                // Validation before submit (only for Draft status "00") - equivalent to net4 lines 2265-2272
                if (loEntity.CSTATUS == FAT00100ViewModel.DEFAULT_STATUS_DRAFT)
                {
                    var loValidationResult = await _VM.ValidationBeforeSubmitAsync(
                        ClientHelper.CompanyId,
                        loEntity.CDEPT_CODE,
                        loEntity.CTRANSACTION_CODE,
                        loEntity.CREFERENCE_NO
                    );

                    // Check if validation result is empty/null (equivalent to net4 line 2268)
                    // NET4 returns a string, NET6 returns DTO with CASSET_CODE property
                    if (string.IsNullOrWhiteSpace(loValidationResult?.CASSET_CODE))
                    {
                        loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "PS036"));
                        R_DisplayException(loEx);
                        return;
                    }
                }

                // Show confirmation message based on current status - equivalent to net4 line 2275
                string lcMessageKey = loEntity.CSTATUS == FAT00100ViewModel.DEFAULT_STATUS_DRAFT ? "_msgSubmit" : "_msgDraft";
                string lcMessage = R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), lcMessageKey);

                var leResult = await R_MessageBox.Show(
                    "",
                    lcMessage,
                    R_eMessageBoxButtonType.YesNo
                );

                if (leResult == R_eMessageBoxResult.No)
                {
                    return;
                }

                // Call submit process - equivalent to net4 line 2278
                // ViewModel uses CurrentRecord for CDEPT_CODE, CTRANSACTION_CODE, CREFERENCE_NO
                await _VM.SubmitProcessAsync(
                    ClientHelper.CompanyId,
                    ClientHelper.CultureUI.TwoLetterISOLanguageName,
                    ClientHelper.UserId
                );

                // Reload entity to get updated status - equivalent to net4 conForm.R_GetEntity(loParam)
                var loParam = new FAT00100DTO
                {
                    CCOMPANY_ID = ClientHelper.CompanyId,
                    CLANG_ID = ClientHelper.CultureUI.TwoLetterISOLanguageName,
                    CDEPT_CODE = _VM.PoDeptCode,
                    CFILTER_TRANS_CODE = FAT00100ViewModel.DEFAULT_TRANSACTION_CODE,
                    CREFERENCE_NO = loEntity.CREFERENCE_NO
                };

                await _VM.GetEntity(loParam);

                // Refresh grid - equivalent to net4 RefreshGridRow()
                if (_gridRef != null)
                    await _gridRef.R_RefreshGrid(null);

                // After grid refresh, get the first row and display it in conductor
                // This ensures the conductor displays the data from the first row that was selected by the grid
                if (_VM.DataGridList != null && _VM.DataGridList.Count > 0)
                {
                    var loFirstRow = _VM.DataGridList[0];
                    var loFirstRowParam = new FAT00100DTO
                    {
                        CCOMPANY_ID = ClientHelper.CompanyId,
                        CLANG_ID = ClientHelper.CultureUI.TwoLetterISOLanguageName,
                        CDEPT_CODE = loFirstRow.CDEPT_CODE ?? string.Empty,
                        CFILTER_TRANS_CODE = FAT00100ViewModel.DEFAULT_TRANSACTION_CODE,
                        CREFERENCE_NO = loFirstRow.CREFERENCE_NO
                    };

                    // Get entity for the first row
                    await _VM.GetEntity(loFirstRowParam);

                    // Display first row in conductor
                    if (_conductorRef != null)
                        await _conductorRef.R_GetEntity(loFirstRowParam);
                }
                else
                {
                    // If no data in grid, just refresh conductor with the submitted record
                    if (_conductorRef != null)
                        await _conductorRef.R_GetEntity(loParam);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private async Task btnRedraft_OnClick()
        {
            var loEx = new R_Exception();

            try
            {
                // Get current entity from conductor
                var loEntity = _VM.CurrentRecord;

                // Validate that we have a reference number
                if (string.IsNullOrWhiteSpace(loEntity.CREFERENCE_NO))
                {
                    loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "PS009"));
                    R_DisplayException(loEx);
                    return;
                }

                // Validate status is Open (01) - redraft only works from Open status
                if (loEntity.CSTATUS != "01")
                {
                    loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "PS009"));
                    R_DisplayException(loEx);
                    return;
                }

                // Show confirmation message for redraft - equivalent to net4 line 2275
                // When status is "01" (Open), show "_msgDraft" message
                string lcMessage = R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "_msgDraft");

                var leResult = await R_MessageBox.Show(
                    "",
                    lcMessage,
                    R_eMessageBoxButtonType.YesNo
                );

                if (leResult == R_eMessageBoxResult.No)
                {
                    return;
                }

                // Call submit process - same method handles both submit and redraft
                // ViewModel uses CurrentRecord for CDEPT_CODE, CTRANSACTION_CODE, CREFERENCE_NO
                await _VM.SubmitProcessAsync(
                    ClientHelper.CompanyId,
                    ClientHelper.CultureUI.TwoLetterISOLanguageName,
                    ClientHelper.UserId
                );

                // Reload entity to get updated status - equivalent to net4 conForm.R_GetEntity(loParam)
                var loParam = new FAT00100DTO
                {
                    CCOMPANY_ID = ClientHelper.CompanyId,
                    CLANG_ID = ClientHelper.CultureUI.TwoLetterISOLanguageName,
                    CDEPT_CODE = _VM.PoDeptCode,
                    CFILTER_TRANS_CODE = FAT00100ViewModel.DEFAULT_TRANSACTION_CODE,
                    CREFERENCE_NO = loEntity.CREFERENCE_NO
                };

                await _VM.GetEntity(loParam);

                // Refresh grid - equivalent to net4 RefreshGridRow()
                if (_gridRef != null)
                    await _gridRef.R_RefreshGrid(null);

                // After grid refresh, get the first row and display it in conductor
                // This ensures the conductor displays the data from the first row that was selected by the grid
                if (_VM.DataGridList != null && _VM.DataGridList.Count > 0)
                {
                    var loFirstRow = _VM.DataGridList[0];
                    var loFirstRowParam = new FAT00100DTO
                    {
                        CCOMPANY_ID = ClientHelper.CompanyId,
                        CLANG_ID = ClientHelper.CultureUI.TwoLetterISOLanguageName,
                        CDEPT_CODE = loFirstRow.CDEPT_CODE ?? string.Empty,
                        CFILTER_TRANS_CODE = FAT00100ViewModel.DEFAULT_TRANSACTION_CODE,
                        CREFERENCE_NO = loFirstRow.CREFERENCE_NO
                    };

                    // Get entity for the first row
                    await _VM.GetEntity(loFirstRowParam);

                    // Display first row in conductor
                    if (_conductorRef != null)
                        await _conductorRef.R_GetEntity(loFirstRowParam);
                }
                else
                {
                    // If no data in grid, just refresh conductor with the redrafted record
                    if (_conductorRef != null)
                        await _conductorRef.R_GetEntity(loParam);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private async Task btnPrint_OnClick()
        {
            var loEx = new R_Exception();

            try
            {
                // TODO: Implement print button click handler
                // This typically opens a print dialog or popup
                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private async Task btnJournal_OnClick()
        {
            var loEx = new R_Exception();

            try
            {
                // TODO: Implement journal button click handler
                // This typically opens a journal form/page
                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        #endregion

        #region Asset List Tab Page Event Handlers

        /// <summary>
        /// Before opening Asset List tab - passes FAT00100DTO as parameter and sets target page type
        /// </summary>
        private void BeforeOpenAssetList(R_BeforeOpenTabPageEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                // Pass CurrentRecord DTO as parameter to the AssetList component
                eventArgs.Parameter = _VM?.CurrentRecord ?? new FAT00100DTO();
                eventArgs.TargetPageType = typeof(FAT00100AssetList);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        /// <summary>
        /// After opening Asset List tab - handles post-open logic
        /// </summary>
        private async Task AfterOpenAssetList(R_AfterOpenTabPageEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                // Refresh asset list if needed after tab is opened
                // Asset list should already be loaded from parent ViewModel
                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        /// <summary>
        /// Asset List tab event callback - handles callbacks from the AssetList component
        /// </summary>
        private void AssetListTabEventCallBack(object poParam)
        {
            var loEx = new R_Exception();
            try
            {
                // Handle callbacks from AssetList component if needed
                // For now, no specific callback handling required
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
