using PMR00210COMMON.DTO_s;
using System;
using System.Collections.Generic;

namespace PMR00210COMMON
{
    public interface IPMR00210General
    {
        PMR00210ResultBaseDTO<TodayDTO> GetTodayDate();
        IAsyncEnumerable<PropertyDTO> GetPropertyList();
        IAsyncEnumerable<PeriodDtDTO> GetPeriodList();
        PMR00210ResultBaseDTO<PeriodYearDTO> GetPeriodYearRecord(PeriodYearDTO poParam);
    }
}
