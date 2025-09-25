using CBT01200Common;
using R_APIClient;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CBT01200Common.DTOs;

namespace CBT01200MODEL
{
    public class CBT01210Model : R_BusinessObjectServiceClientBase<CBT01210ParamDTO>, ICBT01210
    {
        private const string DEFAULT_HTTP = "R_DefaultServiceUrlCB";
        private const string DEFAULT_ENDPOINT = "api/CBT01210";
        private const string DEFAULT_MODULE = "CB";

        public CBT01210Model
            (
                string pcHttpClientName = DEFAULT_HTTP,
                string pcRequestServiceEndPoint = DEFAULT_ENDPOINT,
                bool plSendWithContext = true,
                bool plSendWithToken = true
            )
        : base
            (
                pcHttpClientName,
                pcRequestServiceEndPoint,
                DEFAULT_MODULE,
                plSendWithContext,
                plSendWithToken
            )
        { }

        public async Task<List<CBT01201DTO>> GetJournalDetailListAsync()
        {
            var loEx = new R_Exception();
            List<CBT01201DTO> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<CBT01201DTO>(
                    _RequestServiceEndPoint,
                    nameof(ICBT01210.GetJournalDetailList),
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
        public IAsyncEnumerable<CBT01201DTO> GetJournalDetailList()
        {
            throw new NotImplementedException();
        }
        #endregion

      
    }
}
