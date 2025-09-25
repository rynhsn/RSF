using LMM01500COMMON;
using R_APIClient;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LMM01500MODEL
{
    public class LMM01500Model : R_BusinessObjectServiceClientBase<LMM01500DTO>, ILMM01500
    {
        private const string DEFAULT_HTTP = "R_DefaultServiceUrlLM";
        private const string DEFAULT_ENDPOINT = "api/LMM01500";
        private const string DEFAULT_MODULE = "LM";

        public LMM01500Model(
            string pcHttpClientName = DEFAULT_HTTP,
            string pcRequestServiceEndPoint = DEFAULT_ENDPOINT,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        public IAsyncEnumerable<LMM01501DTO> GetInvoiceGrpList()
        {
            throw new NotImplementedException();
        }

        public async Task<List<LMM01501DTO>> GetInvoiceGrpListAsync(string poPropertyIdParam)
        {
            var loEx = new R_Exception();
            List<LMM01501DTO> loResult = null;

            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstant.CPROPERTY_ID, poPropertyIdParam);

                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<LMM01501DTO>(
                    _RequestServiceEndPoint,
                    nameof(ILMM01500.GetInvoiceGrpList),
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

        public IAsyncEnumerable<LMM01500DTOPropety> GetProperty()
        {
            throw new NotImplementedException();
        }

        public async Task<List<LMM01500DTOPropety>> GetPropertyAsync()
        {
            var loEx = new R_Exception();
            List<LMM01500DTOPropety> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<LMM01500DTOPropety>(
                    _RequestServiceEndPoint,
                    nameof(ILMM01500.GetProperty),
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

        public LMM01500DTO LMM01500ActiveInactive(LMM01500DTO poParam)
        {
            throw new NotImplementedException();
        }

        public async Task LMM01500ActiveInactiveAsync(LMM01500DTO poParam)
        {
            var loEx = new R_Exception();
            LMM01500DTO loRtn = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<LMM01500DTO, LMM01500DTO>(
                    _RequestServiceEndPoint,
                    nameof(ILMM01500.LMM01500ActiveInactive),
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
        }

        public IAsyncEnumerable<LMM01502DTO> LMM01500LookupBank()
        {
            throw new NotImplementedException();
        }

        public async Task<List<LMM01502DTO>> LMM01500LookupBankAsync()
        {
            var loEx = new R_Exception();
            List<LMM01502DTO> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<LMM01502DTO>(
                    _RequestServiceEndPoint,
                    nameof(ILMM01500.LMM01500LookupBank),
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
    }
}
