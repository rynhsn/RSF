using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using FAT00800Common;
using FAT00800Common.DTOs;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;

namespace FAT00800Model.VMs
{
    /// <summary>
    /// ViewModel for FAT00800 List operations - Transaction List functionality
    /// Handles UI data binding and business logic for list operations
    /// </summary>
    public class FAT00800ListViewModel : R_ViewModel<FAT00800TransListResultDTO>
    {
        private FAT00800ListModel _model;
        private FAT00800Model _mainModel; // For validation methods

        public FAT00800ListViewModel()
        {
            _model = new FAT00800ListModel();
            _mainModel = new FAT00800Model();
        }

        #region Properties

        public ObservableCollection<FAT00800TransListResultDTO> TransactionList { get; set; } = new ObservableCollection<FAT00800TransListResultDTO>();

        // Period filter properties
        public int PeriodFromYear { get; set; } = DateTime.Now.Year;
        public string PeriodFromMonth { get; set; } = "01";
        public int PeriodToYear { get; set; } = DateTime.Now.Year;
        public string PeriodToMonth { get; set; } = "12";

        // Filter criteria
        public string DeptCode { get; set; } = string.Empty;
        public string AssetCode { get; set; } = string.Empty;

        // Month List for ComboBox
        public List<PeriodMonthDTO> MonthList { get; set; } = new List<PeriodMonthDTO>
        {
            new PeriodMonthDTO { CPERIOD_NO = "01" },
            new PeriodMonthDTO { CPERIOD_NO = "02" },
            new PeriodMonthDTO { CPERIOD_NO = "03" },
            new PeriodMonthDTO { CPERIOD_NO = "04" },
            new PeriodMonthDTO { CPERIOD_NO = "05" },
            new PeriodMonthDTO { CPERIOD_NO = "06" },
            new PeriodMonthDTO { CPERIOD_NO = "07" },
            new PeriodMonthDTO { CPERIOD_NO = "08" },
            new PeriodMonthDTO { CPERIOD_NO = "09" },
            new PeriodMonthDTO { CPERIOD_NO = "10" },
            new PeriodMonthDTO { CPERIOD_NO = "11" },
            new PeriodMonthDTO { CPERIOD_NO = "12" }
        };

        #endregion

        #region Methods

        /// <summary>
        /// Get transaction list with individual parameters (compatible with FAT00800.razor.cs)
        /// </summary>
        /// <param name="pcTransCode">Transaction code</param>
        /// <param name="pcDeptCode">Department code</param>
        /// <param name="pcFromPeriod">From period (YYYYMM)</param>
        /// <param name="pcToPeriod">To period (YYYYMM)</param>
        /// <param name="pcAssetCode">Asset code</param>
        /// <param name="pcLanguageId">Language ID</param>
        public async Task GetTransactionList(
            string pcTransCode,
            string pcDeptCode,
            string pcFromPeriod,
            string pcToPeriod,
            string pcAssetCode,
            string pcLanguageId)
        {
            var loEx = new R_Exception();
            try
            {
                // Update internal filter properties
                DeptCode = pcDeptCode;
                AssetCode = pcAssetCode;

                // Set streaming context parameters
                R_FrontContext.R_SetStreamingContext(ContextConstants.CTRANS_CODE, pcTransCode);
                R_FrontContext.R_SetStreamingContext(ContextConstants.CDEPT_CODE, pcDeptCode);
                R_FrontContext.R_SetStreamingContext(ContextConstants.CFROM_PERIOD, pcFromPeriod);
                R_FrontContext.R_SetStreamingContext(ContextConstants.CTO_PERIOD, pcToPeriod);
                R_FrontContext.R_SetStreamingContext(ContextConstants.CASSET_CODE, pcAssetCode);
                R_FrontContext.R_SetStreamingContext(ContextConstants.CLANGUAGE_ID, pcLanguageId);

                var loResult = await _model.FAT00800TransListAsync();
                TransactionList = new ObservableCollection<FAT00800TransListResultDTO>(loResult.Data ?? new List<FAT00800TransListResultDTO>());
                TransactionList.Select(x =>x.CREF_DATE_DISPLAY = DateTime.ParseExact(x.CREF_DATE, "yyyyMMdd", CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy")).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        /// <summary>
        /// Get transaction list with parameter DTO (backward compatibility)
        /// </summary>
        /// <param name="poParameter">Parameters for transaction list retrieval</param>
        public async Task GetTransactionListAsync(FAT00800TransListParameterDTO poParameter)
        {
            await GetTransactionList(
                poParameter.CTRANS_CODE,
                poParameter.CDEPT_CODE,
                poParameter.CFROM_PERIOD,
                poParameter.CTO_PERIOD,
                poParameter.CASSET_CODE,
                poParameter.CLANGUAGE_ID);
        }

        /// <summary>
        /// Validate department code
        /// </summary>
        /// <param name="pcCompanyId">Company ID</param>
        /// <param name="pcDeptCode">Department code to validate</param>
        /// <param name="pcUserId">User ID</param>
        /// <returns>Validation result (0 = invalid, 1 = valid)</returns>
        public async Task<int> ValidateDepartmentAsync(string pcCompanyId, string pcDeptCode, string pcUserId)
        {
            var loEx = new R_Exception();
            int liResult = 0;
            try
            {
                var loParam = new FAT00800GetValidateDepartmentParameterDTO
                {
                    CCOMPANY_ID = pcCompanyId,
                    CDEPT_CODE = pcDeptCode,
                    CUSER_ID = pcUserId
                };

                var loResult = await _mainModel.GetValidateDepartment(loParam);
                liResult = loResult.Data?.Result ?? 0;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            return liResult;
        }

        /// <summary>
        /// Clear transaction list
        /// </summary>
        public void ClearTransactionList()
        {
            TransactionList.Clear();
        }

        #endregion
    }

}
