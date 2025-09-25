using System;
using System.Collections.Generic;
using System.Text;

namespace APT00100COMMON.DTOs.APT00110Print
{
    public class APT00110PrintReportSubDetailDTO
    {
        public string CGLACCOUNT_NO { get; set; } = "";
        public string CGLACCOUNT_NAME { get; set; } = "";
        public string CCENTER_NAME { get; set; } = "";
        public string CCURRENCY_CODE { get; set; } = "";
        public decimal NDEBIT { get; set; } = 0;
        public decimal NCREDIT { get; set; } = 0;
    }
}
