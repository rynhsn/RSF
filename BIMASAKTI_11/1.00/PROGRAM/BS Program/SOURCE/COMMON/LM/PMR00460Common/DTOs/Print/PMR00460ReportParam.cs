namespace PMR00460Common.DTOs.Print
{
    public class PMR00460ReportParam
    {
        public string CCOMPANY_ID { get; set; }
        public string CUSER_ID { get; set; }
        public bool LIS_PRINT { get; set; }
        public string CREPORT_FILETYPE { get; set; } = "";
        public string CREPORT_FILENAME { get; set; } = "";
        
        public string CPROPERTY_ID { get; set; }
        public string CPROPERTY_NAME { get; set; }
        public string CFROM_BUILDING_ID { get; set; }
        public string CFROM_BUILDING_NAME { get; set; }
        public string CTO_BUILDING_ID { get; set; } = "";
        public string CTO_BUILDING_NAME { get; set; }
        public string CFROM_PERIOD { get; set; }
        public string CTO_PERIOD { get; set; }
        public int IFROM_PERIOD_YEAR { get; set; }
        public string CFROM_PERIOD_MONTH { get; set; }
        public string CFROM_PERIOD_MONTH_NAME { get; set; }
        public int ITO_PERIOD_YEAR { get; set; }
        public string CTO_PERIOD_MONTH { get; set; }
        public string CTO_PERIOD_MONTH_NAME { get; set; }
        public string CREPORT_TYPE { get; set; }
        public string CREPORT_TYPE_NAME { get; set; }
        public string CTYPE { get; set; }
        public string CTYPE_NAME { get; set; }
        public string CLANG_ID { get; set; }
        
        public bool LOPEN { get; set; }
        public bool LSCHEDULED { get; set; }
        public bool LCONFIRMED { get; set; }
        public bool LCLOSED { get; set; }
    }
    
    
}