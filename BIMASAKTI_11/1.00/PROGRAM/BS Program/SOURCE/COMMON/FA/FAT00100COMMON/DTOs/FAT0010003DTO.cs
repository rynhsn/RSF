namespace FAT00100Common.DTOs
{
    /// <summary>
    /// Main entity DTO for FAT0010003 - Fixed Asset Transaction Detail operations
    /// </summary>
    public class FAT0010003DTO
    {
        // Standard properties (ALWAYS at top)
        public string CCOMPANY_ID { get; set; } = string.Empty;
        public string CLANG_ID { get; set; } = string.Empty;
        public string CUSER_ID { get; set; } = string.Empty;

        // Business properties
        public string CFOREIGN_LANGUAGE { get; set; } = string.Empty;
        public string PCFR_DEPT_CODE { get; set; } = string.Empty;
        public string PCFR_TRANSACTION_CODE { get; set; } = string.Empty;
        public string PCFR_REFERENCE_NO { get; set; } = string.Empty;
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
        public string CLOCAL_CURRENCY_CODE { get; set; } = string.Empty;
        public string CBASE_CURRENCY_CODE { get; set; } = string.Empty;
        public string CFR_DEPT_CODE { get; set; } = string.Empty;
        public string CFR_TRANSACTION_CODE { get; set; } = string.Empty;
        public string CFR_REFERENCE_NO { get; set; } = string.Empty;
    }
}

