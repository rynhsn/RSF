using FAM00100Common.DTOs;
using FAM00100Common.DTOs.FAM00100;

namespace FAM00100Common
{
    public interface IFAM00100
    {
        FAM00100SingleResult<FAM00100ValidateInitDTO> GetInitValidate();
        FAM00100SingleResult<FAM00100DTO> GetSystemParamCB();
        FAM00100SingleResult<FAM00100GSPeriodYearRangeDTO> GetGSPeriodYearRange();
        FAM00100SingleResult<FAM00100DTO> SaveSystemParamCB(FAM00100SaveParameterDTO poEntity);
    }
}
