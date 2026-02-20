namespace FAT01100Common.DTOs
{
    /// <summary>
    /// Parameter DTO for FAT01100GetGetSystemParam method (RSP_FA_GET_SYSTEM_PARAM)
    /// </summary>
    public class FAT01100GetGetSystemParamParameterDTO
    {
        public string CCOMPANY_ID { get; set; } = string.Empty;
        public string CLANGUAGE_ID { get; set; } = string.Empty;
    }
}
