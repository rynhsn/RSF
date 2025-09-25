using CBT01200Common.DTOs;
using CBT01200Common;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CBT01200MODEL
{
    public class CBT01200InitModel : R_BusinessObjectServiceClientBase<CBT01200InitDTO>, ICBT01200Init
    {
        private const string DEFAULT_HTTP = "R_DefaultServiceUrlCB";
        private const string DEFAULT_ENDPOINT = "api/CBT01200Init";
        private const string DEFAULT_MODULE = "CB";

        public CBT01200InitModel
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

        public async Task<List<CBT01200GSCenterDTO>> GetCenterListAsync()
        {
            var loEx = new R_Exception();
            List<CBT01200GSCenterDTO> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<CBT01200GSCenterDTO>(
                    _RequestServiceEndPoint,
                    nameof(ICBT01200Init.GetCenterList),
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
        public async Task<List<CBT01200GSCurrencyDTO>> GetCurrencyListAsync()
        {
            var loEx = new R_Exception();
            List<CBT01200GSCurrencyDTO> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<CBT01200GSCurrencyDTO>(
                    _RequestServiceEndPoint,
                    nameof(ICBT01200Init.GetCurrencyList),
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
        public async Task<List<CBT01200GSGSBCodeDTO>> GetGSBCodeListAsync()
        {
            var loEx = new R_Exception();
            List<CBT01200GSGSBCodeDTO> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<CBT01200GSGSBCodeDTO>(
                    _RequestServiceEndPoint,
                    nameof(ICBT01200Init.GetGSBCodeList),
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
        public async Task<CBT01200GLSystemParamDTO> GetGLSystemParamAsync()
        {
            var loEx = new R_Exception();
            CBT01200GLSystemParamDTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<CBT01200RecordResult<CBT01200GLSystemParamDTO>>(
                    _RequestServiceEndPoint,
                    nameof(ICBT01200Init.GetGLSystemParam),
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
        public async Task<CBT01200CBSystemParamDTO> GetCBSystemParamAsync()
        {
            var loEx = new R_Exception();
            CBT01200CBSystemParamDTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<CBT01200RecordResult<CBT01200CBSystemParamDTO>>(
                    _RequestServiceEndPoint,
                    nameof(ICBT01200Init.GetCBSystemParam),
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
        public async Task<CBT01200GSCompanyInfoDTO> GetGSCompanyInfoAsync()
        {
            var loEx = new R_Exception();
            CBT01200GSCompanyInfoDTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<CBT01200RecordResult<CBT01200GSCompanyInfoDTO>>(
                    _RequestServiceEndPoint,
                    nameof(ICBT01200Init.GetGSCompanyInfo),
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
        public async Task<CBT01200GSPeriodYearRangeDTO> GetGSPeriodYearRangeAsync()
        {
            var loEx = new R_Exception();
            CBT01200GSPeriodYearRangeDTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<CBT01200RecordResult<CBT01200GSPeriodYearRangeDTO>>(
                    _RequestServiceEndPoint,
                    nameof(ICBT01200Init.GetGSPeriodYearRange),
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
        public async Task<CBT01200GLSystemEnableOptionInfoDTO> GetGSSystemEnableOptionInfoAsync()
        {
            var loEx = new R_Exception();
            CBT01200GLSystemEnableOptionInfoDTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<CBT01200RecordResult<CBT01200GLSystemEnableOptionInfoDTO>>(
                    _RequestServiceEndPoint,
                    nameof(ICBT01200Init.GetGSSystemEnableOptionInfo),
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
        public async Task<CBT01200GSTransInfoDTO> GetGSTransCodeInfoAsync()
        {
            var loEx = new R_Exception();
            CBT01200GSTransInfoDTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<CBT01200RecordResult<CBT01200GSTransInfoDTO>>(
                    _RequestServiceEndPoint,
                    nameof(ICBT01200Init.GetGSTransCodeInfo),
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
        public async Task<CBT01200InitDTO> GetTabJournalListInitVarAsync()
        {
            var loEx = new R_Exception();
            CBT01200InitDTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<CBT01200RecordResult<CBT01200InitDTO>>(
                    _RequestServiceEndPoint,
                    nameof(ICBT01200Init.GetTabJournalListInitVar),
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
        public async Task<CBT01200TodayDateDTO> GetTodayDateAsync()
        {
            var loEx = new R_Exception();
            CBT01200TodayDateDTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<CBT01200RecordResult<CBT01200TodayDateDTO>>(
                    _RequestServiceEndPoint,
                    nameof(ICBT01200Init.GetTodayDate),
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
        public async Task<CBT01210InitDTO> GetTabJournalEntryInitVarAsync()
        {
            var loEx = new R_Exception();
            CBT01210InitDTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<CBT01200RecordResult<CBT01210InitDTO>>(
                    _RequestServiceEndPoint,
                    nameof(ICBT01200Init.GetTabJournalEntryInitVar),
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
        public async Task<CBT01200GSPeriodDTInfoDTO> GetGSPeriodDTInfoAsync(CBT01200ParamGSPeriodDTInfoDTO poEntity)
        {
            var loEx = new R_Exception();
            CBT01200GSPeriodDTInfoDTO loResult = null;
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<CBT01200RecordResult<CBT01200GSPeriodDTInfoDTO>, CBT01200ParamGSPeriodDTInfoDTO>(
                _RequestServiceEndPoint,
                nameof(ICBT01200Init.GetGSPeriodDTInfo),
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

        #region Not Implemet
        public IAsyncEnumerable<CBT01200GSCenterDTO> GetCenterList()
        {
            throw new NotImplementedException();
        }
        public IAsyncEnumerable<CBT01200GSCurrencyDTO> GetCurrencyList()
        {
            throw new NotImplementedException();
        }
        public CBT01200RecordResult<CBT01200GLSystemParamDTO> GetGLSystemParam()
        {
            throw new NotImplementedException();
        }
        public IAsyncEnumerable<CBT01200GSGSBCodeDTO> GetGSBCodeList()
        {
            throw new NotImplementedException();
        }
        public CBT01200RecordResult<CBT01200GSCompanyInfoDTO> GetGSCompanyInfo()
        {
            throw new NotImplementedException();
        }
        public CBT01200RecordResult<CBT01200GSPeriodDTInfoDTO> GetGSPeriodDTInfo(CBT01200ParamGSPeriodDTInfoDTO poEntity)
        {
            throw new NotImplementedException();
        }
        public CBT01200RecordResult<CBT01200GSPeriodYearRangeDTO> GetGSPeriodYearRange()
        {
            throw new NotImplementedException();
        }
        public CBT01200RecordResult<CBT01200GLSystemEnableOptionInfoDTO> GetGSSystemEnableOptionInfo()
        {
            throw new NotImplementedException();
        }
        public CBT01200RecordResult<CBT01200GSTransInfoDTO> GetGSTransCodeInfo()
        {
            throw new NotImplementedException();
        }
        public CBT01200RecordResult<CBT01200InitDTO> GetTabJournalListInitVar()
        {
            throw new NotImplementedException();
        }
        public CBT01200RecordResult<CBT01200TodayDateDTO> GetTodayDate()
        {
            throw new NotImplementedException();
        }
        public CBT01200RecordResult<CBT01210InitDTO> GetTabJournalEntryInitVar()
        {
            throw new NotImplementedException();
        }

        public CBT01200RecordResult<CBT01200CBSystemParamDTO> GetCBSystemParam()
        {
            throw new NotImplementedException();
        }

      
        #endregion
    }
}

