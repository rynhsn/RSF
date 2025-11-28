using BlazorClientHelper;
using Microsoft.AspNetCore.Components;
using FAT00800Common.DTOs;
using FAT00800FrontResources;
using FAT00800Model.VMs;
using R_BlazorFrontEnd;
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

namespace FAT00800Front;

public partial class FAT00800Entry : R_Page
{
    private FAT00800ViewModel _viewModel = new();
    private FAT00800AssetInfoViewModel _assetInfoViewModel = new();
    private R_Conductor? _conductorRef;
    
    // Tab references
    private R_TabStrip _tabMain = new();
    private R_TabStripTab _tabEntry = new();
    private R_TabStripTab _tabAssetInfo = new();
    private R_TabPage _pageAssetInfo { get; set; } = new();

    #region Display Fields
    private string _lcDeptDesc = string.Empty;
    private string _lcAllocDesc = string.Empty;
    private string _lcCreateDate = string.Empty;
    private string _lcUpdateDate = string.Empty;
    private decimal _lnGainLossLocal;
    private decimal _lnGainLossBase;
    private decimal _lnLocalRateDisplay = 1.00m;
    private decimal _lnBaseRateDisplay = 1.00m;
    
    // OnChange Sale Amount - Display fields
    private string _lcGainLossStatus = string.Empty;
    private string _lcCalculationSummary = string.Empty;
    private string _lcExchangeRateInfo = string.Empty;
    private bool _llCalculationInProgress = false;
    
    // OnChange Sale Amount - Timer for debounced calculations
    private System.Timers.Timer? _calculationTimer;
    private decimal _lnPendingSaleAmount;
    private const int CALCULATION_DELAY_MS = 500;
    #endregion

    #region Dependency Injection
    [Inject] private IClientHelper _clientHelper { get; set; } = default!;
    #endregion

    protected override async Task R_Init_From_Master(object poParam)
    {
        var loEx = new R_Exception();

        try
        {
            // DEBUG: ONCE FA IS INTEGRATED IN BIMASAKTI_11_BSI, THIS SHOULD BE DELETED
            _clientHelper.Set_CompanyId("HGRBH");
            _clientHelper.Set_UserId("ZF");

            // Debug: Log the parameter
            System.Diagnostics.Debug.WriteLine($"R_Init_From_Master - poParam: {poParam?.ToString() ?? "NULL"}");
                
            await _viewModel.GetInitialProcessAsync(
                _clientHelper.CompanyId,
                _clientHelper.CultureUI.TwoLetterISOLanguageName,
                _clientHelper.UserId);

            if (poParam != null)
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<FAT00800DTO>(poParam);
                System.Diagnostics.Debug.WriteLine($"R_Init_From_Master - Converted param: CompanyId={loParam.CCOMPANY_ID}, RefNo={loParam.CREFERENCE_NO}, TransCode={loParam.CTRANSACTION_CODE}");
                await _conductorRef.R_GetEntity(loParam);
                System.Diagnostics.Debug.WriteLine($"R_Init_From_Master - After R_GetEntity call");
            }
            else
            {
                // If poParam is null, set new entity to show empty form (Add mode)
                System.Diagnostics.Debug.WriteLine("R_Init_From_Master - No parameter provided, initializing new empty entity");
                var loNewEntity = new FAT00800DTO
                {
                    CCOMPANY_ID = _clientHelper.CompanyId,
                    CTRANSACTION_CODE = FAT00800ViewModel.VAR_CTRANS_CODE,
                    CLANG_ID = _clientHelper.CultureUI.TwoLetterISOLanguageName
                };
                _viewModel.Entity = loNewEntity;
                System.Diagnostics.Debug.WriteLine($"R_Init_From_Master - New entity initialized: CompanyId={loNewEntity.CCOMPANY_ID}, TransCode={loNewEntity.CTRANSACTION_CODE}");
            }
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            System.Diagnostics.Debug.WriteLine($"R_Init_From_Master - Error: {ex.Message}");
        }

        loEx.ThrowExceptionIfErrors();
    }

    #region CRUD

    private async Task ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            var loParam = R_FrontUtility.ConvertObjectToObject<FAT00800DTO>(eventArgs.Data);
            loParam.CCOMPANY_ID = _clientHelper.CompanyId;
            loParam.CLANG_ID = _clientHelper.CultureUI.TwoLetterISOLanguageName;
            
            // Ensure CTRANSACTION_CODE is set if not provided
            if (string.IsNullOrEmpty(loParam.CTRANSACTION_CODE))
            {
                loParam.CTRANSACTION_CODE = FAT00800ViewModel.VAR_CTRANS_CODE; // Use constant from ViewModel
            }

            // Debug: Log the input parameter
            System.Diagnostics.Debug.WriteLine($"ServiceGetRecord - Input: CompanyId={loParam.CCOMPANY_ID}, RefNo={loParam.CREFERENCE_NO}, TransCode={loParam.CTRANSACTION_CODE}");

            await _viewModel.GetRecordAsync(loParam);

            // Debug: Log the result
            System.Diagnostics.Debug.WriteLine($"ServiceGetRecord - Result: RefNo={_viewModel.Entity.CREFERENCE_NO}, DeptCode={_viewModel.Entity.CDEPT_CODE}, Status={_viewModel.Entity.CSTATUS}");

            eventArgs.Result = _viewModel.Entity;  // Return Entity for R_Conductor synchronization

            // Update display fields
            UpdateDisplayFields();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            System.Diagnostics.Debug.WriteLine($"ServiceGetRecord - Error: {ex.Message}");
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task Display(R_DisplayEventArgs eventArgs)
    {
        var loEx = new R_Exception();
        try
        {
            // Set enableEdit based on conductor mode
            if (eventArgs.ConductorMode == R_eConductorMode.Add)
            {
                _viewModel.LenableEdit = false;
            }
            else if (eventArgs.ConductorMode == R_eConductorMode.Edit)
            {
                _viewModel.LenableEdit = true;
            }
            else if (eventArgs.ConductorMode == R_eConductorMode.Normal)
            {
                // In normal mode, enableEdit depends on status (only draft can be edited)
                _viewModel.LenableEdit = _viewModel.Entity.CSTATUS == "00";
            }
            
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }
        loEx.ThrowExceptionIfErrors();
    }

    private void Validation(R_ValidationEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            var loEntity = (FAT00800DTO)eventArgs.Data;

            if (string.IsNullOrEmpty(loEntity.CDEPT_CODE))
            {
                loEx.Add(R_FrontUtility.R_GetError(typeof(FAT00800FrontResources.Resources_Dummy_Class), "PS013"));
            }

            if (string.IsNullOrEmpty(loEntity.CASSET_CODE))
            {
                loEx.Add(R_FrontUtility.R_GetError(typeof(FAT00800FrontResources.Resources_Dummy_Class), "PS015"));
            }

            if (string.IsNullOrEmpty(loEntity.CCURRENCY_CODE))
            {
                loEx.Add(R_FrontUtility.R_GetError(typeof(FAT00800FrontResources.Resources_Dummy_Class), "PS016"));
            }

            if (string.IsNullOrEmpty(loEntity.CALLOC_EXPENSE_CODE))
            {
                loEx.Add(R_FrontUtility.R_GetError(typeof(FAT00800FrontResources.Resources_Dummy_Class), "PS017"));
            }

            if (loEntity.NTRANSACTION_AMOUNT <= 0)
            {
                loEx.Add(R_FrontUtility.R_GetError(typeof(FAT00800FrontResources.Resources_Dummy_Class), "PS018"));
            }
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private void Saving(R_SavingEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            var loEntity = (FAT00800DTO)eventArgs.Data;
            
            // Basic field assignments (always set)
            loEntity.CCOMPANY_ID = _clientHelper.CompanyId;
            loEntity.CLANG_ID = _clientHelper.CultureUI.TwoLetterISOLanguageName;
            loEntity.CUSER_ID = _clientHelper.UserId;
            loEntity.CREFERENCE_NO = eventArgs.ConductorMode == R_eConductorMode.Add ? "" : loEntity.CREFERENCE_NO;
            
            // Mode-specific processing
            if (eventArgs.ConductorMode == R_eConductorMode.Add)
            {
                // Set transaction header fields
                loEntity.CTRANSACTION_CODE = FAT00800ViewModel.VAR_CTRANS_CODE; // Fixed Asset Sale
                loEntity.CSTATUS = "00"; // Draft status
                loEntity.LINCREMENT_FLAG = _viewModel.LINCREMENT_FLAG;
                loEntity.CTRANSACTION_DATE = DateTime.Now.ToString("yyyyMMdd");
                loEntity.LGLLINK = (_viewModel.CGLLINK_DATE.CompareTo(loEntity.CTRANSACTION_DATE) <= 0);
                loEntity.CGL_TRF_STATUS = "0";
                loEntity.CCREATE_BY = _clientHelper.UserId;
                loEntity.CUPDATE_BY = _clientHelper.UserId;
                
                // Set transaction amounts
                loEntity.NTRANSACTION_AMOUNT = loEntity.NTRANSACTION_AMOUNT1;
                loEntity.NLTRANSACTION_AMOUNT = loEntity.NLTRANSACTION_AMOUNT1;
                loEntity.NBTRANSACTION_AMOUNT = loEntity.NBTRANSACTION_AMOUNT1;
            }
            else if (eventArgs.ConductorMode == R_eConductorMode.Edit)
            {
                loEntity.CUPDATE_BY = _clientHelper.UserId;
                loEntity.CTRANSACTION_CODE = FAT00800ViewModel.VAR_CTRANS_CODE;
                
                // Handle special edit modes
                if (!_viewModel.LCHANGE_DESC && !_viewModel.LCHANGE_ALLOC)
                {
                    // Normal edit mode
                    loEntity.LCHANGE_DESC = false;
                    loEntity.LCHANGE_ALLOC = false;
                    loEntity.NTRANSACTION_AMOUNT = loEntity.NTRANSACTION_AMOUNT1;
                    loEntity.NLTRANSACTION_AMOUNT = loEntity.NLTRANSACTION_AMOUNT1;
                    loEntity.NBTRANSACTION_AMOUNT = loEntity.NBTRANSACTION_AMOUNT1;
                    loEntity.LGLLINK = (_viewModel.CGLLINK_DATE.CompareTo(loEntity.CTRANSACTION_DATE) <= 0);
                }
                else if (_viewModel.LCHANGE_DESC)
                {
                    // Change description mode
                    loEntity.LCHANGE_DESC = true;
                    loEntity.CTRANSACTION_DATE = DateTime.Now.ToString("yyyyMMdd");
                }
                else if (_viewModel.LCHANGE_ALLOC)
                {
                    // Change allocation mode
                    loEntity.LCHANGE_ALLOC = true;
                    loEntity.CTRANSACTION_DATE = DateTime.Now.ToString("yyyyMMdd");
                }
            }
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task ServiceSave(R_ServiceSaveEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            var loParam = R_FrontUtility.ConvertObjectToObject<FAT00800DTO>(eventArgs.Data);
            await _viewModel.SaveRecordAsync(loParam, eventArgs.ConductorMode);
            eventArgs.Result = _viewModel.Entity;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task BeforeDelete(R_BeforeDeleteEventArgs eventArgs)
    {
        var leMsg = await R_MessageBox.Show("", _localizer["PS008"], R_eMessageBoxButtonType.YesNo);
        eventArgs.Cancel = leMsg != R_eMessageBoxResult.Yes;
    }

    private async Task ServiceDelete(R_ServiceDeleteEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            var loParam = (FAT00800DTO)eventArgs.Data;
            loParam.CCOMPANY_ID = _clientHelper.CompanyId;
            loParam.CUSER_ID = _clientHelper.UserId;
            loParam.CTRANSACTION_CODE = FAT00800ViewModel.VAR_CTRANS_CODE; // Fixed Asset Sale

            await _viewModel.DeleteRecordAsync(loParam);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private void AfterAdd(R_AfterAddEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            var loEntity = (FAT00800DTO)eventArgs.Data;
            
            // Set only essential default values
            loEntity.CDEPT_CODE = _viewModel.CDEFAULT_TRX_DEPT_CODE;
            loEntity.CTRANSACTION_CODE = "270010"; // Fixed Asset Sale
            loEntity.CTRANSACTION_DATE = DateTime.Now.ToString("yyyyMMdd");
            loEntity.DTRANSACTION_DATE = DateTime.Now;
            loEntity.CSTATUS = "00"; // Draft
            loEntity.CSTATUS_DESC = "Draft";
            loEntity.CCURRENCY_CODE = _viewModel.CLOCAL_CURRENCY_CODE;
            loEntity.NLCURRENCY_RATE_AMOUNT = 1;
            loEntity.NBCURRENCY_RATE_AMOUNT = 1;
            
            // Set enableEdit to false in add mode
            _viewModel.LenableEdit = false;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private void CheckAdd(R_CheckAddEventArgs eventArgs)
    {
        eventArgs.Allow = true;
    }

    private void CheckEdit(R_CheckEditEventArgs eventArgs)
    {
        eventArgs.Allow = _viewModel.Entity.CSTATUS == "00"; // Draft only
    }

    private void CheckDelete(R_CheckDeleteEventArgs eventArgs)
    {
        eventArgs.Allow = _viewModel.Entity.CSTATUS == "00"; // Draft only
    }

    private void SetOther(R_SetEventArgs eventArgs)
    {
        // Button enabling is now handled by individual button components
    }

    #endregion

    #region Submit/Draft

    private async Task OnClickSubmit()
    {
        var loEx = new R_Exception();

        try
        {
            var lcMsg = _localizer["PS009"]; // Submit confirmation

            var leMsg = await R_MessageBox.Show("", lcMsg, R_eMessageBoxButtonType.YesNo);

            if (leMsg == R_eMessageBoxResult.No)
            {
                return;
            }

            // Submit transaction
            await _viewModel.SubmitAsync(
                _clientHelper.CompanyId,
                _clientHelper.CultureUI.TwoLetterISOLanguageName,
                _clientHelper.UserId,
                _viewModel.Data.CDEPT_CODE,
                _viewModel.Data.CREFERENCE_NO);

            await _conductorRef.R_GetEntity(_viewModel.Data);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task OnClickDraft()
    {
        var loEx = new R_Exception();

        try
        {
            var lcMsg = _localizer["PS010"]; // Redraft confirmation

            var leMsg = await R_MessageBox.Show("", lcMsg, R_eMessageBoxButtonType.YesNo);

            if (leMsg == R_eMessageBoxResult.No)
            {
                return;
            }

            // Redraft transaction
            await _viewModel.SubmitAsync(
                _clientHelper.CompanyId,
                _clientHelper.CultureUI.TwoLetterISOLanguageName,
                _clientHelper.UserId,
                _viewModel.Data.CDEPT_CODE,
                _viewModel.Data.CREFERENCE_NO);

            await _conductorRef.R_GetEntity(_viewModel.Data);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task OnClickJournal()
    {
        var loEx = new R_Exception();

        try
        {
            // TODO: Implement journal popup navigation
            // Based on NET4: Opens FAI00020 form with parameters:
            // - CCOMPANY_ID
            // - CDEPT_CODE
            // - CTRANSACTION_CODE
            // - CREFERENCE_NO
            await R_MessageBox.Show("", "Journal functionality not yet implemented", R_eMessageBoxButtonType.OK);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    #endregion

    #region OnChange Sale Amount Implementation

    /// <summary>
    /// OnChange Sale Amount - Main event handler for sale amount changes
    /// Implements debounced calculation to prevent excessive processing during typing
    /// </summary>
    public async Task OnSaleAmountChanged()
    {
        var loEx = new R_Exception();
        try
        {
            // Stop existing timer
            _calculationTimer?.Stop();
            _lnPendingSaleAmount = _viewModel.Data.NTRANSACTION_AMOUNT1;
            
            // Start new timer with delay to debounce rapid changes
            _calculationTimer = new System.Timers.Timer(CALCULATION_DELAY_MS);
            _calculationTimer.Elapsed += async (s, e) =>
            {
                _calculationTimer.Stop();
                await InvokeAsync(async () => await PerformSaleAmountCalculation(_lnPendingSaleAmount));
            };
            _calculationTimer.Start();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }
        loEx.ThrowExceptionIfErrors();
    }

    /// <summary>
    /// OnChange Sale Amount - Lost focus event handler for immediate calculation
    /// </summary>
    public async Task OnSaleAmountLostFocus()
    {
        var loEx = new R_Exception();
        try
        {
            // Stop any pending timer and calculate immediately
            _calculationTimer?.Stop();
            await PerformSaleAmountCalculation(_viewModel.Data.NTRANSACTION_AMOUNT1);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }
        loEx.ThrowExceptionIfErrors();
    }

    /// <summary>
    /// OnChange Sale Amount - Perform the actual calculation using ViewModel business logic
    /// </summary>
    /// <param name="saleAmount">Sale amount to calculate</param>
    private async Task PerformSaleAmountCalculation(decimal saleAmount)
    {
        var loEx = new R_Exception();
        
        try
        {
            // Set calculation in progress flag
            _llCalculationInProgress = true;
            StateHasChanged();

            // Check basic prerequisites before attempting calculation
            if (saleAmount <= 0)
            {
                // Clear calculated fields for zero or negative amounts
                _viewModel.Data.NTRANSACTION_AMOUNT = 0;
                _viewModel.Data.NTRANSACTION_AMOUNT1 = 0;
                _viewModel.Data.NLTRANSACTION_AMOUNT = 0;
                _viewModel.Data.NLTRANSACTION_AMOUNT1 = 0;
                _viewModel.Data.NBTRANSACTION_AMOUNT = 0;
                _viewModel.Data.NBTRANSACTION_AMOUNT1 = 0;
                _viewModel.Data.NLGAIN_LOSS = 0;
                _viewModel.Data.NBGAIN_LOSS = 0;
                return;
            }

            // Check if we have minimum required data for calculation
            var llHasAsset = !string.IsNullOrWhiteSpace(_viewModel.Data.CASSET_CODE);
            var llHasCurrency = !string.IsNullOrWhiteSpace(_viewModel.Data.CCURRENCY_CODE);
            var llHasRates = _viewModel.Data.NLCURRENCY_RATE_AMOUNT > 0 && 
                           _viewModel.Data.NLBASE_RATE_AMOUNT > 0 &&
                           _viewModel.Data.NBCURRENCY_RATE_AMOUNT > 0 && 
                           _viewModel.Data.NBBASE_RATE_AMOUNT > 0;

            if (!llHasAsset || !llHasCurrency || !llHasRates)
            {
                // Prerequisites not met - show informative message but don't error
                var lcMissingItems = new List<string>();
                if (!llHasAsset) lcMissingItems.Add("Asset Code");
                if (!llHasCurrency) lcMissingItems.Add("Currency");
                if (!llHasRates) lcMissingItems.Add("Exchange Rates");

                System.Diagnostics.Debug.WriteLine($"OnChange Sale Amount: Missing prerequisites - {string.Join(", ", lcMissingItems)}");
                
                // Still update the original amount even if we can't calculate conversions
                _viewModel.Data.NTRANSACTION_AMOUNT = saleAmount;   // Main field for validation
                _viewModel.Data.NTRANSACTION_AMOUNT1 = saleAmount;  // Detailed field for UI
                _viewModel.Data.NLTRANSACTION_AMOUNT = 0;
                _viewModel.Data.NLTRANSACTION_AMOUNT1 = 0;
                _viewModel.Data.NBTRANSACTION_AMOUNT = 0;
                _viewModel.Data.NBTRANSACTION_AMOUNT1 = 0;
                _viewModel.Data.NLGAIN_LOSS = 0;
                _viewModel.Data.NBGAIN_LOSS = 0;
                return;
            }

            // Perform direct calculation (preserving exact VB.NET business logic)
            try
            {
                // Calculate Local Sale Amount: SaleAmount * LocalCurrencyRate / LocalBaseRate
                var lnLocalSaleAmount = Math.Round(saleAmount * _viewModel.Data.NLCURRENCY_RATE_AMOUNT / _viewModel.Data.NLBASE_RATE_AMOUNT, 2);
                
                // Calculate Base Sale Amount: SaleAmount * BaseCurrencyRate / BaseBaseRate  
                var lnBaseSaleAmount = Math.Round(saleAmount * _viewModel.Data.NBCURRENCY_RATE_AMOUNT / _viewModel.Data.NBBASE_RATE_AMOUNT, 2);
                
                // Calculate Gain/Loss: SaleAmount - BookValue
                var lnLocalGainLoss = lnLocalSaleAmount - _viewModel.Data.NLBOOKVAL;
                var lnBaseGainLoss = lnBaseSaleAmount - _viewModel.Data.NBBOOKVAL;
                
                // Update Data (which is bound to UI) - Both main and detailed amount fields
                _viewModel.Data.NTRANSACTION_AMOUNT = saleAmount;      // Main field for validation
                _viewModel.Data.NTRANSACTION_AMOUNT1 = saleAmount;     // Detailed field for UI
                _viewModel.Data.NLTRANSACTION_AMOUNT = lnLocalSaleAmount;  // Main local amount
                _viewModel.Data.NLTRANSACTION_AMOUNT1 = lnLocalSaleAmount; // Detailed local amount
                _viewModel.Data.NBTRANSACTION_AMOUNT = lnBaseSaleAmount;   // Main base amount
                _viewModel.Data.NBTRANSACTION_AMOUNT1 = lnBaseSaleAmount;  // Detailed base amount
                _viewModel.Data.NLGAIN_LOSS = lnLocalGainLoss;
                _viewModel.Data.NBGAIN_LOSS = lnBaseGainLoss;
                
                // Update display fields
                _lnGainLossLocal = lnLocalGainLoss;
                _lnGainLossBase = lnBaseGainLoss;
                
                System.Diagnostics.Debug.WriteLine($"OnChange Sale Amount: Original={saleAmount:N2}, Local={lnLocalSaleAmount:N2}, Base={lnBaseSaleAmount:N2}, Gain/Loss={lnLocalGainLoss:N2}");
            }
            catch (DivideByZeroException)
            {
                await HandleCalculationError("PS019", "Currency rates cannot be zero");
            }
            catch (Exception ex)
            {
                await HandleCalculationError("CALC_ERROR", $"Calculation error: {ex.Message}");
            }
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            await HandleCalculationError("CALC_ERROR", ex.Message);
        }
        finally
        {
            _llCalculationInProgress = false;
            StateHasChanged();
        }

        loEx.ThrowExceptionIfErrors();
    }

    /// <summary>
    /// OnChange Sale Amount - Update UI fields with calculated results
    /// </summary>
    /// <param name="result">Calculation results from ViewModel</param>
    private async Task UpdateCalculatedFields(OnChangeSaleAmountResult result)
    {
        try
        {
            // Update local and base sale amounts (these are bound to _viewModel.Data)
            // The ViewModel has already updated the Entity, so we just need to trigger UI refresh
            
            // Update gain/loss display fields
            _lnGainLossLocal = result.LocalGainLoss;
            _lnGainLossBase = result.BaseGainLoss;
            _lcGainLossStatus = result.GainLossStatus;
            
            // Update summary displays
            _lcCalculationSummary = _viewModel.GetCalculationSummary();
            _lcExchangeRateInfo = _viewModel.GetExchangeRateInfo();
            
            // Update currency display labels
            UpdateCurrencyLabels();
            
            // Apply visual styling based on gain/loss status
            ApplyGainLossVisualStyling(result.GainLossStatus);
            
            // Trigger UI refresh
            StateHasChanged();
        }
        catch (Exception ex)
        {
            await HandleCalculationError("UI_UPDATE_ERROR", $"Failed to update display: {ex.Message}");
        }
    }

    /// <summary>
    /// OnChange Sale Amount - Update currency labels for display
    /// </summary>
    private void UpdateCurrencyLabels()
    {
        // Currency labels are handled through _viewModel.loCurrencyTemp binding
        // This method can be extended for additional currency display logic
    }

    /// <summary>
    /// OnChange Sale Amount - Apply visual styling based on gain/loss status
    /// </summary>
    /// <param name="gainLossStatus">Status: GAIN, LOSS, or BREAK EVEN</param>
    private void ApplyGainLossVisualStyling(string gainLossStatus)
    {
        // Visual styling will be applied through CSS classes in the Razor component
        // This method prepares the status for CSS class binding
        _lcGainLossStatus = gainLossStatus;
    }

    /// <summary>
    /// OnChange Sale Amount - Handle calculation errors with user feedback
    /// </summary>
    /// <param name="errorCode">Error code for specific handling</param>
    /// <param name="errorMessage">Error message to display</param>
    private async Task HandleCalculationError(string errorCode, string errorMessage)
    {
        try
        {
            // Reset calculated fields on error
            _viewModel.ResetCalculatedFields();
            
            // Clear display fields
            _lnGainLossLocal = 0;
            _lnGainLossBase = 0;
            _lcGainLossStatus = string.Empty;
            _lcCalculationSummary = string.Empty;
            
            // Show specific error messages based on error code
            switch (errorCode)
            {
                case "PS018":
                    await R_MessageBox.Show(_localizer["_Error"], 
                        _localizer["PS018"], // "Sale amount must be positive"
                        R_eMessageBoxButtonType.OK);
                    break;
                    
                case "PS019":
                    await R_MessageBox.Show(_localizer["_Error"], 
                        _localizer["PS019"], // "Currency rates cannot be zero"
                        R_eMessageBoxButtonType.OK);
                    break;
                    
                case "CALC001":
                    await R_MessageBox.Show(_localizer["_Error"], 
                        "Calculated local amount exceeds system limits. Please use a smaller amount.", 
                        R_eMessageBoxButtonType.OK);
                    break;
                    
                case "CALC002":
                    await R_MessageBox.Show(_localizer["_Error"], 
                        "Calculated base amount exceeds system limits. Please use a smaller amount.", 
                        R_eMessageBoxButtonType.OK);
                    break;
                    
                default:
                    await R_MessageBox.Show(_localizer["_Error"], 
                        $"Calculation error: {errorMessage}", 
                        R_eMessageBoxButtonType.OK);
                    break;
            }
            
            StateHasChanged();
        }
        catch (Exception ex)
        {
            // Fallback error handling
            System.Diagnostics.Debug.WriteLine($"Error in HandleCalculationError: {ex.Message}");
        }
    }

    /// <summary>
    /// OnChange Sale Amount - Show warning message to user
    /// </summary>
    /// <param name="warningMessage">Warning message to display</param>
    private async Task ShowWarning(string warningMessage)
    {
        try
        {
            await R_MessageBox.Show(_localizer["_Warning"], 
                warningMessage, 
                R_eMessageBoxButtonType.OK);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error showing warning: {ex.Message}");
        }
    }

    /// <summary>
    /// OnChange Sale Amount - Get CSS class for gain/loss styling
    /// </summary>
    /// <returns>CSS class name based on gain/loss status</returns>
    private string GetGainLossCssClass()
    {
        return _lcGainLossStatus switch
        {
            "GAIN" => "text-success fw-bold", // Green for gain
            "LOSS" => "text-danger fw-bold",  // Red for loss
            "BREAK EVEN" => "text-muted fw-bold", // Gray for break even
            _ => "text-dark"
        };
    }

    /// <summary>
    /// OnChange Sale Amount - Check if calculation is valid and complete
    /// </summary>
    /// <returns>True if calculation is valid</returns>
    private bool IsCalculationValid()
    {
        return _viewModel.ValCalculationComplete && 
               _viewModel.ValAmountPositive && 
               _viewModel.ValCurrencyRatesValid;
    }

    /// <summary>
    /// OnChange Sale Amount - Cleanup calculation timer when component is destroyed
    /// </summary>
    ~FAT00800Entry()
    {
        _calculationTimer?.Stop();
        _calculationTimer?.Dispose();
    }

    #endregion

    #region Lookup - Department

    private async Task OnLostFocusDept()
    {
        var loEx = new R_Exception();

        try
        {
            if (string.IsNullOrWhiteSpace(_viewModel.Entity.CDEPT_CODE))
            {
                _lcDeptDesc = "";
                return;
            }

            // TODO: Implement department lookup
            _lcDeptDesc = "";
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        await R_DisplayExceptionAsync(loEx);
    }

    private void BeforeOpenLookupDept(R_BeforeOpenLookupEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            // TODO: Implement lookup parameter
            // eventArgs.Parameter = loParameter;
            // eventArgs.TargetPageType = typeof(GSL00700);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private void AfterOpenLookupDept(R_AfterOpenLookupEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            if (eventArgs.Result == null) return;

            // TODO: Handle lookup result
            // var loTempResult = eventArgs.Result;
            // _viewModel.Entity.CDEPT_CODE = loTempResult.CDEPT_CODE;
            // _lcDeptDesc = loTempResult.CDEPT_NAME;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    #endregion

    #region Lookup - Asset

    private async Task OnLostFocusAsset()
    {
        var loEx = new R_Exception();

        try
        {
            if (string.IsNullOrWhiteSpace(_viewModel.Data.CASSET_CODE))
            {
                _viewModel.Data.CASSET_NAME = "";
                _viewModel.Data.NLBOOKVAL = 0;
                _viewModel.Data.NBBOOKVAL = 0;
                return;
            }

            // Get asset information including book values
            var loAssetInfoParam = new FAT00800GetAssetInfoParameterDTO
            {
                CCOMPANY_ID = _clientHelper.CompanyId,
                CASSET_CODE = _viewModel.Data.CASSET_CODE,
                CLANG_ID = _clientHelper.Culture.Name,
                CCURRENCY_CODE = "IDR"
            };
            await _assetInfoViewModel.GetAssetInfoAsync(loAssetInfoParam);

            // Get grid allocation list for asset information display
            await _assetInfoViewModel.GetGridAllocListAsync(loAssetInfoParam);

            // Get book values for the asset
            var loBookValues = await _viewModel.GetBookValueAsync(
                _clientHelper.CompanyId,
                _viewModel.Data.CASSET_CODE);

            // Update Data with asset information (this is what's bound to UI)
            _viewModel.Data.CASSET_NAME = _assetInfoViewModel.AssetInfo.CASSET_NAME;
            _viewModel.Data.NLBOOKVAL = loBookValues.NLBOOKVAL;
            _viewModel.Data.NBBOOKVAL = loBookValues.NBBOOKVAL;

            // Refresh asset information tab page if it's currently active
            if (_tabMain.ActiveTab?.Id == nameof(FAT00800AssetInformation))
            {
                var loRefreshParam = new FAT00800AssetInformationParam();
                loRefreshParam.ViewModel = _assetInfoViewModel;
                await _pageAssetInfo.InvokeRefreshTabPageAsync(loRefreshParam);
            }

            // Trigger sale amount calculation if sale amount is already entered
            if (_viewModel.Data.NTRANSACTION_AMOUNT1 > 0)
            {
                await OnSaleAmountChanged();
            }
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        await R_DisplayExceptionAsync(loEx);
    }

    private void BeforeOpenLookupAsset(R_BeforeOpenLookupEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            // TODO: Implement lookup parameter
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async void AfterOpenLookupAsset(R_AfterOpenLookupEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            if (eventArgs.Result == null) return;

            // Handle asset lookup result - trigger the same logic as OnLostFocusAsset
            await OnLostFocusAsset();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    #endregion

    #region Lookup - Currency

    private async Task OnLostFocusCurrency()
    {
        var loEx = new R_Exception();

        try
        {
            if (string.IsNullOrWhiteSpace(_viewModel.Data.CCURRENCY_CODE))
            {
                // Reset currency rates to default
                _viewModel.Data.NLCURRENCY_RATE_AMOUNT = 1;
                _viewModel.Data.NLBASE_RATE_AMOUNT = 1;
                _viewModel.Data.NBCURRENCY_RATE_AMOUNT = 1;
                _viewModel.Data.NBBASE_RATE_AMOUNT = 1;
                return;
            }

            // Get currency exchange rates
            var loCurrencyRates = await _viewModel.GetCurrencyAsync(
                _clientHelper.CompanyId,
                _viewModel.Data.CCURRENCY_CODE,
                _viewModel.CRATETYPE_CODE,
                _viewModel.Data.CTRANSACTION_DATE);

            // Update Data with currency rates (this is what's bound to UI)
            _viewModel.Data.NLCURRENCY_RATE_AMOUNT = loCurrencyRates.NLCURRENCY_RATE_AMOUNT;
            _viewModel.Data.NLBASE_RATE_AMOUNT = loCurrencyRates.NLBASE_RATE_AMOUNT;
            _viewModel.Data.NBCURRENCY_RATE_AMOUNT = loCurrencyRates.NBCURRENCY_RATE_AMOUNT;
            _viewModel.Data.NBBASE_RATE_AMOUNT = loCurrencyRates.NBBASE_RATE_AMOUNT;

            // Update currency display
            _viewModel.loCurrencyTemp = _viewModel.Data.CCURRENCY_CODE;

            // Trigger sale amount calculation if sale amount is already entered
            if (_viewModel.Data.NTRANSACTION_AMOUNT1 > 0)
            {
                await OnSaleAmountChanged();
            }
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        await R_DisplayExceptionAsync(loEx);
    }

    private void BeforeOpenLookupCurrency(R_BeforeOpenLookupEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            // TODO: Implement lookup parameter
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async void AfterOpenLookupCurrency(R_AfterOpenLookupEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            if (eventArgs.Result == null) return;

            // Handle currency lookup result - trigger the same logic as OnLostFocusCurrency
            await OnLostFocusCurrency();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    #endregion

    #region Lookup - Allocation

    private async Task OnLostFocusAlloc()
    {
        var loEx = new R_Exception();

        try
        {
            if (string.IsNullOrWhiteSpace(_viewModel.Entity.CALLOC_EXPENSE_CODE))
            {
                _lcAllocDesc = "";
                return;
            }

            // TODO: Implement allocation lookup
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        await R_DisplayExceptionAsync(loEx);
    }

    private void BeforeOpenLookupAlloc(R_BeforeOpenLookupEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            // TODO: Implement lookup parameter
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private void AfterOpenLookupAlloc(R_AfterOpenLookupEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            if (eventArgs.Result == null) return;

            // TODO: Handle lookup result
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    #endregion

    #region Helper Methods

    private void UpdateDisplayFields()
    {
        // Update date displays
        if (_viewModel.Entity.DCREATE_DATE != default)
        {
            _lcCreateDate = _viewModel.Entity.DCREATE_DATE.ToString("dd-MMM-yyyy HH:mm:ss");
        }
        if (_viewModel.Entity.DUPDATE_DATE != default)
        {
            _lcUpdateDate = _viewModel.Entity.DUPDATE_DATE.ToString("dd-MMM-yyyy HH:mm:ss");
        }

        // Update calculated fields - Gain/Loss
        _lnGainLossLocal = _viewModel.Entity.NLTRANSACTION_AMOUNT - _viewModel.Entity.NLBOOKVAL;
        _lnGainLossBase = _viewModel.Entity.NBTRANSACTION_AMOUNT - _viewModel.Entity.NBBOOKVAL;
    }

    #endregion

    #region Tab Event Handlers

    private void OnChangeTab(R_TabStripActiveTabIndexChangingEventArgs eventArgs)
    {
        // Handle tab switching logic if needed
        // Currently no specific logic required for tab switching
    }

    private void BeforeOpenAssetInfoTab(R_BeforeOpenTabPageEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            // Create parameter object for the Asset Information tab page
            var loParam = _viewModel.Entity;
            
            eventArgs.Parameter = loParam;
            eventArgs.TargetPageType = typeof(FAT00800AssetInformation);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task AssetInfoTabEventCallBack(object poParam)
    {
        var loEx = new R_Exception();

        try
        {
            // Handle any callback events from the Asset Information tab
            // Currently no specific callback handling required
            
            // If asset code is available, ensure asset information is loaded
            if (!string.IsNullOrEmpty(_viewModel.Data.CASSET_CODE))
            {
                // Load asset information if not already loaded or if asset code changed
                if (string.IsNullOrEmpty(_assetInfoViewModel.AssetInfo.CASSET_CODE) || 
                    _assetInfoViewModel.AssetInfo.CASSET_CODE != _viewModel.Data.CASSET_CODE)
                {
                    var loAssetInfoParam = new FAT00800GetAssetInfoParameterDTO
                    {
                        CCOMPANY_ID = _clientHelper.CompanyId,
                        CASSET_CODE = _viewModel.Data.CASSET_CODE,
                        CLANG_ID = _clientHelper.Culture.Name,
                        CCURRENCY_CODE = "IDR"
                    };
                    await _assetInfoViewModel.GetAssetInfoAsync(loAssetInfoParam);

                    await _assetInfoViewModel.GetGridAllocListAsync(loAssetInfoParam);
                }

                // Refresh the tab page if it's currently active
                if (_tabMain.ActiveTab?.Id == nameof(FAT00800AssetInformation))
                {
                    var loRefreshParam = new FAT00800AssetInformationParam();
                    loRefreshParam.ViewModel = _assetInfoViewModel;
                    await _pageAssetInfo.InvokeRefreshTabPageAsync(loRefreshParam);
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

    #region Cancel Event Handler

    private async Task BeforeCancel(R_BeforeCancelEventArgs eventArgs)
    {
        var loEx = new R_Exception();
        try
        {
            // Show confirmation dialog before canceling
            var leMsg = await R_MessageBox.Show("", R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "MSG_BEFORE_CANCEL"), R_eMessageBoxButtonType.YesNo);
            eventArgs.Cancel = leMsg != R_eMessageBoxResult.Yes;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            // If there's an error showing the dialog, don't cancel the operation
            eventArgs.Cancel = false;
        }
        
        // Only throw exceptions if there are any, but don't block the cancel operation
        if (loEx.HasError)
        {
            R_DisplayException(loEx);
        }
    }

    #endregion
}
