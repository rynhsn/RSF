using PMT04200Common.DTOs;
using PMT04200Common;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PMT04200MODEL
{
    public class PMT04200InitModel : R_BusinessObjectServiceClientBase<PMT04200InitDTO>, IPMT04200Init
    {
        private const string DEFAULT_HTTP = "R_DefaultServiceUrlPM";
        private const string DEFAULT_ENDPOINT = "api/PMT04200Init";
        private const string DEFAULT_MODULE = "PM";

        public PMT04200InitModel
            (
                string pcHttpClientName = DEFAULT_HTTP,
                string pcRequestServiceEndPoint = DEFAULT_ENDPOINT,
                bool plSendWithContext = true,
                bool plSendWithToken = true
            )
            : base
            (
              pcHttpClientName,
              pcRequestServiceEndPoint,
              DEFAULT_MODULE,
              plSendWithContext,
              plSendWithToken
            )
        { }

        public async Task<PropertyListDataDTO> GetProperyListAsync()
        {
            var loEx = new R_Exception();
            PropertyListDataDTO loResult = new PropertyListDataDTO();
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                var loTempResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PropertyListDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT04200Init.GetPropertyList),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);
                loResult.Data = loTempResult;

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        public async Task<List<PMT04200GSCurrencyDTO>> GetCurrencyListAsync()
        {
            var loEx = new R_Exception();
            List<PMT04200GSCurrencyDTO> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMT04200GSCurrencyDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT04200Init.GetCurrencyList),
                    DEFAULT_MODULE,
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
        public async Task<List<PMT04200GSGSBCodeDTO>> GetGSBCodeListAsync()
        {
            var loEx = new R_Exception();
            List<PMT04200GSGSBCodeDTO> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMT04200GSGSBCodeDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT04200Init.GetGSBCodeList),
                    DEFAULT_MODULE,
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
        public async Task<PMT04200GLSystemParamDTO> GetGLSystemParamAsync()
        {
            var loEx = new R_Exception();
            PMT04200GLSystemParamDTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<PMT04200RecordResult<PMT04200GLSystemParamDTO>>(
                    _RequestServiceEndPoint,
                    nameof(IPMT04200Init.GetGLSystemParam),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loResult = loTempResult.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        public async Task<PMT04200CBSystemParamDTO> GetCBSystemParamAsync()
        {
            var loEx = new R_Exception();
            PMT04200CBSystemParamDTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<PMT04200RecordResult<PMT04200CBSystemParamDTO>>(
                    _RequestServiceEndPoint,
                    nameof(IPMT04200Init.GetCBSystemParam),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loResult = loTempResult.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        public async Task<PMT04200PMSystemParamDTO> GetPMSystemParamAsync()
        {
            var loEx = new R_Exception();
            PMT04200PMSystemParamDTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<PMT04200RecordResult<PMT04200PMSystemParamDTO>>(
                    _RequestServiceEndPoint,
                    nameof(IPMT04200Init.GetPMSystemParam),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loResult = loTempResult.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        public async Task<PMT04200GSCompanyInfoDTO> GetGSCompanyInfoAsync()
        {
            var loEx = new R_Exception();
            PMT04200GSCompanyInfoDTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<PMT04200RecordResult<PMT04200GSCompanyInfoDTO>>(
                    _RequestServiceEndPoint,
                    nameof(IPMT04200Init.GetGSCompanyInfo),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loResult = loTempResult.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        public async Task<PMT04200GSPeriodYearRangeDTO> GetGSPeriodYearRangeAsync()
        {
            var loEx = new R_Exception();
            PMT04200GSPeriodYearRangeDTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<PMT04200RecordResult<PMT04200GSPeriodYearRangeDTO>>(
                    _RequestServiceEndPoint,
                    nameof(IPMT04200Init.GetGSPeriodYearRange),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loResult = loTempResult.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        public async Task<PMT04200GSTransInfoDTO> GetGSTransCodeInfoAsync()
        {
            var loEx = new R_Exception();
            PMT04200GSTransInfoDTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<PMT04200RecordResult<PMT04200GSTransInfoDTO>>(
                    _RequestServiceEndPoint,
                    nameof(IPMT04200Init.GetGSTransCodeInfo),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loResult = loTempResult.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        public async Task<PMT04200InitDTO> GetTabJournalListInitVarAsync()
        {
            var loEx = new R_Exception();
            PMT04200InitDTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<PMT04200RecordResult<PMT04200InitDTO>>(
                    _RequestServiceEndPoint,
                    nameof(IPMT04200Init.GetTabTransactionListInitVar),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loResult = loTempResult.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        public async Task<PMT04200TodayDateDTO> GetTodayDateAsync()
        {
            var loEx = new R_Exception();
            PMT04200TodayDateDTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<PMT04200RecordResult<PMT04200TodayDateDTO>>(
                    _RequestServiceEndPoint,
                    nameof(IPMT04200Init.GetTodayDate),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loResult = loTempResult.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        public async Task<PMT04200GSPeriodDTInfoDTO> GetGSPeriodDTInfoAsync(PMT04200ParamGSPeriodDTInfoDTO poEntity)
        {
            var loEx = new R_Exception();
            PMT04200GSPeriodDTInfoDTO loResult = null;
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<PMT04200RecordResult<PMT04200GSPeriodDTInfoDTO>, PMT04200ParamGSPeriodDTInfoDTO>(
                _RequestServiceEndPoint,
                nameof(IPMT04200Init.GetGSPeriodDTInfo),
                poEntity,
                DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loResult = loTempResult.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        
        public async Task<PMT04200LastCurrencyRateDTO> GetLastCurrencyRateAsync(PMT04200LastCurrencyRateDTO poEntity)
        {
            var loEx = new R_Exception();
            PMT04200LastCurrencyRateDTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<PMT04200RecordResult<PMT04200LastCurrencyRateDTO>, PMT04200LastCurrencyRateDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT04200Init.GetLastCurrencyRate),
                    poEntity,
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loResult = loTempResult.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        #region Not Implemented

        public IAsyncEnumerable<PropertyListDTO> GetPropertyList()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<PMT04200GSCurrencyDTO> GetCurrencyList()
        {
            throw new NotImplementedException();
        }
        public PMT04200RecordResult<PMT04200GLSystemParamDTO> GetGLSystemParam()
        {
            throw new NotImplementedException();
        }
        public IAsyncEnumerable<PMT04200GSGSBCodeDTO> GetGSBCodeList()
        {
            throw new NotImplementedException();
        }

        public PMT04200RecordResult<PMT04200LastCurrencyRateDTO> GetLastCurrencyRate(PMT04200LastCurrencyRateDTO poEntity)
        {
            throw new NotImplementedException();
        }

        public PMT04200RecordResult<PMT04200InitDTO> GetTabTransactionListInitVar()
        {
            throw new NotImplementedException();
        }

        public PMT04200RecordResult<PMT04200JournalListInitialProcessDTO> GetTabJournalListInitialProcess()
        {
            throw new NotImplementedException();
        }

        public PMT04200RecordResult<PMT04210JournalEntryInitialProcessDTO> GetTabJournalEntryInitialProcess()
        {
            throw new NotImplementedException();
        }

        public PMT04200RecordResult<PMT04200GSCompanyInfoDTO> GetGSCompanyInfo()
        {
            throw new NotImplementedException();
        }

        public PMT04200RecordResult<PMT04200PMSystemParamDTO> GetPMSystemParam()
        {
            throw new NotImplementedException();
        }

        public PMT04200RecordResult<PMT04200GSPeriodDTInfoDTO> GetGSPeriodDTInfo(PMT04200ParamGSPeriodDTInfoDTO poEntity)
        {
            throw new NotImplementedException();
        }
        public PMT04200RecordResult<PMT04200GSPeriodYearRangeDTO> GetGSPeriodYearRange()
        {
            throw new NotImplementedException();
        }
        public PMT04200RecordResult<PMT04200GSTransInfoDTO> GetGSTransCodeInfo()
        {
            throw new NotImplementedException();
        }
        public PMT04200RecordResult<PMT04200InitDTO> GetTabJournalListInitVar()
        {
            throw new NotImplementedException();
        }
        public PMT04200RecordResult<PMT04200TodayDateDTO> GetTodayDate()
        {
            throw new NotImplementedException();
        }
        public PMT04200RecordResult<PMT04200CBSystemParamDTO> GetCBSystemParam()
        {
            throw new NotImplementedException();
        }

      
        #endregion
    }
}

