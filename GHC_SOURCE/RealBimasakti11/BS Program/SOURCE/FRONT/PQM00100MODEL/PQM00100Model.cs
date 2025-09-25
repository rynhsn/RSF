using PQM00100COMMON;
using PQM00100COMMON.DTO_s;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PQM00100MODEL
{
    public class PQM00100Model : R_BusinessObjectServiceClientBase<ServiceGridDTO>, IPQM00100
    {
        private const string DEFAULT_CHECKPOINT_NAME = "api/PQM00100";

        public PQM00100Model(string pcHttpClientName = PQM00100ContextConstant.DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_CHECKPOINT_NAME,
            bool plSendWithContext = true,
            bool plSendWithToken = true
            ) : base(
                pcHttpClientName,
                pcRequestServiceEndPoint,
                PQM00100ContextConstant.DEFAULT_MODULE,
                plSendWithContext,
                plSendWithToken)
        {
        }

        public async Task<List<ServiceGridDTO>> GetList_ServiceAsync()
        {
            var loEx = new R_Exception();
            List<ServiceGridDTO> loResult = null;
            try
            {
                R_HTTPClientWrapper.httpClientName = PQM00100ContextConstant.DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<ServiceGridDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPQM00100.GetList_Service),
                    PQM00100ContextConstant.DEFAULT_MODULE,
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

        public IAsyncEnumerable<ServiceGridDTO> GetList_Service()
        {
            throw new NotImplementedException();
        }
    }
}