using System;
using System.Collections.Generic;
using System.Text;

namespace CBT01100COMMON
{
    public class CBT01111DTO : CBT01110DTO
    {
        public string CPARENT_ID { get; set; }
        public string CINPUT_TYPE { get; set; }
        public string CGLACCOUNT_NO { get; set; }
        public string CGLACCOUNT_NAME { get; set; }
        public string CCENTER_CODE { get; set; }
        public string CCENTER_NAME { get; set; }
        public string CCASH_FLOW_GROUP_CODE { get; set; }
        public string CCASH_FLOW_CODE { get; set; }
        public string CCASH_FLOW_NAME { get; set; }
        public string CDBCR { get; set; }
        public string CBSIS { get; set; }
        public decimal NDEBIT { get; set; }
        public decimal NCREDIT { get; set; }
        public string CDETAIL_DESC { get; set; }
        public string CDOCUMENT_NO { get; set; }
        public string CDOCUMENT_DATE { get; set; }
        public DateTime? DDOCUMENT_DATE { get; set; }
        public bool LSUSPENSE_ACCOUNT { get; set; }
    }
}
