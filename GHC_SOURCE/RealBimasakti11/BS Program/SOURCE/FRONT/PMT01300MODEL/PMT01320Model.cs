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
    public class PMT01320Model : R_BusinessObjectServiceClientBase<PMT01320DTO>, IPMT01320
    {
        private const string DEFAULT_HTTP = "R_DefaultServiceUrlPM";
        private const string DEFAULT_ENDPOINT = "api/PMT01320";
        private const string DEFAULT_MODULE = "PM";

        public PMT01320Model(
            string pcHttpClientName = DEFAULT_HTTP,
            string pcRequestServiceEndPoint = DEFAULT_ENDPOINT,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        public async Task<List<PMT01320DTO>> GetLOIUtilitiesStreamAsync(PMT01320DTO poEntity)
        {
            var loEx = new R_Exception();
            List<PMT01320DTO> loResult = null;

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
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMT01320DTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT01320.GetLOIUtilitiesStream),
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
        public async Task ChangeStatusLOIUtilityAsync(PMT01320ActiveInactiveDTO poParam)
        {
            var loEx = new R_Exception();

            try
            {
                //Set Context
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<PMT01300SingleResult<PMT01320ActiveInactiveDTO>, PMT01320ActiveInactiveDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT01320.ChangeStatusLOIUtility),
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
        public IAsyncEnumerable<PMT01320DTO> GetLOIUtilitiesStream()
        {
            throw new NotImplementedException();
        }

        public PMT01300SingleResult<PMT01320ActiveInactiveDTO> ChangeStatusLOIUtility(PMT01320ActiveInactiveDTO poEntity)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
