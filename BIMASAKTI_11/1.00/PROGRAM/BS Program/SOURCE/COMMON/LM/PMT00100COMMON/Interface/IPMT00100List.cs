using PMT00100COMMON.UnitList;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMT00100COMMON.Interface
{
    public interface IPMT00100List
    {
        IAsyncEnumerable<PMT00100DTO> GetBuildingUnitList();
        IAsyncEnumerable<PMT00100BuildingDTO> GetBuildingList();
        IAsyncEnumerable<PMT00100AgreementByUnitDTO> GetAgreementByUnitList();
    }
}
