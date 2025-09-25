using CBT01200Common.DTOs;
using CBT01200Common;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CBT01200Common;

namespace CBT01200MODEL
{
    public class CBT01200Model : R_BusinessObjectServiceClientBase<CBT01200JournalHDParam>, ICBT01200
    {
        private const string DEFAULT_HTTP = "R_DefaultServiceUrlCB";
        private const string DEFAULT_ENDPOINT = "api/CBT01200";
        private const string DEFAULT_MODULE = "CB";

        public CBT01200Model(string pcHttpClientName = DEFAULT_HTTP,
                             string pcRequestServiceEndPoint = DEFAULT_ENDPOINT,
                             bool plSendWithContext = true,
                             bool plSendWithToken = true) : base(
                                 pcHttpClientName,
                                 pcRequestServiceEndPoint,
        DEFAULT_MODULE,
                                 plSendWithContext,
                                 plSendWithToken)
        { }

        public async Task<List<CBT01200DTO>> GetJournalListAsync(CBT01200ParamDTO poEntity)
        {
            var loEx = new R_Exception();
            List<CBT01200DTO> loResult = null;

            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstant.CDEPT_CODE, poEntity.CDEPT_CODE);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CPERIOD, poEntity.CPERIOD);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CSTATUS, string.IsNullOrWhiteSpace(poEntity.CSTATUS) ? "" : poEntity.CSTATUS);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CSEARCH_TEXT, string.IsNullOrWhiteSpace(poEntity.CSEARCH_TEXT) ? "" : poEntity.CSEARCH_TEXT);


                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<CBT01200DTO>(
                    _RequestServiceEndPoint,
                    nameof(ICBT01200.GetJournalList),
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
        
        public async Task UpdateJournalStatusAsync(CBT01200UpdateStatusDTO poEntity)
        {
            var loEx = new R_Exception();
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<CBT01200RecordResult<CBT01200UpdateStatusDTO>, CBT01200UpdateStatusDTO>(
                _RequestServiceEndPoint,
                nameof(ICBT01200.UpdateJournalStatus),
                poEntity,
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        public async Task<CBT01210LastCurrencyRateDTO> GetLastCurrencyAsync(CBT01210LastCurrencyRateDTO poEntity)
        {
            var loEx = new R_Exception();
            CBT01210LastCurrencyRateDTO loRtn = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<CBT01200RecordResult<CBT01210LastCurrencyRateDTO>, CBT01210LastCurrencyRateDTO>(
                    _RequestServiceEndPoint,
                    nameof(ICBT01200.GetLastCurrency),
                    poEntity,
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loRtn = loTempResult.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }
        
        
        #region Not Implement
        public IAsyncEnumerable<CBT01200DTO> GetJournalList()
        {
            throw new NotImplementedException();
        }

        public CBT01200RecordResult<CBT01200UpdateStatusDTO> UpdateJournalStatus(CBT01200UpdateStatusDTO poEntity)
        {
            throw new NotImplementedException();
        }

        public CBT01200RecordResult<CBT01210LastCurrencyRateDTO> GetLastCurrency(CBT01210LastCurrencyRateDTO poEntity)
        {
            throw new NotImplementedException();
        }


        #endregion


    }
}
