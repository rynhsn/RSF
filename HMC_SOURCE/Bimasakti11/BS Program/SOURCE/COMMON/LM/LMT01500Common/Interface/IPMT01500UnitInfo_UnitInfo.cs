using System.Collections.Generic;
using PMT01500Common.DTO._3._Unit_Info;
using PMT01500Common.Utilities;
using R_CommonFrontBackAPI;

namespace PMT01500Common.Interface
{
    public interface IPMT01500UnitInfo_UnitInfo : R_IServiceCRUDBase<PMT01500UnitInfoUnitInfoDetailDTO>
    {
        PMT01500UnitInfoHeaderDTO GetUnitInfoHeader(PMT01500GetHeaderParameterDTO poParameter);
        IAsyncEnumerable<PMT01500UnitInfoUnitInfoListDTO> GetUnitInfoList();
    }
}