using PMT01700COMMON.DTO._1._Other_Unit_List;
using PMT01700COMMON.DTO.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMT01700COMMON.Interface
{
    public interface IPMT01700UnitList
    {
        IAsyncEnumerable<PMT01700PropertyListDTO> GetPropertyList();
        IAsyncEnumerable<PMT01700OtherUnitList_OtherUnitListDTO> GetUnitOtherList();
    }
}
