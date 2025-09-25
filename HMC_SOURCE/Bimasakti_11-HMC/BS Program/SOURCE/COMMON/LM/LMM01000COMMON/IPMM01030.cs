using R_CommonFrontBackAPI;
using System.Collections.Generic;

namespace PMM01000COMMON
{
    public interface IPMM01030 : R_IServiceCRUDBase<PMM01030DTO>
    {
        IAsyncEnumerable<PMM01030DTO> GetRatePGDateList();
    }

}
