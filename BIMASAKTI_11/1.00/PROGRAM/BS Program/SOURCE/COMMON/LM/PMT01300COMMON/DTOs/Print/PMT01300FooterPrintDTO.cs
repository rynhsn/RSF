using System;

namespace PMT01300COMMON
{
    public class PMT01300FooterPrintDTO
    {
        public string CTOTAL_AMOUNT_IN_WORDS { get; set; }
        public string CTRANS_DESC { get; set; }
        public decimal NTAXABLE_AMOUNT { get; set; }
        public decimal NTAX { get; set; }
        public decimal NOTHER_TAX { get; set; }
        public decimal NADDITION { get; set; }
        public decimal NDEDUCTION { get; set; }
        public decimal NTRANS_AMOUNT { get; set; }
    }
}
