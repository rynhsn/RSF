using GSM02500COMMON.DTOs;
using GSM02500COMMON.DTOs.GSM02500;
using GSM02500COMMON.DTOs.GSM02510;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSM02500BACK
{
    public interface IGSM02510 : R_IServiceCRUDAsyncBase<GSM02510ParameterDTO>
    {
        IAsyncEnumerable<GSM02510DTO> GetBuildingList();
        Task<GSM02500ActiveInactiveResultDTO> RSP_GS_ACTIVE_INACTIVE_BUILIDNGMethod(GSM02500ActiveInactiveParameterDTO poParam);
    }
}
