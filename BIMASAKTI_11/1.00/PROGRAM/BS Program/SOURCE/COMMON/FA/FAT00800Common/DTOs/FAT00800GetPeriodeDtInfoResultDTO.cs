namespace FAT00800Common.DTOs
{
    /// <summary>
    /// Result DTO for FAT00800GetPeriodeDtInfo method
    /// </summary>
    public class FAT00800GetPeriodeDtInfoResultDTO
    {
        public string CCYEAR { get; set; } = string.Empty;
        public string CPERIOD_NO { get; set; } = string.Empty;
        public string CSTART_DATE { get; set; } = string.Empty;
        public string CEND_DATE { get; set; } = string.Empty;
    }
}
