using PMT01100Common.Context._2._LOO._3._Unit___Charges._1._Unit___Charges;
using PMT01100Common.DTO._2._LOO._3._LOO___Unit___Charges._1._LOO___Unit___Charges___Unit___Charges;
using PMT01100Common.DTO._2._LOO._4._LOO___Deposit;
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
using PMT01100Common.Context._2._LOO._4._Deposit;
using PMT01100Common.Utilities.Response;
using PMT01100Common.Utilities;

namespace PMT01100Model
{
    public class PMT01100LOO_DepositModel : R_BusinessObjectServiceClientBase<PMT01100LOO_Deposit_DepositDetailDTO>, IPMT01100LOO_Deposit
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlPM";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/PMT01100LOO_Deposit";
        private const string DEFAULT_MODULE = "PM";

        public PMT01100LOO_DepositModel(
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

        public async Task<PMT01100LOO_Deposit_DepositHeaderDTO> GetDepositHeaderAsync(PMT01100LOO_Deposit_RequestDepositHeaderDTO poParameter)
        {
            var loEx = new R_Exception();
            PMT01100LOO_Deposit_DepositHeaderDTO loResult = new PMT01100LOO_Deposit_DepositHeaderDTO();

            try
            {
                if (!string.IsNullOrEmpty(poParameter.CPROPERTY_ID))
                {
                    R_HTTPClientWrapper.httpClientName = _HttpClientName;
                    loResult = await R_HTTPClientWrapper.R_APIRequestObject<PMT01100LOO_Deposit_DepositHeaderDTO, PMT01100LOO_Deposit_RequestDepositHeaderDTO>(
                        _RequestServiceEndPoint,
                        nameof(IPMT01100LOO_Deposit.GetDepositHeader),
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

        public async Task<PMT01100UtilitiesListDTO<PMT01100LOO_Deposit_DepositListDTO>> GetDepositListAsync(PMT01100UtilitiesParameterDepositListDTO poParameter)
        {
            var loEx = new R_Exception();
            PMT01100UtilitiesListDTO<PMT01100LOO_Deposit_DepositListDTO> loResult = new PMT01100UtilitiesListDTO<PMT01100LOO_Deposit_DepositListDTO>();
            try
            {
                if (!string.IsNullOrEmpty(poParameter.CREF_NO))
                {
                    R_FrontContext.R_SetStreamingContext(PMT01100UtilitiesParameterDepositListContextDTO.CPROPERTY_ID, poParameter.CPROPERTY_ID);
                    R_FrontContext.R_SetStreamingContext(PMT01100UtilitiesParameterDepositListContextDTO.CDEPT_CODE, poParameter.CDEPT_CODE);
                    R_FrontContext.R_SetStreamingContext(PMT01100UtilitiesParameterDepositListContextDTO.CTRANS_CODE, poParameter.CTRANS_CODE);
                    R_FrontContext.R_SetStreamingContext(PMT01100UtilitiesParameterDepositListContextDTO.CREF_NO, poParameter.CREF_NO);
                    R_FrontContext.R_SetStreamingContext(PMT01100UtilitiesParameterDepositListContextDTO.CBUILDING_ID, poParameter.CBUILDING_ID);

                    R_HTTPClientWrapper.httpClientName = _HttpClientName;
                    var loTempResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMT01100LOO_Deposit_DepositListDTO>(
                        _RequestServiceEndPoint,
                        nameof(IPMT01100LOO_Deposit.GetDepositList),
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

        public async Task<PMT01100UtilitiesListDTO<PMT01100ResponseUtilitiesCurrencyParameterDTO>> GetComboBoxDataCurrencyAsync()
        {
            var loEx = new R_Exception();
            PMT01100UtilitiesListDTO<PMT01100ResponseUtilitiesCurrencyParameterDTO> loResult = new PMT01100UtilitiesListDTO<PMT01100ResponseUtilitiesCurrencyParameterDTO>();
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMT01100ResponseUtilitiesCurrencyParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT01100LOO_Deposit.GetComboBoxDataCurrency),
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

        #region Not Used!

        public PMT01100LOO_Deposit_DepositHeaderDTO GetDepositHeader(PMT01100LOO_Deposit_RequestDepositHeaderDTO poParameter)
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<PMT01100LOO_Deposit_DepositListDTO> GetDepositList()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<PMT01100ResponseUtilitiesCurrencyParameterDTO> GetComboBoxDataCurrency()
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}
