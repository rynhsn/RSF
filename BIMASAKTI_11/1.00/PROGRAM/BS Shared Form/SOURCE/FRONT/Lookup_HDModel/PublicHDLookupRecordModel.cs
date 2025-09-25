
using System;
using System.Threading.Tasks;
using Lookup_HDCOMMON;
using Lookup_HDCOMMON.DTOs;
using Lookup_HDCOMMON.DTOs.HDL00100;
using Lookup_HDCOMMON.DTOs.HDL00200;
using Lookup_HDCOMMON.DTOs.HDL00300;
using Lookup_HDCOMMON.DTOs.HDL00400;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;

namespace Lookup_HDModel
{
    public class PublicHDLookupRecordModel : R_BusinessObjectServiceClientBase<HDL00100DTO>, IPublicHDLookupRecord
    {
        private const string DEFAULT_HTTP = "R_DefaultServiceUrlHD";
        private const string DEFAULT_ENDPOINT = "api/PublicLookupHDRecord";
        private const string DEFAULT_MODULE = "HD";

        public PublicHDLookupRecordModel(
            string pcHttpClientName = DEFAULT_HTTP,
            string pcRequestServiceEndPoint = DEFAULT_ENDPOINT,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }
        public async Task<HDL00100DTO> HDL00100GetRecordAsync(HDL00100ParameterDTO poEntity)
        {
            var loEx = new R_Exception();
            HDL00100DTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                var loTempResult = await R_HTTPClientWrapper
                    .R_APIRequestObject<HDLGenericRecord<HDL00100DTO>, HDL00100ParameterDTO>(
                        _RequestServiceEndPoint,
                        nameof(IPublicHDLookupRecord.HDL00100GetRecord),
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
        
        public async Task<HDL00200DTO> HDL00200GetRecordAsync(HDL00200ParameterDTO poEntity)
        {
            var loEx = new R_Exception();
            HDL00200DTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                var loTempResult = await R_HTTPClientWrapper
                    .R_APIRequestObject<HDLGenericRecord<HDL00200DTO>, HDL00200ParameterDTO>(
                        _RequestServiceEndPoint,
                        nameof(IPublicHDLookupRecord.HDL00200GetRecord),
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
        
        public async Task<HDL00300DTO> HDL00300GetRecordAsync(HDL00300ParameterDTO poEntity)
        {
            var loEx = new R_Exception();
            HDL00300DTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                var loTempResult = await R_HTTPClientWrapper
                    .R_APIRequestObject<HDLGenericRecord<HDL00300DTO>, HDL00300ParameterDTO>(
                        _RequestServiceEndPoint,
                        nameof(IPublicHDLookupRecord.HDL00300GetRecord),
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
        
        public async Task<HDL00400DTO> HDL00400GetRecordAsync(HDL00400ParameterDTO poEntity)
        {
            var loEx = new R_Exception();
            HDL00400DTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                var loTempResult = await R_HTTPClientWrapper
                    .R_APIRequestObject<HDLGenericRecord<HDL00400DTO>, HDL00400ParameterDTO>(
                        _RequestServiceEndPoint,
                        nameof(IPublicHDLookupRecord.HDL00400GetRecord),
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
        
        public HDLGenericRecord<HDL00100DTO> HDL00100GetRecord(HDL00100ParameterDTO poEntity)
        {
            throw new System.NotImplementedException();
        }
        
        public HDLGenericRecord<HDL00200DTO> HDL00200GetRecord(HDL00200ParameterDTO poEntity)
        {
            throw new System.NotImplementedException();
        }

        public HDLGenericRecord<HDL00300DTO> HDL00300GetRecord(HDL00300ParameterDTO poEntity)
        {
            throw new NotImplementedException();
        }

        public HDLGenericRecord<HDL00400DTO> HDL00400GetRecord(HDL00400ParameterDTO poEntity)
        {
            throw new NotImplementedException();
        }
    }
}