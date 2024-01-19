using LMM01500COMMON;
using R_APIClient;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LMM01500Models
{
    public class LMM01510Model : R_BusinessObjectServiceClientBase<LMM01511DTO>, ILMM01510
    {
        private const string DEFAULT_HTTP = "R_DefaultServiceUrlLM";
        private const string DEFAULT_ENDPOINT = "api/LMM01510";
        private const string DEFAULT_MODULE = "LM";

        public LMM01510Model(
            string pcHttpClientName = DEFAULT_HTTP,
            string pcRequestServiceEndPoint = DEFAULT_ENDPOINT,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }
        public IAsyncEnumerable<LMM01510DTO> LMM01510TemplateAndBankAccountList()
        {
            throw new NotImplementedException();
        }

        public async Task<List<LMM01510DTO>> LMM01510TemplateAndBankAccountListAsync(string poPropertyId, string poInvGrpId)
        {
            var loEx = new R_Exception();
            List<LMM01510DTO> loResult = null;

            try
            {

                R_FrontContext.R_SetStreamingContext(ContextConstant.CPROPERTY_ID, poPropertyId);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CINVGRP_CODE, poInvGrpId);

                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<LMM01510DTO>(
                    _RequestServiceEndPoint,
                    nameof(ILMM01510.LMM01510TemplateAndBankAccountList),
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
