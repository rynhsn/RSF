using System;
using System.Collections.Generic;
using System.Text;

namespace PMR00150COMMON.Utility_Report
{
    public class PMR00150DBParamDTO : PMR00150ParameterVisibleDTO
    {
        public string? CCOMPANY_ID { get; set; }
        public string? CUSER_ID { get; set; }
        public string? CLANG_ID { get; set; }
        public string CPROPERTY_ID { get; set; } = "";
        public string CPROPERTY_NAME { get; set; } = "";


        public string CFROM_DEPARTMENT_ID { get; set; } = "";
        public string CFROM_DEPARTMENT_NAME { get; set; } = "";
        public string CTO_DEPARTMENT_ID { get; set; } = "";
        public string CTO_DEPARTMENT_NAME { get; set; } = "";
        public string CFROM_SALESMAN_ID { get; set; } = "";
        public string CFROM_SALESMAN_NAME { get; set; } = "";
        public string CTO_SALESMAN_ID { get; set; } = "";
        public string CTO_SALESMAN_NAME { get; set; } = "";
        public string CFROM_PERIOD { get; set; } = "";
        public string CTO_PERIOD { get; set; } = "";
        public string CREPORT_TYPE { get; set; } = "";
        public string CREPORT_NAME{ get; set; } = "";
        public string CFROM_PERIOD_NAME { get; set; } = "";
        public string CTO_PERIOD_NAME { get; set; } = "";
        public bool LIS_PRINT { get; set; }
        public string CREPORT_FILENAME { get; set; } = "";
        public string CREPORT_FILETYPE { get; set; } = "";
    }
}
