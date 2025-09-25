using GSM02500COMMON.DTOs.GSM02502;
using GSM02500COMMON.DTOs;
using GSM02500COMMON;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GSM02500COMMON.DTOs.GSM02504;

namespace GSM02500MODEL
{
    public class GSM02504Model : R_BusinessObjectServiceClientBase<GSM02504ParameterDTO>, IGSM02504
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrl";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/GSM02504";
        private const string DEFAULT_MODULE = "gs";

        public GSM02504Model(string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        public IAsyncEnumerable<GSM02504DTO> GetUnitViewList()
        {
            throw new NotImplementedException();
        }

        public async Task<GSM02504ListDTO> GetUnitViewListStreamAsync()
        {
            R_Exception loEx = new R_Exception();
            List<GSM02504DTO> loResult = null;
            GSM02504ListDTO loRtn = new GSM02504ListDTO();
            //R_ContextHeader loContextHeader = new R_ContextHeader();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GSM02504DTO>(
                    _RequestServiceEndPoint,
                    nameof(IGSM02504.GetUnitViewList),
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
