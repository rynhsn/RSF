using System.Collections.Generic;

namespace PMM04700Common.DTOs;

public class PricingParamDTO : PricingDTO
{
    public string CUSER_ID { get; set; }
    public string CACTION { get; set; }
    public string CTYPE { get; set; }
    public string CVALID_FROM_DATE { get; set; }
}
public class PricingSaveParamDTO : PricingParamDTO
{
    public List<PricingBulkSaveDTO> PRICING_LIST { get; set; }
}
public class PricingRateSaveParamDTO : PricingParamDTO
{
    public string CRATE_DATE { get; set; }
    public List<PricingRateBulkSaveDTO> PRICING_RATE_LIST { get; set; }
}