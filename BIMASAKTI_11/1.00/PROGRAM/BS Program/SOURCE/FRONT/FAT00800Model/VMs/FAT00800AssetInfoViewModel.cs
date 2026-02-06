using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using FAT00800Common;
using FAT00800Common.DTOs;
using FAT00800FrontResources;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_CommonFrontBackAPI;

namespace FAT00800Model.VMs
{
    /// <summary>
    /// ViewModel for FAT00800 Asset Information operations
    /// Handles asset information display and allocation grid data
    /// </summary>
    public class FAT00800AssetInfoViewModel : R_ViewModel<FAT00800GetAssetInfoResultDTO>
    {
        private readonly FAT00800EntryModel _model = new FAT00800EntryModel();

        // Asset information for display
        public FAT00800GetAssetInfoResultDTO AssetInfo { get; set; } = new FAT00800GetAssetInfoResultDTO();
        
        // Grid allocation list for asset information display
        public ObservableCollection<FAT00800GetGridAllocResultDTO> GridAllocList { get; set; } = new ObservableCollection<FAT00800GetGridAllocResultDTO>();

        // Formatted date properties for UI display
        public string FormattedStartDate 
        { 
            get 
            {
                return FormatDateString(AssetInfo.CSTART_DATE);
            }
        }

        public string FormattedLastTransDate 
        { 
            get 
            {
                return FormatDateString(AssetInfo.CLAST_TRANS_DATE);
            }
        }

        #region Asset Information Methods

        /// <summary>
        /// Get asset information for Page 2
        /// </summary>
        /// <param name="loParam">Parameter DTO containing company ID, asset code, and currency code</param>
        /// <returns>Task</returns>
        public async Task GetAssetInfoAsync(FAT00800GetAssetInfoParameterDTO loParam)
        {
            var loEx = new R_Exception();
            try
            {
                await Task.CompletedTask;
                // GetAssetInfo not available on FAT00800EntryModel - use default
                AssetInfo = new FAT00800GetAssetInfoResultDTO
                {
                    CLOCAL_CURRENCY_CODE = loParam.CCURRENCY_CODE,
                    CBASE_CURRENCY_CODE = loParam.CCURRENCY_CODE
                };
                FormatDates();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        /// <summary>
        /// Get grid allocation list for asset information display
        /// </summary>
        /// <param name="loParam">Parameter DTO containing company ID, language ID, and asset code</param>
        /// <returns>Task</returns>
        public async Task GetGridAllocListAsync(FAT00800GetAssetInfoParameterDTO loParam)
        {
            var loEx = new R_Exception();
            try
            {
                // Debug: Log the parameters
                System.Diagnostics.Debug.WriteLine($"GetGridAllocListAsync - CompanyId: '{loParam.CCOMPANY_ID}', LangId: '{loParam.CLANG_ID}', AssetCode: '{loParam.CASSET_CODE}'");
                
                // Set streaming context for custom parameters
                R_FrontContext.R_SetStreamingContext(ContextConstants.CASSET_CODE, loParam.CASSET_CODE);

                await Task.CompletedTask;
                // GetGridAllocAsync not available on FAT00800EntryModel - use empty list
                var loData = new List<FAT00800GetGridAllocResultDTO>();
                GridAllocList = new ObservableCollection<FAT00800GetGridAllocResultDTO>(loData);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Format date string from database format (yyyyMMdd) to dd-MMM-yyyy format
        /// </summary>
        /// <param name="dateString">Date string from database (e.g., "20240716")</param>
        /// <returns>Formatted date string (e.g., "16-Jul-2024")</returns>
        private string FormatDateString(string dateString)
        {
            try
            {
                if (string.IsNullOrEmpty(dateString))
                {
                    return string.Empty;
                }

                // Debug: Log original date value
                System.Diagnostics.Debug.WriteLine($"FormatDateString - Input: '{dateString}'");

                // Try to parse as yyyyMMdd format first (database format)
                if (dateString.Length == 8 && DateTime.TryParseExact(dateString, "yyyyMMdd", null, System.Globalization.DateTimeStyles.None, out DateTime parsedDate))
                {
                    var formatted = parsedDate.ToString("dd-MMM-yyyy");
                    System.Diagnostics.Debug.WriteLine($"FormatDateString - Parsed yyyyMMdd format: '{dateString}' -> '{formatted}'");
                    return formatted;
                }

                // Try standard DateTime parsing as fallback
                if (DateTime.TryParse(dateString, out DateTime standardDate))
                {
                    var formatted = standardDate.ToString("dd-MMM-yyyy");
                    System.Diagnostics.Debug.WriteLine($"FormatDateString - Parsed standard format: '{dateString}' -> '{formatted}'");
                    return formatted;
                }

                // If parsing fails, return original value
                System.Diagnostics.Debug.WriteLine($"FormatDateString - Failed to parse: '{dateString}', returning original");
                return dateString;
            }
            catch (Exception ex)
            {
                // Log error but don't throw - date formatting is not critical
                System.Diagnostics.Debug.WriteLine($"FormatDateString - Error formatting date '{dateString}': {ex.Message}");
                return dateString; // Return original value on error
            }
        }

        /// <summary>
        /// Format date fields to dd-MMM-yyyy format (legacy method, kept for debugging)
        /// </summary>
        private void FormatDates()
        {
            try
            {
                // Debug: Log original date values
                System.Diagnostics.Debug.WriteLine($"FormatDates - Original CSTART_DATE: '{AssetInfo.CSTART_DATE}'");
                System.Diagnostics.Debug.WriteLine($"FormatDates - Original CLAST_TRANS_DATE: '{AssetInfo.CLAST_TRANS_DATE}'");

                // Format CSTART_DATE
                if (!string.IsNullOrEmpty(AssetInfo.CSTART_DATE))
                {
                    if (DateTime.TryParse(AssetInfo.CSTART_DATE, out DateTime startDate))
                    {
                        var formattedStartDate = startDate.ToString("dd-MMM-yyyy");
                        System.Diagnostics.Debug.WriteLine($"FormatDates - Formatted CSTART_DATE: '{formattedStartDate}'");
                        AssetInfo.CSTART_DATE = formattedStartDate;
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine($"FormatDates - Failed to parse CSTART_DATE: '{AssetInfo.CSTART_DATE}'");
                    }
                }

                // Format CLAST_TRANS_DATE
                if (!string.IsNullOrEmpty(AssetInfo.CLAST_TRANS_DATE))
                {
                    if (DateTime.TryParse(AssetInfo.CLAST_TRANS_DATE, out DateTime lastTransDate))
                    {
                        var formattedLastTransDate = lastTransDate.ToString("dd-MMM-yyyy");
                        System.Diagnostics.Debug.WriteLine($"FormatDates - Formatted CLAST_TRANS_DATE: '{formattedLastTransDate}'");
                        AssetInfo.CLAST_TRANS_DATE = formattedLastTransDate;
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine($"FormatDates - Failed to parse CLAST_TRANS_DATE: '{AssetInfo.CLAST_TRANS_DATE}'");
                    }
                }
            }
            catch (Exception ex)
            {
                // Log error but don't throw - date formatting is not critical
                System.Diagnostics.Debug.WriteLine($"FormatDates - Error formatting dates: {ex.Message}");
            }
        }

        #endregion
    }
}
