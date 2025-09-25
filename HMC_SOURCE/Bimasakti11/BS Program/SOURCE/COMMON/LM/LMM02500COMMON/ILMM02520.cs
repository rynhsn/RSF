using System.Collections.Generic;
using LMM02500Common.DTO;
using R_CommonFrontBackAPI;

namespace LMM02500Common
{
    public interface ILMM02520 : R_IServiceCRUDBase<LMM02520DTO>
    {
        IAsyncEnumerable<LMM02520GridDTO> GetAllTenantGroupListStream();
        SaveMoveTenantGroupResultDTO SaveMoveTenantGroupCategory(ObjectParameterLMM02500MoveTenantGroup poParameter);
    }
}