using System;
using System.Collections.Generic;
using System.Text;

namespace PMM04500COMMON.DTO_s
{
    public class PricingRateBulkSaveDTO
    {
        public string CCURRENCY_CODE { get; set; }
        public decimal NBASE_RATE_AMOUNT { get; set; }
        public decimal NCURRENCY_RATE_AMOUNT { get; set; }
    }
}
