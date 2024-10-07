using System;
using System.Threading.Tasks;
using Lookup_APCOMMON;
using Lookup_APCOMMON.DTOs;
using Lookup_APCOMMON.DTOs.APL00100;
using Lookup_APCOMMON.DTOs.APL00110;
using Lookup_APCOMMON.DTOs.APL00200;
using Lookup_APCOMMON.DTOs.APL00300;
using Lookup_APCOMMON.DTOs.APL00400;
using Lookup_APCOMMON.DTOs.APL00500;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;

namespace Lookup_APModel
{
    public class PublicAPLookupRecordModel : R_BusinessObjectServiceClientBase<APL00100DTO>, IPublicAPLookupRecord
    {
        private const string DEFAULT_HTTP = "R_DefaultServiceUrlAP";
        private const string DEFAULT_ENDPOINT = "api/PublicAPLookupRecord";
        private const string DEFAULT_MODULE = "AP";

        public PublicAPLookupRecordModel(
            string pcHttpClientName = DEFAULT_HTTP,
            string pcRequestServiceEndPoint = DEFAULT_ENDPOINT,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        public async Task<APL00100DTO> APL00100GetRecordAsync(APL00100ParameterDTO poEntity)
        {
            var loEx = new R_Exception();
            APL00100DTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                var loTempResult = await R_HTTPClientWrapper
                    .R_APIRequestObject<APLGenericRecord<APL00100DTO>, APL00100ParameterDTO>(
                        _RequestServiceEndPoint,
                        nameof(IPublicAPLookupRecord.APL00100GetRecord),
                        poEntity,
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
        public async Task<APL00110DTO> APL00110GetRecordAsync(APL00110ParameterDTO poEntity)
        {
            var loEx = new R_Exception();
            APL00110DTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                var loTempResult = await R_HTTPClientWrapper
                    .R_APIRequestObject<APLGenericRecord<APL00110DTO>, APL00110ParameterDTO>(
                        _RequestServiceEndPoint,
                        nameof(IPublicAPLookupRecord.APL00110GetRecord),
                        poEntity,
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

        public async Task<APL00200DTO> APL00200GetRecordAsync(APL00200ParameterDTO poEntity)
        {
            var loEx = new R_Exception();
            APL00200DTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                var loTempResult = await R_HTTPClientWrapper
                    .R_APIRequestObject<APLGenericRecord<APL00200DTO>, APL00200ParameterDTO>(
                        _RequestServiceEndPoint,
                        nameof(IPublicAPLookupRecord.APL00200GetRecord),
                        poEntity,
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

        public async Task<APL00300DTO> APL00300GetRecordAsync(APL00300ParameterDTO poEntity)
        {
            var loEx = new R_Exception();
            APL00300DTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                var loTempResult = await R_HTTPClientWrapper
                    .R_APIRequestObject<APLGenericRecord<APL00300DTO>, APL00300ParameterDTO>(
                        _RequestServiceEndPoint,
                        nameof(IPublicAPLookupRecord.APL00300GetRecord),
                        poEntity,
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
        public async Task<APL00400DTO> APL00400GetRecordAsync(APL00400ParameterDTO poEntity)
        {
            var loEx = new R_Exception();
            APL00400DTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                var loTempResult = await R_HTTPClientWrapper
                    .R_APIRequestObject<APLGenericRecord<APL00400DTO>, APL00400ParameterDTO>(
                        _RequestServiceEndPoint,
                        nameof(IPublicAPLookupRecord.APL00400GetRecord),
                        poEntity,
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

        public async Task<APL00500DTO> APL00500GetRecordAsync(APL00500ParameterDTO poEntity)
        {
            var loEx = new R_Exception();
            APL00500DTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                var loTempResult = await R_HTTPClientWrapper
                    .R_APIRequestObject<APLGenericRecord<APL00500DTO>, APL00500ParameterDTO>(
                        _RequestServiceEndPoint,
                        nameof(IPublicAPLookupRecord.APL00500GetRecord),
                        poEntity,
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


        public APLGenericRecord<APL00100DTO> APL00100GetRecord(APL00100ParameterDTO poEntity)
        {
            throw new NotImplementedException();
        }

        public APLGenericRecord<APL00200DTO> APL00200GetRecord(APL00200ParameterDTO poEntity)
        {
            throw new NotImplementedException();
        }

        public APLGenericRecord<APL00300DTO> APL00300GetRecord(APL00300ParameterDTO poEntity)
        {
            throw new NotImplementedException();
        }

        public APLGenericRecord<APL00110DTO> APL00110GetRecord(APL00110ParameterDTO poEntity)
        {
            throw new System.NotImplementedException();
        }

        public APLGenericRecord<APL00400DTO> APL00400GetRecord(APL00400ParameterDTO poEntity)
        {
            throw new System.NotImplementedException();
        }

        public APLGenericRecord<APL00500DTO> APL00500GetRecord(APL00500ParameterDTO poEntity)
        {
            throw new NotImplementedException();
        }
    }
}