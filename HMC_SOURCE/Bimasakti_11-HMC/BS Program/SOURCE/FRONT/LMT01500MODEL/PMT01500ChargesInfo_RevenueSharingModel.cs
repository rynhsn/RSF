using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PMT01500Common.Context;
using PMT01500Common.DTO._4._Charges_Info;
using PMT01500Common.Interface;
using PMT01500Common.Utilities;
using R_APIClient;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;

namespace PMT01500Model
{
    public class PMT01500ChargesInfo_RevenueSharingModel : R_BusinessObjectServiceClientBase<PMT01500ChargesInfo_RevenueSharingSchemeOriginalDTO>, IPMT01500ChargesInfo_RevenueSharing
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlPM";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/PMT01500ChargesInfo_RevenueSharing";
        private const string DEFAULT_MODULE = "PM";

        public PMT01500ChargesInfo_RevenueSharingModel(
            string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            string pcModule = DEFAULT_MODULE,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, pcModule, plSendWithContext, plSendWithToken)
        {
        }

        public async Task<List<PMT01500ChargesInfo_RevenueSharingSchemeOriginalDTO>> GetRevenueSharingSchemeListAsync(PMT01500ParameterForChargesInfo_RevenueSharingDTO poParameter)
        {
            var loEx = new R_Exception();
            List<PMT01500ChargesInfo_RevenueSharingSchemeOriginalDTO>? loResult = null;

            try
            {
                R_FrontContext.R_SetStreamingContext(PMT01500GetAgreementChargesInfo_RevenueSharingContextDTO.CPROPERTY_ID, poParameter.CPROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(PMT01500GetAgreementChargesInfo_RevenueSharingContextDTO.CDEPT_CODE, poParameter.CDEPT_CODE);
                R_FrontContext.R_SetStreamingContext(PMT01500GetAgreementChargesInfo_RevenueSharingContextDTO.CREF_NO, poParameter.CREF_NO);
                R_FrontContext.R_SetStreamingContext(PMT01500GetAgreementChargesInfo_RevenueSharingContextDTO.CTRANS_CODE, poParameter.CTRANS_CODE);
                R_FrontContext.R_SetStreamingContext(PMT01500GetAgreementChargesInfo_RevenueSharingContextDTO.CCHARGE_SEQ_NO, poParameter.CCHARGE_SEQ_NO);
                /*
                R_FrontContext.R_SetStreamingContext(PMT01500GetHeaderParameterContextConstantDTO.CCHARGE_MODE, poParameter.CCHARGE_MODE);
                R_FrontContext.R_SetStreamingContext(PMT01500GetHeaderParameterContextConstantDTO.CBUILDING_ID, poParameter.CBUILDING_ID);
                R_FrontContext.R_SetStreamingContext(PMT01500GetHeaderParameterContextConstantDTO.CFLOOR_ID, poParameter.CFLOOR_ID);
                R_FrontContext.R_SetStreamingContext(PMT01500GetHeaderParameterContextConstantDTO.CUNIT_ID, poParameter.CUNIT_ID);
                */

                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMT01500ChargesInfo_RevenueSharingSchemeOriginalDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT01500ChargesInfo_RevenueSharing.GetRevenueSharingSchemeList),
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

#pragma warning disable CS8603 // Possible null reference return.
            return loResult;
#pragma warning restore CS8603 // Possible null reference return.
        }

        public async Task<List<PMT01500ChargesInfo_RevenueMinimumRentDTO>> GetRevenueMinimumRentListAsync(PMT01500ParameterForChargesInfo_RevenueSharingDTO poParameter)
        {
            var loEx = new R_Exception();
            List<PMT01500ChargesInfo_RevenueMinimumRentDTO>? loResult = null;

            try
            {
                R_FrontContext.R_SetStreamingContext(PMT01500GetAgreementChargesInfo_RevenueSharingContextDTO.CPROPERTY_ID, poParameter.CPROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(PMT01500GetAgreementChargesInfo_RevenueSharingContextDTO.CDEPT_CODE, poParameter.CDEPT_CODE);
                R_FrontContext.R_SetStreamingContext(PMT01500GetAgreementChargesInfo_RevenueSharingContextDTO.CREF_NO, poParameter.CREF_NO);
                R_FrontContext.R_SetStreamingContext(PMT01500GetAgreementChargesInfo_RevenueSharingContextDTO.CTRANS_CODE, poParameter.CTRANS_CODE);
                R_FrontContext.R_SetStreamingContext(PMT01500GetAgreementChargesInfo_RevenueSharingContextDTO.CCHARGE_SEQ_NO, poParameter.CCHARGE_SEQ_NO);

                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMT01500ChargesInfo_RevenueMinimumRentDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT01500ChargesInfo_RevenueSharing.GetRevenueMinimumRentList),
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

#pragma warning disable CS8603 // Possible null reference return.
            return loResult;
#pragma warning restore CS8603 // Possible null reference return.
        }


        #region Not Used!

        public IAsyncEnumerable<PMT01500ChargesInfo_RevenueMinimumRentDTO> GetRevenueMinimumRentList()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<PMT01500ChargesInfo_RevenueSharingSchemeOriginalDTO> GetRevenueSharingSchemeList()
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}