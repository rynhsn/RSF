using GSM12000COMMON;
using R_APIClient;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GSM12000MODEL
{
    public class GSM12000Model : R_BusinessObjectServiceClientBase<GSM12000DTO>, IGSM12000
    {
        private const string DEFAULT_HTTP = "R_DefaultServiceUrl";
        private const string DEFAULT_ENDPOINT = "api/GSM12000";
        private const string DEFAULT_MODULE = "GS";

        public GSM12000Model(
            string pcHttpClientName = DEFAULT_HTTP,
            string pcRequestServiceEndPoint = DEFAULT_ENDPOINT,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        public async Task<List<GSM12000GSBCodeDTO>> GetListMessageTypeAsync()
        {
            var loEx = new R_Exception();
            List<GSM12000GSBCodeDTO> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GSM12000GSBCodeDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGSM12000.GetListMessageType),
                    DEFAULT_MODULE,
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
        public async Task<List<GSM12000DTO>> GetListMessageAsync(string pcMessageType)
        {
            var loEx = new R_Exception();
            List<GSM12000DTO> loResult = null;

            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstant.CMESSAGE_TYPE, string.IsNullOrWhiteSpace(pcMessageType) ? "" : pcMessageType);

                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GSM12000DTO>(
                    _RequestServiceEndPoint,
                    nameof(IGSM12000.GetListMessage),
                    DEFAULT_MODULE,
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
        
        public async Task<GSM12000DTO> ActiveInActiveMessageAsync(GSM12000DTO poParamDto)
        {
            var loEx = new R_Exception();
            GSM12000DTO loRtn = null;
            //R_ContextHeader loContextHeader = new R_ContextHeader();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loRtn = await R_HTTPClientWrapper
                    .R_APIRequestObject<GSM12000DTO, GSM12000DTO>(
                        _RequestServiceEndPoint,
                        nameof(IGSM12000.GetActiveInactive), poParamDto, DEFAULT_MODULE,
                        _SendWithContext,
                        _SendWithToken);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            EndBlock:
            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }
        public IAsyncEnumerable<GSM12000GSBCodeDTO> GetListMessageType()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<GSM12000DTO> GetListMessage()
        {
            throw new NotImplementedException();
        }

        public GSM12000DTO GetActiveInactive(GSM12000DTO poParamDto)
        {
            throw new NotImplementedException();
        }
    }
}
