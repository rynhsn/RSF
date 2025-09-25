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
    public class GLT00100Model : R_BusinessObjectServiceClientBase<GLT00100DTO>, IGLT00100
    {
        private const string DEFAULT_HTTP = "R_DefaultServiceUrlGL";
        private const string DEFAULT_ENDPOINT = "api/GLT00100";
        private const string DEFAULT_MODULE = "GL";

        public GLT00100Model(
            string pcHttpClientName = DEFAULT_HTTP,
            string pcRequestServiceEndPoint = DEFAULT_ENDPOINT,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        public async Task<List<GLT00101DTO>> GetJournalDetailListAsync(GLT00101DTO poEntity)
        {
            var loEx = new R_Exception();
            List<GLT00101DTO> loResult = null;

            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstant.CREC_ID, poEntity.CREC_ID);


                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GLT00101DTO>(
                    _RequestServiceEndPoint,
                    nameof(IGLT00100.GetJournalDetailList),
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
        public async Task<List<GLT00100DTO>> GetJournalListAsync(GLT00100ParamDTO poEntity)
        {
            var loEx = new R_Exception();
            List<GLT00100DTO> loResult = null;

            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstant.CDEPT_CODE, poEntity.CDEPT_CODE);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CPERIOD, poEntity.CPERIOD);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CSTATUS, string.IsNullOrWhiteSpace(poEntity.CSTATUS) ? "" : poEntity.CSTATUS);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CSEARCH_TEXT, string.IsNullOrWhiteSpace(poEntity.CSEARCH_TEXT) ? "" : poEntity.CSEARCH_TEXT);


                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GLT00100DTO>(
                    _RequestServiceEndPoint,
                    nameof(IGLT00100.GetJournalList),
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
        public async Task UpdateJournalStatusAsync(GLT00100UpdateStatusDTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<GLT00100RecordResult<GLT00100UpdateStatusDTO>, GLT00100UpdateStatusDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGLT00100.UpdateJournalStatus),
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
        public async Task<GLT00100RapidApprovalValidationDTO> ValidationRapidApprovalAsync(GLT00100RapidApprovalValidationDTO poEntity)
        {
            var loEx = new R_Exception();
            GLT00100RapidApprovalValidationDTO loRtn = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<GLT00100RecordResult<GLT00100RapidApprovalValidationDTO>, GLT00100RapidApprovalValidationDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGLT00100.ValidationRapidApproval),
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
        public IAsyncEnumerable<GLT00101DTO> GetJournalDetailList()
        {
            throw new NotImplementedException();
        }
        public IAsyncEnumerable<GLT00100DTO> GetJournalList()
        {
            throw new NotImplementedException();
        }
        public GLT00100RecordResult<GLT00100UpdateStatusDTO> UpdateJournalStatus(GLT00100UpdateStatusDTO poEntity)
        {
            throw new NotImplementedException();
        }

        public GLT00100RecordResult<GLT00100RapidApprovalValidationDTO> ValidationRapidApproval(GLT00100RapidApprovalValidationDTO poEntity)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
