namespace FAT00800Common.DTOs
{
    /// <summary>
    /// Parameter DTO for FAT00800GetTransCodeInfo method
    /// </summary>
    public class FAT00800GetTransCodeInfoParameterDTO
    {
        // Standard properties (ALWAYS at top)
        public string CCOMPANY_ID { get; set; } = string.Empty;
        public string CLANG_ID { get; set; } = string.Empty;
        public string CUSER_ID { get; set; } = string.Empty;

        // Method-specific properties
        public string CTRANS_CODE { get; set; } = string.Empty;
    }
}
