using APT00200COMMON.DTOs.APT00230;
using APT00200COMMON;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Text;
using APT00200COMMON.DTOs.APT00231;
using APT00200COMMON.DTOs.APT00210;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using System.Threading.Tasks;

namespace APT00200MODEL
{
    internal class APT00231Model : R_BusinessObjectServiceClientBase<APT00231ParameterDTO>, IAPT00231
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlAP";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/APT00231";
        private const string DEFAULT_MODULE = "AP";

        public APT00231Model(string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        public IAsyncEnumerable<APT00231DTO> GetAdditionalList()
        {
            throw new NotImplementedException();
        }
        public async Task<APT00231ResultDTO> GetAdditionalListStreamAsync()
        {
            R_Exception loEx = new R_Exception();
            List<APT00231DTO> loResult = null;
            APT00231ResultDTO loRtn = new APT00231ResultDTO();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<APT00231DTO>(
                    _RequestServiceEndPoint,
                    nameof(IAPT00231.GetAdditionalList),
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
