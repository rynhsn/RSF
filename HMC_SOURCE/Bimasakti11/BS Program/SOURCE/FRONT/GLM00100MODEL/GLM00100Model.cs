using System;
using System.Threading.Tasks;
using GLM00100Common;
using GLM00100Common.DTOs;
using R_APIClient;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;

namespace GLM00100Model
{
    public class GLM00100Model : R_BusinessObjectServiceClientBase<GLM00100DTO>, IGLM00100
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlGL";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/GLM00100";
        private const string DEFAULT_MODULE = "GL";

        public GLM00100Model(
            string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            string pcModule = DEFAULT_MODULE,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, pcModule, plSendWithContext, plSendWithToken)
        {
        }

        public async Task<GLM00100CreateSystemParameterResultDTO> CreateSystemParameterAsync(GLM00100ParameterCreateSystemParameterResultDTO poParameter)
        {
            var loEx = new R_Exception();
            var loResult = new GLM00100CreateSystemParameterResultDTO();

            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstantGLM00100CreateSystemParameter.CSTARTING_MM, poParameter.CSTARTING_MM);
                R_FrontContext.R_SetStreamingContext(ContextConstantGLM00100CreateSystemParameter.CSTARTING_YY, poParameter.CSTARTING_YY);

                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<GLM00100CreateSystemParameterResultDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGLM00100.CreateSystemParameter),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken
                );

                loResult = loTempResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
                
            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        public async Task<GLM00100GSMPeriod> GetStartingPeriodYearAsync()
        {
            var loEx = new R_Exception();
            var loResult = new GLM00100GSMPeriod();


            try
            {

                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<GLM00100GSMPeriod>(
                    _RequestServiceEndPoint,
                    nameof(IGLM00100.GetStartingPeriodYear),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken
                );

                loResult = loTempResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        public async Task<GLM00100ResultData> GetCheckerSystemParameterAsync()
        {
            var loEx = new R_Exception();
            var loResult = new GLM00100ResultData();


            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<GLM00100ResultData>(
                    _RequestServiceEndPoint,
                    nameof(IGLM00100.GetCheckerSystemParameter),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken
                );

                loResult = loTempResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        #region NotUsed
        public GLM00100CreateSystemParameterResultDTO CreateSystemParameter()
        {
            throw new System.NotImplementedException();
        }

        public GLM00100GSMPeriod GetStartingPeriodYear()
        {
            throw new System.NotImplementedException();
        }

        public GLM00100ResultData GetCheckerSystemParameter()
        {
            throw new System.NotImplementedException();
        }
        #endregion
    }
}