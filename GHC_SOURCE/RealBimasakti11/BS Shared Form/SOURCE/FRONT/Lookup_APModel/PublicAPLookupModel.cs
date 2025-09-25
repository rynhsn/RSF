using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading.Tasks;
using Lookup_APCOMMON;
using Lookup_APCOMMON.DTOs;
using Lookup_APCOMMON.DTOs.APL00100;
using Lookup_APCOMMON.DTOs.APL00110;
using Lookup_APCOMMON.DTOs.APL00200;
using Lookup_APCOMMON.DTOs.APL00300;
using Lookup_APCOMMON.DTOs.APL00400;
using Lookup_APCOMMON.DTOs.APL00500;
using Lookup_APCOMMON.DTOs.APL00600;
using Lookup_APCOMMON.DTOs.APL00700;
using R_APIClient;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;

namespace Lookup_APModel
{
    public class PublicAPLookupModel : R_BusinessObjectServiceClientBase<APL00100DTO>, IPublicAPLookup
    {
        private const string DEFAULT_HTTP = "R_DefaultServiceUrlAP";
        private const string DEFAULT_ENDPOINT = "api/PublicAPLookup";
        private const string DEFAULT_MODULE = "AP";

        public PublicAPLookupModel(
            string pcHttpClientName = DEFAULT_HTTP,
            string pcRequestServiceEndPoint = DEFAULT_ENDPOINT,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        #region APL00100

        public IAsyncEnumerable<APL00100DTO> APL00100SupplierLookUp()
        {
            throw new NotImplementedException();
        }

        public async Task<List<APL00100DTO>> APL00100SupplierLookUpAsync(APL00100ParameterDTO poParam)
        {
            var loEx = new R_Exception();
            List<APL00100DTO> loResult = null;

            try
            {
                //context
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CPROPERTY_ID, poParam.CPROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CSEARCH_TEXT, poParam.CSEARCH_TEXT);

                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<APL00100DTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicAPLookup.APL00100SupplierLookUp),
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

        #endregion

        #region APL00110

        public IAsyncEnumerable<APL00110DTO> APL00110SupplierInfoLookUp()
        {
            throw new NotImplementedException();
        }

        public async Task<List<APL00110DTO>> APL00110SupplierInfoLookUpAsync(APL00110ParameterDTO poParam)
        {
            var loEx = new R_Exception();
            List<APL00110DTO> loResult = null;

            try
            {
                //context
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CPROPERTY_ID, poParam.CPROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CSUPPLIER_ID, poParam.CSUPPLIER_ID);

                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<APL00110DTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicAPLookup.APL00110SupplierInfoLookUp),
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

        #endregion

        #region APL00200

        public IAsyncEnumerable<APL00200DTO> APL00200ExpenditureLookUp()
        {
            throw new NotImplementedException();
        }

        public async Task<List<APL00200DTO>> APL00200ExpenditureLookUpAsync(APL00200ParameterDTO poParam)
        {
            var loEx = new R_Exception();
            List<APL00200DTO> loResult = null;

            try
            {
                //context
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CPROPERTY_ID, poParam.CPROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CTAXABLE_TYPE, poParam.CTAXABLE_TYPE);
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CACTIVE_TYPE, poParam.CACTIVE_TYPE);
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CCATEGORY_ID, poParam.CCATEGORY_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CTAX_DATE, poParam.CTAX_DATE);

                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<APL00200DTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicAPLookup.APL00200ExpenditureLookUp),
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

        #endregion

        #region APL00300

        public IAsyncEnumerable<APL00300DTO> APL00300ProductLookUp()
        {
            throw new NotImplementedException();
        }

        public async Task<List<APL00300DTO>> APL00300ProductLookUpAsync(APL00300ParameterDTO poParam)
        {
            var loEx = new R_Exception();
            List<APL00300DTO> loResult = null;

            try
            {
                //context
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CPROPERTY_ID, poParam.CPROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CTAXABLE_TYPE, poParam.CTAXABLE_TYPE);
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CACTIVE_TYPE, poParam.CACTIVE_TYPE);
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CCATEGORY_ID, poParam.CCATEGORY_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CTAX_DATE, poParam.CTAX_DATE);
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CBUYSELL, poParam.CBUYSELL);

                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<APL00300DTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicAPLookup.APL00300ProductLookUp),
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

        #endregion

        #region APL00400

        public IAsyncEnumerable<APL00400DTO> APL00400ProductAllocationLookUp()
        {
            throw new NotImplementedException();
        }

        public async Task<List<APL00400DTO>> APL00400ProductAllocationLookUpAsync(APL00400ParameterDTO poParam)
        {
            var loEx = new R_Exception();
            List<APL00400DTO> loResult = null;

            try
            {
                //context
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CPROPERTY_ID, poParam.CPROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CACTIVE_TYPE, poParam.CACTIVE_TYPE);

                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<APL00400DTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicAPLookup.APL00400ProductAllocationLookUp),
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

        #endregion

        #region APL00500

        public IAsyncEnumerable<APL00500DTO> APL00500TransactionLookup()
        {
            throw new NotImplementedException();
        }

        public async Task<List<APL00500DTO>> APL00500TransactionLookupAsync(APL00500ParameterDTO poParam)
        {
            var loEx = new R_Exception();
            List<APL00500DTO> loResult = null;

            try
            {
                //context
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CPROPERTY_ID, poParam.CPROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CACTIVE_TYPE, poParam.CTRANS_CODE);
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CDEPT_CODE, poParam.CDEPT_CODE);
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CSUPPLIER_ID, poParam.CSUPPLIER_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CTRANS_CODE, poParam.CTRANS_CODE);
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CPERIOD, poParam.CPERIOD);
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.LHAS_REMAINING, poParam.LHAS_REMAINING);
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.LNO_REMAINING, poParam.LNO_REMAINING);
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CCURRENCY_CODE, poParam.CCURRENCY_CODE);

                R_HTTPClientWrapper.httpClientName = _HttpClientName;   
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<APL00500DTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicAPLookup.APL00500TransactionLookup),
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

        #endregion

        #region APL00500Init

        public APL00500PeriodDTO APLInitiateTransactionLookup()
        {
            throw new NotImplementedException();
        }

       

        public async Task <APL00500PeriodDTO> APLInitiateTransactionLookupAsync()
        {
            var loEx = new R_Exception();
            APL00500PeriodDTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<APL00500PeriodDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicAPLookup.APLInitiateTransactionLookup),
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

        #endregion

        #region APL00600

        public IAsyncEnumerable<APL00600DTO> APL00600ApSchedulePaymentLookup()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<APL00600DTO> APL00600ApInvoiceListLookup()
        {
            throw new NotImplementedException();
        }
        
        public async Task<List<APL00600DTO>> APL00600ApSchedulePaymentLookupAsync(APL00600ParameterDTO poParam)
        {
            var loEx = new R_Exception();
            List<APL00600DTO> loResult = null;

            try
            {
                //context
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CPROPERTY_ID, poParam.CPROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CDEPT_CODE, poParam.CDEPT_CODE);
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CPAYMENT_TYPE, poParam.CPAYMENT_TYPE);
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CSUPPLIER_ID, poParam.CSUPPLIER_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CSCH_PAYMENT_ID, poParam.CSCH_PAYMENT_ID);

                R_HTTPClientWrapper.httpClientName = _HttpClientName;   
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<APL00600DTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicAPLookup.APL00600ApSchedulePaymentLookup),
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
        public async Task<List<APL00600DTO>> APL00600ApInvoiceListLookupAsync(APL00600ParameterDTO poParam)
        {
            var loEx = new R_Exception();
            List<APL00600DTO> loResult = null;

            try
            {
                //context
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CREC_ID, poParam.CREC_ID);

                R_HTTPClientWrapper.httpClientName = _HttpClientName;   
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<APL00600DTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicAPLookup.APL00600ApInvoiceListLookup),
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
        #endregion

        #region APL00700

        public IAsyncEnumerable<APL00700DTO> APL00700CancelPaymentToSupplierLookup()
        {
            throw new NotImplementedException();
        }

        public async Task<List<APL00700DTO>> APL00700CancelPaymentToSupplierLookupAsync(APL00700ParameterDTO poParam)
        {
            var loEx = new R_Exception();
            List<APL00700DTO> loResult = null;

            try
            {
                //context
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CPROPERTY_ID, poParam.CPROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CPERIOD, poParam.CPERIOD);
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CSUPPLIER_ID, poParam.CSUPPLIER_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CSCH_PAYMENT_ID, poParam.CSCH_PAYMENT_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CDEPT_CODE, poParam.CDEPT_CODE);


                R_HTTPClientWrapper.httpClientName = _HttpClientName;   
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<APL00700DTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicAPLookup.APL00700CancelPaymentToSupplierLookup),
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
        public APL00700DTO APL00700InitialProcess()
        {
            throw new NotImplementedException();
        }

        public async Task <APL00700DTO> APL00700InitialProcessAsync()
        {
            var loEx = new R_Exception();
            APL00700DTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<APL00700DTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicAPLookup.APL00700InitialProcess),
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
        #endregion
    }
}