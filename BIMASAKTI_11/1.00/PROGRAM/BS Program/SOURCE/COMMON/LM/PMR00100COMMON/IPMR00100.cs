using PMR00100Common.DTOs;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMR00100Common
{
    public interface IPMR00100 : R_IServiceCRUDBase<PMR00100DTO>
    {
        IAsyncEnumerable<PropertyDTO> GetPropertyList();
        PeriodYearRangeDTO GetPeriodYear();
        PeriodDT_DataDTO GetPeriodDTList(PMR00100ParamDTO poData);
        LOOStatusDataDTO GetLOOStatusList();
        IAsyncEnumerable<PMR00100DTO> GetLOOPrintList(PrintParamDTO poData);

    }
}
