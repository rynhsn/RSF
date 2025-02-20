using PMR00460Common.DTOs;

namespace PMR00460Common
{
    public interface IPMR00460
    {
        PMR00460ListDTO<PMR00460PropertyDTO> PMR00460GetPropertyList();
        PMR00460SingleDTO<PMR00460PeriodYearRangeDTO> PMR00460GetYearRange();
    }
}