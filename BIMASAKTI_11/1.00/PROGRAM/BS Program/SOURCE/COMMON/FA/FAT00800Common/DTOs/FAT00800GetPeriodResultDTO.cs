namespace FAT00800Common.DTOs
{
    /// <summary>
    /// Result DTO for GetPeriod method
    /// </summary>
    public class FAT00800GetPeriodResultDTO
    {
        public string CDEFAULT_TRX_DEPT_CODE { get; set; } = string.Empty;
        public string CSOFT_PERIOD { get; set; } = string.Empty;
        public string CCURRENT_PERIOD { get; set; } = string.Empty;
        public string CGLLINK_DATE { get; set; } = string.Empty;
        public string CRATETYPE_CODE { get; set; } = string.Empty;
    }
}

