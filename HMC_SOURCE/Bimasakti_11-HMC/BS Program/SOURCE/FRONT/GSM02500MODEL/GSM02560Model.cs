using GSM02500COMMON.DTOs.GSM02560;
using GSM02500COMMON;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Text;
using GSM02500COMMON.DTOs.GSM02550;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using System.Threading.Tasks;

namespace GSM02500MODEL
{
    public class GSM02560Model : R_BusinessObjectServiceClientBase<GSM02560ParameterDTO>, IGSM02560
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrl";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/GSM02560";
        private const string DEFAULT_MODULE = "gs";

        public GSM02560Model(string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        public IAsyncEnumerable<GSM02560DTO> GetDepartmentList()
        {
            throw new NotImplementedException();
        }

        public async Task<GSM02560ResultDTO> GetDepartmentListStreamAsync()
        {
            {
                R_Exception loEx = new R_Exception();
                List<GSM02560DTO> loResult = null;
                GSM02560ResultDTO loRtn = new GSM02560ResultDTO();

                try
                {
                    R_HTTPClientWrapper.httpClientName = _HttpClientName;

                    loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GSM02560DTO>(
                        _RequestServiceEndPoint,
                        nameof(IGSM02560.GetDepartmentList),
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
        public IAsyncEnumerable<GetDepartmentLookupListDTO> GetDepartmentLookupList()
        {
            throw new NotImplementedException();
        }

        public async Task<GetDepartmentLookupListResultDTO> GetDepartmentLookupListStreamAsync()
        {
            {
                R_Exception loEx = new R_Exception();
                List<GetDepartmentLookupListDTO> loResult = null;
                GetDepartmentLookupListResultDTO loRtn = new GetDepartmentLookupListResultDTO();

                try
                {
                    R_HTTPClientWrapper.httpClientName = _HttpClientName;

                    loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GetDepartmentLookupListDTO>(
                        _RequestServiceEndPoint,
                        nameof(IGSM02560.GetDepartmentLookupList),
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
