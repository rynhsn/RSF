using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FAT00800Common;
using FAT00800Common.DTOs;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using R_CommonFrontBackAPI;

namespace FAT00800Model
{
    /// <summary>
    /// Model class for FAT00800 List operations - Transaction List functionality
    /// Handles communication with FAT00800Controller
    /// </summary>
    public class FAT00800Model : R_BusinessObjectServiceClientBase<FAT00800GetTransListResultDTO>, IFAT00800
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlFA";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/FAT00800";
        private const string DEFAULT_MODULE = "FA";

        public FAT00800Model()
            : base(DEFAULT_HTTP_NAME, DEFAULT_SERVICEPOINT_NAME, DEFAULT_MODULE, true, true)
        {
        }

        #region Streaming Methods

        /// <summary>
        /// Get transaction list - Interface method (streaming, throws NotImplementedException)
        /// </summary>
        public IAsyncEnumerable<FAT00800GetTransListResultDTO> FAT00800GetTransList()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get transaction list - Actual implementation via streaming endpoint
        /// </summary>
        public async Task<FAT00800ResultDTO<List<FAT00800GetTransListResultDTO>>> FAT00800GetTransListAsync()
        {
            var loEx = new R_Exception();
            var loRtn = new FAT00800ResultDTO<List<FAT00800GetTransListResultDTO>>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loRtn.Data = await R_HTTPClientWrapper.R_APIRequestStreamingObject<FAT00800GetTransListResultDTO>(
                    _RequestServiceEndPoint,
                    nameof(IFAT00800.FAT00800GetTransList),
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

        #region FAT00800Cls Delegation (GetSystemParam, GetYearRange)

        /// <summary>
        /// Get system parameters - calls FAT00800Controller.FAT00800GetGetSystemParam
        /// </summary>
        public async Task<FAT00800ResultDTO<FAT00800GetGetSystemParamResultDTO>> FAT00800GetGetSystemParam(FAT00800GetGetSystemParamParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            var loRtn = new FAT00800ResultDTO<FAT00800GetGetSystemParamResultDTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<FAT00800ResultDTO<FAT00800GetGetSystemParamResultDTO>, FAT00800GetGetSystemParamParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IFAT00800.FAT00800GetGetSystemParam),
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
            return loRtn;
        }

        /// <summary>
        /// Get year range - calls FAT00800Controller.FAT00800GetYearRange
        /// </summary>
        public async Task<FAT00800ResultDTO<FAT00800GetYearRangeResultDTO>> FAT00800GetYearRange(FAT00800GetYearRangeParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            var loRtn = new FAT00800ResultDTO<FAT00800GetYearRangeResultDTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<FAT00800ResultDTO<FAT00800GetYearRangeResultDTO>, FAT00800GetYearRangeParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IFAT00800.FAT00800GetYearRange),
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
            return loRtn;
        }

        #endregion
    }
}
