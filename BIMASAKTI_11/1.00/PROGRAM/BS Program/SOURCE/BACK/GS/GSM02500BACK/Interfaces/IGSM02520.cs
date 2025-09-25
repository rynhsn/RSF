using GSM02500COMMON.DTOs;
using GSM02500COMMON.DTOs.GSM02500;
using GSM02500COMMON.DTOs.GSM02510;
using GSM02500COMMON.DTOs.GSM02520;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSM02500BACK
{
    public interface IGSM02520 : R_IServiceCRUDAsyncBase<GSM02520ParameterDTO>
    {
        IAsyncEnumerable<GSM02520DTO> GetFloorList();
        Task<GSM02500ActiveInactiveResultDTO> RSP_GS_ACTIVE_INACTIVE_FLOORMethod(GSM02500ActiveInactiveParameterDTO poParam);
        Task<TemplateFloorDTO> DownloadTemplateFloor();
    }
}
