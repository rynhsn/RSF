using System.Collections.Generic;
using CBB00200Common.DTOs;
using CBB00200Common.Params;

namespace CBB00200Common
{
    public interface ICBB00200
    {
        
        CBB00200SingleDTO<CBB00200SystemParamDTO> CBB00200GetSystemParam();
        // CBB00200SingleDTO<CBB00200ClosePeriodResultDTO> CBB00200ClosePeriod(CBB00200ClosePeriodParam poParams);
        IAsyncEnumerable<CBB00200ClosePeriodToDoListDTO> CBB00200SoftClosePeriodStream();
    }
}