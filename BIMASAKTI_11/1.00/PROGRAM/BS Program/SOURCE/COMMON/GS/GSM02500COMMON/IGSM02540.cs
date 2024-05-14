using GSM02500COMMON.DTOs;
using GSM02500COMMON.DTOs.GSM02531;
using GSM02500COMMON.DTOs.GSM02540;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSM02500COMMON
{
    public interface IGSM02540 : R_IServiceCRUDBase<GSM02540ParameterDTO>
    {
        IAsyncEnumerable<GSM02540DTO> GetUnitPromotionTypeList();
        GSM02500ActiveInactiveResultDTO RSP_GS_ACTIVE_INACTIVE_UNIT_PROMOTION_TYPEMethod(GSM02500ActiveInactiveParameterDTO poParam);
        TemplateUnitPromotionTypeDTO DownloadTemplateUnitPromotionType();
    }
}
