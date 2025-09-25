using PMT02600COMMON.DTOs.PMT02600;
using PMT02600COMMON;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Text;
using PMT02600COMMON.DTOs.PMT02610;

namespace PMT02600MODEL
{
    public class PMT02610Model : R_BusinessObjectServiceClientBase<PMT02610ParameterDTO>, IPMT02610
    {
        private const string DEFAULT_HTTP = "R_DefaultServiceUrlPM";
        private const string DEFAULT_ENDPOINT = "api/PMT02610";
        private const string DEFAULT_MODULE = "PM";

        public PMT02610Model(
            string pcHttpClientName = DEFAULT_HTTP,
            string pcRequestServiceEndPoint = DEFAULT_ENDPOINT,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

    }
}
