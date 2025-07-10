using GLT00100COMMON;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GLT00100MODEL
{
    public class GLT00100UniversalModel : R_BusinessObjectServiceClientBase<GLT00100UniversalDTO>, IGLT00100Universal
    {
        private const string DEFAULT_HTTP = "R_DefaultServiceUrlGL";
        private const string DEFAULT_ENDPOINT = "api/GLT00100Universal";
        private const string DEFAULT_MODULE = "GL";

        public GLT00100UniversalModel(
            string pcHttpClientName = DEFAULT_HTTP,
            string pcRequestServiceEndPoint = DEFAULT_ENDPOINT,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        public async Task<List<GLT00100GSCenterDTO>> GetCenterListAsync()
        {
            var loEx = new R_Exception();
            List<GLT00100GSCenterDTO> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GLT00100GSCenterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGLT00100Universal.GetCenterList),
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
        public async Task<List<GLT00100GSCurrencyDTO>> GetCurrencyListAsync()
        {
            var loEx = new R_Exception();
            List<GLT00100GSCurrencyDTO> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GLT00100GSCurrencyDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGLT00100Universal.GetCurrencyList),
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
        public async Task<List<GLT00100GSGSBCodeDTO>> GetGSBCodeListAsync()
        {
            var loEx = new R_Exception();
            List<GLT00100GSGSBCodeDTO> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GLT00100GSGSBCodeDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGLT00100Universal.GetGSBCodeList),
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
        public async Task<GLT00100GLSystemParamDTO> GetGLSystemParamAsync()
        {
            var loEx = new R_Exception();
            GLT00100GLSystemParamDTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<GLT00100RecordResult<GLT00100GLSystemParamDTO>>(
                    _RequestServiceEndPoint,
                    nameof(IGLT00100Universal.GetGLSystemParam),
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
        public async Task<GLT00100GSCompanyInfoDTO> GetGSCompanyInfoAsync()
        {
            var loEx = new R_Exception();
            GLT00100GSCompanyInfoDTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<GLT00100RecordResult<GLT00100GSCompanyInfoDTO>>(
                    _RequestServiceEndPoint,
                    nameof(IGLT00100Universal.GetGSCompanyInfo),
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
        public async Task<GLT00100GSPeriodYearRangeDTO> GetGSPeriodYearRangeAsync()
        {
            var loEx = new R_Exception();
            GLT00100GSPeriodYearRangeDTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<GLT00100RecordResult<GLT00100GSPeriodYearRangeDTO>>(
                    _RequestServiceEndPoint,
                    nameof(IGLT00100Universal.GetGSPeriodYearRange),
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
        public async Task<GLT00100GLSystemEnableOptionInfoDTO> GetGSSystemEnableOptionInfoAsync()
        {
            var loEx = new R_Exception();
            GLT00100GLSystemEnableOptionInfoDTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<GLT00100RecordResult<GLT00100GLSystemEnableOptionInfoDTO>>(
                    _RequestServiceEndPoint,
                    nameof(IGLT00100Universal.GetGSSystemEnableOptionInfo),
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
        public async Task<GLT00100GSTransInfoDTO> GetGSTransCodeInfoAsync()
        {
            var loEx = new R_Exception();
            GLT00100GSTransInfoDTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<GLT00100RecordResult<GLT00100GSTransInfoDTO>>(
                    _RequestServiceEndPoint,
                    nameof(IGLT00100Universal.GetGSTransCodeInfo),
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
        public async Task<GLT00100UniversalDTO> GetTabJournalListUniversalVarAsync()
        {
            var loEx = new R_Exception();
            GLT00100UniversalDTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<GLT00100RecordResult<GLT00100UniversalDTO>>(
                    _RequestServiceEndPoint,
                    nameof(IGLT00100Universal.GetTabJournalListUniversalVar),
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
        public async Task<GLT00100TodayDateDTO> GetTodayDateAsync()
        {
            var loEx = new R_Exception();
            GLT00100TodayDateDTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<GLT00100RecordResult<GLT00100TodayDateDTO>>(
                    _RequestServiceEndPoint,
                    nameof(IGLT00100Universal.GetTodayDate),
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
        public async Task<GLT00110UniversalDTO> GetTabJournalEntryUniversalVarAsync()
        {
            var loEx = new R_Exception();
            GLT00110UniversalDTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<GLT00100RecordResult<GLT00110UniversalDTO>>(
                    _RequestServiceEndPoint,
                    nameof(IGLT00100Universal.GetTabJournalEntryUniversalVar),
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
        public async Task<GLT00100GSPeriodDTInfoDTO> GetGSPeriodDTInfoAsync(GLT00100ParamGSPeriodDTInfoDTO poEntity)
        {
            var loEx = new R_Exception();
            GLT00100GSPeriodDTInfoDTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<GLT00100RecordResult<GLT00100GSPeriodDTInfoDTO>, GLT00100ParamGSPeriodDTInfoDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGLT00100Universal.GetGSPeriodDTInfo),
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
        public IAsyncEnumerable<GLT00100GSCenterDTO> GetCenterList()
        {
            throw new NotImplementedException();
        }
        public IAsyncEnumerable<GLT00100GSCurrencyDTO> GetCurrencyList()
        {
            throw new NotImplementedException();
        }
        public GLT00100RecordResult<GLT00100GLSystemParamDTO> GetGLSystemParam()
        {
            throw new NotImplementedException();
        }
        public IAsyncEnumerable<GLT00100GSGSBCodeDTO> GetGSBCodeList()
        {
            throw new NotImplementedException();
        }
        public GLT00100RecordResult<GLT00100GSCompanyInfoDTO> GetGSCompanyInfo()
        {
            throw new NotImplementedException();
        }
        public GLT00100RecordResult<GLT00100GSPeriodDTInfoDTO> GetGSPeriodDTInfo(GLT00100ParamGSPeriodDTInfoDTO poEntity)
        {
            throw new NotImplementedException();
        }
        public GLT00100RecordResult<GLT00100GSPeriodYearRangeDTO> GetGSPeriodYearRange()
        {
            throw new NotImplementedException();
        }
        public GLT00100RecordResult<GLT00100GLSystemEnableOptionInfoDTO> GetGSSystemEnableOptionInfo()
        {
            throw new NotImplementedException();
        }
        public GLT00100RecordResult<GLT00100GSTransInfoDTO> GetGSTransCodeInfo()
        {
            throw new NotImplementedException();
        }
        public GLT00100RecordResult<GLT00100UniversalDTO> GetTabJournalListUniversalVar()
        {
            throw new NotImplementedException();
        }

        public GLT00100RecordResult<GLT00100TodayDateDTO> GetTodayDate()
        {
            throw new NotImplementedException();
        }

        public GLT00100RecordResult<GLT00110UniversalDTO> GetTabJournalEntryUniversalVar()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
