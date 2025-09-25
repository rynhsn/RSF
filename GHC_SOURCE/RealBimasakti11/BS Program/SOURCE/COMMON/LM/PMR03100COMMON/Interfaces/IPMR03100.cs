using System;
using System.Collections.Generic;
using System.Text;
using PMR03100COMMON.DTO_s.General;
using PMR03100COMMON.DTO_s.Helper;

namespace PMR03100COMMON.Interfaces
{
    public interface IPMR03100
    {
        IAsyncEnumerable<PropertyDTO> GetPropertyList();
        GeneralAPIResultDTO<PeriodYearRangeDTO> GetPeriodYearRangeRecord(PeriodYearRangeParamDTO poParam);
    }
}
