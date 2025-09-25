using HDM00500COMMON.DTO_s;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;

namespace HDM00500COMMON.Interfaces
{
    public interface IHDM00501 : R_IServiceCRUDBase<ChecklistDTO>
    {
        IAsyncEnumerable<ChecklistDTO> GetList_Checklist();
    }
}
