using System;
using System.Collections.Generic;
using System.Text;

namespace CBT01200Common.DTOs
{
    public class CBT01200TransHDRecordDTO : CBT01200DTO
    {
        public string CTRANSACTION_NAME { get; set; }
        public string CPAYMENT_TYPE { get; set; }
     
        public string CCB_ACCOUNT_NAME { get; set; }
        public string CCUST_SUPP_ID { get; set; }
        public string CINFO_SEQ_NO { get; set; }
        public string CFRTO_COMPANY_ID { get; set; }
        public string CFRTO_DEPT_CODE { get; set; }
        public string CFRTO_TRANS_CODE { get; set; }
        public string CFRTO_REF_NO { get; set; }
        public string CFRTO_CB_CODE { get; set; }
        public string CFRTO_CB_ACCOUNT_NO { get; set; }
        public string CSTRANS_CODE { get; set; }
        public string CSREF_NO { get; set; }
        public string CFRTO_BANK_ACCOUNT_NO { get; set; }
        
    
        public decimal NLTRANS_AMOUNT { get; set; }
        public decimal NBTRANS_AMOUNT { get; set; }
        public decimal NBANK_CHARGES { get; set; }
        public decimal NLBANK_CHARGES { get; set; }
        public decimal NBBANK_CHARGES { get; set; }
        public decimal NNET_AMOUNT { get; set; }
        public decimal NLNET_AMOUNT { get; set; }
        public decimal NBNET_AMOUNT { get; set; }
        public string CAPPLIED_CURRENCY_CODE { get; set; }
        public decimal NAPPLIED_AMOUNT { get; set; }
        public decimal NLAPPLIED_AMOUNT { get; set; }
        public decimal NBAPPLIED_AMOUNT { get; set; }
        public decimal NREMAINING { get; set; }
        public decimal NLREMAINING { get; set; }
        public decimal NBREMAINING { get; set; }
        public string CDOC_SEQ_NO { get; set; }
        public string CREVERSE_DATE { get; set; }
        public bool LREVERSE { get; set; }
        public string CLOCAL_CURRENCY_CODE { get; set; }
        public string CBASE_CURRENCY_CODE { get; set; }
       
        public decimal NLDEBIT_AMOUNT { get; set; }
        public decimal NLCREDIT_AMOUNT { get; set; }
        public decimal NBDEBIT_AMOUNT { get; set; }
        public decimal NBCREDIT_AMOUNT { get; set; }
        public bool LGLINK { get; set; }
        public string CGL_REF_NO { get; set; }
        public string CAPPROVE_BY { get; set; }
        public DateTime DAPPROVE_DATE { get; set; }
        public string CCOMMIT_BY { get; set; }
        public DateTime DCOMMIT_DATE { get; set; }
    }
}
