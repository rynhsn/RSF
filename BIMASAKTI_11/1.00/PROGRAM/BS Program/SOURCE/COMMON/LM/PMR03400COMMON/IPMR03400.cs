using PMR03400COMMON.DTO_s;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMR03400COMMON
{
    public interface IPMR03400
    {
        IAsyncEnumerable<PMR03400SPResultDTO> GetReportData();
    }
}
