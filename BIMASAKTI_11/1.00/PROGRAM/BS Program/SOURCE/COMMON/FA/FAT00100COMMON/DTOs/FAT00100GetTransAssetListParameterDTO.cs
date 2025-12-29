namespace FAT00100Common.DTOs
{
    /// <summary>
    /// Parameter DTO for FAT00100GetTransAssetList method
    /// </summary>
    public class FAT00100GetTransAssetListParameterDTO
    {
        public string CCOMPANY_ID { get; set; } = string.Empty;
        public string CREC_ID { get; set; } = string.Empty;
        public string CDEPT_CODE { get; set; } = string.Empty;
        public string CREF_NO { get; set; } = string.Empty;
        public string CLANGUAGE_ID { get; set; } = string.Empty;
    }
}

