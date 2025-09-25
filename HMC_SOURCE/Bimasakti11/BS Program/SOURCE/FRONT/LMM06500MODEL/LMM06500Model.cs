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
    public class LMM06500Model : R_BusinessObjectServiceClientBase<LMM06500DTO>, ILMM06500
    {
        private const string DEFAULT_HTTP = "R_DefaultServiceUrlLM";
        private const string DEFAULT_ENDPOINT = "api/LMM06500";
        private const string DEFAULT_MODULE = "LM";


        public LMM06500Model(
            string pcHttpClientName = DEFAULT_HTTP,
            string pcRequestServiceEndPoint = DEFAULT_ENDPOINT,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        public IAsyncEnumerable<LMM06500DTOInitial> GetProperty()
        {
            throw new NotImplementedException();
        }

        public async Task<List<LMM06500DTOInitial>> GetPropertyAsync()
        {
            var loEx = new R_Exception();
            List<LMM06500DTOInitial> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<LMM06500DTOInitial>(
                    _RequestServiceEndPoint,
                    nameof(ILMM06500.GetProperty),
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

        public IAsyncEnumerable<LMM06500DTO> GetStaffList()
        {
            throw new NotImplementedException();
        }
        public async Task<List<LMM06500DTO>> GetStaffListAsync(string poPropertyID)
        {
            var loEx = new R_Exception();
            List<LMM06500DTO> loResult = null;

            try
            {

                R_FrontContext.R_SetStreamingContext(ContextConstant.CPROPERTY_ID, poPropertyID);

                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<LMM06500DTO>(
                    _RequestServiceEndPoint,
                    nameof(ILMM06500.GetStaffList),
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

        public LMM06500DTO LMM06500ActiveInactive(LMM06500DTO poParam)
        {
            throw new NotImplementedException();
        }

        public async Task LMM06500ActiveInactiveAsync(LMM06500DTO poParam)
        {
            var loEx = new R_Exception();
            LMM06500DTO loRtn = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<LMM06500DTO, LMM06500DTO>(
                    _RequestServiceEndPoint,
                    nameof(ILMM06500.LMM06500ActiveInactive),
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
    }
}
