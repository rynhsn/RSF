namespace PMT01900Common.DTO.Front
{
    public class PMT01900ParameterFrontChangePageToChargesDTO
    {
        public string? CPROPERTY_ID { get; set; }
        public string? CDEPT_CODE { get; set; }
        public string? CREF_NO { get; set; }
        public string? CTRANS_CODE { get; set; } = "";
        public string? CBUILDING_ID { get; set; } = "";
        public string CCHARGE_MODE { get; set; } = "";
        public string? CFLOOR_ID { get; set; }
        public string? CUNIT_ID { get; set; }
        // ini buat Charges di LOC 
        public decimal NTOTAL_ACTUAL_AREA { get; set; }
        public string? CREF_DATE { get; set; }
        public string? CSTART_DATE { get; set; }
        public string? CEND_DATE { get; set; }
        public int IYEARS { get; set; }
        public int IMONTHS { get; set; }
        public int IDAYS { get; set; }
    }
}
