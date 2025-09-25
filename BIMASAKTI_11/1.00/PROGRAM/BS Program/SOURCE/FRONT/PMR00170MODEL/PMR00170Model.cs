using PMR00170COMMON;
using PMR00170COMMON.Utility_Report;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PMR00170MODEL
{
    public class PMR00170Model : R_BusinessObjectServiceClientBase<PMR00170PropertyDTO>, IPMR00170
    {
        private const string DEFAULT_HTTP = "R_DefaultServiceUrlPM";
        private const string DEFAULT_ENDPOINT = "api/PMR00170";
        private const string DEFAULT_MODULE = "PM";
        public PMR00170Model(
            string pcHttpClientName = DEFAULT_HTTP,
            string pcRequestServiceEndPoint = DEFAULT_ENDPOINT,
            string pcModuleName = DEFAULT_MODULE,
            bool plSendWithContext = true,
            bool plSendWithToken = true)
            : base(pcHttpClientName, pcRequestServiceEndPoint, pcModuleName, plSendWithContext, plSendWithToken)
        {
        }

        #region implementLibrary
        public IAsyncEnumerable<PMR00170PropertyDTO> GetPropertyListStream()
        {
            throw new NotImplementedException();
        }
        public PMR00170InitialProcess GetInitialProcess()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region implements
        public async Task<ListPropertyDTO> GetPropertyListStreamAsyncModel()
        {
            var loEx = new R_Exception();
            ListPropertyDTO loResult = new ListPropertyDTO();
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var temp = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMR00170PropertyDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMR00170.GetPropertyListStream),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);
                loResult.Data = temp;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            return loResult;
        }
        public async Task<PMR00170InitialProcess> GetInitialProcessModel()
        {
            var loEx = new R_Exception();
            PMR00170InitialProcess loResult = new PMR00170InitialProcess();
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var temp = await R_HTTPClientWrapper.R_APIRequestObject<PMR00170InitialProcess>(
                    _RequestServiceEndPoint,
                    nameof(IPMR00170.GetInitialProcess),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);
                loResult = temp;
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
