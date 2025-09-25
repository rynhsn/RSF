using System;
using System.Threading.Tasks;
using Lookup_CBCOMMON;
using Lookup_CBCOMMON.DTOs;
using Lookup_CBCOMMON.DTOs.CBL00100;
using Lookup_CBCOMMON.DTOs.CBL00200;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;

namespace Lookup_CBModel
{
    public class PublicCBLookupRecordModel : R_BusinessObjectServiceClientBase<CBL00100DTO>, IPublicCBLookupRecord
    {
        private const string DEFAULT_HTTP = "R_DefaultServiceUrlCB";
        private const string DEFAULT_ENDPOINT = "api/PublicCBLookupRecord";
        private const string DEFAULT_MODULE = "CB";

        public PublicCBLookupRecordModel(
            string pcHttpClientName = DEFAULT_HTTP,
            string pcRequestServiceEndPoint = DEFAULT_ENDPOINT,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }
        
        public async Task<CBL00100DTO> CBL00100GetRecordAsync(CBL00100ParameterDTO poEntity)
        {
            var loEx = new R_Exception();
            CBL00100DTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                var loTempResult = await R_HTTPClientWrapper
                    .R_APIRequestObject<CBLGenericRecord<CBL00100DTO>, CBL00100ParameterDTO>(
                        _RequestServiceEndPoint,
                        nameof(IPublicCBLookupRecord.CBL00100GetRecord),
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
        
        public async Task<CBL00200DTO> CBL00200GetRecordAsync(CBL00200ParameterDTO poEntity)
        {
            var loEx = new R_Exception();
            CBL00200DTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                var loTempResult = await R_HTTPClientWrapper
                    .R_APIRequestObject<CBLGenericRecord<CBL00200DTO>, CBL00200ParameterDTO>(
                        _RequestServiceEndPoint,
                        nameof(IPublicCBLookupRecord.CBL00200GetRecord),
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

        public CBLGenericRecord<CBL00100DTO> CBL00100GetRecord(CBL00100ParameterDTO poEntity)
        {
            throw new System.NotImplementedException();
        }

        public CBLGenericRecord<CBL00200DTO> CBL00200GetRecord(CBL00200ParameterDTO poEntity)
        {
            throw new NotImplementedException();
        }
    }
}