using GLF00100COMMON;
using R_APIClient;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GLF00100Model
{
    public class GLF00100Model : R_BusinessObjectServiceClientBase<GLF00100DTO>, IGLF00100
    {
        private const string DEFAULT_HTTP = "R_DefaultServiceUrlGL";
        private const string DEFAULT_ENDPOINT = "api/GLF00100";
        private const string DEFAULT_MODULE = "GL";

        public GLF00100Model(
            string pcHttpClientName = DEFAULT_HTTP,
            string pcRequestServiceEndPoint = DEFAULT_ENDPOINT,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        #region GetInfoCompany
        public GLF00100SingleResult<GLF00100InitialDTO> GetInfoCompany()
        {
            throw new NotImplementedException();
        }
        public async Task<GLF00100InitialDTO> GetInfoCompanyAsync()
        {
            var loEx = new R_Exception();
            GLF00100InitialDTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<GLF00100SingleResult<GLF00100InitialDTO>>(
                    _RequestServiceEndPoint,
                    nameof(IGLF00100.GetInfoCompany),
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
        #endregion

        #region GetJournalDetail
        public GLF00100SingleResult<GLF00100DTO> GetJournalDetail(GLF00100ParameterDTO poParam)
        {
            throw new NotImplementedException();
        }
        public async Task<GLF00100DTO> GetJournalDetailAsync(GLF00100ParameterDTO poParam)
        {
            var loEx = new R_Exception();
            GLF00100DTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<GLF00100SingleResult<GLF00100DTO>, GLF00100ParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGLF00100.GetJournalDetail),
                    poParam,
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
        #endregion

        #region GetJournalDetailList
        public IAsyncEnumerable<GLF00101DTO> GetJournalDetailList()
        {
            throw new NotImplementedException();
        }
        public async Task<List<GLF00101DTO>> GetJournalDetailListAsync(GLF00101DTO poParam)
        {
            var loEx = new R_Exception();
            List<GLF00101DTO> loResult = null;

            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstant.CJRN_ID, poParam.CJRN_ID);

                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GLF00101DTO>(
                    _RequestServiceEndPoint,
                    nameof(IGLF00100.GetJournalDetailList),
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
        #endregion
    }
}
