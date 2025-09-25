using PMR00200COMMON.DTO_s;
using System;
using System.Collections.Generic;

namespace PMR00200COMMON
{
    public interface IPMR00200General
    {
        PMR00200ResultBaseDTO<TodayDTO> GetTodayDate();
        IAsyncEnumerable<PropertyDTO> GetPropertyList();
        IAsyncEnumerable<PeriodDtDTO> GetPeriodList();
        PMR00200ResultBaseDTO<PeriodYearDTO> GetPeriodYearRecord(PeriodYearDTO poParam);
    }
}
