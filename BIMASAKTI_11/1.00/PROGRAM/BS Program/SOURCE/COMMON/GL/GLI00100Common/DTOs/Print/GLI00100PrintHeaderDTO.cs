using System.Collections.Generic;

namespace GLI00100Common.DTOs.Print
{
    public class GLI00100PrintResultDTO
    {
        public string Title { get; set; }
        public GLI00100PrintHeaderTitleDTO HeaderTitle { get; set; }
        // public GLI00100PrintHeaderDTO Header { get; set; }
        public GLI00100PrintColumnDTO Column { get; set; }
        public GLI00100PrintRowDTO Row { get; set; }
        public GLI00100AccountAnalysisDTO Data { get; set; }
    }
    
    public class GLI00100PrintRowDTO
    {
        public string OPENING { get; set; } = "Opening";
        public string PERIOD_01 { get; set; } = "Period 01";
        public string PERIOD_02 { get; set; } = "Period 02";
        public string PERIOD_03 { get; set; } = "Period 03";
        public string PERIOD_04 { get; set; } = "Period 04";
        public string PERIOD_05 { get; set; } = "Period 05";
        public string PERIOD_06 { get; set; } = "Period 06";
        public string PERIOD_07 { get; set; } = "Period 07";
        public string PERIOD_08 { get; set; } = "Period 08";
        public string PERIOD_09 { get; set; } = "Period 09";
        public string PERIOD_10 { get; set; } = "Period 10";
        public string PERIOD_11 { get; set; } = "Period 11";
        public string PERIOD_12 { get; set; } = "Period 12";
        public string PERIOD_13 { get; set; } = "Period 13";
        public string PERIOD_14 { get; set; } = "Period 14";
        public string PERIOD_15 { get; set; } = "Period 15";
    } 

    public class GLI00100PrintColumnDTO
    {
        public string COLUMN_CURRENT_MONTH { get; set; } = "Current Month";
        public string COLUMN_BUDGET { get; set; } = "Budget";
        public string COLUMN_CURRENT_YEAR { get; set; } = "Current Year";
        public string COLUMN_LAST_YEAR { get; set; } = "Last Year";
    }

    public class GLI00100PrintHeaderDTO
    {
        public string CGLACCOUNT_NO { get; set; }
        public string CGLACCOUNT_NAME { get; set; }
        public string CBSIS_LONG_NAME { get; set; }
        public string CDBCR_LONG_NAME { get; set; }
        public string CYEAR { get; set; }
        public string CCENTER { get; set; }
        public string CBUDGET { get; set; }
        public string CCURRENCY { get; set; }
    }

    public class GLI00100PrintHeaderTitleDTO
    {
        public string ACCOUNT_NO { get; set; } = "Account No.";
        public string ACCOUNT_TYPE { get; set; } = "Account Type";
        public string NORMAL_JOURNAL { get; set; } = "Normal Journal";
        public string YEAR { get; set; } = "Year";
        public string CENTER { get; set; } = "Center";
        public string BUDGET { get; set; } = "Budget No.";
        public string CURRENCY { get; set; } = "Currency";
    }

    public class GLI00100PrintWithBaseHeaderDTO : BaseHeaderReportCOMMON.BaseHeaderResult
    {
        public GLI00100PrintResultDTO Data { get; set; }
    }
}