using GLT00100COMMON;
using R_APIClient;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GLT00100MODEL
{
    public class GLT00110Model : R_BusinessObjectServiceClientBase<GLT00110DTO>, IGLT00110
    {
        private const string DEFAULT_HTTP = "R_DefaultServiceUrlGL";
        private const string DEFAULT_ENDPOINT = "api/GLT00110";
        private const string DEFAULT_MODULE = "GL";

        public GLT00110Model(
            string pcHttpClientName = DEFAULT_HTTP,
            string pcRequestServiceEndPoint = DEFAULT_ENDPOINT,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        public async Task<GLT00110DTO> GetJournalRecordAsync(GLT00110DTO poEntity)
        {
            var loEx = new R_Exception();
            GLT00110DTO loRtn = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<GLT00100RecordResult<GLT00110DTO>, GLT00110DTO>(
                    _RequestServiceEndPoint,
                    nameof(IGLT00110.GetJournalRecord),
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
        public async Task<GLT00110LastCurrencyRateDTO> GetLastCurrencyAsync(GLT00110LastCurrencyRateDTO poEntity)
        {
            var loEx = new R_Exception();
            GLT00110LastCurrencyRateDTO loRtn = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<GLT00100RecordResult<GLT00110LastCurrencyRateDTO>, GLT00110LastCurrencyRateDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGLT00110.GetLastCurrency),
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
        public async Task<GLT00110DTO> SaveJournalAsync(GLT00110HeaderDetailDTO poEntity)
        {
            var loEx = new R_Exception();
            GLT00110DTO loRtn = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<GLT00100RecordResult<GLT00110DTO>, GLT00110HeaderDetailDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGLT00110.SaveJournal),
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
        public GLT00100RecordResult<GLT00110DTO> GetJournalRecord(GLT00110DTO poEntity)
        {
            throw new NotImplementedException();
        }

        public GLT00100RecordResult<GLT00110LastCurrencyRateDTO> GetLastCurrency(GLT00110LastCurrencyRateDTO poEntity)
        {
            throw new NotImplementedException();
        }

        public GLT00100RecordResult<GLT00110DTO> SaveJournal(GLT00110HeaderDetailDTO poEntity)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
