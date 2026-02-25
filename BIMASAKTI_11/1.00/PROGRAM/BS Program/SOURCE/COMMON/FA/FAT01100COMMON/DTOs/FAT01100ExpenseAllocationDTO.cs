using System;

namespace FAT01100Common.DTOs
{
    public class FAT01100ExpenseAllocationDTO
    {
        public string CCOMPANY_ID { get; set; } = string.Empty;
        public string CASSET_CODE { get; set; } = string.Empty;
        public string CLANGUAGE_ID { get; set; } = string.Empty;
        public string CEXPENSE_DEPT_CODE { get; set; } = string.Empty;
        public string CEXPENSE_DEPT_NAME { get; set; } = string.Empty;
        public decimal NEXPENSE_PCT { get; set; }
        public string CDEPT_CODE { get; set; } = string.Empty;

        public string CREF_NO { get; set; } = string.Empty;
        public string CREF_DATE { get; set; } = string.Empty;
        public DateTime? DREF_DATE { get; set; }
        public string CTRANS_SEQ_NO { get; set; } = string.Empty;
        public string CREC_ID { get; set; } = string.Empty;

    }
}
