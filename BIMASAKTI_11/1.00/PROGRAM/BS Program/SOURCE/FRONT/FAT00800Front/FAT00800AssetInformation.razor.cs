using BlazorClientHelper;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Enums;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.Popup;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_BlazorFrontEnd.Interfaces;
using R_CommonFrontBackAPI;
using FAT00800Common.DTOs;
using FAT00800FrontResources;
using FAT00800Model.VMs;
using R_LockingFront;
using System;
using System.Threading.Tasks;

namespace FAT00800Front
{
    /// <summary>
    /// Asset Information tab page for FAT00800
    /// Shows readonly asset information in grouped sections
    /// </summary>
    public partial class FAT00800AssetInformation : R_Page
    {
        #region Private Fields
        
        private FAT00800AssetInfoViewModel _viewModel = new();
        private R_Grid<FAT00800GetGridAllocResultDTO>? _gridAlloc;
        
        #endregion

        #region Dependency Injection
        
        [Inject] private R_ILocalizer<Resources_Dummy_Class> _localizer { get; set; } = default!;
        [Inject] private IClientHelper ClientHelper { get; set; } = default!;
        #endregion

        #region Lifecycle Methods
        
        protected override async Task R_Init_From_Master(object poParam)
        {
            var loEx = new R_Exception();

            try
            {
                
                var loParam = R_FrontUtility.ConvertObjectToObject<FAT00800GetAssetInfoParameterDTO>(poParam);
                
                // Load asset information and grid data using the provided parameters
                await LoadAssetInfoAsync(loParam);
                
                // Grid will automatically refresh when data source changes
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        
        #endregion

        #region Public Methods
        
        /// <summary>
        /// Refresh the asset information display
        /// Called when asset data is updated
        /// </summary>
        public async Task RefreshAsync()
        {
            var loEx = new R_Exception();
            try
            {
                await InvokeAsync(StateHasChanged);
                // Grid will automatically refresh when data source changes
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        
        #endregion

        #region Grid Event Handlers
        
        /// <summary>
        /// Grid service get list record event handler
        /// Populates the allocation expense grid with data
        /// </summary>
        /// <param name="eventArgs">Grid event arguments</param>
        private async Task Grid_R_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                // Debug: Log grid data details
                System.Diagnostics.Debug.WriteLine($"Grid_R_ServiceGetListRecord - GridAllocList Count: {_viewModel.GridAllocList?.Count ?? 0}");
                
                if (_viewModel.GridAllocList?.Count > 0)
                {
                    var firstItem = _viewModel.GridAllocList[0];
                    System.Diagnostics.Debug.WriteLine($"First Item - DeptCode: '{firstItem.CEXPENSE_DEPT_CODE}', DeptName: '{firstItem.CEXPENSE_DEPT_NAME}', Percent: {firstItem.NEXPENSE_PCT}");
                }
                
                // Return the current grid allocation list
                eventArgs.ListEntityResult = _viewModel.GridAllocList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        
        #endregion

    /// <summary>
    /// Load asset information and allocation grid data
    /// </summary>
    /// <param name="loParam">Parameter DTO containing company ID, language ID, and asset code</param>
    public async Task LoadAssetInfoAsync(FAT00800GetAssetInfoParameterDTO loParam)
    {
        var loEx = new R_Exception();
        try
        {
            if (_viewModel != null)
            {
                // Load asset information
                await _viewModel.GetAssetInfoAsync(loParam);
                
                // Load grid allocation list
                await _viewModel.GetGridAllocListAsync(loParam);
                
                // Debug: Log the loaded data
                System.Diagnostics.Debug.WriteLine($"LoadAssetInfoAsync - GridAllocList Count: {_viewModel.GridAllocList?.Count ?? 0}");
                if (_viewModel.GridAllocList?.Count > 0)
                {
                    var firstItem = _viewModel.GridAllocList[0];
                    System.Diagnostics.Debug.WriteLine($"LoadAssetInfoAsync - First Item: DeptCode='{firstItem.CEXPENSE_DEPT_CODE}', DeptName='{firstItem.CEXPENSE_DEPT_NAME}', Percent={firstItem.NEXPENSE_PCT}");
                }
                
                // Trigger UI refresh
                await InvokeAsync(StateHasChanged);
                
                // Refresh the grid to display the loaded data
                if (_gridAlloc != null)
                {
                    await _gridAlloc.R_RefreshGrid(null);
                }
            }
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }
        loEx.ThrowExceptionIfErrors();
    }

   
   

    }
}
