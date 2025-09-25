using PMT02100COMMON.DTOs.PMT02100;
using PMT02100COMMON;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Text;
using PMT02100COMMON.DTOs.PMT02110;
using PMT02100COMMON.DTOs.Utility;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using System.Threading.Tasks;

namespace PMT02100MODEL
{
    public class PMT02110Model : R_BusinessObjectServiceClientBase<PMT02110ConfirmParameterDTO>, IPMT02110
    {
        private const string DEFAULT_HTTP = "R_DefaultServiceUrlPM";
        private const string DEFAULT_ENDPOINT = "api/PMT02110";
        private const string DEFAULT_MODULE = "PM";

        public PMT02110Model(
            string pcHttpClientName = DEFAULT_HTTP,
            string pcRequestServiceEndPoint = DEFAULT_ENDPOINT,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        public async Task ConfirmScheduleProcessAsync(PMT02110ConfirmParameterDTO poParameter)
        {
            R_Exception loEx = new R_Exception();
            PMT02100VoidResultHelperDTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<PMT02100VoidResultHelperDTO, PMT02110ConfirmParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT02110.ConfirmScheduleProcess),
                    poParameter,
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task<PMT02110TenantResultDTO> GetTenantDetailAsync(PMT02110TenantParameterDTO poParameter)
        {
            R_Exception loEx = new R_Exception();
            PMT02110TenantResultDTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<PMT02110TenantResultDTO, PMT02110TenantParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT02110.GetTenantDetail),
                    poParameter,
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        public PMT02100VoidResultHelperDTO ConfirmScheduleProcess(PMT02110ConfirmParameterDTO poParam)
        {
            throw new NotImplementedException();
        }

        public PMT02110TenantResultDTO GetTenantDetail(PMT02110TenantParameterDTO poParam)
        {
            throw new NotImplementedException();
        }
    }
}
