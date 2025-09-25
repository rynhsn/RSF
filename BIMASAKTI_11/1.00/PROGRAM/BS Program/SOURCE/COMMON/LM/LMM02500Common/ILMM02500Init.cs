using LMM02500Common.DTOs;

namespace LMM02500Common
{
    public interface ILMM02500Init
    {
        LMM02500ListDTO<LMM02500PropertyDTO> LMM02500GetPropertyList();
    }
}