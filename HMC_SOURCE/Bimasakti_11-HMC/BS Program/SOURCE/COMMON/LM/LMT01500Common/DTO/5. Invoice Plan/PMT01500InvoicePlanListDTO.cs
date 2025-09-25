namespace PMT01500Common.DTO._5._Invoice_Plan
{
    public class PMT01500InvoicePlanListDTO
    {
        public string? CSEQ_NO { get; set; }
        public string? CSTART_DATE { get; set; }
        public string? CEND_DATE { get; set; }
        public string? CPERIOD { get; set; }
        public decimal NAMOUNT { get; set; }
        public decimal NTAX_AMOUNT { get; set; }
        public decimal NAFTER_TAX_AMOUNT { get; set; }
        public decimal NBOOKING_AMOUNT { get; set; }
        public decimal NTOTAL_AMOUNT { get; set; }
        public string? CSTATUS_DESCR { get; set; }
        public bool LINVOICED { get; set; }
        public string? CINV_NO { get; set; }
        public bool LPAYMENT { get; set; }
        public string? CPAYMENT_NO { get; set; }
        public decimal NPAYMENT_AMOUNT { get; set; }
        public string? CDESCRIPTION { get; set; }
    }
}
