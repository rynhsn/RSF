namespace FAT00100Common.DTOs
{
    /// <summary>
    /// Parameter DTO for ValidationAssetCode method
    /// </summary>
    public class FAT00100ValidationAssetCodeParameterDTO
    {
        // Standard properties (ALWAYS at top)
        public string CCOMPANY_ID { get; set; } = string.Empty;
        public string CLANG_ID { get; set; } = string.Empty;
        public string CUSER_ID { get; set; } = string.Empty;

        // Method-specific properties
        public string CDEPT_CODE { get; set; } = string.Empty;
        public string CFILTER_TRANS_CODE { get; set; } = string.Empty;
        public string CREFERENCE_NO { get; set; } = string.Empty;
    }
}

