using APR00600COMMON.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace APR00600COMMON
{
    public interface IAPR00600
    {
        Task<APR00600ListDTO<APR00600PropertyDTO>> APR00600GetPropertyList();
        Task<APR00600SingleDTO<APR00600GetCompanyInfoDTO>> GetCompanyInfo();
        Task<APR00600SingleDTO<APR00600GetPeriodeYearRangeDTO>> GetPeriodeYearRange();
        Task<APR00600SingleDTO<APR00600GetSystemParamDTO>> GetSystemParam();
        Task<APR00600ListDTO<APR00600GetPeriodDtListDTO>> GetPeriodDtList();

    }
}
