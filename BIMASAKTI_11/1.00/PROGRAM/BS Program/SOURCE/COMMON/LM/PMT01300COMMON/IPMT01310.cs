using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;

namespace PMT01300COMMON
{
    public interface IPMT01310 : R_IServiceCRUDBase<PMT01310DTO>
    {
        IAsyncEnumerable<PMT01310DTO> GetLOIUnitListStream();
    }
}
