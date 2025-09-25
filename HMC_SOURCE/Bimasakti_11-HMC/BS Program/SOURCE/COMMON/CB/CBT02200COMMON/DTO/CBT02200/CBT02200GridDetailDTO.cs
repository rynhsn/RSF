using System;
using System.Collections.Generic;
using System.Text;

namespace CBT02200COMMON.DTO.CBT02200
{
    public class CBT02200GridDetailDTO
    {
        public string CREC_ID { get; set; }
        public int INO { get; set; }
        public string CGLACCOUNT_NO { get; set; }
        public string CGLACCOUNT_NAME { get; set; }
        public string CCENTER_NAME { get; set; }
        public string CCASH_FLOW_CODE { get; set; }
        public string CCASH_FLOW_NAME { get; set; }
        public decimal NDEBIT { get; set; }
        public decimal NCREDIT { get; set; }
        public string CDETAIL_DESC { get; set; }
        public string CDOCUMENT_NO { get; set; }
        public string CDOCUMENT_DATE { get; set; }
        public DateTime DDOCUMENT_DATE { get; set; }
        public decimal NLDEBIT { get; set; }
        public decimal NLCREDIT { get; set; }
        public decimal NBDEBIT { get; set; }
        public decimal NBCREDIT { get; set; }
        public string CDBCR { get; set; }
    }
}
