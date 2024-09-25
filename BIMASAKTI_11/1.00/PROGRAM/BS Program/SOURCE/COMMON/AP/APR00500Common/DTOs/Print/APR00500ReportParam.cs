namespace APR00500Common.DTOs.Print
{
    public class APR00500ReportParam
    {
        public string CCOMPANY_ID { get; set; } = "";
        public string CPROPERTY_ID { get; set; } = "";
        public string CCUT_OFF_DATE { get; set; } = "";
        public string CDEPT_CODE { get; set; } = "";
        public string CFROM_PERIOD { get; set; } = "";
        public string CTO_PERIOD { get; set; } = "";
        public string CFROM_REFERENCE_DATE { get; set; } = "";
        public string CTO_REFERENCE_DATE { get; set; } = "";
        public string CFROM_DUE_DATE { get; set; } = "";
        public string CTO_DUE_DATE { get; set; } = "";
        public string CSUPPLIER_ID { get; set; } = "";
        public string CFROM_REFERENCE_NO { get; set; } = "";
        public string CTO_REFERENCE_NO { get; set; } = "";
        public string CCURRENCY { get; set; } = "";
        public decimal NFROM_TOTAL_AMOUNT { get; set; }
        public decimal NTO_TOTAL_AMOUNT { get; set; }
        public decimal NFROM_REMAINING_AMOUNT { get; set; }
        public decimal NTO_REMAINING_AMOUNT { get; set; }
        public int IFROM_DAYS_LATE { get; set; }
        public int ITO_DAYS_LATE { get; set; }
        public string CLANG_ID { get; set; } = "";
    }
}