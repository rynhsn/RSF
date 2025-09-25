using System.Collections.Generic;
using LMM02500Common.DTOs;

namespace LMM02500Common
{
    public interface ILMM02500List
    {
        IAsyncEnumerable<LMM02500TenantGroupDTO> LMM02500GetTenantGroupList();
    }
}