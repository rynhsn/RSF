using PMT01100Common.Context._2._LOO._3._Unit___Charges._1._Unit___Charges;
using PMT01100Common.DTO._2._LOO._3._LOO___Unit___Charges._1._LOO___Unit___Charges___Unit___Charges;
using PMT01100Common.DTO._2._LOO._3._LOO___Unit___Charges._2._LOO___Unit___Charges___Utilities;
using PMT01100Common.Interface;
using PMT01100Common.Utilities;
using PMT01100Common.Utilities.Db;
using PMT01100Common.Utilities.Request;
using PMT01100Common.Utilities.Response;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd;
using R_BusinessObjectFront;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PMT01100Common.Context._2._LOO._3._Unit___Charges._2._Utilities;

namespace PMT01100Model
{
    public class PMT01100LOO_UnitCharges_UtilitiesModel : R_BusinessObjectServiceClientBase<PMT01100LOO_UnitCharges_Utilities_UtilitiesListDTO>, IPMT01100LOO_UnitCharges_Utilities
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlPM";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/PMT01100LOO_UnitCharges_Utilities";
        private const string DEFAULT_MODULE = "PM";

        public PMT01100LOO_UnitCharges_UtilitiesModel(
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

        public async Task<PMT01100UtilitiesListDTO<PMT01100ComboBoxDTO>> GetComboBoxDataCCHARGES_TYPEAsync()
        {
            var loEx = new R_Exception();
            PMT01100UtilitiesListDTO<PMT01100ComboBoxDTO> loResult = new PMT01100UtilitiesListDTO<PMT01100ComboBoxDTO>();
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMT01100ComboBoxDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT01100LOO_UnitCharges_Utilities.GetComboBoxDataCCHARGES_TYPE),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken
                );
                loResult.Data = loTempResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult!;
        }

        public async Task<PMT01100UtilitiesListDTO<PMT01100ResponseUtilitiesCMeterNoParameterDTO>> GetComboBoxDataCMETER_NOAsync(PMT01100RequestUtilitiesCMeterNoParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            PMT01100UtilitiesListDTO<PMT01100ResponseUtilitiesCMeterNoParameterDTO> loResult = new PMT01100UtilitiesListDTO<PMT01100ResponseUtilitiesCMeterNoParameterDTO>();
            PMT01100RequestUtilitiesCMeterNoParameterDTO loParameter;

            try
            {
                if (!string.IsNullOrEmpty(poParameter.CUNIT_ID))
                {
                    loParameter = poParameter;

                    R_HTTPClientWrapper.httpClientName = _HttpClientName;
                    var loTempResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMT01100ResponseUtilitiesCMeterNoParameterDTO, PMT01100RequestUtilitiesCMeterNoParameterDTO>(
                        _RequestServiceEndPoint,
                        nameof(IPMT01100LOO_UnitCharges_Utilities.GetComboBoxDataCMETER_NO),
                        loParameter,
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

        public async Task<PMT01100UtilitiesListDTO<PMT01100LOO_UnitCharges_Utilities_UtilitiesListDTO>> GetUtilitiesListAsync(PMT01100UtilitiesParameterUtilitiesListDTO poParameter)
        {
            var loEx = new R_Exception();
            PMT01100UtilitiesListDTO<PMT01100LOO_UnitCharges_Utilities_UtilitiesListDTO> loResult = new PMT01100UtilitiesListDTO<PMT01100LOO_UnitCharges_Utilities_UtilitiesListDTO>();
            try
            {
                if (!string.IsNullOrEmpty(poParameter.CREF_NO))
                {
                    R_FrontContext.R_SetStreamingContext(PMT01100ParameterUtilitiesListContextDTO.CPROPERTY_ID, poParameter.CPROPERTY_ID);
                    R_FrontContext.R_SetStreamingContext(PMT01100ParameterUtilitiesListContextDTO.CDEPT_CODE, poParameter.CDEPT_CODE);
                    R_FrontContext.R_SetStreamingContext(PMT01100ParameterUtilitiesListContextDTO.CTRANS_CODE, poParameter.CTRANS_CODE);
                    R_FrontContext.R_SetStreamingContext(PMT01100ParameterUtilitiesListContextDTO.CREF_NO, poParameter.CREF_NO);
                    R_FrontContext.R_SetStreamingContext(PMT01100ParameterUtilitiesListContextDTO.CUNIT_ID, poParameter.CUNIT_ID);
                    R_FrontContext.R_SetStreamingContext(PMT01100ParameterUtilitiesListContextDTO.CFLOOR_ID, poParameter.CFLOOR_ID);
                    R_FrontContext.R_SetStreamingContext(PMT01100ParameterUtilitiesListContextDTO.CBUILDING_ID, poParameter.CBUILDING_ID);

                    R_HTTPClientWrapper.httpClientName = _HttpClientName;
                    var loTempResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMT01100LOO_UnitCharges_Utilities_UtilitiesListDTO>(
                        _RequestServiceEndPoint,
                        nameof(IPMT01100LOO_UnitCharges_Utilities.GetUtilitiesList),
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


        #region Not Used

        public IAsyncEnumerable<PMT01100ComboBoxDTO> GetComboBoxDataCCHARGES_TYPE()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<PMT01100ResponseUtilitiesCMeterNoParameterDTO> GetComboBoxDataCMETER_NO(PMT01100RequestUtilitiesCMeterNoParameterDTO poParameter)
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<PMT01100LOO_UnitCharges_Utilities_UtilitiesListDTO> GetUtilitiesList()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
