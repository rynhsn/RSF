using CBT01100COMMON.DTO_s.CBT01110;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;

namespace CBT01100COMMON
{
    public interface ICBT01110 : R_IServiceCRUDBase<CBT01110ParamDTO>
    {
        IAsyncEnumerable<CBT01101DTO> GetJournalDetailList();

    }
}
