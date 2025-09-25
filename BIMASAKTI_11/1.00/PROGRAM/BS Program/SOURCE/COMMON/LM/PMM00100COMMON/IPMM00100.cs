using PMM00100COMMON.DTO_s;
using PMM00100COMMON.DTO_s.General;
using PMM00100COMMON.DTO_s.Helper;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMM00100COMMON
{
    public interface IPMM00100
    {
        IAsyncEnumerable<PropertyDTO> GetPropertyList();

        IAsyncEnumerable<PeriodDtDTO> GetPeriodDtList();

        IAsyncEnumerable<GeneralTypeDTO> GetGSBCodeInfoList();

        GeneralAPIResultDTO<PeriodYearRangeDTO> GetPeriodYearRangeRecord(PeriodYearRangeParamDTO poParam);

    }
}
