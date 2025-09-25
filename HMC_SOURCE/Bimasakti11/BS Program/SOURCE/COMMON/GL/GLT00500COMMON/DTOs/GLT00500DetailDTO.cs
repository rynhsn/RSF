using System;
using System.Collections.Generic;
using System.Text;

namespace GLT00500COMMON.DTOs
{
    public class GLT00500DetailDTO
    {
        public int SEQ_NO { get; set; } = 0;
        public string CGLACCOUNT_NO { get; set; } = "";
        public string CGLACCOUNT_NAME { get; set; } = "";
        public string CCENTER_CODE { get; set; } = "";
        public string CDBCR { get; set; } = "";
        public decimal NTRANS_AMOUNT { get; set; } = 0;
        public decimal NDEBIT { get; set; } = 0;
        public decimal NCREDIT { get; set; } = 0;
        public string CDETAIL_DESC { get; set; } = "";
        public decimal NLDEBIT { get; set; } = 0;
        public decimal NLCREDIT { get; set; } = 0;
        public decimal NBDEBIT { get; set; } = 0;
        public decimal NBCREDIT { get; set; } = 0;
        public string CDOCUMENT_NO { get; set; } = "";
        public DateTime CDOCUMENT_DATE { get; set; }
        public string CVALID { get; set; } = "Y";
        public string CNOTES { get; set; } = "";

    }
}
