using System;

namespace FAT00800Common.DTOs
{
    /// <summary>
    /// Main entity DTO for FAT00800 - Fixed Asset Transaction
    /// </summary>
    public class FAT00800DTO
    {
        // Standard properties (ALWAYS at top)
        public string CCOMPANY_ID { get; set; } = string.Empty;
        public string CLANG_ID { get; set; } = string.Empty;
        public string CUSER_ID { get; set; } = string.Empty;

        // INITIAL PROCESS
        public string CDEFAULT_TRX_DEPT_CODE { get; set; } = string.Empty;
        public string CSOFT_PERIOD { get; set; } = string.Empty;
        public string CGLLINK_DATE { get; set; } = string.Empty;
        public string CRATETYPE_CODE { get; set; } = string.Empty;
        
        // Get Local and Base Currency
        public string CLOCAL_CURRENCY_CODE { get; set; } = string.Empty;
        public string CBASE_CURRENCY_CODE { get; set; } = string.Empty;
        public bool LCUST_PERIOD_FLAG { get; set; }
        
        // get FA Transaction Type description
        public string CTRANS_DESC { get; set; } = string.Empty;
        public bool LTRANS_APPROVAL { get; set; }
        public bool LINCREMENT_FLAG { get; set; }

        public string CACTIVITY_CODE { get; set; } = string.Empty;
        public string CASSET_CODE { get; set; } = string.Empty;
        public string CASSET_TRANS_SEQNO { get; set; } = string.Empty;

        // Save
        public decimal NLFA { get; set; }
        public decimal NLAD { get; set; }
        public decimal NLRFA { get; set; }
        public decimal NLRAD { get; set; }
        public decimal NBFA { get; set; }
        public decimal NBAD { get; set; }
        public decimal NBRFA { get; set; }
        public decimal NBRAD { get; set; }
        public string CNSEQUENCE_NO { get; set; } = string.Empty;

        public string CPRD { get; set; } = string.Empty;
        public string CLAST_TRANS_DATE { get; set; } = string.Empty;
        public string CASSET_STATUS { get; set; } = string.Empty;

        public string CSUPPLIER_ID { get; set; } = string.Empty;
        public string CINFO_SEQNO { get; set; } = string.Empty;
        public string CSUPPLIER_NAME { get; set; } = string.Empty;
        public string CTRANSACTION_PRD { get; set; } = string.Empty;
        public string CDOCUMENT_DATE { get; set; } = string.Empty;
        public string CDOCUMENT_NO { get; set; } = string.Empty;
        public string CCURRENCY_CODE { get; set; } = string.Empty;
        public string CFR_MODULE { get; set; } = string.Empty;
        public string CFR_DEPT_CODE { get; set; } = string.Empty;
        public string CFR_REFERENCE_NO { get; set; } = string.Empty;
        public decimal NLBASE_RATE_AMOUNT { get; set; }
        public decimal NLCURRENCY_RATE_AMOUNT { get; set; }
        public decimal NBBASE_RATE_AMOUNT { get; set; }
        public decimal NBCURRENCY_RATE_AMOUNT { get; set; }
        public decimal NTRANSACTION_AMOUNT { get; set; }
        public decimal NLTRANSACTION_AMOUNT { get; set; }
        public decimal NBTRANSACTION_AMOUNT { get; set; }
        public string CSTATUS { get; set; } = string.Empty;
        public bool LGLLINK { get; set; }
        public string CGL_TRF_STATUS { get; set; } = string.Empty;
        public string CGL_REFERENCE_NO { get; set; } = string.Empty;
        public string CAPPROVED_BY { get; set; } = string.Empty;
        public DateTime DAPPROVED_DATE { get; set; }
        public string CCOMMIT_BY { get; set; } = string.Empty;
        public DateTime DCOMMIT_DATE { get; set; }
        public string CCANCEL_BY { get; set; } = string.Empty;
        public DateTime DCANCEL_DATE { get; set; }
        public string CDEPT_CODE { get; set; } = string.Empty;
        public string CTRANSACTION_CODE { get; set; } = string.Empty;
        public string CREFERENCE_NO { get; set; } = string.Empty;
        public string CTRANSACTION_DATE { get; set; } = string.Empty;
        public DateTime DTRANSACTION_DATE { get; set; }
        public string CTRANSACTION_DESCR { get; set; } = string.Empty;
        public string CCREATE_BY { get; set; } = string.Empty;
        public DateTime DCREATE_DATE { get; set; }
        public string CUPDATE_BY { get; set; } = string.Empty;
        public DateTime DUPDATE_DATE { get; set; }

        public string CTRANS_SEQNO { get; set; } = string.Empty;
        public decimal NTRANSACTION_AMOUNT1 { get; set; }
        public decimal NTRANSACTION_AMOUNT2 { get; set; }
        public decimal NTRANSACTION_AMOUNT3 { get; set; }
        public decimal NTRANSACTION_AMOUNT4 { get; set; }
        public decimal NTRANSACTION_AMOUNT5 { get; set; }
        public decimal NLTRANSACTION_AMOUNT1 { get; set; }
        public decimal NLTRANSACTION_AMOUNT2 { get; set; }
        public decimal NLTRANSACTION_AMOUNT3 { get; set; }
        public decimal NLTRANSACTION_AMOUNT4 { get; set; }
        public decimal NLTRANSACTION_AMOUNT5 { get; set; }
        public decimal NBTRANSACTION_AMOUNT1 { get; set; }
        public decimal NBTRANSACTION_AMOUNT2 { get; set; }
        public decimal NBTRANSACTION_AMOUNT3 { get; set; }
        public decimal NBTRANSACTION_AMOUNT4 { get; set; }
        public decimal NBTRANSACTION_AMOUNT5 { get; set; }
        public string CALLOC_EXPENSE_CODE { get; set; } = string.Empty;
        public string CASSET_DEPT_CODE { get; set; } = string.Empty;
        public string CJRNGRP_CODE { get; set; } = string.Empty;
        public string CTAX_CATEGORY_CODE { get; set; } = string.Empty;
        public string CDEPR_METHOD { get; set; } = string.Empty;
        public string COASSET_DEPT_CODE { get; set; } = string.Empty;
        public string COASSET_LOCATION { get; set; } = string.Empty;
        public bool LDELETE_FLAG { get; set; }
        public bool LCHANGE_DESC { get; set; }
        public bool LCHANGE_ALLOC { get; set; }

        public string CCANCEL_APPROVED_BY { get; set; } = string.Empty;
        public string CCANCEL_REASON_CODE { get; set; } = string.Empty;
        public string CMODE { get; set; } = string.Empty;
        public string CSTATUS_DESC { get; set; } = string.Empty;

        public decimal NLBOOKVAL { get; set; }
        public decimal NBBOOKVAL { get; set; }
        
        // Gain/Loss amounts for asset disposal
        public decimal NLGAIN_LOSS { get; set; }
        public decimal NBGAIN_LOSS { get; set; }
        
        // Page2
        public string CASSET_NAME { get; set; } = string.Empty;
        public string CSERIAL_NUMBER { get; set; } = string.Empty;
        public string CASSET_LOCATION { get; set; } = string.Empty;
        public string CCATEGORY_CODE { get; set; } = string.Empty;
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
        public int IUSEFUL_LIVE_YR { get; set; }
        public int IUSEFUL_LIVE_MO { get; set; }
        public string CASSET_DEPT_NAME { get; set; } = string.Empty;
        public string CCATEGORY_DESC { get; set; } = string.Empty;
        public string CDEPR_METHOD_DESC { get; set; } = string.Empty;
        public string CAPPROVAL_CODE { get; set; } = string.Empty;
        public byte IAPPROVAL_OPTION { get; set; }
        public string CLSEQUENCE_NO { get; set; } = string.Empty;
        
        // CR1
        public string CCURRENT_PERIOD { get; set; } = string.Empty;
    }
}

