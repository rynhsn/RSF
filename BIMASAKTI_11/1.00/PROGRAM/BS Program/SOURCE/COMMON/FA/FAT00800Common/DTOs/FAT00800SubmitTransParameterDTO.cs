namespace FAT00800Common.DTOs
{
    /// <summary>
    /// Parameter DTO for FAT00800SubmitTrans method (RSP_FAT00800_SUBMIT_TRANS)
    /// </summary>
    public class FAT00800SubmitTransParameterDTO
    {
        public string CCOMPANY_ID { get; set; } = string.Empty;
        public string CUSER_ID { get; set; } = string.Empty;
        public string CREC_ID { get; set; } = string.Empty;
    }
}
