using PMR03400COMMON.DTO_s;
using PMR03400COMMON.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PMR03400COMMON
{
    public interface IPMR03400General
    {
        IAsyncEnumerable<PropertyDTO> GetPropertyList();
        IAsyncEnumerable<PeriodDtDTO> GetPeriodList();
        Task<PMR03400ResultBaseDTO<PeriodYearDTO>> GetPeriodYearRecord(PeriodYearDTO poParam);
        Task<PMR03400SingleDTO<PMR03400SystemParamDTO>> PMR03400GetSystemParam(PMR03400SystemParamDTO loParam);
    }
}
