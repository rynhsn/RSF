using System;
using System.Collections.Generic;
using System.Text;

namespace APT00100COMMON.DTOs.APT00110Print
{
    public class APT00110PrintReportDTO
    {
        //HEADER
        public string CREF_NO { get; set; } = "";
        public string CREF_DATE { get; set; } = "";
        public DateTime DREF_DATE { get; set; } 
        public string CDOC_NO { get; set; } = "";
        public string CDOC_DATE { get; set; } = "";
        public DateTime DDOC_DATE { get; set; }
        public string CSUPPLIER_NAME { get; set; } = "";
        public string CSUPPLIER_ADDRESS { get; set; } = "";
        public string CCURRENCY_CODE { get; set; } = "";
        public string CPAY_TERM_NAME { get; set; } = "";
        public string CSUPPLIER_PHONE1 { get; set; } = "";
        public string CSUPPLIER_FAX1 { get; set; } = "";
        public string CSUPPLIER_EMAIL1 { get; set; } = "";
        public string CJRN_ID { get; set; } = "";

        //DETAIL
        public int INO { get; set; } = 0;
        public string CPRODUCT_ID { get; set; } = "";
        public string CPRODUCT_NAME { get; set; } = "";
        public string CDETAIL_DESC { get; set; } = "";
        public decimal NTRANS_QTY { get; set; } = 0;
        public string CUNIT { get; set; } = "";
        public decimal NUNIT_PRICE { get; set; } = 0;
        public decimal NLINE_AMOUNT { get; set; } = 0;
        public decimal NTOTAL_DISCOUNT { get; set; } = 0;
        public decimal NDIST_ADD_ON { get; set; } = 0;
        public decimal NLINE_TAXABLE_AMOUNT { get; set; } = 0;

        //FOOTER
        public string CTOTAL_AMOUNT_IN_WORDS { get; set; } = "";
        public string CTRANS_DESC { get; set; } = "";
        public decimal NTAXABLE_AMOUNT { get; set; } = 0;
        public decimal NTAX { get; set; } = 0;
        public decimal NOTHER_TAX { get; set; } = 0;
        public decimal NADDITION { get; set; } = 0;
        public decimal NDEDUCTION { get; set; } = 0;
        public decimal NTRANS_AMOUNT { get; set; } = 0;

        ////SUB DETAIL
        //public string CGLACCOUNT_NO { get; set; } = "";
        //public string CGLACCOUNT_NAME { get; set; } = "";
        //public string CCENTER { get; set; } = "";
        ////public string CCURRENCY_CODE { get; set; } = "";
        //public decimal NDEBIT { get; set; } = 0;
        //public decimal NCREDIT { get; set; } = 0;

        //LOGO
        public byte[] OLOGO { get; set; }
    }
}
