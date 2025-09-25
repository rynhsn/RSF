using GSM02500COMMON.DTOs.GSM02503;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSM02500BACK
{
    public interface IGSM02570 : R_IServiceCRUDAsyncBase<GSM02503ImageParameterDTO>
    {
        IAsyncEnumerable<GSM02503ImageDTO> GetUnitTypeImageList();
        Task<ShowUnitTypeImageResultDTO> ShowUnitTypeImage(ShowUnitTypeImageParameterDTO poParam);
    }
}
