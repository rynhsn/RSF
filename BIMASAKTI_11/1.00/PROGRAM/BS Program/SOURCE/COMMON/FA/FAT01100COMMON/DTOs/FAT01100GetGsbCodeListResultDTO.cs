namespace FAT01100Common.DTOs
{
    /// <summary>
    /// Result DTO for FAT01100GetGsbCodeList method (RSP_GS_GET_GSB_CODE_LIST) - list
    /// </summary>
    public class FAT01100GetGsbCodeListResultDTO
    {
        public string CCODE { get; set; } = string.Empty;
        public string CNAME { get; set; } = string.Empty;
    }
}
