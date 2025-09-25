using PMT03000COMMON.DTO_s;
using System;
using System.Collections.Generic;

namespace PMT03000COMMON
{
    public interface IPMT03000
    {
        IAsyncEnumerable<BuildingDTO> GetList_Building();
        IAsyncEnumerable<BuildingUnitDTO> GetList_BuildingUnit();
        IAsyncEnumerable<TransByUnitDTO> GetList_TransByUnit();
        IAsyncEnumerable<PropertyDTO> GetPropertyList();

    }
}
