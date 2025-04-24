using PMB04000COMMON.DTO.DTOs;
using PMB04000COMMON.DTO.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMB04000COMMON.Interface
{
    public interface IPMB04000
    {
        IAsyncEnumerable<PMB04000DTO> GetInvReceipt_List();
        PeriodYearDTO GetPeriodYearRange();
        IAsyncEnumerable<TemplateDTO> GetTemplateList();
    }
}
