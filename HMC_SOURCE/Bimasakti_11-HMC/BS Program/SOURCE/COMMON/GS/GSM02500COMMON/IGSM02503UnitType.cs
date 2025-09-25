using GSM02500COMMON.DTOs;
using GSM02500COMMON.DTOs.GSM02500;
using GSM02500COMMON.DTOs.GSM02502;
using GSM02500COMMON.DTOs.GSM02503;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSM02500COMMON
{
    public interface IGSM02503UnitType : R_IServiceCRUDBase<GSM02503UnitTypeParameterDTO>
    {
        IAsyncEnumerable<GSM02503UnitTypeDTO> GetUnitTypeList();
        GSM02500ActiveInactiveResultDTO RSP_GS_ACTIVE_INACTIVE_UNIT_TYPEMethod(GSM02500ActiveInactiveParameterDTO poParam);
        TemplateUnitTypeDTO DownloadTemplateUnitType();
    }
}
