using System;

namespace ICR00600Common.DTOs.Print
{
    public class ICR00600ReportParam
    {
        public string? CPROPERTY_ID { get; set; } = "";
        public string? CCOMPANY_ID { get; set; } = "";
        public string? CUSER_ID { get; set; } = "";
        public string? CDEPT_CODE { get; set; } = "";
        public string? CPERIOD { get; set; } = "";
        public string? COPTION_PRINT { get; set; } = "";
        public string? CFROM_REF_NO { get; set; } = "";
        public string? CTO_REF_NO { get; set; } = "";
        public string? CLANG_ID { get; set; } = "";
        
        #region additional
        public bool LDEPT { get; set; } 
        public int IPERIOD_YEAR { get; set; }
        public string? COPTION_PRINT_NAME { get; set; } = "";
        public string? CPERIOD_MONTH { get; set; } = "";
        public string? CPROPERTY_NAME { get; set; } = "";
        public string? CDEPT_NAME { get; set; } = "";
        #endregion
        
        public string? CREPORT_FILETYPE { get; set; } = "";
        public string? CREPORT_FILENAME { get; set; } = "";
        public bool LIS_PRINT { get; set; } = false;

    }
}