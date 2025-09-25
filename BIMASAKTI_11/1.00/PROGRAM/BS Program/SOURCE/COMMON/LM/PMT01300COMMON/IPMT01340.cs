using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;

namespace PMT01300COMMON
{
    public interface IPMT01340 : R_IServiceCRUDBase<PMT01340DTO>
    {
        IAsyncEnumerable<PMT01340DTO> GetLOIDepositStream();
    }
}
