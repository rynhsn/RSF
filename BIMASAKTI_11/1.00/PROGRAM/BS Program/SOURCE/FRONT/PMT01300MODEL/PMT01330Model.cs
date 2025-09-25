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
    public class PMT01330Model : R_BusinessObjectServiceClientBase<PMT01330DTO>, IPMT01330
    {
        private const string DEFAULT_HTTP = "R_DefaultServiceUrlPM";
        private const string DEFAULT_ENDPOINT = "api/PMT01330";
        private const string DEFAULT_MODULE = "PM";

        public PMT01330Model(
            string pcHttpClientName = DEFAULT_HTTP,
            string pcRequestServiceEndPoint = DEFAULT_ENDPOINT,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        public async Task<List<PMT01330DTO>> GetLOIChargeStreamAsync(PMT01330DTO poEntity)
        {
            var loEx = new R_Exception();
            List<PMT01330DTO> loResult = null;

            try
            {
                //Set Context
                R_FrontContext.R_SetStreamingContext(ContextConstant.CPROPERTY_ID, poEntity.CPROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CDEPT_CODE, poEntity.CDEPT_CODE);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CREF_NO, poEntity.CREF_NO);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CCHARGE_MODE, poEntity.CCHARGE_MODE);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CUNIT_ID, poEntity.CUNIT_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CFLOOR_ID, poEntity.CFLOOR_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CBUILDING_ID, poEntity.CBUILDING_ID);

                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMT01330DTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT01330.GetLOIChargeStream),
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

        public async Task ChangeStatusLOIChargesAsync(PMT01330ActiveInactiveDTO poParam)
        {
            var loEx = new R_Exception();

            try
            {
                //Set Context
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<PMT01300SingleResult<PMT01330ActiveInactiveDTO>, PMT01330ActiveInactiveDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT01330.ChangeStatusLOICharges),
                    poParam,
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        #region Not Implment
        public IAsyncEnumerable<PMT01330DTO> GetLOIChargeStream()
        {
            throw new NotImplementedException();
        }

        public PMT01300SingleResult<PMT01330ActiveInactiveDTO> ChangeStatusLOICharges(PMT01330ActiveInactiveDTO poEntity)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
