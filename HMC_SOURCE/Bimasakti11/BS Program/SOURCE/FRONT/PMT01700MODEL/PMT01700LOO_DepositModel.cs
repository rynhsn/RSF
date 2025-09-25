using PMT01700COMMON.DTO._2._LOO._2._LOO___Offer;
using PMT01700COMMON.DTO._2._LOO._4._LOO___Deposit;
using PMT01700COMMON.DTO.Utilities;
using PMT01700COMMON.DTO.Utilities.ParamDb.LOO;
using PMT01700COMMON.DTO.Utilities.Response;
using PMT01700COMMON.Interface;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PMT01700MODEL
{
    public class PMT01700LOO_DepositModel : R_BusinessObjectServiceClientBase<PMT01700LOO_Deposit_DepositDetailDTO>, IPMT01700LOO_Deposit
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlPM";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/PMT01700LOO_Deposit";
        private const string DEFAULT_MODULE = "PM";

        public PMT01700LOO_DepositModel(
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
        public async Task<PMT01700LOO_Deposit_DepositHeaderDTO> GetDepositHeaderAsync(PMT01700LOO_UnitUtilities_ParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            var loResult = new PMT01700LOO_Deposit_DepositHeaderDTO();
            try
            {

                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<PMT01700LOO_Deposit_DepositHeaderDTO, PMT01700LOO_UnitUtilities_ParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT01700LOO_Deposit.GetDepositHeader),
                    poParameter,
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken
                );
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            return loResult;
        }
        public async Task<PMT01700GenericList<PMT01700ResponseCurrencyParameterDTO>> GetComboBoxDataCurrencyAsync()
        {
            var loEx = new R_Exception();
            var loResult = new PMT01700GenericList<PMT01700ResponseCurrencyParameterDTO>();
            try
            {

                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMT01700ResponseCurrencyParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT01700LOO_Deposit.GetComboBoxDataCurrency),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken
                );
                loResult.Data = loTempResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);            }

            loEx.ThrowExceptionIfErrors();
            return loResult;
        }
        public async Task<PMT01700GenericList<PMT01700LOO_Deposit_DepositListDTO>> GetDepositListAsync()
        {
            var loEx = new R_Exception();
            var loResult = new PMT01700GenericList<PMT01700LOO_Deposit_DepositListDTO>();
            try
            {

                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMT01700LOO_Deposit_DepositListDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT01700LOO_Deposit.GetDepositList),
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
            return loResult;
        }
       
        #region Just Implements
        public IAsyncEnumerable<PMT01700ResponseCurrencyParameterDTO> GetComboBoxDataCurrency()
        {
            throw new NotImplementedException();
        }

        public PMT01700LOO_Deposit_DepositHeaderDTO GetDepositHeader(PMT01700LOO_UnitUtilities_ParameterDTO poParameter)
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<PMT01700LOO_Deposit_DepositListDTO> GetDepositList()
        {
            throw new NotImplementedException();
        }
        #endregion


    }
}
