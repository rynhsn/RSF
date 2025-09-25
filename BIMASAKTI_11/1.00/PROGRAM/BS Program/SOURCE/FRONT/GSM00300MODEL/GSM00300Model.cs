using GSM00300COMMON;
using GSM00300COMMON.DTO_s;
using GSM00300COMMON.DTO_s.Helper;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Threading.Tasks;

namespace GSM00300MODEL
{
    public class GSM00300Model : R_BusinessObjectServiceClientBase<CompanyParamRecordDTO>, IGSM00300
    {
        private const string DEFAULT_CHECKPOINT_NAME = "api/GSM00300";

        public GSM00300Model(
            string pcHttpClientName = GSM00300ContextConstant.DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_CHECKPOINT_NAME,
            bool plSendWithContext = true,
            bool plSendWithToken = true
            ) : base(
                pcHttpClientName,
                pcRequestServiceEndPoint,
                GSM00300ContextConstant.DEFAULT_MODULE,
                plSendWithContext,
                plSendWithToken)
        {
        }

        public async Task<CheckPrimaryAccountDTO> CheckPrimaryAccountAsync()
        {
            R_Exception loEx = new R_Exception();
            CheckPrimaryAccountDTO loRtn = new CheckPrimaryAccountDTO();
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                var loRtnTemp = await R_HTTPClientWrapper.R_APIRequestObject<GeneralAPIResultDTO<CheckPrimaryAccountDTO>>(
                    _RequestServiceEndPoint,
                    nameof(IGSM00300.CheckIsPrimaryAccount),
                    GSM00300ContextConstant.DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);
                loRtn = loRtnTemp.data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            return loRtn;
        }
        public async Task<ValidateCompanyDTO> CheckIsCompanyParamEditableAsync()
        {
            R_Exception loEx = new R_Exception();
            ValidateCompanyDTO loRtn = new ValidateCompanyDTO();
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                var loRtnTemp = await R_HTTPClientWrapper.R_APIRequestObject<GeneralAPIResultDTO<ValidateCompanyDTO>>(
                    _RequestServiceEndPoint,
                    nameof(IGSM00300.CheckIsCompanyParamEditable),
                    GSM00300ContextConstant.DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);
                loRtn = loRtnTemp.data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            return loRtn;
        }

        public GeneralAPIResultDTO<CheckPrimaryAccountDTO> CheckIsPrimaryAccount()
        {
            throw new NotImplementedException();
        }

        public GeneralAPIResultDTO<ValidateCompanyDTO> CheckIsCompanyParamEditable()
        {
            throw new NotImplementedException();
        }
    }
}