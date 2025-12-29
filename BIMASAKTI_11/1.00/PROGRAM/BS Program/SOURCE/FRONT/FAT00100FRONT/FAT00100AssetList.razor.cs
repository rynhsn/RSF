using BlazorClientHelper;
using FAT00100Common;
using FAT00100Common.DTOs;
using FAT00100Model.VMs;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.Base;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Enums;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_BlazorFrontEnd.Interfaces;
using R_CommonFrontBackAPI;
using FAT00100FrontResources;
using System;
using System.Linq;
using System.Threading.Tasks;
using R_BlazorFrontEnd.Controls.Tab;

namespace FAT00100Front
{
    /// <summary>
    /// Code-behind for FAT00100AssetList component
    /// Displays asset list grid and detail form
    /// </summary>
    public partial class FAT00100AssetList : R_Page, R_ITabPage
    {
        [Parameter]
        public FAT00100AssetListViewModel VM { get; set; } = new FAT00100AssetListViewModel();

        private R_Grid<FAT00100GetTransAssetListResultDTO>? _gridRef;
        private R_Conductor? _conductorRef;

        [Inject] private IClientHelper ClientHelper { get; set; } = default!;
        [Inject] private R_ILocalizer<FAT00100FrontResources.Resources_Dummy_Class> Localizer { get; set; } = default!;

        /// <summary>
        /// Display property for local currency code (placeholder - can be populated from parent if needed)
        /// </summary>
        public string LocalCurrencyDisplay => "IDR";

        /// <summary>
        /// Display property for base currency code (placeholder - can be populated from parent if needed)
        /// </summary>
        public string BaseCurrencyDisplay => "IDR";



        #region Lifecycle Methods

        /// <summary>
        /// Initialize from master - called when tab page is opened
        /// </summary>
        protected override async Task R_Init_From_Master(object? poParam)
        {
            var loEx = new R_Exception();
            try
            {
                ClientHelper.Set_CompanyId("HGRBH");
                ClientHelper.Set_UserId("ZF");
                
                // Parameter is FAT00100DTO passed from parent component
                if (poParam is FAT00100DTO loDTO)
                {
                    // Store current record in ViewModel (data state)
                    VM.CurrentRecord = loDTO;
                    
                    // Refresh grid to load data - use InvokeAsync to ensure it happens after rendering
                    // Streaming context will be set in Grid_R_ServiceGetListRecord before API call
                    await InvokeAsync(async () =>
                    {
                        if (_gridRef != null)
                        {
                            await _gridRef.R_RefreshGrid(null);
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

        #endregion

        #region Grid Service Handlers

        /// <summary>
        /// Grid service handler to get list of assets
        /// </summary>
        private async Task Grid_R_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                // Only call API if we have a valid DTO with required fields
                // Required fields: CDEPT_CODE (or PoDeptCode), CTRANSACTION_CODE, CREFERENCE_NO
                if (VM.CurrentRecord != null && !string.IsNullOrWhiteSpace(VM.CurrentRecord.CREFERENCE_NO))
                {
                    // Get parameters from DTO and ClientHelper
                    string lcCompanyId = ClientHelper.CompanyId;
                    string lcLangId = ClientHelper.CultureUI.TwoLetterISOLanguageName;
                    string lcRecId = VM.CurrentRecord.CREC_ID ?? string.Empty;
                    string lcDeptCode = VM.CurrentRecord.CDEPT_CODE ?? string.Empty;
                    string lcTransactionCode = VM.CurrentRecord.CTRANSACTION_CODE ?? string.Empty;
                    string lcReferenceNo = VM.CurrentRecord.CREFERENCE_NO ?? string.Empty;
                    string lcStatus = VM.CurrentRecord.CTRANS_STATUS ?? string.Empty;

                    // Call ViewModel method to get asset list
                    await VM.FAT00100GetTransAssetListAsync(
                        lcCompanyId,
                        lcLangId,
                        lcRecId,
                        lcDeptCode,
                        lcReferenceNo
                    );

                    // Set the result from ViewModel
                    eventArgs.ListEntityResult = VM.AssetList ?? new System.Collections.ObjectModel.ObservableCollection<FAT00100GetTransAssetListResultDTO>();
                }
                else
                {
                    // Return empty list if DTO is null or incomplete
                    eventArgs.ListEntityResult = new System.Collections.ObjectModel.ObservableCollection<FAT00100GetTransAssetListResultDTO>();
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        /// <summary>
        /// Conductor service handler - gets selected asset record from grid
        /// Called automatically by R_Conductor when a grid row is selected (Navigator grid)
        /// Calls FAT00100GetTransAssetAsync to get full asset details and maps to VM.Data
        /// </summary>
        private async Task Conductor_R_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                // Convert event data to parameter DTO (selected asset from grid)
                var loSelectedAsset = R_FrontUtility.ConvertObjectToObject<FAT00100GetTransAssetListResultDTO>(eventArgs.Data);

                if (loSelectedAsset != null && VM.CurrentRecord != null)
                {
                    // Get parameters for FAT00100GetTransAssetAsync
                    string lcCompanyId = ClientHelper.CompanyId;
                    string lcLangId = ClientHelper.CultureUI.TwoLetterISOLanguageName;
                    string lcRecId = loSelectedAsset.CREC_ID ?? string.Empty;
                    string lcDeptCode = loSelectedAsset.CDEPT_CODE ?? string.Empty;
                    string lcRefNo = loSelectedAsset.CREF_NO ?? string.Empty;
                    string lcTransSeqNo = loSelectedAsset.CTRANS_SEQ_NO ?? string.Empty;

                    // Call ViewModel method to get full transaction asset details
                    await VM.FAT00100GetTransAssetAsync(
                        lcCompanyId,
                        lcRecId,
                        lcDeptCode,
                        lcRefNo,
                        lcTransSeqNo,
                        lcLangId
                    );

                    // Map properties from TransAssetData to VM.Data (FAT00100GetTransAssetListResultDTO)
                    // This ensures the form displays the detailed information
                    if (VM.TransAssetData != null)
                    {
                        // Use R_FrontUtility.ConvertObjectToObject to automatically map matching properties
                        var loResult = R_FrontUtility.ConvertObjectToObject<FAT00100GetTransAssetListResultDTO>(VM.TransAssetData);
                        
                        // Set properties that need special handling (not directly mappable)
                        loResult.CCOMPANY_ID = lcCompanyId;
                        loResult.CDEPT_CODE = VM.TransAssetData.CASSET_DEPT_CODE;
                        loResult.CREF_NO = lcRefNo;
                        loResult.CTRANS_SEQ_NO = lcTransSeqNo;

                        // Return the result - conductor will automatically set VM.Data from this result
                        eventArgs.Result = loResult;
                    }
                    else
                    {
                        // Fallback to GetSelectedAsset if TransAssetData is null
                        var loResult = VM.GetSelectedAsset(loSelectedAsset);
                        eventArgs.Result = loResult;
                    }
                }
                else
                {
                    // If no valid asset or CurrentRecord, return empty DTO
                    eventArgs.Result = new FAT00100GetTransAssetListResultDTO();
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        #endregion

        #region Lookup Handlers

        /// <summary>
        /// Asset Department lookup - before open
        /// </summary>
        private void btnAssetDeptLookup_R_Before_Open_Lookup(R_BeforeOpenLookupEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                // TODO: Implement department lookup if needed
                // For now, lookup is disabled
                var loParam = new
                {
                    CCOMPANY_ID = ClientHelper.CompanyId,
                    CLANG_ID = ClientHelper.CultureUI.TwoLetterISOLanguageName,
                    CLOOKUP_SENDER_FLAG = "GSL00500",
                    LACTIVE = true
                };

                eventArgs.Parameter = loParam;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        /// <summary>
        /// Asset Department lookup - after open
        /// </summary>
        private void btnAssetDeptLookup_R_After_Open_Lookup(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                if (eventArgs.Result == null) return;

                // TODO: Implement department lookup result handling if needed
                // For now, lookup is disabled (display only)
                dynamic loResult = eventArgs.Result;
                if (VM.Data != null)
                {
                    VM.Data.CASSET_DEPT_CODE = loResult.cDeptCode?.ToString().Trim() ?? string.Empty;
                    VM.Data.CASSET_DEPT_NAME = loResult.cDeptDesc?.ToString().Trim() ?? string.Empty;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        /// <summary>
        /// Asset Category lookup - before open
        /// </summary>
        private void btnAssetCategoryLookup_R_Before_Open_Lookup(R_BeforeOpenLookupEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                // TODO: Implement category lookup if needed
                // For now, lookup is disabled
                var loParam = new
                {
                    CCOMPANY_ID = ClientHelper.CompanyId,
                    CLANG_ID = ClientHelper.CultureUI.TwoLetterISOLanguageName,
                    CLOOKUP_SENDER_FLAG = "FAT00100"
                };

                eventArgs.Parameter = loParam;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        /// <summary>
        /// Asset Category lookup - after open
        /// </summary>
        private void btnAssetCategoryLookup_R_After_Open_Lookup(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                if (eventArgs.Result == null) return;

                // TODO: Implement category lookup result handling if needed
                // For now, lookup is disabled (display only)
                dynamic loResult = eventArgs.Result;
                if (VM.Data != null)
                {
                    //VM.Data.CTAX_CATEGORY_CODE = loResult.cCategoryCode?.ToString().Trim() ?? string.Empty;
                    //VM.Data.CTAX_CATEGORY_DESC = loResult.cCategoryDesc?.ToString().Trim() ?? string.Empty;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        /// <summary>
        /// Refresh tab page - called when parent component refreshes this tab
        /// Follows PMM01010 pattern: converts parameter, updates ViewModel, and refreshes grid
        /// </summary>
        public async Task RefreshTabPageAsync(object poParam)
        {
            var loEx = new R_Exception();

            try
            {
                // Convert parameter to FAT00100DTO
                var loParam = R_FrontUtility.ConvertObjectToObject<FAT00100DTO>(poParam);
                
                // Store current record in ViewModel (data state)
                VM.CurrentRecord = loParam;

                // Check if we have valid data (CTRANSACTION_CODE or CREFERENCE_NO)
                bool llHasData = loParam != null && 
                    (!string.IsNullOrWhiteSpace(loParam.CTRANSACTION_CODE) || 
                     !string.IsNullOrWhiteSpace(loParam.CREFERENCE_NO));

                if (!llHasData)
                {
                    // No valid data - reset ViewModel and clear grid
                    VM.R_SetCurrentData(new FAT00100GetTransAssetListResultDTO());
                    
                    // Clear grid if it has data
                    if (_gridRef != null && _gridRef.DataSource != null && _gridRef.DataSource.Count > 0)
                    {
                        _gridRef.DataSource.Clear();
                    }
                    
                    // Also clear the ViewModel's AssetList collection
                    if (VM.AssetList != null && VM.AssetList.Count > 0)
                    {
                        VM.AssetList.Clear();
                    }
                }
                else
                {
                    // Has valid data - refresh grid to load asset list
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

            loEx.ThrowExceptionIfErrors();
        }

        #endregion
    }
}

