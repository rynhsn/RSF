using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FAT01100Common;
using FAT01100Common.DTOs;
using R_APIClient;
using R_APICommonDTO;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using R_CommonFrontBackAPI;

namespace FAT01100Model
{
    /// <summary>
    /// Model class for FAT01100 List operations - Get Transaction List
    /// Handles communication with FAT01100Controller
    /// </summary>
    public class FAT01100Model : R_BusinessObjectServiceClientBase<FAT01100GeTransListResultDTO>, IFAT01100
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlFA";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/FAT01100";
        private const string DEFAULT_MODULE = "FA";

        public FAT01100Model()
            : base(DEFAULT_HTTP_NAME, DEFAULT_SERVICEPOINT_NAME, DEFAULT_MODULE, true, true)
        {
        }

        /// <summary>
        /// Get transaction list - calls FAT01100Controller.FAT01100GeTransList
        /// </summary>
        public async Task<FAT01100ResultDTO<List<FAT01100GeTransListResultDTO>>> FAT01100GeTransList(FAT01100GeTransListParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            FAT01100ResultDTO<List<FAT01100GeTransListResultDTO>> loRtn = new FAT01100ResultDTO<List<FAT01100GeTransListResultDTO>>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<FAT01100ResultDTO<List<FAT01100GeTransListResultDTO>>, FAT01100GeTransListParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IFAT01100.FAT01100GeTransList),
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
        /// Get department lookup list - calls FAT01100Controller.FAT01100GetDeptLookupList
        /// </summary>
        public async Task<FAT01100ResultDTO<List<FAT01100GetDeptLookupListResultDTO>>> FAT01100GetDeptLookupList(FAT01100GetDeptLookupListParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            FAT01100ResultDTO<List<FAT01100GetDeptLookupListResultDTO>> loRtn = new FAT01100ResultDTO<List<FAT01100GetDeptLookupListResultDTO>>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<FAT01100ResultDTO<List<FAT01100GetDeptLookupListResultDTO>>, FAT01100GetDeptLookupListParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IFAT01100.FAT01100GetDeptLookupList),
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
        /// Get year range - calls FAT01100Controller.FAT01100GetYearRange
        /// </summary>
        public async Task<FAT01100ResultDTO<FAT01100GetYearRangeResultDTO>> FAT01100GetYearRange(FAT01100GetYearRangeParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            FAT01100ResultDTO<FAT01100GetYearRangeResultDTO> loRtn = new FAT01100ResultDTO<FAT01100GetYearRangeResultDTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<FAT01100ResultDTO<FAT01100GetYearRangeResultDTO>, FAT01100GetYearRangeParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IFAT01100.FAT01100GetYearRange),
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
        /// Get system param - calls FAT01100Controller.FAT01100GetGetSystemParam
        /// </summary>
        public async Task<FAT01100ResultDTO<FAT01100GetGetSystemParamResultDTO>> FAT01100GetGetSystemParam(FAT01100GetGetSystemParamParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            FAT01100ResultDTO<FAT01100GetGetSystemParamResultDTO> loRtn = new FAT01100ResultDTO<FAT01100GetGetSystemParamResultDTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<FAT01100ResultDTO<FAT01100GetGetSystemParamResultDTO>, FAT01100GetGetSystemParamParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IFAT01100.FAT01100GetGetSystemParam),
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
    }
}
