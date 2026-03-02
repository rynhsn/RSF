namespace FAT00700Common.DTOs
{
    public class GetAssetInformationResultDTO
    {
        public decimal NBBASE_RATE { get; set; }
        public decimal NBCURRENCY_RATE { get; set; }
        public decimal NLFA { get; set; }
        public decimal NLAD { get; set; }
        public decimal NLRFA { get; set; }
        public decimal NLRAD { get; set; }
        public decimal NBFA { get; set; }
        public decimal NBAD { get; set; }
        public decimal NBRFA { get; set; }
        public decimal NBRAD { get; set; }
        public string CNSEQUENCE_NO { get; set; } = string.Empty;
        public string CASSET_DEPT_CODE { get; set; } = string.Empty;
        public string CJRNGRP_CODE { get; set; } = string.Empty;
        public string CTAX_CATEGORY_CODE { get; set; } = string.Empty;
        public string CDEPR_METHOD { get; set; } = string.Empty;
    }
}

