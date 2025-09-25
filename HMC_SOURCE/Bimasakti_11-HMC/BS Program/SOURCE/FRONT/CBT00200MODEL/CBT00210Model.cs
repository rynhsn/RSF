using CBT00200COMMON;
using R_APIClient;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CBT00200MODEL
{
    public class CBT00210Model : R_BusinessObjectServiceClientBase<CBT00210DTO>, ICBT00210
    {
        private const string DEFAULT_HTTP = "R_DefaultServiceUrlCB";
        private const string DEFAULT_ENDPOINT = "api/CBT00210";
        private const string DEFAULT_MODULE = "CB";
        public CBT00210Model(
           string pcHttpClientName = DEFAULT_HTTP,
           string pcRequestServiceEndPoint = DEFAULT_ENDPOINT,
           bool plSendWithContext = true,
           bool plSendWithToken = true) :
           base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        public async Task<List<CBT00210DTO>> GetJournalDetailListAsync(CBT00210DTO poEntity)
        {
            var loEx = new R_Exception();
            List<CBT00210DTO> loResult = null;

            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstant.CREC_ID, poEntity.CREC_ID);

                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<CBT00210DTO>(
                    _RequestServiceEndPoint,
                    nameof(ICBT00210.GetJournalDetailList),
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

        #region Not Implement
        public IAsyncEnumerable<CBT00210DTO> GetJournalDetailList()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
