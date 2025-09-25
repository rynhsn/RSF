using System;
using System.Collections.Generic;

namespace CBR00600COMMON
{
    public class CBR00600AllocationHeaderDTO
    {
        public string CREPORT_TITLE { get; set; }

        public string CCB_REF_NO { get; set; }
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
        public List<CBR00600AllocationDetailDTO> DetailAllocation { get; set; }
    }
}
