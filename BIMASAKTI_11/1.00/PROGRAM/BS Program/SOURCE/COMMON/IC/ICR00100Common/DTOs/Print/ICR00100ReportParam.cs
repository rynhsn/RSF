using System;
using System.Data.Common;

namespace ICR00100Common.DTOs.Print
{
    public class ICR00100ReportParam
    {
        public string CPROPERTY_ID { get; set; } = ""; // All
        public string CPROPERTY_NAME { get; set; } = ""; // All
        public string CDATE_FILTER { get; set; } = ""; // PERIOD/DATE
        public string CDATE_FILTER_DESC { get; set; } = ""; // Period/Date
        
        public int IPERIOD_YEAR { get; set; }
        public string? CPERIOD_MONTH { get; set; } = ""; // 01-12
        public string CPERIOD { get; set; } = ""; // yyyyMM
        
        public DateTime? DFROM_DATE { get; set; }
        public DateTime? DTO_DATE { get; set; }
        public string? CFROM_DATE { get; set; } = ""; // yyyyMMdd
        public string? CTO_DATE { get; set; } = ""; // yyyyMMdd
        
        public string CWAREHOUSE_CODE { get; set; } = ""; // All
        public string CWAREHOUSE_NAME { get; set; } = ""; // All
        
        // public bool LDEPARTMENT { get; set; } // All
        public string CDEPT_CODE { get; set; } = ""; // All
        public string CDEPT_NAME { get; set; } = ""; // All
        
        public bool LINC_FUTURE_TRANSACTION { get; set; } // All
        
        public string CFILTER_BY { get; set; } = ""; // PROD, CATEGORY, JOURNAL
        public string CFILTER_BY_DESC { get; set; } = ""; // Product, Category, Journal Group
        
        public string CFROM_PROD_ID { get; set; } = ""; // All
        public string CFROM_PROD_NAME { get; set; } = ""; // All
        public string CTO_PROD_ID { get; set; } = ""; // All
        public string CTO_PROD_NAME { get; set; } = ""; // All
        
        public string CFILTER_DATA { get; set; } = ""; // All
        public string CFILTER_DATA_NAME { get; set; } = ""; // All
        
        public string CFILTER_DATA_CATEGORY { get; set; } = ""; // All
        public string CFILTER_DATA_CATEGORY_NAME { get; set; } = ""; // All
        public string CFILTER_DATA_JOURNAL { get; set; } = ""; // All
        public string CFILTER_DATA_JOURNAL_NAME { get; set; } = ""; // All
        
        public bool LINC_RV_AND_GR_REPLACEMENT { get; set; } // All
        public bool LINC_TRANS_DESC { get; set; } // All
        public bool LSUPRESS_TOTAL_QTY { get; set; } // All
        
        public string COPTION_PRINT { get; set; } = ""; // QTY, UNIT1
        public string COPTION_PRINT_DESC { get; set; } = ""; // By Qty Unit, By Unit 
        
        public string? CCOMPANY_ID { get; set; } = "";
        public string? CUSER_ID { get; set; } = "";
        public string? CLANG_ID { get; set; } = "";
        public string? CREPORT_FILETYPE { get; set; } = "";
        public string? CREPORT_FILENAME { get; set; } = "";
        public bool LIS_PRINT { get; set; } = false;
    }
}