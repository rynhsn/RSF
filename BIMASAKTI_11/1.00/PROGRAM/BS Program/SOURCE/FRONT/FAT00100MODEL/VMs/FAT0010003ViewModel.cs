using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using FAT00100Common;
using FAT00100Common.DTOs;
using FAT00100FrontResources;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace FAT00100Model.VMs
{
    /// <summary>
    /// ViewModel for FAT0010003 - Fixed Asset Transaction Detail
    /// Handles form operations, validation, and data retrieval
    /// </summary>
    public class FAT0010003ViewModel : R_ViewModel<FAT0010003DTO>
    {
        private readonly FAT0010003Model _model = new FAT0010003Model();

        // Current form data
        public FAT0010003DTO CurrentRecord { get; set; } = new FAT0010003DTO();

        // Header data
        public FAT0010003GetDataHeaderResultDTO HeaderData { get; set; } = new FAT0010003GetDataHeaderResultDTO();

        // Lists
        public ObservableCollection<FAT0010003GetDataGridResultDTO> DataGridList { get; set; } = new ObservableCollection<FAT0010003GetDataGridResultDTO>();

        // Form state properties (currency codes from master parameter)
        public string LocalCurrencyCode { get; set; } = string.Empty;
        public string BaseCurrencyCode { get; set; } = string.Empty;

        #region Non-Streaming Methods

        /// <summary>
        /// Get data header - non-streaming method
        /// </summary>
        public async Task GetDataHeaderAsync(string pcCompanyId, string pcLangId, string pcForeignLanguage, string pcFrDeptCode, string pcFrTransactionCode, string pcFrReferenceNo)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = new FAT0010003GetDataHeaderParameterDTO
                {
                    CCOMPANY_ID = pcCompanyId,
                    CLANG_ID = pcLangId,
                    CFOREIGN_LANGUAGE = pcForeignLanguage,
                    PCFR_DEPT_CODE = pcFrDeptCode,
                    PCFR_TRANSACTION_CODE = pcFrTransactionCode,
                    PCFR_REFERENCE_NO = pcFrReferenceNo
                };

                var loResult = await _model.GetDataHeader(loParam);
                if (loResult.Data != null)
                {
                    HeaderData = loResult.Data;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        #endregion

        #region Streaming Methods

        /// <summary>
        /// Get data grid - streaming method for main grid
        /// </summary>
        public async Task GetDataGridAsync(string pcCompanyId, string pcFrDeptCode, string pcFrTransactionCode, string pcFrReferenceNo)
        {
            var loEx = new R_Exception();

            try
            {
                // Set streaming context for custom parameters (NOT CCOMPANY_ID - handled automatically)
                R_FrontContext.R_SetStreamingContext(ContextConstants.PCFR_DEPT_CODE, pcFrDeptCode);
                R_FrontContext.R_SetStreamingContext(ContextConstants.PCFR_TRANSACTION_CODE, pcFrTransactionCode);
                R_FrontContext.R_SetStreamingContext(ContextConstants.PCFR_REFERENCE_NO, pcFrReferenceNo);

                var loResult = await _model.GetDataGridAsync();
                DataGridList = new ObservableCollection<FAT0010003GetDataGridResultDTO>(loResult.Data ?? new List<FAT0010003GetDataGridResultDTO>());
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

