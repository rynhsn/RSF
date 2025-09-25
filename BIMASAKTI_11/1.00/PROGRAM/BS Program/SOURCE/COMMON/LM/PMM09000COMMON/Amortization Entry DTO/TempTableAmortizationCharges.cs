namespace PMM09000COMMON.Amortization_Entry_DTO
{
    public class TempTableAmortizationCharges
    {
        public string? CSEQ_NO { get; set; }
        public string? CCHARGES_TYPE_ID { get; set; }
        public string? CCHARGES_ID { get; set; }
        public string? CSTART_DATE { get; set; }
        public string? CEND_DATE { get; set; }
        public decimal NAMOUNT { get; set; } =0;
    }
}
