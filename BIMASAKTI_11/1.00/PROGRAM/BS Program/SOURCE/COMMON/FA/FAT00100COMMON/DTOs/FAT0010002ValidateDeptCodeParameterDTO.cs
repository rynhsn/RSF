namespace FAT00100Common.DTOs
{
    /// <summary>
    /// Parameter DTO for ValidateDeptCode method
    /// </summary>
    public class FAT0010002ValidateDeptCodeParameterDTO
    {
        public string CCOMPANY_ID { get; set; } = string.Empty;
        public string CLANG_ID { get; set; } = string.Empty;
        public string CUSER_ID { get; set; } = string.Empty;
        public string CDEPT_CODE { get; set; } = string.Empty;
    }
}

