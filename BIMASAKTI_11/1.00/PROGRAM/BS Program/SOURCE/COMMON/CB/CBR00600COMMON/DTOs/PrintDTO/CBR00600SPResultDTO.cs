using System;

namespace CBR00600COMMON
{
    public class CBR00600SPResultDTO
    {
        //RECEIPT
        public string CREF_NO { get; set; }
        public string CREF_DATE { get; set; }
        public DateTime? DREF_DATE { get; set; }
        public string CCUSTOMER_ID { get; set; }
        public string CUSTOMER_NAME { get; set; }
        public string CCUSTOMER_ID_NAME { get; set; }
        public string CCURRENCY_CODE { get; set; }
        public decimal NTRANS_AMOUNT { get; set; }
        public string CTRANS_AMOUNT { get; set; }
        public string CTRANS_DESC { get; set; }
        public string CMESSAGE_DESC { get; set; }
        public string CMESSAGE_DESC_RTF { get; set; }
        public string CADDITIONAL_INFO { get; set; }
        public string CADDITIONAL_INFO_RTF { get; set; }

        //ALLOCATION
        public string CREPORT_TITLE { get; set; }
        public string CCB_REF_NO { get; set; }
        public string CALLOC_DEPT_CODE { get; set; }
        public string CCB_REF_DATE { get; set; }
        public DateTime? DCB_REF_DATE { get; set; }
        public decimal NCB_AMOUNT { get; set; }
        public string CCUST_SUPP_ID { get; set; }
        public string CCUST_SUPP_ID_NAME { get; set; }
        public string CCB_CODE { get; set; }
        public string CCB_NAME { get; set; }
        public string CCB_ACCOUNT_NO { get; set; }
        public string CCB_DEPT_CODE { get; set; }
        public string CCB_DEPT_NAME { get; set; }
        public string CCB_DOC_NO { get; set; }
        public string CCB_DOC_DATE { get; set; }
        public DateTime? DCB_DOC_DATE { get; set; }
        public string CCB_CHEQUE_NO { get; set; }
        public string CCB_CHEQUE_DATE { get; set; }
        public DateTime? DCB_CHEQUE_DATE { get; set; }
        public string CCB_CURRENCY_CODE { get; set; }
        public string CCOMPANY_BASE_CURRENCY_CODE { get; set; }
        public string CCOMPANY_LOCAL_CURRENCY_CODE { get; set; }
        public string CCB_AMOUNT_WORDS { get; set; }
        public decimal NCBLBASE_RATE { get; set; }
        public decimal NCBLCURRENCY_RATE { get; set; }
        public decimal NCBBBASE_RATE { get; set; }
        public decimal NCBBCURRENCY_RATE { get; set; }
        public string CCB_PAYMENT_TYPE { get; set; }
        public string CCB_TRANS_DESC { get; set; }

        public int INO { get; set; }
        public string CALLOC_NO { get; set; }
        public string CALLOC_DATE { get; set; }
        public DateTime? DALLOC_DATE { get; set; }
        public string CINVOICE_NO { get; set; }
        public string CINVOICE_DESC { get; set; }

        //JOURNAL
        public string CGLACCOUNT_NO { get; set; }
        public string CGLACCOUNT_NAME { get; set; }
        public string CCENTER_CODE { get; set; }
        public decimal NDEBIT_AMOUNT { get; set; }
        public decimal NCREDIT_AMOUNT { get; set; }
    }
}
