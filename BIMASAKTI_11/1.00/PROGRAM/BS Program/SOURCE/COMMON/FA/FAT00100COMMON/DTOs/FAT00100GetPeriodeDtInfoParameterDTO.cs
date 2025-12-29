namespace FAT00100Common.DTOs
{
    /// <summary>
    /// Parameter DTO for FAT00100GetPeriodeDtInfo method
    /// </summary>
    public class FAT00100GetPeriodeDtInfoParameterDTO
    {
        // Standard properties (ALWAYS at top)
        public string CCOMPANY_ID { get; set; } = string.Empty;
        public string CLANG_ID { get; set; } = string.Empty;
        public string CUSER_ID { get; set; } = string.Empty;

        // Method-specific properties
        public string CYEAR { get; set; } = string.Empty;
        public string CPERIOD_NO { get; set; } = string.Empty;
    }
}

