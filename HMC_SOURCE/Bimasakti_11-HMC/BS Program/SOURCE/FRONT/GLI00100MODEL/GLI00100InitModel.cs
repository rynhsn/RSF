using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GLI00100Common;
using GLI00100Common.DTOs;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;

namespace GLI00100Model
{
    public class GLI00100InitModel : R_BusinessObjectServiceClientBase<GLI00100AccountGridDTO>, IGLI00100Init
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlGL";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/GLI00100Init";
        private const string DEFAULT_MODULE = "gl";

        public GLI00100InitModel(
            string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            string pcModuleName = DEFAULT_MODULE,
            bool plSendWithContext = true,
            bool plSendWithToken = true) : base(pcHttpClientName, pcRequestServiceEndPoint, pcModuleName,
            plSendWithContext,
            plSendWithToken)
        {
        }

        #region not implemented members

        public GLI00100GSMCompanyDTO GLI00100GetGSMCompany()
        {
            throw new NotImplementedException();
        }

        public GLI00100GLSystemParamDTO GLI00100GetGLSystemParam()
        {
            throw new NotImplementedException();
        }

        public GLI00100GSMPeriodDTO GLI00100GetGSMPeriod()
        {
            throw new NotImplementedException();
        }

        public GLI00100PeriodCountDTO GLI00100GetPeriodCount(GLI00100YearParamsDTO poParams)
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<GLI00100AccountGridDTO> GLI00100GetGLAccountListStream()
        {
            throw new NotImplementedException();
        }

        #endregion


        public async Task<GLI00100GSMCompanyDTO> GLI00100GetGSMCompanyModel()
        {
            var loEx = new R_Exception();
            GLI00100GSMCompanyDTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<GLI00100GSMCompanyDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGLI00100Init.GLI00100GetGSMCompany),
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
        public async Task<GLI00100GLSystemParamDTO> GLI00100GetGLSystemParamModel()
        {
            var loEx = new R_Exception();
            GLI00100GLSystemParamDTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<GLI00100GLSystemParamDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGLI00100Init.GLI00100GetGLSystemParam),
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

        public async Task<GLI00100GSMPeriodDTO> GLI00100GetGSMPeriodModel()
        {
            var loEx = new R_Exception();
            GLI00100GSMPeriodDTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<GLI00100GSMPeriodDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGLI00100Init.GLI00100GetGSMPeriod),
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

        public async Task<GLI00100PeriodCountDTO> GLI00100GetPeriodCountModel(GLI00100YearParamsDTO poParams)
        {
            var loEx = new R_Exception();
            GLI00100PeriodCountDTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<GLI00100PeriodCountDTO, GLI00100YearParamsDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGLI00100Init.GLI00100GetPeriodCount),
                    poParams,
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

        public async Task<List<GLI00100AccountGridDTO>> GLI00100GetGLAccountListStreamModel()
        {
            var loEx = new R_Exception();
            List<GLI00100AccountGridDTO> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GLI00100AccountGridDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGLI00100Init.GLI00100GetGLAccountListStream),
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
    }
}
