using PMT02100COMMON.DTOs.PMT02110;
using PMT02100COMMON.DTOs.PMT02120;
using PMT02100COMMON.DTOs.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMT02100COMMON
{
    public interface IPMT02120
    {
        IAsyncEnumerable<PMT02120EmployeeListDTO> GetEmployeeList();
        IAsyncEnumerable<PMT02120RescheduleListDTO> GetRescheduleList();
        IAsyncEnumerable<PMT02120HandoverUtilityDTO> GetHandoverUtilityList();
        PMT02100VoidResultHelperDTO HandoverProcess(PMT02120HandoverProcessParameterDTO poParam);
        PMT02100VoidResultHelperDTO ReinviteProcess(PMT02120ReinviteProcessParameterDTO poParam);
        PMT02100VoidResultHelperDTO RescheduleProcess(PMT02120RescheduleProcessParameterDTO poParam);
        PMT02100VoidResultHelperDTO AssignEmployee(PMT02120AssignEmployeeParameterDTO poParam);
    }
}
