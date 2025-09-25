using PMT00100COMMON.Booking;
using PMT00100COMMON.ChangeUnit;
using PMT00100COMMON.Interface;
using PMT00100COMMON.UnitList;
using PMT00100COMMON.UtilityDTO;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading.Tasks;

namespace PMT00100MODEL
{
    public class PMT00100BookingModel : R_BusinessObjectServiceClientBase<PMT00100BookingDTO>, IPMT00100Booking
    {
        private const string DEFAULT_HTTP = "R_DefaultServiceUrlPM";
        private const string DEFAULT_ENDPOINT = "api/PMT00100Booking";
        private const string DEFAULT_MODULE = "PM";
        public PMT00100BookingModel(
            string pcHttpClientName = DEFAULT_HTTP,
            string pcRequestServiceEndPoint = DEFAULT_ENDPOINT,
            string pcModuleName = DEFAULT_MODULE,
            bool plSendWithContext = true,
            bool plSendWithToken = true)
            : base(pcHttpClientName, pcRequestServiceEndPoint, pcModuleName, plSendWithContext, plSendWithToken)
        {
        }
        public async Task<VarGsmTransactionCodeDTO> GetVAR_GSM_TRANSACTION_CODEAsync()
        {
            var loEx = new R_Exception();
            var loResult = new VarGsmTransactionCodeDTO();
            try
            {

                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<VarGsmTransactionCodeDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT00100Booking.GetVAR_GSM_TRANSACTION_CODE),
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
        public async Task<PMT00100BookingDTO> GetAgreementDetailAsync(PMT00100BookingDTO poParam)
        {
            var loEx = new R_Exception();
            var loResult = new PMT00100BookingDTO();
            try
            {

                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<PMT00100BookingDTO, PMT00100BookingDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT00100Booking.GetAgreementDetail),
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
        public async Task<AgreementProcessDTO> UpdateAgreementAsync(AgreementProcessDTO poEntity)
        {
            var loEx = new R_Exception();
            AgreementProcessDTO loResult = new AgreementProcessDTO();
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<AgreementProcessDTO, AgreementProcessDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT00100Booking.UpdateAgreement),
                    poEntity,
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken
                );

                loResult = loTempResult;


            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult!;
        }
        //Implement Library
        public PMT00100BookingDTO GetAgreementDetail(PMT00100BookingDTO poParam)
        {
            throw new NotImplementedException();
        }

        public VarGsmTransactionCodeDTO GetVAR_GSM_TRANSACTION_CODE()
        {
            throw new NotImplementedException();
        }

        public AgreementProcessDTO UpdateAgreement(AgreementProcessDTO poEntity)
        {
            throw new NotImplementedException();
        }
    }
}
