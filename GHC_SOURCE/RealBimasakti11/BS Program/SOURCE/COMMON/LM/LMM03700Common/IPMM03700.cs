using PMM03700COMMON.DTO_s;
using R_CommonFrontBackAPI;
using System.Collections.Generic;

namespace PMM03700COMMON
{
    public interface IPMM03700 : R_IServiceCRUDBase<TenantClassificationGroupDTO>
    {
        IAsyncEnumerable<TenantClassificationGroupDTO> GetTenantClassGroupList();
        IAsyncEnumerable<PropertyDTO> PMM03700GetPropertyData();
    }
}
