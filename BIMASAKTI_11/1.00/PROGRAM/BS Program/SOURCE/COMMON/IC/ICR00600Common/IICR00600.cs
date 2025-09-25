using System;
using ICR00600Common.DTOs;
using ICR00600Common.Params;

namespace ICR00600Common
{
    public interface IICR00600
    {
        ICR00600ListDTO<ICR00600PropertyDTO> ICR00600GetPropertyList();
        ICR00600SingleDTO<ICR00600PeriodYearRangeDTO> ICR00600GetPeriodYearRange();
        ICR00600ListDTO<ICR00600PeriodDTO> ICR00600GetPeriodList(ICR00600PeriodParam poParam);
        ICR00600SingleDTO<ICR00600TransCodeDTO> ICR00600GetTransCode();
    }
}