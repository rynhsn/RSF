using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GSM12000COMMON
{
    public interface IGSM12000 : R_IServiceCRUDAsyncBase<GSM12000DTO>
    {
        IAsyncEnumerable<GSM12000GSBCodeDTO> GetListMessageType();
        IAsyncEnumerable<GSM12000DTO> GetListMessage();
        GSM12000DTO GetActiveInactive(GSM12000DTO poParamDto);
    }
}
