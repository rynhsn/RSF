namespace GLR00300Common
{
    public class GLR00300ParamDBToGetReportDTO
    {
        public string? CUSER_ID { get; set; }
        public string? CCOMPANY_ID { get; set; }
        public string? CLANGUAGE_ID { get; set; }
        public string? CTB_TYPE_CODE { get; set; }
        public string? CJOURNAL_ADJ_MODE_CODE { get; set; }
        public string? CCURRENCY_TYPE_CODE { get; set; }
        public string? CFROM_ACCOUNT_NO { get; set; }
        public string? CTO_ACCOUNT_NO { get; set; }
        public string?   CFROM_CENTER_CODE { get; set; }
        public string? CTO_CENTER_CODE { get; set; }
        public string? CYEAR { get; set; }
        public string? CFROM_PERIOD_NO { get; set; }
        public string? CTO_PERIOD_NO { get; set; }
        public string? CPRINT_METHOD_CODE { get; set; }
        public string? CBUDGET_NO { get; set; }
        public bool LIS_PRINT { get; set; }
        public string CREPORT_FILENAME { get; set; } = "";
        public string CREPORT_FILETYPE { get; set; } = "";
    }
}