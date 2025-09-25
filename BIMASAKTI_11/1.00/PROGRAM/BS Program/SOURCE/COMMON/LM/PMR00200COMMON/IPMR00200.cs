using PMR00200COMMON.DTO_s;
using System;
using System.Collections.Generic;

namespace PMR00200COMMON
{
    public interface IPMR00200
    {
        IAsyncEnumerable<PMR00200SPResultDTO> GetReportData();
    }
}
