using PMT01700COMMON.DTO._2._LOO._3._LOO___Unit___Charges.LOO___Unit___Charges___Unit___Charges;
using PMT01700COMMON.DTO.Utilities;
using PMT01700COMMON.DTO.Utilities.ParamDb.LOO;
using PMT01700COMMON.DTO.Utilities.Response;
using PMT01700COMMON.Interface;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading.Tasks;

namespace PMT01700MODEL
{
    public class PMT01700LOO_UnitCharges_UtilitiesModel : R_BusinessObjectServiceClientBase<PMT01700LOO_UnitUtilities_UnitUtilities_UtilitiesDTO>, IPMT01700LOO_UnitUtilities_Utilities

    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlPM";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/PMT01700_LOO_UnitUtilities_Utilities";
        private const string DEFAULT_MODULE = "PM";

        public PMT01700LOO_UnitCharges_UtilitiesModel(
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
        { }

        public async Task<PMT01700GenericList<PMT01700LOO_UnitUtilities_UnitUtilities_UtilitiesDTO>> GetUtilitiesListAsync()
        {
            var loEx = new R_Exception();
            PMT01700GenericList<PMT01700LOO_UnitUtilities_UnitUtilities_UtilitiesDTO> loResult = new PMT01700GenericList<PMT01700LOO_UnitUtilities_UnitUtilities_UtilitiesDTO>();
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMT01700LOO_UnitUtilities_UnitUtilities_UtilitiesDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT01700LOO_UnitUtilities_Utilities.GetUtilitiesList),
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
        public async Task<PMT01700GenericList<PMT01700ResponseUtilitiesCMeterNoParameterDTO>> GetComboBoxDataCMETER_NOAsync(PMT01700LOO_UnitUtilities_ParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            PMT01700GenericList<PMT01700ResponseUtilitiesCMeterNoParameterDTO> loResult = new PMT01700GenericList<PMT01700ResponseUtilitiesCMeterNoParameterDTO >();
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMT01700ResponseUtilitiesCMeterNoParameterDTO, PMT01700LOO_UnitUtilities_ParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT01700LOO_UnitUtilities_Utilities.GetComboBoxDataCMETER_NO),
                    poParameter,
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

        public async Task<PMT01700GenericList<PMT01700ComboBoxDTO>> GetComboBoxDataCCHARGES_TYPEAsync()
        {
            var loEx = new R_Exception();
            PMT01700GenericList<PMT01700ComboBoxDTO> loResult = new PMT01700GenericList<PMT01700ComboBoxDTO>();
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMT01700ComboBoxDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT01700LOO_UnitUtilities_Utilities.GetComboBoxDataCCHARGES_TYPE),
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


        #region just Impelment
        public IAsyncEnumerable<PMT01700ComboBoxDTO> GetComboBoxDataCCHARGES_TYPE()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<PMT01700ResponseUtilitiesCMeterNoParameterDTO> GetComboBoxDataCMETER_NO(PMT01700LOO_UnitUtilities_ParameterDTO poParameter)
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<PMT01700LOO_UnitUtilities_UnitUtilities_UtilitiesDTO> GetUtilitiesList()
        {
            throw new NotImplementedException();
        }


        #endregion
    }
}
