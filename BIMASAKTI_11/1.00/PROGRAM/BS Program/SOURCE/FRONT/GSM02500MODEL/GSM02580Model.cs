using GSM02500COMMON.DTOs.GSM02580;
using GSM02500COMMON;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Text;
using GSM02500COMMON.DTOs.GSM02550;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using System.Threading.Tasks;

namespace GSM02500MODEL
{
    public class GSM02580Model : R_BusinessObjectServiceClientBase<GSM02580ParameterDTO>, IGSM02580
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrl";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/GSM02580";
        private const string DEFAULT_MODULE = "gs";

        public GSM02580Model(string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }
    }
}
