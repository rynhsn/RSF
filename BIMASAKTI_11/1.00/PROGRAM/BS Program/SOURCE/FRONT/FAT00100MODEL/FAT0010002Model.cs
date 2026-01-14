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
    /// Model class for FAT0010002 - Fixed Asset Acquisition Detail operations
    /// Handles communication with backend service
    /// </summary>
    public class FAT0010002Model : R_BusinessObjectServiceClientBase<FAT0010002DTO>, IFAT0010002
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlFA";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/FAT0010002";
        private const string DEFAULT_MODULE = "FA";

        public FAT0010002Model()
            : base(DEFAULT_HTTP_NAME, DEFAULT_SERVICEPOINT_NAME, DEFAULT_MODULE, true, true)
        {
        }

        #region CRUD Methods

        public async Task<R_ServiceGetRecordResultDTO<FAT0010002DTO>> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<FAT0010002DTO> poParameter)
        {
            var loEx = new R_Exception();
            R_ServiceGetRecordResultDTO<FAT0010002DTO> loResult = new R_ServiceGetRecordResultDTO<FAT0010002DTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<R_ServiceGetRecordResultDTO<FAT0010002DTO>, R_ServiceGetRecordParameterDTO<FAT0010002DTO>>(
                    _RequestServiceEndPoint,
                    nameof(IFAT0010002.R_ServiceGetRecord),
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

        public async Task<R_ServiceSaveResultDTO<FAT0010002DTO>> R_ServiceSave(R_ServiceSaveParameterDTO<FAT0010002DTO> poParameter)
        {
            var loEx = new R_Exception();
            R_ServiceSaveResultDTO<FAT0010002DTO> loResult = new R_ServiceSaveResultDTO<FAT0010002DTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<R_ServiceSaveResultDTO<FAT0010002DTO>, R_ServiceSaveParameterDTO<FAT0010002DTO>>(
                    _RequestServiceEndPoint,
                    nameof(IFAT0010002.R_ServiceSave),
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

        public async Task<R_ServiceDeleteResultDTO> R_ServiceDelete(R_ServiceDeleteParameterDTO<FAT0010002DTO> poParameter)
        {
            var loEx = new R_Exception();
            R_ServiceDeleteResultDTO loResult = new R_ServiceDeleteResultDTO();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<R_ServiceDeleteResultDTO, R_ServiceDeleteParameterDTO<FAT0010002DTO>>(
                    _RequestServiceEndPoint,
                    nameof(IFAT0010002.R_ServiceDelete),
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

        #region Non-Streaming Methods

        public async Task<FAT0010002ResultDTO<FAT0010002GetFAAcquisitionDetailHeaderResultDTO>> GetFAAcquisitionDetailHeader(FAT0010002GetFAAcquisitionDetailHeaderParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            FAT0010002ResultDTO<FAT0010002GetFAAcquisitionDetailHeaderResultDTO> loResult = new FAT0010002ResultDTO<FAT0010002GetFAAcquisitionDetailHeaderResultDTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<FAT0010002ResultDTO<FAT0010002GetFAAcquisitionDetailHeaderResultDTO>, FAT0010002GetFAAcquisitionDetailHeaderParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IFAT0010002.GetFAAcquisitionDetailHeader),
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

        public async Task<FAT0010002ResultDTO<FAT0010002ValidateDeptCodeResultDTO>> ValidateDeptCode(FAT0010002ValidateDeptCodeParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            FAT0010002ResultDTO<FAT0010002ValidateDeptCodeResultDTO> loResult = new FAT0010002ResultDTO<FAT0010002ValidateDeptCodeResultDTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<FAT0010002ResultDTO<FAT0010002ValidateDeptCodeResultDTO>, FAT0010002ValidateDeptCodeParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IFAT0010002.ValidateDeptCode),
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

        public async Task<FAT0010002ResultDTO<FAT0010002GetDecliningDeprAmtResultDTO>> GetDecliningDeprAmt(FAT0010002GetDecliningDeprAmtParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            FAT0010002ResultDTO<FAT0010002GetDecliningDeprAmtResultDTO> loResult = new FAT0010002ResultDTO<FAT0010002GetDecliningDeprAmtResultDTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<FAT0010002ResultDTO<FAT0010002GetDecliningDeprAmtResultDTO>, FAT0010002GetDecliningDeprAmtParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IFAT0010002.GetDecliningDeprAmt),
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

        public async Task<FAT0010002ResultDTO<FAT0010002GetTransDetailResultDTO>> FAT0010002GetTransDetail(FAT0010002GetTransDetailParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            FAT0010002ResultDTO<FAT0010002GetTransDetailResultDTO> loResult = new FAT0010002ResultDTO<FAT0010002GetTransDetailResultDTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<FAT0010002ResultDTO<FAT0010002GetTransDetailResultDTO>, FAT0010002GetTransDetailParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IFAT0010002.FAT0010002GetTransDetail),
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

        #region Streaming Methods

        /// <summary>
        /// Get combo depreciation method - Interface method (throws NotImplementedException)
        /// </summary>
        public IAsyncEnumerable<FAT00100GetStatusListResultDTO> GetComboDepreciationMethod()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get combo depreciation method - Actual implementation
        /// </summary>
        public async Task<FAT0010002ResultDTO<List<FAT00100GetStatusListResultDTO>>> GetComboDepreciationMethodAsync()
        {
            var loEx = new R_Exception();
            FAT0010002ResultDTO<List<FAT00100GetStatusListResultDTO>> loRtn = new FAT0010002ResultDTO<List<FAT00100GetStatusListResultDTO>>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loRtn.Data = await R_HTTPClientWrapper.R_APIRequestStreamingObject<FAT00100GetStatusListResultDTO>(
                    _RequestServiceEndPoint,
                    nameof(IFAT0010002.GetComboDepreciationMethod),
                    _ModuleName,
                    true,
                    true);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }

        /// <summary>
        /// Get FA acquisition detail asset list - Interface method (throws NotImplementedException)
        /// </summary>
        public IAsyncEnumerable<FAT0010002GetFAAcquisitionDetailAssetListResultDTO> GetFAAcquisitionDetailAssetList()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get FA acquisition detail asset list - Actual implementation
        /// </summary>
        public async Task<FAT0010002ResultDTO<List<FAT0010002GetFAAcquisitionDetailAssetListResultDTO>>> GetFAAcquisitionDetailAssetListAsync()
        {
            var loEx = new R_Exception();
            FAT0010002ResultDTO<List<FAT0010002GetFAAcquisitionDetailAssetListResultDTO>> loRtn = new FAT0010002ResultDTO<List<FAT0010002GetFAAcquisitionDetailAssetListResultDTO>>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loRtn.Data = await R_HTTPClientWrapper.R_APIRequestStreamingObject<FAT0010002GetFAAcquisitionDetailAssetListResultDTO>(
                    _RequestServiceEndPoint,
                    nameof(IFAT0010002.GetFAAcquisitionDetailAssetList),
                    _ModuleName,
                    true,
                    true);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }

        /// <summary>
        /// Get FA acquisition detail alloc expen page list - Interface method (throws NotImplementedException)
        /// </summary>
        public IAsyncEnumerable<FAT0010002GetFAAcquisitionDetailAllocExpenPageListResultDTO> GetFAAcquisitionDetailAllocExpenPageList()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get FA acquisition detail alloc expen page list - Actual implementation
        /// </summary>
        public async Task<FAT0010002ResultDTO<List<FAT0010002GetFAAcquisitionDetailAllocExpenPageListResultDTO>>> GetFAAcquisitionDetailAllocExpenPageListAsync()
        {
            var loEx = new R_Exception();
            FAT0010002ResultDTO<List<FAT0010002GetFAAcquisitionDetailAllocExpenPageListResultDTO>> loRtn = new FAT0010002ResultDTO<List<FAT0010002GetFAAcquisitionDetailAllocExpenPageListResultDTO>>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loRtn.Data = await R_HTTPClientWrapper.R_APIRequestStreamingObject<FAT0010002GetFAAcquisitionDetailAllocExpenPageListResultDTO>(
                    _RequestServiceEndPoint,
                    nameof(IFAT0010002.GetFAAcquisitionDetailAllocExpenPageList),
                    _ModuleName,
                    true,
                    true);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }

        /// <summary>
        /// Get transaction expense allocation list - Interface method (throws NotImplementedException)
        /// </summary>
        public IAsyncEnumerable<FAT00100GetTransExpAllocListResultDTO> FAT00100GetTransExpAllocList()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get transaction expense allocation list - Actual implementation
        /// </summary>
        public async Task<FAT0010002ResultDTO<List<FAT00100GetTransExpAllocListResultDTO>>> FAT00100GetTransExpAllocListAsync()
        {
            var loEx = new R_Exception();
            FAT0010002ResultDTO<List<FAT00100GetTransExpAllocListResultDTO>> loRtn = new FAT0010002ResultDTO<List<FAT00100GetTransExpAllocListResultDTO>>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loRtn.Data = await R_HTTPClientWrapper.R_APIRequestStreamingObject<FAT00100GetTransExpAllocListResultDTO>(
                    _RequestServiceEndPoint,
                    nameof(IFAT0010002.FAT00100GetTransExpAllocList),
                    _ModuleName,
                    true,
                    true);
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

