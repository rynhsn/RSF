using ICR00100Common.DTOs;

namespace ICR00100Common
{
    public interface IICR00100
    {
        ICR00100SingleDTO<ICR00100PeriodYearRangeDTO> ICR00100GetPeriodYearRange();
        ICR00100ListDTO<ICR00100PropertyDTO> ICR00100GetPropertyList();
    }
}