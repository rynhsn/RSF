using PMT06500Common.DTOs;
using PMT06500Common.Params;

namespace PMT06500Common
{
    public interface IPMT06500Init
    {
        PMT06500ListDTO<PMT06500PropertyDTO> PMT06500GetPropertyList();
        PMT06500ListDTO<PMT06500PeriodDTO> PMT06500GetPeriodList(PMT06500YearParam poParams);
        PMT06500SingleDTO<PMT06500YearRangeDTO> PMT06500GetYearRange();
        PMT06500SingleDTO<PMT06500TransCodeDTO> PMT06500GetTransCode(PMT06500TransCodeParam poParam);
    }
}