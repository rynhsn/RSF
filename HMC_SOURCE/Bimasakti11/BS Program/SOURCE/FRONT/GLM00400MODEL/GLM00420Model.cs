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
    public class GLM00420Model : R_BusinessObjectServiceClientBase<GLM00420DTO>, IGLM00420
    {
        private const string DEFAULT_HTTP = "R_DefaultServiceUrlGL";
        private const string DEFAULT_ENDPOINT = "api/GLM00420";
        private const string DEFAULT_MODULE = "GL";

        public GLM00420Model(
            string pcHttpClientName = DEFAULT_HTTP,
            string pcRequestServiceEndPoint = DEFAULT_ENDPOINT,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        #region AllocationCenter
        public IAsyncEnumerable<GLM00421DTO> GetAllocationCenterList()
        {
            throw new NotImplementedException();
        }

        public async Task<List<GLM00421DTO>> GetAllocationCenterListAsync(GLM00421DTO poParam)
        {
            var loEx = new R_Exception();
            List<GLM00421DTO> loResult = null;

            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstant.CREC_ID_ALLOCATION_ID, poParam.CREC_ID_ALLOCATION_ID);

                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GLM00421DTO>(
                    _RequestServiceEndPoint,
                    nameof(IGLM00420.GetAllocationCenterList),
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

        #region GetSourceAllocationCenter
        public IAsyncEnumerable<GLM00420DTO> GetSourceAllocationCenterList()
        {
            throw new NotImplementedException();
        }

        public async Task<List<GLM00420DTO>> GetSourceAllocationCenterListAsync(GLM00420DTO poParam)
        {
            var loEx = new R_Exception();
            List<GLM00420DTO> loResult = null;

            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstant.CREC_ID_ALLOCATION_ID, poParam.CREC_ID_ALLOCATION_ID);

                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GLM00420DTO>(
                    _RequestServiceEndPoint,
                    nameof(IGLM00420.GetSourceAllocationCenterList),
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

        #region SaveAllocationCenter
        public GLM00421DTO SaveAllocationCenterList(GLM00421DTO poParam)
        {
            throw new NotImplementedException();
        }

        public async Task SaveAllocationCenterListAsync(GLM00421DTO poParam)
        {
            var loEx = new R_Exception();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                await R_HTTPClientWrapper.R_APIRequestObject<GLM00421DTO, GLM00421DTO>(
                    _RequestServiceEndPoint,
                    nameof(IGLM00420.SaveAllocationCenterList),
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
        #endregion
    }
}
