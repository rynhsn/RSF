using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PMT06500Common;
using PMT06500Common.DTOs;
using PMT06500Common.Params;
using R_APIClient;
using R_APICommonDTO;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using R_CommonFrontBackAPI;

namespace PMT06500Model
{
    public class PMT06500Model : R_BusinessObjectServiceClientBase<PMT06500InvoiceDTO>, IPMT06500
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlPM";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/PMT06500";
        private const string DEFAULT_MODULE = "pm";

        public PMT06500Model(
            string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        public IAsyncEnumerable<PMT06500AgreementDTO> PMT06500GetAgreementListStream()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<PMT06500OvtDTO> PMT06500GetOvertimeListStream()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<PMT06500ServiceDTO> PMT06500GetServiceListStream()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<PMT06500UnitDTO> PMT06500GetUnitListStream()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<PMT06500InvoiceDTO> PMT06500GetInvoiceListStream()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<PMT06500SummaryDTO> PMT06500GetSummaryListStream()
        {
            throw new NotImplementedException();
        }

        public PMT06500SingleDTO<PMT06500PropertyDTO> PMT06500ProcessSubmit(PMT06500ProcessSubmitParam poParameter)
        {
            throw new NotImplementedException();
        }

        public PMT06500SingleDTO<PMT06500InvoiceDTO> PMT06500SavingInvoice(SavingInvoiceParamDTO<PMT06500InvoiceDTO> poParameter)
        {
            throw new NotImplementedException();
        }


        //Untuk fetch data streaming dari controller  
        public async Task<List<T>> GetListStreamAsync<T>(string pcNameOf)
        {
            var loEx = new R_Exception();
            var loResult = new List<T>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<T>(
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

        //Untuk fetch data streaming dari controller dengan parameter
        public async Task<List<T>> GetListStreamAsync<T, T1>(string pcNameOf, T1 poParameter)
        {
            var loEx = new R_Exception();
            List<T> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<T, T1>(
                    _RequestServiceEndPoint,
                    pcNameOf,
                    poParameter,
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

        //Untuk fetch data object dari controller
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

        //Untuk fetch data object dari controller dengan parameter
        public async Task<T> GetAsync<T, T1>(string pcNameOf, T1 poParameter) where T : R_APIResultBaseDTO
        {
            var loEx = new R_Exception();
            var loResult = default(T);

            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<T, T1>(
                    _RequestServiceEndPoint,
                    pcNameOf,
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
    }
}