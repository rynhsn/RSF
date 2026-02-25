using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using FAT01100Common;
using FAT01100Common.DTOs;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_CommonFrontBackAPI;

namespace FAT01100Model.VMs
{
    /// <summary>
    /// ViewModel for FAT01100 Expense Allocation operations
    /// Handles UI data binding and business logic for expense allocation
    /// </summary>
    public class FAT01100ExpenseAllocationViewModel : R_ViewModel<FAT01100ExpenseAllocationDTO>
    {
        private readonly FAT01100ExpenseAllocationModel _model = new FAT01100ExpenseAllocationModel();

        /// <summary>
        /// Main entity for synchronization
        /// </summary>
        public FAT01100ExpenseAllocationDTO Entity { get; set; } = new FAT01100ExpenseAllocationDTO();

        /// <summary>
        /// Asset expense allocation list collection for grid binding
        /// </summary>
        public ObservableCollection<FAT01100ExpenseAllocationRSP_FA_GET_ASSET_EXP_ALLOC_LISTResultDTO> AssetExpAllocList { get; set; } = new ObservableCollection<FAT01100ExpenseAllocationRSP_FA_GET_ASSET_EXP_ALLOC_LISTResultDTO>();

        /// <summary>
        /// Transaction expense allocation list collection for grid binding
        /// </summary>
        public ObservableCollection<FAT01100ExpenseAllocationRSP_FA_GET_TRANS_EXP_ALLOC_LISTResultDTO> TransExpAllocList { get; set; } = new ObservableCollection<FAT01100ExpenseAllocationRSP_FA_GET_TRANS_EXP_ALLOC_LISTResultDTO>();

       

        
        #region RSP_FA_GET_ASSET_EXP_ALLOC_LIST

        /// <summary>
        /// Get asset expense allocation list
        /// </summary>
        /// <param name="poParameter">Parameter DTO</param>
        public async Task GetAssetExpAllocListAsync(string lcAssetCode)
        {
            var loEx = new R_Exception();
            try
            {
                 R_FrontContext.R_SetStreamingContext(FAT01100ContextConstants.CASSET_CODE, lcAssetCode);
                var loResult = await _model.RSP_FA_GET_ASSET_EXP_ALLOC_LISTAsync();
                AssetExpAllocList = new ObservableCollection<FAT01100ExpenseAllocationRSP_FA_GET_ASSET_EXP_ALLOC_LISTResultDTO>(
                    loResult.Data ?? new List<FAT01100ExpenseAllocationRSP_FA_GET_ASSET_EXP_ALLOC_LISTResultDTO>());
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        #endregion

        #region RSP_FA_GET_TRANS_EXP_ALLOC_LIST

        /// <summary>
        /// Get transaction expense allocation list
        /// </summary>
        /// <param name="poParameter">Parameter DTO</param>
        public async Task GetTransExpAllocListAsync(FAT01100ExpenseAllocationRSP_FA_GET_TRANS_EXP_ALLOC_LISTParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            try
            {
                var loResult = await _model.RSP_FA_GET_TRANS_EXP_ALLOC_LISTAsync();
                TransExpAllocList = new ObservableCollection<FAT01100ExpenseAllocationRSP_FA_GET_TRANS_EXP_ALLOC_LISTResultDTO>(
                    loResult.Data ?? new List<FAT01100ExpenseAllocationRSP_FA_GET_TRANS_EXP_ALLOC_LISTResultDTO>());
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
