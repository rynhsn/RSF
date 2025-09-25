using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;

namespace PMT01300COMMON
{
    public interface IPMT01330 : R_IServiceCRUDBase<PMT01330DTO>
    {
        IAsyncEnumerable<PMT01330DTO> GetLOIChargeStream();
        PMT01300SingleResult<PMT01330ActiveInactiveDTO> ChangeStatusLOICharges(PMT01330ActiveInactiveDTO poEntity);
    }
}
