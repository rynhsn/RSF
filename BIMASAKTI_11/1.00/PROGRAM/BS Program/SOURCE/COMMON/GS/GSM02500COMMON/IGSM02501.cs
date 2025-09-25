using GSM02500COMMON.DTOs;
using GSM02500COMMON.DTOs.GSM02501;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSM02500COMMON
{
    public interface IGSM02501 : R_IServiceCRUDBase<GSM02501DetailDTO>
    {
        IAsyncEnumerable<GSM02501PropertyDTO> GetPropertyList();
        GSM02500ActiveInactiveResultDTO RSP_GS_ACTIVE_INACTIVE_PROPERTYMethod(GSM02500ActiveInactiveParameterDTO poParam);
    }
}
