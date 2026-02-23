using GSM06500Common;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GSM06500Model
{
    public class GSM06500Model : R_BusinessObjectServiceClientBase<GSM06500DTO>, IGSM06500
    {
        private const string DEFAULT_HTTP = "R_DefaultServiceUrl";
        private const string DEFAULT_ENDPOINT = "api/GSM06500";
        private const string DEFAULT_MODULE = "GS";

        public string lcPropertyId { get; set; }


        public GSM06500Model(
            string pcHttpClientName = DEFAULT_HTTP,
            string pcRequestServiceEndPoint = DEFAULT_ENDPOINT,
            string pcModuleName = DEFAULT_MODULE,
            bool plSendWithContext = true,
            bool plSendWithToken = true)
            : base(pcHttpClientName, pcRequestServiceEndPoint, pcModuleName, plSendWithContext, plSendWithToken)
        {
        }
        public IAsyncEnumerable<GSM06500DTO> GetTermOfPaymentList()
        {
            throw new NotImplementedException();
        }
        public async Task<GSM06500ListDTO> GetTermOfPaymentListAsyncModel()
        {
            var loEx = new R_Exception();
            GSM06500ListDTO loResult = new GSM06500ListDTO();
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loResultTemp = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GSM06500DTO>(
                    _RequestServiceEndPoint,
                    nameof(IGSM06500.GetTermOfPaymentList),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loResult.ListData = loResultTemp;
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
