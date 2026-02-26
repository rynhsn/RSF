using FAM00100Common.DTOs;
using FAM00100Common.DTOs.FAM00100;

namespace FAM00100Back
{
    public interface IFAM00100
    {
        Task<FAM00100SingleResult<FAM00100ValidateInitDTO>> GetInitValidate();

        Task<FAM00100SingleResult<FAM00100DTO>> GetSystemParamCB();
        Task<FAM00100SingleResult<FAM00100GSPeriodYearRangeDTO>> GetGSPeriodYearRange();

        Task<FAM00100SingleResult<FAM00100DTO>> SaveSystemParamCB(FAM00100SaveParameterDTO poEntity);
    }
}
