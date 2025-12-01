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

        // Period filter properties
        public int PeriodFromYear { get; set; } = 2023;
        public string PeriodFromMonth { get; set; } = "01";
        public int PeriodToYear { get; set; } = 2025;
        public string PeriodToMonth { get; set; } = "12";

        // Status filter property
        public string SelectedStatus { get; set; } = "ALL";

        [Inject] private IClientHelper ClientHelper { get; set; } = default!;
        [Inject] private R_ILocalizer<FAT00100FrontResources.Resources_Dummy_Class> Localizer { get; set; } = default!;

        // Source radio button list for FA/PJ
        public class SourceOptionDTO
        {
            public string CVALUE { get; set; } = string.Empty;
            public string CDESCRIPTION { get; set; } = string.Empty;
        }

        private List<SourceOptionDTO> _sourceList = new List<SourceOptionDTO>
        {
            new SourceOptionDTO { CVALUE = "FA", CDESCRIPTION = "FA" },
            new SourceOptionDTO { CVALUE = "PJ", CDESCRIPTION = "PJ" }
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
                    "420010", // CPJ_TRANS_CODE (hardcoded as per VB.NET)
                    "200010"  // CTRANSACTION_CODE (hardcoded as per VB.NET)
                );

                // Update CurrentRecord with transaction code and description from initial process
                if (_VM.InitialProcessData != null)
                {
                    _VM.CurrentRecord.CTRANSACTION_CODE = "200010";
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
                _VM.CurrentRecord.CDEPT_CODE = loResult.cDeptCode?.ToString().Trim() ?? string.Empty;
                _VM.CurrentRecord.CDEPT_NAME = loResult.cDeptDesc?.ToString().Trim() ?? string.Empty;
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
                if (string.IsNullOrWhiteSpace(_VM.CurrentRecord.CDEPT_CODE))
                {
                    _VM.CurrentRecord.CDEPT_NAME = string.Empty;
                    return;
                }

                // Validate department code using ViewModel method
                var liResult = await _VM.ValidateDeptCodeAsync(ClientHelper.CompanyId, _VM.CurrentRecord.CDEPT_CODE, ClientHelper.UserId);
                
                if (liResult == 0)
                {
                    // Department not found - clear fields
                    loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "PS001"));
                    _VM.CurrentRecord.CDEPT_CODE = string.Empty;
                    _VM.CurrentRecord.CDEPT_NAME = string.Empty;
                }
                else
                {
                    // TODO: Get department description if ViewModel method exists
                    // For now, description will be populated from lookup
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
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
                _VM.CurrentRecord.CSUPPLIER_ID = loResult.cSupplierId?.ToString().Trim() ?? string.Empty;
                _VM.CurrentRecord.CSUPPLIER_NAME = loResult.cSupplierName?.ToString().Trim() ?? string.Empty;
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
                if (string.IsNullOrWhiteSpace(_VM.CurrentRecord.CSUPPLIER_ID))
                {
                    _VM.CurrentRecord.CSUPPLIER_NAME = string.Empty;
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
                string lcStatusDraft = "0";
                string lcStatusOpen = "0";
                string lcStatusApproved = "0";
                string lcStatusClosed = "0";

                if (SelectedStatus == "ALL")
                {
                    lcStatusDraft = "1";
                    lcStatusOpen = "1";
                    lcStatusApproved = "1";
                    lcStatusClosed = "1";
                }
                else if (SelectedStatus == "DRAFT")
                {
                    lcStatusDraft = "1";
                }
                else if (SelectedStatus == "OPEN")
                {
                    lcStatusOpen = "1";
                }
                else if (SelectedStatus == "INPROGRESS")
                {
                    lcStatusOpen = "1";
                }
                else if (SelectedStatus == "APPROVED")
                {
                    lcStatusApproved = "1";
                }
                else if (SelectedStatus == "CLOSED")
                {
                    lcStatusClosed = "1";
                }

                // Call GetDataGridAsync with filter parameters
                await _VM.GetDataGridAsync(
                    ClientHelper.CompanyId,
                    ClientHelper.CultureUI.TwoLetterISOLanguageName,
                    _VM.CurrentRecord.CDEPT_CODE,
                    _VM.CurrentRecord.CTRANSACTION_CODE,
                    _VM.CurrentRecord.CREFERENCE_NO,
                    _VM.CurrentRecord.CSUPPLIER_ID,
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
                string lcStatusDraft = "0";
                string lcStatusOpen = "0";
                string lcStatusApproved = "0";
                string lcStatusClosed = "0";

                if (SelectedStatus == "ALL")
                {
                    lcStatusDraft = "1";
                    lcStatusOpen = "1";
                    lcStatusApproved = "1";
                    lcStatusClosed = "1";
                }
                else if (SelectedStatus == "DRAFT")
                {
                    lcStatusDraft = "1";
                }
                else if (SelectedStatus == "OPEN")
                {
                    lcStatusOpen = "1";
                }
                else if (SelectedStatus == "INPROGRESS")
                {
                    lcStatusOpen = "1";
                }
                else if (SelectedStatus == "APPROVED")
                {
                    lcStatusApproved = "1";
                }
                else if (SelectedStatus == "CLOSED")
                {
                    lcStatusClosed = "1";
                }

                // Call GetDataGridAsync with filter parameters
                await _VM.GetDataGridAsync(
                    ClientHelper.CompanyId,
                    ClientHelper.CultureUI.TwoLetterISOLanguageName,
                    _VM.CurrentRecord.CDEPT_CODE,
                    _VM.CurrentRecord.CTRANSACTION_CODE,
                    _VM.CurrentRecord.CREFERENCE_NO,
                    _VM.CurrentRecord.CSUPPLIER_ID,
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
                _VM.CurrentRecord.CTRANSACTION_CODE = loResult.cTransactionCode?.ToString().Trim() ?? string.Empty;
                _VM.CurrentRecord.CTRANSACTION_NAME = loResult.cTransactionName?.ToString().Trim() ?? string.Empty;
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
                _VM.CurrentRecord.CCURRENCY_CODE = loResult.cCurrencyCode?.ToString().Trim() ?? string.Empty;
                _VM.CurrentRecord.CCURRENCY_NAME = loResult.cCurrencyName?.ToString().Trim() ?? string.Empty;
                
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
                if (string.IsNullOrWhiteSpace(_VM.CurrentRecord.CCURRENCY_CODE))
                {
                    _VM.CurrentRecord.CCURRENCY_NAME = string.Empty;
                    _VM.CurrentRecord.NLBASE_RATE_AMOUNT = 0;
                    _VM.CurrentRecord.NLCURRENCY_RATE_AMOUNT = 0;
                    return;
                }

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
                        CFILTER_TRANS_CODE = "200010", // CFILTER_TRANS_CODE (hardcoded as per initialization)
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

        private void Conductor_R_Display(R_DisplayEventArgs eventArgs)
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
                // Prepare DTO from CurrentRecord
                var loParam = R_FrontUtility.ConvertObjectToObject<FAT00100DTO>(eventArgs.Data);
                
                // Set CompanyId, UserId, LangId from ClientHelper
                loParam.CCOMPANY_ID = ClientHelper.CompanyId;
                loParam.CUSER_ID = ClientHelper.UserId;
                loParam.CLANG_ID = ClientHelper.CultureUI.TwoLetterISOLanguageName;

                // Call ViewModel save method
                // TODO: Replace with actual ViewModel method when available
                // await _VM.R_ServiceSaveAsync(loParam, (eCRUDMode)eventArgs.ConductorMode);
                
                // Set result
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
                // Prepare DTO from CurrentRecord with key fields
                var loParam = R_FrontUtility.ConvertObjectToObject<FAT00100DTO>(eventArgs.Data);
                
                // Set CompanyId, UserId, LangId from ClientHelper
                loParam.CCOMPANY_ID = ClientHelper.CompanyId;
                loParam.CUSER_ID = ClientHelper.UserId;
                loParam.CLANG_ID = ClientHelper.CultureUI.TwoLetterISOLanguageName;

                // Call ViewModel delete method
                // TODO: Replace with actual ViewModel method when available
                // await _VM.R_ServiceDeleteAsync(loParam);

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
                // Validate required fields
                if (string.IsNullOrWhiteSpace(_VM.CurrentRecord.CTRANSACTION_DATE) && _VM.CurrentRecord.DTRANSACTION_DATE == null)
                {
                    loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "PS002"));
                }
                if (string.IsNullOrWhiteSpace(_VM.CurrentRecord.CDEPT_CODE))
                {
                    loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "PS003"));
                }
                if (string.IsNullOrWhiteSpace(_VM.CurrentRecord.CSUPPLIER_ID))
                {
                    loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "PS004"));
                }
                if (string.IsNullOrWhiteSpace(_VM.CurrentRecord.CCURRENCY_CODE))
                {
                    loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "PS005"));
                }

                // TODO: Call ViewModel validation method if available

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

        #endregion
    }
}
