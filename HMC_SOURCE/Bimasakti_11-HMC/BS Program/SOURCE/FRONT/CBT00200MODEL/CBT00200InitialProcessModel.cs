using CBT00200COMMON;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CBT00200MODEL
{
    public class CBT00200InitialProcessModel : R_BusinessObjectServiceClientBase<CBT00200DTO>, ICBT00200InitialProcess
    {
        private const string DEFAULT_HTTP = "R_DefaultServiceUrlCB";
        private const string DEFAULT_ENDPOINT = "api/CBT00200InitialProcess";
        private const string DEFAULT_MODULE = "CB";
        public CBT00200InitialProcessModel(
           string pcHttpClientName = DEFAULT_HTTP,
           string pcRequestServiceEndPoint = DEFAULT_ENDPOINT,
           bool plSendWithContext = true,
           bool plSendWithToken = true) :
           base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        public async Task<CBT00200CBSystemParamDTO> GetCBSystemParamAsync()
        {
            var loEx = new R_Exception();
            CBT00200CBSystemParamDTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<CBT00200SingleResult<CBT00200CBSystemParamDTO>>(
                    _RequestServiceEndPoint,
                    nameof(ICBT00200InitialProcess.GetCBSystemParam),
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
        public async Task<CBT00200GLSystemParamDTO> GetGLSystemParamAsync()
        {
            var loEx = new R_Exception();
            CBT00200GLSystemParamDTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<CBT00200SingleResult<CBT00200GLSystemParamDTO>>(
                    _RequestServiceEndPoint,
                    nameof(ICBT00200InitialProcess.GetGLSystemParam),
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
        public async Task<CBT00200GSCompanyInfoDTO> GetGSCompanyInfoAsync()
        {
            var loEx = new R_Exception();
            CBT00200GSCompanyInfoDTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<CBT00200SingleResult<CBT00200GSCompanyInfoDTO>>(
                    _RequestServiceEndPoint,
                    nameof(ICBT00200InitialProcess.GetGSCompanyInfo),
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
        public async Task<CBT00200GSPeriodYearRangeDTO> GetGSPeriodYearRangeAsync()
        {
            var loEx = new R_Exception();
            CBT00200GSPeriodYearRangeDTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<CBT00200SingleResult<CBT00200GSPeriodYearRangeDTO>>(
                    _RequestServiceEndPoint,
                    nameof(ICBT00200InitialProcess.GetGSPeriodYearRange),
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
        public async Task<CBT00200GSTransInfoDTO> GetGSTransCodeInfoAsync()
        {
            var loEx = new R_Exception();
            CBT00200GSTransInfoDTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<CBT00200SingleResult<CBT00200GSTransInfoDTO>>(
                    _RequestServiceEndPoint,
                    nameof(ICBT00200InitialProcess.GetGSTransCodeInfo),
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
        public async Task<CBT00200TodayDateDTO> GetTodayDateAsync()
        {
            var loEx = new R_Exception();
            CBT00200TodayDateDTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<CBT00200SingleResult<CBT00200TodayDateDTO>>(
                    _RequestServiceEndPoint,
                    nameof(ICBT00200InitialProcess.GetTodayDate),
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
        public async Task<CBT00200GSPeriodDTInfoDTO> GetGSPeriodDTInfoAsync(CBT00200ParamGSPeriodDTInfoDTO poEntity)
        {
            var loEx = new R_Exception();
            CBT00200GSPeriodDTInfoDTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<CBT00200SingleResult<CBT00200GSPeriodDTInfoDTO>, CBT00200ParamGSPeriodDTInfoDTO>(
                    _RequestServiceEndPoint,
                    nameof(ICBT00200InitialProcess.GetGSPeriodDTInfo),
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
        public async Task<CBT00200JournalListInitialProcessDTO> GetTabJournalListInitialProcessAsync()
        {
            var loEx = new R_Exception();
            CBT00200JournalListInitialProcessDTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<CBT00200SingleResult<CBT00200JournalListInitialProcessDTO>>(
                    _RequestServiceEndPoint,
                    nameof(ICBT00200InitialProcess.GetTabJournalListInitialProcess),
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
        public async Task<List<CBT00200GSCenterDTO>> GetCenterListAsync()
        {
            var loEx = new R_Exception();
            List<CBT00200GSCenterDTO> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<CBT00200GSCenterDTO>(
                    _RequestServiceEndPoint,
                    nameof(ICBT00200InitialProcess.GetCenterList),
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
        public async Task<List<CBT00200GSGSBCodeDTO>> GetGSBCodeListAsync()
        {
            var loEx = new R_Exception();
            List<CBT00200GSGSBCodeDTO> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<CBT00200GSGSBCodeDTO>(
                    _RequestServiceEndPoint,
                    nameof(ICBT00200InitialProcess.GetGSBCodeList),
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
        public async Task<CBT00210JournalEntryInitialProcessDTO> GetTabJournalEntryInitialProcessAsync()
        {
            var loEx = new R_Exception();
            CBT00210JournalEntryInitialProcessDTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<CBT00200SingleResult<CBT00210JournalEntryInitialProcessDTO>>(
                    _RequestServiceEndPoint,
                    nameof(ICBT00200InitialProcess.GetTabJournalEntryInitialProcess),
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
        public async Task<CBT00200LastCurrencyRateDTO> GetLastCurrencyRateAsync(CBT00200LastCurrencyRateDTO poEntity)
        {
            var loEx = new R_Exception();
            CBT00200LastCurrencyRateDTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<CBT00200SingleResult<CBT00200LastCurrencyRateDTO>, CBT00200LastCurrencyRateDTO>(
                    _RequestServiceEndPoint,
                    nameof(ICBT00200InitialProcess.GetLastCurrencyRate),
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

        #region Not Impkement
        public CBT00200SingleResult<CBT00200CBSystemParamDTO> GetCBSystemParam()
        {
            throw new NotImplementedException();
        }
        public IAsyncEnumerable<CBT00200GSCenterDTO> GetCenterList()
        {
            throw new NotImplementedException();
        }
        public CBT00200SingleResult<CBT00200GLSystemParamDTO> GetGLSystemParam()
        {
            throw new NotImplementedException();
        }
        public IAsyncEnumerable<CBT00200GSGSBCodeDTO> GetGSBCodeList()
        {
            throw new NotImplementedException();
        }
        public CBT00200SingleResult<CBT00200GSCompanyInfoDTO> GetGSCompanyInfo()
        {
            throw new NotImplementedException();
        }
        public CBT00200SingleResult<CBT00200GSPeriodDTInfoDTO> GetGSPeriodDTInfo(CBT00200ParamGSPeriodDTInfoDTO poEntity)
        {
            throw new NotImplementedException();
        }
        public CBT00200SingleResult<CBT00200GSPeriodYearRangeDTO> GetGSPeriodYearRange()
        {
            throw new NotImplementedException();
        }
        public CBT00200SingleResult<CBT00200GSTransInfoDTO> GetGSTransCodeInfo()
        {
            throw new NotImplementedException();
        }
        public CBT00200SingleResult<CBT00200JournalListInitialProcessDTO> GetTabJournalListInitialProcess()
        {
            throw new NotImplementedException();
        }
        public CBT00200SingleResult<CBT00200TodayDateDTO> GetTodayDate()
        {
            throw new NotImplementedException();
        }
        public CBT00200SingleResult<CBT00210JournalEntryInitialProcessDTO> GetTabJournalEntryInitialProcess()
        {
            throw new NotImplementedException();
        }
        public CBT00200SingleResult<CBT00200LastCurrencyRateDTO> GetLastCurrencyRate(CBT00200LastCurrencyRateDTO poEntity)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
