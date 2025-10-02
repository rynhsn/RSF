using PMB01800Common.DTOs;
using PMB01800COMMON;
using PMB01800COMMON.DTOs;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PMB01800MODEL
{
    public class PMB01800Model : R_BusinessObjectServiceClientBase<PMB01800GetDepositListDTO>, IPMB01800
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlPM";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/PMB01800";
        private const string DEFAULT_MODULE = "PM";

        public PMB01800Model(
            string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            string pcModuleName = DEFAULT_MODULE,
            bool plSendWithContext = true,
            bool plSendWithToken = true) : base(pcHttpClientName, pcRequestServiceEndPoint, pcModuleName,
            plSendWithContext,
            plSendWithToken)
        {
        }

        public IAsyncEnumerable<PMB01800GetDepositListDTO> PMB01800GetDepositListStream()
        {
            throw new NotImplementedException();
        }

        public async Task<List<T>> PMB01800GetDepositListStreamAsync<T>()
        {
            var loEx = new R_Exception();
            List<T> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<T>(
                    _RequestServiceEndPoint,
                    nameof(IPMB01800.PMB01800GetDepositListStream),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken,
                    null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public async Task<PMB01800ListBase<PMB01800PropertyDTO>> PMB01800GetPropertyList()
        {
            var loEx = new R_Exception();
            var loResult = new PMB01800ListBase<PMB01800PropertyDTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<PMB01800ListBase<PMB01800PropertyDTO>>(
                    _RequestServiceEndPoint,
                    nameof(IPMB01800.PMB01800GetPropertyList),
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
    }
}
