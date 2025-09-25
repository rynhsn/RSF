using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PMT02500Common.DTO._2._Agreement;
using PMT02500Common.Interface;
using PMT02500Common.Utilities;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;

namespace PMT02500Model
{
    public class PMT02500AgreementModel : R_BusinessObjectServiceClientBase<PMT02500AgreementDetailDTO>, IPMT02500Agreement
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlPM";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/PMT02500Agreement";
        private const string DEFAULT_MODULE = "PM";

        public PMT02500AgreementModel(
            string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            string pcModule = DEFAULT_MODULE,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, pcModule, plSendWithContext, plSendWithToken)
        {
        }

        public async Task<List<PMT02500ComboBoxDTO>> GetComboBoxDataCChargesModeAsync()
        {
            var loEx = new R_Exception();
            List<PMT02500ComboBoxDTO>? loResult = null;

            try
            {

                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMT02500ComboBoxDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT02500Agreement.GetComboBoxDataCChargesMode),
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

        public async Task<List<PMT02500ComboBoxDTO>> GetComboBoxDataCLeaseModeAsync()
        {
            var loEx = new R_Exception();
            List<PMT02500ComboBoxDTO>? loResult = null;

            try
            {

                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMT02500ComboBoxDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT02500Agreement.GetComboBoxDataCLeaseMode),
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

        #region NotUsed        
        
        public IAsyncEnumerable<PMT02500ComboBoxDTO> GetComboBoxDataCChargesMode()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<PMT02500ComboBoxDTO> GetComboBoxDataCLeaseMode()
        {
            throw new NotImplementedException();
        }
        
        #endregion


    }
}