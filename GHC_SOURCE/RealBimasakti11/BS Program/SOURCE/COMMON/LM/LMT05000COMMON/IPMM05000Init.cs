using PMT05000COMMON.DTO_s;
using System.Collections.Generic;

namespace PMT05000COMMON
{
    public interface IPMM05000Init
    {
        IAsyncEnumerable<PropertyDTO> GetPropertyList();
        IAsyncEnumerable<GSB_CodeInfoDTO> GetGSBCodeInfoList();
        IAsyncEnumerable<GSPeriodDT_DTO> GetGSPeriodDTList();
    }
}
