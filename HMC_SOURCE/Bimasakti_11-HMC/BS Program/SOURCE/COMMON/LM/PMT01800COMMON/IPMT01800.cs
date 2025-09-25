using System.Collections.Generic;
using PMT01800COMMON.DTO;
using R_CommonFrontBackAPI;

namespace PMT01800COMMON
{
    public interface  IPMT01800 : R_IServiceCRUDBase<PMT01800DTO>
    {
        IAsyncEnumerable<PMT01800PropertyDTO> GetPropertySream();
        IAsyncEnumerable<PMT01800DTO> GetPMT01800Stream();
        IAsyncEnumerable<PMT01800DTO> GetPMT01800DetailStream();
        PMT01800InitialProcessDTO GetInitDayStream();
    }
}