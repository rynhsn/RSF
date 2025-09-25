using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PMT01800COMMON;
using PMT01800COMMON.DTO;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;

namespace PMT01800Model.Model
{
    public class PMT01810Model : R_BusinessObjectServiceClientBase<PMT01810DTO>, IPMT01810
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlPM";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/PMT01810";
        private const string DEFAULT_MODULE = "PM";

        public PMT01810Model(
            string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            string pcModule = DEFAULT_MODULE,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, pcModule, plSendWithContext, plSendWithToken)
        {
        }

        public async Task<PMT01810ListDTO> GetPMT01810StreamAsync()
        {
            var loEx = new R_Exception();
            PMT01810ListDTO loResult = new PMT01810ListDTO();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTemp = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMT01810DTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT01810.GetPMT01810Stream),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);
                loResult.Data = loTemp;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public async Task<PMT01810ListDTO> GetPMT01810DetailStreamAsync()
        {
            var loEx = new R_Exception();
            PMT01810ListDTO loResult = new PMT01810ListDTO();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTemp = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMT01810DTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT01810.GetPMT01810DetailStream),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);
                loResult.Data = loTemp;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public IAsyncEnumerable<PMT01810DTO> GetPMT01810Stream()
        {
            throw new System.NotImplementedException();
        }

        public IAsyncEnumerable<PMT01810DTO> GetPMT01810DetailStream()
        {
            throw new System.NotImplementedException();
        }
    }
}