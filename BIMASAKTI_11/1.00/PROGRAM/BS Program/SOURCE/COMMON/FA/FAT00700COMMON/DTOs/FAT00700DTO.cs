using System;

namespace FAT00700Common.DTOs
{
    public class FAT00700DTO
    {
        public string CASSET_CODE { get; set; } = string.Empty;
        public string CASSET_NAME { get; set; } = string.Empty;
        public string CASSET_TRANS_SEQ_NO { get; set; } = string.Empty;
        public string CCOMPANY_ID { get; set; } = string.Empty;
        public string CCREATE_BY { get; set; } = string.Empty;
        public string CCURRENCY_CODE { get; set; } = string.Empty;
        public string CDEPT_CODE { get; set; } = string.Empty;
        public string CDEPT_NAME { get; set; } = string.Empty;
        public string CGL_REF_NO { get; set; } = string.Empty;
        public string CREC_ID { get; set; } = string.Empty;
        public string CREF_DATE { get; set; } = string.Empty;
        public string CREF_NO { get; set; } = string.Empty;
        public string CTRANS_DESC { get; set; } = string.Empty;
        public string CTRANS_STATUS { get; set; } = string.Empty;
        public string CTRANS_STATUS_NAME { get; set; } = string.Empty;
        public string CUNIT { get; set; } = string.Empty;
        public string CUPDATE_BY { get; set; } = string.Empty;
        public string CUSER_ID { get; set; } = string.Empty;
        public string CLANG_ID { get; set; } = string.Empty;   
        public string CASSET_DEPT_CODE { get; set; } = string.Empty;    
        public string CASSET_DEPT_NAME { get; set; } = string.Empty;
        public string CLOCAL_CURRENCY_CODE { get; set; } = string.Empty;
        public string CBASE_CURRENCY_CODE { get; set; } = string.Empty;
        public string CEXPENSE_ALLOC_ID { get; set; } = string.Empty;
        public string CEXPENSE_ALLOC_NAME { get; set; } = string.Empty; 
        public string CSOFT_PERIOD { get; set; } = string.Empty;
        public string CACTION { get; set; } = string.Empty;
        public string CNEW_STATUS { get; set; } = string.Empty; 
        public DateTime? DCREATE_DATE { get; set; }
        public DateTime? DREF_DATE { get; set; }
        public DateTime? DUPDATE_DATE { get; set; }

        public int IQTY { get; set; }

        public bool LINCREMENT_FLAG { get; set; }

        public decimal NBBASE_RATE { get; set; }
        public decimal NBBOOK_VALUE { get; set; }
        public decimal NBCURRENCY_RATE { get; set; }
        public decimal NBTRANS_AMOUNT { get; set; }
        public decimal NBOOK_VALUE { get; set; }
        public decimal NLBASE_RATE { get; set; }
        public decimal NLBOOK_VALUE { get; set; }
        public decimal NLCURRENCY_RATE { get; set; }
        public decimal NLTRANS_AMOUNT { get; set; }
        public decimal NTRANS_AMOUNT { get; set; }

        //additional
        public string CDEPT_CODE_DEFAULT { get; set; }

    }
}

