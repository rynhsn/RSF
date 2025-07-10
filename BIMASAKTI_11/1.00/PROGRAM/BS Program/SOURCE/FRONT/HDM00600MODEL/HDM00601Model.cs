using HDM00600COMMON.DTO;
using HDM00600COMMON.Interfaces;
using R_BusinessObjectFront;

namespace HDM00600MODEL
{
    public class HDM00601Model : R_BusinessObjectServiceClientBase<PricelistDTO>, IHDM00601
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlHD";
        private const string DEFAULT_CHECKPOINT_NAME = "api/HDM00601";
        private const string DEFAULT_MODULE = "HD";
        public HDM00601Model(string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_CHECKPOINT_NAME,
            bool plSendWithContext = true,
            bool plSendWithToken = true
            ) : base(
                pcHttpClientName,
                pcRequestServiceEndPoint,
                DEFAULT_MODULE,
                plSendWithContext,
                plSendWithToken)
        {
        }
    }
}
