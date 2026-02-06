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
                
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }

        private void btnDeptLook_R_Before_Open_Lookup(R_BeforeOpenLookupEventArgs eventArgs)
        {
            
        }

        private async Task btnDeptLook_R_After_Open_Lookup(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                
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
                
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }

        private void btnAssetLook_R_Before_Open_Lookup(R_BeforeOpenLookupEventArgs eventArgs)
        {
           
        }

        private async Task btnAssetLook_R_After_Open_Lookup(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
               
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
                if (loSelectedData != null)
                {
                    var loParam = new FAT00800DTO
                    {
                        CCOMPANY_ID = ClientHelper.CompanyId
                    };

                    eventArgs.Parameter = loParam;
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
