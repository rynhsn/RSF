using PMT04100COMMON;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PMT04100MODEL
{
    public class PMT04100InitialProcessModel : R_BusinessObjectServiceClientBase<PMT04100DTO>, IPMT04100InitialProcess
    {
        private const string DEFAULT_HTTP = "R_DefaultServiceUrlPM";
        private const string DEFAULT_ENDPOINT = "api/PMT04100InitialProcess";
        private const string DEFAULT_MODULE = "PM";
        public PMT04100InitialProcessModel(
           string pcHttpClientName = DEFAULT_HTTP,
           string pcRequestServiceEndPoint = DEFAULT_ENDPOINT,
           bool plSendWithContext = true,
           bool plSendWithToken = true) :
           base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        public async Task<PMT04100CBSystemParamDTO> GetCBSystemParamAsync()
        {
            var loEx = new R_Exception();
            PMT04100CBSystemParamDTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<PMT04100SingleResult<PMT04100CBSystemParamDTO>>(
                    _RequestServiceEndPoint,
                    nameof(IPMT04100InitialProcess.GetCBSystemParam),
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
        public async Task<PMT04100GLSystemParamDTO> GetGLSystemParamAsync()
        {
            var loEx = new R_Exception();
            PMT04100GLSystemParamDTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<PMT04100SingleResult<PMT04100GLSystemParamDTO>>(
                    _RequestServiceEndPoint,
                    nameof(IPMT04100InitialProcess.GetGLSystemParam),
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
        public async Task<PMT04100GSCompanyInfoDTO> GetGSCompanyInfoAsync()
        {
            var loEx = new R_Exception();
            PMT04100GSCompanyInfoDTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<PMT04100SingleResult<PMT04100GSCompanyInfoDTO>>(
                    _RequestServiceEndPoint,
                    nameof(IPMT04100InitialProcess.GetGSCompanyInfo),
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
        public async Task<PMT04100GSPeriodYearRangeDTO> GetGSPeriodYearRangeAsync()
        {
            var loEx = new R_Exception();
            PMT04100GSPeriodYearRangeDTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<PMT04100SingleResult<PMT04100GSPeriodYearRangeDTO>>(
                    _RequestServiceEndPoint,
                    nameof(IPMT04100InitialProcess.GetGSPeriodYearRange),
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
        public async Task<PMT04100GSTransInfoDTO> GetGSTransCodeInfoAsync()
        {
            var loEx = new R_Exception();
            PMT04100GSTransInfoDTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<PMT04100SingleResult<PMT04100GSTransInfoDTO>>(
                    _RequestServiceEndPoint,
                    nameof(IPMT04100InitialProcess.GetGSTransCodeInfo),
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
        public async Task<PMT04100TodayDateDTO> GetTodayDateAsync()
        {
            var loEx = new R_Exception();
            PMT04100TodayDateDTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<PMT04100SingleResult<PMT04100TodayDateDTO>>(
                    _RequestServiceEndPoint,
                    nameof(IPMT04100InitialProcess.GetTodayDate),
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
        public async Task<PMT04100GSPeriodDTInfoDTO> GetGSPeriodDTInfoAsync(PMT04100ParamGSPeriodDTInfoDTO poEntity)
        {
            var loEx = new R_Exception();
            PMT04100GSPeriodDTInfoDTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<PMT04100SingleResult<PMT04100GSPeriodDTInfoDTO>, PMT04100ParamGSPeriodDTInfoDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT04100InitialProcess.GetGSPeriodDTInfo),
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
        public async Task<PMT04100JournalListInitialProcessDTO> GetTabJournalListInitialProcessAsync()
        {
            var loEx = new R_Exception();
            PMT04100JournalListInitialProcessDTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<PMT04100SingleResult<PMT04100JournalListInitialProcessDTO>>(
                    _RequestServiceEndPoint,
                    nameof(IPMT04100InitialProcess.GetTabJournalListInitialProcess),
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
        public async Task<List<PMT04100GSGSBCodeDTO>> GetGSBCodeListAsync()
        {
            var loEx = new R_Exception();
            List<PMT04100GSGSBCodeDTO> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMT04100GSGSBCodeDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT04100InitialProcess.GetGSBCodeList),
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
        public async Task<List<PMT04100PropertyDTO>> GetPropertyListAsync()
        {
            var loEx = new R_Exception();
            List<PMT04100PropertyDTO> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMT04100PropertyDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT04100InitialProcess.GetPropertyList),
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
        public async Task<PMT04110JournalEntryInitialProcessDTO> GetTabJournalEntryInitialProcessAsync(PMTInitialParamDTO poEntity)
        {
            var loEx = new R_Exception();
            PMT04110JournalEntryInitialProcessDTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<PMT04100SingleResult<PMT04110JournalEntryInitialProcessDTO>, PMTInitialParamDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT04100InitialProcess.GetTabJournalEntryInitialProcess),
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
        public async Task<PMT04100LastCurrencyRateDTO> GetLastCurrencyRateAsync(PMT04100LastCurrencyRateDTO poEntity)
        {
            var loEx = new R_Exception();
            PMT04100LastCurrencyRateDTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<PMT04100SingleResult<PMT04100LastCurrencyRateDTO>, PMT04100LastCurrencyRateDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT04100InitialProcess.GetLastCurrencyRate),
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
        public async Task<PMT04100PMSystemParamDTO> GetPMSystemParamAsync(PMTInitialParamDTO poEntity)
        {
            var loEx = new R_Exception();
            PMT04100PMSystemParamDTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<PMT04100SingleResult<PMT04100PMSystemParamDTO>, PMTInitialParamDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT04100InitialProcess.GetPMSystemParam),
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
        public PMT04100SingleResult<PMT04100CBSystemParamDTO> GetCBSystemParam()
        {
            throw new NotImplementedException();
        }
        public PMT04100SingleResult<PMT04100GLSystemParamDTO> GetGLSystemParam()
        {
            throw new NotImplementedException();
        }
        public IAsyncEnumerable<PMT04100GSGSBCodeDTO> GetGSBCodeList()
        {
            throw new NotImplementedException();
        }
        public PMT04100SingleResult<PMT04100GSCompanyInfoDTO> GetGSCompanyInfo()
        {
            throw new NotImplementedException();
        }
        public PMT04100SingleResult<PMT04100GSPeriodDTInfoDTO> GetGSPeriodDTInfo(PMT04100ParamGSPeriodDTInfoDTO poEntity)
        {
            throw new NotImplementedException();
        }
        public PMT04100SingleResult<PMT04100GSPeriodYearRangeDTO> GetGSPeriodYearRange()
        {
            throw new NotImplementedException();
        }
        public PMT04100SingleResult<PMT04100GSTransInfoDTO> GetGSTransCodeInfo()
        {
            throw new NotImplementedException();
        }
        public PMT04100SingleResult<PMT04100JournalListInitialProcessDTO> GetTabJournalListInitialProcess()
        {
            throw new NotImplementedException();
        }
        public PMT04100SingleResult<PMT04100TodayDateDTO> GetTodayDate()
        {
            throw new NotImplementedException();
        }
        public PMT04100SingleResult<PMT04110JournalEntryInitialProcessDTO> GetTabJournalEntryInitialProcess(PMTInitialParamDTO poEntity)
        {
            throw new NotImplementedException();
        }
        public PMT04100SingleResult<PMT04100LastCurrencyRateDTO> GetLastCurrencyRate(PMT04100LastCurrencyRateDTO poEntity)
        {
            throw new NotImplementedException();
        }
        public IAsyncEnumerable<PMT04100PropertyDTO> GetPropertyList()
        {
            throw new NotImplementedException();
        }
        public PMT04100SingleResult<PMT04100PMSystemParamDTO> GetPMSystemParam(PMTInitialParamDTO poEntity)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
