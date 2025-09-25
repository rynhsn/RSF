using PMR00150COMMON;
using PMR00150COMMON.Utility_Report;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PMR00150MODEL
{
    public class PMR00150Model : R_BusinessObjectServiceClientBase<PMR00150PropertyDTO>, IPMR00150
    {
        private const string DEFAULT_HTTP = "R_DefaultServiceUrlPM";
        private const string DEFAULT_ENDPOINT = "api/PMR00150";
        private const string DEFAULT_MODULE = "PM";
        public PMR00150Model(
            string pcHttpClientName = DEFAULT_HTTP,
            string pcRequestServiceEndPoint = DEFAULT_ENDPOINT,
            string pcModuleName = DEFAULT_MODULE,
            bool plSendWithContext = true,
            bool plSendWithToken = true)
            : base(pcHttpClientName, pcRequestServiceEndPoint, pcModuleName, plSendWithContext, plSendWithToken)
        {
        }

        #region implementLibrary
        public IAsyncEnumerable<PMR00150PropertyDTO> GetPropertyListStream()
        {
            throw new NotImplementedException();
        }
        public PMR00150InitialProcess GetInitialProcess()
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
                var temp = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMR00150PropertyDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMR00150.GetPropertyListStream),
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
        public async Task<PMR00150InitialProcess> GetInitialProcessModel()
        {
            var loEx = new R_Exception();
            PMR00150InitialProcess loResult = new PMR00150InitialProcess();
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var temp = await R_HTTPClientWrapper.R_APIRequestObject<PMR00150InitialProcess>(
                    _RequestServiceEndPoint,
                    nameof(IPMR00150.GetInitialProcess),
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
