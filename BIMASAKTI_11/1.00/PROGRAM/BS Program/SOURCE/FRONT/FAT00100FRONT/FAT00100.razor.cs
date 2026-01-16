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
using Lookup_GSFRONT;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSModel.ViewModel;

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

        protected override async Task R_Init_From_Master(object? poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                //ClientHelper.Set_CompanyId("BSI");
                //ClientHelper.Set_UserId("zf");
                // Extract parameters from poParameter if available
                string lcReferenceNo = string.Empty;
                string lcDeptCode = string.Empty;

                if (poParameter is FAT00100DTO loParameter)
                {
                    lcReferenceNo = loParameter.CREF_NO ?? string.Empty;
                    lcDeptCode = loParameter.CDEPT_CODE ?? string.Empty;
                }

                // Set department code
                if (!string.IsNullOrWhiteSpace(lcDeptCode))
                {
                    _VM.CurrentRecord.CDEPT_CODE = lcDeptCode;
                }

                // Call GetCompanyInfoAsync
                await _VM.GetCompanyInfoAsync(ClientHelper.CompanyId, ClientHelper.UserId, ClientHelper.CultureUI.TwoLetterISOLanguageName);

                // Call GetGetSystemParamAsync
                await _VM.GetGetSystemParamAsync(ClientHelper.CompanyId, ClientHelper.CultureUI.TwoLetterISOLanguageName);

                // Call GetPeriodeDtInfoAsync if SoftPeriod is available
                if (!string.IsNullOrEmpty(_VM.SoftPeriod) && _VM.SoftPeriod.Length >= 6)
                {
                    string lcYear = _VM.SoftPeriod.Substring(0, 4);
                    string lcPeriodNo = _VM.SoftPeriod.Substring(4, 2);
                    await _VM.GetPeriodeDtInfoAsync(ClientHelper.CompanyId, lcYear, lcPeriodNo);
                }

                // Call GetTransCodeInfoAsync
                await _VM.GetTransCodeInfoAsync(ClientHelper.CompanyId, FAT00100ViewModel.DEFAULT_TRANSACTION_CODE);

                // Call GetYearRangeAsync
                string lcCurrentYear = DateTime.Now.Year.ToString();
                await _VM.GetYearRangeAsync(ClientHelper.CompanyId, lcCurrentYear, string.Empty);

                // Call GetDeptLookupListAsync
                await _VM.GetDeptLookupListAsync(ClientHelper.CompanyId, ClientHelper.UserId, string.Empty);

                // Call GetStatusListAsync
                await _VM.GetStatusListAsync(ClientHelper.CompanyId, ClientHelper.CultureUI.TwoLetterISOLanguageName);

                // Call GetCurrencyListAsync
                await _VM.GetCurrencyListAsync(ClientHelper.CompanyId, ClientHelper.UserId);

                // Initialize period month combo
                _VM.SetComboPeriodMonthList();

                // Initialize period values from SoftPeriod if available, otherwise use current year
                if (!string.IsNullOrEmpty(_VM.SoftPeriod) && _VM.SoftPeriod.Length >= 6)
                {
                    _VM.PeriodFromYear = int.Parse(_VM.SoftPeriod.Substring(0, 4));
                    _VM.PeriodFromMonth = _VM.SoftPeriod.Substring(4, 2);
                    _VM.PeriodToYear = int.Parse(_VM.SoftPeriod.Substring(0, 4));
                    _VM.PeriodToMonth = _VM.SoftPeriod.Substring(4, 2);
                }
                else
                {
                    // Set default to current year
                    _VM.PeriodFromYear = DateTime.Now.Year;
                    _VM.PeriodToYear = DateTime.Now.Year;
                }
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
                eventArgs.Parameter = new GSL00700ParameterDTO();
                eventArgs.TargetPageType = typeof(GSL00700);
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

                var loTempResult = (GSL00700DTO)eventArgs.Result;
                if (loTempResult != null)
                {
                    _VM.PoDeptCode = loTempResult.CDEPT_CODE;
                    _VM.PoDeptName = loTempResult.CDEPT_NAME;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void btnDepartmentEntryLookup_R_Before_Open_Lookup(R_BeforeOpenLookupEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                eventArgs.Parameter = new GSL00700ParameterDTO();
                eventArgs.TargetPageType = typeof(GSL00700);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void btnDepartmentEntryLookup_R_After_Open_Lookup(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {

                var loTempResult = (GSL00700DTO)eventArgs.Result;
                if (loTempResult != null)
                {
                    _VM.Data.CDEPT_CODE = loTempResult.CDEPT_CODE;
                    _VM.Data.CDEPT_NAME = loTempResult.CDEPT_NAME;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task txtDepartmentCode_OnLostFocused()
        {
            R_Exception loEx = new();

            try
            {
                if (string.IsNullOrWhiteSpace(_VM.PoDeptCode))
                {
                    _VM.PoDeptCode = "";
                    _VM.PoDeptName = "";
                    return;
                }


                LookupGSL00700ViewModel loLookupViewModel = new();
                var param = new GSL00700ParameterDTO
                {
                    CCOMPANY_ID = ClientHelper.CompanyId,
                    CUSER_ID = ClientHelper.UserId,
                    CSEARCH_TEXT = _VM.PoDeptCode
                };
                var loResult = await loLookupViewModel.GetDepartment(param);

                if (loResult == null)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                        "_ErrLookup01"));
                    _VM.PoDeptCode = "";
                    _VM.PoDeptName = "";
                }
                else
                {
                    _VM.PoDeptCode = loResult.CDEPT_CODE;
                    _VM.PoDeptName = loResult.CDEPT_NAME;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private async Task txtDepartmentCodeEntry_OnLostFocused()
        {
            R_Exception loEx = new();

            try
            {
                FAT00100DTO loGetData = _VM.Data;

                if (string.IsNullOrWhiteSpace(loGetData.CDEPT_CODE))
                {
                    loGetData.CDEPT_CODE = "";
                    loGetData.CDEPT_NAME = "";
                    return;
                }

                LookupGSL00700ViewModel loLookupViewModel = new();
                var param = new GSL00700ParameterDTO
                {
                    CCOMPANY_ID = ClientHelper.CompanyId,
                    CUSER_ID = ClientHelper.UserId,
                    CSEARCH_TEXT = loGetData.CDEPT_CODE
                };
                var loResult = await loLookupViewModel.GetDepartment(param);

                if (loResult == null)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                        "_ErrLookup01"));
                    loGetData.CDEPT_CODE = "";
                    loGetData.CDEPT_NAME = "";
                }
                else
                {
                    loGetData.CDEPT_CODE = loResult.CDEPT_CODE;
                    loGetData.CDEPT_NAME = loResult.CDEPT_NAME;
                }
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
                eventArgs.Parameter = new GSL00700ParameterDTO();
                eventArgs.TargetPageType = typeof(GSL00700);
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

                var loTempResult = (GSL00700DTO)eventArgs.Result;
                if (loTempResult != null)
                {
                    _VM.Data.CFR_DEPT_CODE = loTempResult.CDEPT_CODE;
                    _VM.Data.CFR_DEPT_NAME = loTempResult.CDEPT_CODE;
                }
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

                GSL02900ParameterDTO loParam = new GSL02900ParameterDTO()
                {
                };
                eventArgs.Parameter = loParam;
                eventArgs.TargetPageType = typeof(GSL02900);
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

                GSL02900DTO loTempResult = (GSL02900DTO)eventArgs.Result;
                if (loTempResult == null)
                {
                    return;
                }
                _VM.PoSupplierId = loTempResult.CSUPPLIER_ID;
                _VM.PoSupplierName = loTempResult.CSUPPLIER_NAME;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void btnSupplierEntryLookup_R_Before_Open_Lookup(R_BeforeOpenLookupEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {

                GSL02900ParameterDTO loParam = new GSL02900ParameterDTO()
                {
                };
                eventArgs.Parameter = loParam;
                eventArgs.TargetPageType = typeof(GSL02900);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void btnSupplierEntryLookup_R_After_Open_Lookup(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {

                GSL02900DTO loTempResult = (GSL02900DTO)eventArgs.Result;
                if (loTempResult == null)
                {
                    return;
                }
                _VM.Data.CSUPPLIER_ID = loTempResult.CSUPPLIER_ID;
                _VM.Data.CSUPPLIER_NAME = loTempResult.CSUPPLIER_NAME;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task txtSupplierCode_OnLostFocused()
        {
            R_Exception loEx = new();

            try
            {
                if (string.IsNullOrWhiteSpace(_VM.PoSupplierId))
                {
                    _VM.PoSupplierId = "";
                    _VM.PoSupplierName = "";
                    return;
                }
                //GSL02900ParameterDTO

                LookupGSL02900ViewModel loLookupViewModel = new();
                var param = new GSL02900ParameterDTO
                {
                    CSEARCH_TEXT = _VM.PoSupplierId
                };
                var loResult = await loLookupViewModel.GetSupplier(param);

                if (loResult == null)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                        "_ErrLookup01"));
                    _VM.PoSupplierId = "";
                    _VM.PoSupplierName = "";
                }
                else
                {
                    _VM.PoSupplierId = loResult.CSUPPLIER_ID;
                    _VM.PoSupplierName = loResult.CSUPPLIER_NAME;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private async Task txtSupplierCodeEntry_OnLostFocused()
        {
            R_Exception loEx = new();

            try
            {
                FAT00100DTO loGetData = _VM.Data;

                if (string.IsNullOrWhiteSpace(loGetData.CSUPPLIER_ID))
                {
                    loGetData.CSUPPLIER_ID = "";
                    loGetData.CSUPPLIER_NAME = "";
                    return;
                }

                LookupGSL02900ViewModel loLookupViewModel = new();
                var param = new GSL02900ParameterDTO
                {
                    CSEARCH_TEXT = loGetData.CSUPPLIER_ID
                };
                var loResult = await loLookupViewModel.GetSupplier(param);

                if (loResult == null)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                        "_ErrLookup01"));
                    loGetData.CSUPPLIER_ID = "";
                    loGetData.CSUPPLIER_NAME = "";
                }
                else
                {
                    loGetData.CSUPPLIER_ID = loResult.CSUPPLIER_ID;
                    loGetData.CSUPPLIER_NAME = loResult.CSUPPLIER_NAME;
                }
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
                string lcPeriodFrom = $"{_VM.PeriodFromYear}{_VM.PeriodFromMonth.PadLeft(2, '0')}";
                string lcPeriodTo = $"{_VM.PeriodToYear}{_VM.PeriodToMonth.PadLeft(2, '0')}";

                // Convert selected status to backend parameters
                string lcStatusDraft = FAT00100ViewModel.STATUS_FLAG_DISABLED;
                string lcStatusOpen = FAT00100ViewModel.STATUS_FLAG_DISABLED;
                string lcStatusApproved = FAT00100ViewModel.STATUS_FLAG_DISABLED;
                string lcStatusClosed = FAT00100ViewModel.STATUS_FLAG_DISABLED;

                if (_VM.SelectedStatus == "ALL")
                {
                    lcStatusDraft = FAT00100ViewModel.STATUS_FLAG_ENABLED;
                    lcStatusOpen = FAT00100ViewModel.STATUS_FLAG_ENABLED;
                    lcStatusApproved = FAT00100ViewModel.STATUS_FLAG_ENABLED;
                    lcStatusClosed = FAT00100ViewModel.STATUS_FLAG_ENABLED;
                }
                else if (_VM.SelectedStatus == "DRAFT")
                {
                    lcStatusDraft = FAT00100ViewModel.STATUS_FLAG_ENABLED;
                }
                else if (_VM.SelectedStatus == "OPEN")
                {
                    lcStatusOpen = FAT00100ViewModel.STATUS_FLAG_ENABLED;
                }
                else if (_VM.SelectedStatus == "INPROGRESS")
                {
                    lcStatusOpen = FAT00100ViewModel.STATUS_FLAG_ENABLED;
                }
                else if (_VM.SelectedStatus == "APPROVED")
                {
                    lcStatusApproved = FAT00100ViewModel.STATUS_FLAG_ENABLED;
                }
                else if (_VM.SelectedStatus == "CLOSED")
                {
                    lcStatusClosed = FAT00100ViewModel.STATUS_FLAG_ENABLED;
                }

                // Set filter parameters in ViewModel
                _VM.FilterPeriodFrom = lcPeriodFrom;
                _VM.FilterPeriodTo = lcPeriodTo;
                _VM.FilterStatusDraft = lcStatusDraft;
                _VM.FilterStatusOpen = lcStatusOpen;
                _VM.FilterStatusApproved = lcStatusApproved;
                _VM.FilterStatusClosed = lcStatusClosed;
                _VM.FilterReferenceNo = string.Empty; // Can be set from CurrentRecord.CREF_NO if needed

                // Call GetDataGridAsync (reads parameters from ViewModel properties)
                await _VM.GetDataGridAsync();

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
                string lcPeriodFrom = $"{_VM.PeriodFromYear}{_VM.PeriodFromMonth.PadLeft(2, '0')}";
                string lcPeriodTo = $"{_VM.PeriodToYear}{_VM.PeriodToMonth.PadLeft(2, '0')}";

                // Convert selected status to backend parameters
                string lcStatusDraft = FAT00100ViewModel.STATUS_FLAG_DISABLED;
                string lcStatusOpen = FAT00100ViewModel.STATUS_FLAG_DISABLED;
                string lcStatusApproved = FAT00100ViewModel.STATUS_FLAG_DISABLED;
                string lcStatusClosed = FAT00100ViewModel.STATUS_FLAG_DISABLED;

                if (_VM.SelectedStatus == "ALL")
                {
                    lcStatusDraft = FAT00100ViewModel.STATUS_FLAG_ENABLED;
                    lcStatusOpen = FAT00100ViewModel.STATUS_FLAG_ENABLED;
                    lcStatusApproved = FAT00100ViewModel.STATUS_FLAG_ENABLED;
                    lcStatusClosed = FAT00100ViewModel.STATUS_FLAG_ENABLED;
                }
                else if (_VM.SelectedStatus == "DRAFT")
                {
                    lcStatusDraft = FAT00100ViewModel.STATUS_FLAG_ENABLED;
                }
                else if (_VM.SelectedStatus == "OPEN")
                {
                    lcStatusOpen = FAT00100ViewModel.STATUS_FLAG_ENABLED;
                }
                else if (_VM.SelectedStatus == "INPROGRESS")
                {
                    lcStatusOpen = FAT00100ViewModel.STATUS_FLAG_ENABLED;
                }
                else if (_VM.SelectedStatus == "APPROVED")
                {
                    lcStatusApproved = FAT00100ViewModel.STATUS_FLAG_ENABLED;
                }
                else if (_VM.SelectedStatus == "CLOSED")
                {
                    lcStatusClosed = FAT00100ViewModel.STATUS_FLAG_ENABLED;
                }

                // Set filter parameters in ViewModel
                _VM.FilterPeriodFrom = lcPeriodFrom;
                _VM.FilterPeriodTo = lcPeriodTo;
                _VM.FilterStatusDraft = lcStatusDraft;
                _VM.FilterStatusOpen = lcStatusOpen;
                _VM.FilterStatusApproved = lcStatusApproved;
                _VM.FilterStatusClosed = lcStatusClosed;
                _VM.FilterReferenceNo = string.Empty; // Can be set from CurrentRecord.CREF_NO if needed

                // Call GetDataGridAsync (reads parameters from ViewModel properties)
                await _VM.GetDataGridAsync();

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
                //_VM.Data.CTRANSACTION_NAME = loResult.cTransactionName?.ToString().Trim() ?? string.Empty;
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
                
                // Set currency rate codes from CompanyInfoData
                if (_VM.CompanyInfoData != null)
                {
                    _VM.Data.CLOCAL_CURRENCY_CODE = _VM.CompanyInfoData.CLOCAL_CURRENCY_CODE ?? string.Empty;
                    _VM.Data.CBASE_CURRENCY_CODE = _VM.CompanyInfoData.CBASE_CURRENCY_CODE ?? string.Empty;
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
                    _VM.Data.NLBASE_RATE = 0;
                    _VM.Data.NLCURRENCY_RATE = 0;
                    _VM.Data.NBBASE_RATE = 0;
                    _VM.Data.NBCURRENCY_RATE = 0;
                    _VM.Data.CLOCAL_CURRENCY_CODE = string.Empty;
                    _VM.Data.CBASE_CURRENCY_CODE = string.Empty;
                    return;
                }

                // Set currency rate codes from CompanyInfoData
                if (_VM.CompanyInfoData != null)
                {
                    _VM.Data.CLOCAL_CURRENCY_CODE = _VM.CompanyInfoData.CLOCAL_CURRENCY_CODE ?? string.Empty;
                    _VM.Data.CBASE_CURRENCY_CODE = _VM.CompanyInfoData.CBASE_CURRENCY_CODE ?? string.Empty;
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
                _VM.CurrentRecord.CFR_REF_NO = loResult.cReferenceNo?.ToString().Trim() ?? string.Empty;
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

                if (loGridRow != null && !string.IsNullOrWhiteSpace(loGridRow.CREF_NO))
                {
                    // Convert grid row to FAT00100DTO (following GSM02000 pattern)
                    var loParam = new FAT00100DTO
                    {
                        CCOMPANY_ID = ClientHelper.CompanyId,
                        CDEPT_CODE = loGridRow.CDEPT_CODE ?? string.Empty,
                        CREF_NO = loGridRow.CREF_NO,
                        CREC_ID=loGridRow.CREC_ID
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
                    if (_tabStripRef?.ActiveTab?.Id == nameof(FAT00100AssetList) && _tabPageAssetList != null)
                    {
                        var loTabPageAssetList = _tabPageAssetList;
                        var loTempParam = _conductorRef!.R_GetCurrentData();
                        FAT00100DTO loParam;
                        if (loTempParam is FAT00100DTO loDTO)
                        {
                            loParam = loDTO;
                        }
                        else if (loTempParam != null)
                        {
                            loParam = R_FrontUtility.ConvertObjectToObject<FAT00100DTO>(loTempParam) ?? new FAT00100DTO();
                        }
                        else
                        {
                            loParam = _VM?.CurrentRecord ?? new FAT00100DTO();
                        }

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
                var loEntity = (FAT00100DTO)eventArgs.Data;
                string lcFilterSupplierId = _VM.PoSupplierId;
                loEntity.CSUPPLIER_ID = string.Empty;
                loEntity.CSUPPLIER_NAME = string.Empty;
                loEntity.CDEPT_CODE = string.Empty;
                loEntity.CCURRENCY_CODE = string.Empty;
                loEntity.CCURRENCY_NAME = string.Empty;
                loEntity.CREF_NO = string.Empty;

                loEntity.DREF_DATE = DateTime.Now;
                loEntity.CREF_DATE = DateTime.Now.ToString("yyyyMMdd");

                // Set document date to transaction date
                loEntity.DDOCUMENT_DATE = loEntity.DREF_DATE;
                loEntity.CDOCUMENT_DATE = loEntity.CREF_DATE;
                loEntity.CCREATE_DATE = loEntity.CCREATE_DATE;
                loEntity.CUPDATE_DATE = loEntity.CUPDATE_DATE;
                loEntity.CSOURCE_MODULE = FAT00100ViewModel.DEFAULT_SOURCE_MODULE_FA;
                loEntity.CTRANSACTION_CODE = FAT00100ViewModel.DEFAULT_TRANSACTION_CODE;

                // Set default currency rate codes from CompanyInfoData
                if (_VM.CompanyInfoData != null)
                {
                    loEntity.CLOCAL_CURRENCY_CODE = _VM.CompanyInfoData.CLOCAL_CURRENCY_CODE ?? string.Empty;
                    loEntity.CBASE_CURRENCY_CODE = _VM.CompanyInfoData.CBASE_CURRENCY_CODE ?? string.Empty;
                }
                else
                {
                    loEntity.CLOCAL_CURRENCY_CODE = string.Empty;
                    loEntity.CBASE_CURRENCY_CODE = string.Empty;
                }
                loEntity.CREF_NO = string.Empty;
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
                loEntity.CCREATE_BY = ClientHelper.UserId;
                loEntity.CUPDATE_BY = ClientHelper.UserId;
                loEntity.CCOMPANY_ID = ClientHelper.CompanyId;
                loEntity.CTRANSACTION_CODE = _VM.CurrentRecord.CTRANSACTION_CODE; 
                if (loEntity.DREF_DATE != default)
                {
                    loEntity.CREF_DATE = loEntity.DREF_DATE.ToString("yyyyMMdd");
                }
                else
                {
                    loEntity.CREF_DATE = string.Empty;
                }

                if (loEntity.DDOCUMENT_DATE != default)
                {
                    loEntity.CDOCUMENT_DATE = loEntity.DDOCUMENT_DATE.ToString("yyyyMMdd");
                }
                else
                {
                    loEntity.CDOCUMENT_DATE = string.Empty;
                }

                // Handle FA/PJ Source Selection (equivalent to net4 lines 821-828)
                if (loEntity.CSOURCE_MODULE == FAT00100ViewModel.DEFAULT_SOURCE_MODULE_FA)
                {
                    // If FA: clear FR fields
                    loEntity.CFR_DEPT_CODE = string.Empty;
                    loEntity.CFR_DEPT_CODE = string.Empty;
                    loEntity.CFR_REF_NO = string.Empty;
                }
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
                var loParam = (FAT00100DTO)eventArgs.Data;
                loParam.CCOMPANY_ID = ClientHelper.CompanyId;
                loParam.CUSER_ID = ClientHelper.UserId;
                loParam.CLANG_ID = ClientHelper.CultureUI.TwoLetterISOLanguageName;
                await _VM.SaveRecordAsync(
                    loParam, 
                    (eCRUDMode)eventArgs.ConductorMode, 
                    ClientHelper.CompanyId, 
                    ClientHelper.CultureUI.TwoLetterISOLanguageName
                );
                eventArgs.Result = _VM.CurrentRecord;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task Conductor_AfterSave(R_AfterSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
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
                        loEntity = (FAT00100DTO)eventArgs.Data;
                    }
                    else
                    {
                        loEntity = _VM.CurrentRecord;
                    }
                }
                else if (eventArgs.Data != null && eventArgs.Data is FAT00100DTO)
                {
                    loEntity = (FAT00100DTO)eventArgs.Data;
                }
                else
                {
                    loEntity = _VM.CurrentRecord;
                }

                if (string.IsNullOrWhiteSpace(loEntity.CDEPT_CODE))
                {
                    loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "Val_department"));
                }

                // Add mode: Validate transaction number when LINCREMENT_FLAG is false
                if (eventArgs.ConductorMode == R_eConductorMode.Add)
                {
                    // Use IncrementFlag property (equivalent to plIncrementFlag in net4)
                    bool llIncrementFlag = _VM.TransCodeInfoData.LINCREMENT_FLAG;
                    if (!llIncrementFlag)
                    {
                        if (string.IsNullOrWhiteSpace(loEntity.CREF_NO))
                        {
                            loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "PS009"));
                        }
                    }
                }

                // Validate transaction date
                if (loEntity.DREF_DATE == null)
                {
                    loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "PS006"));
                }
                if (string.IsNullOrWhiteSpace(loEntity.CSUPPLIER_ID))
                {
                    // Note: Supplier validation - check net4 for correct error code if different
                    loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "PS010"));
                }

                // Validate currency
                if (string.IsNullOrWhiteSpace(loEntity.CCURRENCY_CODE))
                {
                    loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "PS007"));
                }
                if (loEntity.CSOURCE_MODULE == FAT00100ViewModel.DEFAULT_SOURCE_MODULE_PJ)
                {
                    if (string.IsNullOrWhiteSpace(loEntity.CFR_DEPT_CODE) || string.IsNullOrWhiteSpace(loEntity.CFR_REF_NO))
                    {
                        loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "PS008"));
                    }
                }

                string lcDocumentDate = string.Empty;
                string lcTransactionDate = string.Empty;

                if (loEntity.DDOCUMENT_DATE != default)
                {
                    lcDocumentDate = loEntity.DDOCUMENT_DATE.ToString("yyyyMMdd");
                }
                else
                {
                    lcDocumentDate = !string.IsNullOrWhiteSpace(loEntity.CDOCUMENT_DATE) ? loEntity.CDOCUMENT_DATE : string.Empty;
                }

                if (loEntity.DREF_DATE != default)
                {
                    lcTransactionDate = loEntity.DREF_DATE.ToString("yyyyMMdd");
                }
                else
                {
                    lcTransactionDate = !string.IsNullOrWhiteSpace(loEntity.CREF_DATE) ? loEntity.CREF_DATE : string.Empty;
                }

                if (!string.IsNullOrEmpty(lcDocumentDate) && !string.IsNullOrEmpty(lcTransactionDate))
                {
                    if (string.Compare(lcDocumentDate, lcTransactionDate) > 0)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "PS013"));
                    }
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
                if (string.IsNullOrWhiteSpace(_VM.Data.CTRANS_DESC))
                {
                    loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "Val_Description"));
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
                // MANDATORY: Update CSOURCE_MODULE property manually when using ValueChanged (cannot use @bind-Value)
                _VM.Data.CSOURCE_MODULE = pcNewValue;

                // Check if conductor is in Add or Edit mode
                if (_conductorRef != null)
                {
                    var leMode = _conductorRef.R_ConductorMode;
                    bool llIsAddOrEdit = (leMode == R_eConductorMode.Add || leMode == R_eConductorMode.Edit);
                    //bool llChangeDesc = _VM.Data.LCHANGE_DESC;

                    if (llIsAddOrEdit)
                    {
                        if (pcNewValue == FAT00100ViewModel.DEFAULT_SOURCE_MODULE_PJ)
                        {
                            //// PJ selected - equivalent to rdbPJ.CheckStateChanged in net4
                            //// SourceOptional() logic: Enable PJ fields, disable/clear FA fields
                            //if (!llChangeDesc)
                            //{
                            //    // Set PJ transaction code and name (equivalent to txtTransactionTypeCode.Text = "420010" in net4)
                            //    _VM.Data.CFR_TRANSACTION_CODE = FAT00100ViewModel.DEFAULT_PJ_TRANSACTION_CODE;
                            //    _VM.Data.CFR_TRANSACTION_NAME = _VM.PJTransDesc;
                            //}
                            //else
                            //{
                            //    // When LCHANGE_DESC is true, clear transaction code
                            //    _VM.Data.CFR_TRANSACTION_CODE = string.Empty;
                            //    _VM.Data.CFR_TRANSACTION_NAME = string.Empty;
                            //}

                            //// Clear FA-related fields (equivalent to disabling them in net4)
                            //// Supplier fields
                            //_VM.Data.CSUPPLIER_ID = string.Empty;
                            //_VM.Data.CSUPPLIER_NAME = string.Empty;
                            //_VM.PoSupplierId = string.Empty;
                            //_VM.PoSupplierName = string.Empty;
                            //_VM.Data.CINFO_SEQNO = string.Empty;
                            
                            //// Document fields
                            //_VM.Data.CDOCUMENT_NO = string.Empty;
                            //_VM.Data.CDOCUMENT_DATE = string.Empty;
                            //_VM.Data.DDOCUMENT_DATE = null;
                            
                            //// Currency fields
                            //_VM.Data.CCURRENCY_CODE = string.Empty;
                            //_VM.Data.CCURRENCY_NAME = string.Empty;
                            //_VM.Data.NLBASE_RATE_AMOUNT = 0;
                            //_VM.Data.NLCURRENCY_RATE_AMOUNT = 0;
                            //_VM.Data.NBBASE_RATE_AMOUNT = 0;
                            //_VM.Data.NBCURRENCY_RATE_AMOUNT = 0;
                            
                            //// Clear nested DTOs
                            //_VM.Data.oCP = new List<FAT00100CPDTO>();
                            //_VM.Data.oSupp = null;
                            //_VM.ContactPersonList.Clear();
                            //_VM.SupplierInfo = new FAT00100GetGSM_SUPPLIER_INFOResultDTO();
                        }
                        else if (pcNewValue == FAT00100ViewModel.DEFAULT_SOURCE_MODULE_FA)
                        {
                            // FA selected - equivalent to rdbFA.CheckStateChanged in net4
                            // SourceOptional() logic: Enable FA fields, disable/clear PJ fields
                            
                            // Clear PJ transaction code and name (equivalent to txtTransactionTypeCode.Text = "" in net4)
                            _VM.Data.CFR_TRANS_CODE = string.Empty;
                            _VM.Data.CFR_TRANS_NAME = string.Empty;
                            
                            // Clear PJ-related fields
                            _VM.Data.CFR_DEPT_CODE = string.Empty;
                            _VM.Data.CFR_DEPT_NAME = string.Empty;
                            _VM.Data.CFR_REF_NO = string.Empty;
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

        private async Task _valueChangedCurrency(string value)
        {
            var loEx = new R_Exception();
            try
            {

                if (!string.IsNullOrEmpty(value))
                {
                    if (value == _VM.Data.CCURRENCY_CODE) return;


                    _VM.Data.CCURRENCY_CODE = value;
                    _VM.CurrencyListtemp= R_FrontUtility.ConvertCollectionToCollection<FAT00100GetCurrencyListResultDTO>(_VM.CurrencyList).ToList();
                    _VM.Data.CCURRENCY_NAME = _VM.CurrencyListtemp.Find(x => x.CCURRENCY_CODE == value)?.CCURRENCY_NAME ?? string.Empty;
                    string crefDate = _VM.Data.DREF_DATE.ToString("yyyyMMdd");
                    await _VM.FAT00100GetLastCurrencyRateAsync(ClientHelper.CompanyId, value, crefDate);
                    if (_VM.LastCurrencyRateData != null)
                    {
                        _VM.Data.NLBASE_RATE = _VM.LastCurrencyRateData.NLBASE_RATE_AMOUNT;
                        _VM.Data.NLCURRENCY_RATE = _VM.LastCurrencyRateData.NLCURRENCY_RATE_AMOUNT;
                        _VM.Data.NBBASE_RATE = _VM.LastCurrencyRateData.NBBASE_RATE_AMOUNT;
                        _VM.Data.NBCURRENCY_RATE = _VM.LastCurrencyRateData.NBCURRENCY_RATE_AMOUNT;
                    }
                    else
                    {
                        _VM.Data.NLBASE_RATE = 1;
                        _VM.Data.NLCURRENCY_RATE = 1;
                        _VM.Data.NBBASE_RATE = 1;
                        _VM.Data.NBCURRENCY_RATE = 1;
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

                if (loEntity == null || string.IsNullOrWhiteSpace(loEntity.CREF_NO))
                {
                    loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "PS009"));
                    loEx.ThrowExceptionIfErrors();
                    return;
                }

                // Create parameter DTO for FAT0010002
                var loParam = new FAT0010002DTO
                {
                    CDEPT_CODE = loEntity.CDEPT_CODE ?? string.Empty,
                    CTRANSACTION_CODE = loEntity.CSOURCE_MODULE == "FA" ? "200010" : "420010",
                    CREF_NO = loEntity.CREF_NO,
                    CSTATUS = loEntity.CTRANS_STATUS ?? string.Empty,
                    CMODE = "T", // T=Transaction, V=View
                    CLOCAL_CURRENCY_CODE = _VM.CompanyInfoData?.CLOCAL_CURRENCY_CODE ?? string.Empty,
                    CBASE_CURRENCY_CODE = _VM.CompanyInfoData?.CBASE_CURRENCY_CODE ?? string.Empty,
                    LASSET_INCREMENT_FLAG = _VM.SystemParamData.LINCREMENT_FLAG,
                    LJRNGRP_MODE = _VM.JrngrpMode,
                    LDEPT_MODE = _VM.DeptMode,
                    CASSET_DEPT_CODE = _VM.DefaultAssetDeptCode ?? string.Empty,
                    LGLLINK = _VM.GLLink,
                    CGLLINK_DATE = _VM.GlinkDate ?? string.Empty,
                    CREC_ID = loEntity.CREC_ID ?? string.Empty,
                    CSOFT_PERIOD = _VM.SystemParamData.CSOFT_PERIOD ?? string.Empty
                };

                // Create popup settings with large size
                var loPopupSettings = new R_PopupSettings
                {
                    PageTitle = Localizer["_Detail"],
                    WithLock = true,
                    Page = this,
                    Width = "100%"
                };

                // tutup 
                var loResult = await PopupService.Show(typeof(FAT0010002), loParam, poPopupSettings: loPopupSettings);
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
                            CREF_NO = _VM.CurrentRecord.CREF_NO
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
                if (string.IsNullOrWhiteSpace(loEntity.CREF_NO))
                {
                    loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "PS009"));
                    R_DisplayException(loEx);
                    return;
                }

                // Validation before submit (only for Draft status "00") - equivalent to net4 lines 2265-2272
                if (loEntity.CTRANS_STATUS == FAT00100ViewModel.DEFAULT_STATUS_DRAFT)
                {
                    var loValidationResult = await _VM.ValidationBeforeSubmitAsync(
                        ClientHelper.CompanyId,
                        loEntity.CDEPT_CODE,
                        loEntity.CTRANSACTION_CODE,
                        loEntity.CREF_NO
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
                string lcMessageKey = loEntity.CTRANS_STATUS == FAT00100ViewModel.DEFAULT_STATUS_DRAFT ? "_msgSubmit" : "_msgDraft";
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
                // ViewModel uses CurrentRecord for CDEPT_CODE, CTRANSACTION_CODE, CREF_NO
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
                    CREF_NO = loEntity.CREF_NO
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
                        CREF_NO = loFirstRow.CREF_NO
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
                if (string.IsNullOrWhiteSpace(loEntity.CREF_NO))
                {
                    loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "PS009"));
                    R_DisplayException(loEx);
                    return;
                }

                // Validate status is Open (01) - redraft only works from Open status
                if (loEntity.CTRANS_STATUS != "10")
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
                // ViewModel uses CurrentRecord for CDEPT_CODE, CTRANSACTION_CODE, CREF_NO
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
                    CREF_NO = loEntity.CREF_NO
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
                        CREF_NO = loFirstRow.CREF_NO
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
                // tutup  Pass CurrentRecord DTO as parameter to the AssetList component
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
