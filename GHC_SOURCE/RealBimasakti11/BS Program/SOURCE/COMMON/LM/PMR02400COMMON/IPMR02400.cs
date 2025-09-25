using PMR02400COMMON.DTO_s;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMR02400COMMON
{
    public interface IPMR02400 
    {
        PMR02400Result<TodayDTO> GetTodayDate();
        IAsyncEnumerable<PropertyDTO> GetPropertyList();
        PMR02400Result<PeriodYearDTO> GetPeriodYearRecord(PeriodYearDTO poParam);
    }
}
