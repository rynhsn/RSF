using PMR00220COMMON.DTO_s;
using System;
using System.Collections.Generic;

namespace PMR00220COMMON
{
    public interface IPMR00220General
    {
        PMR00220ResultBaseDTO<TodayDTO> GetTodayDate();
        IAsyncEnumerable<PropertyDTO> GetPropertyList();
        IAsyncEnumerable<PeriodDtDTO> GetPeriodList();
        PMR00220ResultBaseDTO<PeriodYearDTO> GetPeriodYearRecord(PeriodYearDTO poParam);
    }
}
