namespace FAT00100Common.DTOs
{
    /// <summary>
    /// Grid display DTO for FAT0010002 - Fixed Asset Acquisition Detail
    /// </summary>
    public class FAT0010002GridDTO
    {
        public string CASSET_CODE { get; set; } = string.Empty;
        public string CTRANS_SEQNO { get; set; } = string.Empty;
        public string CASSET_TRANS_SEQNO { get; set; } = string.Empty;
        public decimal NTRANSACTION_AMOUNT1 { get; set; }
        public decimal NLTRANSACTION_AMOUNT1 { get; set; }
        public decimal NBTRANSACTION_AMOUNT1 { get; set; }
        public int ITRANSACTION_QTY1 { get; set; }
        public string CUNIT { get; set; } = string.Empty;
        public string CASSET_DEPT_CODE { get; set; } = string.Empty;
        public string CASSET_DEPT_NAME { get; set; } = string.Empty;
        public string CASSET_LOCATION { get; set; } = string.Empty;
        public string CJRNGRP_CODE { get; set; } = string.Empty;
        public string CJRNGRP_DESC { get; set; } = string.Empty;
        public string CJRNGRP_NAME { get; set; } = string.Empty;
        public string CTAX_CATEGORY_CODE { get; set; } = string.Empty;
        public string CTAX_CATEGORY_DESC { get; set; } = string.Empty;
        public string CCATEGORY_CODE { get; set; } = string.Empty;
        public string CCATEGORY_DESC { get; set; } = string.Empty;
        public string CDEPR_METHOD { get; set; } = string.Empty;
        public string CDEPR_METHOD_DESC { get; set; } = string.Empty;
        public string CASSET_NAME { get; set; } = string.Empty;
        public string CASSET_OWNER { get; set; } = string.Empty;
    }
}

