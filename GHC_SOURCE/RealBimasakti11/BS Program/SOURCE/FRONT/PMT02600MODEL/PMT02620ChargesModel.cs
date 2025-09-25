using PMT01400COMMON.DTOs.Helper;
using PMT02600COMMON;
using PMT02600COMMON.DTOs;
using PMT02600COMMON.DTOs.PMT02620;
using R_APIClient;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PMT02600MODEL
{
    public class PMT02620ChargesModel : R_BusinessObjectServiceClientBase<PMT02620ChargesDTO>, IPMT02620Charges
    {
        private const string DEFAULT_HTTP = "R_DefaultServiceUrlPM";
        private const string DEFAULT_ENDPOINT = "api/PMT02620Charges";
        private const string DEFAULT_MODULE = "PM";

        public PMT02620ChargesModel(
            string pcHttpClientName = DEFAULT_HTTP,
            string pcRequestServiceEndPoint = DEFAULT_ENDPOINT,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        public async Task<List<PMT02620ChargesDTO>> GetAgreementChargeStreamAsync(PMT02620ChargesDTO poEntity)
        {
            var loEx = new R_Exception();
            List<PMT02620ChargesDTO> loResult = null;

            try
            {
                //Set Context
                R_FrontContext.R_SetStreamingContext(ContextConstant.PMT02620_CHARGES_GET_AGREEMENT_CHARGE_STREAMING_CONTEXT, new PMT02620ChargesDTO()
                {
                    CPROPERTY_ID = poEntity.CPROPERTY_ID,
                    CDEPT_CODE = poEntity.CDEPT_CODE,
                    CREF_NO = poEntity.CREF_NO,
                    CCHARGE_MODE = poEntity.CCHARGE_MODE,
                    CUNIT_ID = poEntity.CUNIT_ID,
                    CFLOOR_ID = poEntity.CFLOOR_ID,
                    CBUILDING_ID = poEntity.CBUILDING_ID,
                    CTRANS_CODE = poEntity.CTRANS_CODE
                });

                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMT02620ChargesDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT02620Charges.GetAgreementChargeStream),
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

        public async Task<List<PMT02620AgreementChargeCalUnitDTO>> GetAllAgreementChargeCallUnitListStreamAsync(PMT02620ParameterAgreementChargeCalUnitDTO poEntity)
        {
            var loEx = new R_Exception();
            List<PMT02620AgreementChargeCalUnitDTO> loResult = null;

            try
            {
                //Set Context
                R_FrontContext.R_SetStreamingContext(ContextConstant.PMT02620_CHARGES_FOR_TOTAL_AREA_STREAMING_CONTEXT, new PMT02620ParameterAgreementChargeCalUnitDTO()
                {
                    CPROPERTY_ID = poEntity.CPROPERTY_ID,
                    CDEPT_CODE = poEntity.CDEPT_CODE,
                    CREF_NO = poEntity.CREF_NO,
                    CSEQ_NO = poEntity.CSEQ_NO,
                    CTRANS_CODE = poEntity.CTRANS_CODE,
                });

                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMT02620AgreementChargeCalUnitDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT02620Charges.GetAllAgreementChargeCallUnitList),
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

        public async Task<List<CodeDescDTO>> GetFeeMethodStreamAsync()
        {
            var loEx = new R_Exception();
            List<CodeDescDTO> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<CodeDescDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT02620Charges.GetFeeMethod),
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
        public IAsyncEnumerable<PMT02620ChargesDTO> GetAgreementChargeStream()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<PMT02620AgreementChargeCalUnitDTO> GetAllAgreementChargeCallUnitList()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<CodeDescDTO> GetFeeMethod()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
