using PMT50600COMMON.DTOs.PMT50630;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMT50600COMMON
{
    public interface IPMT50630 : R_IServiceCRUDBase<PMT50630ParameterDTO>
    {
        PMT50630ResultDTO GetSummaryInfo(GetSummaryParameterDTO poParameter);
    }
}
