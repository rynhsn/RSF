using System;

namespace FAT00700Common.Print
{
    public class FAT00700PrintDataDTO
    {
        public string CDEPT_CODE { get; set; } = string.Empty;
        public string CDEPT_NAME { get; set; } = string.Empty;
        public string CREFERENCE_NO { get; set; } = string.Empty;
        public string CTRANS_STATUS_NAME { get; set; } = string.Empty;
        public string CCOMMIT_BY { get; set; } = string.Empty;
        public DateTime? DCOMMIT_DATE { get; set; }
        public string CTRANSACTION_DATE { get; set; } = string.Empty;
        public string CASSET_TRANS_SEQNO { get; set; } = string.Empty;
        public string CSTART_DATE { get; set; } = string.Empty;
        public string CASSET_CODE { get; set; } = string.Empty;
        public string CASSET_NAME { get; set; } = string.Empty;
        public string CASSET_LOCATION { get; set; } = string.Empty;
        public string CCATEGORY_DESC { get; set; } = string.Empty;
        public string CSERIAL_NUMBER { get; set; } = string.Empty;
        public decimal NTRANSACTION_AMOUNT { get; set; }
        public string CBASE_CURRENCY_CODE { get; set; } = string.Empty;
        public string CALLOC_EXPENSE_CODE { get; set; } = string.Empty;
        public string CTRANSACTION_DESCR { get; set; } = string.Empty;
    }
}
