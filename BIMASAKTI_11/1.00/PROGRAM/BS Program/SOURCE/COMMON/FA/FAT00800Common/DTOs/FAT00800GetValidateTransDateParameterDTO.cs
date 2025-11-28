namespace FAT00800Common.DTOs
{
    /// <summary>
    /// Parameter DTO for GetValidateTransDate method
    /// </summary>
    public class FAT00800GetValidateTransDateParameterDTO
    {
        // Standard properties (ALWAYS at top)
        public string CCOMPANY_ID { get; set; } = string.Empty;
        public string CLANG_ID { get; set; } = string.Empty;
        public string CUSER_ID { get; set; } = string.Empty;

        // Method-specific properties
        public string CTRANSACTION_DATE { get; set; } = string.Empty;
    }
}

