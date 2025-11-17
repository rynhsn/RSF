using PMR03300COMMON.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PMR03300COMMON
{
    public interface IPMR03300
    {
        Task<PMR03300ListDTO<PMR03300PropertyDTO>> PMR03300GetPropertyList();
        Task<PMR03300SingleDTO<PMR03300GetCompanyInfoDTO>> GetCompanyInfo();
        Task<PMR03300SingleDTO<PMR03300GetPeriodeYearRangeDTO>> GetPeriodeYearRange();
        Task<PMR03300SingleDTO<PMR03300GetSystemParamDTO>> GetSystemParam();
        Task<PMR03300ListDTO<PMR03300GetPeriodDtListDTO>> GetPeriodDtList();

    }
}
