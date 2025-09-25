using System.Collections.Generic;
using PMT06000Common.DTOs;
using PMT06000Common.Params;
using R_CommonFrontBackAPI;

namespace PMT06000Common
{
    public interface IPMT06000Overtime : R_IServiceCRUDBase<PMT06000OvtDTO>
    {
        IAsyncEnumerable<PMT06000OvtGridDTO> PMT06000GetOvertimeListStream();
        IAsyncEnumerable<PMT06000OvtServiceGridDTO> PMT06000GetOvertimeServiceListStream();
        IAsyncEnumerable<PMT06000OvtUnitDTO> PMT06000GetOvertimeUnitListStream();
        PMT06000SingleDTO<PMT06000PropertyDTO> PMT06000ProcessSubmit(PMT06000ProcessSubmitParam poParameter);
    }
}