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
    public class PMM01050Model : R_BusinessObjectServiceClientBase<PMM01050DTO>, IPMM01050
    {
        private const string DEFAULT_HTTP = "R_DefaultServiceUrlPM";
        private const string DEFAULT_ENDPOINT = "api/PMM01050";
        private const string DEFAULT_MODULE = "PM";

        public PMM01050Model(
            string pcHttpClientName = DEFAULT_HTTP,
            string pcRequestServiceEndPoint = DEFAULT_ENDPOINT,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        public IAsyncEnumerable<PMM01050DTO> GetRateOTDateList()
        {
            throw new NotImplementedException();
        }
        public async Task<PMM01000List<PMM01050DTO>> GetRateOTDateListAsync(PMM01050DTO poParam)
        {
            var loEx = new R_Exception();
            PMM01000List<PMM01050DTO> loRtn = new PMM01000List<PMM01050DTO>();

            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstant.CPROPERTY_ID, poParam.CPROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CCHARGES_TYPE, poParam.CCHARGES_TYPE);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CCHARGES_ID, poParam.CCHARGES_ID);

                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loRtn.Data = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMM01050DTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMM01050.GetRateOTDateList),
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
        public IAsyncEnumerable<PMM01051DTO> GetRateOTList()
        {
            throw new NotImplementedException();
        }

        public async Task<PMM01000List<PMM01051DTO>> GetRateOTListAsync(PMM01051DTO poParam)
        {
            var loEx = new R_Exception();
            PMM01000List<PMM01051DTO> loRtn = new PMM01000List<PMM01051DTO>();

            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstant.CPROPERTY_ID, poParam.CPROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CCHARGES_TYPE, poParam.CCHARGES_TYPE);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CCHARGES_ID, poParam.CCHARGES_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CDAY_TYPE, poParam.CDAY_TYPE);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CCHARGES_DATE, poParam.CCHARGES_DATE);

                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loRtn.Data = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMM01051DTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMM01050.GetRateOTList),
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
