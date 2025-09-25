using R_CommonFrontBackAPI;
using System.Collections.Generic;

namespace PMM01500COMMON
{
    public interface IPMM01520
    {
        IAsyncEnumerable<PMM01520DTO> GetPinaltyDateListStream();
        PMM01500SingleResult<PMM01520DTO> GetPinaltyDate(PMM01520DTO poEntity);
        PMM01500SingleResult<PMM01520DTO> GetPinalty(PMM01520DTO poEntity);
        PMM01500SingleResult<PMM01520DTO> SaveDeletePinalty(PMM01520SaveParameterDTO<PMM01520DTO> poEntity);
    }

}
