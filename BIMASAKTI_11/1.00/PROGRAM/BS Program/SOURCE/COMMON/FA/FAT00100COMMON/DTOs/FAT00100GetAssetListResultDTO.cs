namespace FAT00100Common.DTOs
{
    /// <summary>
    /// Result DTO for GetAssetList streaming method
    /// </summary>
    public class FAT00100GetAssetListResultDTO
    {
        public string CASSET_CODE { get; set; } = string.Empty;
        public string CASSET_TRANS_SEQNO { get; set; } = string.Empty;
        public decimal NTRANSACTION_AMOUNT1 { get; set; }
        public decimal NLTRANSACTION_AMOUNT1 { get; set; }
        public int ITRANSACTION_QTY1 { get; set; }
        public string CUNIT { get; set; } = string.Empty;
        public string CTRANSACTION_DESCR { get; set; } = string.Empty;
        public string CASSET_DEPT_CODE { get; set; } = string.Empty;
        public string CASSET_DEPT_NAME { get; set; } = string.Empty;
        public string CASSET_LOCATION { get; set; } = string.Empty;
        public string CJRNGRP_CODE { get; set; } = string.Empty;
        public string CJRNGRP_NAME { get; set; } = string.Empty;
        public string CTAX_CATEGORY_CODE { get; set; } = string.Empty;
        public string CTAX_CATEGORY_DESC { get; set; } = string.Empty;
        public string CASSET_NAME { get; set; } = string.Empty;
    }
}

