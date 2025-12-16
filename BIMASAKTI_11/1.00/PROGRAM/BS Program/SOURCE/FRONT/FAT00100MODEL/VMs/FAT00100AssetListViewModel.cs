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
    public class FAT00100AssetListViewModel : R_ViewModel<FAT00100GetAssetListResultDTO>
    {
        private readonly FAT00100AssetListModel _assetListModel = new FAT00100AssetListModel();

        // Asset list collection
        public ObservableCollection<FAT00100GetAssetListResultDTO> AssetList { get; set; } = new ObservableCollection<FAT00100GetAssetListResultDTO>();

        // Current record from parent component (used for API parameters)
        public FAT00100DTO? CurrentRecord { get; set; }

        public FAT00100AssetListViewModel()
        {
            // Initialize Data to avoid null reference issues in UI bindings
            R_SetCurrentData(new FAT00100GetAssetListResultDTO());
        }

        /// <summary>
        /// Get asset list - streaming method for asset grid
        /// </summary>
        public async Task GetAssetListAsync(string pcCompanyId, string pcLangId, string pcDeptCode, string pcTransactionCode, string pcReferenceNo, string pcStatus)
        {
            var loEx = new R_Exception();

            try
            {
                // Set streaming context for custom parameters (NOT CCOMPANY_ID, CFOREIGN_LANGUAGE)
                R_FrontContext.R_SetStreamingContext(ContextConstants.CDEPT_CODE, pcDeptCode);
                R_FrontContext.R_SetStreamingContext(ContextConstants.CTRANSACTION_CODE, pcTransactionCode);
                R_FrontContext.R_SetStreamingContext(ContextConstants.CREFERENCE_NO, pcReferenceNo);
                R_FrontContext.R_SetStreamingContext(ContextConstants.CSTATUS, pcStatus);
                //R_FrontContext.R_SetStreamingContext(ContextConstants.DUPDATE_DATE, pdUpdateDate);

                var loResult = await _assetListModel.GetAssetListAsync();
                AssetList = new ObservableCollection<FAT00100GetAssetListResultDTO>(loResult.Data ?? new List<FAT00100GetAssetListResultDTO>());
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        /// <summary>
        /// Get selected asset record - finds asset in AssetList and returns it
        /// Called by Conductor_R_ServiceGetRecord - returns the asset to be set as Data
        /// </summary>
        public FAT00100GetAssetListResultDTO GetSelectedAsset(FAT00100GetAssetListResultDTO poAsset)
        {
            var loEx = new R_Exception();
            FAT00100GetAssetListResultDTO loResult = new FAT00100GetAssetListResultDTO();

            try
            {
                if (poAsset == null)
                {
                    return loResult;
                }

                // Find matching asset in AssetList collection
                FAT00100GetAssetListResultDTO? loFoundAsset = null;
                
                if (AssetList != null)
                {
                    loFoundAsset = AssetList.FirstOrDefault(x => 
                        x.CASSET_CODE == poAsset.CASSET_CODE && 
                        x.CASSET_TRANS_SEQNO == poAsset.CASSET_TRANS_SEQNO);
                }

                // Use found asset from collection, or use the provided asset
                FAT00100GetAssetListResultDTO loAssetToDisplay = loFoundAsset ?? poAsset;

                // Create a new instance and manually copy properties to ensure Blazor detects the change
                loResult = new FAT00100GetAssetListResultDTO
                {
                    CASSET_CODE = loAssetToDisplay.CASSET_CODE ?? string.Empty,
                    CASSET_TRANS_SEQNO = loAssetToDisplay.CASSET_TRANS_SEQNO ?? string.Empty,
                    NTRANSACTION_AMOUNT1 = loAssetToDisplay.NTRANSACTION_AMOUNT1,
                    NLTRANSACTION_AMOUNT1 = loAssetToDisplay.NLTRANSACTION_AMOUNT1,
                    ITRANSACTION_QTY1 = loAssetToDisplay.ITRANSACTION_QTY1,
                    CUNIT = loAssetToDisplay.CUNIT ?? string.Empty,
                    CTRANSACTION_DESCR = loAssetToDisplay.CTRANSACTION_DESCR ?? string.Empty,
                    CASSET_DEPT_CODE = loAssetToDisplay.CASSET_DEPT_CODE ?? string.Empty,
                    CASSET_DEPT_NAME = loAssetToDisplay.CASSET_DEPT_NAME ?? string.Empty,
                    CASSET_LOCATION = loAssetToDisplay.CASSET_LOCATION ?? string.Empty,
                    CJRNGRP_CODE = loAssetToDisplay.CJRNGRP_CODE ?? string.Empty,
                    CJRNGRP_NAME = loAssetToDisplay.CJRNGRP_NAME ?? string.Empty,
                    CTAX_CATEGORY_CODE = loAssetToDisplay.CTAX_CATEGORY_CODE ?? string.Empty,
                    CTAX_CATEGORY_DESC = loAssetToDisplay.CTAX_CATEGORY_DESC ?? string.Empty,
                    CASSET_NAME = loAssetToDisplay.CASSET_NAME ?? string.Empty
                };
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loResult;
        }
    }
}

