using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GSM06000Common;
using R_APIClient;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;

namespace GSM06000Model
{
    public class GSM06000Model : R_BusinessObjectServiceClientBase<GSM06000DTO>, IGSM06000
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrl";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/GSM06000";
        private const string DEFAULT_MODULE = "GS";


        public GSM06000Model(
            string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            string pcModule = DEFAULT_MODULE,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, pcModule, plSendWithContext, plSendWithToken)
        {
        }

        public async Task<GSM06000ListDTO> GetAllCashBankStreamAsync(string pcType, string pcBank)
        {
            var loEx = new R_Exception();
            var loResult = new GSM06000ListDTO();


            //pcType = "1";
            //pcBank = "B";

            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstant.CCB_TYPE, pcType);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CBANK_TYPE, pcBank);

                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GSM06000DTO>(
                    _RequestServiceEndPoint,
                    nameof(IGSM06000.GetAllCashBankStream),
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


        #region not used

        public IAsyncEnumerable<GSM06000DTO> GetAllCashBankStream()
        {
            throw new NotImplementedException();
        }

        public GSM06000ListDTO GetCashPropertyType()
        {
            throw new NotImplementedException();
        }

        public GSM06000ListDTO GetBankPropertyType()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region GetTypeOf Cash And Bank

        public async Task<GSM06000ListDTO> GetCashTypeAsync()
        {
            var loEx = new R_Exception();
            GSM06000ListDTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<GSM06000ListDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGSM06000.GetCashPropertyType),
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

        public async Task<GSM06000ListDTO> GetBankTypeAsync()
        {
            var loEx = new R_Exception();
            GSM06000ListDTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<GSM06000ListDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGSM06000.GetBankPropertyType),
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

        #endregion
    }
}