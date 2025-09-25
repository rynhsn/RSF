using System;
using System.Collections.Generic;
using System.Text;

namespace PMR02400COMMON.DTO_s.Print
{
    public class SubtotalCurrencyDTO
    {
        public string CCURRENCY_CODE { get; set; }
        public string CCURRENCY_NAME { get; set; }
        public decimal NINVOICE_AMOUNT { get; set; }
        public decimal NREDEEMED_AMOUNT { get; set; }
        public decimal NPAID_AMOUNT { get; set; }
        public decimal NOUTSTANDING_AMOUNT { get; set; }
    }
}
