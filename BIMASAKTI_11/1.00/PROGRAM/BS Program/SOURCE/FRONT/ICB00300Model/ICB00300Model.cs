using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ICB00300Common;
using ICB00300Common.DTOs;
using R_APIClient;
using R_APICommonDTO;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;

namespace ICB00300Model
{
    public class ICB00300Model : R_BusinessObjectServiceClientBase<ICB00300ProductDTO>, IICB00300
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlIC";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/ICB00300";
        private const string DEFAULT_MODULE = "ic";

        public ICB00300Model(
            string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            string pcModuleName = DEFAULT_MODULE,
            bool plSendWithContext = true,
            bool plSendWithToken = true) : base(pcHttpClientName, pcRequestServiceEndPoint, pcModuleName,
            plSendWithContext,
            plSendWithToken)
        {
        }

        public ICB00300ListDTO<ICB00300PropertyDTO> ICB00300GetPropertyList()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<ICB00300ProductDTO> ICB00300GetProductListStream()
        {
            throw new NotImplementedException();
        }
        
        
        //Untuk fetch data streaming dari controller  
        public async Task<List<T>> GetListStreamAsync<T>(string pcNameOf)
        {
            var loEx = new R_Exception();
            var loResult = new List<T>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<T>(
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
        
        //Untuk fetch data streaming dari controller dengan parameter
        public async Task<List<T>> GetListStreamAsync<T, T1>(string pcNameOf, T1 poParameter)
        {
            var loEx = new R_Exception();
            List<T> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<T, T1>(
                    _RequestServiceEndPoint,
                    pcNameOf,
                    poParameter,
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

        //Untuk fetch data object dari controller
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

        //Untuk fetch data object dari controller dengan parameter
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