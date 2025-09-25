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
        public decimal NQTY_IN1 { get; set; }
        public decimal NQTY_IN2 { get; set; }
        public decimal NQTY_IN3 { get; set; }
        public decimal NQTY_OUT1 { get; set; }
        public decimal NQTY_OUT2 { get; set; }
        public decimal NQTY_OUT3 { get; set; }
        public decimal NQTY_BAL1 { get; set; }
        public decimal NQTY_BAL2 { get; set; }
        public decimal NQTY_BAL3 { get; set; }
        public decimal NQTY_BEGINNING1 { get; set; }
        public decimal NQTY_BEGINNING2 { get; set; }
        public decimal NQTY_BEGINNING3 { get; set; }
        public string CUNIT1 { get; set; }
        public string CUNIT2 { get; set; }
        public string CUNIT3 { get; set; }
    }
}