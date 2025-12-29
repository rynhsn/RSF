namespace FAT00100Common.DTOs
{
    /// <summary>
    /// Parameter DTO for FAT00100GetTransAsset method
    /// </summary>
    public class FAT00100GetTransAssetParameterDTO
    {
        public string CCOMPANY_ID { get; set; } = string.Empty;
        public string CREC_ID { get; set; } = string.Empty;
        public string CDEPT_CODE { get; set; } = string.Empty;
        public string CREF_NO { get; set; } = string.Empty;
        public string CTRANS_SEQ_NO { get; set; } = string.Empty;
        public string CLANGUAGE_ID { get; set; } = string.Empty;
    }
}

