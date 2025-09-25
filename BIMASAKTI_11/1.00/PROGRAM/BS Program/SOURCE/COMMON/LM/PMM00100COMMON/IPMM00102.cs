using PMM00100COMMON.DTO_s;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;

namespace PMM00100COMMON
{
    public interface IPMM00102 : R_IServiceCRUDBase<HoUtilBuildingMappingDTO>
    {
        IAsyncEnumerable<HoUtilBuildingMappingDTO> GetBuildingList();
    }
}
