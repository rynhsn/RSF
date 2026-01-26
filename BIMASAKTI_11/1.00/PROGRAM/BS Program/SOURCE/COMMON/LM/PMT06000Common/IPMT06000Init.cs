using PMT06000Common.DTOs;
using PMT06000Common.Params;

namespace PMT06000Common
{
    public interface IPMT06000Init
    {
        PMT06000ListDTO<PMT06000PropertyDTO> PMT06000GetPropertyList();
        PMT06000ListDTO<PMT06000PeriodDTO> PMT06000GetPeriodList(PMT06000YearParam poParams);
        PMT06000SingleDTO<PMT06000YearRangeDTO> PMT06000GetYearRange();
        PMT06000SingleDTO<PMT06000GetSystemParamResultDTO> PMT06000GetSystemParam(PMT06000GetSystemParamParameterDTO poParam);
    }
}