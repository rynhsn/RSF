using PMT02600COMMON.DTOs.PMT02650;
using PMT02600COMMON;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PMT02600COMMON.DTOs;

namespace PMT02600MODEL
{
    public class PMT02650Model : R_BusinessObjectServiceClientBase<PMT02650DTO>, IPMT02650
    {
        private const string DEFAULT_HTTP = "R_DefaultServiceUrlPM";
        private const string DEFAULT_ENDPOINT = "api/PMT02650";
        private const string DEFAULT_MODULE = "PM";

        public PMT02650Model(
            string pcHttpClientName = DEFAULT_HTTP,
            string pcRequestServiceEndPoint = DEFAULT_ENDPOINT,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        public async Task<List<PMT02650DTO>> GetAgreementInvoicePlanStreamAsync(PMT02650ParameterDTO poEntity)
        {
            var loEx = new R_Exception();
            List<PMT02650DTO> loResult = null;

            try
            {
                //Set Context
                R_FrontContext.R_SetStreamingContext(ContextConstant.PMT02650_AGREEMENT_INVOICE_PLAN_STREAMING_CONTEXT, poEntity);

                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMT02650DTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT02650.GetAgreementInvoicePlanStream),
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

        public async Task<List<PMT02650ChargeDTO>> GetAgreementChargeStreamAsync(PMT02650ParameterDTO poEntity)
        {
            var loEx = new R_Exception();
            List<PMT02650ChargeDTO> loResult = null;

            try
            {
                //Set Context
                R_FrontContext.R_SetStreamingContext(ContextConstant.PMT02650_AGREEMENT_CHARGE_STREAMING_CONTEXT, poEntity);

                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMT02650ChargeDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT02650.GetAgreementChargeStream),
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
        #region Not Implment
        public IAsyncEnumerable<PMT02650DTO> GetAgreementInvoicePlanStream()
        {
            throw new NotImplementedException();
        }
        public IAsyncEnumerable<PMT02650ChargeDTO> GetAgreementChargeStream()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
