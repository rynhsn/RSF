namespace FAT00100Common.DTOs
{
    /// <summary>
    /// Result DTO for GetFAAcquisitionDetailHeader method
    /// </summary>
    public class FAT0010002GetFAAcquisitionDetailHeaderResultDTO
    {
        public string CDEPT_CODE { get; set; } = string.Empty;
        public string CTRANSACTION_CODE { get; set; } = string.Empty;
        public string CREFERENCE_NO { get; set; } = string.Empty;
        public string CTRANSACTION_DATE { get; set; } = string.Empty;
        public string CSTATUS { get; set; } = string.Empty;
        public string CCURRENCY_CODE { get; set; } = string.Empty;
        public decimal NLBASE_RATE_AMOUNT { get; set; }
        public decimal NLCURRENCY_RATE_AMOUNT { get; set; }
        public decimal NBBASE_RATE_AMOUNT { get; set; }
        public decimal NBCURRENCY_RATE_AMOUNT { get; set; }
        public decimal NTRANSACTION_AMOUNT { get; set; }
        public decimal NLTRANSACTION_AMOUNT { get; set; }
        public decimal NBTRANSACTION_AMOUNT { get; set; }
        public string CDOCUMENT_DATE { get; set; } = string.Empty;
        public string CSUPPLIER_ID { get; set; } = string.Empty;
        public string CSUPPLIER_NAME { get; set; } = string.Empty;
        public string CFR_MODULE { get; set; } = string.Empty;
        public string CFR_DEPT_CODE { get; set; } = string.Empty;
        public string CFR_TRANSACTION_CODE { get; set; } = string.Empty;
        public string CFR_REFERENCE_NO { get; set; } = string.Empty;
        public string CDEPT_NAME { get; set; } = string.Empty;
        public string CCURRENCY_NAME { get; set; } = string.Empty;
        public string CTRANSACTION_NAME { get; set; } = string.Empty;
        public decimal NLRATE { get; set; }
        public decimal NBRATE { get; set; }
        public decimal NBXRATE { get; set; }
    }
}

