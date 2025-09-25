using APT00200COMMON.DTOs.APT00210;
using APT00200COMMON;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Text;
using APT00200COMMON.DTOs.APT00221;
using APT00200COMMON.DTOs.APT00211;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using System.Threading.Tasks;

namespace APT00200MODEL
{
    public class APT00221Model : R_BusinessObjectServiceClientBase<APT00221ParameterDTO>, IAPT00221
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlAP";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/APT00221";
        private const string DEFAULT_MODULE = "AP";

        public APT00221Model(string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        public IAsyncEnumerable<GetProductTypeDTO> GetProductTypeList()
        {
            throw new NotImplementedException();
        }
        public async Task<GetProductTypeResultDTO> GetProductTypeListStreamAsync()
        {
            R_Exception loEx = new R_Exception();
            List<GetProductTypeDTO> loResult = null;
            GetProductTypeResultDTO loRtn = new GetProductTypeResultDTO();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GetProductTypeDTO>(
                    _RequestServiceEndPoint,
                    nameof(IAPT00221.GetProductTypeList),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loRtn.Data = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

        EndBlock:
            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }


        public APT00221ResultDTO RefreshPurchaseReturnItem(APT00221RefreshParameterDTO poParameter)
        {
            throw new NotImplementedException();
        }
        public async Task<APT00221ResultDTO> RefreshPurchaseReturnItemAsync(APT00221RefreshParameterDTO poParameter)
        {
            R_Exception loEx = new R_Exception();
            APT00221ResultDTO loRtn = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<APT00221ResultDTO, APT00221RefreshParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IAPT00221.RefreshPurchaseReturnItem),
                    poParameter,
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

        EndBlock:
            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }
    }
}
