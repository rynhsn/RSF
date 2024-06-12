using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PMI00500Common;
using PMI00500Common.DTOs;
using R_APIClient;
using R_APICommonDTO;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;

namespace PMI00500Model
{
    public class PMI00500Model : R_BusinessObjectServiceClientBase<PMI00500PropertyDTO>, IPMI00500
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlPM";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/PMI00500";
        private const string DEFAULT_MODULE = "pm";

        public PMI00500Model(
            string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            string pcModuleName = DEFAULT_MODULE,
            bool plSendWithContext = true,
            bool plSendWithToken = true) : base(pcHttpClientName, pcRequestServiceEndPoint, pcModuleName,
            plSendWithContext,
            plSendWithToken)
        {
        }

        public PMI00500ListDTO<PMI00500PropertyDTO> PMI00500GetPropertyList()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<PMI00500HeaderDTO> PMI00500GetHeaderListStream()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<PMI00500DTAgreementDTO> PMI00500GetDTAgreementListStream()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<PMI00500DTReminderDTO> PMI00500GetDTReminderListStream()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<PMI00500DTInvoiceDTO> PMI00500GetDTInvoiceListStream()
        {
            throw new NotImplementedException();
        }

        public async Task<List<T>> GetListStreamAsync<T>(string pcNameOf)
        {
            var loEx = new R_Exception();
            List<T> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<T>(
                    _RequestServiceEndPoint,
                    pcNameOf,
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

        public async Task<T> GetAsync<T>(string pcNameOf) where T : R_APIResultBaseDTO
        {
            var loEx = new R_Exception();
            var loResult = default(T);

            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<T>(
                    _RequestServiceEndPoint,
                    pcNameOf,
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