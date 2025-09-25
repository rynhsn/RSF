namespace PMT01700COMMON.DTO.Utilities.Front
{
    public class PMT01700ParameterChargesTab : PMT01700ParameterFrontChangePageDTO
    {
        public int IYEARS { get; set; }
        public int IMONTHS { get; set; }
        public int IDAYS { get; set; }
        public string? CSTART_DATE { get; set; }
        public string? CEND_DATE { get; set; }
        public decimal NTOTAL_GROSS_AREA { get; set; }
    }
}
