using GSM02500COMMON.DTOs.GSM02550;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSM02500COMMON
{
    public interface IGSM02550 : R_IServiceCRUDBase<GSM02550ParameterDTO>
    {
        IAsyncEnumerable<GSM02550DTO> GetUserPropertyList();
        IAsyncEnumerable<GetUserIdNameDTO> GetUserIdNameList();
    }
}
