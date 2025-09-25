using LMM06500COMMON;
using R_APIClient;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LMM06500MODEL
{
    public class LMM06502Model : R_BusinessObjectServiceClientBase<LMM06502DTO>, ILMM06502
    {
        private const string DEFAULT_HTTP = "R_DefaultServiceUrlLM";
        private const string DEFAULT_ENDPOINT = "api/LMM06502";
        private const string DEFAULT_MODULE = "LM";

        public LMM06502Model(
            string pcHttpClientName = DEFAULT_HTTP,
            string pcRequestServiceEndPoint = DEFAULT_ENDPOINT,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        public IAsyncEnumerable<LMM06502DetailDTO> GetStaffMoveList()
        {
            throw new NotImplementedException();
        }
        public async Task<List<LMM06502DetailDTO>> GetStaffMoveListAsync(LMM06502DetailDTO poParam)
        {
            var loEx = new R_Exception();
            List<LMM06502DetailDTO> loRtn = null;

            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstant.CPROPERTY_ID, poParam.CPROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstant.COLD_SUPERVISOR_ID, poParam.COLD_SUPERVISOR_ID);

                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loRtn = await R_HTTPClientWrapper.R_APIRequestStreamingObject<LMM06502DetailDTO>(
                    _RequestServiceEndPoint,
                    nameof(ILMM06502.GetStaffMoveList),
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

            return loRtn;
        }

        public LMM06502DTO SaveNewMoveStaff(LMM06502DTO poEntity)
        {
            throw new NotImplementedException();
        }
        public async Task SaveNewMoveStaffAsync(LMM06502DTO poEntity)
        {
            var loEx = new R_Exception();
            LMM06502DTO loRtn = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<LMM06502DTO, LMM06502DTO>(
                    _RequestServiceEndPoint,
                    nameof(ILMM06502.SaveNewMoveStaff),
                    poEntity,
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
    }
}
