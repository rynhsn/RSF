using PMB01600Common.DTOs;
using PMB01600Common.Params;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PMB01600Common
{
    public interface IPMB01600
    {
        Task<PMB01600ListDTO<PMB01600PropertyDTO>> PMB01600GetPropertyList();
        Task<PMB01600SingleDTO<PMB01600YearRangeDTO>> PMB01600GetYearRange();
        Task<PMB01600ListDTO<PMB01600PeriodDTO>> PMB01600GetPeriodList(PMB01600PeriodParam poParam);
        Task<PMB01600SingleDTO<PMB01600SystemParamDTO>> PMB01600GetSystemParam(PMB01600SystemParamParam poParam);
        IAsyncEnumerable<PMB01600BillingStatementHeaderDTO> PMB01600GetBillingStatementHeaderListStream();
        IAsyncEnumerable<PMB01600BillingStatementDetailDTO> PMB01600GetBillingStatementDetailListStream();
    }
}
