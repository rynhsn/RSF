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
    /// Model class for FAT01100 Entry - Change Asset Data Transaction (CRUD + init/lookup)
    /// Handles communication with FAT01100EntryController
    /// </summary>
    public class FAT01100EntryModel : R_BusinessObjectServiceClientBase<FAT01100DTO>, IFAT01100Entry
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlFA";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/FAT01100Entry";
        private const string DEFAULT_MODULE = "FA";

        public FAT01100EntryModel()
            : base(DEFAULT_HTTP_NAME, DEFAULT_SERVICEPOINT_NAME, DEFAULT_MODULE, true, true)
        {
        }

        #region CRUD Methods

        public new async Task<R_ServiceGetRecordResultDTO<FAT01100DTO>> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<FAT01100DTO> poParameter)
        {
            var loEx = new R_Exception();
            R_ServiceGetRecordResultDTO<FAT01100DTO> loResult = new R_ServiceGetRecordResultDTO<FAT01100DTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<R_ServiceGetRecordResultDTO<FAT01100DTO>, R_ServiceGetRecordParameterDTO<FAT01100DTO>>(
                    _RequestServiceEndPoint,
                    nameof(IFAT01100Entry.R_ServiceGetRecord),
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

        public new async Task<R_ServiceSaveResultDTO<FAT01100DTO>> R_ServiceSave(R_ServiceSaveParameterDTO<FAT01100DTO> poParameter)
        {
            var loEx = new R_Exception();
            R_ServiceSaveResultDTO<FAT01100DTO> loResult = new R_ServiceSaveResultDTO<FAT01100DTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<R_ServiceSaveResultDTO<FAT01100DTO>, R_ServiceSaveParameterDTO<FAT01100DTO>>(
                    _RequestServiceEndPoint,
                    nameof(IFAT01100Entry.R_ServiceSave),
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

        public new async Task<R_ServiceDeleteResultDTO> R_ServiceDelete(R_ServiceDeleteParameterDTO<FAT01100DTO> poParameter)
        {
            var loEx = new R_Exception();
            R_ServiceDeleteResultDTO loResult = new R_ServiceDeleteResultDTO();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<R_ServiceDeleteResultDTO, R_ServiceDeleteParameterDTO<FAT01100DTO>>(
                    _RequestServiceEndPoint,
                    nameof(IFAT01100Entry.R_ServiceDelete),
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

        #region Init / Lookup Methods

        public async Task<FAT01100ResultDTO<FAT01100GetCompanyInfoResultDTO>> FAT01100GetCompanyInfo(FAT01100GetCompanyInfoParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            FAT01100ResultDTO<FAT01100GetCompanyInfoResultDTO> loRtn = new FAT01100ResultDTO<FAT01100GetCompanyInfoResultDTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<FAT01100ResultDTO<FAT01100GetCompanyInfoResultDTO>, FAT01100GetCompanyInfoParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IFAT01100Entry.FAT01100GetCompanyInfo),
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

        public async Task<FAT01100ResultDTO<FAT01100GetGetSystemParamResultDTO>> FAT01100GetGetSystemParam(FAT01100GetGetSystemParamParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            FAT01100ResultDTO<FAT01100GetGetSystemParamResultDTO> loRtn = new FAT01100ResultDTO<FAT01100GetGetSystemParamResultDTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<FAT01100ResultDTO<FAT01100GetGetSystemParamResultDTO>, FAT01100GetGetSystemParamParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IFAT01100Entry.FAT01100GetGetSystemParam),
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

        public async Task<FAT01100ResultDTO<FAT01100GetPeriodeDtInfoResultDTO>> FAT01100GetPeriodeDtInfo(FAT01100GetPeriodeDtInfoParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            FAT01100ResultDTO<FAT01100GetPeriodeDtInfoResultDTO> loRtn = new FAT01100ResultDTO<FAT01100GetPeriodeDtInfoResultDTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<FAT01100ResultDTO<FAT01100GetPeriodeDtInfoResultDTO>, FAT01100GetPeriodeDtInfoParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IFAT01100Entry.FAT01100GetPeriodeDtInfo),
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

        public async Task<FAT01100ResultDTO<List<FAT01100GetCurrencyListResultDTO>>> GetCurrencyList(FAT01100GetCurrencyListParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            FAT01100ResultDTO<List<FAT01100GetCurrencyListResultDTO>> loRtn = new FAT01100ResultDTO<List<FAT01100GetCurrencyListResultDTO>>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<FAT01100ResultDTO<List<FAT01100GetCurrencyListResultDTO>>, FAT01100GetCurrencyListParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IFAT01100Entry.GetCurrencyList),
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

        public async Task<FAT01100ResultDTO<List<FAT01100GetDeptLookupListResultDTO>>> FAT01100GetDeptLookupList(FAT01100GetDeptLookupListParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            FAT01100ResultDTO<List<FAT01100GetDeptLookupListResultDTO>> loRtn = new FAT01100ResultDTO<List<FAT01100GetDeptLookupListResultDTO>>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<FAT01100ResultDTO<List<FAT01100GetDeptLookupListResultDTO>>, FAT01100GetDeptLookupListParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IFAT01100Entry.FAT01100GetDeptLookupList),
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

        public async Task<FAT01100ResultDTO<FAT01100GetTransCodeInfoResultDTO>> FAT01100GetTransCodeInfo(FAT01100GetTransCodeInfoParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            FAT01100ResultDTO<FAT01100GetTransCodeInfoResultDTO> loRtn = new FAT01100ResultDTO<FAT01100GetTransCodeInfoResultDTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<FAT01100ResultDTO<FAT01100GetTransCodeInfoResultDTO>, FAT01100GetTransCodeInfoParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IFAT01100Entry.FAT01100GetTransCodeInfo),
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

        public async Task<FAT01100ResultDTO<FAT01100GetYearRangeResultDTO>> FAT01100GetYearRange(FAT01100GetYearRangeParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            FAT01100ResultDTO<FAT01100GetYearRangeResultDTO> loRtn = new FAT01100ResultDTO<FAT01100GetYearRangeResultDTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<FAT01100ResultDTO<FAT01100GetYearRangeResultDTO>, FAT01100GetYearRangeParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IFAT01100Entry.FAT01100GetYearRange),
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

        public async Task<FAT01100ResultDTO<FAT01100GetLastCurrencyRateResultDTO>> FAT01100GetLastCurrencyRate(FAT01100GetLastCurrencyRateParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            FAT01100ResultDTO<FAT01100GetLastCurrencyRateResultDTO> loRtn = new FAT01100ResultDTO<FAT01100GetLastCurrencyRateResultDTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<FAT01100ResultDTO<FAT01100GetLastCurrencyRateResultDTO>, FAT01100GetLastCurrencyRateParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IFAT01100Entry.FAT01100GetLastCurrencyRate),
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

        public async Task<FAT01100ResultDTO<object>> FAT01100UpdateTransHdStatus(FAT01100UpdateTransHdStatusParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            FAT01100ResultDTO<object> loRtn = new FAT01100ResultDTO<object>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<FAT01100ResultDTO<object>, FAT01100UpdateTransHdStatusParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IFAT01100Entry.FAT01100UpdateTransHdStatus),
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

        public async Task<FAT01100ResultDTO<object>> FAT01100SubmitTrans(FAT01100SubmitTransParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            FAT01100ResultDTO<object> loRtn = new FAT01100ResultDTO<object>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<FAT01100ResultDTO<object>, FAT01100SubmitTransParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IFAT01100Entry.FAT01100SubmitTrans),
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

        public async Task<FAT01100ResultDTO<FAT01100GetAssetResultDTO>> FAT01100GetAsset(FAT01100GetAssetParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            FAT01100ResultDTO<FAT01100GetAssetResultDTO> loRtn = new FAT01100ResultDTO<FAT01100GetAssetResultDTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<FAT01100ResultDTO<FAT01100GetAssetResultDTO>, FAT01100GetAssetParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IFAT01100Entry.FAT01100GetAsset),
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
