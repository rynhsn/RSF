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
using R_BlazorFrontEnd.Controls.Tab;
using System;
using System.Reflection;
using System.Threading.Tasks;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using Lookup_FAFront;
using Lookup_FACommon.DTOs;
using Microsoft.AspNetCore.Components.Forms;
using R_BlazorFrontEnd.Controls.MessageBox;
using Lookup_GSModel.ViewModel;
using Lookup_FAModel.ViewModel.FAL00200;
using System.Xml.Linq;
using System.Collections.Generic;
using static System.Net.Mime.MediaTypeNames;

namespace FAT00100Front
{
    public partial class FAT0010002 : R_Page
    {
        private readonly FAT0010002ViewModel _VM = new FAT0010002ViewModel();
        private readonly FAT00100ViewModel _VM00100 = new FAT00100ViewModel();

        [Inject] private IClientHelper ClientHelper { get; set; } = default!;
        private R_eFileSelectAccept[] accepts = { R_eFileSelectAccept.Image };
        [Inject] private R_ILocalizer<FAT00100FrontResources.Resources_Dummy_Class> Localizer { get; set; } = default!;
        [Inject] public R_PopupService PopupService { get; set; } = default!;

        private R_Grid<FAT0010002GetFAAcquisitionDetailAssetListResultDTO>? gvAssetList;
        private R_Conductor? _conductorAssetInfoRef;
        private R_ConductorGrid? _conductorGridRef;

        private bool IsSuccess { get; set; } = false;
        private bool IsCRUDMode = true;
        private bool IsNormalMode = true;
        private R_TabStrip tabStripRef;
        private R_TabStripTab? _tabExpenseAllocation;
        private R_TabPage? _tabPageExpenseAllocation;

        private bool IsErrorEmptyFile = false;

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
                    _VM.ReferenceNo = loParam.CREF_NO ?? string.Empty;
                    _VM.Status = loParam.CSTATUS ?? string.Empty;
                    _VM.Mode = loParam.CMODE ?? string.Empty;
                    _VM.LocalCurrencyCode = loParam.CLOCAL_CURRENCY_CODE ?? string.Empty;
                    _VM.BaseCurrencyCode = loParam.CBASE_CURRENCY_CODE ?? string.Empty;
                    _VM.AssetIncrementFlag = loParam.LASSET_INCREMENT_FLAG;
                    _VM.JrngrpCode = loParam.LJRNGRP_MODE;
                    _VM.DeptMode = loParam.LDEPT_MODE;
                    _VM.RecId = loParam.CREC_ID ?? string.Empty;
                    _VM.SoftPeriod = loParam.CSOFT_PERIOD ?? string.Empty;
                    _VM.DeptCodeDefault= loParam.CDEPT_CODE_DEFAULT ?? string.Empty;
                }

                await _VM00100.GetDeptLookupListAsync(ClientHelper.CompanyId, ClientHelper.UserId, string.Empty);

                // Call GetFAAcquisitionDetailHeaderAsync to load header data
                if (!string.IsNullOrWhiteSpace(loParam.CDEPT_CODE) &&
                    !string.IsNullOrWhiteSpace(loParam.CTRANSACTION_CODE) &&
                    !string.IsNullOrWhiteSpace(loParam.CREF_NO))
                {
                    // Ensure CompanyId and LangId are set
                    if (string.IsNullOrWhiteSpace(ClientHelper.CompanyId))
                    {
                        loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "PS001"));
                    }
                    else
                    {
                        await _VM.GetTransDetailAsync(
                            ClientHelper.CompanyId,
                            ClientHelper.CultureUI?.TwoLetterISOLanguageName ?? "en",
                            _VM.RecId,
                            _VM.DeptCode,
                            _VM.ReferenceNo
                        );
                    }

                    // Load depreciation method combo box
                    await _VM.GetComboDepreciationMethodAsync(
                        ClientHelper.CompanyId,
                        ClientHelper.CultureUI?.TwoLetterISOLanguageName ?? "en"
                    );

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
                string lcRecId = _VM.TransDetailData?.CREC_ID ?? string.Empty;


                // Get update date - use current date (matching VB.NET pattern: DateTime.Now)
                DateTime ldUpdateDate = DateTime.Now;

                // Call ViewModel method to get asset list (streaming method)
                await _VM.GetFAAcquisitionDetailAssetListAsync(
                    lcCompanyId,
                    lcLangId,
                    lcDeptCode,
                    lcTransactionCode,
                    lcReferenceNo,
                    lcRecId,
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
        /// Asset Department lookup - after open
        /// </summary>
        private void btnAssetDeptLookup_R_After_Open_Lookup(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loTempResult = (GSL00700DTO)eventArgs.Result;
                if (loTempResult != null)
                {
                    _VM.Data.CASSET_DEPT_CODE = loTempResult.CDEPT_CODE;
                    _VM.Data.CASSET_DEPT_NAME = loTempResult.CDEPT_NAME;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task txtDepartment_OnLostFocused()
        {
            R_Exception loEx = new();

            try
            {
                FAT0010002DTO loGetData = _VM.Data;

                if (string.IsNullOrWhiteSpace(loGetData.CASSET_DEPT_CODE))
                {
                    loGetData.CASSET_DEPT_CODE = "";
                    loGetData.CASSET_DEPT_NAME = "";
                    return;
                }

                LookupGSL00700ViewModel loLookupViewModel = new();
                var param = new GSL00700ParameterDTO
                {
                    CCOMPANY_ID = ClientHelper.CompanyId,
                    CUSER_ID = ClientHelper.UserId,
                    CSEARCH_TEXT = loGetData.CASSET_DEPT_CODE
                };
                var loResult = await loLookupViewModel.GetDepartment(param);

                if (loResult == null)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                        "_ErrLookup01"));
                    loGetData.CASSET_DEPT_CODE = "";
                    loGetData.CASSET_DEPT_NAME = "";
                }
                else
                {
                    loGetData.CASSET_DEPT_CODE = loResult.CDEPT_CODE;
                    loGetData.CASSET_DEPT_NAME = loResult.CDEPT_NAME;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        /// <summary>
        /// Asset Journal Group lookup - before open
        /// </summary>
        private void btnAssetJournalGroupLookup_R_Before_Open_Lookup(R_BeforeOpenLookupEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = new GSL00400ParameterDTO()
                {
                    CCOMPANY_ID = ClientHelper.CompanyId,
                    CPROPERTY_ID = "",
                    CJRNGRP_TYPE = "60"
                };
                eventArgs.Parameter = loParam;
                eventArgs.TargetPageType = typeof(GSL00400);
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
                    _VM.Data.CJRNGRP_CODE = loResult.CJRNGRP_CODE?.ToString().Trim() ?? string.Empty;
                    _VM.Data.CJRNGRP_NAME = loResult.CJRNGRP_NAME?.ToString().Trim() ?? string.Empty;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task txtJournalGroup_OnLostFocused()
        {
            R_Exception loEx = new();

            try
            {
                FAT0010002DTO loGetData = _VM.Data;

                if (string.IsNullOrWhiteSpace(loGetData.CJRNGRP_CODE))
                {
                    loGetData.CJRNGRP_CODE = "";
                    loGetData.CJRNGRP_NAME = "";
                    return;
                }

                LookupGSL00400ViewModel loLookupViewModel = new();
                var param = new GSL00400ParameterDTO
                {
                    CCOMPANY_ID = ClientHelper.CompanyId,
                    CPROPERTY_ID = "",
                    CJRNGRP_TYPE = "60",
                    CSEARCH_TEXT = loGetData.CASSET_DEPT_CODE
                };
                var loResult = await loLookupViewModel.GetJournalGroup(param);

                if (loResult == null)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                        "_ErrLookup01"));
                    loGetData.CJRNGRP_CODE = "";
                    loGetData.CJRNGRP_NAME = "";
                }
                else
                {
                    loGetData.CJRNGRP_CODE = loResult.CJRNGRP_CODE;
                    loGetData.CJRNGRP_NAME = loResult.CJRNGRP_NAME;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        /// <summary>
        /// Asset Category lookup - before open
        /// </summary>
        private void btnAssetCategoryLookup_R_Before_Open_Lookup(R_BeforeOpenLookupEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                eventArgs.Parameter = new GSL01800DTOParameter()
                {
                    CCOMPANY_ID = ClientHelper.CompanyId,
                    CUSER_ID = ClientHelper.UserId,
                    CPROPERTY_ID = "",
                    CCATEGORY_TYPE = "60"
                };
                eventArgs.TargetPageType = typeof(GSL01800);
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

                var loData = (GSL01800DTO)eventArgs.Result;
                if (loData == null)
                    return;

                _VM.Data.CCATEGORY_CODE = loData.CCATEGORY_ID;
                _VM.Data.CCATEGORY_NAME = loData.CCATEGORY_NAME;

                
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task txtAssetCategory_OnLostFocused()
        {
            R_Exception loEx = new();

            try
            {
                FAT0010002DTO loGetData = _VM.Data;

                if (string.IsNullOrWhiteSpace(loGetData.CCATEGORY_CODE))
                {
                    loGetData.CCATEGORY_CODE = "";
                    loGetData.CCATEGORY_NAME = "";
                    return;
                }

                LookupGSL01800ViewModel loLookupViewModel = new();
                var param = new GSL01800DTOParameter
                {
                    CCOMPANY_ID = ClientHelper.CompanyId,
                    CUSER_ID = ClientHelper.UserId,
                    CPROPERTY_ID = "",
                    CCATEGORY_TYPE = "60",
                    CSEARCH_TEXT = loGetData.CASSET_DEPT_CODE
                };
                var loResult = await loLookupViewModel.GetCategory(param);

                if (loResult == null)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                        "_ErrLookup01"));
                    loGetData.CCATEGORY_CODE = "";
                    loGetData.CCATEGORY_NAME = "";
                }
                else
                {
                    loGetData.CCATEGORY_CODE = loResult.CCATEGORY_ID;
                    loGetData.CCATEGORY_NAME = loResult.CCATEGORY_NAME;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        /// <summary>
        /// Asset Tax Category lookup - before open
        /// </summary>
        private void btnAssetTaxCategoryLookup_R_Before_Open_Lookup(R_BeforeOpenLookupEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                eventArgs.Parameter = new FAL00200ParameterDTO()
                {
                    CCOMPANY_ID = ClientHelper.CompanyId,
                    CSTATUS = "ACTIVE",
                    CTAX_CATEGORY_ID = _VM.Data.CTAX_CATEGORY_CODE ?? "",
                    CLANGUAGE_ID = ClientHelper.CultureUI?.TwoLetterISOLanguageName ?? "en"
                };
                eventArgs.TargetPageType = typeof(FAL00200);
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

                var loData = (FAL00200DTO)eventArgs.Result;
                if (loData == null)
                    return;

                _VM.Data.CTAX_CATEGORY_CODE = loData.CTAX_CATEGORY_ID;
                _VM.Data.CTAX_CATEGORY_NAME = loData.CTAX_CATEGORY_NAME;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task txtAssetTaxCategory_OnLostFocused()
        {
            R_Exception loEx = new();

            try
            {
                FAT0010002DTO loGetData = _VM.Data;

                if (string.IsNullOrWhiteSpace(loGetData.CTAX_CATEGORY_CODE))
                {
                    loGetData.CTAX_CATEGORY_CODE = "";
                    loGetData.CTAX_CATEGORY_NAME = "";
                    return;
                }

                LookupFAL00200ViewModel loLookupViewModel = new();
                var param = new FAL00200ParameterDTO
                {
                    CCOMPANY_ID = ClientHelper.CompanyId,
                    CSTATUS = "ACTIVE",
                    CTAX_CATEGORY_ID = _VM.Data.CTAX_CATEGORY_CODE ?? "",
                    CLANGUAGE_ID = ClientHelper.CultureUI?.TwoLetterISOLanguageName ?? "en",
                };
                var loResult = await loLookupViewModel.GetTaxCategory(param);

                if (loResult == null)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                        "_ErrLookup01"));
                    loGetData.CTAX_CATEGORY_CODE = "";
                    loGetData.CTAX_CATEGORY_NAME = "";
                }
                else
                {
                    loGetData.CTAX_CATEGORY_CODE = loResult.CTAX_CATEGORY_ID;
                    loGetData.CTAX_CATEGORY_NAME = loResult.CTAX_CATEGORY_NAME;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private void btnLocationLookup_R_Before_Open_Lookup(R_BeforeOpenLookupEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                eventArgs.Parameter = new GSL03800ParameterDTO()
                {
                    CPROPERTY_ID = _conductorAssetInfoRef.R_ConductorMode == R_eConductorMode.Add ? "" :_VM.Data.CPROPERTY_ID,
                    CACTIVE_TYPE = "ACTIVE",
                    CLOCATION_ID =_conductorAssetInfoRef.R_ConductorMode == R_eConductorMode.Add ? "" : _VM.Data.CLOCATION_ID,
                    CSEARCH_TEXT = ""
                };
                eventArgs.TargetPageType = typeof(GSL03800);
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
        private void btnLocationLookup_R_After_Open_Lookup(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {

                var loData = (GSL03800DTO)eventArgs.Result;
                if (loData == null)
                    return;

                _VM.Data.CLOCATION_ID = loData.CLOCATION_ID;
                _VM.Data.CLOCATION_NAME = loData.CLOCATION_NAME;
                _VM.Data.CPROPERTY_ID = loData.CPROPERTY_ID;
                _VM.Data.CPROPERTY_NAME = loData.CPROPERTY_ID;
                _VM.Data.CBUILDING_ID = loData.CBUILDING_ID;
                _VM.Data.CBUILDING_NAME= loData.CBUILDING_NAME;
                _VM.Data.CFLOOR_ID = loData.CFLOOR_ID;
                _VM.Data.CFLOOR_NAME = loData.CFLOOR_NAME;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task txtLocation_OnLostFocused()
        {
            R_Exception loEx = new();

            try
            {
                FAT0010002DTO loGetData = _VM.Data;

                if (string.IsNullOrWhiteSpace(loGetData.CLOCATION_ID))
                {
                    loGetData.CLOCATION_ID = "";
                    loGetData.CLOCATION_NAME = "";
                    loGetData.CPROPERTY_ID = "";
                    loGetData.CPROPERTY_NAME = "";
                    loGetData.CBUILDING_ID = "";
                    loGetData.CBUILDING_NAME = "";
                    loGetData.CFLOOR_ID = "";
                    loGetData.CFLOOR_NAME = "";
                    return;
                }

                LookupGSL03800ViewModel loLookupViewModel = new();
                var param = new GSL03800ParameterDTO
                {
                    CPROPERTY_ID = _conductorAssetInfoRef.R_ConductorMode == R_eConductorMode.Add ? "" : loGetData.CPROPERTY_ID,
                    CACTIVE_TYPE = "ACTIVE",
                    CLOCATION_ID = _conductorAssetInfoRef.R_ConductorMode == R_eConductorMode.Add ? "" : loGetData.CLOCATION_ID,
                    CSEARCH_TEXT = loGetData.CLOCATION_ID,
                };
                var loResult = await loLookupViewModel.GetLocation(param);

                if (loResult == null)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                        "_ErrLookup01"));
                    loGetData.CLOCATION_ID = "";
                    loGetData.CLOCATION_NAME = "";
                    loGetData.CPROPERTY_ID = "";
                    loGetData.CPROPERTY_NAME = "";
                    loGetData.CBUILDING_ID = "";
                    loGetData.CBUILDING_NAME = "";
                    loGetData.CFLOOR_ID = "";
                    loGetData.CFLOOR_NAME = "";
                }
                else
                {
                    loGetData.CLOCATION_ID = loResult.CLOCATION_ID;
                    loGetData.CLOCATION_NAME = loResult.CLOCATION_NAME;
                    loGetData.CPROPERTY_ID = loResult.CPROPERTY_ID;
                    loGetData.CPROPERTY_NAME = loResult.CPROPERTY_ID;
                    loGetData.CBUILDING_ID = loResult.CBUILDING_ID;
                    loGetData.CBUILDING_NAME = loResult.CBUILDING_NAME;
                    loGetData.CFLOOR_ID = loResult.CFLOOR_ID;
                    loGetData.CFLOOR_NAME = loResult.CFLOOR_NAME;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
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
                    _VM.Data.OIMAGE = null;
                    _VM.Data.CSTORAGE_ID = "";
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

        #region Business Process Methods - Amount Calculation
        private async Task OnAmountChanged(decimal value, string valFrom, string set1, string set2, bool calculateBookValue)
        {
            var loEx = new R_Exception();

            try
            {
                if (_VM.Data == null)
                    return;

                // Use reflection to set properties dynamically on _VM.Data
                var loType = typeof(FAT0010002DTO);
                var loValFrom = loType.GetProperty(valFrom);
                var loProperty1 = loType.GetProperty(set1);
                var loProperty2 = loType.GetProperty(set2);

                if (loValFrom != null)
                {
                    loValFrom.SetValue(_VM.Data, value);
                }

                if (loProperty1 != null)
                {
                    if (_VM.TransDetailData != null && _VM.TransDetailData.NLBASE_RATE != 0)
                    {
                        decimal lnCalculatedValue = (value / _VM.TransDetailData.NLBASE_RATE) * _VM.TransDetailData.NLCURRENCY_RATE;
                        // Set property on _VM.Data using reflection
                        loProperty1.SetValue(_VM.Data, lnCalculatedValue);
                    }
                    else
                    {
                        // Set property on _VM.Data to 0
                        loProperty1.SetValue(_VM.Data, 0m);
                    }
                }

                if (loProperty2 != null)
                {
                    if (_VM.TransDetailData != null && _VM.TransDetailData.NBBASE_RATE != 0)
                    {
                        decimal lnCalculatedValue = (value / _VM.TransDetailData.NBBASE_RATE) * _VM.TransDetailData.NBCURRENCY_RATE;
                        // Set property on _VM.Data using reflection
                        loProperty2.SetValue(_VM.Data, lnCalculatedValue);
                    }
                    else
                    {
                        // Set property on _VM.Data to 0
                        loProperty2.SetValue(_VM.Data, 0m);
                    }
                }

                if (calculateBookValue == true)
                {
                    _VM.Data.NBOOK_VALUE = _VM.Data.NINIT_COST + _VM.Data.NADDITION - _VM.Data.NDEDUCTION - _VM.Data.NPRIOR_DEPR - _VM.Data.NYTD_DEPR;
                    await OnAmountChanged(_VM.Data.NBOOK_VALUE, "NBOOK_VALUE", "NLBOOK_VALUE", "NBBOOK_VALUE", false);
                    if (_VM.Data.LNEW_FLAG)
                    {
                        _VM.Data.NBEG_BOOK_VALUE = _VM.Data.NBOOK_VALUE;
                        await OnAmountChanged(_VM.Data.NBEG_BOOK_VALUE, "NBEG_BOOK_VALUE", "NLBEG_BOOK_VALUE", "NBBEG_BOOK_VALUE", false);
                    }
                }
                else
                {
                    CalculateYearlyDepreciationProcess(_VM.Data.CDEPR_METHOD);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void OnFlagNewChanged(bool poParam)
        {
            _VM.Data.LNEW_FLAG = poParam;
        }

        private void OnUsefulLifeYearChanged(int poParam)
        {
            _VM.Data.IUSEFUL_LIFE_YY = poParam;
            if (_VM.Data.LNEW_FLAG)
            {
                _VM.Data.IREMAINING_LIFE_YY = _VM.Data.IUSEFUL_LIFE_YY;
            }
            CalculateYearlyDepreciationProcess(_VM.Data.CDEPR_METHOD);
        }

        private void OnUsefulLifeMonthChanged(int poParam)
        {
            _VM.Data.IUSEFUL_LIFE_MM = poParam;
            if (_VM.Data.LNEW_FLAG)
            {
                _VM.Data.IREMAINING_LIFE_MM = _VM.Data.IUSEFUL_LIFE_MM;
            }
            CalculateYearlyDepreciationProcess(_VM.Data.CDEPR_METHOD);
        }

        private void OnRemainingYearChanged(int poParam)
        {
            _VM.Data.IREMAINING_LIFE_YY = poParam;
            CalculateYearlyDepreciationProcess(_VM.Data.CDEPR_METHOD);
        }

        private void OnRemainingMonthChanged(int poParam)
        {
            _VM.Data.IREMAINING_LIFE_MM = poParam;
            CalculateYearlyDepreciationProcess(_VM.Data.CDEPR_METHOD);
        }

        /// <summary>
        /// Handle depreciation method changed event
        /// </summary>
        private void OnDepreciationMethodChanged(string? value)
        {
            if (_VM.Data != null)
            {
                _VM.Data.CDEPR_METHOD = value;
                CalculateYearlyDepreciationProcess(value);

            }
        }

        private async Task OnBookValueChanged(decimal poParam)
        {
            _VM.Data.NBOOK_VALUE = poParam;
            await OnAmountChanged(poParam, "NBOOK_VALUE", "NLBOOK_VALUE", "NBBOOK_VALUE",false);
            if (_VM.Data.LNEW_FLAG)
            {
                _VM.Data.NBEG_BOOK_VALUE = _VM.Data.NBOOK_VALUE;
            }
        }

        private void CalculateYearlyDepreciationProcess(string value)
        { 
            //VAR_NO_DEPRECIATION = 00-ND
            //VAR_MANUAL = 10-MN
            //VAR_STRAIGHT_LINE = 20-SL
            //VAR_DECLINING = 30-DC
            //VAR_DOUBLE_DECLINING = 40-DD

            if (value== "00-ND" || value == "10-MN")

            {
                _VM.Data.NYEAR_DEPR_PCT = 0;
            }else 
            if (value == "40-DD") 
            {
                //_VM.Data.NYEAR_DEPR_PCT = ((1m / ((_VM.Data.IREMAINING_LIFE_YY * 12) + _VM.Data.IREMAINING_LIFE_MM)) *12) *200;
                decimal lnDenominator = (_VM.Data.IREMAINING_LIFE_YY * 12) + _VM.Data.IREMAINING_LIFE_MM;
                if (lnDenominator == 0)
                {
                    _VM.Data.NYEAR_DEPR_PCT = 0;
                }
                else
                {
                    _VM.Data.NYEAR_DEPR_PCT = ((1m / lnDenominator) * 12) * 200;
                }
            }
            else
            {
                //_VM.Data.NYEAR_DEPR_PCT = ((1m / ((_VM.Data.IREMAINING_LIFE_YY * 12) + _VM.Data.IREMAINING_LIFE_MM)) * 12) * 100;
                decimal lnDenominator = (_VM.Data.IREMAINING_LIFE_YY * 12) + _VM.Data.IREMAINING_LIFE_MM;
                if (lnDenominator == 0)
                {
                    _VM.Data.NYEAR_DEPR_PCT = 0;
                }
                else
                {
                    _VM.Data.NYEAR_DEPR_PCT = ((1m / lnDenominator) * 12) * 100;
                }
            }

            decimal decVal = 0.01m;
            if (value == "20-SL")
            {
                
                //_VM.Data.NYEAR_DEPR = _VM.Data.NYEAR_DEPR_PCT * (_VM.Data.NBEG_BOOK_VALUE - _VM.Data.NRESIDUAL_VALUE) * decVal;
                decimal lnDenominator = (_VM.Data.NBEG_BOOK_VALUE - _VM.Data.NRESIDUAL_VALUE);
                if (lnDenominator == 0)
                {
                    _VM.Data.NYEAR_DEPR = 0;
                }
                else
                {
                    _VM.Data.NYEAR_DEPR = _VM.Data.NYEAR_DEPR_PCT * lnDenominator * decVal;
                }
                //_VM.Data.NLYEAR_DEPR = _VM.Data.NYEAR_DEPR_PCT * (_VM.Data.NLBEG_BOOK_VALUE - _VM.Data.NLRESIDUAL_VALUE) * decVal;
                decimal lnDenominator2 = (_VM.Data.NLBEG_BOOK_VALUE - _VM.Data.NLRESIDUAL_VALUE);
                if (lnDenominator2 == 0)
                {
                    _VM.Data.NLYEAR_DEPR = 0;
                }
                else
                {
                    _VM.Data.NLYEAR_DEPR = _VM.Data.NYEAR_DEPR_PCT * lnDenominator2 * decVal;
                }
                //_VM.Data.NBYEAR_DEPR = _VM.Data.NYEAR_DEPR_PCT * (_VM.Data.NBBEG_BOOK_VALUE - _VM.Data.NBRESIDUAL_VALUE) * decVal;
                decimal lnDenominator3 = (_VM.Data.NBBEG_BOOK_VALUE - _VM.Data.NBRESIDUAL_VALUE);
                if (lnDenominator3 == 0)
                {
                    _VM.Data.NBYEAR_DEPR = 0;
                }
                else
                {
                    _VM.Data.NBYEAR_DEPR = _VM.Data.NYEAR_DEPR_PCT * lnDenominator3 * decVal;
                }
            }
            else
            {
                //  _VM.Data.NYEAR_DEPR = _VM.Data.NYEAR_DEPR_PCT * _VM.Data.NBEG_BOOK_VALUE * decVal;
                decimal lnDenominator4 = _VM.Data.NBEG_BOOK_VALUE;
                if (lnDenominator4 == 0)
                {
                    _VM.Data.NYEAR_DEPR = 0;
                }
                else
                {
                    _VM.Data.NYEAR_DEPR = _VM.Data.NYEAR_DEPR_PCT * lnDenominator4 * decVal;
                }
                //_VM.Data.NLYEAR_DEPR = _VM.Data.NYEAR_DEPR_PCT * _VM.Data.NLBEG_BOOK_VALUE * decVal;
                decimal lnDenominator5 = _VM.Data.NLBEG_BOOK_VALUE;
                if (lnDenominator5 == 0)
                {
                    _VM.Data.NLYEAR_DEPR = 0;
                }
                else
                {
                    _VM.Data.NLYEAR_DEPR = _VM.Data.NYEAR_DEPR_PCT * lnDenominator5 * decVal;
                }
                //_VM.Data.NBYEAR_DEPR = _VM.Data.NYEAR_DEPR_PCT * _VM.Data.NBBEG_BOOK_VALUE * decVal;
                decimal lnDenominator6 = _VM.Data.NBBEG_BOOK_VALUE;
                if (lnDenominator6 == 0)
                {
                    _VM.Data.NBYEAR_DEPR = 0;
                }
                else
                {
                    _VM.Data.NBYEAR_DEPR = _VM.Data.NYEAR_DEPR_PCT * lnDenominator6 * decVal;
                }
            }
            



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
                // Get record from grid selection - convert directly to DTO
                var loGridRow = R_FrontUtility.ConvertObjectToObject<FAT0010002DTO>(eventArgs.Data);

                if (loGridRow != null && !string.IsNullOrWhiteSpace(loGridRow.CASSET_CODE))
                {
                    // Ensure we have required parameters
                    if (string.IsNullOrWhiteSpace(loGridRow.CREC_ID))
                    {
                        loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "PS002"));
                    }
                    else
                    {
                        await _VM.GetRecordAsync(loGridRow);

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
        /// NET4: conAssetInfo_R_Display (line 527-565)
        /// Converts string date fields (CINSERVICE_DATE, CSTART_DATE) to DateTime? for date pickers
        /// </summary>
        private async Task ConductorAssetInfo_R_Display(R_DisplayEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                if (_VM.Data != null)
                {
                    // Convert CINSERVICE_DATE (string) to DINSERVICE_DATE (DateTime?)
                    // NET4: If String.IsNullOrWhiteSpace(loEntity._CINSERVICE_DATE) Then dtpInServiceDate.Value = Nothing Else dtpInServiceDate.Value = Common.setDateTimeFromString(loEntity._CINSERVICE_DATE)
                    if (string.IsNullOrWhiteSpace(_VM.Data.CINSERVICE_DATE))
                    {
                        _VM.Data.DINSERVICE_DATE = null;
                    }
                    else
                    {
                        // Parse date from yyyyMMdd format
                        if (_VM.Data.CINSERVICE_DATE.Length == 8)
                        {
                            string lcYear = _VM.Data.CINSERVICE_DATE.Substring(0, 4);
                            string lcMonth = _VM.Data.CINSERVICE_DATE.Substring(4, 2);
                            string lcDay = _VM.Data.CINSERVICE_DATE.Substring(6, 2);
                            if (int.TryParse(lcYear, out int liYear) &&
                                int.TryParse(lcMonth, out int liMonth) &&
                                int.TryParse(lcDay, out int liDay))
                            {
                                _VM.Data.DINSERVICE_DATE = new DateTime(liYear, liMonth, liDay);
                            }
                            else
                            {
                                _VM.Data.DINSERVICE_DATE = null;
                            }
                        }
                        else
                        {
                            _VM.Data.DINSERVICE_DATE = null;
                        }
                    }

                    // Convert CSTART_DATE (string) to DSTART_DATE (DateTime?)
                    // NET4: If String.IsNullOrWhiteSpace(loEntity._CSTART_DATE) Then dtpStartDate.Value = Nothing Else dtpStartDate.Value = Common.setDateTimeFromString(loEntity._CSTART_DATE)
                    if (string.IsNullOrWhiteSpace(_VM.Data.CSTART_DATE))
                    {
                        _VM.Data.DSTART_DATE = null;
                    }
                    else
                    {
                        // Parse date from yyyyMMdd format
                        if (_VM.Data.CSTART_DATE.Length == 8)
                        {
                            string lcYear = _VM.Data.CSTART_DATE.Substring(0, 4);
                            string lcMonth = _VM.Data.CSTART_DATE.Substring(4, 2);
                            string lcDay = _VM.Data.CSTART_DATE.Substring(6, 2);
                            if (int.TryParse(lcYear, out int liYear) &&
                                int.TryParse(lcMonth, out int liMonth) &&
                                int.TryParse(lcDay, out int liDay))
                            {
                                _VM.Data.DSTART_DATE = new DateTime(liYear, liMonth, liDay);
                            }
                            else
                            {
                                _VM.Data.DSTART_DATE = null;
                            }
                        }
                        else
                        {
                            _VM.Data.DSTART_DATE = null;
                        }
                    }

                    // Calculate Initial Cost (NTRANSACTION_AMOUNT) from NLTRANSACTION_AMOUNT1
                    // NET4: NLTRANSACTION_AMOUNT1 = PNLRATE * spInitialCostAmnt.Value
                    // So: spInitialCostAmnt.Value = NLTRANSACTION_AMOUNT1 / PNLRATE
                    // In NET6: NTRANSACTION_AMOUNT = NLTRANSACTION_AMOUNT1 / LocalRate
                    if (_VM.LocalRate != 0 && _VM.Data.NLTRANSACTION_AMOUNT1 != 0)
                    {
                        _VM.Data.NTRANSACTION_AMOUNT = Math.Round(_VM.Data.NLTRANSACTION_AMOUNT1 / _VM.LocalRate, 2);
                    }
                    else if (_VM.Data.NLTRANSACTION_AMOUNT1 == 0)
                    {
                        _VM.Data.NTRANSACTION_AMOUNT = 0;
                    }
                    // If LocalRate is 0, keep NTRANSACTION_AMOUNT as is (should not happen in normal cases)

                    // Set Beg Book Value from Book Value when displaying
                    // NET4: spLocalBegBookVal.Value = spBookValueLocalAmnt.Value (line 2228)
                    // NET4: spBaseBegBookVal.Value = spBookValueBaseAmnt.Value (line 2229)
                    // This happens in LostFocus event in NET4, but for display we should set it from Book Value
                    // Only set if Book Value is not zero (to preserve existing Beg Book Value if Book Value is zero)
                    if (_VM.Data.NLBOOK_VALUE != 0)
                    {
                        _VM.Data.NLBEG_BOOK_VALUE = _VM.Data.NLBOOK_VALUE;
                    }
                    if (_VM.Data.NBBOOK_VALUE != 0)
                    {
                        _VM.Data.NBBEG_BOOK_VALUE = _VM.Data.NBBOOK_VALUE;
                    }
                }

                CalculateYearlyDepreciationProcess(_VM.Data.CDEPR_METHOD);

                // Refresh Expense Allocation tab page with current data (equivalent to InvokeRefreshTabPageAsync in FAT00100)
                // Check if conductor is in Normal mode and Expense Allocation tab is active
                if (_conductorAssetInfoRef != null && _conductorAssetInfoRef.R_ConductorMode == R_eConductorMode.Normal)
                {
                    // Check if Expense Allocation tab is active (equivalent to _TabGeneral.ActiveTab.Id == "ExpenseAllocation" in FAT00100)
                    if (tabStripRef?.ActiveTab?.Id == nameof(FAT0010002ExpenseAllocation) && _tabPageExpenseAllocation != null)
                    {
                        // Store reference to avoid null reference warning
                        var loTabPageExpenseAllocation = _tabPageExpenseAllocation;

                        // Get current data from conductor (equivalent to loTempParamUtility in FAT00100)
                        var loTempParam = _conductorAssetInfoRef!.R_GetCurrentData();

                        // Convert to FAT0010002DTO for Expense Allocation tab parameter
                        FAT0010002DTO loParam;
                        if (loTempParam is FAT0010002DTO loDTO)
                        {
                            loParam = loDTO;
                        }
                        else if (loTempParam != null)
                        {
                            // Convert if needed - loTempParam is not null here due to the if condition
                            loParam = R_FrontUtility.ConvertObjectToObject<FAT0010002DTO>(loTempParam) ?? new FAT0010002DTO();
                        }
                        else
                        {
                            // Use Data if no data from conductor
                            loParam = _VM?.Data ?? new FAT0010002DTO();
                        }

                        // Refresh Expense Allocation tab page with current data
                        // loTabPageExpenseAllocation is not null here due to the null check above
                        await loTabPageExpenseAllocation!.InvokeRefreshTabPageAsync(loParam);
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
        /// Conductor - After Add
        /// Based on NET4: conAssetInfo_R_AfterAdd (line 358-419)
        /// </summary>
        private async void ConductorAssetInfo_R_AfterAdd(R_AfterAddEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {

                var loEntity = (FAT0010002DTO)eventArgs.Data;
                // Set transaction date to current date
                loEntity.DINSERVICE_DATE = DateTime.Now;
                loEntity.CINSERVICE_DATE = DateTime.Now.ToString("yyyyMMdd");
                loEntity.DSTART_DATE = loEntity.DINSERVICE_DATE;
                loEntity.CSTART_DATE = DateTime.Now.ToString("yyyyMMdd");
                loEntity.LNEW_FLAG = true;

                // Set default department from parent form
                //if (!string.IsNullOrWhiteSpace(_VM.TransDetailData.CDEPT_CODE))
                //{
                //    loEntity.CASSET_DEPT_CODE = _VM.TransDetailData.CDEPT_CODE;
                //    loEntity.CASSET_DEPT_NAME = _VM.TransDetailData.CDEPT_NAME;
                //}

                var foundDept = _VM00100.DeptLookupList?.ToList().Find(x => x.CDEPT_CODE == _VM.DeptCodeDefault);
                if (foundDept != null)
                {
                    loEntity.CASSET_DEPT_CODE = foundDept.CDEPT_CODE;
                    loEntity.CASSET_DEPT_NAME = foundDept.CDEPT_NAME;
                }
                // Initialize depreciation fields
                if (_VM.ComboDepreciationMethodFirstItem != null)
                {
                    loEntity.CDEPR_METHOD = _VM.ComboDepreciationMethodFirstItem.CCODE; // Default to "No Depreciation"
                }
                loEntity.IUSEFUL_LIVE_YR = 0;
                loEntity.IUSEFUL_LIVE_MO = 0;
                loEntity.IREM_UL_YR = 0;
                loEntity.IREM_UL_MO = 0;
                loEntity.NYEAR_DEPR_PCT = 0;
                loEntity.NLYEAR_DEPR_AMT = 0;
                loEntity.NBYEAR_DEPR_AMT = 0;
                loEntity.NLRESIDUAL_VALUE = 0;
                loEntity.NBRESIDUAL_VALUE = 0;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        

        private async Task OnClickNextButton()
        {
            R_Exception loException = new R_Exception();
            try
            {
                //validation when next
                if (_VM == null || _VM.Data == null)
                {
                    loException.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "PS002"));
                }
                else
                {
                    // Validate Asset Code
                    if (string.IsNullOrWhiteSpace(_VM.Data.CASSET_CODE) && _VM.AssetIncrementFlag==false)
                    {
                        loException.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "Val_AssetCode"));
                    }

                    // Validate Asset Name
                    if (string.IsNullOrWhiteSpace(_VM.Data.CASSET_NAME))
                    {
                        loException.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "Val_AssetName"));
                    }

                    // Validate Asset Department
                    if (string.IsNullOrWhiteSpace(_VM.Data.CASSET_DEPT_CODE))
                    {
                        loException.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "Val_AssetDepartment"));
                    }

                    // Validate Asset Journal Group
                    if (string.IsNullOrWhiteSpace(_VM.Data.CJRNGRP_CODE))
                    {
                        loException.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "Val_AssetJournalGroup"));
                    }

                    // Validate Asset Category
                    if (string.IsNullOrWhiteSpace(_VM.Data.CCATEGORY_CODE))
                    {
                        loException.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "Val_AssetCategory"));
                    }

                    // Validate Asset Tax Category
                    if (string.IsNullOrWhiteSpace(_VM.Data.CTAX_CATEGORY_CODE))
                    {
                        loException.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "Val_AssetTaxCategory"));
                    }

                    // Validate Unit
                    if (_VM.Data.IQTY<=0m)
                    {
                        loException.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "val_Quantity"));
                    }

                    // Validate Unit
                    if (string.IsNullOrWhiteSpace(_VM.Data.CUNIT))
                    {
                        loException.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "Val_Unit"));
                    }

                    // Validate Location
                    if (string.IsNullOrWhiteSpace(_VM.Data.CLOCATION_ID))
                    {
                        loException.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "Val_Location"));
                    }
                }

                if (!loException.HasError)
                {
                    IsSuccess = true;
                    await tabStripRef.SetActiveTabAsync("DepreciationInfo");
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        private async Task OnClickPreviousButton()
        {
            R_Exception loException = new R_Exception();
            try
            {
                if (!loException.HasError)
                {
                    await tabStripRef.SetActiveTabAsync("AssetInfo");
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        private void ConductorAssetInfo_BeforeCancel(R_BeforeCancelEventArgs eventArgs)
        {
            IsSuccess = false;
        }

        private void OnActiveTabIndexChanging(R_TabStripActiveTabIndexChangingEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            try
            {
                if (_conductorAssetInfoRef.R_ConductorMode == R_eConductorMode.Add || _conductorAssetInfoRef.R_ConductorMode == R_eConductorMode.Edit)
                {
                    if (eventArgs.TabStripTab.Id == "DepreciationInfo" && IsSuccess)
                    {
                        IsSuccess = false;
                    }
                    else if (eventArgs.TabStripTab.Id == "AssetInfo")
                    {
                        // Allow navigation back to AssetInfo tab (Previous button)
                        // No need to cancel - allow the navigation
                    }
                    else
                    {
                        eventArgs.Cancel = true;
                    }
                }
                else
                {
                    if (eventArgs.TabStripTab.Id == "DepreciationInfo" || eventArgs.TabStripTab.Id == "AssetInfo")
                    {
                        if (IsCRUDMode)
                        {
                            eventArgs.Cancel = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                eventArgs.Cancel = true;
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        private async Task OnActiveTabIndexChanged(R_TabStripTab eventArgs)
        {
            if (_conductorAssetInfoRef.R_ConductorMode == R_eConductorMode.Add || _conductorAssetInfoRef.R_ConductorMode == R_eConductorMode.Edit)
            {
                if (eventArgs.Id == "DepreciationInfo")
                {
                    //await _taxIdRef.FocusAsync();
                    //loTenantProfileViewModel.Data.TaxInfo.CTENANT_ID = loTenantProfileViewModel.Data.Profile.CTENANT_ID;
                    //loTenantProfileViewModel.Data.TaxInfo.CTENANT_NAME = loTenantProfileViewModel.Data.Profile.CTENANT_NAME;
                    //loTenantProfileViewModel.Data.TaxInfo.CTAX_ADDRESS = loTenantProfileViewModel.Data.Profile.CADDRESS;
                }
            }
            if (_conductorAssetInfoRef.R_ConductorMode == R_eConductorMode.Normal)
            {
                //if (string.IsNullOrWhiteSpace(loTenantProfileViewModel.Data.TaxInfo.CID_EXPIRED_DATE) == false)
                //{
                //    loTenantProfileViewModel.DID_EXPIRED_DATE = DateTime.ParseExact(loTenantProfileViewModel.Data.TaxInfo.CID_EXPIRED_DATE, "yyyyMMdd", null);
                //}
                //else
                //{
                //    loTenantProfileViewModel.DID_EXPIRED_DATE = null;
                //}
            }
        }

        /// <summary>
        /// Conductor - Set Other (Button Enablement)
        /// NET4: Button enablement in R_SetHasData based on PCMODE, PCSTATUS, and PCFR_MODULE
        /// NET4: btnImportPJ.Enabled = PCMODE = "T" And PCSTATUS = "00" And PCFR_MODULE = "PJ"
        /// NET4: btnImportExist.Enabled = PCMODE = "T" And PCSTATUS = "00" And PCFR_MODULE = "FA"
        /// NET4: btnEditAllocExpense.Enabled = PCMODE = "T" And PCSTATUS = "00"
        /// 
        /// NET6: Button enablement is controlled via ViewModel properties (EnableImportPJ, EnableImportExisting, EnableEditAllocExpense)
        /// which are calculated based on Mode, Status, and FrModule. These properties are bound in the razor file.
        /// </summary>
        private async Task AssetInfoAndDepreciationInfo_SetOther(R_SetEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                IsCRUDMode = !eventArgs.Enable;
                
                // Button enablement is now controlled via ViewModel properties:
                // - EnableImportPJ: Mode == "T" && Status == "00" && FrModule == "PJ"
                // - EnableImportExisting: Mode == "T" && Status == "00" && FrModule == "FA"
                // - EnableEditAllocExpense: Mode == "T" && Status == "00"
                // These properties are bound in the razor file and will automatically update when Mode/Status/FrModule change.
                
                // Trigger property change notification if needed (properties are calculated, so they update automatically)
                await InvokeTabEventCallbackAsync(eventArgs.Enable);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task R_TabEventCallback(object poValue)
        {
            IsCRUDMode = !(bool)poValue;
            await InvokeTabEventCallbackAsync(poValue);
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
        /// Based on NET4: conAssetInfo_R_Validation (line 920-1038)
        /// </summary>
        private async void ConductorAssetInfo_R_Validation(R_ValidationEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                if (_VM.Data == null)
                    return;

                // Validate Department
                if (string.IsNullOrWhiteSpace(_VM.TransDetailData.CDEPT_CODE) && 
                    (_VM.TransDetailData == null || string.IsNullOrWhiteSpace(_VM.TransDetailData.CDEPT_CODE)))
                {
                    loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "Val_department"));
                }

                // Validate Reference Date
                if (string.IsNullOrWhiteSpace(_VM.TransDetailData?.CREF_DATE))
                {
                    loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "Val_ReferenceDate"));
                }

                // Validate Asset Code
                if (string.IsNullOrWhiteSpace(_VM.Data.CASSET_CODE) && _VM.AssetIncrementFlag == false)
                {
                    loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "Val_AssetCode"));
                }

                // Validate Description
                if (string.IsNullOrWhiteSpace(_VM.Data.CTRANS_DESC))
                {
                    loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "Val_Description"));
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
        /// Conductor - Saving
        /// Based on NET4: conAssetInfo_R_Saving (line 610-742)
        /// </summary>
        private void ConductorAssetInfo_R_Saving(R_SavingEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                
                var loEntity = (FAT0010002DTO)eventArgs.Data;

                // Set header fields
                loEntity.LASSET_INCREMENT_FLAG = _VM.AssetIncrementFlag;
                loEntity.CCOMPANY_ID = ClientHelper.CompanyId;
                loEntity.CUSER_ID = ClientHelper.UserId;

                if (_VM.TransDetailData != null)
                {
                    loEntity.CDEPT_CODE = _VM.TransDetailData.CDEPT_CODE;
                    loEntity.CREF_NO = _VM.TransDetailData.CREF_NO;
                    loEntity.CREF_DATE = _VM.TransDetailData.CREF_DATE;
                    loEntity.NLBASE_RATE = _VM.TransDetailData.NLBASE_RATE;
                    loEntity.NLCURRENCY_RATE = _VM.TransDetailData.NLCURRENCY_RATE;
                    loEntity.NBBASE_RATE = _VM.TransDetailData.NBBASE_RATE;
                    loEntity.NBCURRENCY_RATE = _VM.TransDetailData.NBCURRENCY_RATE;
                }
                // Set start date
                if (loEntity.DSTART_DATE.HasValue)
                {
                    loEntity.CSTART_DATE = loEntity.DSTART_DATE.Value.ToString("yyyyMMdd");
                }
                else
                {
                    loEntity.CSTART_DATE = string.Empty;
                }

                // Set in-service date
                if (loEntity.DINSERVICE_DATE.HasValue)
                {
                    loEntity.CINSERVICE_DATE = loEntity.DINSERVICE_DATE.Value.ToString("yyyyMMdd");
                }
                else
                {
                    loEntity.CINSERVICE_DATE = string.Empty;
                }
                //loEntity.NYEAR_DEPR_PCT = _VM.lnNYEAR_DEPR_PCT;

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
                var loEntity = (FAT0010002DTO)eventArgs.Data;

                // Ensure entity is not null
                if (loEntity == null)
                {
                    loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "PS002"));
                }
                else
                {

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
                    await _VM.GetTransDetailAsync(
                        ClientHelper.CompanyId,
                        ClientHelper.CultureUI?.TwoLetterISOLanguageName ?? "en",
                        _VM.RecId,
                        _VM.DeptCode,
                        _VM.ReferenceNo
                    );
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        /// <summary>
        /// Conductor - Check Add
        /// NET4: plValid = PCMODE = "T" And PCSTATUS = "00" And PCFR_MODULE = "FA"
        /// </summary>
        private void ConductorAssetInfo_R_CheckAdd(R_CheckAddEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                // NET4: plValid = PCMODE = "T" And PCSTATUS = "00" And PCFR_MODULE = "FA"
                // Allow add when: Mode = "T" (Transaction mode), Status = "00" (Draft), FrModule = "FA" (Fixed Asset), and conductor is in Normal mode
                eventArgs.Allow = _VM.Mode == "T" 
                               && _VM.Status == "00" 
                               && _VM.FrModule == "FA"
                               && _conductorAssetInfoRef?.R_ConductorMode == R_eConductorMode.Normal;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        /// <summary>
        /// Conductor - Check Edit
        /// NET4: plValid = PCMODE = "T" And PCSTATUS = "00"
        /// </summary>
        private void ConductorAssetInfo_R_CheckEdit(R_CheckEditEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                // NET4: plValid = PCMODE = "T" And PCSTATUS = "00"
                // Allow edit when: Mode = "T" (Transaction mode), Status = "00" (Draft), and conductor is in Normal mode
                eventArgs.Allow = _VM.Mode == "T" 
                               && _VM.Status == "00"
                               && _conductorAssetInfoRef?.R_ConductorMode == R_eConductorMode.Normal;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        /// <summary>
        /// Conductor - Check Delete
        /// NET4: plValid = PCMODE = "T" And PCSTATUS = "00"
        /// </summary>
        private void ConductorAssetInfo_R_CheckDelete(R_CheckDeleteEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                // NET4: plValid = PCMODE = "T" And PCSTATUS = "00"
                // Allow delete when: Mode = "T" (Transaction mode), Status = "00" (Draft), and conductor is in Normal mode
                eventArgs.Allow = _VM.Mode == "T" 
                               && _VM.Status == "00"
                               && _conductorAssetInfoRef?.R_ConductorMode == R_eConductorMode.Normal;
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
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        #endregion

        #region Expense Allocation Tab Handlers

        /// <summary>
        /// Before opening Expense Allocation tab - sets up parameters
        /// Follows FAT00100AssetList pattern: passes DTO as parameter
        /// </summary>
        private void BeforeOpenExpenseAllocation(R_BeforeOpenTabPageEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                // Pass current asset data DTO as parameter to the ExpenseAllocation component
                // Following FAT00100AssetList pattern: pass DTO, not ViewModel
                eventArgs.Parameter = _VM?.Data ?? new FAT0010002DTO();
                eventArgs.TargetPageType = typeof(FAT0010002ExpenseAllocation);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        /// <summary>
        /// After opening Expense Allocation tab - handles post-open logic
        /// </summary>
        private async Task AfterOpenExpenseAllocation(R_AfterOpenTabPageEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                // Load expense allocation data when tab is opened
                // NET4: gvAllocExpense.R_RefreshGrid(loParam) is called when tab is activated
                if (_tabPageExpenseAllocation != null && _VM?.Data != null)
                {
                    // The component will be initialized via R_Init_From_Master with the parameter
                    // Additional refresh can be done here if needed
                    await Task.CompletedTask;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        /// <summary>
        /// Expense Allocation tab event callback - handles callbacks from the ExpenseAllocation component
        /// </summary>
        private void ExpenseAllocationTabEventCallBack(object poParam)
        {
            var loEx = new R_Exception();
            try
            {
                // Handle callbacks from ExpenseAllocation component if needed
                // For now, no specific callback handling required
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        

        

        /// <summary>
        /// Handle yearly depreciation percentage changed event
        /// </summary>
        private void OnYearlyDepreciationPctChanged(decimal value)
        {
            //if (_VM.Data != null)
            //{
            //    _VM.Data.NYEAR_DEPR_PCT = value;
            //}
        }

        private async Task OnChangeInputFile(InputFileChangeEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loByteFile = await R_FrontUtility.ConvertStreamToByteAsync(eventArgs.File.OpenReadStream());
                string loFile = eventArgs.File.Name;

                _VM.Data.OIMAGE = loByteFile;
                _VM.Data.CFILE_NAME = Path.GetFileNameWithoutExtension(loFile);
                _VM.Data.CFILE_EXTENSION = Path.GetExtension(loFile);
            }
            catch (Exception ex)
            {
                if (IsErrorEmptyFile)
                {
                    await R_MessageBox.Show("", "File is Empty", R_eMessageBoxButtonType.OK);
                }
                else
                {
                    loEx.Add(ex);
                }
            }
            loEx.ThrowExceptionIfErrors();
        }

        #endregion
    }
}

