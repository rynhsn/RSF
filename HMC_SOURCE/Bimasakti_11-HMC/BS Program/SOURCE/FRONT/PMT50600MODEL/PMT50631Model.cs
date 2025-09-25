using PMT50600COMMON.DTOs.PMT50630;
using PMT50600COMMON;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Text;
using PMT50600COMMON.DTOs.PMT50631;
using PMT50600COMMON.DTOs.PMT50610;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using System.Threading.Tasks;

namespace PMT50600MODEL
{
    internal class PMT50631Model : R_BusinessObjectServiceClientBase<PMT50631ParameterDTO>, IPMT50631
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlPM";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/PMT50631";
        private const string DEFAULT_MODULE = "PM";

        public PMT50631Model(string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        public IAsyncEnumerable<PMT50631DTO> GetAdditionalList()
        {
            throw new NotImplementedException();
        }
        public async Task<PMT50631ResultDTO> GetAdditionalListStreamAsync()
        {
            R_Exception loEx = new R_Exception();
            List<PMT50631DTO> loResult = null;
            PMT50631ResultDTO loRtn = new PMT50631ResultDTO();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMT50631DTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT50631.GetAdditionalList),
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
