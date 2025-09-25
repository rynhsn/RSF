using CBT00200COMMON;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CBT00200MODEL
{
    public class CBT00200Model : R_BusinessObjectServiceClientBase<CBT00200DTO>, ICBT00200
    {
        private const string DEFAULT_HTTP = "R_DefaultServiceUrlCB";
        private const string DEFAULT_ENDPOINT = "api/CBT00200";
        private const string DEFAULT_MODULE = "CB";
        public CBT00200Model(
           string pcHttpClientName = DEFAULT_HTTP,
           string pcRequestServiceEndPoint = DEFAULT_ENDPOINT,
           bool plSendWithContext = true,
           bool plSendWithToken = true) :
           base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        public async Task<List<CBT00200DTO>> GetJournalListAsync(CBT00200ParamDTO poEntity)
        {
            var loEx = new R_Exception();
            List<CBT00200DTO> loResult = null;

            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstant.CDEPT_CODE, poEntity.CDEPT_CODE);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CPERIOD, poEntity.CPERIOD);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CSTATUS, string.IsNullOrWhiteSpace(poEntity.CSTATUS) ? "" : poEntity.CSTATUS);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CSEARCH_TEXT, string.IsNullOrWhiteSpace(poEntity.CSEARCH_TEXT) ? "" : poEntity.CSEARCH_TEXT);


                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<CBT00200DTO>(
                    _RequestServiceEndPoint,
                    nameof(ICBT00200.GetJournalList),
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
        public async Task<CBT00200DTO> GetJournalRecordAsync(CBT00200DTO poEntity)
        {
            var loEx = new R_Exception();
            CBT00200DTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<CBT00200SingleResult<CBT00200DTO>, CBT00200DTO>(
                    _RequestServiceEndPoint,
                    nameof(ICBT00200.GetJournalRecord),
                    poEntity,
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loResult = loTempResult.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        public async Task<CBT00200DTO> SaveJournalRecordAsync(CBT00200SaveParamDTO poEntity)
        {
            var loEx = new R_Exception();
            CBT00200DTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<CBT00200SingleResult<CBT00200DTO>, CBT00200SaveParamDTO>(
                    _RequestServiceEndPoint,
                    nameof(ICBT00200.SaveJournalRecord),
                    poEntity,
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loResult = loTempResult.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        public async Task UpdateJournalStatusAsync(CBT00200UpdateStatusDTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<CBT00200SingleResult<CBT00200UpdateStatusDTO>, CBT00200UpdateStatusDTO>(
                    _RequestServiceEndPoint,
                    nameof(ICBT00200.UpdateJournalStatus),
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

        #region Not Implement
        public IAsyncEnumerable<CBT00200DTO> GetJournalList()
        {
            throw new NotImplementedException();
        }
        public CBT00200SingleResult<CBT00200DTO> GetJournalRecord(CBT00200DTO poEntity)
        {
            throw new NotImplementedException();
        }
        public CBT00200SingleResult<CBT00200DTO> SaveJournalRecord(CBT00200SaveParamDTO poEntity)
        {
            throw new NotImplementedException();
        }
        public CBT00200SingleResult<CBT00200UpdateStatusDTO> UpdateJournalStatus(CBT00200UpdateStatusDTO poEntity)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
