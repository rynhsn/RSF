using GLM00400COMMON;
using R_APIClient;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GLM00400MODEL
{
    public class GLM00410Model : R_BusinessObjectServiceClientBase<GLM00410DTO>, IGLM00410
    {
        private const string DEFAULT_HTTP = "R_DefaultServiceUrlGL";
        private const string DEFAULT_ENDPOINT = "api/GLM00410";
        private const string DEFAULT_MODULE = "GL";

        public GLM00410Model(
            string pcHttpClientName = DEFAULT_HTTP,
            string pcRequestServiceEndPoint = DEFAULT_ENDPOINT,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        #region GetAllocationList
        public IAsyncEnumerable<GLM00411DTO> GetAllocationAccountList()
        {
            throw new NotImplementedException();
        }

        public async Task<List<GLM00411DTO>> GetAllocationAccountListAsync(GLM00411DTO poParam)
        {
            var loEx = new R_Exception();
            List<GLM00411DTO> loResult = null;

            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstant.CREC_ID_ALLOCATION_ID, poParam.CREC_ID_ALLOCATION_ID);

                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GLM00411DTO>(
                    _RequestServiceEndPoint,
                    nameof(IGLM00410.GetAllocationAccountList),
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
        #endregion

        #region GetAllocationPeriodByTargetCenter
        public IAsyncEnumerable<GLM00415DTO> GetAllocationPeriodByTargetCenterList()
        {
            throw new NotImplementedException();
        }

        public async Task<List<GLM00415DTO>> GetAllocationPeriodByTargetCenterListAsync(GLM00415DTO poParam)
        {
            var loEx = new R_Exception();
            List<GLM00415DTO> loResult = null;

            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstant.CREC_ID_ALLOCATION_ID, poParam.CREC_ID_ALLOCATION_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CPERIOD_NO, poParam.CPERIOD_NO);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CYEAR, poParam.CYEAR);

                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GLM00415DTO>(
                    _RequestServiceEndPoint,
                    nameof(IGLM00410.GetAllocationPeriodByTargetCenterList),
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
        #endregion

        #region GetAllocationPeriod
        public IAsyncEnumerable<GLM00414DTO> GetAllocationPeriodList()
        {
            throw new NotImplementedException();
        }

        public async Task<List<GLM00414DTO>> GetAllocationPeriodListAsync(GLM00414DTO poParam)
        {
            var loEx = new R_Exception();
            List<GLM00414DTO> loResult = null;

            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstant.CYEAR, poParam.CCYEAR);

                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GLM00414DTO>(
                    _RequestServiceEndPoint,
                    nameof(IGLM00410.GetAllocationPeriodList),
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

        
        #endregion

        #region GetAllocationTargetCenterByPeriod
        public IAsyncEnumerable<GLM00413DTO> GetAllocationTargetCenterByPeriodList()
        {
            throw new NotImplementedException();
        }
        public async Task<List<GLM00413DTO>> GetAllocationTargetCenterByPeriodListAsync(GLM00413DTO poParam)
        {
            var loEx = new R_Exception();
            List<GLM00413DTO> loResult = null;

            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstant.CYEAR, poParam.CYEAR);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CREC_ID_CENTER_ID, poParam.CREC_ID_CENTER_ID);

                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GLM00413DTO>(
                    _RequestServiceEndPoint,
                    nameof(IGLM00410.GetAllocationTargetCenterByPeriodList),
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
        #endregion

        #region GetAllocationTargetCenter
        public IAsyncEnumerable<GLM00412DTO> GetAllocationTargetCenterList()
        {
            throw new NotImplementedException();
        }

        public async Task<List<GLM00412DTO>> GetAllocationTargetCenterListAsync(GLM00412DTO poParam)
        {
            var loEx = new R_Exception();
            List<GLM00412DTO> loResult = null;

            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstant.CREC_ID_ALLOCATION_ID, poParam.CREC_ID_ALLOCATION_ID);

                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GLM00412DTO>(
                    _RequestServiceEndPoint,
                    nameof(IGLM00410.GetAllocationTargetCenterList),
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

        #endregion

        #region GetAllocationTargetCenterByPeriod
        public GLM00400Result<GLM00413DTO> GetAllocationTargetCenterByPeriod(GLM00413DTO poParam)
        {
            throw new NotImplementedException();
        }

        public async Task<GLM00413DTO> GetAllocationTargetCenterByPeriodAsync(GLM00413DTO poParam)
        {
            var loEx = new R_Exception();
            GLM00413DTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<GLM00400Result<GLM00413DTO>, GLM00413DTO>(
                    _RequestServiceEndPoint,
                    nameof(IGLM00410.GetAllocationTargetCenterByPeriod),
                    poParam,
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loResult = loTempResult.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        #endregion
        #region SaveAllocationTargetCenterByPeriod
        public GLM00400Result<GLM00413DTO> SaveAllocationTargetCenterByPeriod(GLM00413DTO poParam)
        {
            throw new NotImplementedException();
        }

        public async Task<GLM00413DTO> SaveAllocationTargetCenterByPeriodAsync(GLM00413DTO poParam)
        {
            var loEx = new R_Exception();
            GLM00413DTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<GLM00400Result<GLM00413DTO>, GLM00413DTO>(
                    _RequestServiceEndPoint,
                    nameof(IGLM00410.SaveAllocationTargetCenterByPeriod),
                    poParam,
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loResult = loTempResult.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        #endregion
    }
}
