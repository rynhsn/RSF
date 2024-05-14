using GSM02500COMMON;
using GSM02500COMMON.DTOs.GSM02540;
using GSM02500COMMON.DTOs.GSM02550;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GSM02500MODEL
{
    public class GSM02550Model : R_BusinessObjectServiceClientBase<GSM02550ParameterDTO>, IGSM02550
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrl";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/GSM02550";
        private const string DEFAULT_MODULE = "gs";

        public GSM02550Model(string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        public IAsyncEnumerable<GetUserIdNameDTO> GetUserIdNameList()
        {
            throw new NotImplementedException();
        }

        public async Task<GetUserIdNameResultDTO> GetUserIdNameListStreamAsync()
        {
            {
                R_Exception loEx = new R_Exception();
                List<GetUserIdNameDTO> loResult = null;
                GetUserIdNameResultDTO loRtn = new GetUserIdNameResultDTO();

                try
                {
                    R_HTTPClientWrapper.httpClientName = _HttpClientName;

                    loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GetUserIdNameDTO>(
                        _RequestServiceEndPoint,
                        nameof(IGSM02550.GetUserIdNameList),
                        DEFAULT_MODULE,
                        _SendWithContext,
                        _SendWithToken);

                    loRtn.Data = loResult;
                }
                catch (Exception ex)
                {
                    loEx.Add(ex);
                }

            EndBlock:
                loEx.ThrowExceptionIfErrors();

                return loRtn;
            }
        }

        public IAsyncEnumerable<GSM02550DTO> GetUserPropertyList()
        {
            throw new NotImplementedException();
        }

        public async Task<GSM02550ResultDTO> GetUserPropertyListStreamAsync()
        {
            {
                R_Exception loEx = new R_Exception();
                List<GSM02550DTO> loResult = null;
                GSM02550ResultDTO loRtn = new GSM02550ResultDTO();

                try
                {
                    R_HTTPClientWrapper.httpClientName = _HttpClientName;

                    loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GSM02550DTO>(
                        _RequestServiceEndPoint,
                        nameof(IGSM02550.GetUserPropertyList),
                        DEFAULT_MODULE,
                        _SendWithContext,
                        _SendWithToken);

                    loRtn.Data = loResult;
                }
                catch (Exception ex)
                {
                    loEx.Add(ex);
                }

            EndBlock:
                loEx.ThrowExceptionIfErrors();

                return loRtn;
            }
        }
    }
}
