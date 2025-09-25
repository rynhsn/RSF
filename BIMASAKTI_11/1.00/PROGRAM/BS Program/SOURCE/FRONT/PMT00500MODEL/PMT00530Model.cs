using PMT00500COMMON;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PMT00500MODEL
{
    public class PMT00530Model : R_BusinessObjectServiceClientBase<PMT00530DTO>, IPMT00530
    {
        private const string DEFAULT_HTTP = "R_DefaultServiceUrlPM";
        private const string DEFAULT_ENDPOINT = "api/PMT00530";
        private const string DEFAULT_MODULE = "PM";

        public PMT00530Model(
            string pcHttpClientName = DEFAULT_HTTP,
            string pcRequestServiceEndPoint = DEFAULT_ENDPOINT,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        public async Task<List<PMT00530DTO>> GetLOIChargeStreamAsync(PMT00530DTO poEntity)
        {
            var loEx = new R_Exception();
            List<PMT00530DTO> loResult = null;

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
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMT00530DTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT00530.GetLOIChargeStream),
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

        public async Task ChangeStatusLOIChargesAsync(PMT00530ActiveInactiveDTO poParam)
        {
            var loEx = new R_Exception();

            try
            {
                //Set Context
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<PMT00500SingleResult<PMT00530ActiveInactiveDTO>, PMT00530ActiveInactiveDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT00530.ChangeStatusLOICharges),
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
        public IAsyncEnumerable<PMT00530DTO> GetLOIChargeStream()
        {
            throw new NotImplementedException();
        }

        public PMT00500SingleResult<PMT00530ActiveInactiveDTO> ChangeStatusLOICharges(PMT00530ActiveInactiveDTO poEntity)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
