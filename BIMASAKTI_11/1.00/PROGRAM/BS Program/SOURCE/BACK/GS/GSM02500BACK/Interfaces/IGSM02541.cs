using GSM02500COMMON.DTOs;
using GSM02500COMMON.DTOs.GSM02540;
using GSM02500COMMON.DTOs.GSM02541;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSM02500BACK
{
    public interface IGSM02541 : R_IServiceCRUDAsyncBase<GSM02541ParameterDTO>
    {
        IAsyncEnumerable<GSM02541DTO> GetOtherUnitList();
        IAsyncEnumerable<OtherUnitTypeDTO> GetOtherUnitTypeList();
        IAsyncEnumerable<BuildingDTO> GetBuildingList();
        IAsyncEnumerable<FloorDTO> GetFloorList();
        Task<GSM02500ActiveInactiveResultDTO> RSP_GS_ACTIVE_INACTIVE_OTHER_UNITMethod(GSM02500ActiveInactiveParameterDTO poParam);
        Task<TemplateOtherUnitDTO> DownloadTemplateOtherUnit();
    }
}
