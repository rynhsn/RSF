using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lookup_PMCOMMON;
using Lookup_PMCOMMON.DTOs;
using R_BlazorFrontEnd.Exceptions;
using R_APIClient;
using Lookup_PMCOMMON.DTOs.LML01000;
using Lookup_PMCOMMON.DTOs.LML01100;
using Lookup_PMCOMMON.DTOs.LML01300;

namespace Lookup_PMModel
{
    public class PublicLookupLMModel : R_BusinessObjectServiceClientBase<LML00200DTO>, IPublicLookupLM
    {
        private const string DEFAULT_HTTP = "R_DefaultServiceUrlPM";
        private const string DEFAULT_ENDPOINT = "api/PublicLookupLM";
        private const string DEFAULT_MODULE = "PM";

        public PublicLookupLMModel(
            string pcHttpClientName = DEFAULT_HTTP,
            string pcRequestServiceEndPoint = DEFAULT_ENDPOINT,
            string pcModuleName = DEFAULT_MODULE,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, pcModuleName, plSendWithContext, plSendWithToken)
        {
        }

        #region implements INTERFACE
        public IAsyncEnumerable<LML00200DTO> LML00200UnitChargesList()
        {
            throw new NotImplementedException();
        }
        public IAsyncEnumerable<LML00300DTO> LML00300SupervisorList()
        {
            throw new NotImplementedException();
        }
        public IAsyncEnumerable<LML00400DTO> LML00400UtilityChargesList()
        {
            throw new NotImplementedException();
        }
        public IAsyncEnumerable<LML00500DTO> LML00500SalesmanList()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<LML00600DTO> LML00600TenantList()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<LML00700DTO> LML00700DiscountList()
        {
            throw new NotImplementedException();
        }
        public IAsyncEnumerable<LML00800DTO> LML00800AgreementList()
        {
            throw new NotImplementedException();
        }
        public IAsyncEnumerable<LML00900DTO> LML00900TransactionList()
        {
            throw new NotImplementedException();
        }
        public IAsyncEnumerable<LML01200DTO> LML01200InvoiceGroupList()
        {
            throw new NotImplementedException();
        }
        public IAsyncEnumerable<LML01000DTO> LML01000BillingRuleList()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<LML01100DTO> LML01100TNCList()
        {
            throw new NotImplementedException();
        }
        public LML00900InitialProcessDTO InitialProcess()
        {
            throw new NotImplementedException();
        }
        #endregion


        #region LML00200 

        public async Task<LMLGenericList<LML00200DTO>> LML00200GetUnitChargesListAsync ()
        {
            var loEx = new R_Exception();
            LMLGenericList<LML00200DTO> loResult = new LMLGenericList<LML00200DTO>();
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<LML00200DTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicLookupLM.LML00200UnitChargesList),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loResult.Data = loTempResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loResult;
        }
        #endregion

        #region LML00300 

        public async Task<LMLGenericList<LML00300DTO>> LML00300SupervisorListAsync ()
        {
            var loEx = new R_Exception();
            LMLGenericList<LML00300DTO> loResult = new LMLGenericList<LML00300DTO>();
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<LML00300DTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicLookupLM.LML00300SupervisorList),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loResult.Data = loTempResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            return loResult;
        }
        #endregion

        #region LML00400
        public async Task<LMLGenericList<LML00400DTO>> LML00400GetUtilityChargesListAsync()
        {
            var loEx = new R_Exception();
            LMLGenericList<LML00400DTO> loResult = new LMLGenericList<LML00400DTO>();
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<LML00400DTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicLookupLM.LML00400UtilityChargesList),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loResult.Data = loTempResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loResult;
        }
        #endregion

        #region LML00500
        public async Task<LMLGenericList<LML00500DTO>> LML00500GetSalesmanListAsync()
        {
            var loEx = new R_Exception();
            LMLGenericList<LML00500DTO> loResult = new LMLGenericList<LML00500DTO>();
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<LML00500DTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicLookupLM.LML00500SalesmanList),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loResult.Data = loTempResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loResult;
        }
        #endregion
        #region LML00600
        public async Task<LMLGenericList<LML00600DTO>> LML00600GetTenantListAsync()
        {
            var loEx = new R_Exception();
            LMLGenericList<LML00600DTO> loResult = new LMLGenericList<LML00600DTO>();
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<LML00600DTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicLookupLM.LML00600TenantList),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loResult.Data = loTempResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loResult;
        }
        #endregion      
        #region LML00700
        public async Task<LMLGenericList<LML00700DTO>> LML00700GetDiscountListAsync()
        {
            var loEx = new R_Exception();
            LMLGenericList<LML00700DTO> loResult = new LMLGenericList<LML00700DTO>();
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<LML00700DTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicLookupLM.LML00700DiscountList),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loResult.Data = loTempResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loResult;
        }
        #endregion
        #region LML00800
        public async Task<LMLGenericList<LML00800DTO>> LML00800GetAgreementListAsync()
        {
            var loEx = new R_Exception();
            LMLGenericList<LML00800DTO> loResult = new LMLGenericList<LML00800DTO>();
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<LML00800DTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicLookupLM.LML00800AgreementList),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loResult.Data = loTempResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loResult;
        }
        #endregion

        #region LML00900
        public async Task<LMLGenericList<LML00900DTO>> LML00900GetTransactionListAsync()
        {
            var loEx = new R_Exception();
            LMLGenericList<LML00900DTO> loResult = new LMLGenericList<LML00900DTO>();
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<LML00900DTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicLookupLM.LML00900TransactionList),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loResult.Data = loTempResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loResult;
        }
        #endregion
        #region LML01000
        public async Task<LMLGenericList<LML01000DTO>> LML01000GetBillingRuleListAsync()
        {
            var loEx = new R_Exception();
            LMLGenericList<LML01000DTO> loResult = new LMLGenericList<LML01000DTO>();
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<LML01000DTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicLookupLM.LML01000BillingRuleList),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loResult.Data = loTempResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loResult;
        }
        #endregion
        #region LML01100
        public async Task<LMLGenericList<LML01100DTO>> LML01100GetTermNConditionListAsync()
        {
            var loEx = new R_Exception();
            LMLGenericList<LML01100DTO> loResult = new LMLGenericList<LML01100DTO>();
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<LML01100DTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicLookupLM.LML01100TNCList),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loResult.Data = loTempResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loResult;
        }
        #endregion
        #region LML01200
        public async Task<LMLGenericList<LML01200DTO>> PML01200InvoiceGroupListAsync()
        {
            var loEx = new R_Exception();
            LMLGenericList<LML01200DTO> loResult = new LMLGenericList<LML01200DTO>();
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<LML01200DTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicLookupLM.LML01200InvoiceGroupList),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loResult.Data = loTempResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loResult;
        }
        #endregion
        #region LML01300
        public async Task<LMLGenericList<LML01300DTO>> LML01300LOIAgreementListAsync()
        {
            var loEx = new R_Exception();
            LMLGenericList<LML01300DTO> loResult = new LMLGenericList<LML01300DTO>();
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<LML01300DTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicLookupLM.LML01300LOIAgreementList),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loResult.Data = loTempResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loResult;
        }
        #endregion


        #region Utility
        public async Task<LML00900InitialProcessDTO> GetInitialProcessAsyncModel()
        {
            var loEx = new R_Exception();
            LML00900InitialProcessDTO loResult = new LML00900InitialProcessDTO();
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<LML00900InitialProcessDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicLookupLM.InitialProcess),
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

        public IAsyncEnumerable<LML01300DTO> LML01300LOIAgreementList()
        {
            throw new NotImplementedException();
        }




        #endregion


    }
}
