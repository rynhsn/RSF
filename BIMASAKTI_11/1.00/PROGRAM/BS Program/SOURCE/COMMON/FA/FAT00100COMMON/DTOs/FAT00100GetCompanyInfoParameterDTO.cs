namespace FAT00100Common.DTOs
{
    /// <summary>
    /// Parameter DTO for FAT00100GetCompanyInfo method
    /// </summary>
    public class FAT00100GetCompanyInfoParameterDTO
    {
        // Standard properties (ALWAYS at top)
        public string CCOMPANY_ID { get; set; } = string.Empty;
        public string CLANG_ID { get; set; } = string.Empty;
        public string CUSER_ID { get; set; } = string.Empty;
    }
}

