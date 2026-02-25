using System;
using System.Collections.Generic;
using System.Text;

namespace FAT01100Common.DTOs
{
    public class FAT01100GetAssetResultDTO
    {
    public string CCOMPANY_ID { get; set; }
    public string CASSET_CODE { get; set; }
    public string CASSET_NAME { get; set; }
    public string CSERIAL_NUMBER { get; set; }
    public string CTRANS_DESCRIPTION { get; set; }
    public string CINSERVICE_DATE { get; set; }
    public string CASSET_DEPT_CODE { get; set; }
    public string CASSET_DEPT_NAME { get; set; }
    public string CASSET_OWNER { get; set; }
    public string CLOCATION_ID { get; set; }
    public string CLOCATION_NAME { get; set; }
    public string CPROPERTY_ID { get; set; }
    public string CPROPERTY_NAME { get; set; }
    public string CBUILDING_ID { get; set; }
    public string CBUILDING_NAME { get; set; }
    public string CFLOOR_ID { get; set; }
    public string CFLOOR_NAME { get; set; }
    public string CJRNGRP_CODE { get; set; }
    public string CJRNGRP_NAME { get; set; }
    public string CTAX_CATEGORY_ID { get; set; }
    public string CTAX_CATEGORY_NAME { get; set; }
    public string CCATEGORY_ID { get; set; }
    public string CCATEGORY_NAME { get; set; }
    public string CDEPR_METHOD { get; set; }
    public string CDEPR_METHOD_NAME { get; set; }
    public string CSTART_DATE { get; set; }

    public decimal NBOOK_VALUE { get; set; }
    public decimal NLBOOK_VALUE { get; set; }
    public decimal NBBOOK_VALUE { get; set; }
    public int IUSEFUL_LIFE { get; set; }
    public decimal NYEAR_DEPR_PCT { get; set; }
    public decimal NLYEAR_DEPR { get; set; }
    public decimal NBYEAR_DEPR { get; set; }
    public decimal NRESIDUAL_VALUE { get; set; }
    public decimal NLRESIDUAL_VALUE { get; set; }
    public decimal NBRESIDUAL_VALUE { get; set; }
    public decimal NLYTD_DEPR { get; set; }
    public decimal NBYTD_DEPR { get; set; }

    public bool LNEW_FLAG { get; set; }
    public string CUNIT { get; set; }
    public string CPURCHASE_DATE { get; set; }
    public string CSUPPLIER_ID { get; set; }
    public string CSUPPLIER_NAME { get; set; }

    public decimal NLBEG_BOOK_VALUE { get; set; }
    public decimal NBBEG_BOOK_VALUE { get; set; }
    public int IBEG_USEFUL_LIFE { get; set; }

    public decimal NLBEGINNING { get; set; }
    public decimal NBBEGINNING { get; set; }
    public decimal NLADDITION { get; set; }
    public decimal NBADDITION { get; set; }
    public decimal NLDEDUCTION { get; set; }
    public decimal NBDEDUCTION { get; set; }
    public decimal NLREVENUE { get; set; }
    public decimal NBREVENUE { get; set; }
    public decimal NLSOLD { get; set; }
    public decimal NBSOLD { get; set; }

    public int IBEGINNING_QTY { get; set; }
    public int IADDITION_QTY { get; set; }
    public int IDEDUCTION_QTY { get; set; }

    public decimal NPRIOR_DEPR { get; set; }
    public decimal NLPRIOR_DEPR { get; set; }
    public decimal NBPRIOR_DEPR { get; set; }

    public decimal NREVALUATION { get; set; }
    public decimal NLREVALUATION { get; set; }
    public decimal NBREVALUATION { get; set; }

    public decimal NPRIOR_REVALUATION { get; set; }
    public decimal NLPRIOR_REVALUATION { get; set; }
    public decimal NBPRIOR_REVALUATION { get; set; }

    public decimal NYTD_REVALUATION { get; set; }
    public decimal NLYTD_REVALUATION { get; set; }
    public decimal NBYTD_REVALUATION { get; set; }

    public string CLAST_SEQ_NO { get; set; }
    public string CLAST_TRANS_DATE { get; set; }
    public string CLAST_DEPR_PERIOD { get; set; }
    public string CASSET_STATUS { get; set; }
    public string CSTORAGE_ID { get; set; }

    public string CFR_DEPT_CODE { get; set; }
    public string CFR_TRANS_CODE { get; set; }
    public string CFR_REF_NO { get; set; }
    public string CFR_REC_ID { get; set; }
    public string CFR_SEQ_NO { get; set; }

    public decimal NLAST_BBASE_RATE { get; set; }
    public decimal NLAST_BCURRENCY_RATE { get; set; }
    public string CLAST_CURR_RATE_DATE { get; set; }
    public decimal NBRATE_REVALUATION { get; set; }

    public string CCREATE_BY { get; set; }
    public DateTime DCREATE_DATE { get; set; }
    public string CUPDATE_BY { get; set; }
    public DateTime DUPDATE_DATE { get; set; }

    public int IUSEFUL_LIFE_YY { get; set; }
    public int IUSEFUL_LIFE_MM { get; set; }

    public decimal NLBASE_RATE { get; set; }
    public decimal NLCURRENCY_RATE { get; set; }
    public decimal NBBASE_RATE { get; set; }
    public decimal NBCURRENCY_RATE { get; set; }

    public string CSERIAL_NO { get; set; }
    public int IASSET_QTY { get; set; }

    public string CLOCAL_CURRENCY_CODE { get; set; }
    public string CBASE_CURRENCY_CODE { get; set; }

    public decimal NYEAR_DEPR { get; set; }
    public decimal NLYEAR_DEPR2 { get; set; }   // renamed to avoid conflict
    public decimal NBYEAR_DEPR2 { get; set; }   // renamed to avoid conflict

    public string CCURRENCY_CODE { get; set; }
    public string CCURRENCY_NAME { get; set; }
    public string CNEXT_START_DATE { get; set; }

        // ADDTIONAL
    public byte[] OASSET_IMAGE { get; set; } = Array.Empty<byte>();
    public string CFILE_NAME { get; set; } = string.Empty;
    public string CFILE_TYPE { get; set; } = string.Empty;
    public string CFILE_EXTENSION { get; set; } = string.Empty;

    }
}
