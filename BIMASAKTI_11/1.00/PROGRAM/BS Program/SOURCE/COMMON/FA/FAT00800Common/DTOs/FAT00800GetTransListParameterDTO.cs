namespace FAT00800Common.DTOs
{
    /// <summary>
    /// Parameter DTO for FAT00800GetTransList method
    /// </summary>
    public class FAT00800GetTransListParameterDTO
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
