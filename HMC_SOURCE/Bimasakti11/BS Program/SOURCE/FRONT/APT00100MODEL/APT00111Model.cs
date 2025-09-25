using APT00100COMMON.DTOs.APT00110;
using APT00100COMMON;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Text;
using APT00100COMMON.DTOs.APT00111;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using System.Threading.Tasks;

namespace APT00100MODEL
{
    public class APT00111Model : R_BusinessObjectServiceClientBase<APT00111ListParameterDTO>, IAPT00111
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlAP";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/APT00111";
        private const string DEFAULT_MODULE = "AP";

        public APT00111Model(string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        public APT00111DetailResultDTO GetDetailInfo(APT00111DetailParameterDTO poParameter)
        {
            throw new NotImplementedException();
        }
        public async Task<APT00111DetailResultDTO> GetDetailInfoAsync(APT00111DetailParameterDTO poParameter)
        {
            R_Exception loEx = new R_Exception();
            APT00111DetailResultDTO loRtn = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<APT00111DetailResultDTO, APT00111DetailParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IAPT00111.GetDetailInfo),
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


        public APT00111HeaderResultDTO GetHeaderInfo(APT00111HeaderParameterDTO poParameter)
        {
            throw new NotImplementedException();
        }
        public async Task<APT00111HeaderResultDTO> GetHeaderInfoAsync(APT00111HeaderParameterDTO poParameter)
        {
            R_Exception loEx = new R_Exception();
            APT00111HeaderResultDTO loRtn = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<APT00111HeaderResultDTO, APT00111HeaderParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IAPT00111.GetHeaderInfo),
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


        public IAsyncEnumerable<APT00111ListDTO> GetInvoiceItemList()
        {
            throw new NotImplementedException();
        }
        public async Task<APT00111ListResultDTO> GetInvoiceItemListStreamAsync()
        {
            R_Exception loEx = new R_Exception();
            List<APT00111ListDTO> loResult = null;
            APT00111ListResultDTO loRtn = new APT00111ListResultDTO();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<APT00111ListDTO>(
                    _RequestServiceEndPoint,
                    nameof(IAPT00111.GetInvoiceItemList),
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
    }
}
