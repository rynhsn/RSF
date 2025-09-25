using System;

namespace PMT01300COMMON
{
    public class PMT01300DetailPrintDTO
    {
        public int INO { get; set; }
        public string CPRODUCT_ID { get; set; }
        public string CPRODUCT_NAME { get; set; }
        public string CDETAIL_DESC { get; set; }
        public decimal NBILL_UNIT_QTY { get; set; }
        public string CUNIT { get; set; }
        public decimal NUNIT_PRICE { get; set; }
        public decimal NLINE_AMOUNT { get; set; }
        public decimal NTOTAL_DISCOUNT { get; set; }
        public decimal NLINE_TAXABLE_AMOUNT { get; set; }
    }
}
