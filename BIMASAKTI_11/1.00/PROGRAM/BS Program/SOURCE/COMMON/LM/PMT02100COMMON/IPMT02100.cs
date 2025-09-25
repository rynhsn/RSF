using PMT02100COMMON.DTOs.PMT02100;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMT02100COMMON
{
    public interface IPMT02100
    {
        IAsyncEnumerable<PMT02100HandoverDTO> GetHandoverList();
        IAsyncEnumerable<PMT02100HandoverBuildingDTO> GetHandoverBuildingList();
        IAsyncEnumerable<GetPropertyListDTO> GetPropertyList();
        GetPMSystemParamResultDTO GetPMSystemParam(GetPMSystemParamParameterDTO poParameter);
    }
}
