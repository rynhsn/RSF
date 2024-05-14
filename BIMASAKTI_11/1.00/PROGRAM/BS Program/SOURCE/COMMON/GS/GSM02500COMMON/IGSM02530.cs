using GSM02500COMMON.DTOs;
using GSM02500COMMON.DTOs.GSM02500;
using GSM02500COMMON.DTOs.GSM02520;
using GSM02500COMMON.DTOs.GSM02530;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSM02500COMMON
{
    public interface IGSM02530 : R_IServiceCRUDBase<GSM02530ParameterDTO>
    {
        IAsyncEnumerable<GSM02530DTO> GetUnitInfoList();
        GSM02500ActiveInactiveResultDTO RSP_GS_ACTIVE_INACTIVE_BUILDING_UNITMethod(GSM02500ActiveInactiveParameterDTO poParam);
        TemplateUnitDTO DownloadTemplateUnit();
    }
}
