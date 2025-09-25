using System;
using System.Collections.Generic;
using System.Text;

namespace CBT01200Common.DTOs
{ 
   public class CBT01201DTO

   { 
        public string CREC_ID { get; set; }
        public int INO { get; set; }
        public string CSEQ_NO { get; set; }
        public string CDEPT_CODE { get; set; }
        public string CREF_NO { get; set; }
        public string CREF_DATE { get; set; }
        public string CGLACCOUNT_NO { get; set; }
        public string CGLACCOUNT_NAME { get; set; }
        public string CCENTER_CODE { get; set; }
        public string CCENTER_NAME { get; set; }
        public string CDBCR { get; set; }
        //public char CDBCR { get; set; }
        public string CCURRENCY_CODE { get; set; }
        public decimal NTRANS_AMOUNT { get; set; }
        public decimal NLTRANS_AMOUNT { get; set; }
        public decimal NBTRANS_AMOUNT { get; set; }
        public decimal NAMOUNT { get; set; }
        public string CDETAIL_DESC { get; set; }
        public string CDOCUMENT_NO { get; set; }
        public string? CDOCUMENT_DATE { get; set; }
        public DateTime? DDOCUMENT_DATE { get; set; }
        public decimal NDEBIT { get; set; }
        public decimal NCREDIT { get; set; }
        public decimal NLDEBIT { get; set; }
        public decimal NLCREDIT { get; set; }
        public decimal NBDEBIT { get; set; }
        public decimal NBCREDIT { get; set; }
        public string CBSIS { get; set; }
        public string CCASH_FLOW_GROUP_CODE { get; set; }
        public string CCASH_FLOW_CODE { get; set; }
        public string CCASH_FLOW_NAME { get; set; }
    }
}
