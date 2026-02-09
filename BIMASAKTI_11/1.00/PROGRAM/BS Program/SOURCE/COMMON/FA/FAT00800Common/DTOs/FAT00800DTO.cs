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

        // From RSP_FAT00800_GET_TRANS_DETAIL / RSP_FAT00800_SAVE_TRANS - Header / Ref
        public string CREC_ID { get; set; } = string.Empty;
        public string CDEPT_CODE { get; set; } = string.Empty;
        public string CDEPT_NAME { get; set; } = string.Empty;
        public string CREF_NO { get; set; } = string.Empty;
        public string CREF_DATE { get; set; } = string.Empty;
        public string CGL_REF_NO { get; set; } = string.Empty;
        public string CACTION { get; set; } = string.Empty;
        public string CTRANSACTION_CODE { get; set; } = string.Empty;
        public string CREFERENCE_NO { get; set; } = string.Empty;
        public string CEXPENSE_ALLOC_NAME { get; set; } = string.Empty;
        public string CASSET_DEPT_CODE { get; set; } = string.Empty;
        public string CASSET_DEPT_NAME { get; set; } = string.Empty;

        // Currency / rates
        public string CCURRENCY_CODE { get; set; } = string.Empty;
        public decimal NLBASE_RATE { get; set; }
        public decimal NLCURRENCY_RATE { get; set; }
        public decimal NBBASE_RATE { get; set; }
        public decimal NBCURRENCY_RATE { get; set; }

        // Transaction description / status
        public string CTRANS_DESC { get; set; } = string.Empty;
        public string CTRANS_STATUS { get; set; } = string.Empty;
        public string CTRANS_STATUS_NAME { get; set; } = string.Empty;
        public string CSTATUS { get; set; } = string.Empty;
        public string CTRANSACTION_DATE { get; set; } = string.Empty;
        public DateTime DTRANSACTION_DATE { get; set; }
        public string CALLOC_EXPENSE_CODE { get; set; } = string.Empty;

        // Amounts (trans, book, gain/loss)
        public decimal NTRANS_AMOUNT { get; set; }
        public decimal NLTRANS_AMOUNT { get; set; }
        public decimal NBTRANS_AMOUNT { get; set; }
        public decimal NTRANSACTION_AMOUNT { get; set; }
        public decimal NLTRANSACTION_AMOUNT { get; set; }
        public decimal NBTRANSACTION_AMOUNT { get; set; }
        public decimal NTRANSACTION_AMOUNT1 { get; set; }
        public decimal NLTRANSACTION_AMOUNT1 { get; set; }
        public decimal NBTRANSACTION_AMOUNT1 { get; set; }
        public decimal NLBASE_RATE_AMOUNT { get; set; }
        public decimal NLCURRENCY_RATE_AMOUNT { get; set; }
        public decimal NBBASE_RATE_AMOUNT { get; set; }
        public decimal NBCURRENCY_RATE_AMOUNT { get; set; }
        public decimal NLBOOKVAL { get; set; }
        public decimal NBBOOKVAL { get; set; }
        public decimal NLGAIN_LOSS { get; set; }
        public decimal NBGAIN_LOSS { get; set; }
        public decimal NSALES_AMOUNT { get; set; }
        public decimal NBOOK_VALUE { get; set; }
        public decimal NLBOOK_VALUE { get; set; }
        public decimal NBBOOK_VALUE { get; set; }
        public decimal NLGAINLOSS { get; set; }
        public decimal NBGAINLOSS { get; set; }

        // Asset
        public string CASSET_CODE { get; set; } = string.Empty;
        public string CASSET_NAME { get; set; } = string.Empty;
        public string CASSET_TRANS_SEQ_NO { get; set; } = string.Empty;
        public int IQTY { get; set; }
        public string CUNIT { get; set; } = string.Empty;

        // Expense allocation
        public string CEXPENSE_ALLOC_ID { get; set; } = string.Empty;

        // Audit
        public string CCREATE_BY { get; set; } = string.Empty;
        public DateTime DCREATE_DATE { get; set; }
        public string CUPDATE_BY { get; set; } = string.Empty;
        public DateTime DUPDATE_DATE { get; set; }
    }
}

