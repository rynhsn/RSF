namespace FAT01100Common.DTOs
{
    /// <summary>
    /// Parameter DTO for FAT01100GetGsbCodeList method (RSP_GS_GET_GSB_CODE_LIST)
    /// </summary>
    public class FAT01100GetGsbCodeListParameterDTO
    {
        public string CAPPLICATION { get; set; } = string.Empty;
        public string CCOMPANY_ID { get; set; } = string.Empty;
        public string CCLASS_ID { get; set; } = string.Empty;
        public string CLANGUAGE_ID { get; set; } = string.Empty;
        public string CREC_ID_LIST { get; set; } = string.Empty;
    }
}
