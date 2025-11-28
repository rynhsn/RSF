namespace FAT00800Common.DTOs
{
    /// <summary>
    /// Result DTO for GetAssetInfo method
    /// </summary>
    public class FAT00800GetAssetInfoResultDTO
    {
        public string CASSET_CODE { get; set; } = string.Empty;
        public string CASSET_NAME { get; set; } = string.Empty;
        public string CSERIAL_NUMBER { get; set; } = string.Empty;
        public string CASSET_DEPT_CODE { get; set; } = string.Empty;
        public string CASSET_LOCATION { get; set; } = string.Empty;
        public string CCATEGORY_CODE { get; set; } = string.Empty;
        public string CDEPR_METHOD { get; set; } = string.Empty;
        public string CSTART_DATE { get; set; } = string.Empty;
        public decimal NLBOOK_VALUE { get; set; }
        public decimal NBBOOK_VALUE { get; set; }
        public decimal NYEAR_DEPR_PCT { get; set; }
        public decimal NLYEAR_DEPR_AMT { get; set; }
        public decimal NBYEAR_DEPR_AMT { get; set; }
        public decimal NLRESIDUAL_VALUE { get; set; }
        public decimal NBRESIDUAL_VALUE { get; set; }
        public int IQTY { get; set; }
        public string CUNIT { get; set; } = string.Empty;
        public string CLAST_TRANS_DATE { get; set; } = string.Empty;
        public int IUSEFUL_LIVE_YR { get; set; }
        public int IUSEFUL_LIVE_MO { get; set; }
        public string CASSET_DEPT_NAME { get; set; } = string.Empty;
        public string CCATEGORY_DESC { get; set; } = string.Empty;
        public string CDEPR_METHOD_DESC { get; set; } = string.Empty;

    /// <summary>
    /// Currency code for local currency (e.g., IDR)
    /// </summary>
    public string CLOCAL_CURRENCY_CODE { get; set; } = string.Empty;

    /// <summary>
    /// Currency code for base currency (e.g., USD)
    /// </summary>
    public string CBASE_CURRENCY_CODE { get; set; } = string.Empty;
    }
}

