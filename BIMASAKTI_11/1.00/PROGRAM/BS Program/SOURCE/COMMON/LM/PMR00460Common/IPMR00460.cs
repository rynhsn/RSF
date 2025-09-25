using PMR00460Common.DTOs;
using PMR00460Common.Params;

namespace PMR00460Common
{
    public interface IPMR00460
    {
        PMR00460ListDTO<PMR00460PropertyDTO> PMR00460GetPropertyList();
        PMR00460SingleDTO<PMR00460PeriodYearRangeDTO> PMR00460GetYearRange();
        PMR00460SingleDTO<PMR00460DefaultParamDTO> PMR00460GetDefaultParam(PMR00460DefaultParamParam poParam);
    }
}