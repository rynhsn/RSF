using System;

namespace FAT00100Common.DTOs
{
    /// <summary>
    /// Parameter DTO for DeleteTransAsset method
    /// </summary>
    public class FAT0010002DeleteTransAssetParameterDTO
    {
        public string CCOMPANY_ID { get; set; } = string.Empty;
        public string CREC_ID { get; set; } = string.Empty;
        public string CDEPT_CODE { get; set; } = string.Empty;
        public string CREF_NO { get; set; } = string.Empty;
        public string CTRANS_SEQ_NO { get; set; } = string.Empty;
    }
}

