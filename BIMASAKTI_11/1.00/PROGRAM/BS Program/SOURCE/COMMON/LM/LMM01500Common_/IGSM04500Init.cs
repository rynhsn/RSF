using LMM01500Common.DTOs;

namespace LMM01500Common
{
    public interface ILMM01500Init
    {
        LMM01500ListDTO<LMM01500PropertyDTO> LMM01500GetPropertyList();
    }
}