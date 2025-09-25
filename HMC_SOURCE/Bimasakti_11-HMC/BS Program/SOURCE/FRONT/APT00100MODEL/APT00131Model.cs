using APT00100COMMON.DTOs.APT00130;
using APT00100COMMON;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Text;
using APT00100COMMON.DTOs.APT00131;
using APT00100COMMON.DTOs.APT00110;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using System.Threading.Tasks;

namespace APT00100MODEL
{
    internal class APT00131Model : R_BusinessObjectServiceClientBase<APT00131ParameterDTO>, IAPT00131
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlAP";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/APT00131";
        private const string DEFAULT_MODULE = "AP";

        public APT00131Model(string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        public IAsyncEnumerable<APT00131DTO> GetAdditionalList()
        {
            throw new NotImplementedException();
        }
        public async Task<APT00131ResultDTO> GetAdditionalListStreamAsync()
        {
            R_Exception loEx = new R_Exception();
            List<APT00131DTO> loResult = null;
            APT00131ResultDTO loRtn = new APT00131ResultDTO();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<APT00131DTO>(
                    _RequestServiceEndPoint,
                    nameof(IAPT00131.GetAdditionalList),
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
