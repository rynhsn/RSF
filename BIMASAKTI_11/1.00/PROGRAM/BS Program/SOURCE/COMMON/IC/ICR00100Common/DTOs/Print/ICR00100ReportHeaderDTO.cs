using System;

namespace ICR00100Common.DTOs.Print
{
    public class ICR00100ReportHeaderDTO
    {
        public string CPROPERTY { get; set; } = "";
        public string CDATE_FILTER { get; set; } = ""; // PERIOD/DATE
        public string CPERIOD { get; set; } = "";
        public string CDATE { get; set; }
        public DateTime? DDATE_FROM { get; set; }
        public DateTime? DDATE_TO { get; set; }
        public string CWAREHOUSE { get; set; } = "";
        public string CDEPARTMENT { get; set; } = "";
        public bool LINC_FUTURE_TRANSACTION { get; set; }
        public string CFILTER_BY { get; set; } = ""; // PROD, CATEGORY, JOURNAL
        public string CFILTER_BY_DESC { get; set; } = ""; // Product, Category, Journal Group
        public string CFILTER_DATA { get; set; } = ""; // All
        
        public string CSUPRESS_MODE { get; set; } = "";
        public string CSELECTED_BY { get; set; } = "";
        public string CPRODUCT { get; set; } = "";
        public string COPTION_PRINT { get; set; } = ""; // QTY, UNIT
        public string COPTION_PRINT_DESC { get; set; } = ""; // By Qty Unit, By Unit
    }
}