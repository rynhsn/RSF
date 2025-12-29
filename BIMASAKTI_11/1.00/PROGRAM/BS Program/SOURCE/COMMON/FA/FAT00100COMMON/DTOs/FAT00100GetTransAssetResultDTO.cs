namespace FAT00100Common.DTOs
{
    /// <summary>
    /// Result DTO for FAT00100GetTransAsset method
    /// </summary>
    public class FAT00100GetTransAssetResultDTO
    {
        public string CREC_ID { get; set; } = string.Empty;
        public string CASSET_CODE { get; set; } = string.Empty;
        public string CASSET_NAME { get; set; } = string.Empty;
        public string CASSET_DEPT_CODE { get; set; } = string.Empty;
        public string CASSET_DEPT_NAME { get; set; } = string.Empty;
        public string CASSET_LOCATION { get; set; } = string.Empty;
        public string CSERIAL_NUMBER { get; set; } = string.Empty;
        public short IQTY { get; set; }
        public string CLOCAL_CURRENCY_CODE { get; set; } = string.Empty;
        public string CBASE_CURRENCY_CODE { get; set; } = string.Empty;
        public decimal NLOCAL_AMOUNT { get; set; }
        public decimal NBASE_AMOUNT { get; set; }
        public string CJRNGRP_CODE { get; set; } = string.Empty;
        public string CJRNGRP_NAME { get; set; } = string.Empty;
        public string CCATEGORY_CODE { get; set; } = string.Empty;
        public string CCATEGORY_NAME { get; set; } = string.Empty;
        public string CTAX_CATEGORY_CODE { get; set; } = string.Empty;
        public string CTAX_CATEGORY_NAME { get; set; } = string.Empty;
        public string CTRANS_DESC { get; set; } = string.Empty;
        public string CSERIAL_NO { get; set; } = string.Empty;
        public string CDEPR_METHOD { get; set; } = string.Empty;
        public string CSTART_DATE { get; set; } = string.Empty;
        public string CINSERVICE_DATE { get; set; } = string.Empty;
        public decimal NINIT_COST { get; set; }
        public decimal NLINIT_COST { get; set; }
        public decimal NBINIT_COST { get; set; }
        public decimal NADDITION { get; set; }
        public decimal NLADDITION { get; set; }
        public decimal NBADDITION { get; set; }
        public decimal NDEDUCTION { get; set; }
        public decimal NLDEDUCTION { get; set; }
        public decimal NBDEDUCTION { get; set; }
        public decimal NPRIOR_DEPR { get; set; }
        public decimal NLPRIOR_DEPR { get; set; }
        public decimal NBPRIOR_DEPR { get; set; }
        public decimal NYTD_DEPR { get; set; }
        public decimal NLYTD_DEPR { get; set; }
        public decimal NBYTD_DEPR { get; set; }
        public decimal NBOOK_VALUE { get; set; }
        public decimal NLBOOK_VALUE { get; set; }
        public decimal NBBOOK_VALUE { get; set; }
        public decimal NBEG_BOOK_VALUE { get; set; }
        public decimal NLBEG_BOOK_VALUE { get; set; }
        public decimal NBBEG_BOOK_VALUE { get; set; }
        public decimal NRESIDUAL_VALUE { get; set; }
        public decimal NLRESIDUAL_VALUE { get; set; }
        public decimal NBRESIDUAL_VALUE { get; set; }
        public int IUSEFUL_LIFE_YY { get; set; }
        public int IUSEFUL_LIFE_MM { get; set; }
        public int IREMAINING_YY { get; set; }
        public int IREMAINING_MM { get; set; }
        public decimal NYEAR_DEPR_PCT { get; set; }
        public decimal NYEAR_DEPR { get; set; }
        public decimal NLYEAR_DEPR { get; set; }
        public decimal NBYEAR_DEPR { get; set; }
    }
}

