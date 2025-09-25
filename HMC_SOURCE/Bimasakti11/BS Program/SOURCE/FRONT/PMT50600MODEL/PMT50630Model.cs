using PMT50600COMMON;
using PMT50600COMMON.DTOs.PMT50611;
using PMT50600COMMON.DTOs.PMT50630;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PMT50600MODEL
{
    public class PMT50630Model : R_BusinessObjectServiceClientBase<PMT50630ParameterDTO>, IPMT50630
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlPM";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/PMT50630";
        private const string DEFAULT_MODULE = "PM";

        public PMT50630Model(string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }
        public PMT50630ResultDTO GetSummaryInfo(GetSummaryParameterDTO poParameter)
        {
            throw new NotImplementedException();
        }
        public async Task<PMT50630ResultDTO> GetSummaryInfoAsync(GetSummaryParameterDTO poParameter)
        {
            R_Exception loEx = new R_Exception();
            PMT50630ResultDTO loRtn = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<PMT50630ResultDTO, GetSummaryParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT50630.GetSummaryInfo),
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
