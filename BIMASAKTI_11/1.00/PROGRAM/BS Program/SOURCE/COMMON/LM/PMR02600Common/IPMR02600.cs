using PMR02600Common.DTOs;

namespace PMR02600Common
{
    public interface IPMR02600
    {
        PMR02600ListDTO<PMR02600PropertyDTO> PMR02600GetPropertyList();
    }
}