using PMM01000COMMON;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PMM01000MODEL
{
    public class PMM01040Model : R_BusinessObjectServiceClientBase<PMM01040DTO>, IPMM01040
    {
        private const string DEFAULT_HTTP = "R_DefaultServiceUrlPM";
        private const string DEFAULT_ENDPOINT = "api/PMM01040";
        private const string DEFAULT_MODULE = "PM";

        public PMM01040Model(
            string pcHttpClientName = DEFAULT_HTTP,
            string pcRequestServiceEndPoint = DEFAULT_ENDPOINT,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        public IAsyncEnumerable<PMM01040DTO> GetRateGUDateList()
        {
            throw new System.NotImplementedException();
        }
        public async Task<PMM01000List<PMM01040DTO>> GetRateGUDateListAsync(PMM01040DTO poParam)
        {
            var loEx = new R_Exception();
            PMM01000List<PMM01040DTO> loRtn = new PMM01000List<PMM01040DTO>();

            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstant.CPROPERTY_ID, poParam.CPROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CCHARGES_TYPE, poParam.CCHARGES_TYPE);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CCHARGES_ID, poParam.CCHARGES_ID);

                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loRtn.Data = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMM01040DTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMM01040.GetRateGUDateList),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

        EndBlock:
            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }
    }
}
