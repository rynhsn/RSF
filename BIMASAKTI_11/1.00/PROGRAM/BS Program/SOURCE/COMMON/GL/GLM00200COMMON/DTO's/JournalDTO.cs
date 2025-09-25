using System;
using System.Collections.Generic;
using System.Text;

namespace GLM00200COMMON
{
    public class JournalDTO
    {
        public string CUSER_ID { get; set; } = "";
        public string CJRN_ID { get; set; } = "";
        public string CREC_ID { get; set; } = "";
        public string CACTION { get; set; } = "";
        public string CCOMPANY_ID { get; set; } = "";
        public string CDEPT_CODE { get; set; } = "";
        public string CDEPT_NAME { get; set; } = "";
        public string CTRANS_CODE { get; set; } = "";
        public string CREF_NO { get; set; } = "";
        public string CREF_DATE { get; set; } = "";
        public string CREF_DATE_Display { get; set; }
        public DateTime DREF_DATE { get; set; }
        public string CDOC_NO { get; set; } = "";
        public string CDOC_DATE { get; set; } = "";
        public DateTime DDOC_DATE { get; set; }
        public int IFREQUENCY { get; set; }
        public int IAPPLIED { get; set; }
        public int IPERIOD { get; set; }
        public string CSTATUS { get; set; } = "";
        public string CSTART_DATE { get; set; } = "";
        public string CSTART_DATE_Display { get; set; }
        public DateTime DSTART_DATE { get; set; }
        public string CNEXT_DATE { get; set; } = "";
        public string CNEXT_DATE_Display { get; set; }
        public DateTime DNEXT_DATE { get; set; }
        public string CLAST_DATE { get; set; } = "";
        public string CLAST_DATE_Display { get; set; }
        public DateTime DLAST_DATE { get; set; }
        public string CTRANS_DESC { get; set; } = "";
        public string CCURRENCY_CODE { get; set; } = "";
        public string CLOCAL_CURRENCY_CODE { get; set; } = "";
        public string CBASE_CURRENCY_CODE { get; set; } = "";
        public bool LFIX_RATE { get; set; } = true;
        public string CFIX_RATE { get; set; }
        public decimal NLBASE_RATE { get; set; }
        public decimal NLCURRENCY_RATE { get; set; }
        public decimal NBBASE_RATE { get; set; }
        public decimal NBCURRENCY_RATE { get; set; }
        public decimal NDEBIT_AMOUNT { get; set; }
        public decimal NCREDIT_AMOUNT { get; set; }
        public decimal NPRELIST_AMOUNT { get; set; }
        public decimal NNTRANS_AMOUNT_C { get; set; }
        public decimal NNTRANS_AMOUNT_D { get; set; }
        public string CUPDATE_BY { get; set; }
        public DateTime? DUPDATE_DATE { get; set; }
        public string CCREATE_BY { get; set; }
        public DateTime? DCREATE_DATE { get; set; }
        public string CLANGUAGE_ID { get; set; } = "";
        public bool LALLOW_APPROVE { get; set; }
        public string CNEXT_PRD { get; set; } = "";
        public decimal NTRANS_AMOUNT { get; set; }
        public string CSTATUS_NAME { get; set; } = "";
    }
}
