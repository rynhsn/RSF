using PMT02100COMMON.DTOs.PMT02130;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMT02100COMMON
{
    public interface IPMT02130
    {
        IAsyncEnumerable<PMT02130HandoverUnitDTO> GetHandoverUnitList();
        IAsyncEnumerable<PMT02130HandoverUnitChecklistDTO> GetHandoverUnitChecklistList();
    }
}
