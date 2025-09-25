using PMT02500Common.Context;
using PMT02500Common.DTO._4._Charges_Info.Db;
using PMT02500Common.DTO._4._Charges_Info.Revenue_Sharing_Process;
using PMT02500Common.Interface;
using PMT02500Common.Utilities;
using R_APIClient;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PMT02500Model
{
    public class PMT02500UnitCharges_Charges_RevenueSharingHDModel : R_BusinessObjectServiceClientBase<PMT02500UnitCharges_Charges_RevenueSharingHDDTO>, IPMT02500UnitCharges_Charges_RevenueSharingHD
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlPM";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/PMT02500UnitCharges_Charges_RevenueSharingHD";
        private const string DEFAULT_MODULE = "PM";

        public PMT02500UnitCharges_Charges_RevenueSharingHDModel(
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


        public async Task<PMT02500UtilitiesListDTO<PMT02500UnitCharges_Charges_RevenueSharingHDDTO>> GetRevenueSharingHDListAsync(PMT02500UtilitiesParameterChargesInfo_RevenueSharingDTO poParameter)
        {
            var loEx = new R_Exception();
            PMT02500UtilitiesListDTO<PMT02500UnitCharges_Charges_RevenueSharingHDDTO> loResult = new PMT02500UtilitiesListDTO<PMT02500UnitCharges_Charges_RevenueSharingHDDTO>();
            try
            {
                if (poParameter != null)
                {

                    R_FrontContext.R_SetStreamingContext(PMT02500GetAgreementChargesInfo_RevenueSharingSchemeContextDTO.CPROPERTY_ID, poParameter.CPROPERTY_ID);
                    R_FrontContext.R_SetStreamingContext(PMT02500GetAgreementChargesInfo_RevenueSharingSchemeContextDTO.CDEPT_CODE, poParameter.CDEPT_CODE);
                    R_FrontContext.R_SetStreamingContext(PMT02500GetAgreementChargesInfo_RevenueSharingSchemeContextDTO.CTRANS_CODE, poParameter.CTRANS_CODE);
                    R_FrontContext.R_SetStreamingContext(PMT02500GetAgreementChargesInfo_RevenueSharingSchemeContextDTO.CREF_NO, poParameter.CREF_NO);
                    R_FrontContext.R_SetStreamingContext(PMT02500GetAgreementChargesInfo_RevenueSharingSchemeContextDTO.CCHARGE_SEQ_NO, poParameter.CCHARGE_SEQ_NO);

                    R_HTTPClientWrapper.httpClientName = _HttpClientName;
                    var loTempResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMT02500UnitCharges_Charges_RevenueSharingHDDTO>(
                        _RequestServiceEndPoint,
                        nameof(IPMT02500UnitCharges_Charges_RevenueSharingHD.GetRevenueSharingHDList),
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

        public IAsyncEnumerable<PMT02500UnitCharges_Charges_RevenueSharingHDDTO> GetRevenueSharingHDList()
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}
