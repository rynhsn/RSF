using System;
using System.Threading.Tasks;
using GSM05000Common;
using GSM05000Common.DTOs;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using R_CommonFrontBackAPI;

namespace GSM05000Model
{
    public class GSM05000Model : R_BusinessObjectServiceClientBase<GSM05000DTO>, IGSM05000
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrl";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/GSM05000";

        public GSM05000Model(
            string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, plSendWithContext, plSendWithToken)
        {
        }

        public GSM05000ListDTO<GSM05000GridDTO> GetTransactionCodeList()
        {
            throw new NotImplementedException();
        }

        public GSM05000ListDTO<GSM05000DelimiterDTO> GetDelimiterList()
        {
            throw new NotImplementedException();
        }
        
        public async Task<GSM05000ListDTO<GSM05000GridDTO>> GetAllAsync()
        {
            var loEx = new R_Exception();
            GSM05000ListDTO<GSM05000GridDTO> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<GSM05000ListDTO<GSM05000GridDTO>>(
                    _RequestServiceEndPoint,
                    nameof(IGSM05000.GetTransactionCodeList),
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

        public async Task<GSM05000ListDTO<GSM05000DelimiterDTO>> GetDelimiterAsync()
        {
            var loEx = new R_Exception();
            GSM05000ListDTO<GSM05000DelimiterDTO> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<GSM05000ListDTO<GSM05000DelimiterDTO>>(
                    _RequestServiceEndPoint,
                    nameof(IGSM05000.GetDelimiterList),
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
    }
}