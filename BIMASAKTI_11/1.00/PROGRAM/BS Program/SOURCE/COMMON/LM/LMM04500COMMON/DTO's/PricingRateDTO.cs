using PMM04500COMMON.DTO_s;
using System;
using System.Globalization;

namespace PMM04500COMMON
{
    public class PricingRateDTO : PricingDTO
    {
        public string CRATE_DATE { get; set; }
        public DateTime? DRATE_DATE { get; set; }
    
        public string CCURRENCY_CODE { get; set; }
        public decimal NBASE_RATE_AMOUNT { get; set; }
        public decimal NCURRENCY_RATE_AMOUNT { get; set; }
    }
}