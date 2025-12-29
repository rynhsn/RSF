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
    public class FAT00100AssetListModel : R_BusinessObjectServiceClientBase<FAT00100GetTransAssetListResultDTO>, IFAT00100AssetList
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlFA";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/FAT00100AssetList";
        private const string DEFAULT_MODULE = "FA";

        public FAT00100AssetListModel()
            : base(DEFAULT_HTTP_NAME, DEFAULT_SERVICEPOINT_NAME, DEFAULT_MODULE, true, true)
        {
        }

        #region Streaming Methods

        /// <summary>
        /// Get transaction asset list - Interface method (throws NotImplementedException)
        /// </summary>
        public IAsyncEnumerable<FAT00100GetTransAssetListResultDTO> FAT00100GetTransAssetList()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get transaction asset list - Actual implementation
        /// </summary>
        public async Task<FAT00100ResultDTO<List<FAT00100GetTransAssetListResultDTO>>> FAT00100GetTransAssetListAsync()
        {
            var loEx = new R_Exception();
            FAT00100ResultDTO<List<FAT00100GetTransAssetListResultDTO>> loRtn = new FAT00100ResultDTO<List<FAT00100GetTransAssetListResultDTO>>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loRtn.Data = await R_HTTPClientWrapper.R_APIRequestStreamingObject<FAT00100GetTransAssetListResultDTO>(
                    _RequestServiceEndPoint,
                    nameof(IFAT00100AssetList.FAT00100GetTransAssetList),
                    _ModuleName,
                    _SendWithContext,
                    _SendWithToken);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }

        #endregion

        #region Non-Streaming Methods

        /// <summary>
        /// Get transaction asset - Non-streaming method
        /// </summary>
        /// <param name="poParameter">Parameter containing company ID, record ID, dept code, ref no, trans seq no, and language ID</param>
        /// <returns>Transaction asset result DTO</returns>
        public async Task<FAT00100ResultDTO<FAT00100GetTransAssetResultDTO>> FAT00100GetTransAsset(FAT00100GetTransAssetParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            FAT00100ResultDTO<FAT00100GetTransAssetResultDTO> loResult = new FAT00100ResultDTO<FAT00100GetTransAssetResultDTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<FAT00100ResultDTO<FAT00100GetTransAssetResultDTO>, FAT00100GetTransAssetParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IFAT00100AssetList.FAT00100GetTransAsset),
                    poParameter,
                    _ModuleName,
                    _SendWithContext,
                    _SendWithToken);
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

