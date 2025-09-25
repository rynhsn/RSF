using PMT01700COMMON.DTO._2._LOO._3._LOO___Unit___Charges.LOO___Unit___Charges___Charges;
using PMT01700COMMON.DTO._2._LOO._3._LOO___Unit___Charges.LOO___Unit___Charges___Unit___Charges;
using PMT01700COMMON.DTO.Utilities;
using PMT01700COMMON.Interface;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PMT01700MODEL
{
    public class PMT01700LOO_UnitCharges_ChargesModel : R_BusinessObjectServiceClientBase<PMT01700LOO_UnitCharges_ChargesDetailDTO>, IPMT01700LOO_UnitUtilities_Charges
    {

        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlPM";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/PMT01700LOO_UnitUtilities_Charges";
        private const string DEFAULT_MODULE = "PM";

        public PMT01700LOO_UnitCharges_ChargesModel(
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

        public async Task<PMT01700GenericList<PMT01700LOO_UnitCharges_ChargesListDTO>> GetChargesListAsync()
        {
            var loEx = new R_Exception();
            PMT01700GenericList<PMT01700LOO_UnitCharges_ChargesListDTO> loResult = new PMT01700GenericList<PMT01700LOO_UnitCharges_ChargesListDTO>();
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMT01700LOO_UnitCharges_ChargesListDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT01700LOO_UnitUtilities_Charges.GetChargesList),
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
        public async Task<PMT01700GenericList<PMT01700ComboBoxDTO>> GetFeeMethodListAsync()
        {
            var loEx = new R_Exception();
            PMT01700GenericList<PMT01700ComboBoxDTO> loResult = new PMT01700GenericList<PMT01700ComboBoxDTO>();
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMT01700ComboBoxDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT01700LOO_UnitUtilities_Charges.GetFeeMethodList),
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
        public async Task<PMT01700GenericList<PMT01700ComboBoxDTO>> GetPeriodModeListAsync()
        {
            var loEx = new R_Exception();
            PMT01700GenericList<PMT01700ComboBoxDTO> loResult = new PMT01700GenericList<PMT01700ComboBoxDTO>();
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMT01700ComboBoxDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT01700LOO_UnitUtilities_Charges.GetPeriodModeList),
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
        public async Task<PMT01700GenericList<PMT01700LOO_UnitCharges_Charges_ChargesItemDTO>> GetDetailItemListAsync()
        {
            var loEx = new R_Exception();
            PMT01700GenericList<PMT01700LOO_UnitCharges_Charges_ChargesItemDTO> loResult = new PMT01700GenericList<PMT01700LOO_UnitCharges_Charges_ChargesItemDTO>();
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMT01700LOO_UnitCharges_Charges_ChargesItemDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT01700LOO_UnitUtilities_Charges.GetDetailItemList),
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
        #region Just Implements
        public IAsyncEnumerable<PMT01700LOO_UnitCharges_ChargesListDTO> GetChargesList()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<PMT01700LOO_UnitCharges_Charges_ChargesItemDTO> GetDetailItemList()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<PMT01700ComboBoxDTO> GetFeeMethodList()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<PMT01700ComboBoxDTO> GetPeriodModeList()
        {
            throw new NotImplementedException();
        }
        #endregion
    }

}