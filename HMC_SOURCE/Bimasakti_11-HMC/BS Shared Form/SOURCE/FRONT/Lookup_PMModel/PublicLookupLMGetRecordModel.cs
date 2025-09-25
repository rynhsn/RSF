using Lookup_PMCOMMON;
using Lookup_PMCOMMON.DTOs;
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
        private const string DEFAULT_HTTP = "R_DefaultServiceUrlLM";
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
        #region LML00600GetRecord
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

    }
}
