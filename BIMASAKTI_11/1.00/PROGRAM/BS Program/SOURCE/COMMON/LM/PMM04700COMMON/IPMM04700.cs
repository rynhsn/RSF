using System.Collections.Generic;
using PMM04700Common.DTOs;

namespace PMM04700Common;

public interface IPMM04700
{
    IAsyncEnumerable<PropertyDTO> GetPropertyList();
    IAsyncEnumerable<OtherUnitDTO> GetOtherUnitList();
    IAsyncEnumerable<PricingDTO> GetPricingList();
    IAsyncEnumerable<PricingDTO> GetPricingDateList();
    PricingDumpResultDTO SavePricing(PricingSaveParamDTO poParam);
    IAsyncEnumerable<TypeDTO> GetPriceChargesType();
    IAsyncEnumerable<TypeDTO> GetRftGSBCodeInfoList();
    PricingDumpResultDTO ActiveInactivePricing(PricingParamDTO poParam);
}