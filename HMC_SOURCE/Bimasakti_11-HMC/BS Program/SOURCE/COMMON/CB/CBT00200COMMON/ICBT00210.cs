using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;

namespace CBT00200COMMON
{
    public interface ICBT00210 : R_IServiceCRUDBase<CBT00210DTO>
    {
        IAsyncEnumerable<CBT00210DTO> GetJournalDetailList();
    }
}
