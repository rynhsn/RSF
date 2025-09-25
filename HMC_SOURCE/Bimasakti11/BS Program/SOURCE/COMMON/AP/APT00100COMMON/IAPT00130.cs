using APT00100COMMON.DTOs.APT00130;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace APT00100COMMON
{
    public interface IAPT00130 : R_IServiceCRUDBase<APT00130ParameterDTO>
    {
        APT00130ResultDTO GetSummaryInfo(GetSummaryParameterDTO poParameter);
    }
}
