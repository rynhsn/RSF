using R_CommonFrontBackAPI;
using System.Collections.Generic;

namespace LMM01500COMMON
{
    public interface ILMM01530 : R_IServiceCRUDBase<LMM01530DTO>
    {
        IAsyncEnumerable<LMM01530DTO> GetAllOtherChargerList();
        IAsyncEnumerable<LMM01531DTO> GetOtherChargesLookup();
    }

}
