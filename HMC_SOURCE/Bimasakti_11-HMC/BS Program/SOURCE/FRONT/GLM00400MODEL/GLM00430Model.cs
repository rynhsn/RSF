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
    public class GLM00430Model : R_BusinessObjectServiceClientBase<GLM00430DTO>, IGLM00430
    {
        private const string DEFAULT_HTTP = "R_DefaultServiceUrlGL";
        private const string DEFAULT_ENDPOINT = "api/GLM00430";
        private const string DEFAULT_MODULE = "GL";

        public GLM00430Model(
            string pcHttpClientName = DEFAULT_HTTP,
            string pcRequestServiceEndPoint = DEFAULT_ENDPOINT,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        #region GetAllocationAccount
        public IAsyncEnumerable<GLM00431DTO> GetAllocationAccountList()
        {
            throw new NotImplementedException();
        }

        public async Task<List<GLM00431DTO>> GetAllocationAccountListAsync(GLM00431DTO poParam)
        {
            var loEx = new R_Exception();
            List<GLM00431DTO> loResult = null;

            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstant.CREC_ID_ALLOCATION_ID, poParam.CREC_ID_ALLOCATION_ID);

                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GLM00431DTO>(
                    _RequestServiceEndPoint,
                    nameof(IGLM00430.GetAllocationAccountList),
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

        #region GetSourceAllocationAccount
        public IAsyncEnumerable<GLM00430DTO> GetSourceAllocationAccountList()
        {
            throw new NotImplementedException();
        }

        public async Task<List<GLM00430DTO>> GetSourceAllocationAccountListAsync()
        {
            var loEx = new R_Exception();
            List<GLM00430DTO> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GLM00430DTO>(
                    _RequestServiceEndPoint,
                    nameof(IGLM00430.GetSourceAllocationAccountList),
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

        #region SaveAllocationAccount
        public GLM00431DTO SaveAllocationAccountList(GLM00431DTO poParam)
        {
            throw new NotImplementedException();
        }

        public async Task SaveAllocationAccountListAsync(GLM00431DTO poParam)
        {
            var loEx = new R_Exception();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                await R_HTTPClientWrapper.R_APIRequestObject<GLM00431DTO, GLM00431DTO>(
                    _RequestServiceEndPoint,
                    nameof(IGLM00430.SaveAllocationAccountList),
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
