using PMF00200COMMON;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PMF00200Model
{
    public class PMF00200Model : R_BusinessObjectServiceClientBase<PMF00200DTO>, IPMF00200
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlPM";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/PMF00200";
        private const string DEFAULT_MODULE = "PM";

        public PMF00200Model(string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        public async Task<PMF00200AllInitialProcessDTO> GetAllInitialProcessAsync(PMF00200InputParameterDTO poEntity)
        {
            var loEx = new R_Exception();
            PMF00200AllInitialProcessDTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<PMF00200Record<PMF00200AllInitialProcessDTO>, PMF00200InputParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMF00200.GetAllInitialProcess),
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
        public async Task<PMF00200DTO> GetJournalRecordAsync(PMF00200InputParameterDTO poEntity)
        {
            var loEx = new R_Exception();
            PMF00200DTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<PMF00200Record<PMF00200DTO>, PMF00200InputParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMF00200.GetJournalRecord),
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
        public async Task<PMF00200Record<string>> SendEmail(PMF00200InputParameterDTO poEntity)
        {
            var loEx = new R_Exception();
            PMF00200Record<string> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<PMF00200Record<string>, PMF00200InputParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMF00200.SendEmail),
                    poEntity,
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loResult = loTempResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loResult;
        }

        #region Not Implement
        public PMF00200Record<PMF00200AllInitialProcessDTO> GetAllInitialProcess(PMF00200InputParameterDTO poEntity)
        {
            throw new NotImplementedException();
        }
        public PMF00200Record<PMF00200DTO> GetJournalRecord(PMF00200InputParameterDTO poEntity)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
