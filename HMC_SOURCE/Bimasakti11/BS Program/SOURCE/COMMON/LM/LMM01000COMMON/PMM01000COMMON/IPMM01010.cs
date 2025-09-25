using R_CommonFrontBackAPI;
using System.Collections.Generic;

namespace PMM01000COMMON
{
    public interface IPMM01010 : R_IServiceCRUDBase<PMM01010DTO>
    {
        IAsyncEnumerable<PMM01011DTO> GetRateECList();
        IAsyncEnumerable<PMM01010DTO> GetRateECDateList();

    }

}
