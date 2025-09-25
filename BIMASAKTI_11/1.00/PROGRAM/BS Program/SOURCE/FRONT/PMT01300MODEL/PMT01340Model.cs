using PMT01300COMMON;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PMT01300MODEL
{
    public class PMT01340Model : R_BusinessObjectServiceClientBase<PMT01340DTO>, IPMT01340
    {
        private const string DEFAULT_HTTP = "R_DefaultServiceUrlPM";
        private const string DEFAULT_ENDPOINT = "api/PMT01340";
        private const string DEFAULT_MODULE = "PM";

        public PMT01340Model(
            string pcHttpClientName = DEFAULT_HTTP,
            string pcRequestServiceEndPoint = DEFAULT_ENDPOINT,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        public async Task<List<PMT01340DTO>> GetLOIDepositStreamAsync(PMT01340DTO poEntity)
        {
            var loEx = new R_Exception();
            List<PMT01340DTO> loResult = null;

            try
            {
                //Set Context
                R_FrontContext.R_SetStreamingContext(ContextConstant.CPROPERTY_ID, poEntity.CPROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CDEPT_CODE, poEntity.CDEPT_CODE);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CREF_NO, poEntity.CREF_NO);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CCHARGE_MODE, poEntity.CCHARGE_MODE);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CUNIT_ID, string.IsNullOrWhiteSpace(poEntity.CUNIT_ID) ? "" : poEntity.CUNIT_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CFLOOR_ID, string.IsNullOrWhiteSpace(poEntity.CFLOOR_ID) ? "" : poEntity.CFLOOR_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CBUILDING_ID, string.IsNullOrWhiteSpace(poEntity.CBUILDING_ID) ? "" : poEntity.CBUILDING_ID);

                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMT01340DTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT01340.GetLOIDepositStream),
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

        #region Not Implment
        public IAsyncEnumerable<PMT01340DTO> GetLOIDepositStream()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
