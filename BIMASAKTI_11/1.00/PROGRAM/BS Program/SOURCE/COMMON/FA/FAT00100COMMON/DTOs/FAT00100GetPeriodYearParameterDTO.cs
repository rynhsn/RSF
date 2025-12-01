namespace FAT00100Common.DTOs
{
    /// <summary>
    /// Parameter DTO for GetPeriodYear method
    /// </summary>
    public class FAT00100GetPeriodYearParameterDTO
    {
        // Standard properties (ALWAYS at top)
        public string CCOMPANY_ID { get; set; } = string.Empty;
        public string CLANG_ID { get; set; } = string.Empty;
        public string CUSER_ID { get; set; } = string.Empty;

        // Method-specific properties
        public string CREFERENCE_NO { get; set; } = string.Empty;
        public string CTRANSACTION_PRD { get; set; } = string.Empty;
        public string CSOFT_PERIOD { get; set; } = string.Empty;
    }
}

