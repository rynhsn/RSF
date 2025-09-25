using PMT00500COMMON;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PMT00500MODEL
{
    public class PMT00500InitModel : R_BusinessObjectServiceClientBase<PMT00500DTO>, IPMT00500Init
    {
        private const string DEFAULT_HTTP = "R_DefaultServiceUrlPM";
        private const string DEFAULT_ENDPOINT = "api/PMT00500Init";
        private const string DEFAULT_MODULE = "PM";

        public PMT00500InitModel(
            string pcHttpClientName = DEFAULT_HTTP,
            string pcRequestServiceEndPoint = DEFAULT_ENDPOINT,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        public async Task<PMT00500TransCodeInfoGSDTO> GetTransCodeInfoAsync(string pcCallFrom = "")
        {
            var loEx = new R_Exception();
            PMT00500TransCodeInfoGSDTO loResult = null;

            try
            {

                R_FrontContext.R_SetStreamingContext(ContextConstant.CCALLER_TRANSACTION, pcCallFrom);

                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<PMT00500SingleResult<PMT00500TransCodeInfoGSDTO>>(
                    _RequestServiceEndPoint,
                    nameof(IPMT00500Init.GetTransCodeInfo),
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

        public async Task<List<PMT00500AgreementChargeCalUnitDTO>> GetAllAgreementChargeCallUnitListAsync(PMT00500ParameterAgreementChargeCalUnitDTO poEntity)
        {
            var loEx = new R_Exception();
            List<PMT00500AgreementChargeCalUnitDTO> loResult = null;

            try
            {
                //Set Context
                R_FrontContext.R_SetStreamingContext(ContextConstant.CPROPERTY_ID, poEntity.CPROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CDEPT_CODE, poEntity.CDEPT_CODE);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CREF_NO, poEntity.CREF_NO);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CSEQ_NO, string.IsNullOrWhiteSpace(poEntity.CSEQ_NO) ? "" : poEntity.CSEQ_NO);

                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMT00500AgreementChargeCalUnitDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT00500Init.GetAllAgreementChargeCallUnitList),
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
        public async Task<List<PMT00500PropertyDTO>> GetAllPropertyListAsync()
        {
            var loEx = new R_Exception();
            List<PMT00500PropertyDTO> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMT00500PropertyDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT00500Init.GetAllPropertyList),
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
        public async Task<List<PMT00500UniversalDTO>> GetAllUniversalListAsync(string pcParameter)
        {
            var loEx = new R_Exception();
            List<PMT00500UniversalDTO> loResult = null;

            try
            {
                //Set Context
                R_FrontContext.R_SetStreamingContext(ContextConstant.CPARAMETER, pcParameter);

                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMT00500UniversalDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT00500Init.GetAllUniversalList),
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
        public async Task<List<PMT00500BuildingDTO>> GetAllBuildingListAsync(string poPropertyId)
        {
            var loEx = new R_Exception();
            List<PMT00500BuildingDTO> loResult = null;

            try
            {
                //Set Context
                R_FrontContext.R_SetStreamingContext(ContextConstant.CPARAMETER, poPropertyId);

                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMT00500BuildingDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT00500Init.GetAllBuildingList),
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
        public async Task<List<PMT00500AgreementBuildingUtilitiesDTO>> GetAllBuildingUtilitiesListAsync(PMT00500ParameterAgreementBuildingUtilitiesDTO poEntity)
        {
            var loEx = new R_Exception();
            List<PMT00500AgreementBuildingUtilitiesDTO> loResult = null;

            try
            {
                //Set Context
                R_FrontContext.R_SetStreamingContext(ContextConstant.CPROPERTY_ID, poEntity.CPROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CBUILDING_ID, poEntity.CBUILDING_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CFLOOR_ID, poEntity.CFLOOR_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CUNIT_ID, poEntity.CUNIT_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CCHARGES_TYPE, poEntity.CCHARGES_TYPE);

                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMT00500AgreementBuildingUtilitiesDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT00500Init.GetAllBuildingUtilitiesList),
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
        public async Task<List<PMT00500BuildingUnitDTO>> GetAllBuildingUnitListAsync(PMT00500BuildingUnitDTO poEntity)
        {
            var loEx = new R_Exception();
            List<PMT00500BuildingUnitDTO> loResult = null;

            try
            {
                //Set Context
                R_FrontContext.R_SetStreamingContext(ContextConstant.CPROPERTY_ID, poEntity.CPROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CBUILDING_ID, poEntity.CBUILDING_ID);

                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMT00500BuildingUnitDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT00500Init.GetAllBuildingUnitList),
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
        public PMT00500SingleResult<PMT00500TransCodeInfoGSDTO> GetTransCodeInfo()
        {
            throw new NotImplementedException();
        }
        public IAsyncEnumerable<PMT00500AgreementChargeCalUnitDTO> GetAllAgreementChargeCallUnitList()
        {
            throw new NotImplementedException();
        }
        public IAsyncEnumerable<PMT00500PropertyDTO> GetAllPropertyList()
        {
            throw new NotImplementedException();
        }
        public IAsyncEnumerable<PMT00500UniversalDTO> GetAllUniversalList()
        {
            throw new NotImplementedException();
        }
        public IAsyncEnumerable<PMT00500AgreementBuildingUtilitiesDTO> GetAllBuildingUtilitiesList()
        {
            throw new NotImplementedException();
        }
        public IAsyncEnumerable<PMT00500BuildingDTO> GetAllBuildingList()
        {
            throw new NotImplementedException();
        }
        public IAsyncEnumerable<PMT00500BuildingUnitDTO> GetAllBuildingUnitList()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
