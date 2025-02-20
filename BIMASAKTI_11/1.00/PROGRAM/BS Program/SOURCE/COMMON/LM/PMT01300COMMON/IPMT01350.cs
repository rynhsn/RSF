using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;

namespace PMT01300COMMON
{
    public interface IPMT01350
    {
        IAsyncEnumerable<PMT01351DTO> GetLOIInvPlanChargeStream();
        IAsyncEnumerable<PMT01350DTO> GetAgreementInvPlanListStream();
    }
}
