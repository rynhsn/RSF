using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Lookup_GLCOMMON;
using Lookup_GLCOMMON.DTOs;
using Lookup_GLCOMMON.DTOs.GLL00100;
using Lookup_GLCOMMON.DTOs.GLL00110;
using R_APIClient;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;

namespace Lookup_GLModel
{
    public class PublicLookupGLModel : R_BusinessObjectServiceClientBase<GLL00100DTO>, IPublicLookupGL
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlGL";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/PublicLookupGL";
        private const string DEFAULT_MODULE = "GL";

        public PublicLookupGLModel(
            string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            string pcModule = DEFAULT_MODULE,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, pcModule, plSendWithContext, plSendWithToken)
        {
        }


        #region GLL00100 

        public async Task<GLLGenericList<GLL00100DTO>> GLL00100ReferenceNoLookUpAsync(GLL00100ParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            GLLGenericList<GLL00100DTO> loResult = new GLLGenericList<GLL00100DTO>();
            try
            {
                R_FrontContext.R_SetStreamingContext(GLL00100ContextDTO.CTRANS_CODE, poParameter.CTRANS_CODE);
                R_FrontContext.R_SetStreamingContext(GLL00100ContextDTO.CFROM_DEPT_CODE, poParameter.CFROM_DEPT_CODE);
                R_FrontContext.R_SetStreamingContext(GLL00100ContextDTO.CTO_DEPT_CODE, poParameter.CTO_DEPT_CODE);
                R_FrontContext.R_SetStreamingContext(GLL00100ContextDTO.CFROM_DATE, poParameter.CFROM_DATE);
                R_FrontContext.R_SetStreamingContext(GLL00100ContextDTO.CTO_DATE, poParameter.CTO_DATE);

                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GLL00100DTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicLookupGL.GLL00100ReferenceNoLookUp),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loResult.Data = loTempResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loResult;
        }
        #endregion

        #region GLL00110 

        public async Task<GLLGenericList<GLL00110DTO>> GLL00110ReferenceNoLookUpByPeriodAsync(GLL00110ParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            GLLGenericList<GLL00110DTO> loResult = new GLLGenericList<GLL00110DTO>();
            try
            {
                R_FrontContext.R_SetStreamingContext(GLL00110ContextDTO.CTRANS_CODE, poParameter.CTRANS_CODE);
                R_FrontContext.R_SetStreamingContext(GLL00110ContextDTO.CFROM_DEPT_CODE, poParameter.CFROM_DEPT_CODE);
                R_FrontContext.R_SetStreamingContext(GLL00110ContextDTO.CTO_DEPT_CODE, poParameter.CTO_DEPT_CODE);
                R_FrontContext.R_SetStreamingContext(GLL00110ContextDTO.CFROM_DATE, poParameter.CFROM_DATE);
                R_FrontContext.R_SetStreamingContext(GLL00110ContextDTO.CTO_DATE, poParameter.CTO_DATE);

                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GLL00110DTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicLookupGL.GLL00110ReferenceNoLookUpByPeriod),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loResult.Data = loTempResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loResult;
        }
        #endregion


        #region Not Used!

        public IAsyncEnumerable<GLL00100DTO> GLL00100ReferenceNoLookUp()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<GLL00110DTO> GLL00110ReferenceNoLookUpByPeriod()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}