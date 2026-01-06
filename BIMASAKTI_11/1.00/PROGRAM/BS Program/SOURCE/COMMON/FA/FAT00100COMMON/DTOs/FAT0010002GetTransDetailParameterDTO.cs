using System;

namespace FAT00100Common.DTOs
{
    /// <summary>
    /// Parameter DTO for GetTransDetail method
    /// </summary>
    public class FAT0010002GetTransDetailParameterDTO
    {
        public string CCOMPANY_ID { get; set; } = string.Empty;
        public string CREC_ID { get; set; } = string.Empty;
        public string CDEPT_CODE { get; set; } = string.Empty;
        public string CREF_NO { get; set; } = string.Empty;
        public string CLANGUAGE_ID { get; set; } = string.Empty;
    }
}

