namespace FAT00100Common.DTOs
{
    /// <summary>
    /// Parameter DTO for FAT00100GetTransCodeInfo method
    /// </summary>
    public class FAT00100GetTransCodeInfoParameterDTO
    {
        // Standard properties (ALWAYS at top)
        public string CCOMPANY_ID { get; set; } = string.Empty;
        public string CLANG_ID { get; set; } = string.Empty;
        public string CUSER_ID { get; set; } = string.Empty;

        // Method-specific properties
        public string CTRANS_CODE { get; set; } = string.Empty;
    }
}

