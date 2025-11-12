using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace ICT00900COMMON.Utility_DTO
{
    public class LastCurrencyRateDTO
    {
        public string CCOMPANY_ID { get; set; }
        public string CCURRENCY_CODE { get; set; }
        public string CRATETYPE_CODE { get; set; }
        public string CRATE_DATE { get; set; }
        public decimal NLBASE_RATE_AMOUNT { get; set; }
        public decimal NLCURRENCY_RATE_AMOUNT { get; set; }
        public decimal NBBASE_RATE_AMOUNT { get; set; }
        public decimal NBCURRENCY_RATE_AMOUNT { get; set; }
    }
}
