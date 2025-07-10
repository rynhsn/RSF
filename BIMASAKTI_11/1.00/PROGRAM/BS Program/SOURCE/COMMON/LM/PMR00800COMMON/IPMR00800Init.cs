using PMR00800COMMON.DTO_s;
using PMR00800COMMON.DTO_s.General;
using PMR00800COMMON.DTO_s.Helper;
using System;
using System.Collections.Generic;

namespace PMR00800COMMON
{
    public interface IPMR00800Init
    {
        IAsyncEnumerable<PropertyDTO> GetPropertyList();

        IAsyncEnumerable<PeriodDtDTO> GetPeriodDtList();
        
        GeneralAPIResultDTO<PeriodYearRangeDTO> GetPeriodYearRangeRecord(PeriodYearRangeParamDTO poParam);
    }
}
