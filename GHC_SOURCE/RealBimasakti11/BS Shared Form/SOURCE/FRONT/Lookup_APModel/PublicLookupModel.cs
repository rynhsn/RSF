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
using Lookup_APCOMMON.DTOs.APL00600;
using R_APIClient;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;

namespace Lookup_APModel
{
    public class PublicLookupModel : R_BusinessObjectServiceClientBase<APL00100DTO>, IPublicLookup
    {
        private const string DEFAULT_HTTP = "R_DefaultServiceUrlAP";
        private const string DEFAULT_ENDPOINT = "api/PublicLookup";
        private const string DEFAULT_MODULE = "AP";

        public PublicLookupModel(
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
                    nameof(IPublicLookup.APL00100SupplierLookUp),
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
                    nameof(IPublicLookup.APL00110SupplierInfoLookUp),
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
                    nameof(IPublicLookup.APL00200ExpenditureLookUp),
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
                    nameof(IPublicLookup.APL00300ProductLookUp),
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

        public IAsyncEnumerable<APL00600DTO> APL00600ApSchedulePaymentLookup()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<APL00600DTO> APL00600ApInvoiceListLookup()
        {
            throw new NotImplementedException();
        }
        public async Task<List<APL00600DTO>> APL00600ApInvoiceListLookupAsync(APL00600ParameterDTO poParam)
        {
            var loEx = new R_Exception();
            List<APL00600DTO> loResult = null;

            try
            {
                //context
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
                    nameof(IPublicLookup.APL00400ProductAllocationLookUp),
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
