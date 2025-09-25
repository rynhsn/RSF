using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;

namespace PMT01300COMMON
{
    public interface IPMT01320 : R_IServiceCRUDBase<PMT01320DTO>
    {
        IAsyncEnumerable<PMT01320DTO> GetLOIUtilitiesStream();
        PMT01300SingleResult<PMT01320ActiveInactiveDTO> ChangeStatusLOIUtility(PMT01320ActiveInactiveDTO poEntity);
    }
}
