using System.Collections.Generic;
using PMT01800COMMON.DTO;
using R_CommonFrontBackAPI;

namespace PMT01800COMMON
{
    public interface IPMT01810 : R_IServiceCRUDBase<PMT01810DTO>
    {
        IAsyncEnumerable<PMT01810DTO> GetPMT01810Stream();
        IAsyncEnumerable<PMT01810DTO> GetPMT01810DetailStream();
    }
}