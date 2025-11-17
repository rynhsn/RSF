using APR00700COMMON.DTO_s;
using APR00700COMMON.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace APR00700COMMON
{
    public interface IAPR00700General
    {
        IAsyncEnumerable<PropertyDTO> GetPropertyList();
        IAsyncEnumerable<PeriodDtDTO> GetPeriodList();
        Task<APR00700ResultBaseDTO<PeriodYearDTO>> GetPeriodYearRecord(PeriodYearDTO poParam);
        Task<APR00700SingleDTO<APR00700SystemParamDTO>> APR00700GetSystemParam(APR00700SystemParamDTO loParam);
    }
}
