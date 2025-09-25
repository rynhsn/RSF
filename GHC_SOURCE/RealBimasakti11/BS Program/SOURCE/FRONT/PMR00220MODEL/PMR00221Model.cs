using PMR00220COMMON;
using PMR00220COMMON.DTO_s;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PMR00220MODEL
{
    public class PMR00221Model : R_BusinessObjectServiceClientBase<PMR00220SPResultDTO>, IPMR00220
    {
        

        public PMR00221Model(string pcHttpClientName = PMR00220ContextConstant.DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = PMR00220ContextConstant.DEFAULT_CHECKPOINT_NAME,
            bool plSendWithContext = true,
            bool plSendWithToken = true
            ) : base(
                pcHttpClientName,
                pcRequestServiceEndPoint,
                PMR00220ContextConstant.DEFAULT_MODULE,
                plSendWithContext,
                plSendWithToken)
        {
        }

        public IAsyncEnumerable<PMR00220SPResultDTO> GetReportData()
        {
            throw new NotImplementedException();
        }
        public async Task<List<PMR00220SPResultDTO>> GetReportDataAsync()
        {
            var loEx = new R_Exception();
            List<PMR00220SPResultDTO> loResult = null;
            try
            {
                R_HTTPClientWrapper.httpClientName = PMR00220ContextConstant.DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMR00220SPResultDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMR00220.GetReportData),
                    PMR00220ContextConstant.DEFAULT_MODULE, _SendWithContext,
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
