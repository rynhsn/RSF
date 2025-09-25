using PMT06000Common;
using PMT06000Common.DTOs;
using R_BusinessObjectFront;

namespace PMT06000Model
{
    public class PMT06000UnitModel : R_BusinessObjectServiceClientBase<PMT06000OvtUnitDTO>, IPMT06000Unit
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlPM";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/PMT06000Unit";
        private const string DEFAULT_MODULE = "pm";

        public PMT06000UnitModel(
            string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }
    }
}