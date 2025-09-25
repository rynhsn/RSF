using PMT50600COMMON.DTOs.PMT50631;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMT50600COMMON
{
    public interface IPMT50631 : R_IServiceCRUDBase<PMT50631ParameterDTO>
    {
        IAsyncEnumerable<PMT50631DTO> GetAdditionalList();
    }
}
