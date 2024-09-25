using APR00500Common.DTOs;
using APR00500Common.Params;

namespace APR00500Common
{
    public interface IAPR00500
    { 
        APR00500ListDTO<APR00500PropertyDTO> APR00500GetPropertyList();
        APR00500SingleDTO<APR00500PeriodYearRangeDTO> APR00500GetYearRange();
        APR00500SingleDTO<APR00500SystemParamDTO> APR00500GetSystemParam();
        APR00500SingleDTO<APR00500TransCodeInfoDTO> APR00500GetTransCodeInfo();
        APR00500ListDTO<APR00500PeriodDTO> APR00500GetPeriodList(APR00500PeriodParam poParam);
        APR00500ListDTO<APR00500FunctDTO> APR00500GetCodeInfoList();
    }
}