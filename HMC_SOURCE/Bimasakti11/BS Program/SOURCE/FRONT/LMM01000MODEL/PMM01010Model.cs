using PMM01000COMMON;
using R_APIClient;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PMM01000MODEL
{
    public class PMM01010Model : R_BusinessObjectServiceClientBase<PMM01010DTO>, IPMM01010
    {
        private const string DEFAULT_HTTP = "R_DefaultServiceUrlPM";
        private const string DEFAULT_ENDPOINT = "api/PMM01010";
        private const string DEFAULT_MODULE = "PM";

        public PMM01010Model(
            string pcHttpClientName = DEFAULT_HTTP,
            string pcRequestServiceEndPoint = DEFAULT_ENDPOINT,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        public IAsyncEnumerable<PMM01010DTO> GetRateECDateList()
        {
            throw new NotImplementedException();
        }
        public async Task<PMM01000List<PMM01010DTO>> GetRateECDateListAsync(PMM01010DTO poParam)
        {
            var loEx = new R_Exception();
            PMM01000List<PMM01010DTO> loRtn = new PMM01000List<PMM01010DTO>();

            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstant.CPROPERTY_ID, poParam.CPROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CCHARGES_TYPE, poParam.CCHARGES_TYPE);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CCHARGES_ID, poParam.CCHARGES_ID);


                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loRtn.Data = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMM01010DTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMM01010.GetRateECDateList),
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

        public IAsyncEnumerable<PMM01011DTO> GetRateECList()
        {
            throw new NotImplementedException();
        }

        public async Task<PMM01000List<PMM01011DTO>> GetRateECListAsync(PMM01011DTO poParam)
        {
            var loEx = new R_Exception();
            PMM01000List<PMM01011DTO> loRtn = new PMM01000List<PMM01011DTO>();

            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstant.CPROPERTY_ID, poParam.CPROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CCHARGES_TYPE, poParam.CCHARGES_TYPE);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CCHARGES_ID, poParam.CCHARGES_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CCHARGES_DATE, poParam.CCHARGES_DATE);

                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loRtn.Data = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMM01011DTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMM01010.GetRateECList),
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
