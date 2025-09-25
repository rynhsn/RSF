
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GSM04500Common;

namespace GSM04500Model
{
    public class GSM04500Model : R_BusinessObjectServiceClientBase<GSM04500DTO>, IGSM04500
    {

        private const string DEFAULT_HTTP = "R_DefaultServiceUrl";
        private const string DEFAULT_ENDPOINT = "api/GSM04500";
        private const string DEFAULT_MODULE = "GS";

        public GSM04500Model(
            string pcHttpClientName = DEFAULT_HTTP,
            string pcRequestServiceEndPoint = DEFAULT_ENDPOINT,
            string pcModuleName = DEFAULT_MODULE,
            bool plSendWithContext = true,
            bool plSendWithToken = true)
            : base(pcHttpClientName, pcRequestServiceEndPoint, pcModuleName, plSendWithContext, plSendWithToken)
        {
        }

        public async Task<GSM04500JournalGroupTypeListDTO> GetJournalGroupTypeListAsyncModel()
        {
            var loEx = new R_Exception();
            GSM04500JournalGroupTypeListDTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<GSM04500JournalGroupTypeListDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGSM04500.GetAllJournalGroupTypeList),
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

        public async Task<GSM04500ListDTO> GetAllJournalGroupListAsync(string lcJournalGRPType)
        {
            var loEx = new R_Exception();
            GSM04500ListDTO loResult = new GSM04500ListDTO();
            try
            {
                R_BlazorFrontEnd.R_FrontContext.R_SetStreamingContext(ContextConstant.CJRNGRP_TYPE, lcJournalGRPType);
                R_BlazorFrontEnd.R_FrontContext.R_SetStreamingContext(ContextConstant.CPROPERTY_ID, "");

                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTemp = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GSM04500DTO>(
                    _RequestServiceEndPoint,
                    nameof(IGSM04500.GET_JOURNAL_GRP_LIST_STREAM),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loResult.ListData = loTemp;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            return loResult;

        }

        public GSM04500JournalGroupTypeListDTO GetAllJournalGroupTypeList()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<GSM04500DTO> GET_JOURNAL_GRP_LIST_STREAM()
        {
            throw new NotImplementedException();
        }
    }
}
