using PMM04500COMMON.DTO_s;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMM04500COMMON
{
    public interface IPMM04501 : R_IServiceCRUDBase<PricingRateSaveParamDTO>
    {
        PricingDumpResultDTO SavePricingRate(PricingRateSaveParamDTO poParam);
        IAsyncEnumerable<PricingRateDTO> GetPricingRateList();
        IAsyncEnumerable<PricingRateDTO> GetPricingRateDateList();

    }
}
