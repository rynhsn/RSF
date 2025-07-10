using System;

namespace ICR00100Common.DTOs.Print
{
    public class ICR00100DataResultDTO
    {
        public string CPROPERTY_ID { get; set; }
        public string CDEPT_CODE { get; set; }
        public string CDEPT_NAME { get; set; }
        public string CPRODUCT_ID { get; set; }
        public string CPRODUCT_NAME { get; set; }
        public string CTRANSACTION_NAME { get; set; }
        public string CREF_NO { get; set; }
        public string CREF_DATE { get; set; }
        public DateTime? DREF_DATE { get; set; }
        public string CMOD_ID { get; set; }
        public string CDOC_NO { get; set; }
        public string CDOC_DATE { get; set; }
        public DateTime? DDOC_DATE { get; set; }
        public decimal NQTY_IN { get; set; }
        public decimal NVALUE_IN { get; set; }
        public decimal NQTY_OUT { get; set; }
        public decimal NQTY_BEGINNING { get; set; }
        public decimal NQTY_BALANCE { get; set; }
        public decimal NVALUE_BALANCE { get; set; }
        public decimal NQTY_IN_1 { get; set; }
        public decimal NQTY_IN_2 { get; set; }
        public decimal NQTY_IN_3 { get; set; }
        public decimal NQTY_OUT_1 { get; set; }
        public decimal NQTY_OUT_2 { get; set; }
        public decimal NQTY_OUT_3 { get; set; }
        public decimal NQTY_BAL_1 { get; set; }
        public decimal NQTY_BAL_2 { get; set; }
        public decimal NQTY_BAL_3 { get; set; }
        public decimal NQTY_BEGINNING_1 { get; set; }
        public decimal NQTY_BEGINNING_2 { get; set; }
        public decimal NQTY_BEGINNING_3 { get; set; }
        public string CUNIT1 { get; set; }
        public string CUNIT2 { get; set; }
        public string CUNIT3 { get; set; }
    }
}