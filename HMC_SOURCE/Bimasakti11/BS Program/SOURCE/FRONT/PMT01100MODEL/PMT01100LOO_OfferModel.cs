using PMT01100Common.DTO._2._LOO._2._LOO___Offer;
using PMT01100Common.Interface;
using PMT01100Common.Utilities;
using PMT01100Common.Utilities.Request;
using PMT01100Common.Utilities.Response;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PMT01100Model
{
    public class PMT01100LOO_OfferModel : R_BusinessObjectServiceClientBase<PMT01100LOO_Offer_SelectedOfferDTO>, IPMT01100LOO_Offer
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlPM";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/PMT01100LOO_Offer";
        private const string DEFAULT_MODULE = "PM";

        public PMT01100LOO_OfferModel(
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


        public async Task<PMT01100VarGsmTransactionCodeDTO> GetVAR_GSM_TRANSACTION_CODEAsync()
        {
            var loEx = new R_Exception();
            var loResult = new PMT01100VarGsmTransactionCodeDTO();


            try
            {

                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<PMT01100VarGsmTransactionCodeDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT01100LOO_Offer.GetVAR_GSM_TRANSACTION_CODE),
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

        public async Task<PMT01100UtilitiesListDTO<PMT01100ResponseTenantCategoryDTO>> GetComboBoxDataTenantCategoryAsync(PMT01100RequestTenantCategoryDTO poParameter)
        {
            var loEx = new R_Exception();
            PMT01100UtilitiesListDTO<PMT01100ResponseTenantCategoryDTO> loResult = new PMT01100UtilitiesListDTO<PMT01100ResponseTenantCategoryDTO>();

            try
            {
                if (!string.IsNullOrEmpty(poParameter.CPROPERTY_ID))
                {
                    R_HTTPClientWrapper.httpClientName = _HttpClientName;
                    var loTempResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMT01100ResponseTenantCategoryDTO, PMT01100RequestTenantCategoryDTO>(
                        _RequestServiceEndPoint,
                        nameof(IPMT01100LOO_Offer.GetComboBoxDataTenantCategory),
                        poParameter,
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

        public async Task<PMT01100UtilitiesListDTO<PMT01100ComboBoxDTO>> GetComboBoxDataTaxTypeAsync()
        {
            var loEx = new R_Exception();
            PMT01100UtilitiesListDTO<PMT01100ComboBoxDTO> loResult = new PMT01100UtilitiesListDTO<PMT01100ComboBoxDTO>();

            try
            {

                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMT01100ComboBoxDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT01100LOO_Offer.GetComboBoxDataTaxType),
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

        public async Task<PMT01100UtilitiesListDTO<PMT01100ComboBoxDTO>> GetComboBoxDataIDTypeAsync()
        {
            var loEx = new R_Exception();
            PMT01100UtilitiesListDTO<PMT01100ComboBoxDTO> loResult = new PMT01100UtilitiesListDTO<PMT01100ComboBoxDTO>();

            try
            {

                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMT01100ComboBoxDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT01100LOO_Offer.GetComboBoxDataIDType),
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

        public PMT01100VarGsmTransactionCodeDTO GetVAR_GSM_TRANSACTION_CODE()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<PMT01100ResponseTenantCategoryDTO> GetComboBoxDataTenantCategory(PMT01100RequestTenantCategoryDTO poParam)
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<PMT01100ComboBoxDTO> GetComboBoxDataTaxType()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<PMT01100ComboBoxDTO> GetComboBoxDataIDType()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
