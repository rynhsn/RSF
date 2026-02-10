namespace FAT00300Common.Requests
{
    public class FAT00300VoidProcessParameterDTO
    {
        // Required standard properties (ALWAYS include)
        public string CCOMPANY_ID { get; set; } = string.Empty;
        public string CLANG_ID { get; set; } = string.Empty;
        public string CUSER_ID { get; set; } = string.Empty;

        public string CDEPT_CODE { get; set; } = string.Empty;
        public string CTRANS_CODE { get; set; } = string.Empty;
        public string CREFERENCE_NO { get; set; } = string.Empty;
        public string CCANCEL_REASON_CODE { get; set; } = string.Empty;
        public string CCANCEL_APPROVED_BY { get; set; } = string.Empty;
    }
}







