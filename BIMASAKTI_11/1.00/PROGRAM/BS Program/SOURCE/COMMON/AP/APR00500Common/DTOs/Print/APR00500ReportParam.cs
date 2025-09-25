using System;

namespace APR00500Common.DTOs.Print
{
    public class APR00500ReportParam
    {
        public string CCOMPANY_ID { get; set; } = "";
        public string CPROPERTY_ID { get; set; } = "";
        public string CPROPERTY_NAME { get; set; } = "";
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

        // Helper
        public string CDEPT_NAME { get; set; } = "";
        public string CSUPPLIER_NAME { get; set; } = "";
        public string CCURRENCY_NAME { get; set; } = "";
        public DateTime? DCUT_OFF_DATE { get; set; }
        public int IFROM_PERIOD_YY { get; set; }
        public string CFROM_PERIOD_MM { get; set; } = "";
        public int ITO_PERIOD_YY { get; set; }
        public string CTO_PERIOD_MM { get; set; } = "";
        public DateTime? DFROM_REFERENCE_DATE { get; set; }
        public DateTime? DTO_REFERENCE_DATE { get; set; }
        public DateTime? DFROM_DUE_DATE { get; set; }
        public DateTime? DTO_DUE_DATE { get; set; }

        public string CREPORT_CULTURE { get; set; } = "";
        public string CUSER_ID { get; set; } = "";


        public string CREPORT_FILETYPE { get; set; } = "";
        public string CREPORT_FILENAME { get; set; } = "";
        public bool LIS_PRINT { get; set; } = true;
    }
}