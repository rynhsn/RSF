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

        private bool IsSuccess { get; set; } = false;
        private bool IsCRUDMode = true;
        private bool IsNormalMode = true;
        private R_TabStrip tabStripRef;
        private R_TabStripTab? _tabExpenseAllocation;
        private R_TabPage? _tabPageExpenseAllocation;

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

        #region Business Process Methods - Amount Calculation and Depreciation

        /// <summary>
        /// Recalculate amount and depreciation based on flag
        /// Based on NET4: RecalculateAmountDepreciation (line 1375-1545)
        /// </summary>
        private async Task RecalculateAmountDepreciationAsync(string pcFlagDeprAmnt)
        {
            var loEx = new R_Exception();

            try
            {
                if (_VM.Data == null)
                    return;

                // Get rates from ViewModel
                decimal lnLocalRate = _VM.LocalRate;
                decimal lnBaseRate = _VM.BaseRate;
                decimal lnBaseXRate = _VM.BaseXRate;

                switch (pcFlagDeprAmnt)
                {
                    case "InitialCost":
                        // Calculate transaction amounts from initial cost
                        decimal lnInitialCost = _VM.Data.NTRANSACTION_AMOUNT;
                        _VM.Data.NLTRANSACTION_AMOUNT1 = Math.Round(lnLocalRate * lnInitialCost, 2);
                        _VM.Data.NBTRANSACTION_AMOUNT1 = Math.Round(lnBaseRate * lnInitialCost, 2);

                        // Calculate book values
                        _VM.Data.NLBOOK_VALUE = _VM.Data.NLTRANSACTION_AMOUNT1 + _VM.Data.NLTRANSACTION_AMOUNT2
                            - _VM.Data.NLTRANSACTION_AMOUNT3 - _VM.Data.NLTRANSACTION_AMOUNT4 - _VM.Data.NLTRANSACTION_AMOUNT5;
                        _VM.Data.NBBOOK_VALUE = _VM.Data.NBTRANSACTION_AMOUNT1 + _VM.Data.NBTRANSACTION_AMOUNT2
                            - _VM.Data.NBTRANSACTION_AMOUNT3 - _VM.Data.NBTRANSACTION_AMOUNT4 - _VM.Data.NBTRANSACTION_AMOUNT5;

                        // Set beginning book values
                        if (_VM.Data.LNEW_FLAG)
                        {
                            _VM.Data.NOBBOOK_VALUE = _VM.Data.NBBOOK_VALUE;
                            _VM.Data.NOLBOOK_VALUE = _VM.Data.NLBOOK_VALUE;
                        }
                        else
                        {
                            // Beginning book value is user input (stored in NLBEG_BOOK_VALUE)
                            _VM.Data.NOLBOOK_VALUE = _VM.Data.NLBEG_BOOK_VALUE;
                            _VM.Data.NOBBOOK_VALUE = Math.Round(lnBaseXRate * _VM.Data.NLBEG_BOOK_VALUE, 2);
                        }

                        // Recalculate yearly depreciation
                        await RecalculateAmountDepreciationAsync("YearlyDepreciationLocal");
                        await RecalculateAmountDepreciationAsync("YearlyDepreciationBase");
                        await RecalculateAmountDepreciationAsync("DecliningDeprAmt");
                        break;

                    case "Addition":
                        // Calculate base transaction amount from local addition
                        decimal lnAdditionLocal = _VM.Data.NLADDITION_AMT;
                        _VM.Data.NBTRANSACTION_AMOUNT2 = Math.Round(lnBaseXRate * lnAdditionLocal, 2);

                        // Recalculate book values
                        _VM.Data.NLBOOK_VALUE = _VM.Data.NLTRANSACTION_AMOUNT1 + lnAdditionLocal
                            - _VM.Data.NLTRANSACTION_AMOUNT3 - _VM.Data.NLTRANSACTION_AMOUNT4 - _VM.Data.NLTRANSACTION_AMOUNT5;
                        _VM.Data.NBBOOK_VALUE = _VM.Data.NBTRANSACTION_AMOUNT1 + _VM.Data.NBTRANSACTION_AMOUNT2
                            - _VM.Data.NBTRANSACTION_AMOUNT3 - _VM.Data.NBTRANSACTION_AMOUNT4 - _VM.Data.NBTRANSACTION_AMOUNT5;
                        break;

                    case "Deduction":
                        // Calculate base transaction amount from local deduction
                        decimal lnDeductionLocal = _VM.Data.NLDEDUCTION_AMT;
                        _VM.Data.NBTRANSACTION_AMOUNT3 = Math.Round(lnBaseXRate * lnDeductionLocal, 2);

                        // Recalculate book values
                        _VM.Data.NLBOOK_VALUE = _VM.Data.NLTRANSACTION_AMOUNT1 + _VM.Data.NLTRANSACTION_AMOUNT2
                            - lnDeductionLocal - _VM.Data.NLTRANSACTION_AMOUNT4 - _VM.Data.NLTRANSACTION_AMOUNT5;
                        _VM.Data.NBBOOK_VALUE = _VM.Data.NBTRANSACTION_AMOUNT1 + _VM.Data.NBTRANSACTION_AMOUNT2
                            - _VM.Data.NBTRANSACTION_AMOUNT3 - _VM.Data.NBTRANSACTION_AMOUNT4 - _VM.Data.NBTRANSACTION_AMOUNT5;
                        break;

                    case "PriorDepr":
                        // Calculate base transaction amount from local prior depreciation
                        decimal lnPriorDeprLocal = _VM.Data.NLPRIOR_DEPR_AMT;
                        _VM.Data.NBTRANSACTION_AMOUNT4 = Math.Round(lnBaseXRate * lnPriorDeprLocal, 2);

                        // Recalculate book values
                        _VM.Data.NLBOOK_VALUE = _VM.Data.NLTRANSACTION_AMOUNT1 + _VM.Data.NLTRANSACTION_AMOUNT2
                            - _VM.Data.NLTRANSACTION_AMOUNT3 - lnPriorDeprLocal - _VM.Data.NLTRANSACTION_AMOUNT5;
                        _VM.Data.NBBOOK_VALUE = _VM.Data.NBTRANSACTION_AMOUNT1 + _VM.Data.NBTRANSACTION_AMOUNT2
                            - _VM.Data.NBTRANSACTION_AMOUNT3 - _VM.Data.NBTRANSACTION_AMOUNT4 - _VM.Data.NBTRANSACTION_AMOUNT5;
                        break;

                    case "YTDDepr":
                        // Calculate base transaction amount from local YTD depreciation
                        decimal lnYTDDeprLocal = _VM.Data.NLYTD_DEPR_AMT;
                        _VM.Data.NBTRANSACTION_AMOUNT5 = Math.Round(lnBaseXRate * lnYTDDeprLocal, 2);

                        // Recalculate book values
                        _VM.Data.NLBOOK_VALUE = _VM.Data.NLTRANSACTION_AMOUNT1 + _VM.Data.NLTRANSACTION_AMOUNT2
                            - _VM.Data.NLTRANSACTION_AMOUNT3 - _VM.Data.NLTRANSACTION_AMOUNT4 - lnYTDDeprLocal;
                        _VM.Data.NBBOOK_VALUE = _VM.Data.NBTRANSACTION_AMOUNT1 + _VM.Data.NBTRANSACTION_AMOUNT2
                            - _VM.Data.NBTRANSACTION_AMOUNT3 - _VM.Data.NBTRANSACTION_AMOUNT4 - _VM.Data.NBTRANSACTION_AMOUNT5;
                        break;

                    case "StartDate":
                        // Set start date based on depreciation method
                        if (_VM.Data.CDEPR_METHOD == "0")
                        {
                            _VM.Data.DSTART_DATE = null;
                        }
                        else if (_VM.Data.DINSERVICE_DATE.HasValue && _VM.HeaderData != null)
                        {
                            DateTime? ldInServiceDate = _VM.Data.DINSERVICE_DATE;
                            DateTime? ldTransactionDate = null;

                            if (!string.IsNullOrWhiteSpace(_VM.HeaderData.CTRANSACTION_DATE) &&
                                _VM.HeaderData.CTRANSACTION_DATE.Length == 8)
                            {
                                string lcYear = _VM.HeaderData.CTRANSACTION_DATE.Substring(0, 4);
                                string lcMonth = _VM.HeaderData.CTRANSACTION_DATE.Substring(4, 2);
                                string lcDay = _VM.HeaderData.CTRANSACTION_DATE.Substring(6, 2);
                                if (int.TryParse(lcYear, out int liYear) &&
                                    int.TryParse(lcMonth, out int liMonth) &&
                                    int.TryParse(lcDay, out int liDay))
                                {
                                    ldTransactionDate = new DateTime(liYear, liMonth, liDay);
                                }
                            }

                            if (ldInServiceDate.HasValue && ldTransactionDate.HasValue)
                            {
                                string lcInServiceDateStr = ldInServiceDate.Value.ToString("yyyyMMdd");
                                string lcTransactionDateStr = ldTransactionDate.Value.ToString("yyyyMMdd");
                                if (lcInServiceDateStr.CompareTo(lcTransactionDateStr) > 0)
                                {
                                    _VM.Data.DSTART_DATE = ldInServiceDate;
                                }
                                else
                                {
                                    _VM.Data.DSTART_DATE = ldTransactionDate;
                                }
                            }
                        }
                        break;

                    case "ResidualValue":
                        if (_VM.Data.CDEPR_METHOD == "0")
                        {
                            _VM.Data.NLRESIDUAL_VALUE = 0;
                            _VM.Data.NBRESIDUAL_VALUE = 0;
                        }
                        else
                        {
                            decimal lnResidualValueLocal = _VM.Data.NLRESIDUAL_VALUE;
                            _VM.Data.NBRESIDUAL_VALUE = Math.Round(lnBaseXRate * lnResidualValueLocal, 2);
                        }
                        break;

                    case "UsefulYears":
                        if (_VM.Data.CDEPR_METHOD == "0")
                        {
                            _VM.Data.IUSEFUL_LIVE_YR = 0;
                        }
                        else if (_VM.Data.NYEAR_DEPR_PCT > 0)
                        {
                            decimal lnFactor = _VM.Data.CDEPR_METHOD == "3" ? 200 : 100;
                            _VM.Data.IUSEFUL_LIVE_YR = (int)Math.Floor(lnFactor / _VM.Data.NYEAR_DEPR_PCT);
                        }
                        break;

                    case "UsefulMonths":
                        if (_VM.Data.CDEPR_METHOD == "0")
                        {
                            _VM.Data.IUSEFUL_LIVE_MO = 0;
                        }
                        else if (_VM.Data.NYEAR_DEPR_PCT > 0)
                        {
                            decimal lnFactor = _VM.Data.CDEPR_METHOD == "3" ? 200 : 100;
                            _VM.Data.IUSEFUL_LIVE_MO = (int)Math.Round((lnFactor / _VM.Data.NYEAR_DEPR_PCT * 12) % 12);
                        }
                        break;

                    case "YearlyDepreciation%":
                        if (_VM.Data.CDEPR_METHOD == "0" || _VM.Data.CDEPR_METHOD == "9")
                        {
                            _VM.Data.NYEAR_DEPR_PCT = 0;
                        }
                        else if (_VM.Data.IUSEFUL_LIVE_YR > 0 || _VM.Data.IUSEFUL_LIVE_MO > 0)
                        {
                            decimal lnFactor = _VM.Data.CDEPR_METHOD == "3" ? 200 : 100;
                            decimal lnUsefulLifeYears = _VM.Data.IUSEFUL_LIVE_YR + (_VM.Data.IUSEFUL_LIVE_MO / 12m);
                            if (lnUsefulLifeYears > 0)
                            {
                                _VM.Data.NYEAR_DEPR_PCT = lnFactor / lnUsefulLifeYears;
                            }
                        }
                        break;

                    case "YearlyDepreciationLocal":
                        if (_VM.Data.CDEPR_METHOD == "0" || _VM.Data.CDEPR_METHOD == "9")
                        {
                            _VM.Data.NLYEAR_DEPR_AMT = 0;
                        }
                        else if (_VM.Data.IUSEFUL_LIVE_YR > 0 || _VM.Data.IUSEFUL_LIVE_MO > 0)
                        {
                            decimal lnUsefulLifeYears = _VM.Data.IUSEFUL_LIVE_YR + (_VM.Data.IUSEFUL_LIVE_MO / 12m);
                            decimal lnResidualValue = _VM.Data.CDEPR_METHOD == "1" ? _VM.Data.NLRESIDUAL_VALUE : 0;
                            decimal lnFactor = _VM.Data.CDEPR_METHOD == "3" ? 2 : 1;
                            if (lnUsefulLifeYears > 0)
                            {
                                _VM.Data.NLYEAR_DEPR_AMT = Math.Round((_VM.Data.NLBEG_BOOK_VALUE - lnResidualValue) / lnUsefulLifeYears * lnFactor, 2);
                            }
                        }
                        break;

                    case "YearlyDepreciationBase":
                        if (_VM.Data.CDEPR_METHOD == "0" || _VM.Data.CDEPR_METHOD == "9")
                        {
                            _VM.Data.NBYEAR_DEPR_AMT = 0;
                        }
                        else if (_VM.Data.IUSEFUL_LIVE_YR > 0 || _VM.Data.IUSEFUL_LIVE_MO > 0)
                        {
                            decimal lnUsefulLifeYears = _VM.Data.IUSEFUL_LIVE_YR + (_VM.Data.IUSEFUL_LIVE_MO / 12m);
                            decimal lnResidualValue = _VM.Data.CDEPR_METHOD == "1" ? _VM.Data.NBRESIDUAL_VALUE : 0;
                            decimal lnFactor = _VM.Data.CDEPR_METHOD == "3" ? 2 : 1;
                            if (lnUsefulLifeYears > 0)
                            {
                                _VM.Data.NBYEAR_DEPR_AMT = Math.Round((_VM.Data.NBBEG_BOOK_VALUE - lnResidualValue) / lnUsefulLifeYears * lnFactor, 2);
                            }
                        }
                        break;

                    case "RemUsefulYears":
                        if (_VM.Data.CDEPR_METHOD == "0")
                        {
                            _VM.Data.IREM_UL_YR = 0;
                        }
                        else if (_VM.Data.LNEW_FLAG)
                        {
                            _VM.Data.IREM_UL_YR = _VM.Data.IUSEFUL_LIVE_YR;
                        }
                        break;

                    case "RemUsefulMonths":
                        if (_VM.Data.CDEPR_METHOD == "0")
                        {
                            _VM.Data.IREM_UL_MO = 0;
                        }
                        else if (_VM.Data.LNEW_FLAG)
                        {
                            _VM.Data.IREM_UL_MO = _VM.Data.IUSEFUL_LIVE_MO;
                        }
                        break;

                    case "DecliningDeprAmt":
                        if (_VM.Data.LNEW_FLAG && (_VM.Data.CDEPR_METHOD == "2" || _VM.Data.CDEPR_METHOD == "3"))
                        {
                            if (_VM.Data.IREM_UL_YR > 0 && _VM.Data.IUSEFUL_LIVE_YR > 0 && _VM.Data.NLBEG_BOOK_VALUE > 0)
                            {
                                var loParam = new FAT0010002GetDecliningDeprAmtParameterDTO
                                {
                                    CDEPR_METHOD = _VM.Data.CDEPR_METHOD,
                                    IBEG_UL_YR = _VM.Data.IUSEFUL_LIVE_YR,
                                    IBEG_UL_MO = _VM.Data.IUSEFUL_LIVE_MO,
                                    IREM_UL_YR = _VM.Data.IREM_UL_YR,
                                    IREM_UL_MO = _VM.Data.IREM_UL_MO,
                                    NBEG_BOOK_VAL = _VM.Data.NLBEG_BOOK_VALUE
                                };

                                var loTask = _VM.GetDecliningDeprAmtAsync(
                                    ClientHelper.CompanyId,
                                    ClientHelper.CultureUI?.TwoLetterISOLanguageName ?? "en",
                                    loParam
                                );
                                loTask.Wait();
                                decimal lnDecliningDeprAmt = loTask.Result;

                                if (lnDecliningDeprAmt == 0)
                                {
                                    _VM.Data.NLYEAR_DEPR_AMT = 0;
                                    _VM.Data.NBYEAR_DEPR_AMT = 0;
                                }
                                else
                                {
                                    _VM.Data.NLYEAR_DEPR_AMT = lnDecliningDeprAmt;
                                    _VM.Data.NBYEAR_DEPR_AMT = Math.Round(lnBaseXRate * lnDecliningDeprAmt, 2);
                                }
                            }
                            else
                            {
                                _VM.Data.NLYEAR_DEPR_AMT = 0;
                                _VM.Data.NBYEAR_DEPR_AMT = 0;
                            }
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        /// <summary>
        /// Handle depreciation method selection change
        /// Based on NET4: ddDepreciationMethod_SelectedValueChanged (line 1613-1665)
        /// </summary>
        private async Task OnDepreciationMethodChanged(string? value)
        {
            var loEx = new R_Exception();

            try
            {
                if (_conductorAssetInfoRef == null || _VM.Data == null)
                    return;

                if (_conductorAssetInfoRef.R_ConductorMode != R_eConductorMode.Add &&
                    _conductorAssetInfoRef.R_ConductorMode != R_eConductorMode.Edit)
                    return;

                if (string.IsNullOrWhiteSpace(value))
                {
                    value = "0"; // Default to "No Depreciation"
                }

                _VM.Data.CDEPR_METHOD = value;

                // Set default values based on method
                if (value == "0")
                {
                    _VM.Data.NYEAR_DEPR_PCT = 0;
                    _VM.Data.IUSEFUL_LIVE_YR = 0;
                    _VM.Data.IUSEFUL_LIVE_MO = 0;
                    _VM.Data.IREM_UL_YR = 0;
                    _VM.Data.IREM_UL_MO = 0;
                }
                else if (value == "1" || value == "9")
                {
                    _VM.Data.NYEAR_DEPR_PCT = 0;
                    _VM.Data.IUSEFUL_LIVE_YR = 10;
                    _VM.Data.IUSEFUL_LIVE_MO = 0;
                }

                // Recalculate all depreciation fields
                await RecalculateAmountDepreciationAsync("StartDate");
                await RecalculateAmountDepreciationAsync("ResidualValue");
                await RecalculateAmountDepreciationAsync("UsefulYears");
                await RecalculateAmountDepreciationAsync("UsefulMonths");
                await RecalculateAmountDepreciationAsync("YearlyDepreciation%");
                await RecalculateAmountDepreciationAsync("YearlyDepreciationLocal");
                await RecalculateAmountDepreciationAsync("YearlyDepreciationBase");
                await RecalculateAmountDepreciationAsync("DecliningDeprAmt");
                await RecalculateAmountDepreciationAsync("RemUsefulYears");
                await RecalculateAmountDepreciationAsync("RemUsefulMonths");
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        /// <summary>
        /// Handle initial cost amount change
        /// Based on NET4: spInitialCostAmnt_LostFocus (line 1547-1555)
        /// </summary>
        private async Task OnInitialCostAmountChanged(decimal value)
        {
            var loEx = new R_Exception();

            try
            {
                if (_conductorAssetInfoRef == null)
                    return;

                if (_conductorAssetInfoRef.R_ConductorMode == R_eConductorMode.Add ||
                    _conductorAssetInfoRef.R_ConductorMode == R_eConductorMode.Edit)
                {
                    await RecalculateAmountDepreciationAsync("InitialCost");

                    // Set Beg Book Value from Book Value when Initial Cost changes
                    // NET4: spLocalBegBookVal.Value = spBookValueLocalAmnt.Value (line 2228)
                    // NET4: spBaseBegBookVal.Value = spBookValueBaseAmnt.Value (line 2229)
                    // This happens in LostFocus event for Book Value, but we also need to do it when Initial Cost changes
                    if (_VM.Data != null)
                    {
                        if (_VM.Data.NLBOOK_VALUE != 0)
                        {
                            _VM.Data.NLBEG_BOOK_VALUE = _VM.Data.NLBOOK_VALUE;
                        }
                        if (_VM.Data.NBBOOK_VALUE != 0)
                        {
                            _VM.Data.NBBEG_BOOK_VALUE = _VM.Data.NBBOOK_VALUE;
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

        /// <summary>
        /// Handle addition amount change
        /// Based on NET4: spAdditionLocalAmnt_LostFocus (line 1557-1562)
        /// </summary>
        private async Task OnAdditionAmountChanged()
        {
            var loEx = new R_Exception();

            try
            {
                if (_conductorAssetInfoRef == null)
                    return;

                if (_conductorAssetInfoRef.R_ConductorMode == R_eConductorMode.Add ||
                    _conductorAssetInfoRef.R_ConductorMode == R_eConductorMode.Edit)
                {
                    await RecalculateAmountDepreciationAsync("Addition");
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        /// <summary>
        /// Handle deduction amount change
        /// Based on NET4: spDeductionLocalAmnt_LostFocus (line 1564-1569)
        /// </summary>
        private async Task OnDeductionAmountChanged()
        {
            var loEx = new R_Exception();

            try
            {
                if (_conductorAssetInfoRef == null)
                    return;

                if (_conductorAssetInfoRef.R_ConductorMode == R_eConductorMode.Add ||
                    _conductorAssetInfoRef.R_ConductorMode == R_eConductorMode.Edit)
                {
                    await RecalculateAmountDepreciationAsync("Deduction");
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        /// <summary>
        /// Handle prior depreciation amount change
        /// Based on NET4: spPriorDeprLocalAmnt_LostFocus (line 1571-1576)
        /// </summary>
        private async Task OnPriorDeprAmountChanged()
        {
            var loEx = new R_Exception();

            try
            {
                if (_conductorAssetInfoRef == null)
                    return;

                if (_conductorAssetInfoRef.R_ConductorMode == R_eConductorMode.Add ||
                    _conductorAssetInfoRef.R_ConductorMode == R_eConductorMode.Edit)
                {
                    await RecalculateAmountDepreciationAsync("PriorDepr");
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        /// <summary>
        /// Handle YTD depreciation amount change
        /// Based on NET4: spYTDDeprLocalAmnt_LostFocus (line 1578-1583)
        /// </summary>
        private async Task OnYTDDeprAmountChanged()
        {
            var loEx = new R_Exception();

            try
            {
                if (_conductorAssetInfoRef == null)
                    return;

                if (_conductorAssetInfoRef.R_ConductorMode == R_eConductorMode.Add ||
                    _conductorAssetInfoRef.R_ConductorMode == R_eConductorMode.Edit)
                {
                    await RecalculateAmountDepreciationAsync("YTDDepr");
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        /// <summary>
        /// Handle residual value change
        /// Based on NET4: spResidualValueLocalAmnt_LostFocus (line 1671+)
        /// </summary>
        private async Task OnResidualValueChanged()
        {
            var loEx = new R_Exception();

            try
            {
                if (_conductorAssetInfoRef == null)
                    return;

                if (_conductorAssetInfoRef.R_ConductorMode == R_eConductorMode.Add ||
                    _conductorAssetInfoRef.R_ConductorMode == R_eConductorMode.Edit)
                {
                    await RecalculateAmountDepreciationAsync("ResidualValue");
                    await RecalculateAmountDepreciationAsync("YearlyDepreciationLocal");
                    await RecalculateAmountDepreciationAsync("YearlyDepreciationBase");
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        /// <summary>
        /// Handle residual value amount change (for ValueChanged event)
        /// Based on NET4: spResidualValueLocalAmnt_LostFocus
        /// </summary>
        private async Task OnResidualValueAmountChanged(decimal value)
        {
            var loEx = new R_Exception();

            try
            {
                if (_conductorAssetInfoRef == null || _VM.Data == null)
                    return;

                _VM.Data.NLRESIDUAL_VALUE = value;

                if (_conductorAssetInfoRef.R_ConductorMode == R_eConductorMode.Add ||
                    _conductorAssetInfoRef.R_ConductorMode == R_eConductorMode.Edit)
                {
                    await RecalculateAmountDepreciationAsync("ResidualValue");
                    await RecalculateAmountDepreciationAsync("YearlyDepreciationLocal");
                    await RecalculateAmountDepreciationAsync("YearlyDepreciationBase");
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        /// <summary>
        /// Handle useful life years change
        /// Based on NET4: spUserfulLifeYears_LostFocus (line 1704+)
        /// </summary>
        private async Task OnUsefulLifeYearsChanged(int value)
        {
            var loEx = new R_Exception();

            try
            {
                if (_conductorAssetInfoRef == null)
                    return;

                if (_conductorAssetInfoRef.R_ConductorMode == R_eConductorMode.Add ||
                    _conductorAssetInfoRef.R_ConductorMode == R_eConductorMode.Edit)
                {
                    await RecalculateAmountDepreciationAsync("YearlyDepreciation%");
                    await RecalculateAmountDepreciationAsync("YearlyDepreciationLocal");
                    await RecalculateAmountDepreciationAsync("YearlyDepreciationBase");
                    await RecalculateAmountDepreciationAsync("DecliningDeprAmt");
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        /// <summary>
        /// Handle useful life months change
        /// Based on NET4: spUserfulLifeMonths_LostFocus (line 1718+)
        /// </summary>
        private async Task OnUsefulLifeMonthsChanged(int value)
        {
            var loEx = new R_Exception();

            try
            {
                if (_conductorAssetInfoRef == null)
                    return;

                if (_conductorAssetInfoRef.R_ConductorMode == R_eConductorMode.Add ||
                    _conductorAssetInfoRef.R_ConductorMode == R_eConductorMode.Edit)
                {
                    await RecalculateAmountDepreciationAsync("YearlyDepreciation%");
                    await RecalculateAmountDepreciationAsync("YearlyDepreciationLocal");
                    await RecalculateAmountDepreciationAsync("YearlyDepreciationBase");
                    await RecalculateAmountDepreciationAsync("DecliningDeprAmt");
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        /// <summary>
        /// Handle yearly depreciation percentage change
        /// Based on NET4: spYearlyDepreciation_LostFocus (line 1731+)
        /// </summary>
        private async Task OnYearlyDepreciationPctChanged(decimal value)
        {
            var loEx = new R_Exception();

            try
            {
                if (_conductorAssetInfoRef == null)
                    return;

                if (_conductorAssetInfoRef.R_ConductorMode == R_eConductorMode.Add ||
                    _conductorAssetInfoRef.R_ConductorMode == R_eConductorMode.Edit)
                {
                    await RecalculateAmountDepreciationAsync("UsefulYears");
                    await RecalculateAmountDepreciationAsync("UsefulMonths");
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        /// <summary>
        /// Handle remaining useful life years change
        /// Based on NET4: spRemUsefulLifeYr_LostFocus (line 2266+)
        /// </summary>
        private async Task OnRemainingUsefulLifeYearsChanged(int value)
        {
            var loEx = new R_Exception();

            try
            {
                if (_conductorAssetInfoRef == null)
                    return;

                if (_conductorAssetInfoRef.R_ConductorMode == R_eConductorMode.Add ||
                    _conductorAssetInfoRef.R_ConductorMode == R_eConductorMode.Edit)
                {
                    await RecalculateAmountDepreciationAsync("RemUsefulYears");
                    await RecalculateAmountDepreciationAsync("DecliningDeprAmt");
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        /// <summary>
        /// Handle remaining useful life months change
        /// Based on NET4: spRemUsefulLifeMo_LostFocus (line 2241+)
        /// </summary>
        private async Task OnRemainingUsefulLifeMonthsChanged(int value)
        {
            var loEx = new R_Exception();

            try
            {
                if (_conductorAssetInfoRef == null || _VM.Data == null)
                    return;

                if (_conductorAssetInfoRef.R_ConductorMode == R_eConductorMode.Add ||
                    _conductorAssetInfoRef.R_ConductorMode == R_eConductorMode.Edit)
                {
                    // Validate: months must be between 0 and 11
                    if (_VM.Data.IREM_UL_MO < 0 || _VM.Data.IREM_UL_MO > 11)
                    {
                        _VM.Data.IREM_UL_MO = 0;
                        // NET4: PS045 error - but we'll let validation handle it
                    }

                    await RecalculateAmountDepreciationAsync("RemUsefulMonths");
                    await RecalculateAmountDepreciationAsync("DecliningDeprAmt");
                }
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
                // Initialize new record with default values
                if (_VM.Data != null)
                {
                    // Set header fields
                    _VM.Data.CDEPT_CODE = _VM.DeptCode;
                    _VM.Data.CTRANSACTION_CODE = _VM.TransactionCode;
                    _VM.Data.CREFERENCE_NO = _VM.ReferenceNo;
                    _VM.Data.CSTATUS = _VM.Status;
                    _VM.Data.CASSET_TRANS_SEQNO = "000100";
                    _VM.Data.CASSET_STATUS = "0";
                    _VM.Data.LNEW_FLAG = true;

                    // Set currency rates from header
                    if (_VM.HeaderData != null)
                    {
                        _VM.Data.NLBASE_RATE_AMOUNT = _VM.HeaderData.NLBASE_RATE_AMOUNT;
                        _VM.Data.NLCURRENCY_RATE_AMOUNT = _VM.HeaderData.NLCURRENCY_RATE_AMOUNT;
                        _VM.Data.NBBASE_RATE_AMOUNT = _VM.HeaderData.NBBASE_RATE_AMOUNT;
                        _VM.Data.NBCURRENCY_RATE_AMOUNT = _VM.HeaderData.NBCURRENCY_RATE_AMOUNT;
                        _VM.Data.CCURRENCY_CODE = _VM.HeaderData.CCURRENCY_CODE;
                    }

                    // Set default department from parent form
                    if (!string.IsNullOrWhiteSpace(_VM.DefaultAssetDeptCode))
                    {
                        _VM.Data.CASSET_DEPT_CODE = _VM.DefaultAssetDeptCode;
                    }
                    else if (!string.IsNullOrWhiteSpace(_VM.DeptCode))
                    {
                        _VM.Data.CASSET_DEPT_CODE = _VM.DeptCode;
                    }

                    // Set default in-service date to transaction date
                    if (_VM.HeaderData != null && !string.IsNullOrWhiteSpace(_VM.HeaderData.CTRANSACTION_DATE))
                    {
                        if (_VM.HeaderData.CTRANSACTION_DATE.Length == 8)
                        {
                            string lcYear = _VM.HeaderData.CTRANSACTION_DATE.Substring(0, 4);
                            string lcMonth = _VM.HeaderData.CTRANSACTION_DATE.Substring(4, 2);
                            string lcDay = _VM.HeaderData.CTRANSACTION_DATE.Substring(6, 2);
                            if (int.TryParse(lcYear, out int liYear) &&
                                int.TryParse(lcMonth, out int liMonth) &&
                                int.TryParse(lcDay, out int liDay))
                            {
                                _VM.Data.DINSERVICE_DATE = new DateTime(liYear, liMonth, liDay);
                            }
                        }
                    }

                    // Initialize depreciation fields
                    _VM.Data.CDEPR_METHOD = "0"; // Default to "No Depreciation"
                    _VM.Data.IUSEFUL_LIVE_YR = 0;
                    _VM.Data.IUSEFUL_LIVE_MO = 0;
                    _VM.Data.IREM_UL_YR = 0;
                    _VM.Data.IREM_UL_MO = 0;
                    _VM.Data.NYEAR_DEPR_PCT = 0;
                    _VM.Data.NLYEAR_DEPR_AMT = 0;
                    _VM.Data.NBYEAR_DEPR_AMT = 0;
                    _VM.Data.NLRESIDUAL_VALUE = 0;
                    _VM.Data.NBRESIDUAL_VALUE = 0;

                    // Recalculate start date
                    await RecalculateAmountDepreciationAsync("StartDate");
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
        /// Based on NET4: conAssetInfo_R_Saving (line 610-742)
        /// </summary>
        private void ConductorAssetInfo_R_Saving(R_SavingEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                if (_VM.Data == null)
                    return;

                var loEntity = _VM.Data;

                // Set header fields
                loEntity.LASSET_INCREMENT_FLAG = _VM.AssetIncrementFlag;
                loEntity.CCOMPANY_ID = ClientHelper.CompanyId;
                loEntity.CDEPT_CODE = _VM.DeptCode;
                loEntity.CASSET_DEPT_CODE = loEntity.CASSET_DEPT_CODE ?? string.Empty;
                loEntity.CTRANSACTION_CODE = _VM.TransactionCode;
                loEntity.CTRANS_DESCRIPTION = loEntity.CTRANSACTION_DESCR ?? string.Empty;

                // Set transaction date
                if (_VM.HeaderData != null && !string.IsNullOrWhiteSpace(_VM.HeaderData.CTRANSACTION_DATE))
                {
                    loEntity.CTRANSACTION_DATE = _VM.HeaderData.CTRANSACTION_DATE;
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

                // Set reference fields
                loEntity.CREFERENCE_NO = _VM.ReferenceNo;
                loEntity.CFOREIGN_LANGUAGE = ClientHelper.CultureUI?.TwoLetterISOLanguageName ?? "en";
                loEntity.CUSER_ID = ClientHelper.UserId;

                // Set currency rates from header
                if (_VM.HeaderData != null)
                {
                    loEntity.NLBASE_RATE_AMOUNT = _VM.HeaderData.NLBASE_RATE_AMOUNT;
                    loEntity.NLCURRENCY_RATE_AMOUNT = _VM.HeaderData.NLCURRENCY_RATE_AMOUNT;
                    loEntity.NBBASE_RATE_AMOUNT = _VM.HeaderData.NBBASE_RATE_AMOUNT;
                    loEntity.NBCURRENCY_RATE_AMOUNT = _VM.HeaderData.NBCURRENCY_RATE_AMOUNT;
                }

                // Set foreign reference fields
                loEntity.CFR_DEPT_CODE = _VM.FrDeptCode;
                loEntity.CFR_TRANSACTION_CODE = _VM.FrTransactionCode;
                loEntity.CFR_REFERENCE_NO = _VM.FrReferenceNo;
                if (_VM.FrModule != "FA")
                {
                    loEntity.CFR_TRANSACTION_DATE = _VM.DocumentDate;
                }
                else
                {
                    loEntity.CFR_TRANSACTION_DATE = string.Empty;
                }
                loEntity.CFR_MODULE = _VM.FrModule;
                loEntity.CDOCUMENT_DATE = _VM.DocumentDate;
                loEntity.CSUPPLIER_ID = _VM.SupplierId;
                loEntity.CSUPPLIER_NAME = _VM.SupplierName;

                // Set useful life calculations
                loEntity.IUSEFUL_LIVE = (_VM.Data.IREM_UL_YR * 12) + _VM.Data.IREM_UL_MO;
                loEntity.IBEG_USEFUL_LIVE = (_VM.Data.IUSEFUL_LIVE_YR * 12) + _VM.Data.IUSEFUL_LIVE_MO;

                // Set expense department
                loEntity.CEXPENSE_DEPT_CODE = loEntity.CASSET_DEPT_CODE ?? string.Empty;
                loEntity.CCREATE_BY = ClientHelper.UserId;
                loEntity.CUPDATE_BY = ClientHelper.UserId;
                loEntity.LNEW_FLAG = loEntity.LNEW_FLAG;

                // Set YTD depreciation amounts
                loEntity.NLYTD_DEPR_AMT = loEntity.NLTRANSACTION_AMOUNT5;
                loEntity.NBYTD_DEPR_AMT = loEntity.NBTRANSACTION_AMOUNT5;

                // Set purchase date
                if (string.IsNullOrWhiteSpace(_VM.DocumentDate))
                {
                    if (_VM.HeaderData != null && !string.IsNullOrWhiteSpace(_VM.HeaderData.CTRANSACTION_DATE))
                    {
                        loEntity.CPURCHASE_DATE = _VM.HeaderData.CTRANSACTION_DATE;
                    }
                }
                else
                {
                    loEntity.CPURCHASE_DATE = _VM.DocumentDate;
                }

                // Set book values
                loEntity.NLBEG_BOOK_VALUE = loEntity.NLBOOK_VALUE;
                loEntity.NBBEG_BOOK_VALUE = loEntity.NBBOOK_VALUE;
                loEntity.NLBEGINNING_AMT = loEntity.NLTRANSACTION_AMOUNT1;
                loEntity.NBBEGINNING_AMT = loEntity.NBTRANSACTION_AMOUNT1;
                loEntity.NLADDITION_AMT = loEntity.NLTRANSACTION_AMOUNT2;
                loEntity.NBADDITION_AMT = loEntity.NBTRANSACTION_AMOUNT2;
                loEntity.NLDEDUCTION_AMT = loEntity.NLTRANSACTION_AMOUNT3;
                loEntity.NBDEDUCTION_AMT = loEntity.NBTRANSACTION_AMOUNT3;
                loEntity.IBEGINNING_QTY = loEntity.ITRANSACTION_QTY1;
                loEntity.NLPRIOR_DEPR_AMT = loEntity.NLTRANSACTION_AMOUNT4;
                loEntity.NBPRIOR_DEPR_AMT = loEntity.NBTRANSACTION_AMOUNT4;
                loEntity.NOLBOOK_VALUE = loEntity.NLBEG_BOOK_VALUE;
                loEntity.NOBBOOK_VALUE = loEntity.NBBEG_BOOK_VALUE;
                loEntity.IOUSEFUL_LIVE = (_VM.Data.IUSEFUL_LIVE_YR * 12) + _VM.Data.IUSEFUL_LIVE_MO;
                loEntity.IOUSEFUL_LIVE_YR = _VM.Data.IUSEFUL_LIVE_YR;
                loEntity.IOUSEFUL_LIVE_MO = _VM.Data.IUSEFUL_LIVE_MO;
                loEntity.IUSEFUL_LIVE_MO = _VM.Data.IREM_UL_MO;
                loEntity.IUSEFUL_LIVE_YR = _VM.Data.IREM_UL_YR;
                loEntity.NYEAR_DEPR_PCT = _VM.Data.NYEAR_DEPR_PCT;
                loEntity.CLAST_TRANS_DATE = loEntity.CTRANSACTION_DATE;

                // Set sequence number for Add mode
                if (eventArgs.ConductorMode == R_eConductorMode.Add)
                {
                    loEntity.CLSEQUENCE_NO = "000100";
                    loEntity.CASSET_STATUS = "0";
                }

                // Calculate last base rate amounts
                if (_VM.HeaderData != null)
                {
                    decimal lnLocalBaseRate = _VM.HeaderData.NLBASE_RATE_AMOUNT;
                    decimal lnBaseCurrencyRate = _VM.HeaderData.NBCURRENCY_RATE_AMOUNT;
                    decimal lnBaseBaseRate = _VM.HeaderData.NBBASE_RATE_AMOUNT;
                    decimal lnLocalCurrencyRate = _VM.HeaderData.NLCURRENCY_RATE_AMOUNT;

                    if (lnLocalBaseRate * lnBaseCurrencyRate > lnBaseBaseRate * lnLocalCurrencyRate)
                    {
                        loEntity.NLAST_BBASE_RATE_AMOUNT = Math.Round(lnLocalBaseRate * lnBaseCurrencyRate / lnBaseBaseRate * lnLocalCurrencyRate);
                    }
                    else
                    {
                        loEntity.NLAST_BBASE_RATE_AMOUNT = 1;
                    }

                    if (lnLocalBaseRate * lnBaseCurrencyRate > lnBaseBaseRate * lnLocalCurrencyRate)
                    {
                        loEntity.NLAST_BCURRENCY_RATE_AMOUNT = 1;
                    }
                    else
                    {
                        loEntity.NLAST_BCURRENCY_RATE_AMOUNT = Math.Round(lnBaseBaseRate * lnLocalCurrencyRate / lnLocalBaseRate * lnBaseCurrencyRate);
                    }
                }

                loEntity.CLAST_CURR_RATE_DATE = loEntity.CTRANSACTION_DATE;

                // Calculate GL Link flag
                decimal lnGLINKVAL = loEntity.NTRANSACTION_AMOUNT + loEntity.NLTRANSACTION_AMOUNT1
                    + loEntity.NLTRANSACTION_AMOUNT2 - loEntity.NLTRANSACTION_AMOUNT3
                    - loEntity.NLTRANSACTION_AMOUNT4 - loEntity.NLTRANSACTION_AMOUNT5;

                string lcGLLinkDate = _VM.GLLinkDate;
                string lcTransactionDate = loEntity.CTRANSACTION_DATE;

                if (!string.IsNullOrWhiteSpace(lcGLLinkDate) && !string.IsNullOrWhiteSpace(lcTransactionDate))
                {
                    if (lcGLLinkDate.CompareTo(lcTransactionDate) <= 0 && lnGLINKVAL != 0)
                    {
                        loEntity.LGLLINK = true;
                        _VM.GLLink = true;
                    }
                    else
                    {
                        loEntity.LGLLINK = false;
                        _VM.GLLink = false;
                    }
                }
                else
                {
                    loEntity.LGLLINK = false;
                    _VM.GLLink = false;
                }
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
                //ProfileTaxDTO loParam = new ProfileTaxDTO();
                //loParam = (ProfileTaxDTO)_conductorProfileTaxRef.R_GetCurrentData();
                //loTenantProfileViewModel.TenantProfileValidation(loParam.Profile);
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
        /// Based on NET4: conAssetInfo_R_Validation (line 920-1038)
        /// </summary>
        private async void ConductorAssetInfo_R_Validation(R_ValidationEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                if (_VM.Data == null)
                    return;

                // Validate Asset Name
                if (string.IsNullOrWhiteSpace(_VM.Data.CASSET_NAME))
                {
                    loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "PS020"));
                }

                // Validate Asset Category Code
                if (string.IsNullOrWhiteSpace(_VM.Data.CCATEGORY_CODE))
                {
                    loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "PS021"));
                }

                // Validate Asset Department Code
                if (string.IsNullOrWhiteSpace(_VM.Data.CASSET_DEPT_CODE))
                {
                    loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "PS022"));
                }

                // Validate Asset Journal Group Code
                if (string.IsNullOrWhiteSpace(_VM.Data.CJRNGRP_CODE))
                {
                    loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "PS023"));
                }

                // Validate Asset Tax Category Code
                if (string.IsNullOrWhiteSpace(_VM.Data.CTAX_CATEGORY_CODE))
                {
                    loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "PS024"));
                }

                // Validate Quantity
                if (_VM.Data.ITRANSACTION_QTY1 == 0)
                {
                    loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "PS025"));
                }

                // Validate Unit
                if (string.IsNullOrWhiteSpace(_VM.Data.CUNIT))
                {
                    loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "PS026"));
                }

                // Validate In-Service Date
                if (!_VM.Data.DINSERVICE_DATE.HasValue)
                {
                    loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "PS027"));
                }

                // Validate In-Service Date not less than Transaction Date
                if (_VM.Data.DINSERVICE_DATE.HasValue && _VM.HeaderData != null)
                {
                    DateTime? ldInServiceDate = _VM.Data.DINSERVICE_DATE;
                    DateTime? ldTransactionDate = null;

                    if (!string.IsNullOrWhiteSpace(_VM.HeaderData.CTRANSACTION_DATE) &&
                        _VM.HeaderData.CTRANSACTION_DATE.Length == 8)
                    {
                        string lcYear = _VM.HeaderData.CTRANSACTION_DATE.Substring(0, 4);
                        string lcMonth = _VM.HeaderData.CTRANSACTION_DATE.Substring(4, 2);
                        string lcDay = _VM.HeaderData.CTRANSACTION_DATE.Substring(6, 2);
                        if (int.TryParse(lcYear, out int liYear) &&
                            int.TryParse(lcMonth, out int liMonth) &&
                            int.TryParse(lcDay, out int liDay))
                        {
                            ldTransactionDate = new DateTime(liYear, liMonth, liDay);
                        }
                    }

                    if (ldInServiceDate.HasValue && ldTransactionDate.HasValue)
                    {
                        string lcInServiceDateStr = ldInServiceDate.Value.ToString("yyyyMMdd");
                        string lcTransactionDateStr = ldTransactionDate.Value.ToString("yyyyMMdd");
                        if (!_VM.Data.LNEW_FLAG && lcInServiceDateStr.CompareTo(lcTransactionDateStr) < 0)
                        {
                            loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "PS028"));
                        }
                    }
                }

                // Validate Start Date if depreciation method is not "0"
                if (_VM.Data.CDEPR_METHOD != "0")
                {
                    if (!_VM.Data.DSTART_DATE.HasValue)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "PS029"));
                    }
                    else if (_VM.Data.DINSERVICE_DATE.HasValue && _VM.HeaderData != null)
                    {
                        DateTime? ldStartDate = _VM.Data.DSTART_DATE;
                        DateTime? ldInServiceDate = _VM.Data.DINSERVICE_DATE;
                        DateTime? ldTransactionDate = null;

                        if (!string.IsNullOrWhiteSpace(_VM.HeaderData.CTRANSACTION_DATE) &&
                            _VM.HeaderData.CTRANSACTION_DATE.Length == 8)
                        {
                            string lcYear = _VM.HeaderData.CTRANSACTION_DATE.Substring(0, 4);
                            string lcMonth = _VM.HeaderData.CTRANSACTION_DATE.Substring(4, 2);
                            string lcDay = _VM.HeaderData.CTRANSACTION_DATE.Substring(6, 2);
                            if (int.TryParse(lcYear, out int liYear) &&
                                int.TryParse(lcMonth, out int liMonth) &&
                                int.TryParse(lcDay, out int liDay))
                            {
                                ldTransactionDate = new DateTime(liYear, liMonth, liDay);
                            }
                        }

                        if (ldStartDate.HasValue && ldInServiceDate.HasValue && ldTransactionDate.HasValue)
                        {
                            string lcStartDateStr = ldStartDate.Value.ToString("yyyyMMdd");
                            string lcInServiceDateStr = ldInServiceDate.Value.ToString("yyyyMMdd");
                            string lcTransactionDateStr = ldTransactionDate.Value.ToString("yyyyMMdd");
                            if (lcStartDateStr.CompareTo(lcInServiceDateStr) < 0 || lcStartDateStr.CompareTo(lcTransactionDateStr) < 0)
                            {
                                loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "PS030"));
                            }
                        }
                    }
                }

                // Validate Initial Cost Amount
                if (_VM.Data.NTRANSACTION_AMOUNT == 0)
                {
                    loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "PS031"));
                }

                // Validate Useful Life if depreciation method is not "0"
                if (_VM.Data.CDEPR_METHOD != "0" && _VM.Data.IUSEFUL_LIVE_YR == 0 && _VM.Data.IUSEFUL_LIVE_MO == 0)
                {
                    loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "PS032"));
                }

                // Validate declining balance depreciation method (method "3")
                if (_VM.Data.CDEPR_METHOD == "3")
                {
                    if (_VM.Data.IUSEFUL_LIVE_YR < 2)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "PS041"));
                    }
                    if (_VM.Data.NYEAR_DEPR_PCT < 0 || _VM.Data.NYEAR_DEPR_PCT > 100)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "PS042"));
                    }
                }
                else
                {
                    if (_VM.Data.NYEAR_DEPR_PCT < 0 || _VM.Data.NYEAR_DEPR_PCT > 1200)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "PS042"));
                    }
                }

                // Validate beginning book value (CR21)
                if (_VM.Data.CDEPR_METHOD != "0" && _VM.Data.NLBEG_BOOK_VALUE < _VM.Data.NLBOOK_VALUE)
                {
                    loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "PS043"));
                }

                // Validate remaining useful life not greater than useful life
                if ((_VM.Data.IUSEFUL_LIVE_YR * 12) + _VM.Data.IUSEFUL_LIVE_MO < (_VM.Data.IREM_UL_YR * 12) + _VM.Data.IREM_UL_MO)
                {
                    loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "PS044"));
                }

                // Validate remaining useful life months (0-11)
                if (_VM.Data.IREM_UL_MO < 0 || _VM.Data.IREM_UL_MO > 11)
                {
                    loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "PS045"));
                }

                // Validate Asset Code in Add mode if increment flag is false
                if (eventArgs.ConductorMode == R_eConductorMode.Add)
                {
                    if (!_VM.AssetIncrementFlag)
                    {
                        if (string.IsNullOrWhiteSpace(_VM.Data.CASSET_CODE))
                        {
                            loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "PS033"));
                        }
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

        #endregion
    }
}

