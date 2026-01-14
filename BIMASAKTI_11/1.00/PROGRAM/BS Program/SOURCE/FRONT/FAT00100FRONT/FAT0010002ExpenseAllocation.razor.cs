using BlazorClientHelper;
using FAT00100Common;
using FAT00100Common.DTOs;
using FAT00100FrontResources;
using FAT00100Model.VMs;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.Base;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_BlazorFrontEnd.Interfaces;
using R_CommonFrontBackAPI;
using R_BlazorFrontEnd.Controls.Tab;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;

namespace FAT00100Front
{
    /// <summary>
    /// Expense Allocation Component - Displays expense allocation grid
    /// Follows FAT00100AssetList pattern: implements R_ITabPage, receives DTO as parameter
    /// </summary>
    public partial class FAT0010002ExpenseAllocation : R_Page, R_ITabPage
    {
        [Inject] private IClientHelper ClientHelper { get; set; } = default!;
        [Inject] private R_ILocalizer<FAT00100FrontResources.Resources_Dummy_Class> Localizer { get; set; } = default!;

        // ViewModel from parent component (shared instance)
        // Can be set via Parameter (direct component usage) or CascadingParameter (R_TabPage usage)
        [Parameter] public FAT0010002ViewModel? VM { get; set; }
        [CascadingParameter(Name = "FAT0010002ViewModel")] public FAT0010002ViewModel? CascadingVM { get; set; }

        // Private field to store current DTO when received via R_Init_From_Master (R_TabPage)
        private FAT0010002DTO? _currentDTO;

        // Internal ViewModel reference (use Parameter VM, then CascadingParameter, otherwise create new)
        private FAT0010002ViewModel _VM => VM ?? CascadingVM ?? new FAT0010002ViewModel();

        // Batch ViewModel for expense allocation batch processing
        private readonly FAT0010002ExpenseAllocationBatchViewModel _batchViewModel = new FAT0010002ExpenseAllocationBatchViewModel();

        // Component references
        private R_Grid<FAT00100GetTransExpAllocListResultDTO>? _gridAllocExpense;
        private R_ConductorGrid? _conductorGridAllocExpenseRef;

        // State management
        private bool _isEditMode = false;
        private string _lastDepartmentCode = string.Empty;

        #region Display Properties (for razor binding)

        /// <summary>
        /// Get asset code for display - uses _currentDTO or _VM.Data
        /// </summary>
        private string GetAssetCode()
        {
            FAT0010002DTO? loData = _currentDTO ?? _VM?.Data;
            return loData?.CASSET_CODE ?? string.Empty;
        }

        /// <summary>
        /// Get asset name for display - uses _currentDTO or _VM.Data
        /// </summary>
        private string GetAssetName()
        {
            FAT0010002DTO? loData = _currentDTO ?? _VM?.Data;
            return loData?.CASSET_NAME ?? string.Empty;
        }

        /// <summary>
        /// Get asset department code for display - uses _currentDTO or _VM.Data
        /// </summary>
        private string GetAssetDeptCode()
        {
            FAT0010002DTO? loData = _currentDTO ?? _VM?.Data;
            return loData?.CASSET_DEPT_CODE ?? string.Empty;
        }

        /// <summary>
        /// Get asset department name for display - uses _currentDTO or _VM.Data
        /// </summary>
        private string GetAssetDeptName()
        {
            FAT0010002DTO? loData = _currentDTO ?? _VM?.Data;
            return loData?.CASSET_DEPT_NAME ?? string.Empty;
        }

        #endregion

        /// <summary>
        /// Initialize component - called when component is initialized from parent
        /// Follows FAT00100AssetList pattern: receives DTO as parameter
        /// </summary>
        protected override async Task R_Init_From_Master(object? poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                // Parameter is FAT0010002DTO passed from parent component (following AssetList pattern)
                if (poParameter is FAT0010002DTO loDTO)
                {
                    _currentDTO = loDTO;
                    
                    // Update ViewModel Data with the DTO using R_SetCurrentData (Data is read-only)
                    if (_VM != null)
                    {
                        _VM.R_SetCurrentData(loDTO);
                    }
                    
                    // Refresh grid to load data - use InvokeAsync to ensure it happens after rendering
                    await InvokeAsync(async () =>
                    {
                        if (_gridAllocExpense != null)
                        {
                            await _gridAllocExpense.R_RefreshGrid(null);
                        }
                    });
                }

                // Initialize batch ViewModel callbacks
                _batchViewModel.ShowErrorAction = (ex) =>
                {
                    var loException = new R_Exception();
                    if (ex?.ErrorList != null)
                    {
                        foreach (var loError in ex.ErrorList)
                        {
                            loException.Add(loError.ErrNo, loError.ErrDescp);
                        }
                    }
                    R_DisplayExceptionAsync(loException);
                };

                _batchViewModel.ShowSuccessAction = async () =>
                {
                    // After successful save, disable edit mode and refresh grid
                    _isEditMode = false;
                    _lastDepartmentCode = string.Empty;

                    // Refresh grid to show saved data
                    if (_gridAllocExpense != null)
                    {
                        await _gridAllocExpense.R_RefreshGrid(null);
                    }
                };

                _batchViewModel.StateChangeAction = () =>
                {
                    // Trigger UI update
                    InvokeAsync(StateHasChanged);
                };
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        

        #region Grid Data Loading

        /// <summary>
        /// Grid service handler to get list of expense allocation records
        /// NET4: gvAllocExpense_R_ServiceGetListRecord (lines 1920-1972)
        /// </summary>
        private async Task Grid_R_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                // Get parameters from ViewModel and current asset data
                // Use _currentDTO if available (from R_Init_From_Master), otherwise use _VM.Data
                FAT0010002DTO? loCurrentData = _currentDTO ?? _VM.Data;
                
                string lcCompanyId = ClientHelper.CompanyId;
                string lcLangId = ClientHelper.CultureUI?.TwoLetterISOLanguageName ?? "en";
                string lcDeptCode = loCurrentData?.CDEPT_CODE??"";
                string lcTransactionCode = _VM.TransactionCode;
                string lcReferenceNo = _VM.ReferenceNo;
                string lcAssetCode = loCurrentData?.CASSET_CODE ?? string.Empty;
                string lcAssetTransSeqNo = loCurrentData?.CASSET_TRANS_SEQNO ?? "";
                string lcParentId = loCurrentData?.CREC_ID ?? "";

                // Call ViewModel method to get expense allocation list (streaming method)
                await _VM.FAT00100GetTransExpAllocListAsync(
                    lcCompanyId,
                    lcLangId,
                    lcParentId,
                    lcDeptCode,
                    lcTransactionCode,
                    lcReferenceNo,
                    lcAssetCode,
                    lcAssetTransSeqNo
                );

                // Set the result from ViewModel
                eventArgs.ListEntityResult = _VM.TransExpAllocList ?? new ObservableCollection<FAT00100GetTransExpAllocListResultDTO>();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        /// <summary>
        /// Grid service handler to get single record (for R_ConductorGrid)
        /// NET4: gvAllocExpense_R_ServiceGetRecord (line 2142-2144)
        /// </summary>
        private void GridAllocExpense_R_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                // Return the entity as-is (no additional processing needed)
                eventArgs.Result = eventArgs.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        /// <summary>
        /// Grid after add handler - called when a new row is added to the batch grid
        /// Follows SAB02400 pattern
        /// </summary>
        private void GridAllocExpense_R_AfterAdd(R_AfterAddEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loData = (FAT00100GetTransExpAllocListResultDTO)eventArgs.Data;

                if (loData != null)
                {
                    // Initialize new row with default values if needed
                    // NET4: May set default values for new rows
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        /// <summary>
        /// Grid before save batch handler - validates before batch save
        /// Follows SAB02400 pattern
        /// </summary>
        private void GridAllocExpense_R_BeforeSaveBatch(R_BeforeSaveBatchEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loData = (List<FAT00100GetTransExpAllocListResultDTO>)eventArgs.Data;

                // Validate grid has data
                // NET4: If loBigObject.Count = 0 Then loEx.Add(R_Utility.R_GetError(GetType(Resources_Dummy_Class), "PS018"))
                if (loData == null || loData.Count == 0)
                {
                    loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "PS018"));
                    eventArgs.Cancel = true;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                eventArgs.Cancel = true;
            }

            loEx.ThrowExceptionIfErrors();
        }

        /// <summary>
        /// Grid service save batch handler - performs the actual batch save
        /// Follows SAB02400 pattern, uses existing batch ViewModel
        /// </summary>
        private async Task GridAllocExpense_R_ServiceSaveBatch(R_ServiceSaveBatchEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                // Convert grid data to List<FAT0010002CommonDTO>
                // NET4: loBigObject = (From A As FAT0010002StreamDTO In bsGridAllocExpense.List Select New FAT0010002CommonDTO...)
                List<FAT00100GetTransExpAllocListResultDTO> loGridData = 
                    (List<FAT00100GetTransExpAllocListResultDTO>)eventArgs.Data;

                List<FAT0010002CommonDTO> loBigObject = loGridData
                    .Select(x => new FAT0010002CommonDTO
                    {
                        CEXPENSE_DEPT_CODE = x.CEXPENSE_DEPT_CODE,
                        NEXPENSE_PCT = x.NEXPENSE_PCT
                    })
                    .ToList();

                // Create batch parameter
                // NET4: Sets CDEPT_CODE, CTRANSACTION_CODE, CREFERENCE_NO, CASSET_CODE, CASSET_TRANS_SEQNO
                var loParameter = new R_SaveBatchParameterDTO
                {
                    CCOMPANY_ID = ClientHelper.CompanyId,
                    CUSER_ID = ClientHelper.UserId,
                    UserParameters = new R_SaveBatchUserParameterDTO
                    {
                        CDEPT_CODE = _VM.TransDetailData.CDEPT_CODE,
                        CREF_NO = _VM.TransDetailData.CREF_NO,
                        CASSET_CODE = _VM.Data?.CASSET_CODE,
                        CTRANS_SEQ_NO = _VM.Data.CTRANS_SEQNO,
                        CPARENT_ID = _VM.Data.CREC_ID,
                        CPROPERTY_ID = _VM.Data.CPROPERTY_ID,
                        CTRANSACTION_CODE= _VM.DEFAULT_TRANSACTION_CODE
                    },
                    Data = loBigObject
                };

                // Call batch save using existing batch ViewModel
                string lcLangId = ClientHelper.CultureUI?.TwoLetterISOLanguageName ?? "en";
                await _batchViewModel.R_SaveBatchAsync(loParameter, lcLangId);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        /// <summary>
        /// Grid after save batch handler - called after batch save completes
        /// Follows SAB02400 pattern
        /// </summary>
        private async Task GridAllocExpense_R_AfterSaveBatch(R_AfterSaveBatchEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                // Disable edit mode after successful save
                _isEditMode = false;
                _lastDepartmentCode = string.Empty;

                // Refresh grid to reload data
                if (_gridAllocExpense != null)
                {
                    await _gridAllocExpense.R_RefreshGrid(null);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        #endregion

        #region Button Handlers

        /// <summary>
        /// Edit button click handler
        /// NET4: btnEditAllocExpense_Click (lines 1974-1979)
        /// </summary>
        private async Task btnEditAllocExpense_OnClick()
        {
            var loEx = new R_Exception();

            try
            {
                _isEditMode = true;
                _lastDepartmentCode = string.Empty;
                
                // Notify Blazor that state has changed so grid can update
                await InvokeAsync(StateHasChanged);
                
                // Force grid to update its UI state by calling a refresh
                // This ensures AllowAddNewRow binding change is applied
                if (_gridAllocExpense != null)
                {
                    // Use a small delay to ensure state change is processed
                    await Task.Delay(50);
                    // Refresh grid to apply AllowAddNewRow change
                    await _gridAllocExpense.R_RefreshGrid(null);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        /// <summary>
        /// Cancel button click handler
        /// NET4: btnCancelAllocExpense_Click (lines 1981-1987)
        /// </summary>
        private async Task btnCancelAllocExpense_OnClick()
        {
            var loEx = new R_Exception();

            try
            {
                _isEditMode = false;
                _lastDepartmentCode = string.Empty;

                // Refresh grid to discard changes
                if (_gridAllocExpense != null)
                {
                    await _gridAllocExpense.R_RefreshGrid(null);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        /// <summary>
        /// Save button click handler
        /// NET4: btnSaveAllocExpense_Click (lines 2056-2140)
        /// For batch grid, calls R_SaveBatch() which triggers batch event handlers
        /// Follows SAB02400 pattern
        /// </summary>
        private async Task btnSaveAllocExpense_OnClick()
        {
            var loEx = new R_Exception();

            try
            {
                // For batch grid, call R_SaveBatch() which will trigger:
                // R_BeforeSaveBatch -> R_ServiceSaveBatch -> R_AfterSaveBatch
                if (_gridAllocExpense != null)
                {
                    await _gridAllocExpense.R_SaveBatch();
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                // On error, re-enable edit mode (NET4: lines 2135-2138)
                _isEditMode = true;
            }

            loEx.ThrowExceptionIfErrors();
        }

        #endregion

        #region Department Lookup Handlers

        /// <summary>
        /// Department lookup - before open (for grid lookup column)
        /// NET4: gvAllocExpense_R_Before_Open_LookUpForm (lines 1858-1878)
        /// </summary>
        private void GridDepartmentLookup_R_Before_Open_Lookup(R_BeforeOpenGridLookupColumnEventArgs eventArgs)
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

        /// <summary>
        /// Department lookup - after open (for grid lookup column)
        /// NET4: gvAllocExpense_R_Return_LookUp (lines 1904-1918)
        /// </summary>
        private void GridDepartmentLookup_R_After_Open_Lookup(R_AfterOpenGridLookupColumnEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {

                var loTempResult = (GSL00700DTO)eventArgs.Result;
                var loGridRow = (FAT00100GetTransExpAllocListResultDTO)eventArgs.ColumnData;
                if (loTempResult != null && loGridRow != null)
                {
                    _VM.Data.CASSET_DEPT_CODE = loTempResult.CDEPT_CODE;
                    _VM.Data.CASSET_DEPT_NAME = loTempResult.CDEPT_NAME;

                    loGridRow.CEXPENSE_DEPT_CODE = loTempResult.CDEPT_CODE;
                    loGridRow.CEXPENSE_DEPT_NAME = loTempResult.CDEPT_NAME;
                }

                
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        #endregion

        #region Grid Cell Event Handlers

        /// <summary>
        /// Grid cell value changed - validates department code when entered manually
        /// NET4: gvAllocExpense_CellValueChanged (lines 1920-1942)
        /// </summary>
        private void GridAllocExpense_R_CellValueChanged(R_CellValueChangedEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                // Only handle department code column changes
                if (eventArgs.ColumnName == nameof(FAT0010002GetFAAcquisitionDetailAllocExpenPageListResultDTO.CEXPENSE_DEPT_CODE))
                {
                    var loGridRow = (FAT0010002GetFAAcquisitionDetailAllocExpenPageListResultDTO)eventArgs.CurrentRow;
                    
                    if (loGridRow != null)
                    {
                        // Store the department code for validation
                        _lastDepartmentCode = loGridRow.CEXPENSE_DEPT_CODE ?? string.Empty;
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
        /// Grid cell lost focused - performs lookup when department code is entered manually
        /// NET4: gvAllocExpense_CellValueChanged (lines 1920-1942) - validates and fills department name
        /// </summary>
        private async Task GridAllocExpense_R_CellLostFocused(R_CellLostFocusedEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                // Only handle department code column
                if (eventArgs.ColumnName == nameof(FAT0010002GetFAAcquisitionDetailAllocExpenPageListResultDTO.CEXPENSE_DEPT_CODE))
                {
                    var loGridRow = (FAT0010002GetFAAcquisitionDetailAllocExpenPageListResultDTO)eventArgs.CurrentRow;

                    if (loGridRow != null)
                    {
                        string lcDeptCode = loGridRow.CEXPENSE_DEPT_CODE?.Trim() ?? string.Empty;

                        if (string.IsNullOrWhiteSpace(lcDeptCode))
                        {
                            // Clear department name if code is empty
                            loGridRow.CEXPENSE_DEPT_NAME = string.Empty;
                            _lastDepartmentCode = string.Empty;
                        }
                        else if (lcDeptCode != _lastDepartmentCode)
                        {
                            // Department code changed - perform lookup to get department name
                            // NET4: Uses LookUpForm with GSL00500 sender flag to get department info
                            // In NET6: We need to call the lookup service or ViewModel method to get department name
                            
                            // TODO: Implement department lookup service call here
                            // For now, we'll leave the department name empty if not found via lookup
                            // The lookup should be handled via the R_GridLookupColumn's lookup mechanism
                            // This handler is for when user types the code manually instead of using lookup
                            
                            // Note: If department lookup ViewModel method exists, call it here
                            // Example:
                            // var loDeptResult = await _VM.GetDepartmentNameAsync(lcDeptCode);
                            // if (loDeptResult != null)
                            // {
                            //     loGridRow.CEXPENSE_DEPT_NAME = loDeptResult.CDEPT_NAME;
                            // }
                            
                            _lastDepartmentCode = lcDeptCode;
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

        #region Grid Validation Handlers

        /// <summary>
        /// Grid before delete validation
        /// NET4: gvAllocExpense_R_BeforeDelete (lines 1880-1884)
        /// Cannot delete row if CEXPENSE_DEPT_CODE equals Asset Department Code
        /// </summary>
        private void GridAllocExpense_R_BeforeDelete(R_BeforeDeleteEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loEntity = R_FrontUtility.ConvertObjectToObject<FAT0010002GetFAAcquisitionDetailAllocExpenPageListResultDTO>(eventArgs.Data);
                // Use _currentDTO if available, otherwise use _VM.Data
                FAT0010002DTO? loCurrentData = _currentDTO ?? _VM.Data;
                string lcAssetDeptCode = loCurrentData?.CASSET_DEPT_CODE ?? string.Empty;

                if (loEntity != null && loEntity.CEXPENSE_DEPT_CODE == lcAssetDeptCode)
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

        /// <summary>
        /// Grid before edit validation
        /// NET4: gvAllocExpense_R_BeforeEdit (lines 1886-1890)
        /// Cannot edit row if CEXPENSE_DEPT_CODE equals Asset Department Code
        /// </summary>
        private void GridAllocExpense_R_BeforeEdit(R_BeforeEditEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loEntity = R_FrontUtility.ConvertObjectToObject<FAT0010002GetFAAcquisitionDetailAllocExpenPageListResultDTO>(eventArgs.Data);
                // Use _currentDTO if available, otherwise use _VM.Data
                FAT0010002DTO? loCurrentData = _currentDTO ?? _VM.Data;
                string lcAssetDeptCode = loCurrentData?.CASSET_DEPT_CODE ?? string.Empty;

                if (loEntity != null && loEntity.CEXPENSE_DEPT_CODE == lcAssetDeptCode)
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

        #region R_ITabPage Implementation

        /// <summary>
        /// Refresh tab page - called when parent component refreshes this tab
        /// Follows FAT00100AssetList pattern: converts parameter, updates ViewModel, and refreshes grid
        /// </summary>
        public async Task RefreshTabPageAsync(object poParam)
        {
            var loEx = new R_Exception();

            try
            {
                // Convert parameter to FAT0010002DTO
                var loParam = R_FrontUtility.ConvertObjectToObject<FAT0010002DTO>(poParam);
                
                // Store current DTO
                _currentDTO = loParam;

                // Update ViewModel Data with the DTO using R_SetCurrentData (Data is read-only)
                if (_VM != null && loParam != null)
                {
                    _VM.R_SetCurrentData(loParam);
                }

                // Check if we have valid data (CASSET_CODE)
                bool llHasData = loParam != null && !string.IsNullOrWhiteSpace(loParam.CASSET_CODE);

                if (!llHasData)
                {
                    // No valid data - clear grid
                    if (_gridAllocExpense != null && _gridAllocExpense.DataSource != null && _gridAllocExpense.DataSource.Count > 0)
                    {
                        _gridAllocExpense.DataSource.Clear();
                    }
                    
                    // Also clear the ViewModel's AllocExpenPageList collection
                    if (_VM?.AllocExpenPageList != null && _VM.AllocExpenPageList.Count > 0)
                    {
                        _VM.AllocExpenPageList.Clear();
                    }
                }
                else
                {
                    // Has valid data - refresh grid to load expense allocation list
                    if (_gridAllocExpense != null)
                    {
                        await _gridAllocExpense.R_RefreshGrid(null);
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
    }
}

