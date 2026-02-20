namespace FAT01100Common.DTOs
{
    /// <summary>
    /// Parameter DTO for FAT01100GetTransCodeInfo method (RSP_GS_GET_TRANS_CODE_INFO)
    /// </summary>
    public class FAT01100GetTransCodeInfoParameterDTO
    {
        public string CCOMPANY_ID { get; set; } = string.Empty;
        public string CTRANS_CODE { get; set; } = string.Empty;
    }
}
