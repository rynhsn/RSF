using R_CommonFrontBackAPI;
using System.Collections.Generic;

namespace LMM01500COMMON
{
    public interface ILMM01520 : R_IServiceCRUDBase<LMM01520DTO>
    {
        IAsyncEnumerable<LMM01522DTO> GetAdditionalIdLookup();
    }

}
