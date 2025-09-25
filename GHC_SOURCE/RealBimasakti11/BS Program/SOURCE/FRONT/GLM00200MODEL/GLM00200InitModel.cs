using GLM00200COMMON;
using R_APIClient;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Threading.Tasks;

namespace GLM00200MODEL
{
    public class GLM00200InitModel : R_BusinessObjectServiceClientBase<JournalParamDTO>, IGLM00200Init
    {
        private const string DEFAULT_CHECKPOINT_NAME = "api/GLM00200General";
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlGL";
        private const string DEFAULT_MODULE = "GL";

        public GLM00200InitModel(
            string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_CHECKPOINT_NAME,
            bool plSendWithContext = true,
            bool plSendWithToken = true) : base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        //real function

        public async Task<AllInitRecordDTO> GetAllInitRecordAsync()
        {
            var loEx = new R_Exception();
            AllInitRecordDTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<RecordResultDTO<AllInitRecordDTO>>(
                    _RequestServiceEndPoint,
                    nameof(IGLM00200Init.GetAllInitRecord),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loResult = loTempResult.data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public async Task<List<CurrencyDTO>> GetListCurrencyAsync()
        {
            var loEx = new R_Exception();
            List<CurrencyDTO> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<CurrencyDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGLM00200Init.GetListCurrency),
                    DEFAULT_MODULE, _SendWithContext,
                    _SendWithToken);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public async Task<List<StatusDTO>> GetListStatusAsync()
        {
            var loEx = new R_Exception();
            List<StatusDTO> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<StatusDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGLM00200Init.GetListStatus),
                    DEFAULT_MODULE, _SendWithContext,
                    _SendWithToken);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public async Task<CurrencyRateResultDTO> GetCurrencyRateRecordAsync(CurrencyRateParamDTO poParam)
        {
            var loEx = new R_Exception();
            CurrencyRateResultDTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<RecordResultDTO<CurrencyRateResultDTO>>(
                    _RequestServiceEndPoint,
                    nameof(IGLM00200Init.GetCurrencyRateRecord),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loResult = loTempResult.data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }


        //implement only

        public RecordResultDTO<AllInitRecordDTO> GetAllInitRecord()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<CurrencyDTO> GetListCurrency()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<StatusDTO> GetListStatus()
        {
            throw new NotImplementedException();
        }

        public RecordResultDTO<CurrencyRateResultDTO> GetCurrencyRateRecord(CurrencyRateParamDTO poParam)
        {
            throw new NotImplementedException();
        }


    }
}
