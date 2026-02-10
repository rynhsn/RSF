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
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using Lookup_FACommon.DTOs;
using Lookup_FAFront;
using R_BlazorFrontEnd.Enums;
using Lookup_GSModel.ViewModel;
using Lookup_FAModel.ViewModel.FAL00200;

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
        private R_Grid<FAT00800GetTransListResultDTO>? _gridTransListRef;
        private R_TabStripTab _tabTransList = new();
        private R_TabStripTab _tabEntry = new();
        #endregion

        #region ViewModels
        private readonly FAT00800ViewModel _VM = new();
        #endregion

        #region Lifecycle Methods
        protected override async Task R_Init_From_Master(object? poParameter)
        {
            var loEx = new R_Exception();
            try
            {
                await _VM.FAT00800GetGetSystemParamAsync(ClientHelper.CompanyId, ClientHelper.CultureUI.TwoLetterISOLanguageName, ClientHelper.UserId, ClientHelper.CultureUI.TwoLetterISOLanguageName);
                await _VM.FAT00800GetYearRangeAsync(ClientHelper.CompanyId);
                await _VM.GetDeptLookupListAsync(string.Empty);
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
                await _VM.FAT00800GetTransListAsync();
                eventArgs.ListEntityResult = _VM.TransList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private async Task Conductor_R_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                // For Original grid type, return the selected row data
                var loSelectedRow = R_FrontUtility.ConvertObjectToObject<FAT00800GetTransListResultDTO>(eventArgs.Data);
                
                if (loSelectedRow != null)
                {
                    // Return the selected row as the result
                    eventArgs.Result = loSelectedRow;
                }
                else
                {
                    // If no valid row, return empty DTO
                    eventArgs.Result = new FAT00800GetTransListResultDTO();
                }
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
                if (string.IsNullOrWhiteSpace(_VM.ParameterDTO.CDEPT_CODE))
                {
                    _VM.ParameterDTO.CDEPT_CODE = "";
                    _VM.DeptName = "";
                    return;
                }


                LookupGSL00700ViewModel loLookupViewModel = new();
                var param = new GSL00700ParameterDTO
                {
                    CCOMPANY_ID = ClientHelper.CompanyId,
                    CUSER_ID = ClientHelper.UserId,
                    CSEARCH_TEXT = _VM.ParameterDTO.CDEPT_CODE
                };
                var loResult = await loLookupViewModel.GetDepartment(param);

                if (loResult == null)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                        "_ErrLookup01"));
                    _VM.ParameterDTO.CDEPT_CODE = "";
                    _VM.DeptName = "";
                }
                else
                {
                    _VM.ParameterDTO.CDEPT_CODE = loResult.CDEPT_CODE;
                    _VM.DeptName = loResult.CDEPT_NAME;
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
            eventArgs.Parameter = new GSL00700ParameterDTO();
            eventArgs.TargetPageType = typeof(GSL00700);
        }

        private async Task btnDeptLook_R_After_Open_Lookup(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loTempResult = (GSL00700DTO)eventArgs.Result;
                if (loTempResult != null)
                {
                    _VM.ParameterDTO.CDEPT_CODE = loTempResult.CDEPT_CODE;
                    _VM.DeptName = loTempResult.CDEPT_NAME;
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
                if (string.IsNullOrWhiteSpace(_VM.ParameterDTO.CASSET_CODE))
                {
                    _VM.ParameterDTO.CASSET_CODE = "";
                    _VM.AssetName = "";
                    return;
                }


                LookupFAL00300ViewModel loLookupViewModel = new();
                string cassetCode = _VM.ParameterDTO.CASSET_CODE ?? "";
                var param = new FAL00300ParameterDTO
                {
                    CCOMPANY_ID = ClientHelper.CompanyId,
                    CTRANS_CODE = FAT00800EntryViewModel.DEFAULT_TRANSACTION_CODE,
                    CASSET_CODE = cassetCode,
                    CLANGUAGE_ID = ClientHelper.CultureUI.TwoLetterISOLanguageName
                };
                var loResult = await loLookupViewModel.GetTaxCategory(param);

                if (loResult == null)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                        "_ErrLookup01"));
                    _VM.ParameterDTO.CASSET_CODE = "";
                    _VM.AssetName = "";
                }
                else
                {
                    _VM.ParameterDTO.CASSET_CODE = loResult.CASSET_CODE;
                    _VM.AssetName = loResult.CASSET_NAME;
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
            string cassetCode = _VM.ParameterDTO.CASSET_CODE ?? "";
            eventArgs.Parameter = new FAL00300ParameterDTO
            {
                CCOMPANY_ID = ClientHelper.CompanyId,
                CTRANS_CODE = FAT00800EntryViewModel.DEFAULT_TRANSACTION_CODE,
                CASSET_CODE = cassetCode,
                CLANGUAGE_ID = ClientHelper.CultureUI.TwoLetterISOLanguageName,
                
            };
            eventArgs.TargetPageType = typeof(FAL00300);
        }

        private async Task btnAssetLook_R_After_Open_Lookup(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loData = (FAL00300DTO)eventArgs.Result;
                if (loData == null)
                    return;

                _VM.ParameterDTO.CASSET_CODE = loData.CASSET_CODE;
                _VM.AssetName = loData.CASSET_NAME;
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
                var loSelectedData = _gridTransListRef?.GetCurrentData();
                var lopar = R_FrontUtility.ConvertObjectToObject<FAT00800DTO>(loSelectedData);
                if (loSelectedData != null)
                {
                    eventArgs.Parameter = lopar;
                }
                else
                {
                    eventArgs.Parameter = new FAT00800DTO();
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
