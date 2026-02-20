namespace FAT01100Common.DTOs
{
    /// <summary>
    /// Parameter DTO for FAT01100GetDeptLookupList method (RSP_GS_GET_DEPT_LOOKUP_LIST)
    /// </summary>
    public class FAT01100GetDeptLookupListParameterDTO
    {
        public string CCOMPANY_ID { get; set; } = string.Empty;
        public string CUSER_ID { get; set; } = string.Empty;
        public string CPROGRAM_ID { get; set; } = string.Empty;
    }
}
