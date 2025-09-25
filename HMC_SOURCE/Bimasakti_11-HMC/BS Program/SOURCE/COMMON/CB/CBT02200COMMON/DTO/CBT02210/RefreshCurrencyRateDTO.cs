using System;
using System.Collections.Generic;
using System.Text;

namespace CBT02200COMMON.DTO.CBT02210
{
    public class RefreshCurrencyRateDTO
    {
        public decimal NLBASE_RATE_AMOUNT { get; set; } = 0;
        public decimal NLCURRENCY_RATE_AMOUNT { get; set; } = 0;
        public decimal NBBASE_RATE_AMOUNT { get; set; } = 0;
        public decimal NBCURRENCY_RATE_AMOUNT { get; set; } = 0;
    }
}
