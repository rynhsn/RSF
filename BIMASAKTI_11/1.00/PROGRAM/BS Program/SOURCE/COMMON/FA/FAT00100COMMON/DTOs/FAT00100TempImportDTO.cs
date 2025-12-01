using System;

namespace FAT00100Common.DTOs
{
    /// <summary>
    /// Temporary import DTO for FAT00100 - Asset Import Batch
    /// Used for bulk import operations
    /// </summary>
    public class FAT00100TempImportDTO
    {
        public string RECID { get; set; } = string.Empty;
        public string CASSET_CODE { get; set; } = string.Empty;
        public string CASSET_NAME { get; set; } = string.Empty;
        public string CCATEGORY_CODE { get; set; } = string.Empty;
        public string CASSET_DEPT_CODE { get; set; } = string.Empty;
        public string CJRNGRP_CODE { get; set; } = string.Empty;
        public string CTAX_CATEGORY_CODE { get; set; } = string.Empty;
        public string CASSET_OWNER { get; set; } = string.Empty;
        public string CASSET_LOCATION { get; set; } = string.Empty;
        public int IBEGINNING_QTY { get; set; }
        public string CUNIT { get; set; } = string.Empty;
        public string CTRANS_DESCRIPTION { get; set; } = string.Empty;
        public string CSERIAL_NUMBER { get; set; } = string.Empty;
        public string CINSERVICE_DATE { get; set; } = string.Empty;
        public decimal NBEGINNING_AMT { get; set; }
        public decimal NLADDITION_AMT { get; set; }
        public decimal NLDEDUCTION_AMT { get; set; }
        public decimal NLPRIOR_DEPR_AMT { get; set; }
        public decimal NLYTD_DEPR_AMT { get; set; }
        public string CDEPR_METHOD { get; set; } = string.Empty;
        public string CSTART_DATE { get; set; } = string.Empty;
        public decimal NLBEG_BOOK_VALUE { get; set; }
        public decimal NLRESIDUAL_VALUE { get; set; }
        public int IUSEFUL_LIVE_YR { get; set; }
        public int IUSEFUL_LIVE_MO { get; set; }
        public int IREM_USEFUL_LIVE_YR { get; set; }
        public int IREM_USEFUL_LIVE_MO { get; set; }
    }
}

