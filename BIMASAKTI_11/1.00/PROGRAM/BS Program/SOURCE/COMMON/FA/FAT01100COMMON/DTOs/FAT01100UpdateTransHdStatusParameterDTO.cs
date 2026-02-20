namespace FAT01100Common.DTOs
{
    /// <summary>
    /// Parameter DTO for FAT01100UpdateTransHdStatus method (RSP_FA_UPDATE_TRANS_HD_STATUS)
    /// </summary>
    public class FAT01100UpdateTransHdStatusParameterDTO
    {
        public string CCOMPANY_ID { get; set; } = string.Empty;
        public string CUSER_ID { get; set; } = string.Empty;
        public string CREC_ID { get; set; } = string.Empty;
        public string CNEW_STATUS { get; set; } = string.Empty;
    }
}
