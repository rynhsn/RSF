using System.Collections.Generic;
using LMM02500Common.DTOs;
using R_CommonFrontBackAPI;

namespace LMM02500Common
{
    public interface ILMM02500TenantList : R_IServiceCRUDBase<LMM02500TenantListGroupDTO>
    {
        IAsyncEnumerable<LMM02500TenantListGroupDTO> LMM02500GetTenantListGroup();
    }
}