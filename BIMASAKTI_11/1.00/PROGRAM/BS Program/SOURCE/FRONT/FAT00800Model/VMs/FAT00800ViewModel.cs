using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using FAT00800Common;
using FAT00800Common.DTOs;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_CommonFrontBackAPI;

namespace FAT00800Model.VMs
{
    /// <summary>
    /// ViewModel for FAT00800 List operations - Transaction List functionality
    /// Handles UI data binding and business logic for list operations
    /// </summary>
    public class FAT00800ViewModel : R_ViewModel<FAT00800GetTransListResultDTO>
    {
        private readonly FAT00800Model _listModel = new FAT00800Model();

        /// <summary>
        /// Transaction list collection for grid binding
        /// </summary>
        public ObservableCollection<FAT00800GetTransListResultDTO> TransList { get; set; } = new ObservableCollection<FAT00800GetTransListResultDTO>();

        /// <summary>
        /// System parameter result (from FAT00800GetGetSystemParam)
        /// </summary>
        public FAT00800GetGetSystemParamResultDTO SystemParamData { get; set; } = new FAT00800GetGetSystemParamResultDTO();

        /// <summary>
        /// Year range result (from FAT00800GetYearRange)
        /// </summary>
        public FAT00800GetYearRangeResultDTO YearRangeData { get; set; } = new FAT00800GetYearRangeResultDTO();

        // declare parameter DTO
        public FAT00800GetTransListParameterDTO ParameterDTO { get; set; } = new FAT00800GetTransListParameterDTO();

        public FAT00800ViewModel()
        {
            R_SetCurrentData(new FAT00800GetTransListResultDTO());
        }

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

        public int YearFrom { get; set; } = DateTime.Now.Year;
        public int YearTo { get; set; } = DateTime.Now.Year;  
        public string MonthFrom { get; set; } = DateTime.Now.Month.ToString("00");
        public string MonthTo { get; set; } = DateTime.Now.Month.ToString("00");
        public string DeptName { get; set; } = string.Empty;
        public string AssetName { get; set; } = string.Empty;

        #region Streaming Methods

        /// <summary>
        /// Get transaction list - sets streaming context from parameter DTO and calls FAT00800GetTransList streaming endpoint
        /// </summary>
        /// <param name="poParameter">Parameter DTO (CCOMPANY_ID, CUSER_ID, CDEPT_CODE, CFROM_PERIOD, CTO_PERIOD, CASSET_CODE, CLANGUAGE_ID)</param>
        public async Task FAT00800GetTransListAsync()
        {
            var loEx = new R_Exception();

            try
            {
                ParameterDTO.CFROM_PERIOD = YearFrom + MonthFrom;
                ParameterDTO.CTO_PERIOD = YearTo + MonthTo;
                R_FrontContext.R_SetStreamingContext(ContextConstants.CDEPT_CODE, ParameterDTO.CDEPT_CODE ?? string.Empty);
                R_FrontContext.R_SetStreamingContext(ContextConstants.CFROM_PERIOD, ParameterDTO.CFROM_PERIOD ?? string.Empty);
                R_FrontContext.R_SetStreamingContext(ContextConstants.CTO_PERIOD, ParameterDTO.CTO_PERIOD ?? string.Empty);
                R_FrontContext.R_SetStreamingContext(ContextConstants.CASSET_CODE, ParameterDTO.CASSET_CODE ?? string.Empty);
                R_FrontContext.R_SetStreamingContext(ContextConstants.CLANGUAGE_ID, ParameterDTO.CLANGUAGE_ID ?? string.Empty);

                var loResult = await _listModel.FAT00800GetTransListAsync();
                TransList = new ObservableCollection<FAT00800GetTransListResultDTO>(loResult.Data ?? new List<FAT00800GetTransListResultDTO>());
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        #endregion

        #region FAT00800Cls Delegation (GetSystemParam, GetYearRange)

        /// <summary>
        /// Get system parameters - calls FAT00800Model.FAT00800GetGetSystemParam
        /// </summary>
        /// <param name="pcCompanyId">Company ID</param>
        /// <param name="pcLangId">Language/Culture ID</param>
        /// <param name="pcUserId">User ID</param>
        /// <param name="pcLanguageId">Language ID (method-specific)</param>
        public async Task FAT00800GetGetSystemParamAsync(string pcCompanyId, string pcLangId, string pcUserId, string pcLanguageId)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = new FAT00800GetGetSystemParamParameterDTO
                {
                    CCOMPANY_ID = pcCompanyId,
                    CLANG_ID = pcLangId,
                    CUSER_ID = pcUserId,
                    CLANGUAGE_ID = pcLanguageId
                };

                var loResult = await _listModel.FAT00800GetGetSystemParam(loParam);
                SystemParamData = loResult.Data ?? new FAT00800GetGetSystemParamResultDTO();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        /// <summary>
        /// Get year range - calls FAT00800Model.FAT00800GetYearRange
        /// </summary>
        /// <param name="pcCompanyId">Company ID</param>
        /// <param name="pcLangId">Language/Culture ID</param>
        /// <param name="pcUserId">User ID</param>
        /// <param name="pcCcyear">Currency year</param>
        /// <param name="pcMode">Mode</param>
        public async Task FAT00800GetYearRangeAsync(string pcCompanyId)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = new FAT00800GetYearRangeParameterDTO
                {
                    CCOMPANY_ID = pcCompanyId,
                    CLANG_ID = "",
                    CUSER_ID = "",
                    CCYEAR = "",
                    CMODE = ""
                };

                var loResult = await _listModel.FAT00800GetYearRange(loParam);
                YearRangeData = loResult.Data ?? new FAT00800GetYearRangeResultDTO();
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
