using System;
using System.Collections.Generic;
using System.Text;

namespace CBT02200COMMON.DTO.CBT02210
{
    public class CBT02210DetailDTO
    {
        public string CREC_ID { get; set; } = "";
        public string CBSIS { get; set; } = "";
        public string CINPUT_TYPE { get; set; } = "";
        public int INO { get; set; } = 0;
        public string CGLACCOUNT_NO { get; set; } = "";
        public string CGLACCOUNT_NAME { get; set; } = "";
        public string CCENTER_CODE { get; set; } = "";
        public string CCENTER_NAME { get; set; } = "";
        public string CCASH_FLOW_CODE { get; set; } = "";
        public string CCASH_FLOW_NAME { get; set; } = "";
        public string CCASH_FLOW_GROUP_CODE { get; set; } = "";
        public string CDBCR { get; set; } = "";
        public decimal NTRANS_AMOUNT { get; set; } = 0;
        public decimal NDEBIT { get; set; } = 0;
        public decimal NCREDIT { get; set; } = 0;
        public string CDETAIL_DESC { get; set; } = "";
        public string CDOCUMENT_NO { get; set; } = "";
        public string CDOCUMENT_DATE { get; set; } = "";
        public DateTime? DDOCUMENT_DATE { get; set; }
        public decimal NLDEBIT { get; set; } = 0;
        public decimal NLCREDIT { get; set; } = 0;
        public decimal NBDEBIT { get; set; } = 0;
        public decimal NBCREDIT { get; set; } = 0;
    }
}
