using APT00100COMMON.DTOs.APT00110;
using APT00100COMMON;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Text;
using APT00100COMMON.DTOs.APT00121;
using APT00100COMMON.DTOs.APT00111;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using System.Threading.Tasks;

namespace APT00100MODEL
{
    public class APT00121Model : R_BusinessObjectServiceClientBase<APT00121ParameterDTO>, IAPT00121
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlAP";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/APT00121";
        private const string DEFAULT_MODULE = "AP";

        public APT00121Model(string pcHttpClientName = DEFAULT_HTTP_NAME,
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
                    nameof(IAPT00121.GetProductTypeList),
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


        public APT00121ResultDTO RefreshInvoiceItem(APT00121RefreshParameterDTO poParameter)
        {
            throw new NotImplementedException();
        }
        public async Task<APT00121ResultDTO> RefreshInvoiceItemAsync(APT00121RefreshParameterDTO poParameter)
        {
            R_Exception loEx = new R_Exception();
            APT00121ResultDTO loRtn = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<APT00121ResultDTO, APT00121RefreshParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IAPT00121.RefreshInvoiceItem),
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
