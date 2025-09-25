using PMT06000Common;
using PMT06000Common.DTOs;
using R_BusinessObjectFront;

namespace PMT06000Model
{
    public class PMT06000ServiceModel : R_BusinessObjectServiceClientBase<PMT06000OvtServiceDTO>, IPMT06000Service
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlPM";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/PMT06000Service";
        private const string DEFAULT_MODULE = "pm";

        public PMT06000ServiceModel(
            string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }
    }
}