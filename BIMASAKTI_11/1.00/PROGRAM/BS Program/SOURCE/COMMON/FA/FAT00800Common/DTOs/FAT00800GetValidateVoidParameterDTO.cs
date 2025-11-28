namespace FAT00800Common.DTOs
{
    /// <summary>
    /// Parameter DTO for GetValidateVoid method
    /// </summary>
    public class FAT00800GetValidateVoidParameterDTO
    {
        // Standard properties (ALWAYS at top)
        public string CCOMPANY_ID { get; set; } = string.Empty;
        public string CLANG_ID { get; set; } = string.Empty;
        public string CUSER_ID { get; set; } = string.Empty;

        // Method-specific properties
        public string CASSET_CODE { get; set; } = string.Empty;
        public string CASSET_TRANS_SEQNO { get; set; } = string.Empty;
    }
}

