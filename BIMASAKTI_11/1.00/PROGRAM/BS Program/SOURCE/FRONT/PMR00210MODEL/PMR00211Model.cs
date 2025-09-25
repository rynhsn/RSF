using PMR00210COMMON;
using PMR00210COMMON.DTO_s;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PMR00210MODEL
{
    public class PMR00211Model : R_BusinessObjectServiceClientBase<PMR00210SPResultDTO>, IPMR00210
    {
        

        public PMR00211Model(string pcHttpClientName = PMR00210ContextConstant.DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = PMR00210ContextConstant.DEFAULT_CHECKPOINT_NAME,
            bool plSendWithContext = true,
            bool plSendWithToken = true
            ) : base(
                pcHttpClientName,
                pcRequestServiceEndPoint,
                PMR00210ContextConstant.DEFAULT_MODULE,
                plSendWithContext,
                plSendWithToken)
        {
        }

        public IAsyncEnumerable<PMR00210SPResultDTO> GetReportData()
        {
            throw new NotImplementedException();
        }
        public async Task<List<PMR00210SPResultDTO>> GetReportDataAsync()
        {
            var loEx = new R_Exception();
            List<PMR00210SPResultDTO> loResult = null;
            try
            {
                R_HTTPClientWrapper.httpClientName = PMR00210ContextConstant.DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMR00210SPResultDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMR00210.GetReportData),
                    PMR00210ContextConstant.DEFAULT_MODULE, _SendWithContext,
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
