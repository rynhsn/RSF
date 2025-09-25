using CBT01100COMMON;
using CBT01100COMMON.DTO_s.CBT01110;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CBT01100MODEL
{
    public class CBT01110Model : R_BusinessObjectServiceClientBase<CBT01110ParamDTO>, ICBT01110
    {
        private const string DEFAULT_HTTP = "R_DefaultServiceUrlCB";
        private const string DEFAULT_ENDPOINT = "api/CBT01110";
        private const string DEFAULT_MODULE = "CB";

        public CBT01110Model
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

        public async Task<List<CBT01101DTO>> GetJournalDetailListAsync()
        {
            var loEx = new R_Exception();
            List<CBT01101DTO> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<CBT01101DTO>(
                    _RequestServiceEndPoint,
                    nameof(ICBT01110.GetJournalDetailList),
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
        public IAsyncEnumerable<CBT01101DTO> GetJournalDetailList()
        {
            throw new NotImplementedException();
        }
        #endregion


    }
}
