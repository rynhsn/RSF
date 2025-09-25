using PMR00220COMMON.DTO_s;
using System;
using System.Collections.Generic;

namespace PMR00220COMMON
{
    public interface IPMR00220
    {
        IAsyncEnumerable<PMR00220SPResultDTO> GetReportData();
    }
}
