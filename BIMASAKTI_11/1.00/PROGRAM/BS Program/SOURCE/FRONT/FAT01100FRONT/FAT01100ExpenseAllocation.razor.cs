using BlazorClientHelper;
using FAT01100Common;
using FAT01100Common.DTOs;
using FAT01100FrontResources;
using FAT01100Model.VMs;
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
using FAT01100Model;
using R_BlazorFrontEnd.Controls.Enums;
using R_BlazorFrontEnd.Controls.MessageBox;

namespace FAT01100Front
{
    public partial class FAT01100ExpenseAllocation : R_Page, R_ITabPage

    {
        #region Dependency Injection
        [Inject] private IClientHelper ClientHelper { get; set; } = default!;
        [Inject] private R_ILocalizer<FAT01100FrontResources.Resources_Dummy_Class> Localizer { get; set; } = default!;
        #endregion
        private FAT01100ExpenseAllocationViewModel _VM = new();
        private R_Conductor? _conductorRef;

        // Component references
        private R_Grid<FAT01100ExpenseAllocationRSP_FA_GET_ASSET_EXP_ALLOC_LISTResultDTO>? _gridAsset;
        
        private R_ConductorGrid? _conductorGridAsset;

        // State management
        private bool _isEditMode = false;
        private string _lastDepartmentCode = string.Empty;

        protected override async Task R_Init_From_Master(object? poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                if (poParameter is FAT01100DTO loDTO)
                {
                    _VM.Entity = R_FrontUtility.ConvertObjectToObject<FAT01100ExpenseAllocationDTO>(loDTO); 

                    // Refresh grid to load data - use InvokeAsync to ensure it happens after rendering
                    await InvokeAsync(async () =>
                    {
                        if (_conductorGridAsset != null)
                        {
                            await _gridAsset.R_RefreshGrid(null);
                        }
                    });
                }

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }



        #region Grid Data Loading

      
        private async Task Grid_R_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
               
                string lcAssetCode = _VM.Entity?.CASSET_CODE ?? string.Empty;
                await _VM.GetAssetExpAllocListAsync(lcAssetCode);
                eventArgs.ListEntityResult = _VM.AssetExpAllocList ?? new ObservableCollection<FAT01100ExpenseAllocationRSP_FA_GET_ASSET_EXP_ALLOC_LISTResultDTO>();
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
                var loData = (FAT01100ExpenseAllocationRSP_FA_GET_ASSET_EXP_ALLOC_LISTResultDTO)eventArgs.Data;

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
                var loData = (List<FAT01100ExpenseAllocationRSP_FA_GET_TRANS_EXP_ALLOC_LISTResultDTO>)eventArgs.Data;

                // Validate grid has data
                // NET4: If loBigObject.Count = 0 Then loEx.Add(R_Utility.R_GetError(GetType(Resources_Dummy_Class), "PS018"))
                //if (string.IsNullOrWhiteSpace(_VM.TransDetailData.CDEPT_CODE) &&
                //   (_VM.TransDetailData == null || string.IsNullOrWhiteSpace(_VM.TransDetailData.CDEPT_CODE)))
                //{
                //    loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "Val_department"));
                //}
                decimal lnTotal = loData.Sum(x => x.NEXPENSE_PCT);
                if (lnTotal != 100)
                {
                    loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "Val_expense_percentage_not_100"));
                    eventArgs.Cancel = true;
                }

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
                // Convert grid data to List<FAT0110002CommonDTO>
                // NET4: loBigObject = (From A As FAT0110002StreamDTO In bsGridAllocExpense.List Select New FAT0110002CommonDTO...)
                List<FAT01100ExpenseAllocationRSP_FA_GET_TRANS_EXP_ALLOC_LISTResultDTO> loGridData =
                    (List<FAT01100ExpenseAllocationRSP_FA_GET_TRANS_EXP_ALLOC_LISTResultDTO>)eventArgs.Data;
                List<FAT01100ExpenseAllocationBatchListDisplayDTO> loBigObject = loGridData
                    .Select(x => new FAT01100ExpenseAllocationBatchListDisplayDTO
                    {
                        CEXPENSE_DEPT_CODE = x.CEXPENSE_DEPT_CODE,
                        CEXPENSE_DEPT_NAME = x.CEXPENSE_DEPT_NAME,
                        NEXPENSE_PCT = x.NEXPENSE_PCT
                    })
                    .ToList();


        var loParameter = new FAT01100ExpenseAllocationR_SaveBatchParameterDTO
                {
                    CCOMPANY_ID = ClientHelper.CompanyId,
                    CUSER_ID = ClientHelper.UserId,
                    UserParameters = new FAT01100ExpenseAllocationR_SaveBatchUserParameterDTO
                    {
                        CDEPT_CODE = _VM.Entity.CDEPT_CODE,
                        CREF_NO = _VM.Entity.CREF_NO,
                        CASSET_CODE = _VM.Entity.CASSET_CODE,
                        CTRANS_SEQ_NO = "",
                        CPARENT_ID = _VM.Entity.CREC_ID
                    },
                    Data = loBigObject
                };

                // Call batch save using existing batch ViewModel
                string lcLangId = ClientHelper.CultureUI?.TwoLetterISOLanguageName ?? "en";
                //await _batchViewModel.R_SaveBatchAsync(loParameter, lcLangId);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task GridAllocExpense_R_AfterSaveBatch(R_AfterSaveBatchEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                
                _isEditMode = false;
                _lastDepartmentCode = string.Empty;

              
                //if (_gridAllocExpense != null)
                //{
                //    await _gridAllocExpense.R_RefreshGrid(null);
                //}
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
                if (_gridAsset != null)
                {
                    // Use a small delay to ensure state change is processed
                    await Task.Delay(50);
                    // Refresh grid to apply AllowAddNewRow change
                    await _gridAsset.R_RefreshGrid(null);
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
                if (_gridAsset != null)
                {
                    await _gridAsset.R_RefreshGrid(null);
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
                if (_gridAsset != null)
                {
                    await _gridAsset.R_SaveBatch();
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
                var loGridRow = (FAT01100ExpenseAllocationRSP_FA_GET_TRANS_EXP_ALLOC_LISTResultDTO)eventArgs.ColumnData;
                if (loTempResult != null && loGridRow != null)
                {

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
                if (eventArgs.ColumnName == nameof(FAT01100ExpenseAllocationRSP_FA_GET_TRANS_EXP_ALLOC_LISTResultDTO.CEXPENSE_DEPT_CODE))
                {
                    var loGridRow = (FAT01100ExpenseAllocationRSP_FA_GET_TRANS_EXP_ALLOC_LISTResultDTO)eventArgs.CurrentRow;

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
                if (eventArgs.ColumnName == nameof(FAT01100ExpenseAllocationRSP_FA_GET_TRANS_EXP_ALLOC_LISTResultDTO.CEXPENSE_DEPT_CODE))
                {
                    var loGridRow = (FAT01100ExpenseAllocationRSP_FA_GET_TRANS_EXP_ALLOC_LISTResultDTO)eventArgs.CurrentRow;

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
        /// Grid check delete - enables delete functionality
        /// </summary>
        private void GridAllocExpense_R_CheckDelete(R_CheckDeleteEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                // Allow delete when in edit mode
                eventArgs.Allow = _isEditMode;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

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
                var loEntity = R_FrontUtility.ConvertObjectToObject<FAT01100ExpenseAllocationRSP_FA_GET_TRANS_EXP_ALLOC_LISTResultDTO>(eventArgs.Data);
                
                //FAT0110002DTO? loCurrentData = _currentDTO ?? _VM.Data;
                //string lcAssetDeptCode = loCurrentData?.CASSET_DEPT_CODE ?? string.Empty;
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
                var loEntity = R_FrontUtility.ConvertObjectToObject<FAT01100ExpenseAllocationRSP_FA_GET_TRANS_EXP_ALLOC_LISTResultDTO>(eventArgs.Data);
                // Use _currentDTO if available, otherwise use _VM.Data
                // FAT0110002DTO? loCurrentData = _currentDTO ?? _VM.Data;
                // string lcAssetDeptCode = loCurrentData?.CASSET_DEPT_CODE ?? string.Empty;

                // if (loEntity != null && loEntity.CEXPENSE_DEPT_CODE == lcAssetDeptCode)
                // {
                //     eventArgs.Cancel = true;
                // }
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
        /// Follows FAT01100AssetList pattern: converts parameter, updates ViewModel, and refreshes grid
        /// </summary>
        public async Task RefreshTabPageAsync(object poParam)
        {
            var loEx = new R_Exception();

            try
            {
                // Convert parameter to FAT0110002DTO
                var loParam = R_FrontUtility.ConvertObjectToObject<FAT01100ExpenseAllocationDTO>(poParam);

                // Store current DTO
                _VM.Entity = loParam;

                // Update ViewModel Data with the DTO using R_SetCurrentData (Data is read-only)
                if (_VM != null && loParam != null)
                {
                    _VM.R_SetCurrentData(loParam);
                }

                // Check if we have valid data (CASSET_CODE)
                bool llHasData = loParam != null && !string.IsNullOrWhiteSpace(loParam.CASSET_CODE);

                //if (!llHasData)
                //{
                //    // No valid data - clear grid
                //    if (_gridAllocExpense != null && _gridAllocExpense.DataSource != null && _gridAllocExpense.DataSource.Count > 0)
                //    {
                //        _gridAllocExpense.DataSource.Clear();
                //    }

                //    // Also clear the ViewModel's AllocExpenPageList collection
                //    if (_VM?.AllocExpenPageList != null && _VM.AllocExpenPageList.Count > 0)
                //    {
                //        _VM.AllocExpenPageList.Clear();
                //    }
                //}
                //else
                //{
                //    // Has valid data - refresh grid to load expense allocation list
                //    if (_gridAllocExpense != null)
                //    {
                //        await _gridAllocExpense.R_RefreshGrid(null);
                //    }
                //}
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

