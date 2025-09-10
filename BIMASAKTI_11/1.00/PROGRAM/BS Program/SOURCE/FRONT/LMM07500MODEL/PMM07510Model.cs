using PMM07500COMMON.DTO_s.stamp_code;
using PMM07500COMMON.Interfaces;
using PMM07500COMMON;
using R_BusinessObjectFront;
using System;
using PMM07500COMMON.DTO_s.stamp_date;
using System.Collections.Generic;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using System.Threading.Tasks;

namespace LMM07500MODEL
{
    public class PMM07510Model : R_BusinessObjectServiceClientBase<PMM07510GridDTO>, IPMM07510
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlPM";
        private const string DEFAULT_CHECKPOINT_NAME = "api/PMM07510";
        private const string DEFAULT_MODULE = "PM";
        public PMM07510Model(string pcHttpClientName = DEFAULT_HTTP_NAME,
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

        public IAsyncEnumerable<PMM07510GridDTO> GetStampDateList()
        {
            throw new NotImplementedException();
        }

        public async Task<List<PMM07510GridDTO>> GetStampDateListAsync()
        {
            var loEx = new R_Exception();
            List<PMM07510GridDTO> loResult = null;
            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMM07510GridDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMM07510.GetStampDateList),
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
