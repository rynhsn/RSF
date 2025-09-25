using PMT01100Common.Context._1._Unit_List;
using PMT01100Common.DTO._1._Unit_List;
using PMT01100Common.DTO._2._LOO._2._LOO___Offer;
using PMT01100Common.DTO._2._LOO._3._LOO___Unit___Charges._1._LOO___Unit___Charges___Unit___Charges;
using PMT01100Common.Interface;
using PMT01100Common.Utilities.Db;
using PMT01100Common.Utilities.Request;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PMT01100Common.Utilities.Response;
using PMT01100Common.Context._2._LOO._3._Unit___Charges._1._Unit___Charges;
using PMT01100Common.Utilities;

namespace PMT01100Model
{
    public class PMT01100LOO_UnitCharges_UnitChargesModel : R_BusinessObjectServiceClientBase<PMT01100LOO_UnitCharges_UnitCharges_UnitInfoListDTO>, IPMT01100LOO_UnitCharges_UnitCharges
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlPM";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/PMT01100LOO_UnitCharges_UnitCharges";
        private const string DEFAULT_MODULE = "PM";

        public PMT01100LOO_UnitCharges_UnitChargesModel(
            string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            string pcModule = DEFAULT_MODULE,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(
                pcHttpClientName,
                pcRequestServiceEndPoint,
                pcModule,
                plSendWithContext,
                plSendWithToken)
        {
        }


        public async Task<PMT01100LOO_UnitCharges_UnitCharges_UnitChargesHeaderDTO> GetUnitChargesHeaderAsync(PMT01100LOO_UnitCharges_UnitCharges_RequestBodyUnitChargesHeaderDTO poParameter)
        {
            var loEx = new R_Exception();
            PMT01100LOO_UnitCharges_UnitCharges_UnitChargesHeaderDTO loResult = new PMT01100LOO_UnitCharges_UnitCharges_UnitChargesHeaderDTO();

            try
            {
                if (!string.IsNullOrEmpty(poParameter.CPROPERTY_ID))
                {
                    R_HTTPClientWrapper.httpClientName = _HttpClientName;
                    loResult = await R_HTTPClientWrapper.R_APIRequestObject<PMT01100LOO_UnitCharges_UnitCharges_UnitChargesHeaderDTO, PMT01100LOO_UnitCharges_UnitCharges_RequestBodyUnitChargesHeaderDTO>(
                        _RequestServiceEndPoint,
                        nameof(IPMT01100LOO_UnitCharges_UnitCharges.GetUnitChargesHeader),
                        poParameter,
                        DEFAULT_MODULE,
                        _SendWithContext,
                        _SendWithToken
                    );
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult!;
        }

        public async Task<PMT01100UtilitiesListDTO<PMT01100LOO_UnitCharges_UnitCharges_UnitInfoListDTO>> GetUnitInfoListAsync(PMT01100UtilitiesParameterUnitInfoListDTO poParameter)
        {
            var loEx = new R_Exception();
            PMT01100UtilitiesListDTO<PMT01100LOO_UnitCharges_UnitCharges_UnitInfoListDTO> loResult = new PMT01100UtilitiesListDTO<PMT01100LOO_UnitCharges_UnitCharges_UnitInfoListDTO>();
            try
            {
                if (!string.IsNullOrEmpty(poParameter.CREF_NO))
                {
                    R_FrontContext.R_SetStreamingContext(PMT01100ParameterUnitInfoListContextDTO.CPROPERTY_ID, poParameter.CPROPERTY_ID);
                    R_FrontContext.R_SetStreamingContext(PMT01100ParameterUnitInfoListContextDTO.CDEPT_CODE, poParameter.CDEPT_CODE);
                    R_FrontContext.R_SetStreamingContext(PMT01100ParameterUnitInfoListContextDTO.CTRANS_CODE, poParameter.CTRANS_CODE);
                    R_FrontContext.R_SetStreamingContext(PMT01100ParameterUnitInfoListContextDTO.CREF_NO, poParameter.CREF_NO);

                    R_HTTPClientWrapper.httpClientName = _HttpClientName;
                    var loTempResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMT01100LOO_UnitCharges_UnitCharges_UnitInfoListDTO>(
                        _RequestServiceEndPoint,
                        nameof(IPMT01100LOO_UnitCharges_UnitCharges.GetUnitInfoList),
                        DEFAULT_MODULE,
                        _SendWithContext,
                        _SendWithToken
                    );
                    loResult.Data = loTempResult;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult!;
        }

        public async Task<PMT01100UtilitiesListDTO<PMT01100LOO_UnitCharges_UnitCharges_ChargesListDTO>> GetChargesListAsync(PMT01100UtilitiesParameterChargesListDTO poParameter)
        {
            var loEx = new R_Exception();
            PMT01100UtilitiesListDTO<PMT01100LOO_UnitCharges_UnitCharges_ChargesListDTO> loResult = new PMT01100UtilitiesListDTO<PMT01100LOO_UnitCharges_UnitCharges_ChargesListDTO>();
            try
            {
                if (!string.IsNullOrEmpty(poParameter.CREF_NO))
                {

                    R_FrontContext.R_SetStreamingContext(PMT01100ParameterChargesListContextDTO.CPROPERTY_ID, poParameter.CPROPERTY_ID);
                    R_FrontContext.R_SetStreamingContext(PMT01100ParameterChargesListContextDTO.CDEPT_CODE, poParameter.CDEPT_CODE);
                    R_FrontContext.R_SetStreamingContext(PMT01100ParameterChargesListContextDTO.CTRANS_CODE, poParameter.CTRANS_CODE);
                    R_FrontContext.R_SetStreamingContext(PMT01100ParameterChargesListContextDTO.CREF_NO, poParameter.CREF_NO);
                    R_FrontContext.R_SetStreamingContext(PMT01100ParameterChargesListContextDTO.CBUILDING_ID, poParameter.CBUILDING_ID);

                    R_HTTPClientWrapper.httpClientName = _HttpClientName;
                    var loTempResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMT01100LOO_UnitCharges_UnitCharges_ChargesListDTO>(
                        _RequestServiceEndPoint,
                        nameof(IPMT01100LOO_UnitCharges_UnitCharges.GetChargesList),
                        DEFAULT_MODULE,
                        _SendWithContext,
                        _SendWithToken
                    );

                    loResult.Data = loTempResult;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult!;
        }

        #region Not Used!

        public PMT01100LOO_UnitCharges_UnitCharges_UnitChargesHeaderDTO GetUnitChargesHeader(PMT01100LOO_UnitCharges_UnitCharges_RequestBodyUnitChargesHeaderDTO poParameter)
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<PMT01100LOO_UnitCharges_UnitCharges_ChargesListDTO> GetChargesList()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<PMT01100LOO_UnitCharges_UnitCharges_UnitInfoListDTO> GetUnitInfoList()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
