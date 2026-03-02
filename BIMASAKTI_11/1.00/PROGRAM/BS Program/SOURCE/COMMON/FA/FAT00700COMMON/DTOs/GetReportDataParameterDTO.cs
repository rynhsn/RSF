namespace FAT00700Common.DTOs
{
    /// <summary>
    /// Parameter DTO for FAT00700 GetReportData method in Back layer
    /// </summary>
    public class GetReportDataParameterDTO
    {
        // Required standard properties (ALWAYS include)
        public string CCOMPANY_ID { get; set; } = string.Empty;
        public string CLANGID { get; set; } = string.Empty;
        public string CUSER_ID { get; set; } = string.Empty;

        // Print parameters
        public string CDEPT_CODE { get; set; } = string.Empty;
        public string CTRANSACTION_CODE { get; set; } = string.Empty;
        public string CREFERENCE_NO { get; set; } = string.Empty;
        public string CASSET_CODE { get; set; } = string.Empty;

        // Optional filter parameters
        public string CFROM_DATE { get; set; } = string.Empty;
        public string CTO_DATE { get; set; } = string.Empty;
        public bool LPRINT_DETAIL { get; set; } = true;
        public bool LPRINT_SUMMARY { get; set; } = false;
    }
}

