using PMR00600COMMON.DTO_s;
using System;
using System.Collections.Generic;

namespace PMR00600COMMON
{
    public interface IPMR00600General
    {
        PMR00600Result<TodayDTO> GetTodayDate();
        IAsyncEnumerable<PropertyDTO> GetPropertyList();
        PMR00600Result<PeriodYearDTO> GetPeriodYearRecord(PeriodYearDTO poParam);
    }
}
