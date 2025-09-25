using PMM00200COMMON.DTO_s;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMM00200COMMON
{
    public interface IPMM00200 : R_IServiceCRUDBase<PMM00200DTO>
    {
        IAsyncEnumerable<PMM00200GridDTO> GetUserParamList();
        PMM00200ActiveInactiveParamDTO GetActiveParam(ActiveInactiveParam poParam);
    }
}
