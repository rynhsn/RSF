namespace FAT00100Common.DTOs
{
    /// <summary>
    /// Parameter DTO for GetInitialProcess method
    /// </summary>
    public class FAT00100GetInitialProcessParameterDTO
    {
        // Standard properties (ALWAYS at top)
        public string CCOMPANY_ID { get; set; } = string.Empty;
        public string CLANG_ID { get; set; } = string.Empty;
        public string CUSER_ID { get; set; } = string.Empty;

        // Method-specific properties
        public string CFOREIGN_LANGUAGE { get; set; } = string.Empty;
        public string CDEPT_CODE { get; set; } = string.Empty;
        public string CREFERENCE_NO { get; set; } = string.Empty;
        public string CTRANSACTION_CODE { get; set; } = string.Empty;
        public string CPJ_TRANS_CODE { get; set; } = string.Empty;
    }
}

