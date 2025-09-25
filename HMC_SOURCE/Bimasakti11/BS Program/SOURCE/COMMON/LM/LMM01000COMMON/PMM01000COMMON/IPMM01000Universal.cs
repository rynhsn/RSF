using System.Collections.Generic;

namespace PMM01000COMMON
{
    public interface IPMM01000Universal
    {
        IAsyncEnumerable<PMM01000UniversalDTO> GetChargesTypeList();
        IAsyncEnumerable<PMM01000UniversalDTO> GetStatusList();
        IAsyncEnumerable<PMM01000UniversalDTO> GetTaxExemptionCodeList();
        IAsyncEnumerable<PMM01000UniversalDTO> GetWithholdingTaxTypeList();
        IAsyncEnumerable<PMM01000UniversalDTO> GetUsageRateModeList();
        IAsyncEnumerable<PMM01000UniversalDTO> GetRateTypeList();
        IAsyncEnumerable<PMM01000UniversalDTO> GetAdminFeeTypeList();
        IAsyncEnumerable<PMM01000UniversalDTO> GetAccrualMethodList();
        IAsyncEnumerable<PMM01000DTOPropety> GetProperty();

        PMM01000AllResultInit GetAllInitLMM01000List();
    }

}
