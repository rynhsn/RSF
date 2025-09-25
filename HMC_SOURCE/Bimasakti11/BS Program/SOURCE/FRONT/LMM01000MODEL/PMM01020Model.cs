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
    public class PMM01020Model : R_BusinessObjectServiceClientBase<PMM01020DTO>, IPMM01020
    {
        private const string DEFAULT_HTTP = "R_DefaultServiceUrlPM";
        private const string DEFAULT_ENDPOINT = "api/PMM01020";
        private const string DEFAULT_MODULE = "PM";

        public PMM01020Model(
            string pcHttpClientName = DEFAULT_HTTP,
            string pcRequestServiceEndPoint = DEFAULT_ENDPOINT,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        public async Task<PMM01000List<PMM01021DTO>> GetRateWGListAsync(PMM01021DTO poParam)
        {
            var loEx = new R_Exception();
            PMM01000List<PMM01021DTO> loRtn = new PMM01000List<PMM01021DTO>();

            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstant.CPROPERTY_ID, poParam.CPROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CCHARGES_TYPE, poParam.CCHARGES_TYPE);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CCHARGES_ID, poParam.CCHARGES_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CCHARGES_DATE, poParam.CCHARGES_DATE);

                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loRtn.Data = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMM01021DTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMM01020.GetRateWGList),
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

        public IAsyncEnumerable<PMM01021DTO> GetRateWGList()
        {
            throw new NotImplementedException();
        }

        public async Task<PMM01000List<PMM01020DTO>> GetRateWGDateListAsync(PMM01020DTO poParam)
        {
            var loEx = new R_Exception();
            PMM01000List<PMM01020DTO> loRtn = new PMM01000List<PMM01020DTO>();

            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstant.CPROPERTY_ID, poParam.CPROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CCHARGES_TYPE, poParam.CCHARGES_TYPE);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CCHARGES_ID, poParam.CCHARGES_ID);

                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loRtn.Data = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMM01020DTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMM01020.GetRateWGDateList),
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

        public IAsyncEnumerable<PMM01020DTO> GetRateWGDateList()
        {
            throw new NotImplementedException();
        }
    }
}
