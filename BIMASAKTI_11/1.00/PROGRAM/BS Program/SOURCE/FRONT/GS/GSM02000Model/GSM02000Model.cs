using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GSM02000Common;
using GSM02000Common.DTOs;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;

namespace GSM02000Model
{
    public class GSM02000Model : R_BusinessObjectServiceClientBase<GSM02000DTO>, IGSM02000
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrl";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/GSM02000";

        public GSM02000Model(
            string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, plSendWithContext, plSendWithToken)
        {
        }

        public GSM02000ListDTO GetAllSalesTax()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<GSM02000GridDTO> GetAllSalesTaxStream()
        {
            throw new NotImplementedException();
        }

        public RoundingListDTO GetAllRounding()
        {
            throw new NotImplementedException();
        }

        #region GetRoundingMode

        public async Task<RoundingListDTO> GetRoundingModeAsync()
        {
            var loEx = new R_Exception();
            RoundingListDTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<RoundingListDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGSM02000.GetAllRounding),
                    _SendWithContext,
                    _SendWithToken);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        #endregion

        #region GetAllAsync

        public async Task<GSM02000ListDTO> GetAllAsync()
        {
            var loEx = new R_Exception();
            GSM02000ListDTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<GSM02000ListDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGSM02000.GetAllSalesTax),
                    _SendWithContext,
                    _SendWithToken);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        #endregion

        #region GetAllStreamAsync

        public async Task<List<GSM02000GridDTO>> GetAllStreamAsync()
        {
            var loEx = new R_Exception();
            List<GSM02000GridDTO> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GSM02000GridDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGSM02000.GetAllSalesTaxStream),
                    _SendWithContext,
                    _SendWithToken,
                    null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        #endregion
    }
}