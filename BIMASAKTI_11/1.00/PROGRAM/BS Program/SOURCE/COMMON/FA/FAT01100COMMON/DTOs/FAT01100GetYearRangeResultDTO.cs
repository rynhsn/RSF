namespace FAT01100Common.DTOs
{
    /// <summary>
    /// Result DTO for FAT01100GetYearRange method (RSP_GS_GET_PERIOD_YEAR_RANGE)
    /// </summary>
    public class FAT01100GetYearRangeResultDTO
    {
        public int IMIN_YEAR { get; set; }
        public int IMAX_YEAR { get; set; }
    }
}
