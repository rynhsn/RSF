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
    public class GSM06010Model : R_BusinessObjectServiceClientBase<GSM06010DTO>, IGSM06010
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrl";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/GSM06010";
        private const string DEFAULT_MODULE = "GS";

        public GSM06010Model(
            string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            string pcModule = DEFAULT_MODULE,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, pcModule, plSendWithContext, plSendWithToken)
        {
        }


        public async Task<GSM06010ListDTO<GSM06010GridDTO>> GetAllCashBankInfoStreamAsync(string pcCCB_CODE)
        {
            var loEx = new R_Exception();
            var loResult = new GSM06010ListDTO<GSM06010GridDTO>();

            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstantGSM06010.CCB_CODE, pcCCB_CODE);

                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GSM06010GridDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGSM06010.GetAllCashBankInfoStream),
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

        public async Task<GSM06010ParameterDTO> GetParameterCashBankInfoAsync(ContextParameterGSM06010 poParam)
        {
            var loEx = new R_Exception();
            var loResult = new GSM06010ParameterDTO();
            ContextParameterGSM06010? loParam;


            try
            {
                if (!string.IsNullOrEmpty(poParam.CCB_CODE))
                {
                    loParam = new ContextParameterGSM06010()
                    {
                        CCB_CODE = poParam.CCB_CODE,
                        CCB_TYPE = poParam.CCB_TYPE,
                        CBANK_TYPE = poParam.CBANK_TYPE
                    };

                    R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                    loResult = await R_HTTPClientWrapper.R_APIRequestObject<GSM06010ParameterDTO, ContextParameterGSM06010>(
                        _RequestServiceEndPoint,
                        nameof(IGSM06010.GetParameterCashBankInfo),
                        loParam,
                        DEFAULT_MODULE,
                        _SendWithContext,
                        _SendWithToken
                    );
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public async Task<GSM06010ListDTO<GSM06010DelimiterInfoDTO>> GetDelimiterInfoCashBankInfoStreamAsync()
        {
            var loEx = new R_Exception();
            var loResult = new GSM06010ListDTO<GSM06010DelimiterInfoDTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<GSM06010ListDTO<GSM06010DelimiterInfoDTO>>(
                    _RequestServiceEndPoint,
                    nameof(IGSM06010.GetDelimiterInfoCashBankInfoStream),
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

        #region notUsed

        public GSM06010ListDTO<GSM06010GridDTO> GetAllCashBankInfo()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<GSM06010GridDTO> GetAllCashBankInfoStream()
        {
            throw new NotImplementedException();
        }

        public GSM06010ParameterDTO GetParameterCashBankInfo()
        {
            throw new NotImplementedException();
        }

        public GSM06010ListDTO<GSM06010DelimiterInfoDTO> GetDelimiterInfoCashBankInfoStream()
        {
            throw new NotImplementedException();
        }

        public GSM06010ParameterDTO GetParameterCashBankInfo(ContextParameterGSM06010 poParam)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}