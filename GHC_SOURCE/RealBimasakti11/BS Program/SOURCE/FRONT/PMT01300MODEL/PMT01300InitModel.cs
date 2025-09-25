using PMT01300COMMON;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PMT01300MODEL
{
    public class PMT01300InitModel : R_BusinessObjectServiceClientBase<PMT01300DTO>, IPMT01300Init
    {
        private const string DEFAULT_HTTP = "R_DefaultServiceUrlPM";
        private const string DEFAULT_ENDPOINT = "api/PMT01300Init";
        private const string DEFAULT_MODULE = "PM";

        public PMT01300InitModel(
            string pcHttpClientName = DEFAULT_HTTP,
            string pcRequestServiceEndPoint = DEFAULT_ENDPOINT,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        public async Task<PMT01300TransCodeInfoGSDTO> GetTransCodeInfoAsync()
        {
            var loEx = new R_Exception();
            PMT01300TransCodeInfoGSDTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<PMT01300SingleResult<PMT01300TransCodeInfoGSDTO>>(
                    _RequestServiceEndPoint,
                    nameof(IPMT01300Init.GetTransCodeInfo),
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

        public async Task<List<PMT01300AgreementChargeCalUnitDTO>> GetAllAgreementChargeCallUnitListAsync(PMT01300ParameterAgreementChargeCalUnitDTO poEntity)
        {
            var loEx = new R_Exception();
            List<PMT01300AgreementChargeCalUnitDTO> loResult = null;

            try
            {
                //Set Context
                R_FrontContext.R_SetStreamingContext(ContextConstant.CPROPERTY_ID, poEntity.CPROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CDEPT_CODE, poEntity.CDEPT_CODE);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CREF_NO, poEntity.CREF_NO);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CSEQ_NO, string.IsNullOrWhiteSpace(poEntity.CSEQ_NO) ? "" : poEntity.CSEQ_NO);

                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMT01300AgreementChargeCalUnitDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT01300Init.GetAllAgreementChargeCallUnitList),
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
        public async Task<List<PMT01300PropertyDTO>> GetAllPropertyListAsync()
        {
            var loEx = new R_Exception();
            List<PMT01300PropertyDTO> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMT01300PropertyDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT01300Init.GetAllPropertyList),
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
        public async Task<List<PMT01300UniversalDTO>> GetAllUniversalListAsync(string pcParameter)
        {
            var loEx = new R_Exception();
            List<PMT01300UniversalDTO> loResult = null;

            try
            {
                //Set Context
                R_FrontContext.R_SetStreamingContext(ContextConstant.CPARAMETER, pcParameter);

                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMT01300UniversalDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT01300Init.GetAllUniversalList),
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
        public async Task<List<PMT01300AgreementBuildingUtilitiesDTO>> GetAllBuildingUtilitiesListAsync(PMT01300ParameterAgreementBuildingUtilitiesDTO poEntity)
        {
            var loEx = new R_Exception();
            List<PMT01300AgreementBuildingUtilitiesDTO> loResult = null;

            try
            {
                //Set Context
                R_FrontContext.R_SetStreamingContext(ContextConstant.CPROPERTY_ID, poEntity.CPROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CBUILDING_ID, poEntity.CBUILDING_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CFLOOR_ID, poEntity.CFLOOR_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CUNIT_ID, poEntity.CUNIT_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CCHARGES_TYPE, poEntity.CCHARGES_TYPE);

                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMT01300AgreementBuildingUtilitiesDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT01300Init.GetAllBuildingUtilitiesList),
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
        public PMT01300SingleResult<PMT01300TransCodeInfoGSDTO> GetTransCodeInfo()
        {
            throw new NotImplementedException();
        }
        public IAsyncEnumerable<PMT01300AgreementChargeCalUnitDTO> GetAllAgreementChargeCallUnitList()
        {
            throw new NotImplementedException();
        }
        public IAsyncEnumerable<PMT01300PropertyDTO> GetAllPropertyList()
        {
            throw new NotImplementedException();
        }
        public IAsyncEnumerable<PMT01300UniversalDTO> GetAllUniversalList()
        {
            throw new NotImplementedException();
        }
        public IAsyncEnumerable<PMT01300AgreementBuildingUtilitiesDTO> GetAllBuildingUtilitiesList()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
