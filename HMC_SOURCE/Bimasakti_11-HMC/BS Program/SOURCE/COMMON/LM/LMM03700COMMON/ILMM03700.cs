using LMM03700Common.DTO_s;
using R_CommonFrontBackAPI;
using System.Collections.Generic;

namespace LMM03700Common
{
    public interface ILMM03700 : R_IServiceCRUDBase<TenantClassificationGroupDTO>
    {
        IAsyncEnumerable<TenantClassificationGroupDTO> GetTenantClassGroupList();
        IAsyncEnumerable<PropertyDTO> LMM03700GetPropertyData();
    }
}
