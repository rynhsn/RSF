using PMM04500COMMON.DTO_s;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMM04500COMMON
{
    public interface IPMM04500 : R_IServiceCRUDBase<PricingSaveParamDTO>
    {
        IAsyncEnumerable<PropertyDTO> GetPropertyList();
        IAsyncEnumerable<UnitTypeCategoryDTO> GetUnitTypeCategoryList();
        IAsyncEnumerable<PricingDTO> GetPricingList();
        IAsyncEnumerable<PricingDTO> GetPricingDateList();
        PricingDumpResultDTO SavePricing(PricingSaveParamDTO poParam);
        IAsyncEnumerable<TypeDTO> GetRftGSBCodeInfoList();
        PricingDumpResultDTO ActiveInactivePricing(PricingParamDTO poParam);
    }
}
