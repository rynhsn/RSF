using PMT04200Common.DTOs;
using PMT04200Common;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PMT04200Common;

namespace PMT04200MODEL
{
    public class PMT04200Model : R_BusinessObjectServiceClientBase<PMT04200DTO>, IPMT04200
    {
        private const string DEFAULT_HTTP = "R_DefaultServiceUrlPM";
        private const string DEFAULT_ENDPOINT = "api/PMT04200";
        private const string DEFAULT_MODULE = "PM";

        public PMT04200Model(string pcHttpClientName = DEFAULT_HTTP,
                             string pcRequestServiceEndPoint = DEFAULT_ENDPOINT,
                             bool plSendWithContext = true,
                             bool plSendWithToken = true) : base(
                                 pcHttpClientName,
                                 pcRequestServiceEndPoint,
                                 DEFAULT_MODULE,
                                 plSendWithContext,
                                 plSendWithToken)
        { }

        public async Task<List<PMT04200DTO>> GetJournalListAsync(PMT04200ParamDTO poEntity)
        {
            var loEx = new R_Exception();
            List<PMT04200DTO> loResult = null;

            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstant.CPROPERTY_ID, poEntity.CPROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CDEPT_CODE, poEntity.CDEPT_CODE);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CCUSTOMER_ID, poEntity.CCUSTOMER_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CPERIOD, poEntity.CPERIOD);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CSTATUS, string.IsNullOrWhiteSpace(poEntity.CSTATUS) ? "" : poEntity.CSTATUS);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CSEARCH_TEXT, string.IsNullOrWhiteSpace(poEntity.CSEARCH_TEXT) ? "" : poEntity.CSEARCH_TEXT);


                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMT04200DTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT04200.GetJournalList),
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
        
          public async Task<PMT04200DTO> GetJournalRecordAsync(PMT04200DTO poEntity)
        {
            var loEx = new R_Exception();
            PMT04200DTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<PMT04200RecordResult<PMT04200DTO>, PMT04200DTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT04200.GetJournalRecord),
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
        public async Task<PMT04200DTO> SaveJournalRecordAsync(PMT04200SaveParamDTO poEntity)
        {
            var loEx = new R_Exception();
            PMT04200DTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<PMT04200RecordResult<PMT04200DTO>, PMT04200SaveParamDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT04200.SaveJournalRecord),
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
        public async Task UpdateJournalStatusAsync(PMT04200UpdateStatusDTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<PMT04200RecordResult<PMT04200UpdateStatusDTO>, PMT04200UpdateStatusDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT04200.UpdateJournalStatus),
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
        public async Task SubmitCashReceiptAsync(PMT04200UpdateStatusDTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<PMT04200RecordResult<PMT04200UpdateStatusDTO>, PMT04200UpdateStatusDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT04200.SubmitCashReceipt),
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
        
        
        #region Not Implemented Methods 
        public IAsyncEnumerable<PMT04200DTO> GetJournalList()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<PMT04200AllocationGridDTO> GetAllocationList()
        {
            throw new NotImplementedException();
        }

        public PMT04200RecordResult<PMT04200DTO> GetJournalRecord(PMT04200DTO poEntity)
        {
            throw new NotImplementedException();
        }

        public PMT04200RecordResult<PMT04200DTO> SaveJournalRecord(PMT04200SaveParamDTO poEntity)
        {
            throw new NotImplementedException();
        }

        public PMT04200RecordResult<PMT04200UpdateStatusDTO> UpdateJournalStatus(PMT04200UpdateStatusDTO poEntity)
        {
            throw new NotImplementedException();
        }

        public PMT04200RecordResult<PMT04200UpdateStatusDTO> SubmitCashReceipt(PMT04200UpdateStatusDTO poEntity)
        {
            throw new NotImplementedException();
        }

        #endregion


    }
}
