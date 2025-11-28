namespace FAT00800Common.DTOs
{
    /// <summary>
    /// Parameter DTO for GetPeriod method
    /// </summary>
    public class FAT00800GetPeriodParameterDTO
    {
        // Standard properties (ALWAYS at top)
        public string CCOMPANY_ID { get; set; } = string.Empty;
        public string CLANG_ID { get; set; } = string.Empty;
        public string CUSER_ID { get; set; } = string.Empty;
    }
}

