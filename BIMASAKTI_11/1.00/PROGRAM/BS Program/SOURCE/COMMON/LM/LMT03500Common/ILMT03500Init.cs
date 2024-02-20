using LMT03500Common.DTOs;
using R_CommonFrontBackAPI;

namespace LMT03500Common
{
    public interface ILMT03500Init
    {
        LMT03500ListDTO<LMT03500PropertyDTO> LMT03500GetPropertyList();
        LMT03500ListDTO<LMT03500TransCodeDTO> LMT03500GetTransCodeList();
    }
}