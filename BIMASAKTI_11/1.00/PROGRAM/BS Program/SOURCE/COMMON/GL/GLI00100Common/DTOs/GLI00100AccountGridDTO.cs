using System;
using R_APICommonDTO;

namespace GLI00100Common.DTOs
{
    public class GLI00100AccountGridDTO
    {
        public string CGLACCOUNT_NO { get; set; }
        public string CGLACCOUNT_NAME { get; set; }
    }

    public class GLI00100AccountAnalysisDetailDTO : R_APIResultBaseDTO
    {
        public string CGLACCOUNT_NO { get; set; } = "";
        public string CGLACCOUNT_NAME { get; set; } = "";
        public string CCENTER_CODE { get; set; } = "";
        public string CCENTER_NAME { get; set; } = "";
        public string CYEAR { get; set; } = "";
        public string CPERIOD_NO { get; set; } = "";
        public string CCURRENCY { get; set; } = "";
        public string CCURRENCY_TYPE_NAME { get; set; } = "";
        public int NOPENING { get; set; }
        public int NPTD_DEBIT { get; set; }
        public int NPTD_DEBIT_ADJ { get; set; }
        public int NPTD_CREDIT { get; set; }
        public int NPTD_CREDIT_ADJ { get; set; }
        public int NENDING { get; set; }
    }


    public class GLI00100TransactionGridDTO : R_APIResultBaseDTO
    {
        public string CREC_ID { get; set; }
        public string CREF_DATE { get; set; }
        public DateTime DREF_DATE { get; set; }
        public string CREF_NO { get; set; }
        public string CTRANSACTION_NAME { get; set; }
        public string CCENTER { get; set; }
        public int NDEBIT { get; set; }
        public int NCREDIT { get; set; }
        public string CDOCUMENT_NO { get; set; }
        public string CDETAIL_DESC { get; set; }
    }

    public class GLI00100AccountAnalysisDetailParamDTO
    {
        public string CGLACCOUNT_NO { get; set; }
        public string CPERIOD { get; set; }
        public string CCURRENCY_TYPE { get; set; }
        public string CCENTER_CODE { get; set; }
    }

    public class GLI00100JournalDTO : R_APIResultBaseDTO
    {
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
        public bool LIMPORTED { get; set; }
        public string CSTATUS { get; set; }
        public string CAPPROVE_BY { get; set; }
        public DateTime DAPPROVE_DATE { get; set; }
        public string CCOMMIT_BY { get; set; }
        public DateTime DCOMMIT_DATE { get; set; }
        public string CREC_ID { get; set; }
        public string CCREATE_BY { get; set; }
        public DateTime DCREATE_DATE { get; set; }
        public string CUPDATE_BY { get; set; }
        public DateTime DUPDATE_DATE { get; set; }
        public bool LALLOW_APPROVE { get; set; }
    }

    public class GLI00100JournalParamDTO
    {
        public string CREC_ID { get; set; }
    }

    public class GLI00100JournalGridDTO : R_APIResultBaseDTO
    {
        public string CREC_ID { get; set; }
        public string CBSIS { get; set; }
        public string CGLACCOUNT_NO { get; set; }
        public string CGLACCOUNT_NAME { get; set; }
        public string CCENTER { get; set; }
        public string CBDCR { get; set; }
        public int NDEBIT { get; set; }
        public int NCREDIT { get; set; }
        public string CDETAIL_DESC { get; set; }
        public string CDOCUMENT_NO { get; set; }
        public string CDOCUMENT_DATE { get; set; }
        public DateTime DDOCUMENT_DATE { get; set; }
        public int NLDEBIT { get; set; }
        public int NLCREDIT { get; set; }
        public int NBDEBIT { get; set; }
        public int NBCREDIT { get; set; }
    }

    public class GLI00100PrintParamDTO
    {
        public string CGLACCOUNT_NO { get; set; }
        public string CYEAR { get; set; }
    }
}