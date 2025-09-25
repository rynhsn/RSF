using R_CommonFrontBackAPI;
using System.Collections.Generic;

namespace PMM01000COMMON
{
    public interface IPMM01050 : R_IServiceCRUDBase<PMM01050DTO>
    {
        IAsyncEnumerable<PMM01051DTO> GetRateOTList();
        IAsyncEnumerable<PMM01050DTO> GetRateOTDateList();
    }

}
