using System;

namespace PMR02600Common.Params
{
    public class PMR02600ReportParam
    {
        public string CCOMPANY_ID { get; set; } = ""; 
        public string CUSER_ID { get; set; } = ""; 
        public string CPROPERTY { get; set; } = ""; 
        public string CPROPERTY_NAME { get; set; } = "";
        public string CFROM_BUILDING { get; set; } = ""; 
        public string CFROM_BUILDING_NAME { get; set; } = "";
        public string CTO_BUILDING { get; set; } = ""; 
        public string CTO_BUILDING_NAME { get; set; } = "";
        public string CPERIOD { get; set; } = ""; 
        public DateTime? DPERIOD { get; set; }
        public string CLANG_ID { get; set; } = ""; 
        
        public string CREPORT_CULTURE { get; set; } = "";
        
        

        public string CREPORT_FILETYPE { get; set; } = "";
        public string CREPORT_FILENAME { get; set; } = "";
        public bool LIS_PRINT { get; set; } = true;
    }
}