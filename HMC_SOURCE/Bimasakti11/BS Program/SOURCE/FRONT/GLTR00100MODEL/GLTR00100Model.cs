using GLTR00100COMMON;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Threading.Tasks;

namespace GLTR00100MODEL
{
    public class GLTR00100Model : R_BusinessObjectServiceClientBase<GLTR00100DTO>, IGLTR00100
    {
        private const string DEFAULT_HTTP = "R_DefaultServiceUrlGL";
        private const string DEFAULT_ENDPOINT = "api/GLTR00100";
        private const string DEFAULT_MODULE = "GL";

        public GLTR00100Model(
            string pcHttpClientName = DEFAULT_HTTP,
            string pcRequestServiceEndPoint = DEFAULT_ENDPOINT,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        public GLTR00100Record<GLTR00100DTO> GetGLJournal(GLTR00100DTO poParam)
        {
            throw new NotImplementedException();
        }

        public async Task<GLTR00100Record<GLTR00100DTO>> GetGLJournalAsync(GLTR00100DTO poParam)
        {
            var loEx = new R_Exception();
            GLTR00100Record<GLTR00100DTO> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<GLTR00100Record<GLTR00100DTO>, GLTR00100DTO>(
                    _RequestServiceEndPoint,
                    nameof(IGLTR00100.GetGLJournal),
                    poParam,
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

        public GLTR00100InitialDTO GetInitialVar()
        {
            throw new NotImplementedException();
        }

        public async Task<GLTR00100InitialDTO> GetInitialVarAsync()
        {
            var loEx = new R_Exception();
            GLTR00100InitialDTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<GLTR00100InitialDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGLTR00100.GetInitialVar),
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
