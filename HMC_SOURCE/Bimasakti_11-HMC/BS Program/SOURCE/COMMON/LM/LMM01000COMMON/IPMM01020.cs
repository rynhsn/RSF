using R_CommonFrontBackAPI;
using System.Collections.Generic;

namespace PMM01000COMMON
{
    public interface IPMM01020 : R_IServiceCRUDBase<PMM01020DTO>
    {
        IAsyncEnumerable<PMM01021DTO> GetRateWGList();
        IAsyncEnumerable<PMM01020DTO> GetRateWGDateList();
    }

}
