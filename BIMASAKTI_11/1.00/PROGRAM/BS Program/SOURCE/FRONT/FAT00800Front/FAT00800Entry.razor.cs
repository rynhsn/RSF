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
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using Lookup_FAFront;
using Lookup_FACommon.DTOs;

namespace FAT00800Front;

public partial class FAT00800Entry : R_Page
{
    private FAT00800EntryViewModel _VM = new();
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
            await _VM.GetInitialProcessAsync(
                _clientHelper.CompanyId,
                _clientHelper.UserId,
                _clientHelper.CultureUI.TwoLetterISOLanguageName);
            await _VM.GetCurrencyListAsync();

            if (poParam != null && _conductorRef != null)
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<FAT00800DTO>(poParam);
                await _conductorRef.R_GetEntity(loParam);
            }
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
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
                loParam.CTRANSACTION_CODE = FAT00800EntryViewModel.VAR_CTRANS_CODE; // Use constant from ViewModel
            }
            await _VM.GetRecordAsync(loParam);
            eventArgs.Result = _VM.Entity;  
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task Display(R_DisplayEventArgs eventArgs)
    {
        var loEx = new R_Exception();
        try
        {
            // Set enableEdit based on conductor mode
            //if (eventArgs.ConductorMode == R_eConductorMode.Add)
            //{
            //    _VM.LenableEdit = false;
            //}
            //else if (eventArgs.ConductorMode == R_eConductorMode.Edit)
            //{
            //    _VM.LenableEdit = true;
            //}
            //else if (eventArgs.ConductorMode == R_eConductorMode.Normal)
            //{
            //    // In normal mode, enableEdit depends on status (only draft can be edited)
            //    _VM.LenableEdit = _VM.Entity.CSTATUS == "00";
            //}
            
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

            //if (string.IsNullOrEmpty(loEntity.CDEPT_CODE))
            //{
            //    loEx.Add(R_FrontUtility.R_GetError(typeof(FAT00800FrontResources.Resources_Dummy_Class), "PS013"));
            //}

            //if (string.IsNullOrEmpty(loEntity.CASSET_CODE))
            //{
            //    loEx.Add(R_FrontUtility.R_GetError(typeof(FAT00800FrontResources.Resources_Dummy_Class), "PS015"));
            //}

            //if (string.IsNullOrEmpty(loEntity.CCURRENCY_CODE))
            //{
            //    loEx.Add(R_FrontUtility.R_GetError(typeof(FAT00800FrontResources.Resources_Dummy_Class), "PS016"));
            //}

            //if (string.IsNullOrEmpty(loEntity.CALLOC_EXPENSE_CODE))
            //{
            //    loEx.Add(R_FrontUtility.R_GetError(typeof(FAT00800FrontResources.Resources_Dummy_Class), "PS017"));
            //}

            //if (loEntity.NTRANSACTION_AMOUNT <= 0)
            //{
            //    loEx.Add(R_FrontUtility.R_GetError(typeof(FAT00800FrontResources.Resources_Dummy_Class), "PS018"));
            //}
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
            
            // Mode-specific processing
            if (eventArgs.ConductorMode == R_eConductorMode.Add)
            {
                // Set transaction header fields
                loEntity.CTRANSACTION_CODE = FAT00800EntryViewModel.VAR_CTRANS_CODE; 
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
            await _VM.SaveRecordAsync(loParam, eventArgs.ConductorMode);
            eventArgs.Result = _VM.Entity;
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
            loParam.CTRANSACTION_CODE = FAT00800EntryViewModel.VAR_CTRANS_CODE; // Fixed Asset Sale

            await _VM.DeleteRecordAsync(loParam);
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
            loEntity.CDEPT_CODE = _VM.CDEFAULT_TRX_DEPT_CODE;
            
            // Set enableEdit to false in add mode
            _VM.LenableEdit = false;
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
        eventArgs.Allow = _VM.Entity.CSTATUS == "00"; // Draft only
    }

    private void CheckDelete(R_CheckDeleteEventArgs eventArgs)
    {
        eventArgs.Allow = _VM.Entity.CSTATUS == "00"; // Draft only
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
            //await _VM.SubmitAsync(
            //    _clientHelper.CompanyId,
            //    _clientHelper.CultureUI.TwoLetterISOLanguageName,
            //    _clientHelper.UserId,
            //    _VM.Data.CDEPT_CODE,
            //    _VM.Data.CREFERENCE_NO);

            await _conductorRef.R_GetEntity(_VM.Data);
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

            //// Redraft transaction
            //await _VM.SubmitAsync(
            //    _clientHelper.CompanyId,
            //    _clientHelper.CultureUI.TwoLetterISOLanguageName,
            //    _clientHelper.UserId,
            //    _VM.Data.CDEPT_CODE,
            //    _VM.Data.CREFERENCE_NO);

            await _conductorRef.R_GetEntity(_VM.Data);
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
            
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
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



    #endregion

    #region Lookup - Department

    private async Task OnLostFocusDept()
    {
        var loEx = new R_Exception();

        try
        {
            if (string.IsNullOrWhiteSpace(_VM.Entity.CDEPT_CODE))
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
            eventArgs.Parameter = new GSL00700ParameterDTO();
            eventArgs.TargetPageType = typeof(GSL00700);
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

    #endregion

    #region Lookup - Asset

    private async Task OnLostFocusAsset()
    {
        var loEx = new R_Exception();

        try
        {
            
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
            string cassetCode = _VM.Entity.CASSET_CODE ?? "";
            cassetCode= _conductorRef.R_ConductorMode == R_eConductorMode.Add ? "" : cassetCode;
            eventArgs.Parameter = new FAL00300ParameterDTO { CCOMPANY_ID= _clientHelper.CompanyId, 
                CTRANS_CODE=FAT00800EntryViewModel.DEFAULT_TRANSACTION_CODE, 
                CASSET_CODE = cassetCode,
                CLANGUAGE_ID= _clientHelper.CultureUI.TwoLetterISOLanguageName
            };
            eventArgs.TargetPageType = typeof(FAL00300);
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
            var loData = (FAL00300DTO)eventArgs.Result;
            if (loData == null)
                return;

            _VM.Data.CASSET_CODE = loData.CASSET_CODE;
            _VM.Data.CASSET_TRANS_SEQ_NO = loData.CASSET_TRANS_SEQ_NO;
            _VM.Data.CASSET_NAME = loData.CASSET_NAME;
            _VM.CASSET_DEPT_CODE = loData.CASSET_DEPT_CODE;
            _VM.CASSET_DEPT_NAME = loData.CASSET_DEPT_NAME;
            _VM.NLBOOK_VALUE = loData.NLBOOK_VALUE;
            _VM.NBOOK_VALUE = loData.NLBOOK_VALUE;
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
            if (string.IsNullOrWhiteSpace(_VM.Entity.CALLOC_EXPENSE_CODE))
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
        if (_VM.Entity.DCREATE_DATE != default)
        {
            _lcCreateDate = _VM.Entity.DCREATE_DATE.ToString("dd-MMM-yyyy HH:mm:ss");
        }
        if (_VM.Entity.DUPDATE_DATE != default)
        {
            _lcUpdateDate = _VM.Entity.DUPDATE_DATE.ToString("dd-MMM-yyyy HH:mm:ss");
        }

        // Update calculated fields - Gain/Loss
        _lnGainLossLocal = _VM.Entity.NLTRANSACTION_AMOUNT - _VM.Entity.NLBOOKVAL;
        _lnGainLossBase = _VM.Entity.NBTRANSACTION_AMOUNT - _VM.Entity.NBBOOKVAL;
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
            var loParam = _VM.Entity;
            
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
            if (!string.IsNullOrEmpty(_VM.Data.CASSET_CODE))
            {
                // Load asset information if not already loaded or if asset code changed
                //if (string.IsNullOrEmpty(_assetInfoViewModel.AssetInfo.CASSET_CODE) || 
                //    _assetInfoViewModel.AssetInfo.CASSET_CODE != _VM.Data.CASSET_CODE)
                //{
                //    var loAssetInfoParam = new FAT00800GetAssetInfoParameterDTO
                //    {
                //        CCOMPANY_ID = _clientHelper.CompanyId,
                //        CASSET_CODE = _VM.Data.CASSET_CODE,
                //        CLANG_ID = _clientHelper.Culture.Name,
                //        CCURRENCY_CODE = "IDR"
                //    };
                //    await _assetInfoViewModel.GetAssetInfoAsync(loAssetInfoParam);

                //    await _assetInfoViewModel.GetGridAllocListAsync(loAssetInfoParam);
                //}

                // Refresh the tab page if it's currently active
                if (_tabMain.ActiveTab?.Id == nameof(FAT00800AssetInformation))
                {
                    var loRefreshParam = new FAT00800AssetInformationParam();
                    //loRefreshParam.ViewModel = _assetInfoViewModel;
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
