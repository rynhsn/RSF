using System;
using System.Collections.Generic;
using System.Text;

namespace APR00100COMMON.DTO_s
{
    public class APR00101SpResultDTO
    {
        public string CCOMPANY_ID { get; set; }
        public string CPROPERTY_ID { get; set; }
        public string CDEPT_CODE { get; set; }
        public string CDEPT_NAME { get; set; }
        public string CSUPPLIER_ID { get; set; }
        public string CSUPPLIER_NAME { get; set; }
        public string CSUPPLIER_TYPE_CODE { get; set; }
        public string CSUPPLIER_TYPE_NAME { get; set; }
        public string CREF_NO { get; set; }
        public string CREF_DATE { get; set; }
        public string CSUPPLIER_REF_NO { get; set; }
        public decimal NCURRENCY_RATE { get; set; }
        public string CCURRENCY_CODE { get; set; }
        public string CCURRENCY_NAME { get; set; }
        public string CCURRENCY_TYPE_CODE { get; set; }
        public string CCURRENCY_TYPE_NAME { get; set; }
        public decimal NBEGINNING_APPLY_AMOUNT { get; set; }
        public decimal NREMAINING_AMOUNT { get; set; }
        public decimal NTAX_AMOUNT { get; set; }
        public decimal NGAINLOSS_AMOUNT { get; set; }
        public decimal NCASHBANK_AMOUNT { get; set; }
        public string CPRODUCT_OR_CHARGE_ID { get; set; }
        public string CPRODUCT_OR_CHARGE_NAME { get; set; }
        public string CPRODUCT_DEPARTMENT_CODE { get; set; }
        public string CPRODUCT_DEPARTMENT_NAME { get; set; }
        public int IPRODUCT_QUANTITY { get; set; }
        public string CPRODUCT_MEASUREMENT_NAME { get; set; }
        public decimal NPRODUCT_PRICE_AMOUNT { get; set; }
        public decimal NPRODUCT_LINE_AMOUNT { get; set; }
        public decimal NPRODUCT_DISCOUNT_AMOUNT { get; set; }
        public decimal NPRODUCT_LUXURY_TAX_AMOUNT { get; set; }
        public decimal NPRODUCT_LINE_TOTAL_AMOUNT { get; set; }
    }
}
