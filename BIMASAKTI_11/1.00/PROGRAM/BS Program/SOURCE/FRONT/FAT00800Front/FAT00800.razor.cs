using BlazorClientHelper;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Controls.Tab;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using FAT00800Common.DTOs;
using FAT00800FrontResources;
using FAT00800Model.VMs;
using System;
using System.Linq;
using System.Threading.Tasks;
using R_BlazorFrontEnd.Interfaces;

namespace FAT00800Front
{
    public partial class FAT00800 : R_Page
    {
        #region Dependency Injection
        [Inject] private IClientHelper ClientHelper { get; set; } = default!;
        [Inject] private R_ILocalizer<Resources_Dummy_Class> Localizer { get; set; } = default!;
        [Inject] private R_MessageBoxService MessageBoxService { get; set; } = default!;
        #endregion

        #region Component References
        private R_TabStrip? _tabStripRef;
        private R_Grid<FAT00800TransListResultDTO>? _gridTransListRef;
        private R_TabStripTab _tabTransList = new();
        private R_TabStripTab _tabEntry = new();
        #endregion

        #region ViewModels
        private readonly FAT00800ViewModel _viewModel = new();
        private readonly FAT00800ListViewModel _listViewModel = new();
        #endregion

        #region Display Fields
        private string _lcDeptDesc = string.Empty;
        private string _lcAssetName = string.Empty;
        #endregion

        #region Temp Variables for Lookup
        private string _lcDeptTemp = string.Empty;
        private string _lcAssetTemp = string.Empty;
        #endregion

        #region Lifecycle Methods
        protected override async Task R_Init_From_Master(object? poParameter)
        {
            var loEx = new R_Exception();
            try
            {
                // DEBUG: ONCE FA IS INTEGRATED IN BIMASAKTI_11_BSI, THIS SHOULD BE DELETED
                ClientHelper.Set_CompanyId("HGRBH");
                ClientHelper.Set_UserId("ZF");

                // Initialize default values (uses ListViewModel properties)
                _listViewModel.PeriodFromYear = DateTime.Now.Year;
                _listViewModel.PeriodToYear = DateTime.Now.Year;

                // Initialize process - get period, currency, trans type desc, user rights
                await _viewModel.GetInitialProcessAsync(
                    ClientHelper.CompanyId,
                    ClientHelper.CultureUI.TwoLetterISOLanguageName,
                    ClientHelper.UserId);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        #endregion


        #region Event Handlers
        private async Task TabStrip_OnActiveTabIndexChanged(R_TabStripTab eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                // No asset information tab in list-only view
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }

        private async Task OnRefreshClick()
        {
            var loEx = new R_Exception();
            try
            {
                // Refresh grid based on filter criteria
                await _gridTransListRef?.R_RefreshGrid(null)!;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }
        #endregion

        #region Grid Event Handlers
        private async Task Grid_R_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                // Build period strings (YYYYMM format)
                string lcFromPeriod = $"{_listViewModel.PeriodFromYear}{_listViewModel.PeriodFromMonth}";
                string lcToPeriod = $"{_listViewModel.PeriodToYear}{_listViewModel.PeriodToMonth}";

                // Get transaction list from ListViewModel
                await _listViewModel.GetTransactionList(
                    FAT00800ViewModel.VAR_CTRANS_CODE, // Transaction code for Sale
                    _listViewModel.DeptCode,
                    lcFromPeriod,
                    lcToPeriod,
                    _listViewModel.AssetCode,
                    ClientHelper.CultureUI.TwoLetterISOLanguageName);

                eventArgs.ListEntityResult = _listViewModel.TransactionList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        #endregion

        #region Department Lookup Handlers
        private async Task txtDeptCode_OnLostFocused()
        {
            var loEx = new R_Exception();
            try
            {
                if (string.IsNullOrWhiteSpace(_listViewModel.DeptCode))
                {
                    _lcDeptDesc = string.Empty;
                    return;
                }

                if (_lcDeptTemp != _listViewModel.DeptCode.Trim())
                {
                    // Validate department
                    var liValidate = await _listViewModel.ValidateDepartmentAsync(
                        ClientHelper.CompanyId,
                        _listViewModel.DeptCode,
                        ClientHelper.UserId);

                    if (liValidate == 0)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "PS022"));
                        _listViewModel.DeptCode = string.Empty;
                        _lcDeptDesc = string.Empty;
                    }
                    else
                    {
                        _lcDeptTemp = _listViewModel.DeptCode;
                        // TODO: Get description from lookup service
                    }
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }

        private void btnDeptLook_R_Before_Open_Lookup(R_BeforeOpenLookupEventArgs eventArgs)
        {
            // TODO: Set eventArgs.TargetPageType = typeof(GSL00500);
            eventArgs.Parameter = new
            {
                CCOMPANY_ID = ClientHelper.CompanyId,
                CLANG_ID = ClientHelper.CultureUI.TwoLetterISOLanguageName,
                LACTIVE = true
            };
        }

        private async Task btnDeptLook_R_After_Open_Lookup(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                if (eventArgs.Result == null) return;

                // TODO: Cast to GSL00500DTO when lookup is implemented
                dynamic loResult = eventArgs.Result;
                string lcDeptCode = loResult.CDEPT_CODE?.ToString()?.Trim() ?? string.Empty;

                // Validate department
                var liValidate = await _listViewModel.ValidateDepartmentAsync(ClientHelper.CompanyId, lcDeptCode, ClientHelper.UserId);
                if (liValidate == 0)
                {
                    _listViewModel.DeptCode = string.Empty;
                    _lcDeptDesc = string.Empty;
                    loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "PS022"));
                }
                else
                {
                    _listViewModel.DeptCode = lcDeptCode;
                    _lcDeptDesc = loResult.CDEPT_NAME?.ToString() ?? string.Empty;
                    _lcDeptTemp = lcDeptCode;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }
        #endregion

        #region Asset Lookup Handlers
        private async Task txtAssetCode_OnLostFocused()
        {
            var loEx = new R_Exception();
            try
            {
                if (string.IsNullOrWhiteSpace(_listViewModel.AssetCode))
                {
                    _lcAssetName = string.Empty;
                    return;
                }

                if (_lcAssetTemp != _listViewModel.AssetCode.Trim())
                {
                    // TODO: Call lookup service to validate and get asset info
                    _lcAssetTemp = _listViewModel.AssetCode;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }

        private void btnAssetLook_R_Before_Open_Lookup(R_BeforeOpenLookupEventArgs eventArgs)
        {
            // TODO: Set eventArgs.TargetPageType = typeof(FAL00500);
            eventArgs.Parameter = new
            {
                CCOMPANY_ID = ClientHelper.CompanyId,
                CDEPR_METHOD = string.Empty,
                LSTATUS0 = 0,
                LSTATUS1 = 1,
                LSTATUS2 = 1,
                LSTATUS8 = 0,
                LSTATUS9 = 0
            };
        }

        private async Task btnAssetLook_R_After_Open_Lookup(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                if (eventArgs.Result == null) return;

                // TODO: Cast to FAL00500DTO when lookup is implemented
                dynamic loResult = eventArgs.Result;

                _listViewModel.AssetCode = loResult.CASSET_CODE?.ToString()?.Trim() ?? string.Empty;
                _lcAssetName = loResult.CASSET_NAME?.ToString() ?? string.Empty;
                _lcAssetTemp = _listViewModel.AssetCode;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }
        #endregion

        #region Entry Tab Page Event Handlers
        private void BeforeOpenEntry(R_BeforeOpenTabPageEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                FAT00800TransListResultDTO? loSelectedItem = null;

                // If selected data exists, use it
                var loSelectedData = _gridTransListRef?.GetCurrentData();
                if (loSelectedData != null)
                {
                    // Cast to the correct type
                    loSelectedItem = R_FrontUtility.ConvertObjectToObject<FAT00800TransListResultDTO>(loSelectedData);
                }
                else
                {
                    // If no selected data, get first row using FirstOrDefault
                    loSelectedItem = _listViewModel.TransactionList.FirstOrDefault();
                }

                // If no available transaction in list, send parameter null
                if (loSelectedItem == null)
                {
                    eventArgs.Parameter = null;
                }
                else
                {
                    // Convert selected transaction list item to FAT00800DTO for entry
                    var loParam = new FAT00800DTO
                    {
                        CCOMPANY_ID = ClientHelper.CompanyId,
                        CTRANSACTION_CODE = FAT00800ViewModel.VAR_CTRANS_CODE, // Use constant from ViewModel
                        CREFERENCE_NO = loSelectedItem.CREF_NO,
                        CDEPT_CODE = _listViewModel.DeptCode,
                        CASSET_CODE = loSelectedItem.CASSET_CODE,
                        CLANG_ID = ClientHelper.CultureUI.TwoLetterISOLanguageName
                    };

                    eventArgs.Parameter = loParam;
                }
                eventArgs.TargetPageType = typeof(FAT00800Entry);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private async Task AfterOpenEntry(R_AfterOpenTabPageEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                // Follow PMT06000 pattern: Clear lists and refresh grid
                _listViewModel.TransactionList.Clear();

                await _gridTransListRef!.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private void EntryTabEventCallBack(object poParam)
        {
            var loEx = new R_Exception();
            try
            {
                // Follow PMT06000 pattern: Handle SetOther callback to enable/disable tabs
                var loParamEvent = (FAT00800EntryCallbackParam)poParam;
                if (loParamEvent.LIS_SETOTHER)
                {
                    _tabTransList.Enabled = loParamEvent.LSET_OTHER_STATE;
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

    /// <summary>
    /// Callback parameter class for FAT00800Entry tab communication
    /// </summary>
    public class FAT00800EntryCallbackParam
    {
        public bool LIS_SETOTHER { get; set; } = false;
        public bool LSET_OTHER_STATE { get; set; } = true;
    }
}
