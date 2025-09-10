using System;

namespace APR00300Common.DTOs.Print
{
    public class APR00300ReportParam
    {
        public string CPROPERTY_ID { get; set; } = "";
        public string CPROPERTY_NAME { get; set; } = "";

        public string CFROM_SUPPLIER_ID { get; set; } = "";
        public string CFROM_SUPPLIER_NAME { get; set; } = "";

        public string CTO_SUPPLIER_ID { get; set; } = "";
        public string CTO_SUPPLIER_NAME { get; set; } = "";

        public string CDATE_BASED_ON { get; set; } = "";

        public string CCUT_OFF_DATE { get; set; } = "";
        public DateTime? DCUT_OFF_DATE { get; set; }

        public string CFROM_PERIOD { get; set; } = "";
        public int IFROM_YEAR { get; set; }
        public string CFROM_MONTH { get; set; } = "";
        public string CFROM_MONTH_DISPLAY { get; set; } = "";

        public string CTO_PERIOD { get; set; } = "";
        public int ITO_YEAR { get; set; }
        public string CTO_MONTH { get; set; } = "";
        public string CTO_MONTH_DISPLAY { get; set; } = "";

        // public string CSTATEMENT_DATE { get; set; } = "";
        public DateTime? DSTATEMENT_DATE { get; set; }
        public bool LINCLUDE_ZERO_BALANCE { get; set; } = false;
        public bool LSHOW_AGE_TOTAL { get; set; } = false;

        public string CREPORT_FILETYPE { get; set; } = "";
        public string CREPORT_FILENAME { get; set; } = "";
        public bool LIS_PRINT { get; set; } = true;

        public string CCOMPANY_ID { get; set; } = "";
        public string CLANG_ID { get; set; } = "";
        public string CREPORT_CULTURE { get; set; } = "";
        public string CUSER_ID { get; set; } = "";
    }
}