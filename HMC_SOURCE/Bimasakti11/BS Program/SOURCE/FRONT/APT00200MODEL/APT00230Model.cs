using APT00200COMMON;
using APT00200COMMON.DTOs.APT00211;
using APT00200COMMON.DTOs.APT00230;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace APT00200MODEL
{
    public class APT00230Model : R_BusinessObjectServiceClientBase<APT00230ParameterDTO>, IAPT00230
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlAP";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/APT00230";
        private const string DEFAULT_MODULE = "AP";

        public APT00230Model(string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }
        public APT00230ResultDTO GetSummaryInfo(GetSummaryParameterDTO poParameter)
        {
            throw new NotImplementedException();
        }
        public async Task<APT00230ResultDTO> GetSummaryInfoAsync(GetSummaryParameterDTO poParameter)
        {
            R_Exception loEx = new R_Exception();
            APT00230ResultDTO loRtn = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<APT00230ResultDTO, GetSummaryParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IAPT00230.GetSummaryInfo),
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
