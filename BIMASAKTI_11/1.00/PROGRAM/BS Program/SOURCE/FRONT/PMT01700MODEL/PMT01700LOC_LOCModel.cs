using PMT01700COMMON.DTO._3._LOC._2._LOC;
using PMT01700COMMON.DTO.Utilities;
using PMT01700COMMON.Interface;
using PMT01700COMMON.Interface.LOC;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PMT01700MODEL
{
    internal class PMT01700LOC_LOCModel : R_BusinessObjectServiceClientBase<PMT010700_LOC_LOC_SelectedLOCDTO>, IPMT01700LOC_LOC
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlPM";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/PMT01700LOC_LOC";
        private const string DEFAULT_MODULE = "PM";

        public PMT01700LOC_LOCModel(
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
        #region just Implements

        public PMT01700VarGsmTransactionCodeDTO GetVAR_GSM_TRANSACTION_CODE()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
