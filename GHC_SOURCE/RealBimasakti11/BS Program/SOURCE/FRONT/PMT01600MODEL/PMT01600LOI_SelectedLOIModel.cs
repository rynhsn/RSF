using PMT01600COMMON.DTO.CRUDBase;
using PMT01600COMMON.Interface;
using R_BusinessObjectFront;

namespace PMT01600Model
{
    public class PMT01600LOI_SelectedLOIModel : R_BusinessObjectServiceClientBase<PMT01600LOI_SelectedLOIDTO>, IPMT01600LOI_SelectedLOI
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlPM";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/PMT01600LOI_SelectedLOI";
        private const string DEFAULT_MODULE = "PM";

        public PMT01600LOI_SelectedLOIModel(
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
