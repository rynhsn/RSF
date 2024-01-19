using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LMM02500Common;
using LMM02500Common.DTOs;
using R_APIClient;
using R_APICommonDTO;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;

namespace LMM02500Model
{
    public class LMM02500ListModel : R_BusinessObjectServiceClientBase<LMM02500PropertyDTO>, ILMM02500List
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlLM";
        private const string DEFAULT_ENDPOINT_NAME = "api/LMM02500List";
        private const string DEFAULT_MODULE = "LM";

        public LMM02500ListModel(
            string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_ENDPOINT_NAME,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        public IAsyncEnumerable<LMM02500TenantGroupDTO> LMM02500GetTenantGroupList()
        {
            throw new NotImplementedException();
        }
        
        public async Task<List<T>> GetListStreamAsync<T>(string pcNameOf)
        {
            var loEx = new R_Exception();
            List<T> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<T>(
                    _RequestServiceEndPoint,
                    pcNameOf,
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken,
                    null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        
        public async Task<T> GetAsync<T>(string pcNameOf) where T : R_APIResultBaseDTO
        {
            var loEx = new R_Exception();
            var loResult = default(T);

            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<T>(
                    _RequestServiceEndPoint,
                    pcNameOf,
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
        
        public async Task<T> GetAsync<T, T1>(string pcNameOf, T1 poParameter) where T : R_APIResultBaseDTO
        {
            var loEx = new R_Exception();
            var loResult = default(T);

            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<T, T1>(
                    _RequestServiceEndPoint,
                    pcNameOf,
                    poParameter,
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