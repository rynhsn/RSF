using Lookup_GLCOMMON;
using R_BusinessObjectFront;
using System;
using Lookup_GLCOMMON.DTOs;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using System.Threading.Tasks;
using Lookup_GLCOMMON.DTOs.GLL00100;
using Lookup_GLCOMMON.DTOs.GLL00110;

namespace Lookup_GLModel
{
    public class PublicLookupGetRecordGLModel : R_BusinessObjectServiceClientBase<GLL00100DTO>, IPublicLookupGetRecordGL
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlGL";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/PublicLookupGLGetRecord";
        private const string DEFAULT_MODULE = "GL";

        public PublicLookupGetRecordGLModel(
            string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            string pcModule = DEFAULT_MODULE,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, pcModule, plSendWithContext, plSendWithToken)
        {
        }



        #region GLL00100GetRecord

        public async Task<GLL00100DTO> GLL00100ReferenceNoLookUpAsync(GLL00100ParameterGetRecordDTO poParam)
        {

            var loEx = new R_Exception();
            GLL00100DTO? loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<GLLGenericRecord<GLL00100DTO>, GLL00100ParameterGetRecordDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicLookupGetRecordGL.GLL00100ReferenceNoLookUp),
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

            return loResult!;
        }


        #endregion

        #region GLL00110GetRecord

        public async Task<GLL00110DTO> GLL00100ReferenceNoLookUpByPeriodAsync(GLL00110ParameterGetRecordDTO poParam)
        {

            var loEx = new R_Exception();
            GLL00110DTO? loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<GLLGenericRecord<GLL00110DTO>, GLL00110ParameterGetRecordDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicLookupGetRecordGL.GLL00100ReferenceNoLookUpByPeriod),
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

            return loResult!;
        }

        #endregion

        #region Not Used!

        public GLLGenericRecord<GLL00100DTO> GLL00100ReferenceNoLookUp(GLL00100ParameterGetRecordDTO poParameter)
        {
            throw new NotImplementedException();
        }

        public GLLGenericRecord<GLL00110DTO> GLL00100ReferenceNoLookUpByPeriod(GLL00110ParameterGetRecordDTO poParameter)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
