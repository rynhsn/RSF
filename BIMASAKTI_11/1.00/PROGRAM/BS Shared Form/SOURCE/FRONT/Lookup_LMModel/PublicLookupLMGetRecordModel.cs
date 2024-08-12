using Lookup_PMCOMMON;
using Lookup_PMCOMMON.DTOs;
using Lookup_PMCOMMON.DTOs.LML01000;
using Lookup_PMCOMMON.DTOs.LML01100;
using Lookup_PMCOMMON.DTOs.LML01300;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Lookup_PMModel
{
    public class PublicLookupLMGetRecordModel : R_BusinessObjectServiceClientBase<LML00200DTO>, IGetRecordLookupLM
    {
        private const string DEFAULT_HTTP = "R_DefaultServiceUrlPM";
        private const string DEFAULT_ENDPOINT = "api/PublicLookupLMGetRecord";
        private const string DEFAULT_MODULE = "PM";

        public PublicLookupLMGetRecordModel(
            string pcHttpClientName = DEFAULT_HTTP,
            string pcRequestServiceEndPoint = DEFAULT_ENDPOINT,
            string pcModuleName = DEFAULT_MODULE,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, pcModuleName, plSendWithContext, plSendWithToken)
        {
        }
        #region ImplementsLibrary

        public LMLGenericRecord<LML00200DTO> LML00200UnitCharges(LML00200ParameterDTO poParam)
        {
            throw new NotImplementedException();
        }

        public LMLGenericRecord<LML00300DTO> LML00300Supervisor(LML00300ParameterDTO poParam)
        {
            throw new NotImplementedException();
        }

        public LMLGenericRecord<LML00400DTO> LML00400UtilityCharges(LML00400ParameterDTO poParam)
        {
            throw new NotImplementedException();
        }

        public LMLGenericRecord<LML00500DTO> LML00500Salesman(LML00500ParameterDTO poParam)
        {
            throw new NotImplementedException();
        }

        public LMLGenericRecord<LML00600DTO> LML00600Tenant(LML00600ParameterDTO poParam)
        {
            throw new NotImplementedException();
        }

        public LMLGenericRecord<LML00700DTO> LML00700Discount(LML00700ParameterDTO poParam)
        {
            throw new NotImplementedException();
        }


        public LMLGenericRecord<LML00800DTO> LML00800Agreement(LML00800ParameterDTO poParam)
        {
            throw new NotImplementedException();
        }
        public LMLGenericRecord<LML00900DTO> LML00900Transaction(LML00900ParameterDTO poParam)
        {
            throw new NotImplementedException();
        }
        public LMLGenericRecord<LML01200DTO> LML01200InvoiceGroup(LML01200ParameterDTO poParam)
        {
            throw new NotImplementedException();
        }
        public LMLGenericRecord<LML01000DTO> LML01000BillingRule(LML01000ParameterDTO poParam)
        {
            throw new NotImplementedException();
        }

        public LMLGenericRecord<LML01100DTO> LML01100TNC(LML01100ParameterDTO poParam)
        {
            throw new NotImplementedException();
        }
        public LMLGenericRecord<LML01300DTO> LML01300LOIAgreement(LML01300ParameterDTO poParam)
        {
            throw new NotImplementedException();
        }
        #endregion



        #region LML00200GetRecord
        public async Task<LML00200DTO> LML00200GetUnitChargesAsync(LML00200ParameterDTO poParam)
        {

            var loEx = new R_Exception();
            LML00200DTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<LMLGenericRecord<LML00200DTO>, LML00200ParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGetRecordLookupLM.LML00200UnitCharges),
                    poParam,
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loResult = loTempResult.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        #endregion

        #region LML00300GetRecord
        public async Task<LML00300DTO> LML00300SupervisorAsync(LML00300ParameterDTO poParam)
        {

            var loEx = new R_Exception();
            LML00300DTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<LMLGenericRecord<LML00300DTO>, LML00300ParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGetRecordLookupLM.LML00300Supervisor),
                    poParam,
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loResult = loTempResult.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        #endregion

        #region LML00400GetRecord
        public async Task<LML00400DTO> LML00400GetUtilityChargesAsync(LML00400ParameterDTO poParam)
        {

            var loEx = new R_Exception();
            LML00400DTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<LMLGenericRecord<LML00400DTO>, LML00400ParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGetRecordLookupLM.LML00400UtilityCharges),
                    poParam,
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loResult = loTempResult.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        #endregion

        #region LML00500GetRecord
        public async Task<LML00500DTO> LML00500GetSalesmanAsync(LML00500ParameterDTO poParam)
        {

            var loEx = new R_Exception();
            LML00500DTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<LMLGenericRecord<LML00500DTO>, LML00500ParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGetRecordLookupLM.LML00500Salesman),
                    poParam,
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loResult = loTempResult.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        #endregion

        #region LML00600GetRecord
        public async Task<LML00600DTO> LML00600GetTenantAsync(LML00600ParameterDTO poParam)
        {

            var loEx = new R_Exception();
            LML00600DTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<LMLGenericRecord<LML00600DTO>, LML00600ParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGetRecordLookupLM.LML00600Tenant),
                    poParam,
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loResult = loTempResult.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        #endregion
        #region LML00700GetRecord
        public async Task<LML00700DTO> LML00700GetDiscountAsync(LML00700ParameterDTO poParam)
        {

            var loEx = new R_Exception();
            LML00700DTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<LMLGenericRecord<LML00700DTO>, LML00700ParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGetRecordLookupLM.LML00700Discount),
                    poParam,
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loResult = loTempResult.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        #endregion
        #region LML00800GetRecord
        public async Task<LML00800DTO> LML00800GetAgreementAsync(LML00800ParameterDTO poParam)
        {

            var loEx = new R_Exception();
            LML00800DTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<LMLGenericRecord<LML00800DTO>, LML00800ParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGetRecordLookupLM.LML00800Agreement),
                    poParam,
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loResult = loTempResult.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult!;
        }
        #endregion

        #region LML0900GetRecord
        public async Task<LML00900DTO> LML00900GetTransactionAsync(LML00900ParameterDTO poParam)
        {

            var loEx = new R_Exception();
            LML00900DTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<LMLGenericRecord<LML00900DTO>, LML00900ParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGetRecordLookupLM.LML00900Transaction),
                    poParam,
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loResult = loTempResult.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult!;
        }

        #region LML01000GetRecord
        public async Task<LML01000DTO> LML01000GetBillingRuleAsync(LML01000ParameterDTO poParam)
        {

            var loEx = new R_Exception();
            LML01000DTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<LMLGenericRecord<LML01000DTO>, LML01000ParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGetRecordLookupLM.LML01000BillingRule),
                    poParam,
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loResult = loTempResult.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();

            return loResult!;
        }
        #endregion
        #region LML01100GetRecord
        public async Task<LML01100DTO> LML01100GetTermNConditionAsync(LML01100ParameterDTO poParam)
        {

            var loEx = new R_Exception();
            LML01100DTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<LMLGenericRecord<LML01100DTO>, LML01100ParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGetRecordLookupLM.LML01100TNC),
                    poParam,
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loResult = loTempResult.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();

            return loResult!;
        }
        #endregion
        #endregion
        #region LML01200GetRecord
        public async Task<LML01200DTO> PML01200GetInvoiceGroupAsync(LML01200ParameterDTO poParam)
        {

            var loEx = new R_Exception();
            LML01200DTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<LMLGenericRecord<LML01200DTO>, LML01200ParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGetRecordLookupLM.LML01200InvoiceGroup),
                    poParam,
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loResult = loTempResult.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult!;
        }

        #endregion
        #region LML01300GetRecord
        public async Task<LML01300DTO> LML01300GetLOIAgreementAsync(LML01300ParameterDTO poParam)
        {

            var loEx = new R_Exception();
            LML01300DTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<LMLGenericRecord<LML01300DTO>, LML01300ParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGetRecordLookupLM.LML01300LOIAgreement),
                    poParam,
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loResult = loTempResult.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult!;
        }

        #endregion







    }
}
