using GSM02500COMMON.DTOs.GSM02550;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSM02500BACK
{
    public interface IGSM02550 : R_IServiceCRUDAsyncBase<GSM02550ParameterDTO>
    {
        IAsyncEnumerable<GSM02550DTO> GetUserPropertyList();
        IAsyncEnumerable<GetUserIdNameDTO> GetUserIdNameList();
        Task<GetUserIdNameResultDTO> GetUserIdName(GetUserIdNameParameterDTO poParam);
    }
}
