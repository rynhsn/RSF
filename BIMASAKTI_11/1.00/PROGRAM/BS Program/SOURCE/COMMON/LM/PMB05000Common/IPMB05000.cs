using System.Collections.Generic;
using PMB05000Common.DTOs;
using PMB05000Common.Params;

namespace PMB05000Common
{
    public interface IPMB05000
    {
        PMB05000SingleDTO<PMB05000SystemParamDTO> PMB05000GetSystemParam();
        PMB05000SingleDTO<PMB05000PeriodYearRangeDTO> PMB05000GetPeriod();
        PMB05000SingleDTO<PMB05000PeriodParam> PMB05000UpdateSoftPeriod(PMB05000PeriodParam poParams);
        IAsyncEnumerable<PMB05000ValidateSoftCloseDTO> PMB05000ValidateSoftPeriod();
        PMB05000SingleDTO<PMB05000SoftClosePeriodDTO> PMB05000ProcessSoftPeriod(PMB05000PeriodParam poParams);
    }
}