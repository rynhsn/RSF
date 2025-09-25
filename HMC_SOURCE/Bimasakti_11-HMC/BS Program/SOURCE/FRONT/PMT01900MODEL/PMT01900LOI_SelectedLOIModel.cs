using PMT01900Common.DTO.CRUDBase;
using PMT01900Common.Interface;
using R_BusinessObjectFront;

namespace PMT01900Model
{
    public class PMT01900LOI_SelectedLOIModel : R_BusinessObjectServiceClientBase<PMT01900LOI_SelectedLOIDTO>, IPMT01900LOI_SelectedLOI
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlPM";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/PMT01900LOI_SelectedLOI";
        private const string DEFAULT_MODULE = "PM";

        public PMT01900LOI_SelectedLOIModel(
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
