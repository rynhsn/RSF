using BMM00500COMMON.DTO;
using BMM00500COMMON.Interface;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BMM00500MODEL
{
    public class BMM00500Model : R_BusinessObjectServiceClientBase<BMM00500CRUDParameterDTO>, IBMM00500
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlBM";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/BMM00500";
        private const string DEFAULT_MODULE = "BM";

        public BMM00500Model(
        string pcHttpClientName = DEFAULT_HTTP_NAME,
        string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
        bool plSendWithContext = true,
        bool plSendWithToken = true) :
        base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        #region GET MOBILE PROGRAM LIST
        public IAsyncEnumerable<BMM00500DTO> GetMobileProgramList()
        {
            throw new NotImplementedException();
        }

        public async Task<BMM00500ListResultDTO<BMM00500DTO>> GetMobileProgramListAsync()
        {
            R_Exception loEx = new R_Exception();
            List<BMM00500DTO>? loResult = null;
            BMM00500ListResultDTO<BMM00500DTO> loRtn = new BMM00500ListResultDTO<BMM00500DTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<BMM00500DTO>(
                    _RequestServiceEndPoint,
                    nameof(IBMM00500.GetMobileProgramList),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken
                    );
                loRtn.Data = loResult;

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }
        #endregion
    }
}
