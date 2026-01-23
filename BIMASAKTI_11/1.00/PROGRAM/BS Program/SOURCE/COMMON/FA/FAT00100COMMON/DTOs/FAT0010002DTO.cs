using System;

namespace FAT00100Common.DTOs
{
    /// <summary>
    /// Main entity DTO for FAT0010002 - Fixed Asset Acquisition Detail
    /// </summary>
    public class FAT0010002DTO
    {

        // Standard properties (ALWAYS at top)
        public string CCOMPANY_ID { get; set; } = string.Empty;
        public string CLANG_ID { get; set; } = string.Empty;
        public string CUSER_ID { get; set; } = string.Empty;


        //dto Crud
        public string CASSET_CODE { get; set; } = string.Empty;          // varchar(30)
        public string CTRANS_SEQ_NO { get; set; } = string.Empty;        // char(6)
        public string CASSET_NAME { get; set; } = string.Empty;          // nvarchar(200)
        public string CASSET_OWNER { get; set; } = string.Empty;          // varchar(50)
        public string CASSET_DEPT_CODE { get; set; } = string.Empty;     // varchar(50)
        public string CJRNGRP_CODE { get; set; } = string.Empty;         // varchar(20)
        public string CCATEGORY_CODE { get; set; } = string.Empty;       // varchar(20)
        public string CTAX_CATEGORY_CODE { get; set; } = string.Empty;   // varchar(20)
        public int IQTY { get; set; }                                    // int(4)
        public string CUNIT { get; set; } = string.Empty;                // nvarchar(40)
        public string CSERIAL_NO { get; set; } = string.Empty;           // varchar(30)
        public string CPROPERTY_ID { get; set; } = string.Empty;        // nvarchar(20)
        public string CLOCATION_ID { get; set; } = string.Empty;         // nvarchar(20)
                 
        public string CTRANS_DESC { get; set; } = string.Empty;          // nvarchar(200)
        public string CSTORAGE_ID { get; set; } = string.Empty;          // varchar(50)
        public string CINSERVICE_DATE { get; set; } = string.Empty;      // varchar(8)
        public bool LNEW { get; set; }                                   // bit(1)
        public decimal NINIT_COST { get; set; }                          // numeric(9)
        public decimal NADDITION { get; set; }                           // numeric(9)
        public decimal NDEDUCTION { get; set; }                          // numeric(9)
        public decimal NPRIOR_DEPR { get; set; }                         // numeric(9)
        public decimal NYTD_DEPR { get; set; }                           // numeric(9)
        public string CDEPR_METHOD { get; set; } = string.Empty;         // varchar(20)
        public string CSTART_DATE { get; set; } = string.Empty;          // varchar(8)
        public decimal NBOOK_VALUE { get; set; }                         // numeric(9)
        public decimal NBEG_BOOK_VALUE { get; set; }                     // numeric(9)
        public decimal NRESIDUAL_VALUE { get; set; }                     // numeric(9)
        public int IUSEFUL_LIFE_YY { get; set; }                         // int(4)
        public int IUSEFUL_LIFE_MM { get; set; }                         // int(4)
        public int IREMAINING_LIFE_YY { get; set; }                      // int(4)
        public int IREMAINING_LIFE_MM { get; set; }                      // int(4)
        public decimal NYEAR_DEPR_PCT { get; set; }                      // numeric(5)
        public decimal NYEAR_DEPR { get; set; }                          // numeric(9)
        public decimal NLBASE_RATE { get; set; }                         // numeric(13)
        public decimal NLCURRENCY_RATE { get; set; }                     // numeric(13)
        public decimal NBBASE_RATE { get; set; }                         // numeric(13)
        public decimal NBCURRENCY_RATE { get; set; }                     // numeric(13)


        //dto get Detail
        public string CREC_ID { get; set; } = string.Empty;
        public string CSERIAL_NUMBER { get; set; } = string.Empty;
        public string CLOCAL_CURRENCY_CODE { get; set; } = string.Empty;
        public string CBASE_CURRENCY_CODE { get; set; } = string.Empty;
        public decimal NLOCAL_AMOUNT { get; set; }
        public decimal NBASE_AMOUNT { get; set; }
        public string CJRNGRP_NAME { get; set; } = string.Empty;
        public string CCATEGORY_NAME { get; set; } = string.Empty;
        public string CTAX_CATEGORY_NAME { get; set; } = string.Empty;
        public decimal NLINIT_COST { get; set; }
        public decimal NBINIT_COST { get; set; }
        public decimal NLADDITION { get; set; }
        public decimal NBADDITION { get; set; }
        public decimal NLDEDUCTION { get; set; }
        public decimal NBDEDUCTION { get; set; }
        public decimal NLPRIOR_DEPR { get; set; }
        public decimal NBPRIOR_DEPR { get; set; }
        public decimal NLYTD_DEPR { get; set; }
        public decimal NBYTD_DEPR { get; set; }
        public decimal NLBOOK_VALUE { get; set; }
        public decimal NBBOOK_VALUE { get; set; }
        public decimal NLBEG_BOOK_VALUE { get; set; }
        public decimal NBBEG_BOOK_VALUE { get; set; }
        public decimal NLRESIDUAL_VALUE { get; set; }
        public decimal NBRESIDUAL_VALUE { get; set; }
        public int IREMAINING_YY { get; set; }
        public int IREMAINING_MM { get; set; }
        public decimal NLYEAR_DEPR { get; set; }
        public decimal NBYEAR_DEPR { get; set; }



        

        //additional standard properties
        public string CREF_NO { get; set; } = string.Empty;
        public string CLANGUAGE_ID { get; set; } = string.Empty;
        public DateTime? DSTART_DATE { get; set; }
        public string CREF_DATE { get; set; } = string.Empty;
        public string CSOFT_PERIOD { get; set; } = string.Empty;
        public string CLOCATION_NAME { get; set; } = string.Empty;
        public string CPROPERTY_NAME { get; set; } = string.Empty;
        public string CBUILDING_ID { get; set; } = string.Empty;
        public string CBUILDING_NAME { get; set; } = string.Empty;
        public string CFLOOR_ID { get; set; } = string.Empty;
        public string CFLOOR_NAME { get; set; } = string.Empty;
        public byte[] OIMAGE { get; set; }
        public string CFILE_NAME { get; set; } = "";
        public string CFILE_EXTENSION { get; set; } = "";
        public string CDEPT_CODE_DEFAULT { get; set; } = string.Empty;

        // Business properties
        public string CFOREIGN_LANGUAGE { get; set; } = string.Empty;
        public string CDEPT_CODE { get; set; } = string.Empty;
        public string CTRANSACTION_CODE { get; set; } = string.Empty;
        public string CREFERENCE_NO { get; set; } = string.Empty;
        public string CTRANSACTION_DATE { get; set; } = string.Empty;
        public DateTime? DTRANSACTION_DATE { get; set; }
        public string CSTATUS { get; set; } = string.Empty;
        public string CCURRENCY_CODE { get; set; } = string.Empty;
        public decimal NLBASE_RATE_AMOUNT { get; set; }
        public decimal NLCURRENCY_RATE_AMOUNT { get; set; }
        public decimal NBBASE_RATE_AMOUNT { get; set; }
        public decimal NBCURRENCY_RATE_AMOUNT { get; set; }
        public decimal NTRANSACTION_AMOUNT { get; set; }
        public decimal NLTRANSACTION_AMOUNT { get; set; }
        public decimal NBTRANSACTION_AMOUNT { get; set; }
        public string CDOCUMENT_DATE { get; set; } = string.Empty;
        public DateTime? DDOCUMENT_DATE { get; set; }
        public string CSUPPLIER_ID { get; set; } = string.Empty;
        public string CSUPPLIER_NAME { get; set; } = string.Empty;
        public string CFR_MODULE { get; set; } = string.Empty;
        public string CFR_DEPT_CODE { get; set; } = string.Empty;
        public string CFR_TRANSACTION_CODE { get; set; } = string.Empty;
        public string CFR_REFERENCE_NO { get; set; } = string.Empty;
        public string CDEPT_NAME { get; set; } = string.Empty;
        public string CCURRENCY_NAME { get; set; } = string.Empty;
        public string CTRANSACTION_NAME { get; set; } = string.Empty;
        public string CCODE { get; set; } = string.Empty;
        public string CDESCRIPTION { get; set; } = string.Empty;
        public string CTRANS_SEQNO { get; set; } = string.Empty;
        public string CASSET_TRANS_SEQNO { get; set; } = string.Empty;
        public decimal NTRANSACTION_AMOUNT1 { get; set; }
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
        public int ITRANSACTION_QTY1 { get; set; }
        public string CTRANSACTION_DESCR { get; set; } = string.Empty;
        public string CDEPR_METHOD_DESC { get; set; } = string.Empty;
        
        public int IUSEFUL_LIVE { get; set; }
        public decimal NLYEAR_DEPR_AMT { get; set; }
        public decimal NBYEAR_DEPR_AMT { get; set; }
        public byte[]? OASSET_IMAGE { get; set; }
        public byte[]? OOASSET_IMAGE { get; set; }
        public bool LDELETE_FLAG { get; set; }
        public string CDEPR_STATUS { get; set; } = string.Empty;
        public string CCURRENT_PRD { get; set; } = string.Empty;
        public string CCREATE_BY { get; set; } = string.Empty;
        public string CUPDATE_BY { get; set; } = string.Empty;
        public DateTime? DINSERVICE_DATE { get; set; }
        public bool LNEW_FLAG { get; set; }
        public int IUSEFUL_LIVE_YR { get; set; }
        public int IUSEFUL_LIVE_MO { get; set; }
        public bool LINCREMENT_FLAG { get; set; }
        public string CSEQNO { get; set; } = string.Empty;
        public string CFR_TRANSACTION_DATE { get; set; } = string.Empty;
        public DateTime? DFR_TRANSACTION_DATE { get; set; }
        public string CFR_SEQUENCE_NO { get; set; } = string.Empty;
        public string CTRANS_DESCRIPTION { get; set; } = string.Empty;
        public string CSCATEGORY_CODE { get; set; } = string.Empty;
        public decimal NLYTD_DEPR_AMT { get; set; }
        public decimal NBYTD_DEPR_AMT { get; set; }
        public string CPURCHASE_DATE { get; set; } = string.Empty;
        public DateTime? DPURCHASE_DATE { get; set; }
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
        public decimal NLPRIOR_DEPR_AMT { get; set; }
        public decimal NBPRIOR_DEPR_AMT { get; set; }
        public decimal NLREVALUATION_AMT { get; set; }
        public decimal NBREVALUATION_AMT { get; set; }
        public decimal NLPRIOR_REVALUATION_AMT { get; set; }
        public decimal NBPRIOR_REVALUATION_AMT { get; set; }
        public decimal NLYTD_REVALUATION_AMT { get; set; }
        public decimal NBYTD_REVALUATION_AMT { get; set; }
        public string CLSEQUENCE_NO { get; set; } = string.Empty;
        public string CLAST_TRANS_DATE { get; set; } = string.Empty;
        public DateTime? DLAST_TRANS_DATE { get; set; }
        public string CLAST_DEPR_PERIOD { get; set; } = string.Empty;
        public string CASSET_STATUS { get; set; } = string.Empty;
        public decimal NLAST_BBASE_RATE_AMOUNT { get; set; }
        public decimal NLAST_BCURRENCY_RATE_AMOUNT { get; set; }
        public string CLAST_CURR_RATE_DATE { get; set; } = string.Empty;
        public DateTime? DLAST_CURR_RATE_DATE { get; set; }
        public decimal NBRATE_REVALUATION_AMT { get; set; }
        public decimal NEXPENSE_PCT { get; set; }
        public string CEXPENSE_DEPT_CODE { get; set; } = string.Empty;
        public string COLD_FLAG { get; set; } = string.Empty;
        public string COLD_DEPT_CODE { get; set; } = string.Empty;
        public string CNEW_DEPT_CODE { get; set; } = string.Empty;
        public string CMODE { get; set; } = string.Empty;
        public string CJRNGRP_DESC { get; set; } = string.Empty;
        public string CTAX_CATEGORY_DESC { get; set; } = string.Empty;
        public string CCATEGORY_DESC { get; set; } = string.Empty;
        public string CASSET_DEPT_NAME { get; set; } = string.Empty;
        public bool LASSET_INCREMENT_FLAG { get; set; }
        public bool LJRNGRP_MODE { get; set; }
        public bool LDEPT_MODE { get; set; }
        public decimal NLRATE { get; set; }
        public decimal NBRATE { get; set; }
        public decimal NBXRATE { get; set; }
        public decimal NTOTAL_AMOUNT { get; set; }
        public decimal NLTOTAL_AMOUNT { get; set; }
        public decimal NBTOTAL_AMOUNT { get; set; }
        public DateTime DUPDATE_DATE { get; set; }
        public int IOUSEFUL_LIVE_YR { get; set; }
        public int IOUSEFUL_LIVE_MO { get; set; }
        public int IOUSEFUL_LIVE { get; set; }
        public decimal NOLBOOK_VALUE { get; set; }
        public decimal NOBBOOK_VALUE { get; set; }
        public int IBEG_UL_YR { get; set; }
        public int IBEG_UL_MO { get; set; }
        public int IREM_UL_YR { get; set; }
        public int IREM_UL_MO { get; set; }
        public decimal NBEG_BOOK_VAL { get; set; }
        public decimal DeprAmt { get; set; }
        public bool LGLLINK { get; set; }
        public string CGLLINK_DATE { get; set; } = string.Empty;

    
    }
}
