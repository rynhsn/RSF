using GSM00300COMMON.DTO_s;
using GSM00300COMMON.DTO_s.Helper;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSM00300COMMON
{
    public interface IGSM00301 : R_IServiceCRUDBase<TaxInfoDTO>
    {
        IAsyncEnumerable<GSBCodeInfoDTO> GetRftGSBCodeInfoList();
    }
}
