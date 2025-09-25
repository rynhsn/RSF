using PMT01700COMMON.DTO._2._LOO._2._LOO___Offer;
using PMT01700COMMON.DTO.Utilities;
using PMT01700COMMON.DTO.Utilities.ParamDb;
using PMT01700COMMON.DTO.Utilities.ParamDb.LOO;
using PMT01700COMMON.DTO.Utilities.Response;
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
    public class PMT01700LOO_OfferModel : R_BusinessObjectServiceClientBase<PMT01700LOO_Offer_SelectedOfferDTO>, IPMT01700LOO_Offer
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlPM";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/PMT01700LOO_Offer";
        private const string DEFAULT_MODULE = "PM";

        public PMT01700LOO_OfferModel(
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

        public async Task<PMT01700VarGsmTransactionCodeDTO> GetVAR_GSM_TRANSACTION_CODEAsync()
        {
            var loEx = new R_Exception();
            var loResult = new PMT01700VarGsmTransactionCodeDTO();
            try
            {

                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<PMT01700VarGsmTransactionCodeDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT01700LOO_Offer.GetVAR_GSM_TRANSACTION_CODE),
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
        public async Task<PMT01700GenericList<PMT01700ComboBoxDTO>> GetComboBoxDataIDTypeAsync()
        {
            var loEx = new R_Exception();
            var loResult = new PMT01700GenericList<PMT01700ComboBoxDTO>();
            try
            {

                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMT01700ComboBoxDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT01700LOO_Offer.GetComboBoxDataIDType),
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
        public async Task<PMT01700GenericList<PMT01700ComboBoxDTO>> GetComboBoxDataTaxTypeAsync()
        {
            var loEx = new R_Exception();
            var loResult = new PMT01700GenericList<PMT01700ComboBoxDTO>();
            try
            {

                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMT01700ComboBoxDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT01700LOO_Offer.GetComboBoxDataTaxType),
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
        public async Task<PMT01700GenericList<PMT01700ResponseTenantCategoryDTO>> GetComboBoxDataTenantCategoryAsync(PMT01700BaseParameterDTO poParam)
        {
            var loEx = new R_Exception();
            var loResult = new PMT01700GenericList<PMT01700ResponseTenantCategoryDTO>();
            try
            {

                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMT01700ResponseTenantCategoryDTO, PMT01700BaseParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT01700LOO_Offer.GetComboBoxDataTenantCategory),
                    poParam,
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
        public async Task<PMT01700LOO_Offer_TenantDetailDTO> GetTenantDetailAsync(PMT01700LOO_Offer_TenantParamDTO poParam)
        {
            var loEx = new R_Exception();
            var loResult = new PMT01700LOO_Offer_TenantDetailDTO();
            try
            {

                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<PMT01700LOO_Offer_TenantDetailDTO, PMT01700LOO_Offer_TenantParamDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT01700LOO_Offer.GetTenantDetail),
                    poParam,
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
        public async Task<PMT01700LOO_Offer_SelectedOfferDTO> GetAgreementDetailAsync(PMT01700LOO_Offer_SelectedOfferDTO poParam)
        {
            var loEx = new R_Exception();
            var loResult = new PMT01700LOO_Offer_SelectedOfferDTO();
            try
            {

                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<PMT01700LOO_Offer_SelectedOfferDTO, PMT01700LOO_Offer_SelectedOfferDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT01700LOO_Offer.GetAgreementDetail),
                    poParam,
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

        #region Just Implement
        public IAsyncEnumerable<PMT01700ResponseTenantCategoryDTO> GetComboBoxDataTenantCategory(PMT01700BaseParameterDTO poParam)
        {
            throw new NotImplementedException();
        }
        public IAsyncEnumerable<PMT01700ComboBoxDTO> GetComboBoxDataIDType()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<PMT01700ComboBoxDTO> GetComboBoxDataTaxType()
        {
            throw new NotImplementedException();
        }


        public PMT01700VarGsmTransactionCodeDTO GetVAR_GSM_TRANSACTION_CODE()
        {
            throw new NotImplementedException();
        }

        public PMT01700LOO_Offer_TenantDetailDTO GetTenantDetail(PMT01700LOO_Offer_TenantParamDTO poParam)
        {
            throw new NotImplementedException();
        }

        public PMT01700LOO_Offer_SelectedOfferDTO GetAgreementDetail(PMT01700LOO_Offer_SelectedOfferDTO poParam)
        {
            throw new NotImplementedException();
        }


        #endregion
    }
    }
