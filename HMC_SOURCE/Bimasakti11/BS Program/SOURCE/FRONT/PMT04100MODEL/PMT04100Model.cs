using PMT04100COMMON;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PMT04100MODEL
{
    public class PMT04100Model : R_BusinessObjectServiceClientBase<PMT04100DTO>, IPMT04100
    {
        private const string DEFAULT_HTTP = "R_DefaultServiceUrlPM";
        private const string DEFAULT_ENDPOINT = "api/PMT04100";
        private const string DEFAULT_MODULE = "PM";
        public PMT04100Model(
           string pcHttpClientName = DEFAULT_HTTP,
           string pcRequestServiceEndPoint = DEFAULT_ENDPOINT,
           bool plSendWithContext = true,
           bool plSendWithToken = true) :
           base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        public async Task<List<PMT04100DTO>> GetJournalListAsync(PMT04100ParamDTO poEntity)
        {
            var loEx = new R_Exception();
            List<PMT04100DTO> loResult = null;

            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstant.CDEPT_CODE, string.IsNullOrWhiteSpace(poEntity.CDEPT_CODE) ? "" : poEntity.CDEPT_CODE);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CPERIOD, poEntity.CPERIOD);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CPROPERTY_ID, string.IsNullOrWhiteSpace(poEntity.CPROPERTY_ID) ? "" : poEntity.CPROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CCUSTOMER_ID, string.IsNullOrWhiteSpace(poEntity.CCUSTOMER_ID) ? "" : poEntity.CCUSTOMER_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CSTATUS, string.IsNullOrWhiteSpace(poEntity.CSTATUS) ? "" : poEntity.CSTATUS);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CSEARCH_TEXT, string.IsNullOrWhiteSpace(poEntity.CSEARCH_TEXT) ? "" : poEntity.CSEARCH_TEXT);


                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMT04100DTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT04100.GetJournalList),
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
        public async Task<PMT04100DTO> GetJournalRecordAsync(PMT04100DTO poEntity)
        {
            var loEx = new R_Exception();
            PMT04100DTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<PMT04100SingleResult<PMT04100DTO>, PMT04100DTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT04100.GetJournalRecord),
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
        public async Task<PMT04100DTO> SaveJournalRecordAsync(PMT04100SaveParamDTO poEntity)
        {
            var loEx = new R_Exception();
            PMT04100DTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<PMT04100SingleResult<PMT04100DTO>, PMT04100SaveParamDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT04100.SaveJournalRecord),
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
        public async Task UpdateJournalStatusAsync(PMT04100UpdateStatusDTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<PMT04100SingleResult<PMT04100UpdateStatusDTO>, PMT04100UpdateStatusDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT04100.UpdateJournalStatus),
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
        public async Task SubmitCashReceiptAsync(PMT04100UpdateStatusDTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<PMT04100SingleResult<PMT04100UpdateStatusDTO>, PMT04100UpdateStatusDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT04100.SubmitCashReceipt),
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
        public IAsyncEnumerable<PMT04100DTO> GetJournalList()
        {
            throw new NotImplementedException();
        }
        public PMT04100SingleResult<PMT04100DTO> GetJournalRecord(PMT04100DTO poEntity)
        {
            throw new NotImplementedException();
        }
        public PMT04100SingleResult<PMT04100DTO> SaveJournalRecord(PMT04100SaveParamDTO poEntity)
        {
            throw new NotImplementedException();
        }
        public PMT04100SingleResult<PMT04100UpdateStatusDTO> UpdateJournalStatus(PMT04100UpdateStatusDTO poEntity)
        {
            throw new NotImplementedException();
        }
        public PMT04100SingleResult<PMT04100UpdateStatusDTO> SubmitCashReceipt(PMT04100UpdateStatusDTO poEntity)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
