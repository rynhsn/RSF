using System;
using System.Collections.Generic;
using System.Text;

namespace APT00200COMMON.DTOs.APT00221
{
    public class APT00221DTO
    {
        public string CREC_ID { get; set; } = "";
        public string CPROD_DEPT_CODE { get; set; } = "";
        public string CPROD_DEPT_NAME { get; set; } = "";
        public string CPROD_TYPE { get; set; } = "";
        public string CPRODUCT_ID { get; set; } = "";
        public string CPRODUCT_NAME { get; set; } = "";
        public string CSUP_PRODUCT_ID { get; set; } = "";
        public string CSUP_PRODUCT_NAME { get; set; } = "";
        public string CALLOC_ID { get; set; } = "";
        public string CALLOC_NAME { get; set; } = "";
        public string CDETAIL_DESC { get; set; } = "";
        public string CUNIT1 { get; set; } = "";
        public string CUNIT2 { get; set; } = "";
        public string CUNIT3 { get; set; } = "";
        public string CCREATE_BY { get; set; }
        public DateTime? DCREATE_DATE { get; set; }
        public string CUPDATE_BY { get; set; }
        public DateTime? DUPDATE_DATE { get; set; } 

        //PURCHASE QUANTITY
        public decimal NBILL_UNIT_QTY{ get; set; } = 0;
        public int IBILL_UNIT { get; set; } = 0;
        public string CBILL_UNIT { get; set; } = "";
        public decimal NSUPP_CONV_FACTOR { get; set; } = 0;
        public decimal NUNIT_PRICE { get; set; } = 0;
        public decimal NAMOUNT { get; set; } = 0;

        //LINE DISCOUNT 
        public string CDISC_TYPE { get; set; } = "";
        public decimal NDISC_PCT { get; set; } = 0;
        public decimal NDISC_AMOUNT { get; set; } = 0;
        public decimal NDIST_DISCOUNT { get; set; } = 0;
        public decimal NDIST_ADD_ON { get; set; } = 0;
        public decimal NTAXABLE_AMOUNT { get; set; } = 0;

        //TAX
        public string CTAX_ID { get; set; } = "";
        public string CTAX_NAME { get; set; } = "";
        public decimal NTAX_PCT { get; set; } = 0;
        public decimal NTAX_AMOUNT { get; set; } = 0;

        //OTHER TAX
        public string COTHER_TAX_ID { get; set; } = "";
        public string COTHER_TAX_NAME { get; set; } = "";
        public decimal NOTHER_TAX_PCT { get; set; } = 0;
        public decimal NOTHER_TAX_AMOUNT { get; set; } = 0;

        //OTHER
        public decimal NTOTAL_AMOUNT { get; set; } = 0;
    }
}
