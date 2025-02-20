using R_CommonFrontBackAPI;
using System.Collections.Generic;

namespace PMM01500COMMON
{
    public interface IPMM01530 : R_IServiceCRUDBase<PMM01530DTO>
    {
        IAsyncEnumerable<PMM01530DTO> GetAllOtherChargerList();
        IAsyncEnumerable<PMM01531DTO> GetOtherChargesLookup();
        PMM01500SingleResult<PMM01531DTO> GetOtherChargesRecordLookup(PMM01531DTO poEntity);
    }

}
