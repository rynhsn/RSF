using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using PMM06000COMMON;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using System.Threading.Tasks;

namespace PMM06000Model
{
    public class LMM06000Model : R_BusinessObjectServiceClientBase<LMM06000BillingRuleDetailDTO>, ILMM06000
    {
        private const string DEFAULT_HTTP = "R_DefaultServiceUrlPM";
        private const string DEFAULT_ENDPOINT = "api/LMM06000";
        private const string DEFAULT_MODULE = "PM";
        public LMM06000Model(
            string pcHttpClientName = DEFAULT_HTTP,
            string pcRequestServiceEndPoint = DEFAULT_ENDPOINT,
            string pcModuleName = DEFAULT_MODULE,
            bool plSendWithContext = true,
            bool plSendWithToken = true)
            : base(pcHttpClientName, pcRequestServiceEndPoint, pcModuleName, plSendWithContext, plSendWithToken)
        {
        }

        #region Implements Library
        public IAsyncEnumerable<LMM06000BillingRuleDTO> BillingRuleListStream()
        {
            throw new NotImplementedException();
        }

        public LMM06000PropertyListDTO GetAllPropertyList()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<LMM06000UnitTypeDTO> GetAllUnitTypeList()
        {
            throw new NotImplementedException();
        }

        public LMM06000PeriodListDTO GetAllPeriodList()
        {
            throw new NotImplementedException();
        }

        public LMM06000ActiveInactiveDTO SetActiveInactive(LMM06000ActiveInactiveDTO loParameter)
        {
            throw new NotImplementedException();
        }

#endregion



        public async Task<LMM06000PropertyListDTO> GetPropertyAsyncModel()
        {
            var loEx = new R_Exception();
            LMM06000PropertyListDTO loResult = new LMM06000PropertyListDTO();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<LMM06000PropertyListDTO>(
                    _RequestServiceEndPoint,
                    nameof(ILMM06000.GetAllPropertyList),
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
        public async Task<LMM06000PeriodListDTO> GetPeriodAsyncModel()
        {
            var loEx = new R_Exception();
            LMM06000PeriodListDTO loResult = new LMM06000PeriodListDTO() ;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<LMM06000PeriodListDTO>(
                    _RequestServiceEndPoint,
                    nameof(ILMM06000.GetAllPeriodList),
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
        public async Task<LMM06000UnitTypeListDTO> GetUnitTypeAsyncModel()
        {
            var loEx = new R_Exception();
            LMM06000UnitTypeListDTO loResult = new LMM06000UnitTypeListDTO();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loResultTemp = await R_HTTPClientWrapper.R_APIRequestStreamingObject<LMM06000UnitTypeDTO>(
                    _RequestServiceEndPoint,
                    nameof(ILMM06000.GetAllUnitTypeList),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loResult.Data = loResultTemp;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public async Task<LMM06000BillingRuleListDTO> GetBillingRuleListAsyncModel()
        {
            var loEx = new R_Exception();
            LMM06000BillingRuleListDTO loResult = new LMM06000BillingRuleListDTO();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTemp = await R_HTTPClientWrapper.R_APIRequestStreamingObject<LMM06000BillingRuleDTO>(
                    _RequestServiceEndPoint,
                    nameof(ILMM06000.BillingRuleListStream),
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

        #region SetActiveInactive
        public async Task SetActiveInactiveAsync(LMM06000ActiveInactiveDTO loParameter)
        {
            var loEx = new R_Exception();

            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP;
                await R_HTTPClientWrapper.R_APIRequestObject<LMM06000ActiveInactiveDTO, LMM06000ActiveInactiveDTO>(
                    _RequestServiceEndPoint,
                    nameof(ILMM06000.SetActiveInactive),
                    loParameter,
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        #endregion




    }
}
