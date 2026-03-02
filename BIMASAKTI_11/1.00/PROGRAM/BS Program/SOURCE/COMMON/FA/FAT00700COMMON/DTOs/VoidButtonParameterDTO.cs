namespace FAT00700Common.DTOs
{
    public class VoidButtonParameterDTO
    {
        // Required standard properties (ALWAYS include)
        public string CCOMPANY_ID { get; set; } = string.Empty;
        public string CLANGID { get; set; } = string.Empty;
        public string CUSER_ID { get; set; } = string.Empty;

        public string CDEPT_CODE { get; set; } = string.Empty;
        public string CTRANSACTION_CODE { get; set; } = string.Empty;
        public string CREFERENCE_NO { get; set; } = string.Empty;
        public string CCANCEL_REASON_CODE { get; set; } = string.Empty;
        public string CCANCEL_APPROVED_BY { get; set; } = string.Empty;
    }
}

