using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace GLT00100COMMON
{
    public class GLT00110DTO
    {
        public string CREC_ID { get; set; }
        public string CACTION { get; set; }
        public string CCOMPANY_ID { get; set; }
        public string CDEPT_CODE { get; set; }
        public string CDEPT_NAME { get; set; }
        public string CTRANS_CODE { get; set; }
        public string CTRANSACTION_NAME { get; set; }
        public string CREF_NO { get; set; }
        public string CREF_DATE { get; set; }
        public string CREF_PRD { get; set; }
        public string CDOC_NO { get; set; }
        public string CDOC_DATE { get; set; }
        public string CDOC_SEQ_NO { get; set; }
        public string CREVERSE_DATE { get; set; }
        public bool LREVERSE { get; set; }
        public string CTRANS_DESC { get; set; }
        public string CCURRENCY_CODE { get; set; }
        public string CLOCAL_CURRENCY_CODE { get; set; }
        public string CBASE_CURRENCY_CODE { get; set; }
        public decimal NLBASE_RATE { get; set; }
        public decimal NLCURRENCY_RATE { get; set; }
        public decimal NBBASE_RATE { get; set; }
        public decimal NBCURRENCY_RATE { get; set; }
        public decimal NTRANS_AMOUNT { get; set; }
        public decimal NLTRANS_AMOUNT { get; set; }
        public decimal NBTRANS_AMOUNT { get; set; }
        public decimal NDEBIT_AMOUNT { get; set; }
        public decimal NCREDIT_AMOUNT { get; set; }
        public decimal NLDEBIT_AMOUNT { get; set; }
        public decimal NLCREDIT_AMOUNT { get; set; }
        public decimal NBDEBIT_AMOUNT { get; set; }
        public decimal NBCREDIT_AMOUNT { get; set; }
        public decimal NPRELIST_AMOUNT { get; set; }
        public bool LINTERCO { get; set; }
        public int IINTERCO_TYPE { get; set; }
        public string CSOURCE_TRANS_CODE { get; set; }
        public string CSOURCE_REF_NO { get; set; }
        public string CSOURCE_MODULE { get; set; }
        public string CSOURCE_SEQ_NO { get; set; }

        public bool LIMPORTED { get; set; }
        public string CSTATUS { get; set; }
        public string CSTATUS_NAME { get; set; }
        public string CAPPROVE_BY { get; set; }
        public DateTime DAPPROVE_DATE { get; set; }
        public string CCOMMIT_BY { get; set; }
        public DateTime DCOMMIT_DATE { get; set; }
        public bool LALLOW_APPROVE { get; set; }
        public string CCREATE_BY { get; set; }
        public DateTime? DCREATE_DATE { get; set; }
        public string? CCREATE_DATE { get; set; }
        public string CUPDATE_BY { get; set; }
        public DateTime? DUPDATE_DATE { get; set; }
        public string? CUPDATE_DATE { get; set; }
        
        public string CPARAM_CALLER_ID { get; set; }

    }
}
