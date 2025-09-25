using PMT02100COMMON;
using PMT02100COMMON.DTOs.PMT02130;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PMT02100MODEL
{
    public class PMT02130Model : R_BusinessObjectServiceClientBase<PMT02130HandoverUnitParameterDTO>, IPMT02130
    {
        private const string DEFAULT_HTTP = "R_DefaultServiceUrlPM";
        private const string DEFAULT_ENDPOINT = "api/PMT02130";
        private const string DEFAULT_MODULE = "PM";

        public PMT02130Model(
            string pcHttpClientName = DEFAULT_HTTP,
            string pcRequestServiceEndPoint = DEFAULT_ENDPOINT,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        public async Task<PMT02130HandoverUnitResultDTO> GetHandoverUnitListStreamAsync()
        {
            var loEx = new R_Exception();
            PMT02130HandoverUnitResultDTO loResult = new PMT02130HandoverUnitResultDTO();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult.Data = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMT02130HandoverUnitDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT02130.GetHandoverUnitList),
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

        public async Task<PMT02130HandoverUnitChecklistResultDTO> GetHandoverUnitChecklistListStreamAsync()
        {
            var loEx = new R_Exception();
            PMT02130HandoverUnitChecklistResultDTO loResult = new PMT02130HandoverUnitChecklistResultDTO();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult.Data = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMT02130HandoverUnitChecklistDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT02130.GetHandoverUnitChecklistList),
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

        #region NotImplemented

        public IAsyncEnumerable<PMT02130HandoverUnitDTO> GetHandoverUnitList()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<PMT02130HandoverUnitChecklistDTO> GetHandoverUnitChecklistList()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
