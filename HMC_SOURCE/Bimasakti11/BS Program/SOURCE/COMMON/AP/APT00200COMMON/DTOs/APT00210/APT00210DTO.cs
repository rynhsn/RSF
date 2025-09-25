using System;
using System.Collections.Generic;
using System.Text;

namespace APT00200COMMON.DTOs.APT00210
{
    public class APT00210DTO
    {
        //HIDDEN
        public string CREC_ID { get; set; } = "";
        public string CGL_REF_NO { get; set; } = "";
        public string CTRANS_CODE { get; set; } = "";

        //SHOWN
        public string CPROPERTY_ID { get; set; } = "";
        public string CTRANS_STATUS { get; set; } = "";
        public string CTRANS_STATUS_NAME { get; set; } = "";
        public string CDEPT_CODE { get; set; } = "";
        public string CDEPT_NAME { get; set; } = "";
        public string CREF_NO { get; set; } = "";
        public DateTime DREF_DATE { get; set; } = DateTime.Today;
        public string CREF_DATE { get; set; } = "";
        public string CSUPPLIER_ID { get; set; } = "";
        public string CSUPPLIER_SEQ_NO { get; set; } = "";
        public string CSUPPLIER_NAME { get; set; } = "";
        public bool LONETIME { get; set; } = false;
        public string CDOC_NO { get; set; } = "";
        public string CDOC_DATE { get; set; } = "";
        public DateTime DDOC_DATE { get; set; } = DateTime.Today;
        public string CCURRENCY_CODE { get; set; } = "";
        public decimal NLBASE_RATE { get; set; } = 0;
        public decimal NLCURRENCY_RATE { get; set; } = 0;
        public string CLOCAL_CURRENCY_CODE { get; set; } = "";
        public decimal NBBASE_RATE { get; set; } = 0;
        public decimal NBCURRENCY_RATE { get; set; } = 0;
        public string CBASE_CURRENCY_CODE { get; set; } = "";
        public bool LTAXABLE { get; set; } = false;
        public string CTAX_ID { get; set; } = "";
        public string CTAX_NAME { get; set; } = "";
        public decimal NTAX_PCT { get; set; } = 0;
        public decimal NTAX_BASE_RATE { get; set; } = 0;
        public decimal NTAX_CURRENCY_RATE { get; set; } = 0;
        public string CTRANS_DESC { get; set; } = "";

        //Summary
        public decimal NTRANS_AMOUNT { get; set; } = 0;
        public decimal NAMOUNT { get; set; } = 0;
        public decimal NDISCOUNT { get; set; } = 0;
        public decimal NSUMMARY_DISCOUNT { get; set; } = 0;
        public decimal NADD_ON { get; set; } = 0;
        public decimal NTAXABLE_AMOUNT { get; set; } = 0;
        public decimal NTAX { get; set; } = 0;
        public decimal NLTAX { get; set; } = 0;
        public decimal NOTHER_TAX { get; set; } = 0;
        public decimal NLOTHER_TAX { get; set; } = 0;
        public decimal NADDITION { get; set; } = 0;
        public decimal NDEDUCTION { get; set; } = 0;
        public decimal NTOTAL_AMOUNT { get; set; } = 0;
        public string CCREATE_BY { get; set; } = "";
        public DateTime DCREATE_DATE { get; set; } = DateTime.Now;
        public string CUPDATE_BY { get; set; } = "";
        public DateTime DUPDATE_DATE { get; set; } = DateTime.Now;
    }
}
