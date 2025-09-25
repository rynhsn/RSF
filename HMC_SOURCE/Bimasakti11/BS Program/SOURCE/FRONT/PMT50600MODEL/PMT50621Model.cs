using PMT50600COMMON.DTOs.PMT50610;
using PMT50600COMMON;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Text;
using PMT50600COMMON.DTOs.PMT50621;
using PMT50600COMMON.DTOs.PMT50611;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using System.Threading.Tasks;

namespace PMT50600MODEL
{
    public class PMT50621Model : R_BusinessObjectServiceClientBase<PMT50621ParameterDTO>, IPMT50621
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlPM";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/PMT50621";
        private const string DEFAULT_MODULE = "PM";

        public PMT50621Model(string pcHttpClientName = DEFAULT_HTTP_NAME,
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
                    nameof(IPMT50621.GetProductTypeList),
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


        public PMT50621ResultDTO RefreshInvoiceItem(PMT50621RefreshParameterDTO poParameter)
        {
            throw new NotImplementedException();
        }
        public async Task<PMT50621ResultDTO> RefreshInvoiceItemAsync(PMT50621RefreshParameterDTO poParameter)
        {
            R_Exception loEx = new R_Exception();
            PMT50621ResultDTO loRtn = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<PMT50621ResultDTO, PMT50621RefreshParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT50621.RefreshInvoiceItem),
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
