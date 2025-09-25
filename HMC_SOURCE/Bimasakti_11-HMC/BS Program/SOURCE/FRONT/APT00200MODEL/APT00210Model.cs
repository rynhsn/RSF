using APT00200COMMON;
using APT00200COMMON.DTOs.APT00200;
using APT00200COMMON.DTOs.APT00210;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace APT00200MODEL
{
    public class APT00210Model : R_BusinessObjectServiceClientBase<APT00210ParameterDTO>, IAPT00210
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlAP";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/APT00210";
        private const string DEFAULT_MODULE = "AP";

        public APT00210Model(string pcHttpClientName = DEFAULT_HTTP_NAME,
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
                    nameof(IAPT00210.GetCurrencyList),
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
                    nameof(IAPT00210.GetCurrencyOrTaxRate),
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
                    nameof(IAPT00210.GetPaymentTermList),
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
                    nameof(IAPT00210.RedraftJournalProcess),
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
                    nameof(IAPT00210.SubmitJournalProcess),
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
