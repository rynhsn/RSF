using PMT02100COMMON.DTOs.PMT02110;
using PMT02100COMMON.DTOs.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMT02100COMMON
{
    public interface IPMT02110
    {
        PMT02100VoidResultHelperDTO ConfirmScheduleProcess(PMT02110ConfirmParameterDTO poParam);
        PMT02110TenantResultDTO GetTenantDetail(PMT02110TenantParameterDTO poParam);
    }
}
