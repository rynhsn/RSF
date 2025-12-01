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
    /// Model class for FAT0010003 - Fixed Asset Transaction Detail operations
    /// Handles communication with backend service
    /// </summary>
    public class FAT0010003Model : R_BusinessObjectServiceClientBase<FAT0010003DTO>, IFAT0010003
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlFA";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/FAT0010003";
        private const string DEFAULT_MODULE = "FA";

        public FAT0010003Model()
            : base(DEFAULT_HTTP_NAME, DEFAULT_SERVICEPOINT_NAME, DEFAULT_MODULE, true, true)
        {
        }

        #region CRUD Methods

        public async Task<R_ServiceGetRecordResultDTO<FAT0010003DTO>> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<FAT0010003DTO> poParameter)
        {
            var loEx = new R_Exception();
            R_ServiceGetRecordResultDTO<FAT0010003DTO> loResult = new R_ServiceGetRecordResultDTO<FAT0010003DTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<R_ServiceGetRecordResultDTO<FAT0010003DTO>, R_ServiceGetRecordParameterDTO<FAT0010003DTO>>(
                    _RequestServiceEndPoint,
                    nameof(IFAT0010003.R_ServiceGetRecord),
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

        public async Task<R_ServiceSaveResultDTO<FAT0010003DTO>> R_ServiceSave(R_ServiceSaveParameterDTO<FAT0010003DTO> poParameter)
        {
            var loEx = new R_Exception();
            R_ServiceSaveResultDTO<FAT0010003DTO> loResult = new R_ServiceSaveResultDTO<FAT0010003DTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<R_ServiceSaveResultDTO<FAT0010003DTO>, R_ServiceSaveParameterDTO<FAT0010003DTO>>(
                    _RequestServiceEndPoint,
                    nameof(IFAT0010003.R_ServiceSave),
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

        public async Task<R_ServiceDeleteResultDTO> R_ServiceDelete(R_ServiceDeleteParameterDTO<FAT0010003DTO> poParameter)
        {
            var loEx = new R_Exception();
            R_ServiceDeleteResultDTO loResult = new R_ServiceDeleteResultDTO();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<R_ServiceDeleteResultDTO, R_ServiceDeleteParameterDTO<FAT0010003DTO>>(
                    _RequestServiceEndPoint,
                    nameof(IFAT0010003.R_ServiceDelete),
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

        public async Task<FAT0010003ResultDTO<FAT0010003GetDataHeaderResultDTO>> GetDataHeader(FAT0010003GetDataHeaderParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            FAT0010003ResultDTO<FAT0010003GetDataHeaderResultDTO> loResult = new FAT0010003ResultDTO<FAT0010003GetDataHeaderResultDTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<FAT0010003ResultDTO<FAT0010003GetDataHeaderResultDTO>, FAT0010003GetDataHeaderParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IFAT0010003.GetDataHeader),
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
        /// Get data grid - Interface method (throws NotImplementedException)
        /// </summary>
        public IAsyncEnumerable<FAT0010003GetDataGridResultDTO> GetDataGrid()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get data grid - Actual implementation
        /// </summary>
        public async Task<FAT0010003ResultDTO<List<FAT0010003GetDataGridResultDTO>>> GetDataGridAsync()
        {
            var loEx = new R_Exception();
            FAT0010003ResultDTO<List<FAT0010003GetDataGridResultDTO>> loRtn = new FAT0010003ResultDTO<List<FAT0010003GetDataGridResultDTO>>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loRtn.Data = await R_HTTPClientWrapper.R_APIRequestStreamingObject<FAT0010003GetDataGridResultDTO>(
                    _RequestServiceEndPoint,
                    nameof(IFAT0010003.GetDataGrid),
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

