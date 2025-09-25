using System;
using System.Collections.Generic;
using System.Text;

namespace PMR02000COMMON.DTO_s
{
    public class PMR02001SpResultDTO
    {
        public string CCOMPANY_ID { get; set; }
        public string CPROPERTY_ID { get; set; }
        public string CDEPT_CODE { get; set; }
        public string CDEPT_NAME { get; set; }
        public string CDEPT_CODE_NAME { get; set; }
        public string CCUSTOMER_ID { get; set; }
        public string CCUSTOMER_NAME { get; set; }
        public string CTRANS_CODE { get; set; }
        public string CTRX_TYPE_NAME { get; set; }
        public string CREF_NO { get; set; }
        public string CREF_DATE { get; set; }
        public DateTime DREF_DATE { get; set; }
        public string CTENANT_TYPE_ID { get; set; }
        public string CCUSTOMER_TYPE_NAME { get; set; }
        public string CLOI_AGRMT_NO { get; set; }
        public string CCURRENCY_CODE { get; set; }
        public decimal NBEGINNING_APPLY_AMOUNT { get; set; }
        public decimal NREMAINING_AMOUNT { get; set; }
        public decimal NTAX_AMOUNT { get; set; }
        public decimal NGAINLOSS_AMOUNT { get; set; }
        public decimal NCASHBANK_AMOUNT { get; set; }
        public string CSEQ_NO { get; set; }
        public string CPRODUCT_OR_CHARGE_ID { get; set; }
        public string CPRODUCT_OR_CHARGE_NAME { get; set; }
        public string CPRODUCT_DEPARTMENT_CODE { get; set; }
        public string CPRODUCT_DEPARTMENT_NAME { get; set; }
        public decimal NPRODUCT_QUANTITY { get; set; }
        public string CPRODUCT_MEASUREMENT_NAME { get; set; }
        public decimal NPRODUCT_PRICE_AMOUNT { get; set; }
        public decimal NPRODUCT_LINE_AMOUNT { get; set; }
        public decimal NPRODUCT_DISCOUNT_AMOUNT { get; set; }
        public decimal NOTHER_TAX_AMOUNT { get; set; }
        public decimal NPRODUCT_LINE_TOTAL_AMOUNT { get; set; }
    }
}
