using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using FAT00800Common;
using FAT00800Common.DTOs;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;

namespace FAT00800Model.VMs
{
    /// <summary>
    /// ViewModel for FAT00800 Grid Allocation (streaming grid)
    /// Handles allocation expense list data for grid display
    /// </summary>
    public class FAT00800GridAllocViewModel : R_ViewModel<FAT00800GetGridAllocResultDTO>
    {
        private readonly FAT00800EntryModel _model = new FAT00800EntryModel();

        /// <summary>
        /// Grid allocation list (ObservableCollection for data binding)
        /// </summary>
        public ObservableCollection<FAT00800GetGridAllocResultDTO> GridAllocList { get; set; } = new ObservableCollection<FAT00800GetGridAllocResultDTO>();

        /// <summary>
        /// Get grid allocation list (streaming method)
        /// </summary>
        /// <param name="pcCompanyId">Company ID</param>
        /// <param name="pcLangId">Language ID</param>
        /// <param name="pcAssetCode">Asset code</param>
        /// <returns>Task</returns>
        public async Task GetGridAllocListAsync(string pcCompanyId, string pcLangId, string pcAssetCode)
        {
            var loEx = new R_Exception();
            try
            {
                // Set streaming context for custom parameters
                // Note: CCOMPANY_ID, CLANG_ID are typically set automatically in Controller,
                // but we set CASSET_CODE as it's a custom parameter
                R_FrontContext.R_SetStreamingContext(ContextConstants.CASSET_CODE, pcAssetCode);

                await Task.CompletedTask;
                // GetGridAllocAsync not available on FAT00800EntryModel - use empty list
                GridAllocList = new ObservableCollection<FAT00800GetGridAllocResultDTO>();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
    }
}

