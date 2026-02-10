using System;
using System.Globalization;

namespace FAT00300Common.DTOs
{
    public class FAT00300DTO
    {
        // Required standard properties (ALWAYS inclu
        public DateTime? DREF_DATE { get; set; }
        public string CTRANS_CODE { get; set; } = "";
        public string CLANG_ID { get; set; } = "";

        //For Currenct at Transaction Entry Tab
        public string CLOCAL_CURRENCY_CODE { get; set; } = "";
        public string CBASE_CURRENCY_CODE { get; set; } = "";
        public bool LINCREMENT_FLAG { get; set; }

        public string CMODE { get; set; } = "";
        public string CUSER_ID { get; set; } = "";
        public string CDEPT_CODE { get; set; } = string.Empty;
        public string CDEPT_NAME { get; set; } = string.Empty;
        public string CCOMPANY_ID { get; set; } = string.Empty;
        public string CASSET_TRANS_SEQ_NO { get; set; } = string.Empty;
        public string CASSET_DEPT_CODE { get; set; } = string.Empty;
        public string CASSET_DEPT_NAME { get; set; } = string.Empty;
        public string CASSET_CODE { get; set; } = string.Empty;
        public string CASSET_NAME { get; set; } = string.Empty;
        public string CUNIT { get; set; } = string.Empty;
        public string CREF_NO { get; set; } = string.Empty;
        public string CGL_REF_NO { get; set; } = string.Empty;
        public string CREF_DATE { get; set; } = string.Empty;
        public string CCURRENCY_CODE { get; set; } = string.Empty;
        public string CTRANS_DESC { get; set; } = string.Empty;
        public string CTRANS_STATUS { get; set; } = string.Empty;
        public string CTRANS_STATUS_NAME { get; set; } = string.Empty;
        public string CREC_ID { get; set; } = string.Empty;
        public string CCREATE_BY { get; set; } = string.Empty;
        public string CUPDATE_BY { get; set; } = string.Empty;
        public string CNEW_STATUS { get; set; } = string.Empty;
        public DateTime? DUPDATE_DATE { get; set; }
        public DateTime? DCREATE_DATE { get; set; }

        public int IQTY { get; set; }

        public decimal NTRANS_AMOUNT { get; set; }
        public decimal NLTRANS_AMOUNT { get; set; }
        public decimal NBTRANS_AMOUNT { get; set; }
        public decimal NLBASE_RATE { get; set; }
        public decimal NLCURRENCY_RATE { get; set; }
        public decimal NBBASE_RATE { get; set; }
        public decimal NBCURRENCY_RATE { get; set; }

        // Passing Param Master
        public string CSOFT_PERIOD { get; set; } = string.Empty;

    }
}







