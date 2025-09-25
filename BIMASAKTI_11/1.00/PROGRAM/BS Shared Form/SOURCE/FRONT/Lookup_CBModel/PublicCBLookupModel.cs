using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lookup_CBCOMMON;
using Lookup_CBCOMMON.DTOs;
using Lookup_CBCOMMON.DTOs.CBL00100;
using Lookup_CBCOMMON.DTOs.CBL00200;
using R_APIClient;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;

namespace Lookup_CBModel
{
    public class PublicCBLookupModel : R_BusinessObjectServiceClientBase<CBL00100DTO>, IPublicCBLookup
    {
        private const string DEFAULT_HTTP = "R_DefaultServiceUrlCB";
        private const string DEFAULT_ENDPOINT = "api/PublicCBLookup";
        private const string DEFAULT_MODULE = "CB";

        public PublicCBLookupModel(
            string pcHttpClientName = DEFAULT_HTTP,
            string pcRequestServiceEndPoint = DEFAULT_ENDPOINT,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        public async Task<CBL00100DTO> CBL00100InitialProcessLookupAsync()
        {
            var loEx = new R_Exception();
            CBL00100DTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<CBL00100DTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicCBLookup.CBL00100InitialProcessLookup),
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
        
        public async Task<List<CBL00100DTO>> CBL00100ReceiptFromCustomerLookupAsync(CBL00100ParameterDTO poParam)
        {
            var loEx = new R_Exception();
            List<CBL00100DTO> loResult = null;

            try
            {
                //context
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CPROPERTY_ID, poParam.CPROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CPERIOD, poParam.CPERIOD);
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CDEPT_CODE, poParam.CDEPT_CODE);
                R_HTTPClientWrapper.httpClientName = _HttpClientName;   
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<CBL00100DTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicCBLookup.CBL00100ReceiptFromCustomerLookup),
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

        public async Task<List<CBL00200DTO>> CBL00200JournalLookupAsync(CBL00200ParameterDTO poParam)
        {
            var loEx = new R_Exception();
            List<CBL00200DTO> loResult = null;

            try
            {
                //context
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CDEPT_CODE, poParam.CDEPT_CODE);
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CTRANS_CODE, poParam.CTRANS_CODE);
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CPERIOD, poParam.CPERIOD);
                R_HTTPClientWrapper.httpClientName = _HttpClientName;   
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<CBL00200DTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicCBLookup.CBL00200JournalLookup),
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
        public CBL00100DTO CBL00100InitialProcessLookup()
        {
            throw new NotImplementedException();
        }

        public CBL00100DTO InitialProcessCBL00100Month()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<CBL00100DTO> CBL00100ReceiptFromCustomerLookup()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<CBL00200DTO> CBL00200JournalLookup()
        {
            throw new NotImplementedException();
        }
    }
}