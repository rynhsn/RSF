using GSM02500COMMON.DTOs;
using GSM02500COMMON.DTOs.GSM02531;
using GSM02500COMMON.DTOs.GSM02530;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSM02500COMMON
{
    public interface IGSM02531 : R_IServiceCRUDBase<GSM02531UtilityParameterDTO>
    {
        IAsyncEnumerable<GSM02531UtilityTypeDTO> GetUtilityTypeList();
        IAsyncEnumerable<GSM02531UtilityDTO> GetUtilitiesList();
        GSM02500ActiveInactiveResultDTO RSP_GS_ACTIVE_INACTIVE_BUILDING_UNIT_UTILITIESMethod(GSM02500ActiveInactiveParameterDTO poParam);
        TemplateUnitUtilityDTO DownloadTemplateUnitUtility();
    }
}
