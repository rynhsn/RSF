namespace FAT01100Common.DTOs
{
    /// <summary>
    /// Parameter DTO for FAT01100SubmitTrans method (RSP_FAT01100_SUBMIT_TRANS)
    /// </summary>
    public class FAT01100SubmitTransParameterDTO
    {
        public string CCOMPANY_ID { get; set; } = string.Empty;
        public string CUSER_ID { get; set; } = string.Empty;
        public string CREC_ID { get; set; } = string.Empty;
    }
}
