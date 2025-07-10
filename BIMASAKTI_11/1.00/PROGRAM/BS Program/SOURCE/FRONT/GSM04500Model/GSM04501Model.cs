using GSM04500Common;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GSM04500Model
{
    public class GSM04501Model : R_BusinessObjectServiceClientBase<GSM04510GOADTO>, IGSM04510GOA
    {
        private const string DEFAULT_HTTP = "R_DefaultServiceUrl";
        private const string DEFAULT_ENDPOINT = "api/GSM04510GOA";
        const string DEFAULT_MODULE = "GS";
        public GSM04501Model(
            string pcHttpClientName = DEFAULT_HTTP,
            string pcRequestServiceEndPoint = DEFAULT_ENDPOINT,
            string pcModuleName = DEFAULT_MODULE,
            bool plSendWithContext = true,
            bool plSendWithToken = true)
            : base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        public async Task<GSM04510GOAListDTO> GetAllGOAListAsync(string lcJournalGRPType, string lcJournalGRPCode)
        {
            var loEx = new R_Exception();
            GSM04510GOAListDTO loResult = new GSM04510GOAListDTO();

            try
            {
                R_BlazorFrontEnd.R_FrontContext.R_SetStreamingContext(ContextConstant.CJRNGRP_TYPE, lcJournalGRPType);
                R_BlazorFrontEnd.R_FrontContext.R_SetStreamingContext(ContextConstant.CPROPERTY_ID, "");
                R_BlazorFrontEnd.R_FrontContext.R_SetStreamingContext(ContextConstant.CJOURNAL_GRP_CODE, lcJournalGRPCode);

                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                var loTmp = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GSM04510GOADTO>(
                                       _RequestServiceEndPoint,
                                       nameof(IGSM04510GOA.JOURNAL_GRP_GOA_LIST),
                                       DEFAULT_MODULE,
                                       _SendWithContext,
                                        _SendWithToken);
                loResult.ListData = loTmp;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            return loResult;
        }
        public IAsyncEnumerable<GSM04510GOADTO> JOURNAL_GRP_GOA_LIST()
        {
            throw new NotImplementedException();
        }

    }
}
