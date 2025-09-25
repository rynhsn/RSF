using System;
using System.Collections.Generic;
using System.Text;

namespace APT00100COMMON.DTOs.APT00110Print
{
    public class APT00110PrintReportColumnDTO
    {
        //HEADER
        public string CREF_NO { get; set; } = "Ref. No.";
        public string CREF_DATE { get; set; } = "Date";
        public string CDOC_NO { get; set; } = "Supplier’s Ref. No.";
        public string CDOC_DATE { get; set; } = "Date";
        public string CSUPPLIER_NAME { get; set; } = "Supplier";
        public string CSUPPLIER_ADDRESS { get; set; } = "Address";
        public string CCURRENCY_CODE { get; set; } = "Currency";
        public string CPAY_TERM_NAME { get; set; } = "Terms";
        public string CSUPPLIER_PHONE_FAX1 { get; set; } = "Phone / Fax";
        public string CSUPPLIER_EMAIL1 { get; set; } = "Email";

        //DETAIL
        public string INO { get; set; } = "No.";
        public string CPRODUCT_ID_NAME { get; set; } = "Description";
        public string NTRANS_QTY_CUNIT { get; set; } = "Quantity";
        public string NUNIT_PRICE { get; set; } = "Unit Price";
        public string NLINE_AMOUNT { get; set; } = "Total Price";
        public string NTOTAL_DISCOUNT { get; set; } = "Discount";
        public string NDIST_ADD_ON { get; set; } = "Add On";
        public string NLINE_TAXABLE_AMOUNT { get; set; } = "Amount";

        //FOOTER
        public string CTOTAL_AMOUNT_IN_WORDS { get; set; } = "Says";
        public string CTRANS_DESC { get; set; } = "Notes";
        public string NTAXABLE_AMOUNT { get; set; } = "Sub Total";
        public string NTAX { get; set; } = "Tax";
        public string NOTHER_TAX { get; set; } = "Other Tax";
        public string NADDITION { get; set; } = "Addition";
        public string NDEDUCTION { get; set; } = "Deduction";
        public string NTRANS_AMOUNT { get; set; } = "Grand Total";

        //Sub Total
        //DETAIL
        public string CGLACCOUNT_NO { get; set; } = "Account No.";
        public string CGLACCOUNT_NAME { get; set; } = "Account Name";
        public string CCENTER { get; set; } = "Center";
        public string CSUB_CURRENCY { get; set; } = "Curr.";
        public string CDEBIT { get; set; } = "Debit";
        public string CCREDIT { get; set; } = "Credit";
        //FOOTER
        public string CGRAND_TOTAL { get; set; } = "Grand Total";
    }
}
