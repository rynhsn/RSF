namespace FAT01100Common.DTOs
{
    /// <summary>
    /// Parameter DTO for FAT01100GetYearRange method (RSP_GS_GET_PERIOD_YEAR_RANGE)
    /// </summary>
    public class FAT01100GetYearRangeParameterDTO
    {
        public string CCOMPANY_ID { get; set; } = string.Empty;
        public string CCYEAR { get; set; } = string.Empty;
        public string CMODE { get; set; } = string.Empty;
    }
}
