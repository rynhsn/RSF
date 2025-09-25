using GSM02500COMMON.DTOs.GSM02500;
using GSM02500COMMON.DTOs.GSM02504;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSM02500BACK
{
    public interface IGSM02504 : R_IServiceCRUDAsyncBase<GSM02504ParameterDTO>
    {
        IAsyncEnumerable<GSM02504DTO> GetUnitViewList();
    }
}
