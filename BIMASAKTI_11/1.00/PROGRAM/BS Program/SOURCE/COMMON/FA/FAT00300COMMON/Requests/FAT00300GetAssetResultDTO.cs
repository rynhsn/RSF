using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace FAT00300Common.Requests
{
    public class FAT00300GetAssetResultDTO : R_APIResultBaseDTO
    {
        public string CCOMPANY_ID { get; set; } = "";
        public string CASSET_CODE { get; set; } = "";
        public string CASSET_NAME { get; set; } = "";
        public string CSERIAL_NUMBER { get; set; } = "";
        public string CTRANS_DESCRIPTION { get; set; } = "";
        public string CINSERVICE_DATE { get; set; } = "";
        public string CASSET_DEPT_CODE { get; set; } = "";
        public string CASSET_OWNER { get; set; } = "";
        public string CASSET_LOCATION { get; set; } = "";
        public string CJRNGRP_CODE { get; set; } = "";
        public string CTAX_CATEGORY_CODE { get; set; } = "";
        public string CCATEGORY_CODE { get; set; } = "";
        public string CSCATEGORY_CODE { get; set; } = "";
        public string CDEPR_METHOD { get; set; } = "";
        public string CSTART_DATE { get; set; } = "";
        public decimal NLBOOK_VALUE { get; set; }
        public decimal NBBOOK_VALUE { get; set; }
        public int IUSEFUL_LIVE { get; set; }
        public decimal NYEAR_DEPR_PCT { get; set; }
        public decimal NLYEAR_DEPR_AMT { get; set; }
        public decimal NBYEAR_DEPR_AMT { get; set; }
        public decimal NLRESIDUAL_VALUE { get; set; }
        public decimal NBRESIDUAL_VALUE { get; set; }
        public decimal NLYTD_DEPR_AMT { get; set; }
        public decimal NBYTD_DEPR_AMT { get; set; }
        public bool LNEW_FLAG { get; set; }
        public string CUNIT { get; set; } = "";
        public string CPURCHASE_DATE { get; set; } = "";
        public string CSUPPLIER_ID { get; set; } = "";
        public string CSUPPLIER_NAME { get; set; } = "";
        public decimal NLBEG_BOOK_VALUE { get; set; }
        public decimal NBBEG_BOOK_VALUE { get; set; }
        public int IBEG_USEFUL_LIVE { get; set; }
        public decimal NLBEGINNING_AMT { get; set; }
        public decimal NBBEGINNING_AMT { get; set; }
        public decimal NLADDITION_AMT { get; set; }
        public decimal NBADDITION_AMT { get; set; }
        public decimal NLDEDUCTION_AMT { get; set; }
        public decimal NBDEDUCTION_AMT { get; set; }
        public decimal NLREVENUE_AMT { get; set; }
        public decimal NBREVENUE_AMT { get; set; }
        public decimal NLSOLD_AMT { get; set; }
        public decimal NBSOLD_AMT { get; set; }
        public int IBEGINNING_QTY { get; set; }
        public int IADDITION_QTY { get; set; }
        public int IDEDUCTION_QTY { get; set; }
        public decimal NLBASE_RATE { get; set; }
        public decimal NLCURRENCY_RATE { get; set; }
        public decimal NBBASE_RATE { get; set; }
        public decimal NBCURRENCY_RATE { get; set; }
        public decimal NLPRIOR_DEPR_AMT { get; set; }
        public decimal NBPRIOR_DEPR_AMT { get; set; }
        public decimal NLREVALUATION_AMT { get; set; }
        public decimal NBREVALUATION_AMT { get; set; }
        public decimal NLPRIOR_REVALUATION_AMT { get; set; }
        public decimal NBPRIOR_REVALUATION_AMT { get; set; }
        public decimal NLYTD_REVALUATION_AMT { get; set; }
        public decimal NBYTD_REVALUATION_AMT { get; set; }
        public string CLSEQUENCE_NO { get; set; } = "";
        public string CLAST_TRANS_DATE { get; set; } = "";
        public string CLAST_DEPR_PERIOD { get; set; } = "";
        public string CASSET_STATUS { get; set; } = "";
        public string OASSET_IMAGE { get; set; } = "";
        public string CFR_DEPT_CODE { get; set; } = "";
        public string CFR_TRANSACTION_CODE { get; set; } = "";
        public string CFR_REFERENCE_NO { get; set; } = "";
        public string CFR_TRANSACTION_DATE { get; set; } = "";
        public string CFR_SEQUENCE_NO { get; set; } = "";
        public decimal NLAST_BBASE_RATE_AMOUNT { get; set; }
        public decimal NLAST_BCURRENCY_RATE_AMOUNT { get; set; }
        public string CLAST_CURR_RATE_DATE { get; set; } = "";
        public decimal NBRATE_REVALUATION_AMT { get; set; }
        public string CCREATE_BY { get; set; } = "";
        public DateTime? DCREATE_DATE { get; set; }
        public string CUPDATE_BY { get; set; } = "";
        public DateTime? DUPDATE_DATE { get; set; }
    }
}
