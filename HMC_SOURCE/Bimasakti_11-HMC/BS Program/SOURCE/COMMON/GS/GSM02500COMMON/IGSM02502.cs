using GSM02500COMMON.DTOs;
using GSM02500COMMON.DTOs.GSM02500;
using GSM02500COMMON.DTOs.GSM02502;
using GSM02500COMMON.DTOs.GSM02520;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSM02500COMMON
{
    public interface IGSM02502 : R_IServiceCRUDBase<GSM02502ParameterDTO>
    {
        IAsyncEnumerable<GSM02502DTO> GetUnitTypeCategoryList();
        IAsyncEnumerable<PropertyTypeDTO> GetPropertyTypeList();
        GSM02500ActiveInactiveResultDTO RSP_GS_ACTIVE_INACTIVE_UNIT_TYPE_CATEGORYMethod(GSM02500ActiveInactiveParameterDTO poParam);
        TemplateUnitTypeCategoryDTO DownloadTemplateUnitTypeCategory();
    }
}
