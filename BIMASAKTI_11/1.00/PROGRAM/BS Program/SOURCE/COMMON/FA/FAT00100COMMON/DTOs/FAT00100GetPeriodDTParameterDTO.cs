namespace FAT00100Common.DTOs
{
    /// <summary>
    /// Parameter DTO for GetPeriodDT method
    /// </summary>
    public class FAT00100GetPeriodDTParameterDTO
    {
        // Standard properties (ALWAYS at top)
        public string CCOMPANY_ID { get; set; } = string.Empty;
        public string CLANG_ID { get; set; } = string.Empty;
        public string CUSER_ID { get; set; } = string.Empty;

        // Method-specific properties
        public string CTRANSACTION_DATE { get; set; } = string.Empty;
    }
}

