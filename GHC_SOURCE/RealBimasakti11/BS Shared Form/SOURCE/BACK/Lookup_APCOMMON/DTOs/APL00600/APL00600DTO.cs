using System;

namespace Lookup_APCOMMON.DTOs.APL00600
{
    public class APL00600DTO
    {
        public DateTime? DDUE_DATE { get; set; }
        public string CACCOUNT_NO { get; set; }
        public string CCOMPANY_ID { get; set; }
        public string CPROPERTY_ID { get; set; }
        public string CPAYMENT_TYPE { get; set; }
        public string CSUPPLIER_ID { get; set; }
        public string CLANG_ID { get; set; }
        public string CREC_ID { get; set; }
        public string CCUST_SUPP_ID { get; set; }
        public string CCUST_SUPP_NAME { get; set; }
        public string CDEPT_CODE { get; set; }
        public string CDEPT_NAME { get; set; }
        public string CREF_NO { get; set; }
        public string CREF_DATE { get; set; }
        public DateTime? DREF_DATE { get; set; }
        public string CSCH_PAYMENT_DATE { get; set; }
        public DateTime? DSCH_PAYMENT_DATE { get; set; }
        public string CCB_CODE { get; set; }
        public string CCB_NAME { get; set; }
        public string CCB_ACCOUNT_NO { get; set; }
        public string CCB_ACCOUNT_NAME { get; set; }
        public string CCURRENCY_CODE { get; set; }
        public decimal NAMOUNT { get; set; }
        public string CTRANS_CODE { get; set; }
        public string CTRANS_NAME { get; set; }
        public string CDUE_DATE { get; set; }
        public decimal NALLOC_AMOUNT { get; set; }
        public decimal NAPPLIED_AMOUNT { get; set; }
        public string CTRANS_DESC { get; set; }

        public string CSUPLIER
        {
            get => CCUST_SUPP_ID + " - " + CCUST_SUPP_NAME;
        }
    }
}