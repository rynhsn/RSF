namespace FAT00800Common.DTOs
{
    /// <summary>
    /// Parameter DTO for FAT00800GetYearRange method
    /// </summary>
    public class FAT00800GetYearRangeParameterDTO
    {
        // Standard properties (ALWAYS at top)
        public string CCOMPANY_ID { get; set; } = string.Empty;
        public string CLANG_ID { get; set; } = string.Empty;
        public string CUSER_ID { get; set; } = string.Empty;

        // Method-specific properties
        public string CCYEAR { get; set; } = string.Empty;
        public string CMODE { get; set; } = string.Empty;
    }
}
