using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PMT02600COMMON.DTOs.PMT02620;
using PMT02600COMMON.DTOs;
using PMT02600COMMON;
using PMT01400COMMON.DTOs.Helper;

namespace PMT02600MODEL
{
    public class PMT02620UtilitiesModel : R_BusinessObjectServiceClientBase<PMT02620UtilitiesDTO>, IPMT02620Utilities
    {
        private const string DEFAULT_HTTP = "R_DefaultServiceUrlPM";
        private const string DEFAULT_ENDPOINT = "api/PMT02620Utilities";
        private const string DEFAULT_MODULE = "PM";

        public PMT02620UtilitiesModel(
            string pcHttpClientName = DEFAULT_HTTP,
            string pcRequestServiceEndPoint = DEFAULT_ENDPOINT,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        public async Task<List<PMT02620UtilitiesDTO>> GetAgreementUtilitiesStreamAsync(PMT02620UtilitiesDTO poEntity)
        {
            var loEx = new R_Exception();
            List<PMT02620UtilitiesDTO> loResult = null;

            try
            {
                //Set Context
                R_FrontContext.R_SetStreamingContext(ContextConstant.PMT02620_UTITLITIES_GET_AGREEMENT_UTILITIES_STREAMING_CONTEXT, poEntity);

                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMT02620UtilitiesDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT02620Utilities.GetAgreementUtilitiesStream),
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

        public async Task<List<CodeDescDTO>> GetUtilitiyChargesTypeStreamAsync()
        {
            var loEx = new R_Exception();
            List<CodeDescDTO> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<CodeDescDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT02620Utilities.GetUtilitiyChargesType),
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

        public async Task<List<PMT02620AgreementBuildingUtilitiesDTO>> GetAllBuildingUtilitiesListAsync(PMT02620ParameterAgreementBuildingUtilitiesDTO poEntity)
        {
            var loEx = new R_Exception();
            List<PMT02620AgreementBuildingUtilitiesDTO> loResult = null;

            try
            {
                //Set Context
                R_FrontContext.R_SetStreamingContext(ContextConstant.PMT02620_UTITLITIES_METER_NO_STREAMING_CONTEXT, new PMT02620ParameterAgreementBuildingUtilitiesDTO()
                {
                    CPROPERTY_ID = poEntity.CPROPERTY_ID,
                    CBUILDING_ID = poEntity.CBUILDING_ID,
                    CFLOOR_ID = poEntity.CFLOOR_ID,
                    COTHER_UNIT_ID = poEntity.COTHER_UNIT_ID,
                    CCHARGES_TYPE = poEntity.CCHARGES_TYPE,
                });

                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMT02620AgreementBuildingUtilitiesDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT02620Utilities.GetAllBuildingUtilitiesList),
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
        public IAsyncEnumerable<PMT02620UtilitiesDTO> GetAgreementUtilitiesStream()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<CodeDescDTO> GetUtilitiyChargesType()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<PMT02620AgreementBuildingUtilitiesDTO> GetAllBuildingUtilitiesList()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
