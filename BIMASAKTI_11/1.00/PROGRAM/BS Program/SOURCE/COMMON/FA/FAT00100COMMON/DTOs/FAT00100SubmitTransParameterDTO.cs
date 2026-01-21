namespace FAT00100Common.DTOs
{
    /// <summary>
    /// Parameter DTO for FAT00100SubmitTrans method
    /// </summary>
    public class FAT00100SubmitTransParameterDTO
    {
        public string CCOMPANY_ID { get; set; } = string.Empty;
        public string CUSER_ID { get; set; } = string.Empty;
        public string CREC_ID { get; set; } = string.Empty;
    }
}
