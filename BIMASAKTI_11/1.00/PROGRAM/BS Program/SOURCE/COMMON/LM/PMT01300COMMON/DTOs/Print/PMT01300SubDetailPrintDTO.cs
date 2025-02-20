using System;

namespace PMT01300COMMON
{
    public class PMT01300SubDetailPrintDTO
    {
        public string CGLACCOUNT_NO { get; set; }
        public string CGLACCOUNT_NAME { get; set; }
        public string CCENTER_CODE { get; set; }
        public string CCENTER_NAME { get; set; }
        public string CCURRENCY_CODE { get; set; }
        public decimal NDEBIT { get; set; }
        public decimal NCREDIT { get; set; }
    }
}
