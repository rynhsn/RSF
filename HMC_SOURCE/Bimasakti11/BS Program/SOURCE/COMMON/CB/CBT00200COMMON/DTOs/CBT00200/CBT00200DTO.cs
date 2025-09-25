using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace CBT00200COMMON
{
    public class CBT00200DTO
    {
        public string CINPUT_TYPE { get; set; }
        public string CREC_ID { get; set; }
        public string CACTION { get; set; }
        public string CCOMPANY_ID { get; set; }
        public string CDEPT_CODE { get; set; } = "";
        public string CDEPT_NAME { get; set; }
        public string CTRANS_CODE { get; set; }
        public string CTRANSACTION_NAME { get; set; }
        public string CREF_NO { get; set; }
        public string CREF_DATE { get; set; }
        public DateTime DREF_DATE { get; set; }
        public string CREF_PRD { get; set; }
        public string CPAYMENT_TYPE { get; set; }
        public string CCB_CODE { get; set; } = "";
        public string CCB_NAME { get; set; }
        public string CCB_ACCOUNT_NO { get; set; }
        public string CCB_ACCOUNT_NAME { get; set; }
        public string CCUST_SUPP_ID { get; set; }
        public string CINFO_SEQ_NO { get; set; }
        public string CTRANS_DESC { get; set; }
        public string CFRTO_COMPANY_ID { get; set; }
        public string CFRTO_DEPT_CODE { get; set; }
        public string CFRTO_TRANS_CODE { get; set; }
        public string CFRTO_REF_NO { get; set; }
        public string CFRTO_CB_CODE { get; set; }
        public string CFRTO_CB_ACCOUNT_NO { get; set; }
        public string CSTRANS_CODE { get; set; }
        public string CSREF_NO { get; set; }
        public string CFRTO_BANK_CODE { get; set; }
        public string CFRTO_BANK_ACCOUNT_NO { get; set; }
        public string CDOC_NO { get; set; }
        public string CDOC_DATE { get; set; }
        public string CCURRENCY_CODE { get; set; }
        public decimal NLBASE_RATE { get; set; }
        public decimal NLCURRENCY_RATE { get; set; }
        public decimal NBBASE_RATE { get; set; }
        public decimal NBCURRENCY_RATE { get; set; }
        public decimal NTRANS_AMOUNT { get; set; }
        public decimal NLTRANS_AMOUNT { get; set; }
        public decimal NBTRANS_AMOUNT { get; set; }
        public decimal NBANK_CHARGES { get; set; }
        public decimal NLBANK_CHARGES { get; set; }
        public decimal NBBANK_CHARGES { get; set; }
        public decimal NREMAINING { get; set; }
        public decimal NLREMAINING { get; set; }
        public decimal NBREMAINING { get; set; }
        public string CSTATUS { get; set; }
        public string CSTATUS_NAME { get; set; }
        public bool LGLINK { get; set; }
        public string CGL_REF_NO { get; set; }
        public string CAPPROVE_BY { get; set; }
        public DateTime? DAPPROVE_DATE { get; set; }
        public string CCOMMIT_BY { get; set; }
        public DateTime? DCOMMIT_DATE { get; set; }

        public string CCREATE_BY { get; set; }
        public DateTime? DCREATE_DATE { get; set; }
        public string CUPDATE_BY { get; set; }
        public DateTime? DUPDATE_DATE { get; set; }

        public string CLOCAL_CURRENCY_CODE { get; set; }
        public string CBASE_CURRENCY_CODE { get; set; }
        public bool LALLOW_APPROVE { get; set; }
        public decimal NDEBIT_AMOUNT { get; set; }
        public decimal NCREDIT_AMOUNT { get; set; }


        //Deposit Parameter
        public string PARAM_CALLER_ID { get; set; }
        public string PARAM_CALLER_TRANS_CODE { get; set; }
        public string PARAM_CALLER_REF_NO { get; set; }
        public string PARAM_DEPT_CODE { get; set; }
        public string PARAM_REF_NO { get; set; }
        public string PARAM_DOC_NO { get; set; }
        public string PARAM_DOC_DATE { get; set; }
        public string PARAM_DESCRIPTION { get; set; }
        public string PARAM_GLACCOUNT_NO { get; set; }
        public string PARAM_CENTER_CODE { get; set; }
        public string PARAM_CASH_FLOW_GROUP_CODE { get; set; }
        public string PARAM_CASH_FLOW_CODE { get; set; }
        public string PARAM_AMOUNT { get; set; }
    }
}
