using PMT50600COMMON;
using PMT50600COMMON.DTOs.PMT50600;
using PMT50600COMMON.DTOs.PMT50610;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PMT50600MODEL
{
    public class PMT50610Model : R_BusinessObjectServiceClientBase<PMT50610ParameterDTO>, IPMT50610
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlPM";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/PMT50610";
        private const string DEFAULT_MODULE = "PM";

        public PMT50610Model(string pcHttpClientName = DEFAULT_HTTP_NAME,
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
                    nameof(IPMT50610.GetCurrencyList),
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
                    nameof(IPMT50610.GetCurrencyOrTaxRate),
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
                    nameof(IPMT50610.GetPaymentTermList),
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
                    nameof(IPMT50610.RedraftJournalProcess),
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
                    nameof(IPMT50610.SubmitJournalProcess),
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
