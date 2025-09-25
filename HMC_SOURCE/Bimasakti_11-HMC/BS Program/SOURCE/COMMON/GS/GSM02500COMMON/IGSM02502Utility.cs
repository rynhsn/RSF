using GSM02500COMMON.DTOs.GSM02502Utility;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSM02500COMMON
{
    public interface IGSM02502Utility : R_IServiceCRUDBase<GSM02502UtilityParameterDTO>
    {
        IAsyncEnumerable<GSM02502UtilityDTO> GetUtilityList();
    }
}
