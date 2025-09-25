using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GST00500Common;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;

namespace GST00500Model
{
    public class GST00500OutboxModel : R_BusinessObjectServiceClientBase<GST00500DTO>, IGST00500Outbox
    {
        private const string DEFAULT_HTTP = "R_DefaultServiceUrl";
        private const string DEFAULT_ENDPOINT = "api/GST00500Outbox";
        private const string DEFAULT_MODULE = "GS";
        public GST00500OutboxModel(
            string pcHttpClientName = DEFAULT_HTTP,
            string pcRequestServiceEndPoint = DEFAULT_ENDPOINT,
            string pcModuleName = DEFAULT_MODULE,
            bool plSendWithContext = true,
            bool plSendWithToken = true)
            : base(pcHttpClientName, pcRequestServiceEndPoint, pcModuleName, plSendWithContext, plSendWithToken)
        {
        }
        public IAsyncEnumerable<GST00500DTO> ApprovalOutboxListStream()
        {
            throw new NotImplementedException();
        }

        public List<GST00500ApprovalStatusDTO> GetApprovalStatusList()
        {
            throw new NotImplementedException();
        }

        public async Task<List<GST00500DTO>> GetOutboxListAsyncModel()
        {
            var loEx = new R_Exception();
            List<GST00500DTO> loResult = new List<GST00500DTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GST00500DTO>(
                    _RequestServiceEndPoint,
                    nameof(IGST00500Outbox.ApprovalOutboxListStream),
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

        public async Task<List<GST00500ApprovalStatusDTO>> GetApprovalStatusListAsyncModel()
        {
            var loEx = new R_Exception();
            List<GST00500ApprovalStatusDTO> loResult = new List<GST00500ApprovalStatusDTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GST00500ApprovalStatusDTO>(
                     _RequestServiceEndPoint,
                     nameof(IGST00500Outbox.GetApprovalStatusList),
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


    }
}
