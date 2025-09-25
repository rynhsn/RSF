using System;
using System.Threading.Tasks;
using Lookup_ICCOMMON;
using Lookup_ICCOMMON.DTOs;
using Lookup_ICCOMMON.DTOs.ICL00100;
using Lookup_ICCOMMON.DTOs.ICL00200;
using Lookup_ICCOMMON.DTOs.ICL00300;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;

namespace Lookup_ICModel
{
    public class PublicICLookupRecordModel : R_BusinessObjectServiceClientBase<ICL00100DTO>, IPublicICLookupRecord
    {
        private const string DEFAULT_HTTP = "R_DefaultServiceUrlIC";
        private const string DEFAULT_ENDPOINT = "api/PublicLookupICRecord";
        private const string DEFAULT_MODULE = "IC";

        public PublicICLookupRecordModel(
            string pcHttpClientName = DEFAULT_HTTP,
            string pcRequestServiceEndPoint = DEFAULT_ENDPOINT,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        public async Task<ICL00100DTO> ICL00100GetRecordAsync(ICL00100ParameterDTO poEntity)
        {
            var loEx = new R_Exception();
            ICL00100DTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                var loTempResult = await R_HTTPClientWrapper
                    .R_APIRequestObject<ICLGenericRecord<ICL00100DTO>, ICL00100ParameterDTO>(
                        _RequestServiceEndPoint,
                        nameof(IPublicICLookupRecord.ICL00100GetRecord),
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
        public async Task<ICL00200DTO> ICL00200GetRecordAsync(ICL00200ParameterDTO poEntity)
        {
            var loEx = new R_Exception();
            ICL00200DTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                var loTempResult = await R_HTTPClientWrapper
                    .R_APIRequestObject<ICLGenericRecord<ICL00200DTO>, ICL00200ParameterDTO>(
                        _RequestServiceEndPoint,
                        nameof(IPublicICLookupRecord.ICL00200GetRecord),
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
        public async Task<ICL00300DTO> ICL00300GetRecordAsync(ICL00300ParameterDTO poEntity)
        {
            var loEx = new R_Exception();
            ICL00300DTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                var loTempResult = await R_HTTPClientWrapper
                    .R_APIRequestObject<ICLGenericRecord<ICL00300DTO>, ICL00300ParameterDTO>(
                        _RequestServiceEndPoint,
                        nameof(IPublicICLookupRecord.ICL00300GetRecord),
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
        public ICLGenericRecord<ICL00100DTO> ICL00100GetRecord(ICL00100ParameterDTO poEntity)
        {
            throw new System.NotImplementedException();
        }

        public ICLGenericRecord<ICL00200DTO> ICL00200GetRecord(ICL00200ParameterDTO poEntity)
        {
            throw new NotImplementedException();
        }

        public ICLGenericRecord<ICL00300DTO> ICL00300GetRecord(ICL00300ParameterDTO poEntity)
        {
            throw new NotImplementedException();
        }
    }
}