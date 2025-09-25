using APT00100COMMON;
using APT00100COMMON.DTOs.APT00111;
using APT00100COMMON.DTOs.APT00130;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace APT00100MODEL
{
    public class APT00130Model : R_BusinessObjectServiceClientBase<APT00130ParameterDTO>, IAPT00130
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlAP";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/APT00130";
        private const string DEFAULT_MODULE = "AP";

        public APT00130Model(string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }
        public APT00130ResultDTO GetSummaryInfo(GetSummaryParameterDTO poParameter)
        {
            throw new NotImplementedException();
        }
        public async Task<APT00130ResultDTO> GetSummaryInfoAsync(GetSummaryParameterDTO poParameter)
        {
            R_Exception loEx = new R_Exception();
            APT00130ResultDTO loRtn = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<APT00130ResultDTO, GetSummaryParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IAPT00130.GetSummaryInfo),
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
