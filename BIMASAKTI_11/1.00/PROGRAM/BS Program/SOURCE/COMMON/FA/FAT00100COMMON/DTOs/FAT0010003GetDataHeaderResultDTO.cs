namespace FAT00100Common.DTOs
{
    /// <summary>
    /// Result DTO for GetDataHeader method
    /// </summary>
    public class FAT0010003GetDataHeaderResultDTO
    {
        public string CDEPT_CODE { get; set; } = string.Empty;
        public string CTRANSACTION_CODE { get; set; } = string.Empty;
        public string CREFERENCE_NO { get; set; } = string.Empty;
        public string CTRANSACTION_DATE { get; set; } = string.Empty;
        public string CCURRENCY_CODE { get; set; } = string.Empty;
        public decimal NLBASE_RATE_AMOUNT { get; set; }
        public decimal NLCURRENCY_RATE_AMOUNT { get; set; }
        public decimal NBBASE_RATE_AMOUNT { get; set; }
        public decimal NBCURRENCY_RATE_AMOUNT { get; set; }
        public string CTRANSACTION_DESCR { get; set; } = string.Empty;
        public string CSUPPLIER_ID { get; set; } = string.Empty;
        public string CINFO_SEQNO { get; set; } = string.Empty;
        public string CDEPT_NAME { get; set; } = string.Empty;
        public string CCURRENCY_NAME { get; set; } = string.Empty;
        public string CTRANSACTION_NAME { get; set; } = string.Empty;
        public string CSUPPLIER_NAME { get; set; } = string.Empty;
    }
}

