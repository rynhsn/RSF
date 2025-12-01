namespace FAT00100Common.DTOs
{
    /// <summary>
    /// Parameter DTO for GetDataHeader method
    /// </summary>
    public class FAT0010003GetDataHeaderParameterDTO
    {
        // Standard properties (ALWAYS at top)
        public string CCOMPANY_ID { get; set; } = string.Empty;
        public string CLANG_ID { get; set; } = string.Empty;
        public string CUSER_ID { get; set; } = string.Empty;

        // Method-specific parameters
        public string CFOREIGN_LANGUAGE { get; set; } = string.Empty;
        public string PCFR_DEPT_CODE { get; set; } = string.Empty;
        public string PCFR_TRANSACTION_CODE { get; set; } = string.Empty;
        public string PCFR_REFERENCE_NO { get; set; } = string.Empty;
    }
}

