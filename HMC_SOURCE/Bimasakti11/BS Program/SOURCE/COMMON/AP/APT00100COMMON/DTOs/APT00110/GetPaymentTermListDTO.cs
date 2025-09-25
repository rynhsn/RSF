using System;
using System.Collections.Generic;
using System.Text;

namespace APT00100COMMON.DTOs.APT00110
{
    public class GetPaymentTermListDTO
    {
        public string CPAY_TERM_CODE { get; set; }
        public string CPAY_TERM_NAME { get; set; }
        public int IPAY_TERM_DAYS { get; set; }
    }
}
