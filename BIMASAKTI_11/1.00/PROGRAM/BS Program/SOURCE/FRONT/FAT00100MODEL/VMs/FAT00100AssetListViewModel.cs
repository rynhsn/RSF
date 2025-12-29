using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using FAT00100Common;
using FAT00100Common.DTOs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace FAT00100Model.VMs
{
    /// <summary>
    /// ViewModel for FAT00100 Asset List operations
    /// Handles asset list data retrieval and management
    /// </summary>
    public class FAT00100AssetListViewModel : R_ViewModel<FAT00100GetTransAssetListResultDTO>
    {
        private readonly FAT00100AssetListModel _assetListModel = new FAT00100AssetListModel();

        // Asset list collection
        public ObservableCollection<FAT00100GetTransAssetListResultDTO> AssetList { get; set; } = new ObservableCollection<FAT00100GetTransAssetListResultDTO>();

        // Current record from parent component (used for API parameters)
        public FAT00100DTO? CurrentRecord { get; set; }

        // Transaction asset data (single result)
        public FAT00100GetTransAssetResultDTO TransAssetData { get; set; } = new FAT00100GetTransAssetResultDTO();

        public FAT00100AssetListViewModel()
        {
            // Initialize Data to avoid null reference issues in UI bindings
            R_SetCurrentData(new FAT00100GetTransAssetListResultDTO());
        }

        #region Streaming Methods

        /// <summary>
        /// Get transaction asset list - streaming method for asset grid
        /// </summary>
        public async Task FAT00100GetTransAssetListAsync(string pcCompanyId, string pcLangId, string pcRecId, string pcDeptCode, string pcRefNo)
        {
            var loEx = new R_Exception();

            try
            {
                // Set streaming context for custom parameters (NOT CCOMPANY_ID, CLANGUAGE_ID - handled automatically)
                R_FrontContext.R_SetStreamingContext(ContextConstants.CREC_ID, pcRecId);
                R_FrontContext.R_SetStreamingContext(ContextConstants.CDEPT_CODE, pcDeptCode);
                R_FrontContext.R_SetStreamingContext(ContextConstants.CREF_NO, pcRefNo);

                var loResult = await _assetListModel.FAT00100GetTransAssetListAsync();
                AssetList = new ObservableCollection<FAT00100GetTransAssetListResultDTO>(loResult.Data ?? new List<FAT00100GetTransAssetListResultDTO>());
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        #endregion

        #region Non-Streaming Methods

        /// <summary>
        /// Get transaction asset - non-streaming method for single asset retrieval
        /// </summary>
        /// <param name="pcCompanyId">Company ID</param>
        /// <param name="pcRecId">Record ID</param>
        /// <param name="pcDeptCode">Department code</param>
        /// <param name="pcRefNo">Reference number</param>
        /// <param name="pcTransSeqNo">Transaction sequence number</param>
        /// <param name="pcLangId">Language ID</param>
        public async Task FAT00100GetTransAssetAsync(string pcCompanyId, string pcRecId, string pcDeptCode, string pcRefNo, string pcTransSeqNo, string pcLangId)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = new FAT00100GetTransAssetParameterDTO
                {
                    CCOMPANY_ID = pcCompanyId,
                    CREC_ID = pcRecId,
                    CDEPT_CODE = pcDeptCode,
                    CREF_NO = pcRefNo,
                    CTRANS_SEQ_NO = pcTransSeqNo,
                    CLANGUAGE_ID = pcLangId
                };

                var loResult = await _assetListModel.FAT00100GetTransAsset(loParam);
                TransAssetData = loResult.Data ?? new FAT00100GetTransAssetResultDTO();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Get selected asset record - finds asset in AssetList and returns it
        /// Called by Conductor_R_ServiceGetRecord - returns the asset to be set as Data
        /// </summary>
        public FAT00100GetTransAssetListResultDTO GetSelectedAsset(FAT00100GetTransAssetListResultDTO poAsset)
        {
            var loEx = new R_Exception();
            FAT00100GetTransAssetListResultDTO loResult = new FAT00100GetTransAssetListResultDTO();

            try
            {
                if (poAsset == null)
                {
                    return loResult;
                }

                // Find matching asset in AssetList collection
                FAT00100GetTransAssetListResultDTO? loFoundAsset = null;
                
                if (AssetList != null)
                {
                    loFoundAsset = AssetList.FirstOrDefault(x => 
                        x.CASSET_CODE == poAsset.CASSET_CODE && 
                        x.CTRANS_SEQ_NO == poAsset.CTRANS_SEQ_NO);
                }

                // Use found asset from collection, or use the provided asset
                FAT00100GetTransAssetListResultDTO loAssetToDisplay = loFoundAsset ?? poAsset;

                // Create a new instance using R_FrontUtility.ConvertObjectToObject to ensure Blazor detects the change
                loResult = R_FrontUtility.ConvertObjectToObject<FAT00100GetTransAssetListResultDTO>(loAssetToDisplay) ?? new FAT00100GetTransAssetListResultDTO();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loResult;
        }

        #endregion
    }
}

