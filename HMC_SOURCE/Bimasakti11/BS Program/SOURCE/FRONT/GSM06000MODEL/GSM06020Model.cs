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
    public class GSM06020Model : R_BusinessObjectServiceClientBase<GSM06020DTO>, IGSM06020
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrl";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/GSM06020";
        private const string DEFAULT_MODULE = "GS";

        public GSM06020Model(
            string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            string pcModule = DEFAULT_MODULE,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, pcModule, plSendWithContext, plSendWithToken)
        {
        }


        public async Task<List<GSM06020DTO>> GetAllCashBankDocNumberingStreamAsync(string pcCCB_CODE,
            string pcCCB_ACCOUNT_NO, string pcCPERIOD_MODE)
        {
            var loEx = new R_Exception();
            var loResult = new List<GSM06020DTO>();


            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstantGSM06020.CCB_CODE, pcCCB_CODE);
                R_FrontContext.R_SetStreamingContext(ContextConstantGSM06020.CCB_ACCOUNT_NO, pcCCB_ACCOUNT_NO);
                R_FrontContext.R_SetStreamingContext(ContextConstantGSM06020.CPERIOD_MODE, pcCPERIOD_MODE);

                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GSM06020DTO>(
                    _RequestServiceEndPoint,
                    nameof(IGSM06020.GetAllCashBankDocNumberingStream),
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

            return loResult;
        }

        public async Task<GSM06020ParameterDTO> GetParameterCashBankDocNumberingAsync(string pcCCB_CODE,
            string pcCCB_ACCOUNT_NO)
        {
            var loEx = new R_Exception();
            var loResult = new GSM06020ParameterDTO();
            ContextParameterGSM06020? loParam;


            try
            {
                loParam = new ContextParameterGSM06020()
                {
                    CCB_CODE = pcCCB_CODE,
                    CCB_ACCOUNT_NO = pcCCB_ACCOUNT_NO
                };
                
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<GSM06020ParameterDTO, ContextParameterGSM06020>(
                    _RequestServiceEndPoint,
                    nameof(IGSM06020.GetParameterCashBankDocNumbering),
                    loParam,
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


        #region Not Used

        public IAsyncEnumerable<GSM06020DTO> GetAllCashBankDocNumberingStream()
        {
            throw new NotImplementedException();
        }

        public GSM06020ParameterDTO GetParameterCashBankDocNumbering()
        {
            throw new NotImplementedException();
        }

        public GSM06020ParameterDTO GetParameterCashBankDocNumbering(ContextParameterGSM06020 poParam)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}