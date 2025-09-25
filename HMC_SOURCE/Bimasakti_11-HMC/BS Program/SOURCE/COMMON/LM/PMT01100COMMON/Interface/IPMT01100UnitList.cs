using PMT01100Common.DTO._1._Unit_List;
using PMT01100Common.Utilities;
using System.Collections.Generic;

namespace PMT01100Common.Interface
{
    public interface IPMT01100UnitList
    {
        IAsyncEnumerable<PMT01100UnitList_BuildingDTO> GetBuildingList();
        IAsyncEnumerable<PMT01100UnitList_UnitListDTO> GetUnitList();
        IAsyncEnumerable<PMT01100PropertyListDTO> GetPropertyList();
    }
}
