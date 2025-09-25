using GSM02500COMMON.DTOs.GSM02503;
using R_APICommonDTO;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSM02500COMMON
{
    public interface IGSM02503Image : R_IServiceCRUDBase<GSM02503ImageParameterDTO>
    {
        IAsyncEnumerable<GSM02503ImageDTO> GetUnitTypeImageList();
        ShowUnitTypeImageResultDTO ShowUnitTypeImage(ShowUnitTypeImageParameterDTO poParam);
    }
}
