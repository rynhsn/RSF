using CBR00600COMMON;
using R_APIClient;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CBR00600MODEL
{
    public class CBR00600Model : R_BusinessObjectServiceClientBase<CBR00600DTO>, ICBR00600
    {
        private const string DEFAULT_HTTP = "R_DefaultServiceUrlCB";
        private const string DEFAULT_ENDPOINT = "api/CBR00600";
        private const string DEFAULT_MODULE = "CB";

        public CBR00600Model(
            string pcHttpClientName = DEFAULT_HTTP,
            string pcRequestServiceEndPoint = DEFAULT_ENDPOINT,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        public async Task<List<CBR00600PeriodDTO>> GetAllPeriodDTStreamAsync(int piYear)
        {
            var loEx = new R_Exception();
            List<CBR00600PeriodDTO> loResult = null;

            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstant.CYEAR, piYear.ToString());

                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<CBR00600PeriodDTO>(
                    _RequestServiceEndPoint,
                    nameof(ICBR00600.GetPeriodList),
                    DEFAULT_MODULE,
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
        public async Task<CBR00600Record<CBR00600InitialDTO>> GetInitialAsyncDTO()
        {

            var loEx = new R_Exception();
            CBR00600Record<CBR00600InitialDTO> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<CBR00600Record<CBR00600InitialDTO>>(
                    _RequestServiceEndPoint,
                    nameof(ICBR00600.GetInitialAsyncDTO),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);
                loResult = loTempResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        #region Not Implement
        public IAsyncEnumerable<CBR00600PeriodDTO> GetPeriodList()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
