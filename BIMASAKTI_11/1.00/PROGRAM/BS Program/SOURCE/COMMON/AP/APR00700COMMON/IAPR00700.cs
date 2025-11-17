using APR00700COMMON.DTO_s;
using System;
using System.Collections.Generic;
using System.Text;

namespace APR00700COMMON
{
    public interface IAPR00700
    {
        IAsyncEnumerable<APR00700SPResultDTO> GetReportData();
    }
}
