using R_CommonFrontBackAPI;
using System.Collections.Generic;

namespace PMM01000COMMON
{
    public interface IPMM01040 : R_IServiceCRUDBase<PMM01040DTO>
    {
        IAsyncEnumerable<PMM01040DTO> GetRateGUDateList();
    }

}
