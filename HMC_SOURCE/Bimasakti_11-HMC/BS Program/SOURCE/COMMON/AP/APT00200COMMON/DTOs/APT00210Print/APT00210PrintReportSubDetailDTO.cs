using System;
using System.Collections.Generic;
using System.Text;

namespace APT00200COMMON.DTOs.APT00210Print
{
    public class APT00210PrintReportSubDetailDTO
    {
        public string CGLACCOUNT_NO { get; set; } = "";
        public string CGLACCOUNT_NAME { get; set; } = "";
        public string CCENTER_NAME { get; set; } = "";
        public string CCURRENCY_CODE { get; set; } = "";
        public decimal NDEBIT { get; set; } = 0;
        public decimal NCREDIT { get; set; } = 0;
    }
}
