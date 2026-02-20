namespace FAT01100Common.DTOs
{
    /// <summary>
    /// Parameter DTO for FAT01100GeTransList method (RSP_FAT01100_GET_TRANS_LIST)
    /// </summary>
    public class FAT01100GeTransListParameterDTO
    {
        public string CCOMPANY_ID { get; set; } = string.Empty;
        public string CUSER_ID { get; set; } = string.Empty;
        public string CDEPT_CODE { get; set; } = string.Empty;
        public string CFROM_PERIOD { get; set; } = string.Empty;
        public string CTO_PERIOD { get; set; } = string.Empty;
        public string CASSET_CODE { get; set; } = string.Empty;
        public string CLANGUAGE_ID { get; set; } = string.Empty;
    }
}
