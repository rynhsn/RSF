namespace FAT00100Common.DTOs
{
    /// <summary>
    /// Result DTO for FAT00100GetPeriodeDtInfo method
    /// </summary>
    public class FAT00100GetPeriodeDtInfoResultDTO
    {
        public string CCYEAR { get; set; } = string.Empty;
        public string CPERIOD_NO { get; set; } = string.Empty;
        public string CSTART_DATE { get; set; } = string.Empty;
        public string CEND_DATE { get; set; } = string.Empty;
    }
}

