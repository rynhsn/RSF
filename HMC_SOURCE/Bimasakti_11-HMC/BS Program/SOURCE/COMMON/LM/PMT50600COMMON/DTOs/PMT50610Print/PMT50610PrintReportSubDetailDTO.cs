using System;
using System.Collections.Generic;
using System.Text;

namespace PMT50600COMMON.DTOs.PMT50610Print
{
    public class PMT50610PrintReportSubDetailDTO
    {
        public string CGLACCOUNT_NO { get; set; } = "";
        public string CGLACCOUNT_NAME { get; set; } = "";
        public string CCENTER_NAME { get; set; } = "";
        public string CCURRENCY_CODE { get; set; } = "";
        public decimal NDEBIT { get; set; } = 0;
        public decimal NCREDIT { get; set; } = 0;
    }
}
