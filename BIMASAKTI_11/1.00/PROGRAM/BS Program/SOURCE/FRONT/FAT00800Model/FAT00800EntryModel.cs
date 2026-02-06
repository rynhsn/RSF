using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FAT00800Common;
using FAT00800Common.DTOs;
using R_APIClient;
using R_APICommonDTO;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using R_CommonFrontBackAPI;

namespace FAT00800Model
{
    /// <summary>
    /// Model class for FAT00800 - Fixed Asset Transaction operations
    /// Handles communication with backend service
    /// </summary>
    public class FAT00800EntryModel : R_BusinessObjectServiceClientBase<FAT00800DTO>, IFAT00800Entry
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlFA";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/FAT00800Entry";
        private const string DEFAULT_MODULE = "FA";

        public FAT00800EntryModel()
            : base(DEFAULT_HTTP_NAME, DEFAULT_SERVICEPOINT_NAME, DEFAULT_MODULE, true, true)
        {
        }

        #region CRUD Methods

        public new async Task<R_ServiceGetRecordResultDTO<FAT00800DTO>> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<FAT00800DTO> poParameter)
        {
            var loEx = new R_Exception();
            R_ServiceGetRecordResultDTO<FAT00800DTO> loResult = new R_ServiceGetRecordResultDTO<FAT00800DTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<R_ServiceGetRecordResultDTO<FAT00800DTO>, R_ServiceGetRecordParameterDTO<FAT00800DTO>>(
                    _RequestServiceEndPoint,
                    nameof(IFAT00800Entry.R_ServiceGetRecord),
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

        public new async Task<R_ServiceSaveResultDTO<FAT00800DTO>> R_ServiceSave(R_ServiceSaveParameterDTO<FAT00800DTO> poParameter)
        {
            var loEx = new R_Exception();
            R_ServiceSaveResultDTO<FAT00800DTO> loResult = new R_ServiceSaveResultDTO<FAT00800DTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<R_ServiceSaveResultDTO<FAT00800DTO>, R_ServiceSaveParameterDTO<FAT00800DTO>>(
                    _RequestServiceEndPoint,
                    nameof(IFAT00800Entry.R_ServiceSave),
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

        public new async Task<R_ServiceDeleteResultDTO> R_ServiceDelete(R_ServiceDeleteParameterDTO<FAT00800DTO> poParameter)
        {
            var loEx = new R_Exception();
            R_ServiceDeleteResultDTO loResult = new R_ServiceDeleteResultDTO();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<R_ServiceDeleteResultDTO, R_ServiceDeleteParameterDTO<FAT00800DTO>>(
                    _RequestServiceEndPoint,
                    nameof(IFAT00800Entry.R_ServiceDelete),
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

        #region Get Helper Methods (CompanyInfo, SystemParam, PeriodeDtInfo, DeptLookupList, TransCodeInfo, YearRange)

        public async Task<FAT00800ResultDTO<FAT00800GetCompanyInfoResultDTO>> FAT00800GetCompanyInfoAsync(FAT00800GetCompanyInfoParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            FAT00800ResultDTO<FAT00800GetCompanyInfoResultDTO> loResult = new FAT00800ResultDTO<FAT00800GetCompanyInfoResultDTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<FAT00800ResultDTO<FAT00800GetCompanyInfoResultDTO>, FAT00800GetCompanyInfoParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IFAT00800Entry.FAT00800GetCompanyInfo),
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

        public async Task<FAT00800ResultDTO<FAT00800GetGetSystemParamResultDTO>> FAT00800GetGetSystemParamAsync(FAT00800GetGetSystemParamParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            FAT00800ResultDTO<FAT00800GetGetSystemParamResultDTO> loResult = new FAT00800ResultDTO<FAT00800GetGetSystemParamResultDTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<FAT00800ResultDTO<FAT00800GetGetSystemParamResultDTO>, FAT00800GetGetSystemParamParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IFAT00800Entry.FAT00800GetGetSystemParam),
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

        public async Task<FAT00800ResultDTO<FAT00800GetPeriodeDtInfoResultDTO>> FAT00800GetPeriodeDtInfoAsync(FAT00800GetPeriodeDtInfoParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            FAT00800ResultDTO<FAT00800GetPeriodeDtInfoResultDTO> loResult = new FAT00800ResultDTO<FAT00800GetPeriodeDtInfoResultDTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<FAT00800ResultDTO<FAT00800GetPeriodeDtInfoResultDTO>, FAT00800GetPeriodeDtInfoParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IFAT00800Entry.FAT00800GetPeriodeDtInfo),
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

        public IAsyncEnumerable<FAT00800GetCurrencyListResultDTO> GetCurrencyList()
        {
            throw new NotImplementedException();
        }

        public async Task<FAT00800ResultDTO<List<FAT00800GetCurrencyListResultDTO>>> GetCurrencyListAsync()
        {
            var loEx = new R_Exception();
            var loRtn = new FAT00800ResultDTO<List<FAT00800GetCurrencyListResultDTO>>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loRtn.Data = await R_HTTPClientWrapper.R_APIRequestStreamingObject<FAT00800GetCurrencyListResultDTO>(
                    _RequestServiceEndPoint,
                    nameof(IFAT00800Entry.GetCurrencyList),
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

        public IAsyncEnumerable<FAT00800GetDeptLookupListResultDTO> FAT00800GetDeptLookupList()
        {
            throw new NotImplementedException();
        }

        public async Task<FAT00800ResultDTO<List<FAT00800GetDeptLookupListResultDTO>>> FAT00800GetDeptLookupListAsync()
        {
            var loEx = new R_Exception();
            var loRtn = new FAT00800ResultDTO<List<FAT00800GetDeptLookupListResultDTO>>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loRtn.Data = await R_HTTPClientWrapper.R_APIRequestStreamingObject<FAT00800GetDeptLookupListResultDTO>(
                    _RequestServiceEndPoint,
                    nameof(IFAT00800Entry.FAT00800GetDeptLookupList),
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

        public async Task<FAT00800ResultDTO<FAT00800GetTransCodeInfoResultDTO>> FAT00800GetTransCodeInfoAsync(FAT00800GetTransCodeInfoParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            FAT00800ResultDTO<FAT00800GetTransCodeInfoResultDTO> loResult = new FAT00800ResultDTO<FAT00800GetTransCodeInfoResultDTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<FAT00800ResultDTO<FAT00800GetTransCodeInfoResultDTO>, FAT00800GetTransCodeInfoParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IFAT00800Entry.FAT00800GetTransCodeInfo),
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

        public async Task<FAT00800ResultDTO<FAT00800GetYearRangeResultDTO>> FAT00800GetYearRangeAsync(FAT00800GetYearRangeParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            FAT00800ResultDTO<FAT00800GetYearRangeResultDTO> loResult = new FAT00800ResultDTO<FAT00800GetYearRangeResultDTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<FAT00800ResultDTO<FAT00800GetYearRangeResultDTO>, FAT00800GetYearRangeParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IFAT00800Entry.FAT00800GetYearRange),
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

        public Task<FAT00800ResultDTO<FAT00800GetCompanyInfoResultDTO>> FAT00800GetCompanyInfo(FAT00800GetCompanyInfoParameterDTO poParameter)
        {
            throw new NotImplementedException();
        }

        public Task<FAT00800ResultDTO<FAT00800GetGetSystemParamResultDTO>> FAT00800GetGetSystemParam(FAT00800GetGetSystemParamParameterDTO poParameter)
        {
            throw new NotImplementedException();
        }

        public Task<FAT00800ResultDTO<FAT00800GetPeriodeDtInfoResultDTO>> FAT00800GetPeriodeDtInfo(FAT00800GetPeriodeDtInfoParameterDTO poParameter)
        {
            throw new NotImplementedException();
        }

        public Task<FAT00800ResultDTO<FAT00800GetTransCodeInfoResultDTO>> FAT00800GetTransCodeInfo(FAT00800GetTransCodeInfoParameterDTO poParameter)
        {
            throw new NotImplementedException();
        }

        public Task<FAT00800ResultDTO<FAT00800GetYearRangeResultDTO>> FAT00800GetYearRange(FAT00800GetYearRangeParameterDTO poParameter)
        {
            throw new NotImplementedException();
        }

        #endregion


    }
}

