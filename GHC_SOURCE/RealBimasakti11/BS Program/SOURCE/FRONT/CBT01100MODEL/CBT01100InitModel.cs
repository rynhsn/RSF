using CBT01100COMMON;
using CBT01100COMMON;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CBT01100MODEL
{
    public class CBT01100InitModel : R_BusinessObjectServiceClientBase<CBT01100InitDTO>, ICBT01100Init
    {
        private const string DEFAULT_HTTP = "R_DefaultServiceUrlCB";
        private const string DEFAULT_ENDPOINT = "api/CBT01100Init";
        private const string DEFAULT_MODULE = "CB";

        public CBT01100InitModel
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

        public async Task<List<CBT01100GSCenterDTO>> GetCenterListAsync()
        {
            var loEx = new R_Exception();
            List<CBT01100GSCenterDTO> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<CBT01100GSCenterDTO>(
                    _RequestServiceEndPoint,
                    nameof(ICBT01100Init.GetCenterList),
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
        public async Task<List<CBT01100GSCurrencyDTO>> GetCurrencyListAsync()
        {
            var loEx = new R_Exception();
            List<CBT01100GSCurrencyDTO> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<CBT01100GSCurrencyDTO>(
                    _RequestServiceEndPoint,
                    nameof(ICBT01100Init.GetCurrencyList),
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
        public async Task<List<CBT01100GSGSBCodeDTO>> GetGSBCodeListAsync()
        {
            var loEx = new R_Exception();
            List<CBT01100GSGSBCodeDTO> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<CBT01100GSGSBCodeDTO>(
                    _RequestServiceEndPoint,
                    nameof(ICBT01100Init.GetGSBCodeList),
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
        public async Task<CBT01100GLSystemParamDTO> GetGLSystemParamAsync()
        {
            var loEx = new R_Exception();
            CBT01100GLSystemParamDTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<CBT01100RecordResult<CBT01100GLSystemParamDTO>>(
                    _RequestServiceEndPoint,
                    nameof(ICBT01100Init.GetGLSystemParam),
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
        public async Task<CBT01100CBSystemParamDTO> GetCBSystemParamAsync()
        {
            var loEx = new R_Exception();
            CBT01100CBSystemParamDTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<CBT01100RecordResult<CBT01100CBSystemParamDTO>>(
                    _RequestServiceEndPoint,
                    nameof(ICBT01100Init.GetCBSystemParam),
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
        public async Task<CBT01100GSCompanyInfoDTO> GetGSCompanyInfoAsync()
        {
            var loEx = new R_Exception();
            CBT01100GSCompanyInfoDTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<CBT01100RecordResult<CBT01100GSCompanyInfoDTO>>(
                    _RequestServiceEndPoint,
                    nameof(ICBT01100Init.GetGSCompanyInfo),
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
        public async Task<CBT01100GSPeriodYearRangeDTO> GetGSPeriodYearRangeAsync()
        {
            var loEx = new R_Exception();
            CBT01100GSPeriodYearRangeDTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<CBT01100RecordResult<CBT01100GSPeriodYearRangeDTO>>(
                    _RequestServiceEndPoint,
                    nameof(ICBT01100Init.GetGSPeriodYearRange),
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
        public async Task<CBT01100GLSystemEnableOptionInfoDTO> GetGSSystemEnableOptionInfoAsync()
        {
            var loEx = new R_Exception();
            CBT01100GLSystemEnableOptionInfoDTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<CBT01100RecordResult<CBT01100GLSystemEnableOptionInfoDTO>>(
                    _RequestServiceEndPoint,
                    nameof(ICBT01100Init.GetGSSystemEnableOptionInfo),
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
        public async Task<CBT01100GSTransInfoDTO> GetGSTransCodeInfoAsync()
        {
            var loEx = new R_Exception();
            CBT01100GSTransInfoDTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<CBT01100RecordResult<CBT01100GSTransInfoDTO>>(
                    _RequestServiceEndPoint,
                    nameof(ICBT01100Init.GetGSTransCodeInfo),
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
        public async Task<CBT01100InitDTO> GetTabJournalListInitVarAsync()
        {
            var loEx = new R_Exception();
            CBT01100InitDTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<CBT01100RecordResult<CBT01100InitDTO>>(
                    _RequestServiceEndPoint,
                    nameof(ICBT01100Init.GetTabJournalListInitVar),
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
        public async Task<CBT01100TodayDateDTO> GetTodayDateAsync()
        {
            var loEx = new R_Exception();
            CBT01100TodayDateDTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<CBT01100RecordResult<CBT01100TodayDateDTO>>(
                    _RequestServiceEndPoint,
                    nameof(ICBT01100Init.GetTodayDate),
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
        public async Task<CBT01110InitDTO> GetTabJournalEntryInitVarAsync()
        {
            var loEx = new R_Exception();
            CBT01110InitDTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<CBT01100RecordResult<CBT01110InitDTO>>(
                    _RequestServiceEndPoint,
                    nameof(ICBT01100Init.GetTabJournalEntryInitVar),
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
        public async Task<CBT01100GSPeriodDTInfoDTO> GetGSPeriodDTInfoAsync(CBT01100ParamGSPeriodDTInfoDTO poEntity)
        {
            var loEx = new R_Exception();
            CBT01100GSPeriodDTInfoDTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<CBT01100RecordResult<CBT01100GSPeriodDTInfoDTO>, CBT01100ParamGSPeriodDTInfoDTO>(
                    _RequestServiceEndPoint,
                    nameof(ICBT01100Init.GetGSPeriodDTInfo),
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
        public IAsyncEnumerable<CBT01100GSCenterDTO> GetCenterList()
        {
            throw new NotImplementedException();
        }
        public IAsyncEnumerable<CBT01100GSCurrencyDTO> GetCurrencyList()
        {
            throw new NotImplementedException();
        }
        public CBT01100RecordResult<CBT01100GLSystemParamDTO> GetGLSystemParam()
        {
            throw new NotImplementedException();
        }
        public IAsyncEnumerable<CBT01100GSGSBCodeDTO> GetGSBCodeList()
        {
            throw new NotImplementedException();
        }
        public CBT01100RecordResult<CBT01100GSCompanyInfoDTO> GetGSCompanyInfo()
        {
            throw new NotImplementedException();
        }
        public CBT01100RecordResult<CBT01100GSPeriodDTInfoDTO> GetGSPeriodDTInfo(CBT01100ParamGSPeriodDTInfoDTO poEntity)
        {
            throw new NotImplementedException();
        }
        public CBT01100RecordResult<CBT01100GSPeriodYearRangeDTO> GetGSPeriodYearRange()
        {
            throw new NotImplementedException();
        }
        public CBT01100RecordResult<CBT01100GLSystemEnableOptionInfoDTO> GetGSSystemEnableOptionInfo()
        {
            throw new NotImplementedException();
        }
        public CBT01100RecordResult<CBT01100GSTransInfoDTO> GetGSTransCodeInfo()
        {
            throw new NotImplementedException();
        }
        public CBT01100RecordResult<CBT01100InitDTO> GetTabJournalListInitVar()
        {
            throw new NotImplementedException();
        }
        public CBT01100RecordResult<CBT01100TodayDateDTO> GetTodayDate()
        {
            throw new NotImplementedException();
        }
        public CBT01100RecordResult<CBT01110InitDTO> GetTabJournalEntryInitVar()
        {
            throw new NotImplementedException();
        }

        public CBT01100RecordResult<CBT01100CBSystemParamDTO> GetCBSystemParam()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
