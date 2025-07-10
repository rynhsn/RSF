using System.Collections.Generic;
using ICB00100Common.DTOs;
using ICB00100Common.Params;

namespace ICB00100Common
{
    public interface IICB00100 
    {
        // ICB00100SingleDTO<ICB00100DTO> ICB00100GetSoftClosePeriod();
        ICB00100ListDTO<ICB00100PropertyDTO> ICB00100GetPropertyList();
        
        #region update spec
        
        ICB00100SingleDTO<ICB00100SystemParamDTO> ICB00100GetSystemParam(ICB00100SystemParamParam poParam);
        ICB00100SingleDTO<ICB00100PeriodYearRangeDTO> ICB00100GetPeriodYearRange();
        ICB00100SingleDTO<ICB00100PeriodParam> ICB00100UpdateSoftPeriod(ICB00100PeriodParam poParams);
        IAsyncEnumerable<ICB00100ValidateSoftCloseDTO> ICB00100ValidateSoftPeriod();
        ICB00100SingleDTO<ICB00100SoftClosePeriodDTO> ICB00100ProcessSoftPeriod(ICB00100PeriodParam poParams);
        
        #endregion
        // ICB00100SingleDTO<ICB00100NextPeriodDTO> ICB00100GetNextPeriod();
        // ICB00100ListDTO<ICB00100PeriodDTO> ICB00100GetPeriodList(ICB00100PeriodParam poParam);
        // ICB00100SingleDTO<ICB00100PeriodParam> ICB00100UpdateSoftPeriod(ICB00100PeriodParam poParams);
        // ICB00100ListDTO<ICB00100SoftClosePeriodToDoListDTO> ICB00100SoftClosePeriodStream();
    }
}