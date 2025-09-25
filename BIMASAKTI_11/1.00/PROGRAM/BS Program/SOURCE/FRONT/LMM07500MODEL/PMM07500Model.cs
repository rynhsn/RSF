using PMM07500COMMON;
using PMM07500COMMON.DTO_s.stamp_code;
using PMM07500COMMON.Interfaces;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LMM07500MODEL
{
    public class PMM07500Model : R_BusinessObjectServiceClientBase<PMM07500GridDTO>, IPMM07500
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlPM";
        private const string DEFAULT_CHECKPOINT_NAME = "api/PMM07500";
        private const string DEFAULT_MODULE = "PM";
        public PMM07500Model(string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_CHECKPOINT_NAME,
            bool plSendWithContext = true,
            bool plSendWithToken = true
            ) : base(
                pcHttpClientName,
                pcRequestServiceEndPoint,
                DEFAULT_MODULE,
                plSendWithContext,
                plSendWithToken)
        {
        }


        public IAsyncEnumerable<PMM07500GridDTO> GetStampList()
        {
            throw new NotImplementedException();
        }

        public async Task<List<PMM07500GridDTO>> GetStampListAsync()
        {
            var loEx = new R_Exception();
            List<PMM07500GridDTO> loResult = null;
            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMM07500GridDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMM07500.GetStampList),
                    DEFAULT_MODULE, _SendWithContext,
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
