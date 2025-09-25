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
    public class PMT00520Model : R_BusinessObjectServiceClientBase<PMT00520DTO>, IPMT00520
    {
        private const string DEFAULT_HTTP = "R_DefaultServiceUrlPM";
        private const string DEFAULT_ENDPOINT = "api/PMT00520";
        private const string DEFAULT_MODULE = "PM";

        public PMT00520Model(
            string pcHttpClientName = DEFAULT_HTTP,
            string pcRequestServiceEndPoint = DEFAULT_ENDPOINT,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        public async Task<List<PMT00520DTO>> GetLOIUtilitiesStreamAsync(PMT00520DTO poEntity)
        {
            var loEx = new R_Exception();
            List<PMT00520DTO> loResult = null;

            try
            {
                //Set Context
                R_FrontContext.R_SetStreamingContext(ContextConstant.CPROPERTY_ID, poEntity.CPROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CDEPT_CODE, poEntity.CDEPT_CODE);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CREF_NO, poEntity.CREF_NO);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CUNIT_ID, poEntity.CUNIT_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CFLOOR_ID, poEntity.CFLOOR_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CBUILDING_ID, poEntity.CBUILDING_ID);

                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMT00520DTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT00520.GetLOIUtilitiesStream),
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
        public async Task ChangeStatusLOIUtilityAsync(PMT00520ActiveInactiveDTO poParam)
        {
            var loEx = new R_Exception();

            try
            {
                //Set Context
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<PMT00500SingleResult<PMT00520ActiveInactiveDTO>, PMT00520ActiveInactiveDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT00520.ChangeStatusLOIUtility),
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
        public IAsyncEnumerable<PMT00520DTO> GetLOIUtilitiesStream()
        {
            throw new NotImplementedException();
        }

        public PMT00500SingleResult<PMT00520ActiveInactiveDTO> ChangeStatusLOIUtility(PMT00520ActiveInactiveDTO poEntity)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
