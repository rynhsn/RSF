using System.Collections.Generic;
using PMM04700Common.DTOs;

namespace PMM04700Common;

public interface IPMM04701
{
    PricingDumpResultDTO SavePricingRate(PricingRateSaveParamDTO poParam);
    IAsyncEnumerable<PricingRateDTO> GetPricingRateList();
    IAsyncEnumerable<PricingRateDTO> GetPricingRateDateList();

}