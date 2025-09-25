using System;
using System.Threading.Tasks;
using Lookup_GLCOMMON;
using Lookup_GLCOMMON.DTO;
using Lookup_GLCOMMON.DTOs;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;

namespace Lookup_GLModel
{
    public class PublicLookupModel : R_BusinessObjectServiceClientBase<GLL00100DTO>, IPublicLookupGL
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlGL";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/PublicLookupGL";
        private const string DEFAULT_MODULE = "GL";

        public PublicLookupModel(
            string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            string pcModule = DEFAULT_MODULE,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, pcModule, plSendWithContext, plSendWithToken)
        {
        }

        #region GLL00100

        public GLLGenericList<GLL00100DTO> GLL00100ReferenceNoLookUp(GLL00100ParameterDTO poParameter)
        {
            throw new NotImplementedException();
        }

        public async Task<GLLGenericList<GLL00100DTO>> GLL00100ReferenceNoLookUpAsync(GLL00100ParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            var loResult = new GLLGenericList<GLL00100DTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper
                    .R_APIRequestObject<GLLGenericList<GLL00100DTO>, GLL00100ParameterDTO>(
                        _RequestServiceEndPoint,
                        nameof(IPublicLookupGL.GLL00100ReferenceNoLookUp),
                        poParameter,
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
    }
}