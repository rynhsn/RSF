using LMM01500COMMON;
using R_APIClient;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LMM01500MODEL
{
    public class LMM01530Model : R_BusinessObjectServiceClientBase<LMM01530DTO>, ILMM01530
    {
        private const string DEFAULT_HTTP = "R_DefaultServiceUrlLM";
        private const string DEFAULT_ENDPOINT = "api/LMM01530";
        private const string DEFAULT_MODULE = "LM";

        public LMM01530Model(
            string pcHttpClientName = DEFAULT_HTTP,
            string pcRequestServiceEndPoint = DEFAULT_ENDPOINT,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }


        public IAsyncEnumerable<LMM01530DTO> GetAllOtherChargerList()
        {
            throw new NotImplementedException();
        }
        public async Task<LMM01500List<LMM01530DTO>> GetAllOtherChargerListAsync(string poPropertyId, string poInvGrpId)
        {
            var loEx = new R_Exception();
            LMM01500List<LMM01530DTO> loResult = new LMM01500List<LMM01530DTO>();

            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstant.CPROPERTY_ID, poPropertyId);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CINVGRP_CODE, poInvGrpId);

                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loResult.Data = await R_HTTPClientWrapper.R_APIRequestStreamingObject<LMM01530DTO>(
                    _RequestServiceEndPoint,
                    nameof(ILMM01530.GetAllOtherChargerList),
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


        public IAsyncEnumerable<LMM01531DTO> GetOtherChargesLookup()
        {
            throw new NotImplementedException();
        }

        public async Task<List<LMM01531DTO>> GetOtherChargesLookupAsync(LMM01531DTO poParam)
        {
            var loEx = new R_Exception();
            List<LMM01531DTO> loResult = null;

            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstant.CPROPERTY_ID, poParam.CPROPERTY_ID);

                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<LMM01531DTO>(
                    _RequestServiceEndPoint,
                    nameof(ILMM01530.GetOtherChargesLookup),
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
    }
}
