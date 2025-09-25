using APT00200COMMON.DTOs.APT00230;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace APT00200COMMON
{
    public interface IAPT00230 : R_IServiceCRUDBase<APT00230ParameterDTO>
    {
        APT00230ResultDTO GetSummaryInfo(GetSummaryParameterDTO poParameter);
    }
}
