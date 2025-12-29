namespace FAT00100Common.DTOs
{
    /// <summary>
    /// Parameter DTO for FAT00100GetStatusList method
    /// </summary>
    public class FAT00100GetStatusListParameterDTO
    {
        public string CAPPLICATION { get; set; } = string.Empty;
        public string CCOMPANY_ID { get; set; } = string.Empty;
        public string CCLASS_ID { get; set; } = string.Empty;
        public string CLANGUAGE_ID { get; set; } = string.Empty;
        public string CREC_ID_LIST { get; set; } = string.Empty;
    }
}

