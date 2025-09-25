using LMM01500COMMON;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LMM01500MODEL
{
    public class LMM01520Model : R_BusinessObjectServiceClientBase<LMM01520DTO>, ILMM01520
    {
        private const string DEFAULT_HTTP = "R_DefaultServiceUrlLM";
        private const string DEFAULT_ENDPOINT = "api/LMM01520";
        private const string DEFAULT_MODULE = "LM";

        public LMM01520Model(
            string pcHttpClientName = DEFAULT_HTTP,
            string pcRequestServiceEndPoint = DEFAULT_ENDPOINT,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        public IAsyncEnumerable<LMM01522DTO> GetAdditionalIdLookup()
        {
            throw new NotImplementedException();
        }

        public async Task<List<LMM01522DTO>> GetAdditionalIdLookupAsync()
        {
            var loEx = new R_Exception();
            List<LMM01522DTO> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<LMM01522DTO>(
                    _RequestServiceEndPoint,
                    nameof(ILMM01520.GetAdditionalIdLookup),
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
