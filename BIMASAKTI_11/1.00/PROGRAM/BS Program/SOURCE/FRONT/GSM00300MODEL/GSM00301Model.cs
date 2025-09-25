using GSM00300COMMON;
using GSM00300COMMON.DTO_s;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GSM00300MODEL
{
    public class GSM00301Model : R_BusinessObjectServiceClientBase<TaxInfoDTO>, IGSM00301
    {
        private const string DEFAULT_CHECKPOINT_NAME = "api/GSM00301";

        public GSM00301Model(
            string pcHttpClientName = GSM00300ContextConstant.DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_CHECKPOINT_NAME,
            bool plSendWithContext = true,
            bool plSendWithToken = true
            ) : base(
                pcHttpClientName,
                pcRequestServiceEndPoint,
                GSM00300ContextConstant.DEFAULT_MODULE,
                plSendWithContext,
                plSendWithToken)
        {
        }

        public async Task<List<GSBCodeInfoDTO>> GetRftGSBCodeInfoListAsync()
        {
            var loEx = new R_Exception();
            List<GSBCodeInfoDTO> loResult = null;
            try
            {
                R_HTTPClientWrapper.httpClientName = GSM00300ContextConstant.DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GSBCodeInfoDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGSM00301.GetRftGSBCodeInfoList),
                    GSM00300ContextConstant.DEFAULT_MODULE,
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

        public IAsyncEnumerable<GSBCodeInfoDTO> GetRftGSBCodeInfoList()
        {
            throw new NotImplementedException();
        }
    }
}