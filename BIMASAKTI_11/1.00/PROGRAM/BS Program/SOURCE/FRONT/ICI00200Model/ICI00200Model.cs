using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ICI00200Common;
using ICI00200Common.DTOs;
using ICI00200Common.Params;
using R_APIClient;
using R_APICommonDTO;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;

namespace ICI00200Model
{
    public class ICI00200Model : R_BusinessObjectServiceClientBase<ICI00200ProductDetailDTO>, IICI00200
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlIC";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/ICI00200";
        private const string DEFAULT_MODULE = "ic";

        public ICI00200Model(
            string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }
        
        //Untuk fetch data streaming dari controller
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

        //Untuk fetch data object dari controller
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
        

        public IAsyncEnumerable<ICI00200ProductDTO> ICI00200GetProductListStream()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<ICI00200DeptDTO> ICI00200GetDeptListStream()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<ICI00200WarehouseDTO> ICI00200GetWarehouseListStream()
        {
            throw new NotImplementedException();
        }

        public ICI00200SingleDTO<ICI00200ProductDetailDTO> ICI00200GetProductDetail(ICI00200DetailParam productDetailParam)
        {
            throw new NotImplementedException();
        }

        public ICI00200SingleDTO<ICI00200LastInfoDetailDTO> ICI00200GetLastInfoDetail(ICI00200DetailParam productDetailParam)
        {
            throw new NotImplementedException();
        }

        public ICI00200SingleDTO<ICI00200DeptWareDetailDTO> ICI00200GetDeptWareDetail(ICI00200DetailParam productDetailParam)
        {
            throw new NotImplementedException();
        }
    }
}