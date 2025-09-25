using PMM03700COMMON.DTO_s;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;

namespace PMM03700COMMON
{
    public interface IPMM03710:R_IServiceCRUDBase<TenantClassificationDTO>
    {
        IAsyncEnumerable<TenantClassificationDTO> GetTenantClassificationList();
        IAsyncEnumerable<TenantDTO> GetAssignedTenantList();
        IAsyncEnumerable<TenantDTO> GetAvailableTenantList();
        IAsyncEnumerable<TenantDTO> GetTenantToMoveList();
        TenantResultDumpDTO AssignTenant(TenantParamDTO poParam);
        TenantResultDumpDTO MoveTenant(TenantMoveParamDTO poParam);
    }

}
