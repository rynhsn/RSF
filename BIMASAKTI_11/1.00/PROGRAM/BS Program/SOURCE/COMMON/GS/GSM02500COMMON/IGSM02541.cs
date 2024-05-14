using GSM02500COMMON.DTOs;
using GSM02500COMMON.DTOs.GSM02540;
using GSM02500COMMON.DTOs.GSM02541;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSM02500COMMON
{
    public interface IGSM02541 : R_IServiceCRUDBase<GSM02541ParameterDTO>
    {
        IAsyncEnumerable<GSM02541DTO> GetUnitPromotionList();
        IAsyncEnumerable<UnitPromotionTypeDTO> GetUnitPromotionTypeList();
        IAsyncEnumerable<BuildingDTO> GetBuildingList();
        IAsyncEnumerable<FloorDTO> GetFloorList();
        GSM02500ActiveInactiveResultDTO RSP_GS_ACTIVE_INACTIVE_UNIT_PROMOTIONMethod(GSM02500ActiveInactiveParameterDTO poParam);
        TemplateUnitPromotionDTO DownloadTemplateUnitPromotion();
    }
}
