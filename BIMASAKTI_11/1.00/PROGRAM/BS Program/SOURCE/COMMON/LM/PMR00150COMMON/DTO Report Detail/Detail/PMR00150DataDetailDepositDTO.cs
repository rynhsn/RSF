using System;
using System.Collections.Generic;
using System.Text;

namespace PMR00150COMMON
{
    public class PMR00150DataDetailDepositDTO
    {
        public string? CDEPOSIT_DETAIL_ID { get; set; }
        public string? CDEPOSIT_DETAIL_DATE { get; set; }
        public DateTime? DDEPOSIT_DETAIL_DATE { get; set; }
        public decimal NDEPOSIT_DETAIL_AMOUNT { get; set; }
        public string? CDEPOSIT_DETAIL_DESCRIPTION { get; set; }
    }
}
