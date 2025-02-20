using System;

namespace HDR00200Common.DTOs.Print
{
    public class HDR00200ReportParam
    {
        public string CCOMPANY_ID { get; set; }
        public string CPROPERTY_ID { get; set; }
        public string CPROPERTY_NAME { get; set; }
        public string CREPORT_TYPE { get; set; }
        public string CREPORT_TYPE_NAME { get; set; }
        public string CAREA { get; set; }
        public string CAREA_NAME { get; set; }
        public string CFROM_BUILDING_ID { get; set; }
        public string CFROM_BUILDING_NAME { get; set; }
        public string CTO_BUILDING_ID { get; set; }
        public string CTO_BUILDING_NAME { get; set; }
        public string CFROM_DEPT_CODE { get; set; }
        public string CFROM_DEPT_NAME { get; set; }
        public string CTO_DEPT_CODE { get; set; }
        public string CTO_DEPT_NAME { get; set; }
        public string? CFROM_PERIOD { get; set; }
        public DateTime? DFROM_PERIOD { get; set; }
        public string? CTO_PERIOD { get; set; }
        public DateTime? DTO_PERIOD { get; set; }
        public string CCATEGORY { get; set; }
        public string CSTATUS { get; set; }
        public string CUSER_ID { get; set; }
        public string CLANG_ID { get; set; }
        public bool LCOMPLAINT { get; set; }
        public bool LREQUEST { get; set; }
        public bool LINQUIRY { get; set; }
        public bool LHANDOVER { get; set; }
        public bool LOPEN { get; set; }
        public bool LSUBMITTED { get; set; }
        public bool LASSIGNED { get; set; }
        public bool LON_PROGRESS { get; set; }
        public bool LSOLVED { get; set; }
        public bool LCOMPLETED { get; set; }
        public bool LCONFIRMED { get; set; }
        public bool LCLOSED { get; set; }
        public bool LCANCELLED { get; set; }
        public bool LTERMINATED { get; set; }

        public bool LIS_PRINT { get; set; }
        public string CREPORT_FILENAME { get; set; } = "";
        public string CREPORT_FILETYPE { get; set; } = "";
    }
}