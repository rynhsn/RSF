using BlazorClientHelper;
using FAT00100Common.DTOs;
using FAT00100Model.VMs;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.Enums;
using R_BlazorFrontEnd.Controls.Popup;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_BlazorFrontEnd.Interfaces;
using R_CommonFrontBackAPI;
using FAT00100FrontResources;
using System;
using System.Threading.Tasks;

namespace FAT00100Front
{
    public partial class FAT0010002 : R_Page
    {
        private readonly FAT0010002ViewModel _VM = new FAT0010002ViewModel();

        [Inject] private IClientHelper ClientHelper { get; set; } = default!;
        [Inject] private R_ILocalizer<FAT00100FrontResources.Resources_Dummy_Class> Localizer { get; set; } = default!;
        [Inject] public R_PopupService PopupService { get; set; } = default!;

        private R_Grid<FAT0010002GetFAAcquisitionDetailAssetListResultDTO>? gvAssetList;
        private R_Conductor? _conductorAssetInfoRef;
        private R_ConductorGrid? _conductorGridRef;

        protected override async Task R_Init_From_Master(object? poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                // Extract parameters from parent form (FAT00100)
                // Parameters include: CDEPT_CODE, CTRANSACTION_CODE, CREFERENCE_NO, CSTATUS, CMODE,
                // Currency codes (Local, Base), Flags (Asset Increment, Journal Group Mode, Department Mode)
                FAT0010002DTO loParam = new FAT0010002DTO();

                if (poParameter is FAT0010002DTO loParameter)
                {
                    loParam = loParameter;
                }
                else if (poParameter != null)
                {
                    // Try to convert if it's a different type
                    loParam = R_FrontUtility.ConvertObjectToObject<FAT0010002DTO>(poParameter) ?? new FAT0010002DTO();
                }

                // Store parameters in ViewModel properties for later use
                if (loParam != null)
                {
                    _VM.DeptCode = loParam.CDEPT_CODE ?? string.Empty;
                    _VM.TransactionCode = loParam.CTRANSACTION_CODE ?? string.Empty;
                    _VM.ReferenceNo = loParam.CREFERENCE_NO ?? string.Empty;
                    _VM.Status = loParam.CSTATUS ?? string.Empty;
                    _VM.Mode = loParam.CMODE ?? string.Empty;
                    _VM.LocalCurrencyCode = loParam.CLOCAL_CURRENCY_CODE ?? string.Empty;
                    _VM.BaseCurrencyCode = loParam.CBASE_CURRENCY_CODE ?? string.Empty;
                    _VM.AssetIncrementFlag = loParam.LASSET_INCREMENT_FLAG;
                    _VM.JrngrpCode = loParam.LJRNGRP_MODE;
                    _VM.DeptMode = loParam.LDEPT_MODE;
                }

                // Call GetFAAcquisitionDetailHeaderAsync to load header data
                if (!string.IsNullOrWhiteSpace(loParam.CDEPT_CODE) &&
                    !string.IsNullOrWhiteSpace(loParam.CTRANSACTION_CODE) &&
                    !string.IsNullOrWhiteSpace(loParam.CREFERENCE_NO))
                {
                    // Ensure CompanyId and LangId are set
                    if (string.IsNullOrWhiteSpace(ClientHelper.CompanyId))
                    {
                        loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "PS001"));
                    }
                    else
                    {
                        await _VM.GetFAAcquisitionDetailHeaderAsync(
                            ClientHelper.CompanyId,
                            ClientHelper.CultureUI?.TwoLetterISOLanguageName ?? "en",
                            loParam
                        );
                    }

                    // Load asset list grid after header is loaded
                    if (gvAssetList != null)
                    {
                        await gvAssetList.R_RefreshGrid(null);
                    }
                }
                else
                {
                    // Log warning if required parameters are missing
                    if (string.IsNullOrWhiteSpace(loParam.CDEPT_CODE) ||
                        string.IsNullOrWhiteSpace(loParam.CTRANSACTION_CODE) ||
                        string.IsNullOrWhiteSpace(loParam.CREFERENCE_NO))
                    {
                        // Parameters missing, but don't throw error - just don't load header
                        // This allows the form to be opened even if parameters are incomplete
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
        /// Grid service handler to get list of assets
        /// </summary>
        private async Task Grid_R_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                // Get parameters from ViewModel and ClientHelper
                string lcCompanyId = ClientHelper.CompanyId;
                string lcLangId = ClientHelper.CultureUI?.TwoLetterISOLanguageName ?? "en";
                string lcDeptCode = _VM.DeptCode;
                string lcTransactionCode = _VM.TransactionCode;
                string lcReferenceNo = _VM.ReferenceNo;
                string lcStatus = _VM.Status;

                // Get update date - use current date (matching VB.NET pattern: DateTime.Now)
                DateTime ldUpdateDate = DateTime.Now;

                // Call ViewModel method to get asset list (streaming method)
                await _VM.GetFAAcquisitionDetailAssetListAsync(
                    lcCompanyId,
                    lcLangId,
                    lcDeptCode,
                    lcTransactionCode,
                    lcReferenceNo,
                    lcStatus,
                    ldUpdateDate
                );

                // Set the result from ViewModel
                eventArgs.ListEntityResult = _VM.AssetList ?? new System.Collections.ObjectModel.ObservableCollection<FAT0010002GetFAAcquisitionDetailAssetListResultDTO>();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        #region Asset Info Tab - Lookup Handlers

        /// <summary>
        /// Asset Department lookup - before open
        /// </summary>
        private void btnAssetDeptLookup_R_Before_Open_Lookup(R_BeforeOpenLookupEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = new
                {
                    CCOMPANY_ID = ClientHelper.CompanyId,
                    CLANG_ID = ClientHelper.CultureUI?.TwoLetterISOLanguageName ?? "en",
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

                dynamic loResult = eventArgs.Result;
                if (_VM.Data != null)
                {
                    _VM.Data.CASSET_DEPT_CODE = loResult.cDeptCode?.ToString().Trim() ?? string.Empty;
                    _VM.Data.CASSET_DEPT_NAME = loResult.cDeptDesc?.ToString().Trim() ?? string.Empty;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        /// <summary>
        /// Asset Journal Group lookup - before open
        /// </summary>
        private void btnAssetJournalGroupLookup_R_Before_Open_Lookup(R_BeforeOpenLookupEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = new
                {
                    CCOMPANY_ID = ClientHelper.CompanyId,
                    CLANG_ID = ClientHelper.CultureUI?.TwoLetterISOLanguageName ?? "en",
                    CLOOKUP_SENDER_FLAG = "GSL00600",
                    CTYPE = "6"
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
        /// Asset Journal Group lookup - after open
        /// </summary>
        private void btnAssetJournalGroupLookup_R_After_Open_Lookup(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                if (eventArgs.Result == null) return;

                dynamic loResult = eventArgs.Result;
                if (_VM.Data != null)
                {
                    _VM.Data.CJRNGRP_CODE = loResult.cJrnGrpCode?.ToString().Trim() ?? string.Empty;
                    _VM.Data.CJRNGRP_DESC = loResult.cJrnGrpDesc?.ToString().Trim() ?? string.Empty;
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
                var loParam = new
                {
                    CCOMPANY_ID = ClientHelper.CompanyId,
                    CLANG_ID = ClientHelper.CultureUI?.TwoLetterISOLanguageName ?? "en",
                    CLOOKUP_SENDER_FLAG = "GSL00510",
                    CCATEGORY_ITEM = "51",
                    CTYPE = "C"
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

                dynamic loResult = eventArgs.Result;
                if (_VM.Data != null)
                {
                    _VM.Data.CCATEGORY_CODE = loResult.cCategoryCode?.ToString().Trim() ?? string.Empty;
                    _VM.Data.CCATEGORY_DESC = loResult.cCategoryDesc?.ToString().Trim() ?? string.Empty;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        /// <summary>
        /// Asset Tax Category lookup - before open
        /// </summary>
        private void btnAssetTaxCategoryLookup_R_Before_Open_Lookup(R_BeforeOpenLookupEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = new
                {
                    CCOMPANY_ID = ClientHelper.CompanyId,
                    CLANG_ID = ClientHelper.CultureUI?.TwoLetterISOLanguageName ?? "en",
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
        /// Asset Tax Category lookup - after open
        /// </summary>
        private void btnAssetTaxCategoryLookup_R_After_Open_Lookup(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                if (eventArgs.Result == null) return;

                dynamic loResult = eventArgs.Result;
                if (_VM.Data != null)
                {
                    _VM.Data.CTAX_CATEGORY_CODE = loResult.cTaxCategoryCode?.ToString().Trim() ?? string.Empty;
                    _VM.Data.CTAX_CATEGORY_DESC = loResult.cTaxCategoryDesc?.ToString().Trim() ?? string.Empty;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        #endregion

        #region Asset Info Tab - Button Handlers

        /// <summary>
        /// Upload picture button click handler
        /// </summary>
        private void btnUploadPicture_OnClick()
        {
            var loEx = new R_Exception();

            try
            {
                // TODO: Implement picture upload functionality
                // This should open a file dialog and convert the image to byte array (OASSET_IMAGE)
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        /// <summary>
        /// Reset picture button click handler
        /// </summary>
        private void btnResetPicture_OnClick()
        {
            var loEx = new R_Exception();

            try
            {
                if (_VM.Data != null)
                {
                    _VM.Data.OASSET_IMAGE = null;
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
        /// </summary>
        private void btnCancel_OnClick()
        {
            var loEx = new R_Exception();

            try
            {
                // TODO: Implement cancel functionality
                // This should close the form or reset the current record
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        /// <summary>
        /// Next button click handler
        /// </summary>
        private void btnNext_OnClick()
        {
            var loEx = new R_Exception();

            try
            {
                // TODO: Implement next functionality
                // This should navigate to the next tab (Depreciation Info)
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        #endregion

        #region Conductor Handlers for Asset Info

        /// <summary>
        /// Conductor - Service Get Record
        /// </summary>
        private async Task ConductorAssetInfo_R_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                // Get record from grid selection
                var loGridRow = R_FrontUtility.ConvertObjectToObject<FAT0010002GetFAAcquisitionDetailAssetListResultDTO>(eventArgs.Data);

                if (loGridRow != null && !string.IsNullOrWhiteSpace(loGridRow.CASSET_CODE))
                {
                    // Ensure we have required parameters
                    if (string.IsNullOrWhiteSpace(_VM.DeptCode) || 
                        string.IsNullOrWhiteSpace(_VM.TransactionCode) || 
                        string.IsNullOrWhiteSpace(_VM.ReferenceNo))
                    {
                        loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "PS002"));
                    }
                    else
                    {
                        // Call ViewModel method to get full asset detail record
                        await _VM.GetRecordAsync(
                            ClientHelper.CompanyId,
                            ClientHelper.CultureUI?.TwoLetterISOLanguageName ?? "en",
                            _VM.DeptCode,
                            _VM.TransactionCode,
                            _VM.ReferenceNo,
                            loGridRow.CASSET_CODE
                        );

                        // Set result to CurrentRecord from ViewModel
                        if (_VM.CurrentRecord != null)
                        {
                            eventArgs.Result = _VM.CurrentRecord;
                        }
                        else
                        {
                            // If CurrentRecord is null, return empty DTO (should not happen if GetRecordAsync succeeded)
                            eventArgs.Result = new FAT0010002DTO();
                        }
                    }
                }
                else
                {
                    // If no valid grid row, return empty DTO
                    eventArgs.Result = new FAT0010002DTO();
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        /// <summary>
        /// Conductor - Display
        /// </summary>
        private void ConductorAssetInfo_R_Display(R_DisplayEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                // Update UI when record is displayed
                // Additional UI updates can be done here
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        /// <summary>
        /// Conductor - After Add
        /// </summary>
        private void ConductorAssetInfo_R_AfterAdd(R_AfterAddEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                // Initialize new record with default values
                if (_VM.Data != null)
                {
                    _VM.Data.CDEPT_CODE = _VM.DeptCode;
                    _VM.Data.CTRANSACTION_CODE = _VM.TransactionCode;
                    _VM.Data.CREFERENCE_NO = _VM.ReferenceNo;
                    _VM.Data.CSTATUS = _VM.Status;
                    // Set other default values as needed
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        /// <summary>
        /// Conductor - Saving
        /// </summary>
        private void ConductorAssetInfo_R_Saving(R_SavingEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                // Perform validation and data preparation before save
                // Additional business logic can be added here
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        /// <summary>
        /// Conductor - Service Save
        /// </summary>
        private async Task ConductorAssetInfo_R_ServiceSave(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                // Get entity from event args
                var loEntity = eventArgs.Data as FAT0010002DTO ?? _VM.Data;
                
                // Ensure entity is not null
                if (loEntity == null)
                {
                    loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "PS002"));
                }
                else
                {
                    // Ensure required fields are set for the entity
                    // Set header fields if not already set
                    if (string.IsNullOrWhiteSpace(loEntity.CDEPT_CODE))
                        loEntity.CDEPT_CODE = _VM.DeptCode;
                    if (string.IsNullOrWhiteSpace(loEntity.CTRANSACTION_CODE))
                        loEntity.CTRANSACTION_CODE = _VM.TransactionCode;
                    if (string.IsNullOrWhiteSpace(loEntity.CREFERENCE_NO))
                        loEntity.CREFERENCE_NO = _VM.ReferenceNo;
                    
                    // Call ViewModel method to save asset detail
                    await _VM.SaveRecordAsync(
                        loEntity,
                        eventArgs.ConductorMode == R_eConductorMode.Add ? eCRUDMode.AddMode : eCRUDMode.EditMode,
                        ClientHelper.CompanyId,
                        ClientHelper.CultureUI?.TwoLetterISOLanguageName ?? "en"
                    );

                    // Set result - this will update the conductor's bound entity with the result from backend
                    eventArgs.Result = _VM.CurrentRecord;

                    // Refresh asset list grid after save
                    if (gvAssetList != null)
                    {
                        await gvAssetList.R_RefreshGrid(null);
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
        /// Conductor - Service Delete
        /// </summary>
        private async Task ConductorAssetInfo_R_ServiceDelete(R_ServiceDeleteEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                // Get entity from event args
                var loEntity = eventArgs.Data as FAT0010002DTO ?? _VM.Data;
                
                // Call ViewModel method to delete asset
                await _VM.DeleteRecordAsync(
                    loEntity,
                    ClientHelper.CompanyId,
                    ClientHelper.CultureUI?.TwoLetterISOLanguageName ?? "en"
                );

                // Refresh asset list grid after delete
                if (gvAssetList != null)
                {
                    await gvAssetList.R_RefreshGrid(null);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        /// <summary>
        /// Conductor - Validation
        /// </summary>
        private void ConductorAssetInfo_R_Validation(R_ValidationEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                // Perform field-level validation
                if (_VM.Data != null)
                {
                    if (string.IsNullOrWhiteSpace(_VM.Data.CASSET_CODE))
                    {
                        loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "_ErrAssetCodeRequired"));
                    }
                    if (string.IsNullOrWhiteSpace(_VM.Data.CASSET_NAME))
                    {
                        loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "_ErrAssetNameRequired"));
                    }
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            if (loEx.HasError)
            {
                eventArgs.Cancel = true;
            }

            loEx.ThrowExceptionIfErrors();
        }

        /// <summary>
        /// Conductor - Check Add
        /// </summary>
        private void ConductorAssetInfo_R_CheckAdd(R_CheckAddEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                // Allow add when conductor is in Normal mode
                eventArgs.Allow = _conductorAssetInfoRef?.R_ConductorMode == R_eConductorMode.Normal;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        /// <summary>
        /// Conductor - Check Edit
        /// </summary>
        private void ConductorAssetInfo_R_CheckEdit(R_CheckEditEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                // Allow edit when conductor is in Normal mode
                eventArgs.Allow = _conductorAssetInfoRef?.R_ConductorMode == R_eConductorMode.Normal;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        /// <summary>
        /// Conductor - Check Delete
        /// </summary>
        private void ConductorAssetInfo_R_CheckDelete(R_CheckDeleteEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                // Allow delete when conductor is in Normal mode
                eventArgs.Allow = _conductorAssetInfoRef?.R_ConductorMode == R_eConductorMode.Normal;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        #endregion

        #region Import Button Handlers

        /// <summary>
        /// Import PJ button click handler
        /// </summary>
        private async Task btnImportPJ_OnClick()
        {
            var loEx = new R_Exception();

            try
            {
                // TODO: Implement Import PJ functionality when FAT0010003 is available
                // Create parameter for FAT0010003 (Project Import form)
                // var loParam = new FAT0010003DTO
                // {
                //     PCFR_DEPT_CODE = _VM.DeptCode,
                //     PCFR_TRANSACTION_CODE = _VM.TransactionCode,
                //     PCFR_REFERENCE_NO = _VM.ReferenceNo,
                //     CLOCAL_CURRENCY_CODE = _VM.LocalCurrencyCode,
                //     CBASE_CURRENCY_CODE = _VM.BaseCurrencyCode
                // };

                // Create popup settings
                // var loPopupSettings = new R_PopupSettings
                // {
                //     PageTitle = Localizer["_ImportPJ"],
                //     WithLock = true,
                //     Page = this
                // };

                // Show popup
                // var loResult = await PopupService.Show(typeof(FAT0010003), loParam, poPopupSettings: loPopupSettings);

                // Handle result
                // if (loResult != null)
                // {
                //     // TODO: Process imported project assets
                //     // After import, trigger Add mode in conductor
                //     if (_conductorAssetInfoRef != null)
                //     {
                //         // _conductorAssetInfoRef.R_Add();
                //     }

                //     // Refresh asset list grid
                //     if (gvAssetList != null)
                //     {
                //         await gvAssetList.R_RefreshGrid(null);
                //     }
                // }
                
                // Placeholder - functionality not yet implemented
                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        /// <summary>
        /// Import Existing button click handler
        /// </summary>
        private async Task btnImportExisting_OnClick()
        {
            var loEx = new R_Exception();

            try
            {
                // Create parameter for FAT00100Import form
                var loParam = new
                {
                    CCOMPANY_ID = ClientHelper.CompanyId,
                    CLANG_ID = ClientHelper.CultureUI?.TwoLetterISOLanguageName ?? "en",
                    CDEPT_CODE = _VM.DeptCode,
                    CTRANSACTION_CODE = _VM.TransactionCode,
                    CREFERENCE_NO = _VM.ReferenceNo
                };

                // Create popup settings
                var loPopupSettings = new R_PopupSettings
                {
                    PageTitle = Localizer["_ImportExisting"],
                    WithLock = true,
                    Page = this
                };

                // Show popup
                // TODO: Replace with actual import form type when available
                // var loResult = await PopupService.Show(typeof(FAT00100Import), loParam, poPopupSettings: loPopupSettings);

                // Handle result
                // if (loResult != null)
                // {
                //     // TODO: Process imported existing assets
                //     // Refresh asset list grid
                //     if (gvAssetList != null)
                //     {
                //         await gvAssetList.R_RefreshGrid(null);
                //     }
                // }
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

