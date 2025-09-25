using PMM00200COMMON;
using PMM00200COMMON.DTO_s;
using R_APIClient;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PMM00200MODEL
{
    public class PMM00200Model : R_BusinessObjectServiceClientBase<PMM00200DTO>, IPMM00200
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlPM";
        private const string DEFAULT_CHECKPOINT_NAME = "api/PMM00200";
        private const string DEFAULT_MODULE = "PM";

        public PMM00200Model(
            string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_CHECKPOINT_NAME,
            bool plSendWithContext = true,
            bool plSendWithToken = true
            ) : base(
                pcHttpClientName,
                pcRequestServiceEndPoint,
                DEFAULT_MODULE,
                plSendWithContext,
                plSendWithToken)
        {
        }

        public PMM00200ActiveInactiveParamDTO GetActiveParam(ActiveInactiveParam poParam)
        {
            throw new NotImplementedException();
        }
        public IAsyncEnumerable<PMM00200GridDTO> GetUserParamList()
        {
            throw new NotImplementedException();
        }
        public async Task GetActiveParamAsync(ActiveInactiveParam poParam)
        {
            R_Exception loEx = new R_Exception();
            PMM00200ActiveInactiveParamDTO loRtn = new PMM00200ActiveInactiveParamDTO();
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                await R_HTTPClientWrapper.R_APIRequestObject<PMM00200ActiveInactiveParamDTO, ActiveInactiveParam>(
                    _RequestServiceEndPoint,
                    nameof(IPMM00200.GetActiveParam),
                    poParam,
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

        EndBlock:
            loEx.ThrowExceptionIfErrors();
        }
        public async Task<List<PMM00200GridDTO>> GetUserParamListAsync()

        {
            var loEx = new R_Exception();
            List<PMM00200GridDTO> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMM00200GridDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMM00200.GetUserParamList),
                    DEFAULT_MODULE, _SendWithContext,
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
