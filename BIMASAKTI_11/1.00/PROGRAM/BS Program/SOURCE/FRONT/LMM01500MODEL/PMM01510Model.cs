using PMM01500COMMON;
using R_APIClient;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PMM01500MODEL
{
    public class PMM01510Model : R_BusinessObjectServiceClientBase<PMM01511DTO>, IPMM01510
    {
        private const string DEFAULT_HTTP = "R_DefaultServiceUrlPM";
        private const string DEFAULT_ENDPOINT = "api/PMM01510";
        private const string DEFAULT_MODULE = "PM";

        public PMM01510Model(
            string pcHttpClientName = DEFAULT_HTTP,
            string pcRequestServiceEndPoint = DEFAULT_ENDPOINT,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }
        public IAsyncEnumerable<PMM01510DTO> PMM01510TemplateAndBankAccountList()
        {
            throw new NotImplementedException();
        }

        public async Task<List<PMM01510DTO>> PMM01510TemplateAndBankAccountListAsync(string poPropertyId, string poInvGrpId)
        {
            var loEx = new R_Exception();
            List<PMM01510DTO> loResult = null;

            try
            {

                R_FrontContext.R_SetStreamingContext(ContextConstant.CPROPERTY_ID, poPropertyId);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CINVGRP_CODE, poInvGrpId);

                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMM01510DTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMM01510.PMM01510TemplateAndBankAccountList),
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
