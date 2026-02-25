using System;

namespace FAT01100Common.DTOs
{
    public class FAT01100ExpenseAllocationRSP_FA_GET_TRANS_EXP_ALLOC_LISTResultDTO
    {
        public string CEXPENSE_DEPT_CODE { get; set; } = string.Empty;
        public string CEXPENSE_DEPT_NAME { get; set; } = string.Empty;
        public decimal NEXPENSE_PCT { get; set; }
        public bool? LALLOW_DELETE { get; set; }
        public string CCREATE_BY { get; set; } = string.Empty;
        public DateTime? DCREATE_DATE { get; set; }
        public string CUPDATE_BY { get; set; } = string.Empty;
        public DateTime? DUPDATE_DATE { get; set; }
        public string CCOMPANY_ID { get; set; } = string.Empty;
        public string CDEPT_CODE { get; set; } = string.Empty;
        public string CTRANS_CODE { get; set; } = string.Empty;
        public string CREF_NO { get; set; } = string.Empty;
        public string CASSET_CODE { get; set; } = string.Empty;
        public string CASSET_TRANS_SEQNO { get; set; } = string.Empty;
        public bool LOLD_FLAG { get; set; }
    }
}
