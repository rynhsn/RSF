using System;

namespace Lookup_GSCOMMON.DTOs
{
    public class GSL02500DTO
    {
        public string CCOMPANY_ID { get; set; }
        public string CCB_CODE { get; set; } 
        public string CDEPT_CODE { get; set; }
        public string CDEPT_NAME { get; set; }
        public string CCB_NAME { get; set; }
        public string CCB_TYPE { get; set; }
        public string CBANK_TYPE { get; set; }
        public string CCB_ACCOUNT_NO { get; set; }
        public string CCB_ACCOUNT_NAME { get; set; }
        public string CCURRENCY_CODE { get; set; }
        public string CCB_GLACCOUNT_NO { get; set; }
        public string CBCHG_GLACCOUNT_NO { get; set; }
        public string CCREATE_BY { get; set; }
        public DateTime? DCREATE_DATE { get; set; }
        public string CUPDATE_BY { get; set; }
        public DateTime? DUPDATE_DATE { get; set; }
    }

}
