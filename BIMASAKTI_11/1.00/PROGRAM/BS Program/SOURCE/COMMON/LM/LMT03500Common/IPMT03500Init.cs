using PMT03500Common.DTOs;
using R_CommonFrontBackAPI;

namespace PMT03500Common
{
    public interface IPMT03500Init
    {
        PMT03500ListDTO<PMT03500PropertyDTO> PMT03500GetPropertyList();
        PMT03500ListDTO<PMT03500TransCodeDTO> PMT03500GetTransCodeList();
    }
}