using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lookup_ICCOMMON;
using Lookup_ICCOMMON.DTOs;
using Lookup_ICCOMMON.DTOs.ICL00100;
using Lookup_ICCOMMON.DTOs.ICL00200;
using Lookup_ICCOMMON.DTOs.ICL00300;
using R_APIClient;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;

namespace Lookup_ICModel
{
    public class PublicICLookupModel : R_BusinessObjectServiceClientBase<ICL00100DTO>, IPublicICLookup
    {
        private const string DEFAULT_HTTP = "R_DefaultServiceUrlIC";
        private const string DEFAULT_ENDPOINT = "api/PublicLookupIC";
        private const string DEFAULT_MODULE = "IC";

        public PublicICLookupModel(
            string pcHttpClientName = DEFAULT_HTTP,
            string pcRequestServiceEndPoint = DEFAULT_ENDPOINT,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        public async Task<List<ICL00100DTO>> ICL00100RequestLookupAsync(ICL00100ParameterDTO poParam)
        {
            var loEx = new R_Exception();
            List<ICL00100DTO> loResult = null;

            try
            {
                //context
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CPROPERTY_ID, poParam.CPROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CDEPT_CODE, poParam.CDEPT_CODE);

                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<ICL00100DTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicICLookup.ICL00100RequestLookup),
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
        
        public async Task<List<ICL00200DTO>> ICL00200RequestNoLookupAsync(ICL00200ParameterDTO poParam)
        {
            var loEx = new R_Exception();
            List<ICL00200DTO> loResult = null;

            try
            {
                //context
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CPROPERTY_ID, poParam.CPROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CDEPT_CODE, poParam.CDEPT_CODE);
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CALLOC_ID, poParam.CALLOC_ID);

                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<ICL00200DTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicICLookup.ICL00200RequestNoLookup),
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
        
        public async Task<List<ICL00300DTO>> ICL00300TransactionLookupAsync(ICL00300ParameterDTO poParam)
        {
            var loEx = new R_Exception();
            List<ICL00300DTO> loResult = null;

            try
            {
                //context
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CPROPERTY_ID, poParam.CPROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CTRANS_CODE, poParam.CTRANS_CODE);
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CPERIOD, poParam.CPERIOD);
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CDEPT_CODE, poParam.CDEPT_CODE);
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CWAREHOUSE_ID, poParam.CWAREHOUSE_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CALLOC_ID, poParam.CALLOC_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CTRANS_STATUS, poParam.CTRANS_STATUS);
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<ICL00300DTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicICLookup.ICL00300TransactionLookup),
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
        public async Task <ICL00300PeriodDTO> ICLInitiateTransactionLookupAsync()
        {
            var loEx = new R_Exception();
            ICL00300PeriodDTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<ICL00300PeriodDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicICLookup.ICLInitiateTransactionLookup),
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
        public IAsyncEnumerable<ICL00100DTO> ICL00100RequestLookup()
        {
            throw new System.NotImplementedException();
        }

        public IAsyncEnumerable<ICL00200DTO> ICL00200RequestNoLookup()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<ICL00300DTO> ICL00300TransactionLookup()
        {
            throw new NotImplementedException();
        }

        public ICL00300PeriodDTO ICLInitiateTransactionLookup()
        {
            throw new NotImplementedException();
        }
    }
}