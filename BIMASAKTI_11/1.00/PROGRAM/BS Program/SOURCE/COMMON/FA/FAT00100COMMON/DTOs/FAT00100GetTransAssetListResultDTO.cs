namespace FAT00100Common.DTOs
{
    /// <summary>
    /// Result DTO for FAT00100GetTransAssetList method
    /// </summary>
    public class FAT00100GetTransAssetListResultDTO
    {
        public string CREC_ID { get; set; } = string.Empty;
        public string CCOMPANY_ID { get; set; } = string.Empty;
        public string CDEPT_CODE { get; set; } = string.Empty;
        public string CREF_NO { get; set; } = string.Empty;
        public string CTRANS_SEQ_NO { get; set; } = string.Empty;
        public string CASSET_CODE { get; set; } = string.Empty;
        public string CASSET_NAME { get; set; } = string.Empty;
        public string CASSET_DEPT_CODE { get; set; } = string.Empty;
        public string CASSET_DEPT_NAME { get; set; } = string.Empty;
        public string CASSET_LOCATION { get; set; } = string.Empty;
        public string CLOCATION_ID{ get; set; } = string.Empty;
        public string CLOCATION_NAME { get; set; } = string.Empty;
        public string CSERIAL_NUMBER { get; set; } = string.Empty;
        public string CLOCAL_CURRENCY_CODE { get; set; } = string.Empty;
        public string CBASE_CURRENCY_CODE { get; set; } = string.Empty;
        public decimal NINIT_COST { get; set; }
        public int IQTY { get; set; }
        public string CUNIT { get; set; } = string.Empty;
        public decimal NLINIT_COST { get; set; }
        public decimal NBINIT_COST { get; set; }
        public string CCURRENCY_CODE { get; set; } = string.Empty;

        // Additional fields can be added here as needed
        public string CCATEGORY_CODE { get; set; } = string.Empty;
        public string CCATEGORY_NAME { get; set; } = string.Empty;
        public decimal NLOCAL_AMOUNT { get; set; }
        public decimal NBASE_AMOUNT { get; set; }

    }
}

