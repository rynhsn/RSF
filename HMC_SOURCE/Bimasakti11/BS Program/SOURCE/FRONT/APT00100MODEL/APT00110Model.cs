using APT00100COMMON;
using APT00100COMMON.DTOs.APT00100;
using APT00100COMMON.DTOs.APT00110;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace APT00100MODEL
{
    public class APT00110Model : R_BusinessObjectServiceClientBase<APT00110ParameterDTO>, IAPT00110
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlAP";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/APT00110";
        private const string DEFAULT_MODULE = "AP";

        public APT00110Model(string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        public IAsyncEnumerable<GetCurrencyListDTO> GetCurrencyList()
        {
            throw new NotImplementedException();
        }
        public async Task<GetCurrencyListResultDTO> GetCurrencyListStreamAsync()
        {
            R_Exception loEx = new R_Exception();
            List<GetCurrencyListDTO> loResult = null;
            GetCurrencyListResultDTO loRtn = new GetCurrencyListResultDTO();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GetCurrencyListDTO>(
                    _RequestServiceEndPoint,
                    nameof(IAPT00110.GetCurrencyList),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loRtn.Data = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

        EndBlock:
            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        public GetCurrencyOrTaxRateResultDTO GetCurrencyOrTaxRate(GetCurrencyOrTaxRateParameterDTO poParam)
        {
            throw new NotImplementedException();
        }
        public async Task<GetCurrencyOrTaxRateResultDTO> GetCurrencyOrTaxRateAsync(GetCurrencyOrTaxRateParameterDTO poParam)
        {
            R_Exception loEx = new R_Exception();
            GetCurrencyOrTaxRateResultDTO loRtn = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<GetCurrencyOrTaxRateResultDTO, GetCurrencyOrTaxRateParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IAPT00110.GetCurrencyOrTaxRate),
                    poParam,
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

        EndBlock:
            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }


        public IAsyncEnumerable<GetPaymentTermListDTO> GetPaymentTermList()
        {
            throw new NotImplementedException();
        }
        public async Task<GetPaymentTermListResultDTO> GetPaymentTermListStreamAsync()
        {
            R_Exception loEx = new R_Exception();
            List<GetPaymentTermListDTO> loResult = null;
            GetPaymentTermListResultDTO loRtn = new GetPaymentTermListResultDTO();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GetPaymentTermListDTO>(
                    _RequestServiceEndPoint,
                    nameof(IAPT00110.GetPaymentTermList),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loRtn.Data = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

        EndBlock:
            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        public RedraftJournalResultDTO RedraftJournalProcess(RedraftJournalParameterDTO poParam)
        {
            throw new NotImplementedException();
        }

        public async Task<RedraftJournalResultDTO> RedraftJournalProcessAsync(RedraftJournalParameterDTO poParam)
        {
            R_Exception loEx = new R_Exception();
            RedraftJournalResultDTO loRtn = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<RedraftJournalResultDTO, RedraftJournalParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IAPT00110.RedraftJournalProcess),
                    poParam,
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

        EndBlock:
            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }

        public SubmitJournalResultDTO SubmitJournalProcess(SubmitJournalParameterDTO poParam)
        {
            throw new NotImplementedException();
        }

        public async Task<SubmitJournalResultDTO> SubmitJournalProcessAsync(SubmitJournalParameterDTO poParam)
        {
            R_Exception loEx = new R_Exception();
            SubmitJournalResultDTO loRtn = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<SubmitJournalResultDTO, SubmitJournalParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IAPT00110.SubmitJournalProcess),
                    poParam,
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

        EndBlock:
            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }
    }
}
