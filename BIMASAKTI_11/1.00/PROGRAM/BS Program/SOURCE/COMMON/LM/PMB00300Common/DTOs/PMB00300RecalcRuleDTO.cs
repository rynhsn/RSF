namespace PMB00300Common.DTOs
{
    public class PMB00300RecalcRuleDTO
    {
        public string CSEQ_NO { get; set; } = "";
        public string CSTART_DATE { get; set; } = "";
        public string CEND_DATE { get; set; } = "";
        public string CPERIOD { get; set; } = "";
        public decimal NAMOUNT_BEFORE { get; set; }
        public decimal NTAX_AMOUNT_BEFORE { get; set; }
        public decimal NAFTER_TAX_AMOUNT_BEFORE { get; set; }
        public decimal NBOOKING_AMOUNT_BEFORE { get; set; }
        public decimal NTOTAL_AMOUNT_BEFORE { get; set; }
        public decimal NAMOUNT_AFTER { get; set; }
        public decimal NTAX_AMOUNT_AFTER { get; set; }
        public decimal NAFTER_TAX_AMOUNT_AFTER { get; set; }
        public decimal NBOOKING_AMOUNT_AFTER { get; set; }
        public decimal NTOTAL_AMOUNT_AFTER { get; set; }
    }
}