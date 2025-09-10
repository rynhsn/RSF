using PMR00210COMMON.DTO_s;
using System;
using System.Collections.Generic;

namespace PMR00210COMMON
{
    public interface IPMR00210
    {
        IAsyncEnumerable<PMR00210SPResultDTO> GetReportData();
    }
}
