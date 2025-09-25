using GSM02500COMMON.DTOs.GSM02502Charge;
using GSM02500COMMON;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Text;
using GSM02500COMMON.DTOs.GSM02502Utility;
using R_CommonFrontBackAPI;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using System.Threading.Tasks;

namespace GSM02500MODEL
{
    public class GSM02502UtilityModel : R_BusinessObjectServiceClientBase<GSM02502UtilityParameterDTO>, IGSM02502Utility
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrl";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/GSM02502Utility";
        private const string DEFAULT_MODULE = "gs";

        public GSM02502UtilityModel(string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        public IAsyncEnumerable<GSM02502UtilityDTO> GetUtilityList()
        {
            throw new NotImplementedException();
        }

        public async Task<GSM02502UtilityResultDTO> GetUtilityListStreamAsync()
        {
            R_Exception loEx = new R_Exception();
            List<GSM02502UtilityDTO> loResult = null;
            GSM02502UtilityResultDTO loRtn = new GSM02502UtilityResultDTO();
            //R_ContextHeader loContextHeader = new R_ContextHeader();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GSM02502UtilityDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGSM02502Utility.GetUtilityList),
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
