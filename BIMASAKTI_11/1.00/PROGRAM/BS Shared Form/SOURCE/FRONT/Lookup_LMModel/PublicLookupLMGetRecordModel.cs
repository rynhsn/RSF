using Lookup_PMCOMMON;
using Lookup_PMCOMMON.DTOs;
using Lookup_PMCOMMON.DTOs.GET_USER_PARAM_DETAIL;
using Lookup_PMCOMMON.DTOs.LML01000;
using Lookup_PMCOMMON.DTOs.LML01100;
using Lookup_PMCOMMON.DTOs.LML01300;
using Lookup_PMCOMMON.DTOs.LML01400;
using Lookup_PMCOMMON.DTOs.LML01500;
using Lookup_PMCOMMON.DTOs.LML01600;
using Lookup_PMCOMMON.DTOs.LML01800;
using Lookup_PMCOMMON.DTOs.LML01900;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Lookup_PMCOMMON.DTOs.LML01700;

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
        public LMLGenericRecord<GET_USER_PARAM_DETAILDTO> UserParamDetail(GET_USER_PARAM_DETAILParameterDTO poParam)
        {
            throw new NotImplementedException();
        }
        public LMLGenericRecord<LML01400DTO> LML01400AgreementUnitCharges(LML01400ParameterDTO poParam)
        {
            throw new NotImplementedException();
        }
        public LMLGenericRecord<LML01500DTO> LML01500SLACategory(LML01500ParameterDTO poParam)
        {
            throw new NotImplementedException();
        }
        public LMLGenericRecord<LML01600DTO> LML01600SLACallType(LML01600ParameterDTO poParam)
        {
            throw new NotImplementedException();
        }

        public LMLGenericRecord<LML01700DTO> LML01700CancelReceiptFromCustomer(LML01700ParameterDTO poParam)
        {
            throw new NotImplementedException();
        }

        public LMLGenericRecord<LML01800DTO> LML01800UnitTenant(LML01800ParameterDTO poParam)
        {
            throw new NotImplementedException();
        }
        public LMLGenericRecord<LML01900DTO> LML01900Staff(LML01900ParamaterDTO poParam)
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
        #region LML01400GetRecord       
        public async Task<LML01400DTO> LML01400AgreementUnitChargesAsync(LML01400ParameterDTO poParam)
        {

            var loEx = new R_Exception();
            LML01400DTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<LMLGenericRecord<LML01400DTO>, LML01400ParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGetRecordLookupLM.LML01400AgreementUnitCharges),
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
        #region LML01500GetRecord       
        public async Task<LML01500DTO> LML01500SLACategoryAsync(LML01500ParameterDTO poParam)
        {

            var loEx = new R_Exception();
            LML01500DTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<LMLGenericRecord<LML01500DTO>, LML01500ParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGetRecordLookupLM.LML01500SLACategory),
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
        #region LML01600GetRecord       
        public async Task<LML01600DTO> LML01600SLACallTypeAsync(LML01600ParameterDTO poParam)
        {

            var loEx = new R_Exception();
            LML01600DTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<LMLGenericRecord<LML01600DTO>, LML01600ParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGetRecordLookupLM.LML01600SLACallType),
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
        #region LML01700GetRecord
        
        public async Task<LML01700DTO> LML01700CancelReceiptFromCustomerAsync(LML01700ParameterDTO poParam)
        {
            var loEx = new R_Exception();
            LML01700DTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<LMLGenericRecord<LML01700DTO>, LML01700ParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGetRecordLookupLM.LML01700CancelReceiptFromCustomer),
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
        #region LML01800GetRecord       
        public async Task<LML01800DTO> LML01800UnitTenantAsync(LML01800ParameterDTO poParam)
        {

            var loEx = new R_Exception();
            LML01800DTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<LMLGenericRecord<LML01800DTO>, LML01800ParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGetRecordLookupLM.LML01800UnitTenant),
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
        #region LML01900GetRecord       
        public async Task<LML01900DTO> LML01900StaffAsync(LML01900ParamaterDTO poParam)
        {

            var loEx = new R_Exception();
            LML01900DTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<LMLGenericRecord<LML01900DTO>, LML01900ParamaterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGetRecordLookupLM.LML01900Staff),
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

        #region GET USER PARAM
        public async Task<GET_USER_PARAM_DETAILDTO> GetUserParamDetailAsync(GET_USER_PARAM_DETAILParameterDTO poParam)
        {
            var loEx = new R_Exception();
            GET_USER_PARAM_DETAILDTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<LMLGenericRecord<GET_USER_PARAM_DETAILDTO>, GET_USER_PARAM_DETAILParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGetRecordLookupLM.UserParamDetail),
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
