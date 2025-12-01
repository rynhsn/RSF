namespace FAT00100Common.DTOs
{
    /// <summary>
    /// Result DTO for GetDataGrid streaming method
    /// </summary>
    public class FAT0010003GetDataGridResultDTO
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
        public string CSEQUENCE_NO { get; set; } = string.Empty;
        public string CPROD_DEPT_CODE { get; set; } = string.Empty;
        public int IPRODTYP { get; set; }
        public string CPRODUCT_ID { get; set; } = string.Empty;
        public string CSUPP_PRODUCT_NAME { get; set; } = string.Empty;
        public string CALLOC_EXPENSE_CODE { get; set; } = string.Empty;
        public string CWAREHOUSE_ID { get; set; } = string.Empty;
        public string CBILL_UNIT { get; set; } = string.Empty;
        public decimal NBILL_UNIT_QTY { get; set; }
        public string CPRODTYP_DESC { get; set; } = string.Empty;
        public string CDETAIL_DESCR { get; set; } = string.Empty;
        public decimal NTRANS_AMOUNT { get; set; }
        public decimal NLTRANS_AMOUNT { get; set; }
        public decimal NBTRANS_AMOUNT { get; set; }
    }
}

