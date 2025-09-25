using APT00100COMMON.DTOs.APT00121;
using APT00100COMMON;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Text;
using APT00100COMMON.DTOs.APT00120;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using System.Threading.Tasks;

namespace APT00100MODEL
{
    public class APT00120Model : R_BusinessObjectServiceClientBase<OnCloseProcessParameterDTO>, IAPT00120
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlAP";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/APT00120";
        private const string DEFAULT_MODULE = "AP";

        public APT00120Model(string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        public OnCloseProcessResultDTO CloseFormProcess(OnCloseProcessParameterDTO poParameter)
        {
            throw new NotImplementedException();
        }
        public async Task CloseFormProcessAsync(OnCloseProcessParameterDTO poParameter)
        {
            R_Exception loEx = new R_Exception();
            OnCloseProcessResultDTO loRtn = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<OnCloseProcessResultDTO, OnCloseProcessParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IAPT00120.CloseFormProcess),
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
        }
    }
}
