using LMT03500Common.DTOs;
using R_CommonFrontBackAPI;

namespace LMT03500Common
{
    public interface ILMT03500Init
    {
        LMT03500ListDTO<PMT03500PropertyDTO> LMT03500GetPropertyList();
        LMT03500ListDTO<PMT03500TransCodeDTO> LMT03500GetTransCodeList();
    }
}