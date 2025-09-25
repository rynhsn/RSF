using PMT01900Common.DTO.CRUDBase;
using PMT01900Common.Interface;
using R_BusinessObjectFront;

namespace PMT01900Model
{
    public class PMT01900Unit_UnitInfoDetailModel : R_BusinessObjectServiceClientBase<PMT01900Unit_UnitInfoDetailDTO>, IPMT01900Unit_UnitInfoDetail
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlPM";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/PMT01900Unit_UnitInfoDetail";
        private const string DEFAULT_MODULE = "PM";

        public PMT01900Unit_UnitInfoDetailModel(
            string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            string pcModule = DEFAULT_MODULE,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(
                pcHttpClientName,
                pcRequestServiceEndPoint,
                pcModule,
                plSendWithContext,
                plSendWithToken)
        {
        }
    }
}
