namespace Lookup_FACommon.DTOs
{
    public class FAL00300DTO
    {
        public string CASSET_CODE { get; set; }
        public string CASSET_NAME { get; set; }
        public string CASSET_STATUS { get; set; }
        public string CASSET_STATUS_NAME { get; set; }
        public string CCURRENCY_CODE { get; set; }
        public decimal NBEG_BOOK_VALUE { get; set; }
        public decimal NLBASE_RATE { get; set; }
        public decimal NLCURRENCY_RATE { get; set; }
        public decimal NBBASE_RATE { get; set; }
        public decimal NBCURRENCY_RATE { get; set; }
        public string CASSET_TRANS_SEQ_NO { get; set; }
        public string CLOCATION_ID { get; set; }
        public string CLOCATION_NAME { get; set; }
        public string CLOCATION_INFO { get; set; }
        public string CSERIAL_NO { get; set; }
        public string CREC_ID { get; set; }
        public string CASSET_DEPT_CODE { get; set; }
        public string CASSET_DEPT_NAME { get; set; }
        public string CASSET_OWNER { get; set; }
        public string CJRNGRP_CODE { get; set; }
        public string CJRNGRP_NAME { get; set; }
        public string CTAX_CATEGORY_ID { get; set; }
        public string CTAX_CATEGORY_NAME { get; set; }
        public string CCATEGORY_ID { get; set; }
        public string CCATEGORY_NAME { get; set; }
        public string CDEPR_METHOD { get; set; }
        public string CDEPR_METHOD_NAME { get; set; }
        public string CSTART_DATE { get; set; }
        public int IUSEFUL_LIFE { get; set; }
        public string CUNIT { get; set; }
        public string CSUPPLIER_ID { get; set; }
        public string CSUPPLIER_NAME { get; set; }
        public int IUSEFUL_LIFE_YY { get; set; }
        public int IUSEFUL_LIFE_MM { get; set; }

        public int IBEGINNING_QTY { get; set; }
        public int IADDITION_QTY { get; set; }
        public int IDEDUCTION_QTY { get; set; }
        public int IBALANCE_QTY { get; set; }

        // Dates
        public string CINSERVICE_DATE { get; set; }

        // Book Values
        public decimal NBOOK_VALUE { get; set; }
        public decimal NLBOOK_VALUE { get; set; }
        public decimal NBBOOK_VALUE { get; set; }

        // Depreciation Percentage
        public decimal NYEAR_DEPR_PCT { get; set; }

        // Yearly Depreciation
        public decimal NYEAR_DEPR { get; set; }
        public decimal NLYEAR_DEPR { get; set; }
        public decimal NBYEAR_DEPR { get; set; }

        // Residual Values
        public decimal NRESIDUAL_VALUE { get; set; }
        public decimal NLRESIDUAL_VALUE { get; set; }
        public decimal NBRESIDUAL_VALUE { get; set; }

        // YTD Depreciation
        public decimal NYTD_DEPR { get; set; }
        public decimal NLYTD_DEPR { get; set; }
        public decimal NBYTD_DEPR { get; set; }

        // Beginning Book Values
        public decimal NLBEG_BOOK_VALUE { get; set; }
        public decimal NBBEG_BOOK_VALUE { get; set; }

        // Beginning Values
        public decimal NBEGINNING { get; set; }
        public decimal NLBEGINNING { get; set; }
        public decimal NBBEGINNING { get; set; }

        // Additions
        public decimal NADDITION { get; set; }
        public decimal NLADDITION { get; set; }
        public decimal NBADDITION { get; set; }

        // Deductions
        public decimal NDEDUCTION { get; set; }
        public decimal NLDEDUCTION { get; set; }
        public decimal NBDEDUCTION { get; set; }

        // Revenue
        public decimal NREVENUE { get; set; }
        public decimal NLREVENUE { get; set; }
        public decimal NBREVENUE { get; set; }

        // Sold Values
        public decimal NSOLD { get; set; }
        public decimal NLSOLD { get; set; }
        public decimal NBSOLD { get; set; }

        // Prior Depreciation
        public decimal NPRIOR_DEPR { get; set; }
        public decimal NLPRIOR_DEPR { get; set; }
        public decimal NBPRIOR_DEPR { get; set; }

        // Revaluation
        public decimal NREVALUATION { get; set; }
        public decimal NLREVALUATION { get; set; }
        public decimal NBREVALUATION { get; set; }

        // Prior Revaluation
        public decimal NPRIOR_REVALUATION { get; set; }
        public decimal NLPRIOR_REVALUATION { get; set; }
        public decimal NBPRIOR_REVALUATION { get; set; }

        // YTD Revaluation
        public decimal NYTD_REVALUATION { get; set; }
        public decimal NLYTD_REVALUATION { get; set; }
        public decimal NBYTD_REVALUATION { get; set; }
    }
}
