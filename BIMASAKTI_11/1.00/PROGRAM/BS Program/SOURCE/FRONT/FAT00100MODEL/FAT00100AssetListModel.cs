using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using R_CommonFrontBackAPI;
using FAT00100Common;
using FAT00100Common.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FAT00100Model
{
    /// <summary>
    /// Model class for FAT00100 Asset List operations
    /// Handles communication with backend service for asset list
    /// </summary>
    public class FAT00100AssetListModel : R_BusinessObjectServiceClientBase<FAT00100GetAssetListResultDTO>, IFAT00100AssetList
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlFA";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/FAT00100AssetList";
        private const string DEFAULT_MODULE = "FA";

        public FAT00100AssetListModel()
            : base(DEFAULT_HTTP_NAME, DEFAULT_SERVICEPOINT_NAME, DEFAULT_MODULE, true, true)
        {
        }

        /// <summary>
        /// Get asset list - Interface method (throws NotImplementedException)
        /// </summary>
        public IAsyncEnumerable<FAT00100GetAssetListResultDTO> GetAssetList()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get asset list - Actual implementation
        /// </summary>
        public async Task<FAT00100ResultDTO<List<FAT00100GetAssetListResultDTO>>> GetAssetListAsync()
        {
            var loEx = new R_Exception();
            FAT00100ResultDTO<List<FAT00100GetAssetListResultDTO>> loRtn = new FAT00100ResultDTO<List<FAT00100GetAssetListResultDTO>>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                // Use FAT00100AssetList controller endpoint
                loRtn.Data = await R_HTTPClientWrapper.R_APIRequestStreamingObject<FAT00100GetAssetListResultDTO>(
                    _RequestServiceEndPoint,
                    nameof(IFAT00100AssetList.GetAssetList),
                    _ModuleName,
                    true,
                    true);
            }
            catch (Exception ex)
            {
                // Get all error messages from exception
                string lsEx = GetAllErrorMessages(ex);
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }

        /// <summary>
        /// Get all error messages from exception, including R_Exception ErrorList and inner exceptions
        /// </summary>
        private string GetAllErrorMessages(Exception ex)
        {
            var loMessages = new List<string>();

            // Add main exception message
            if (!string.IsNullOrWhiteSpace(ex.Message))
            {
                loMessages.Add($"Exception: {ex.GetType().Name}");
                loMessages.Add($"Message: {ex.Message}");
            }

            // If it's an R_Exception, get all errors from ErrorList
            if (ex is R_Exception loREx && loREx.ErrorList != null && loREx.ErrorList.Count > 0)
            {
                loMessages.Add("Error List:");
                foreach (var loError in loREx.ErrorList)
                {
                    if (loError != null && !string.IsNullOrWhiteSpace(loError.ErrDescp))
                    {
                        loMessages.Add($"  - {loError.ErrDescp}");
                    }
                }
            }

            // Add stack trace
            if (!string.IsNullOrWhiteSpace(ex.StackTrace))
            {
                loMessages.Add($"StackTrace: {ex.StackTrace}");
            }

            // Recursively get inner exception messages
            Exception? loInnerEx = ex.InnerException;
            int liLevel = 1;
            while (loInnerEx != null && liLevel <= 10) // Limit to 10 levels to prevent infinite loops
            {
                loMessages.Add($"Inner Exception {liLevel}: {loInnerEx.GetType().Name}");
                if (!string.IsNullOrWhiteSpace(loInnerEx.Message))
                {
                    loMessages.Add($"  Message: {loInnerEx.Message}");
                }
                loInnerEx = loInnerEx.InnerException;
                liLevel++;
            }

            return string.Join(Environment.NewLine, loMessages);
        }
    }
}

